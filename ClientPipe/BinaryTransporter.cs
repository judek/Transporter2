using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using MemoryCompressor;


//Taken from FTI 7:47 AM 1/22/2010
namespace ClientPipe
{
    public delegate void DoneDelegate(Exception exp);
    public delegate void ProgressDelegate(long nBytesSent);


    public class BinaryTransporter
    {

        enum MessageType { StartRequest, FileChunk};
        
        static uint TRANSPORTBLOB_HEADER = 0xF0F0;
        static byte[] _bFileChunkHeader = BitConverter.GetBytes(TRANSPORTBLOB_HEADER);
        static uint TRANSPORT_START_REQUEST = 0x0001;
        static byte[] _bStartRequestHeader = BitConverter.GetBytes(TRANSPORT_START_REQUEST);
        static uint TRANSPORT_FINISH_REQUEST = 0x000;
        static byte[] _bFinishRequestHeader = BitConverter.GetBytes(TRANSPORT_FINISH_REQUEST);
        //const long CHUNKSIZE = 1048576 * 8; //8Meg
        const long CHUNKSIZE = 1048576 * 2; //2Meg
        //const long CHUNKSIZE = 128 * 8;
        static byte[] _ChunkBuffer;

        

        string _SessionID;
        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }
        
        
        string _LisID;
        string _URL;
        WebProxy _Proxy;
        bool _blnUserAbortTransport = false;
        bool _blnSystemAbortTransport = false;
        string _SystemAbortMessage = "";
        bool _blnSendingAsync = false;
        string _sFilePath;
        string _sTargetFileName;
        DoneDelegate _doneDelegate = null;
        ProgressDelegate _progressDelegate = null;
        bool _blnDoCompression = false;
        long _TotalBytesSent = 0;
        string _sTargetFolder;


        public BinaryTransporter(string URL, WebProxy Proxy, string SessionID, string ListID, bool UseCompression)
        {
            _SessionID = SessionID;
            _URL = URL;
            _Proxy = Proxy;
            _LisID = ListID;
            _blnDoCompression = UseCompression;
        }

       

        public void AbortTransport()
        {
            _blnUserAbortTransport = true;
        }

        public void SendFile(string sFile)
        {
            _sFilePath = sFile;
            _doneDelegate = null;
            _progressDelegate = null;
            SendFile();
        }

        void ExceptionWrapedSendFile()
        {

            try
            {
                SendFile();
            }
            catch (Exception caughtExp)
            {

                if (null != _doneDelegate)
                    _doneDelegate(caughtExp);
            }
        }


        void SendFile()
        {

            string sTransmittFilePath = _sFilePath;
            //string sDestFilePath = args[2];
            FileInfo fInfo = null;
            long _lFileLength = 0;
            long _nChunk = 0;
            long _totalChunks = 0;


            FileStream fStream = null;
            BinaryReader br = null;


            #region Get Ready
            try
            {
                fInfo = new FileInfo(_sFilePath);

                if (fInfo.Length < 1)
                    throw new Exception("No Data in file");


                _lFileLength = fInfo.Length;

            }
            catch (Exception exp)
            {
                throw new Exception("Unable to open file:" + exp.Message);
            }



            //Console.WriteLine("Requesting transfer start...");

            string sId = _SessionID;


            if (sId.Length != 36)
                throw new Exception("Unable to start upload:Session not equal to 36 characters");

            #endregion

            _totalChunks = _lFileLength / CHUNKSIZE;

            //Console.WriteLine("Sending [" + fInfo.Name + "](" + _lFileLength + ")bytes...");
            //Console.WriteLine(CHUNKSIZE + " bytes per chunk, " + _totalChunks + " chunks total to send.");



            fStream = new FileStream(sTransmittFilePath, FileMode.Open, FileAccess.Read);
            br = new BinaryReader(fStream);
            


            string sSeverFeedBack = "";
            byte[] sendBuffer = null;

            _ChunkBuffer = br.ReadBytes((int)CHUNKSIZE);//Read first chunk
            sendBuffer = _ChunkBuffer;

            try
            {

                do
                {
                    if (true == _blnUserAbortTransport)
                        throw new Exception("Upload aborted by user");

                    if (true == _blnSystemAbortTransport)
                    {
                        throw new Exception("Upload aborted by system:" + _SystemAbortMessage);
                    }


                    if (sendBuffer.Length < 1)
                        break;
                    #region Rerty Loop
                    while (true)//retry chunk indefinitely
                    {
                        try
                        {
                            //Console.Write("\r\nSending chunk " + nChunk + " " + sendBuffer.Length + " bytes...");
                            //sSeverFeedBack = client.UploadChunk(sId, data, nChunk++);
                            sSeverFeedBack = sendchunk(_URL, sId, (_nChunk * CHUNKSIZE), sendBuffer, _blnDoCompression, MessageType.FileChunk);


                            if (true == sSeverFeedBack.StartsWith("ABORT"))
                            {
                                _SystemAbortMessage = sSeverFeedBack;
                                _blnSystemAbortTransport = true;
                                break;
                            }

                            if (false == sSeverFeedBack.StartsWith("OK"))
                                throw new Exception(sSeverFeedBack);//Failed for some reason

                            //all good chunk is complete
                            _nChunk++;
                            _ChunkBuffer = br.ReadBytes((int)CHUNKSIZE);//Read next
                            sendBuffer = _ChunkBuffer;

                            _TotalBytesSent += sendBuffer.Length;
                            if (_progressDelegate != null) _progressDelegate(_TotalBytesSent);
                            break;

                        }
                        catch (Exception exp)
                        {
                            if (_nChunk == 0)
                            {
                                _SystemAbortMessage = exp.Message;
                                _blnSystemAbortTransport = true;
                                break;
                            }

                            //Console.WriteLine(exp.Message);
                            //some sort of failure resend chunk
                            //Console.Write("\r\nWaiting before retry...");
                            System.Threading.Thread.Sleep(7000);//wait before just a bit before retry
                        }
                    }
                    #endregion

                } while (sendBuffer.Length > 0);




                _sTargetFileName = _sTargetFileName.PadRight(100, ' ');

                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

                sSeverFeedBack = sendchunk(_URL, sId, (-1), enc.GetBytes(_sTargetFolder + "/" + StripString(_sTargetFileName)), false, MessageType.FileChunk);

                if (false == sSeverFeedBack.StartsWith("OK|"))
                    throw new Exception("Communication Failure:" + sSeverFeedBack);




                if (_doneDelegate != null) _doneDelegate(null);

            }
            finally
            {
                if (br != null)
                    br.Close();

                if (fStream != null)
                {
                    fStream.Close();
                    fStream.Dispose();
                }
            }
        }

        public string RequestUploadStart(string sTargetFolderPath)
        {

            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            //sSeverFeedBack = sendchunk(_URL, sId, (-1), enc.GetBytes(_sTargetFolder + "/" + StripString(_sTargetFileName)), false, MessageType.FileChunk);


            string sSeverFeedBack = sendchunk(_URL, "", (-1), enc.GetBytes(sTargetFolderPath.Trim()), false, MessageType.StartRequest);

            if (false == sSeverFeedBack.StartsWith("OK|"))
                throw new Exception("Access Denied");


            string [] sSeverFeedBackVariables = sSeverFeedBack.Split('|');
            if (sSeverFeedBackVariables.Length != 2)
                throw new Exception("Invalid server response");

            return sSeverFeedBackVariables[1];
        }
        public void BeginSendFile(string sFileName, string sTargetFolder, string sTargetFileName, DoneDelegate doneDelegate, ProgressDelegate progressDelegate)
        {
            _doneDelegate = doneDelegate;
            _progressDelegate = progressDelegate;
            _sTargetFileName = sTargetFileName;
            _sTargetFolder = sTargetFolder;
            AsyncLog(sFileName);
        }

        void AsyncLog(string sFilePath)
        {
            _sFilePath = sFilePath;
            

            if (_blnSendingAsync == true)
                return;

            try
            {
                _blnSendingAsync = true;
                Thread logTread = new Thread(this.ExceptionWrapedSendFile);
                logTread.Start();
            }
            catch
            { _blnSendingAsync = false; }
        }





        string sendchunk(string receiverURL, string SessionID, long lFileOffset, byte[] pdata, bool blnCompress, MessageType messageType)
        {
            byte[] dataPayload = pdata;

            blnCompress &= _blnDoCompression;
            //Compress or not compress this chunk?
            if ((true == blnCompress) && (pdata.Length > (1048576 / 4) /*250k*/))
                dataPayload = Compressor.Compress(pdata, 0, pdata.Length);
            else
            {
                blnCompress = false;

            }


            string sServerResonpse = "Fail|";

            #region Prepare Request
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(receiverURL);
            Stream os = null;
            StreamReader sr = null;
            WebResponse resp = null;
            req.Accept = "text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
            req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.6) Gecko/20070725 Firefox/2.0.0.6";
            req.KeepAlive = true;
            req.Referer = receiverURL;
            req.ContentType = "application/octet-stream";
            req.Method = "POST";
            #endregion


            byte[] bFileOffset = BitConverter.GetBytes(lFileOffset);
            byte[] bCompress = BitConverter.GetBytes(blnCompress);
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            byte[] bSessionID = enc.GetBytes(SessionID);

            //To Do: this fixes the ListID to be 6 chars only - not good in the long run
            //need to handle this better with padding spaces or something to increase
            //the ability to send longer listids for the future.
            byte[] bListId = enc.GetBytes(_LisID);




            req.ContentLength = _bFileChunkHeader.Length +
                bFileOffset.Length +
                bCompress.Length +
                bSessionID.Length +
                bListId.Length +
                dataPayload.Length;


            os = req.GetRequestStream();

            if(messageType == MessageType.StartRequest)
                os.Write(_bStartRequestHeader, 0, _bStartRequestHeader.Length);
            else
                os.Write(_bFileChunkHeader, 0, _bFileChunkHeader.Length);
            
            
            
            os.Write(bFileOffset, 0, bFileOffset.Length);
            os.Write(bCompress, 0, bCompress.Length);
            os.Write(bSessionID, 0, bSessionID.Length);
            os.Write(bListId, 0, bListId.Length);
            os.Write(dataPayload, 0, dataPayload.Length);

            try
            {
                resp = req.GetResponse();
                sr = new StreamReader(resp.GetResponseStream());
                sServerResonpse = sr.ReadToEnd().Trim();
            }
            catch (Exception exp)
            {
                sServerResonpse = "Fail|" + exp.Message;
            }
            finally
            {
                if (os != null)
                {
                    os.Dispose();
                    os.Close();
                }
                if (sr != null)
                {
                    sr.Dispose();
                    sr.Close();
                }
            }

            return sServerResonpse;
        }

        public string StripString(string targetString)
        {//Simple method in C# to strip non-alphanumeric letters
            return System.Text.RegularExpressions.Regex.Replace(targetString, "[^A-Za-z0-9.]", "");
        }




    }
}
