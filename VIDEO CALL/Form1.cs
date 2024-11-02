using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using NAudio.Wave;

namespace VIDEO_CALL
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private WaveInEvent waveIn;
        private Label audioStatusLabel;
        private bool isCameraOn = false;
        private bool isVoiceInputOn = true; 

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;

           
            audioStatusLabel = new Label
            {
                Text = "",
                AutoSize = true,
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Red,
                Location = new Point(10, 10)
            };
            this.Controls.Add(audioStatusLabel);

            // Thiết lập nút camera
            btnTurnoff.Text = "BẬT CAMERA";
            btnTurnoff.Click += BtnTurnoff_Click;

          
            audio.Click += Audio_Click; 
            audio.Text = "TẮT VOICE INPUT"; 

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeAudio();

            // Lấy danh sách camera và khởi động video
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("Không có camera nào được kết nối.");
                return;
            }

            foreach (FilterInfo device in videoDevices)
            {
                cmbCameras.Items.Add(device.Name);
            }

            cmbCameras.SelectedIndex = 0;
        }

        private void StartVideoStream()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                StopVideoStream();
            }

            videoSource = new VideoCaptureDevice(videoDevices[cmbCameras.SelectedIndex].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
            isCameraOn = true;
            btnTurnoff.Text = "TẮT CAMERA";
        }

        private void StopVideoStream()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                videoSource.NewFrame -= VideoSource_NewFrame;
                videoSource = null;
            }

            // Clear the image from PictureBox and dispose of it if any
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }

            isCameraOn = false;
            btnTurnoff.Text = "BẬT CAMERA";
        }


        private void BtnTurnoff_Click(object sender, EventArgs e)
        {
            if (isCameraOn)
            {
                StopVideoStream();
            }
            else
            {
                StartVideoStream();
            }
        }

        private void Audio_Click(object sender, EventArgs e)
        {
            if (isVoiceInputOn)
            {
                // Tắt Voice Input
                if (waveIn != null)
                {
                    waveIn.StopRecording();
                }
                audio.Text = "BẬT VOICE INPUT"; // Cập nhật văn bản nút
                isVoiceInputOn = false;
            }
            else
            {
                // Bật Voice Input
                if (waveIn != null)
                {
                    waveIn.StartRecording();
                }
                audio.Text = "TẮT VOICE INPUT"; // Cập nhật văn bản nút
                isVoiceInputOn = true;
            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            using (Bitmap frame = (Bitmap)eventArgs.Frame.Clone())
            {
                Bitmap resizedFrame = ResizeImageToFitPictureBox(frame, pictureBox.Width, pictureBox.Height);

                if (pictureBox.InvokeRequired)
                {
                    pictureBox.Invoke(new MethodInvoker(() =>
                    {
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = resizedFrame;
                    }));
                }
                else
                {
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = resizedFrame;
                }
            }
        }

        private Bitmap ResizeImageToFitPictureBox(Bitmap image, int width, int height)
        {
            float ratioX = (float)width / image.Width;
            float ratioY = (float)height / image.Height;
            float ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            Bitmap resizedImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return resizedImage;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopVideoStream();

            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }
        }

        private void cmbCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isCameraOn)
            {
                StopVideoStream();
                StartVideoStream();
            }
        }

        private void InitializeAudio()
        {
            waveIn = new WaveInEvent();
            waveIn.WaveFormat = new WaveFormat(8000, 1);
            waveIn.DataAvailable += WaveIn_DataAvailable;
            waveIn.StartRecording();
        }
        private void EndCall_Click(object sender, EventArgs e)
{
    StopVideoStream();

    if (waveIn != null)
    {
        waveIn.StopRecording();
        waveIn.Dispose();
        waveIn = null;
    }

    // Close the application after stopping all streams
    this.Close();
}

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (!isVoiceInputOn) return; // Bỏ qua xử lý nếu Voice Input bị tắt

            bool hasSound = false;

            for (int index = 0; index < e.BytesRecorded; index += 2)
            {
                short sample = (short)((e.Buffer[index + 1] << 8) | e.Buffer[index]);
                float sample32 = sample / 32768f;

                if (Math.Abs(sample32) > 0.02)
                {
                    hasSound = true;
                    break;
                }
            }

            if (hasSound)
            {
                UpdateAudioStatus("Có âm thanh");
            }
            else
            {
                UpdateAudioStatus("");
            }
        }

        private void UpdateAudioStatus(string message)
        {
            if (audioStatusLabel.InvokeRequired)
            {
                audioStatusLabel.Invoke(new MethodInvoker(() =>
                {
                    audioStatusLabel.Text = message;
                }));
            }
            else
            {
                audioStatusLabel.Text = message;
            }
        }
    }
}
