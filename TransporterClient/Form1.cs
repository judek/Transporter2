using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Diagnostics;   //for Process
using TransporterClient.Properties;


namespace TransporterClient
{
    public delegate void UpdateTextCallback(string text);
    public delegate void UpdateBarCallback(int value);

    public enum InputFileType {Undefined, Audio,Video };
    
    public partial class Form1 : Form
    {

        //TransportService.TransportService1 _transportService = null;

        InputFileType _InputFileType = InputFileType.Undefined;

        List<string> _AudioExtensions = new List<string>()
        {
             ".aac" // Advanced Audio Coding File 
            ,".aif" //  Audio Interchange File Format 
            ,".iff" //  Interchange File Format 
            ,".m3u" //  Media Playlist File 
            ,".mid" //  MIDI File 
            ,".mp3" //  MP3 Audio File 
            ,".mpa" //  MPEG-2 Audio File 
            ,".ra" //  Real Audio File 
            ,".wav" //  WAVE Audio File 
            ,".wma" //  Windows Media Audio File 
        };

        List<string> _VideoExtensions = new List<string>()
        {
             ".3g2" // 3GPP2 Multimedia File 
            ,".3gp" // 3GPP Multimedia File 
            ,".asf" // Advanced Systems Format File 
            ,".asx" // Microsoft ASF Redirector File 
            ,".avi" // Audio Video Interleave File 
            ,".flv" // Flash Video File 
            ,".mov" // Apple QuickTime Movie 
            ,".mp4" // MPEG-4 Video File 
            ,".mpg" // MPEG Video File 
            ,".rm" // Real Media File 
            ,".swf" // Flash Movie 
            ,".vob" // DVD Video Object File 
            ,".wmv" // Windows Media Video File 

        };
        
        
        string _executableDirectoryName;


        string _tempFileName = "test";
        string _ApplicationFolderpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        
        Process _ffmpegprocess = null;
        string _guid = "";
        
        System.Net.WebProxy _proxy = null;

        //ProgramData _programData = null;

        ClientPipe.BinaryTransporter _binaryTransportChannel = null;
        
        public Form1()
        {

            FileInfo executableFileInfo = new FileInfo(Application.ExecutablePath);
            _executableDirectoryName = executableFileInfo.DirectoryName;

            
            InitializeComponent();


            //textBoxReceiver.Text = "http://rivervalleycarpentry.com/Receiver.aspx";
            //textBoxReceiver.Text = "http://judek.com/Receiver.aspx";
            //textBoxReceiver.Text = "http://rivervalleycommunity.org/Receiver.aspx";
            textBoxReceiver.Text = "http://localhost:49160/Receiver.aspx";

            //textBoxTargetFolder.Text = "~/TheRock/multimedia";
             //textBoxTargetFolder.Text = "~/bigwoods/multimedia";
            //textBoxTargetFolder.Text = "~/rivervalley/multimedia";
            textBoxTargetFolder.Text = "~/uploads";
        
         
        }


        private void buttonBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.ShowDialog();


            if (opd.FileName == null)
                return;

            if (opd.FileName.Length < 5)
                return;

            textBoxUploadFileName.Text = opd.FileName;

            FileInfo inputFileInfo = new FileInfo(opd.FileName);

            if (false == inputFileInfo.Exists)
            {
                return;
            }
            
            radioVideo.Enabled = true;
            checkBoxConvert.Checked = false;
            buttonUpload.Enabled = true;



            if (null != _AudioExtensions.Find(delegate(string s) { return s.ToLower() == inputFileInfo.Extension.ToLower(); }))
            {
                _InputFileType = InputFileType.Audio;
                radioAudio.Checked = true;
                radioVideo.Checked = false;
                radioVideo.Enabled = false;
                checkBoxConvert.Checked = true;
                return;
            }
            

            if (null != _VideoExtensions.Find(delegate(string s) { return s.ToLower() == inputFileInfo.Extension.ToLower(); }))
            {
                _InputFileType = InputFileType.Video;
                radioVideo.Checked = true;
                checkBoxConvert.Checked = true;
                return;
                
            }

            _InputFileType = InputFileType.Undefined;
            radioOther.Checked = true;


        }

        void Converting()
        {
            progressBar1.Maximum = 10;

            for (int i =0 ;i<10 ;i++ )
            {
                Thread.Sleep(1000);
                ProgressCallback(i);
            }
            conversionDoneCallback(null);
        }
        
        private void ConvertUpload(string sFileName)
        {
            buttonUpload.Enabled = false;

            if (true == radioAudio.Checked)
            {
                Thread t = new Thread(this.execConversionthread);
                t.Start();
            }
            else
            {
                ConvertVideo(sFileName);
            }


        }

        private void ConvertVideo(string sFileName)
        {

            try
            {

                
                
                _ffmpegprocess = new Process();

                labelProgress.Invoke(new UpdateTextCallback(this.UpdateText), "Re-encoding video please wait...");


                _ffmpegprocess.StartInfo.FileName = _executableDirectoryName + "\\HandBrakeCLI.exe";

                FileInfo fileinfo = new FileInfo(sFileName);

                if (false == fileinfo.Exists)
                    throw new Exception(fileinfo.FullName + " does not exit");


                //string arguments = string.Format("-i \"{0}\" -y \"{1}\"",
                //    fileinfo.FullName,
                //    Outfileinfo.FullName);

                string arguments = string.Format("-i \"{0}\" -t 1 -c 1 -o \"{1}\" -f mp4 -O  -X 320 -l 240 -e x264 -b 500 -a 1 -E faac -6 dpl2 -R 48 -B 160 -D 0.0 -x level=30:bframes=0:cabac=0:ref=1:vbv-maxrate=768:vbv-bufsize=2000:analyse=all:me=umh:no-fast-pskip=1:subq=6:8x8dct=0:trellis=0:weightb=0:mixed-refs=0 -v 1",
                    fileinfo.FullName,
                    _tempFileName);

                _ffmpegprocess.StartInfo.Arguments = arguments;

                _ffmpegprocess.Start();


                if (null == _ffmpegprocess)
                    return;


                _ffmpegprocess.WaitForExit();

                int exitCode = _ffmpegprocess.ExitCode;
  
                if (exitCode != 0)
                    throw new Exception("return code was: " + exitCode);

            }
            catch (Exception e)
            {
                labelProgress.Invoke(new UpdateTextCallback(this.UpdateText), "Video convertion failed:" + e.Message);
                return;
            }
            finally
            {
                _ffmpegprocess.Close();
                _ffmpegprocess.Dispose();
                _ffmpegprocess = null;
                buttonUpload.Enabled = true;
            }

            labelProgress.Invoke(new UpdateTextCallback(this.UpdateText), "Video re-encoding was successful.");            
            Upload(_tempFileName);
            
        }

        private void Upload(string sFileName)
        {
            labelProgress.Invoke(new UpdateTextCallback(this.UpdateText), "Uploading please wait...");

            try
            {

                if (true == sFileName.Trim().Length < 5)
                {
                    throw new ApplicationException("Select File to upload");
                }

                if (false == File.Exists(sFileName.Trim()))
                {
                    throw new ApplicationException("File does not exist");
                }

                _binaryTransportChannel = new ClientPipe.BinaryTransporter(textBoxReceiver.Text.Trim(),
                    _proxy,
                    _guid,
                    "123456",
                    false);


                FileInfo fi = new FileInfo(sFileName.Trim());


                progressBar1.Minimum = 0;
                progressBar1.Maximum = (int)fi.Length;

                buttonUpload.Enabled = false;

                _guid = _binaryTransportChannel.RequestUploadStart(textBoxTargetFolder.Text.Trim());

                _binaryTransportChannel.SessionID = _guid;

                _binaryTransportChannel.BeginSendFile(fi.FullName,
                    textBoxTargetFolder.Text.Trim(),
                    fi.Name,
                    this.uploadDoneCallback, this.ProgressCallback);
                labelProgress.Text = "Uploading...";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Transporter Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _binaryTransportChannel = null;
                buttonUpload.Enabled = true;
                return;
            }
        }
        
        
        private void buttonUpload_Click(object sender, EventArgs e)
        {
                       

            if (true == textBoxUploadFileName.Text.Trim().Length < 5)
            {
                MessageBox.Show("Browse and select File to upload", "Upload",
                           MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }

            FileInfo testinfo = new FileInfo(textBoxUploadFileName.Text.Trim());
            
            if (false == testinfo.Exists)
            {
                MessageBox.Show("Upload file does not exist", "Upload",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((false == radioAudio.Checked) && (false == radioVideo.Checked) &&(true == checkBoxConvert.Checked))
            {
                MessageBox.Show("Select Input File Type:Audio or Video", "Upload",
                    MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }


            Directory.CreateDirectory(_ApplicationFolderpath + "\\transporter\\");

           if(radioAudio.Checked)
                _tempFileName = _ApplicationFolderpath + "\\transporter\\" + testinfo.Name.Replace(testinfo.Extension, ".mp3");
           else
                _tempFileName = _ApplicationFolderpath + "\\transporter\\" + testinfo.Name.Replace(testinfo.Extension, ".mp4");
    

            if (File.Exists(_tempFileName))
                File.Delete(_tempFileName);

            
            try
            {
                File.WriteAllText(_tempFileName, "Hello world!");
            }
            catch
            {
                MessageBox.Show("Unable to create termporary file for conversion", "Upload",
                                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (checkBoxConvert.Checked)
            {
                ConvertUpload(textBoxUploadFileName.Text);
            }
            else
            {
                Upload(textBoxUploadFileName.Text);
            }
        }


        public void conversionDoneCallback(Exception exp)
        {

            buttonUpload.Enabled = true;
           

            ProgressCallback((long)progressBar1.Minimum);

            string msg;

            if (exp == null)
            {
                Upload(_tempFileName);
            }
            else
            {
                msg = "Conversion Failure:" + exp.Message;
                labelProgress.Invoke(new UpdateTextCallback(this.UpdateText), msg);
                File.Delete(_tempFileName);
            }
            
            
        }

        
        public void uploadDoneCallback(Exception exp)
        {

            buttonUpload.Enabled = true;
            
            textBoxUploadFileName.Invoke(new UpdateBarCallback(this.UpdateBarGraph), progressBar1.Minimum);

            string msg;
            
            if (exp == null)
                msg = "Upload Complete no errors";
            else
                msg = "Upload Failure:" + exp.Message;


            textBoxUploadFileName.Invoke(new UpdateTextCallback(this.UpdateText), msg);

            _binaryTransportChannel = null;
        }

        void UpdateText(string text)
        {
            labelProgress.Text = text;
        }

        void UpdateBarGraph(int value)
        {
            try
            {
                progressBar1.Value = value;
            }
            catch { };//Catch everything cause if this fails will dead lock this app
        }

        void SetBarGraphMaximum(int max)
        {
            try
            {
                progressBar1.Maximum = max;
            }
            catch { };//Catch everything cause if this fails will dead lock this app
        }

        public void ProgressCallback(long l)
        {
            int newValue = (int)l;
            textBoxUploadFileName.Invoke(new UpdateBarCallback(this.UpdateBarGraph), newValue);

            //string msg = " Sent " + progressBar1.Value.ToString() + " Bytes...";
            //textBoxUploadFileName.Invoke(new UpdateTextCallback(this.UpdateText), msg);
        }

        public void SetProgressMaximumCallback(long l)
        {
            int newValue = (int)l;
            textBoxUploadFileName.Invoke(new UpdateBarCallback(this.SetBarGraphMaximum), newValue);

            //string msg = " Sent " + progressBar1.Value.ToString() + " Bytes...";
            //textBoxUploadFileName.Invoke(new UpdateTextCallback(this.UpdateText), msg);
        }


        private void execConversionthread()
        {

            bool blnProcessStarted = false;
            try
            {
                FileInfo fileinfo = new FileInfo(textBoxUploadFileName.Text.Trim());

                //string sOutput = textBoxUploadFileName.Text = fileinfo.Name.Replace(fileinfo.Extension, ".64.mp3");
                FileInfo Outfileinfo = new FileInfo(_tempFileName);

                //string sTempFileName =  Path.GetTempFileName();

                if (false == fileinfo.Exists)
                    return;

                /*
                if (true == Outfileinfo.Exists)
                {
                    if (DialogResult.Yes !=
                    MessageBox.Show("Output File Exists. Overwrite?.", "File Convert",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        return;
                }
                */


                _ffmpegprocess = new Process();

                
                labelProgress.Invoke(new UpdateTextCallback(this.UpdateText), "Re-encoding audio please wait...");


                _ffmpegprocess.StartInfo.FileName = "ffmpeg.exe";

                string arguments = string.Format("-i \"{0}\" -y -acodec libmp3lame -ab 64k \"{1}\"",
                    fileinfo.FullName,
                    Outfileinfo.FullName);


                _ffmpegprocess.StartInfo.Arguments = arguments;


                _ffmpegprocess.EnableRaisingEvents = false;
                _ffmpegprocess.StartInfo.UseShellExecute = false;
                _ffmpegprocess.StartInfo.CreateNoWindow = true;
                _ffmpegprocess.StartInfo.RedirectStandardOutput = true;
                _ffmpegprocess.StartInfo.RedirectStandardError = true;
                _ffmpegprocess.Start();
                StreamReader d = _ffmpegprocess.StandardError;
                int stringlength;
                string temp;
                do
                {
                    string s = d.ReadLine();
                    stringlength = s.Length;

                    if (s.Contains("time="))
                    {
                        string[] pairs = s.Split(' ');

                        foreach (string pair in pairs)
                        {
                            if (pair.StartsWith("time="))
                            {
                                temp = (pair.Split('='))[1];
                                float f = float.Parse(temp);
                                //progressBar1.Value = (int)f;
                                ProgressCallback((long)f);
                                blnProcessStarted = true;
                                

                            }
                        }




                        


                        //string currents = functions.ExtractTime(s);
                        //current = functions.CurrentStringToSeconds(currents);
                        //synchCurrent(current.ToString());
                        //synchTextOutput(s);
                    }


                    if (s.Contains("Duration: "))
                    {
                        //labelDuration.Text = s;

                        string[] pairs = s.Split(',');

                        string sduration = pairs[0].Trim();
                        string[] times = sduration.Split(':');

                        float fDuration = 0;

                        fDuration += (float.Parse(times[3]));
                        fDuration += (float.Parse(times[2])) * 60;
                        fDuration += (float.Parse(times[1])) * 3600;

                        SetBarGraphMaximum((int)fDuration);

                    }
                    else
                    {
                        if (s.Contains("frame="))
                        {
                            //string currents = functions.ExtractTime(s);
                            //current = functions.CurrentStringToSeconds(currents);
                            //synchCurrent(current.ToString());
                            //synchTextOutput(s);
                        }
                    }
                } while (!d.EndOfStream);


                //progressBar1.Value = progressBar1.Minimum;

                if (null == _ffmpegprocess)
                    return;


                _ffmpegprocess.WaitForExit();
                _ffmpegprocess.Close();
                _ffmpegprocess.Dispose();
                _ffmpegprocess = null;

                if (false == blnProcessStarted)
                    throw new Exception("Conversion process failed.");


                conversionDoneCallback(null);
            }
            catch(Exception exp)
            {
                conversionDoneCallback(exp);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            KillUploadProcess();
            KillConvesionProcess();
            File.Delete(_tempFileName);
        }

        void KillConvesionProcess()
        {
            if (_ffmpegprocess != null)
            {


                try { _ffmpegprocess.Kill(); }
                catch { }
                
                _ffmpegprocess.Close();
                _ffmpegprocess.Dispose();

                _ffmpegprocess = null;
            }

            
            //File.Delete(TEMPFILE_NAME);

        }

        void KillUploadProcess()
        {

        }

        private void checkBoxConvert_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxConvert_Click(object sender, EventArgs e)
        {
            if (false == checkBoxConvert.Checked)
            {
                if (DialogResult.Yes != MessageBox.Show("Are you sure?\nIt may cause the file not to be played", "Transporter",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    checkBoxConvert.Checked = true;
                }
                return;

            }

        }


        private void radioAudio_Click(object sender, EventArgs e)
        {
            checkBoxConvert.Checked = true;
        }

        private void radioVideo_Click(object sender, EventArgs e)
        {
            checkBoxConvert.Checked = true;
        }

        private void radioOther_CheckedChanged(object sender, EventArgs e)
        {
            if (true == radioOther.Checked)
            {
                checkBoxConvert.Checked = false;
                checkBoxConvert.Enabled = false;
            }
            else
            {
                checkBoxConvert.Enabled = true;
            }
        }

   

    }
}
