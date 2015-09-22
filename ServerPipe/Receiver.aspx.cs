using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using MemoryCompressor;


namespace ServerPipe
{
    public class AbortException : System.Exception
    {
        public AbortException(string msg)
            : base(msg)
        {

        }
    }
    public partial class Receiver : System.Web.UI.Page
    {

        static uint TRANSPORTBLOB_HEADER = 0xF0F0;
        static uint TRANSPORT_START_REQUEST = 0x0001;
        static uint TRANSPORT_FINISH_REQUEST = 0x0002;
        static int GUID_LENGTH = 36;

        uint _BlobHeader = 0;


        protected void AddNotification(string notification)
        {
            try
            {
                if (null == Session["Notifications"])
                    Session["Notifications"] = new List<string>();

                List<string> notifications = Session["Notifications"] as List<string>;

                //Not checking for double notifications at this time
                //if (null != notifications.Find(delegate(string t) { return t.ToLower() == notification.ToLower(); }))
                //return;


                notifications.Add(notification);
            }
            catch { }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BinaryReader transportBlob = new BinaryReader(Request.InputStream);

            if (transportBlob.BaseStream.Length < 1)
                return;//empty content

            Response.ContentType = "text/xml; encoding='utf-8'";

            try
            {

                #region Check packet integerty read header

                _BlobHeader = transportBlob.ReadUInt32();


                if ((_BlobHeader != TRANSPORT_START_REQUEST) && (_BlobHeader != TRANSPORTBLOB_HEADER))
                    throw new Exception("Incorrect packet header.");


                if (_BlobHeader == TRANSPORT_START_REQUEST)
                {
                    Response.Write(ProcessStartUploadRequest(transportBlob));
                    return;
                }
                
                
                
                long lFileOffset = transportBlob.ReadInt64();

                bool blnIsCompressed = transportBlob.ReadBoolean();

                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                String sessionGUID = enc.GetString(transportBlob.ReadBytes(GUID_LENGTH));


                String ListID = enc.GetString(transportBlob.ReadBytes(6));


                #endregion

                int payloadBytesReceived = (int)(transportBlob.BaseStream.Length - transportBlob.BaseStream.Position);
                byte[] ReceiveBuffer = new byte[payloadBytesReceived];
                transportBlob.Read(ReceiveBuffer, 0, payloadBytesReceived);
                byte[] WriteBuffer;

                if (true == blnIsCompressed)
                {
                    try
                    {
                        WriteBuffer = Compressor.Decompress(ReceiveBuffer);
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Compressor.Decompress failure:" + exp.Message);
                    }
                }
                else
                    WriteBuffer = ReceiveBuffer;


                //Do not check if valid guid cause sessionIDs coming from a legacy
                //FTIClient will not be a guid but a time stamp
                //if (false == IsGuid(sessionGUID))
                //throw new Exception("Incorrect session ID.");

                try
                {
                    if (false == ValidateUploadSession(sessionGUID, ListID, lFileOffset))
                        throw new AbortException("Invalid upload session.");
                }
                catch (Exception exp)
                {
                    throw new AbortException("ValidateUploadSession failed" + exp.Message);
                }

                //Finally write chunk to disk
                Response.Write("OK|" + WriteChunk(sessionGUID, ListID, WriteBuffer, lFileOffset));

                //Is first chunk?
                //No need to test for this case ValidateUploadSession() alreay doese what
                //we need to do....SetUploadStart time in DB

                //Is last chunk? - Set upload finish time in DB
                if (-1 == lFileOffset)
                {
                    //To Do: clean up?
                    //SetTransferTimeInDB(sessionGUID, false, null);
                }

            }

            catch (AbortException exp)
            {
                Response.Write("ABORT|" + exp.Message);
            }
            catch (Exception exp)
            {
                Response.Write("FAIL|" + exp.Message);
            }
            finally
            {
                Response.End();
            }


        }

        long WriteChunk(string SessionId, string ListID, byte[] f, long lFileOffset)
        {

            int bytestoWrite = f.Length;
            FileStream fs = null;
            long nBytesWritten = 0;
            long nStart;

            try
            {

                /*
                string CustFilesRoot = System.Configuration.ConfigurationSettings.AppSettings["CustFilesRoot"];

                if (false == CustFilesRoot.EndsWith("\\"))
                    CustFilesRoot += "\\";

                CustFilesRoot += ListID;

                if (false == Directory.Exists(CustFilesRoot))
                    throw new Exception("Directory:" + CustFilesRoot + " does not exist");


                string sFileName = CustFilesRoot + "\\" + SessionId + ".tmp";
                */

                string sFileName = Server.MapPath("~/transientStorage") + "\\" + SessionId + ".tmp";

                if (-1 == lFileOffset)
                {//Transfer finalization

                    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                    string test = enc.GetString(f).Trim();
                    
                    String sNewFileName = Server.MapPath(enc.GetString(f).Trim());

                    FileInfo newFileInfo = new FileInfo(sNewFileName);

                    if (false == File.Exists(newFileInfo.DirectoryName + "\\" + ListID + ".token"))
                    {
                        if (false == File.Exists(newFileInfo.DirectoryName + "\\transport.token"))
                        {
                            File.Delete(sFileName);//Remove temporary file
                            throw new AbortException("Access Denied");
                        }   
                    }
                    
                    //Delete old file
                    if (true == File.Exists(sNewFileName))
                        File.Delete(sNewFileName);

                    File.Move(sFileName, sNewFileName);

                    if (false == File.Exists(newFileInfo.DirectoryName + "\\" + "DO_NOT_NOTIFY" + ".token"))
                        AddNotification("Transport Receiver: New file uploaded:" + test);

                    return 0;
                }



                if (false == File.Exists(sFileName))
                {
                    //File does not exist, create on only if this the first chunk.
                    try
                    {
                        fs = File.Create(sFileName);
                    }
                    catch (Exception exp)
                    {
                        throw new Exception("Could not create file for first chunk:" + exp.Message);
                    }
                }
                else
                {
                    //Otherwise this is a continuation of another upload
                    fs = new FileStream(sFileName, FileMode.Open);
                }


                fs.Seek(lFileOffset, SeekOrigin.Begin);

                nStart = fs.Position;

                fs.Write(f, 0, bytestoWrite);
                nBytesWritten = fs.Position - nStart;

                return nBytesWritten;
            }
            catch (FileNotFoundException)
            {
                //TO DO: log fnfe exception
                throw new AbortException("Access denied");
            }
            catch (Exception exp)
            {
                //TO DO: log exp exception
                throw new AbortException(exp.Message);
            }

            finally
            {
                //if (null != ms) ms.Close();
                if (null != fs) fs.Close();
                if (null != fs) fs.Dispose();

            }
        }

        string ProcessStartUploadRequest(BinaryReader transportBlob)
        {


            long lFileOffset = transportBlob.ReadInt64();

            bool blnIsCompressed = transportBlob.ReadBoolean();




            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            String ListID = enc.GetString(transportBlob.ReadBytes(6));


            int payloadBytesReceived = (int)(transportBlob.BaseStream.Length - transportBlob.BaseStream.Position);
            byte[] ReceiveBuffer = new byte[payloadBytesReceived];
            transportBlob.Read(ReceiveBuffer, 0, payloadBytesReceived);

            string targetFolder = enc.GetString(ReceiveBuffer).Trim();
            
            FileInfo SecToken = new FileInfo(Server.MapPath(targetFolder) + "\\" + ListID + ".token");

            if (false == SecToken.Exists)
            {
                if (false == File.Exists(SecToken.DirectoryName + "\\transport.token"))
                {
                    throw new AbortException("Access Denied");
                }
            }
            
           
            FileStream fs = null;
            string NewSessionID = System.Guid.NewGuid().ToString();

            try
            {
                fs = new FileStream(Server.MapPath("~/transientStorage") + "\\" + NewSessionID + ".tmp", FileMode.CreateNew);
            }
            finally
            {
                if (null != fs)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
            
            return "OK|" + NewSessionID;
        }

        /*
        void SetTransferTimeInDB(string SessionId, bool blnStart, JobSession js)
        {

            if (js == null)
            {
                TransportRequestHandler transportRequestHandler =
                    new TransportRequestHandler();

                js = transportRequestHandler.GetJobSessionBySID(SessionId);
            }

            if (js == null)
                throw new AbortException("SetTransferTimeInDB:Invalid Session");


            if (js.JobID < 1)
                throw new AbortException("SetTransferTimeInDB:Invalid Session");


            SqlCommand cmd = null;

            cmd = new SqlCommand();

            if (true == blnStart)
                cmd.CommandText = "dbo.SetUploadStart";
            else
                cmd.CommandText = "dbo.SetUploadFinish";


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            cmd.Parameters.AddWithValue("@SessionID", SessionId);
            cmd.Parameters.AddWithValue("@JobID", js.JobID);

            try
            {

                FTIDbUtil.ExecuteNonQuery(cmd);

            }
            finally { if (cmd != null) cmd.Dispose(); }


        }
        */



        bool IsGuid(string guidString)
        {
            bool bResult = false;
            try
            {
                Guid g = new Guid(guidString);
                bResult = true;
            }
            catch { bResult = false; }

            return bResult;
        }


        bool ValidateUploadSession(string SessionId, string ListID, long FileOffset)
        {

       
            string sFileName = Server.MapPath("~/transientStorage") + "\\" + SessionId + ".tmp";



            return (true == File.Exists(sFileName));
                


          
        }

    }
}
