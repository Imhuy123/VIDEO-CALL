namespace VIDEO_CALL
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbCameras = new ComboBox();
            btnTurnoff = new Button();
            pictureBox = new PictureBox();
            endCall = new Button();
            pictureBox1 = new PictureBox();
            audio = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // cmbCameras
            // 
            cmbCameras.FormattingEnabled = true;
            cmbCameras.Location = new Point(637, 33);
            cmbCameras.Name = "cmbCameras";
            cmbCameras.Size = new Size(151, 28);
            cmbCameras.TabIndex = 0;
            // 
            // btnTurnoff
            // 
            btnTurnoff.Location = new Point(231, 374);
            btnTurnoff.Name = "btnTurnoff";
            btnTurnoff.Size = new Size(97, 64);
            btnTurnoff.TabIndex = 1;
            btnTurnoff.Text = "TẮT CAMERA";
            btnTurnoff.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(558, 172);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(230, 165);
            pictureBox.TabIndex = 2;
            pictureBox.TabStop = false;
            // 
            // endCall
            // 
            endCall.Location = new Point(462, 374);
            endCall.Name = "endCall";
            endCall.Size = new Size(97, 64);
            endCall.TabIndex = 3;
            endCall.Text = "KẾT THÚC CUỘC GỌI";
            endCall.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(105, 33);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(328, 292);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // audio
            // 
            audio.Location = new Point(114, 374);
            audio.Name = "audio";
            audio.Size = new Size(97, 64);
            audio.TabIndex = 5;
            audio.Text = "TẮT ÂM THANH ";
            audio.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(audio);
            Controls.Add(pictureBox1);
            Controls.Add(endCall);
            Controls.Add(pictureBox);
            Controls.Add(btnTurnoff);
            Controls.Add(cmbCameras);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbCameras;
        private Button btnTurnoff;
        private PictureBox pictureBox;
        private Button endCall;
        private PictureBox pictureBox1;
        private Button audio;
    }
}
