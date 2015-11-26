namespace Audition
{
    partial class AudioDetection
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.micphone_selector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.speaker_selector = new System.Windows.Forms.ComboBox();
            this.micphone_trackbar = new System.Windows.Forms.TrackBar();
            this.speaker_volume = new System.Windows.Forms.TrackBar();
            this.speaker_peak = new System.Windows.Forms.ProgressBar();
            this.micphone_peak = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.micphone_trackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speaker_volume)).BeginInit();
            this.SuspendLayout();
            // 
            // micphone_selector
            // 
            this.micphone_selector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.micphone_selector.FormattingEnabled = true;
            this.micphone_selector.Location = new System.Drawing.Point(96, 12);
            this.micphone_selector.Name = "micphone_selector";
            this.micphone_selector.Size = new System.Drawing.Size(273, 20);
            this.micphone_selector.TabIndex = 0;
            this.micphone_selector.SelectedIndexChanged += new System.EventHandler(this.micphone_selector_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "切换麦克风：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "切换扬声器：";
            // 
            // speaker_selector
            // 
            this.speaker_selector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.speaker_selector.FormattingEnabled = true;
            this.speaker_selector.Location = new System.Drawing.Point(96, 115);
            this.speaker_selector.Name = "speaker_selector";
            this.speaker_selector.Size = new System.Drawing.Size(273, 20);
            this.speaker_selector.TabIndex = 3;
            this.speaker_selector.SelectedIndexChanged += new System.EventHandler(this.speaker_selector_SelectedIndexChanged);
            // 
            // micphone_trackbar
            // 
            this.micphone_trackbar.Location = new System.Drawing.Point(15, 50);
            this.micphone_trackbar.Maximum = 100;
            this.micphone_trackbar.Name = "micphone_trackbar";
            this.micphone_trackbar.Size = new System.Drawing.Size(182, 45);
            this.micphone_trackbar.TabIndex = 4;
            this.micphone_trackbar.Scroll += new System.EventHandler(this.micphone_trackbar_Scroll);
            // 
            // speaker_volume
            // 
            this.speaker_volume.Location = new System.Drawing.Point(15, 154);
            this.speaker_volume.Maximum = 100;
            this.speaker_volume.Name = "speaker_volume";
            this.speaker_volume.Size = new System.Drawing.Size(180, 45);
            this.speaker_volume.TabIndex = 5;
            this.speaker_volume.Scroll += new System.EventHandler(this.speaker_volume_Scroll);
            // 
            // speaker_peak
            // 
            this.speaker_peak.Location = new System.Drawing.Point(209, 159);
            this.speaker_peak.Name = "speaker_peak";
            this.speaker_peak.Size = new System.Drawing.Size(160, 8);
            this.speaker_peak.TabIndex = 6;
            // 
            // micphone_peak
            // 
            this.micphone_peak.Location = new System.Drawing.Point(209, 56);
            this.micphone_peak.Name = "micphone_peak";
            this.micphone_peak.Size = new System.Drawing.Size(160, 8);
            this.micphone_peak.TabIndex = 7;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(384, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 165);
            this.button1.TabIndex = 8;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 189);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.micphone_peak);
            this.Controls.Add(this.speaker_peak);
            this.Controls.Add(this.speaker_volume);
            this.Controls.Add(this.micphone_trackbar);
            this.Controls.Add(this.speaker_selector);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.micphone_selector);
            this.Name = "AudioDetection";
            this.Text = "老师来帮忙-语音测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.micphone_trackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speaker_volume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox micphone_selector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox speaker_selector;
        private System.Windows.Forms.TrackBar micphone_trackbar;
        private System.Windows.Forms.TrackBar speaker_volume;
        private System.Windows.Forms.ProgressBar speaker_peak;
        private System.Windows.Forms.ProgressBar micphone_peak;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
    }
}

