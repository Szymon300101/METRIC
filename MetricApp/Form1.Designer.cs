namespace MetricApp
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
            this.connectionBox = new System.Windows.Forms.GroupBox();
            this.disconnectBtn = new System.Windows.Forms.Button();
            this.pingBtn = new System.Windows.Forms.Button();
            this.connectBtn = new System.Windows.Forms.Button();
            this.portLabel = new System.Windows.Forms.Label();
            this.portCombo = new System.Windows.Forms.ComboBox();
            this.rawSerialLog = new System.Windows.Forms.Label();
            this.modeIdleRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.modeScanRadio = new System.Windows.Forms.RadioButton();
            this.modeReadRadio = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.connectionBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionBox
            // 
            this.connectionBox.Controls.Add(this.disconnectBtn);
            this.connectionBox.Controls.Add(this.pingBtn);
            this.connectionBox.Controls.Add(this.connectBtn);
            this.connectionBox.Controls.Add(this.portLabel);
            this.connectionBox.Controls.Add(this.portCombo);
            this.connectionBox.Location = new System.Drawing.Point(555, 12);
            this.connectionBox.Name = "connectionBox";
            this.connectionBox.Size = new System.Drawing.Size(215, 125);
            this.connectionBox.TabIndex = 0;
            this.connectionBox.TabStop = false;
            this.connectionBox.Text = "Connection";
            // 
            // disconnectBtn
            // 
            this.disconnectBtn.Location = new System.Drawing.Point(89, 76);
            this.disconnectBtn.Name = "disconnectBtn";
            this.disconnectBtn.Size = new System.Drawing.Size(106, 29);
            this.disconnectBtn.TabIndex = 4;
            this.disconnectBtn.Text = "Disconnect";
            this.disconnectBtn.UseVisualStyleBackColor = true;
            this.disconnectBtn.Visible = false;
            this.disconnectBtn.Click += new System.EventHandler(this.disconnectBtn_Click);
            // 
            // pingBtn
            // 
            this.pingBtn.Location = new System.Drawing.Point(16, 76);
            this.pingBtn.Name = "pingBtn";
            this.pingBtn.Size = new System.Drawing.Size(53, 29);
            this.pingBtn.TabIndex = 3;
            this.pingBtn.Text = "Ping";
            this.pingBtn.UseVisualStyleBackColor = true;
            this.pingBtn.Visible = false;
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(16, 76);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(179, 29);
            this.connectBtn.TabIndex = 2;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(16, 29);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(38, 20);
            this.portLabel.TabIndex = 1;
            this.portLabel.Text = "Port:";
            // 
            // portCombo
            // 
            this.portCombo.FormattingEnabled = true;
            this.portCombo.Location = new System.Drawing.Point(89, 29);
            this.portCombo.Name = "portCombo";
            this.portCombo.Size = new System.Drawing.Size(106, 28);
            this.portCombo.TabIndex = 0;
            // 
            // rawSerialLog
            // 
            this.rawSerialLog.BackColor = System.Drawing.SystemColors.Control;
            this.rawSerialLog.Location = new System.Drawing.Point(6, 26);
            this.rawSerialLog.Name = "rawSerialLog";
            this.rawSerialLog.Size = new System.Drawing.Size(76, 260);
            this.rawSerialLog.TabIndex = 1;
            // 
            // modeIdleRadio
            // 
            this.modeIdleRadio.AutoSize = true;
            this.modeIdleRadio.Checked = true;
            this.modeIdleRadio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.modeIdleRadio.Location = new System.Drawing.Point(16, 26);
            this.modeIdleRadio.Name = "modeIdleRadio";
            this.modeIdleRadio.Size = new System.Drawing.Size(55, 24);
            this.modeIdleRadio.TabIndex = 2;
            this.modeIdleRadio.TabStop = true;
            this.modeIdleRadio.Text = "Idle";
            this.modeIdleRadio.UseVisualStyleBackColor = true;
            this.modeIdleRadio.CheckedChanged += new System.EventHandler(this.modeIdleRadio_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.modeScanRadio);
            this.groupBox1.Controls.Add(this.modeReadRadio);
            this.groupBox1.Controls.Add(this.modeIdleRadio);
            this.groupBox1.Location = new System.Drawing.Point(555, 143);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 125);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Board Mode";
            // 
            // modeScanRadio
            // 
            this.modeScanRadio.AutoSize = true;
            this.modeScanRadio.Location = new System.Drawing.Point(16, 86);
            this.modeScanRadio.Name = "modeScanRadio";
            this.modeScanRadio.Size = new System.Drawing.Size(61, 24);
            this.modeScanRadio.TabIndex = 4;
            this.modeScanRadio.TabStop = true;
            this.modeScanRadio.Text = "Scan";
            this.modeScanRadio.UseVisualStyleBackColor = true;
            this.modeScanRadio.CheckedChanged += new System.EventHandler(this.modeScanRadio_CheckedChanged);
            // 
            // modeReadRadio
            // 
            this.modeReadRadio.AutoSize = true;
            this.modeReadRadio.Location = new System.Drawing.Point(16, 56);
            this.modeReadRadio.Name = "modeReadRadio";
            this.modeReadRadio.Size = new System.Drawing.Size(64, 24);
            this.modeReadRadio.TabIndex = 3;
            this.modeReadRadio.TabStop = true;
            this.modeReadRadio.Text = "Read";
            this.modeReadRadio.UseVisualStyleBackColor = true;
            this.modeReadRadio.CheckedChanged += new System.EventHandler(this.modeReadRadio_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rawSerialLog);
            this.groupBox2.Location = new System.Drawing.Point(682, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(88, 298);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Raw Data";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.connectionBox);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Metric Control Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.connectionBox.ResumeLayout(false);
            this.connectionBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox connectionBox;
        private Button connectBtn;
        private Label portLabel;
        private ComboBox portCombo;
        private Label rawSerialLog;
        private Button pingBtn;
        private Button disconnectBtn;
        private RadioButton modeIdleRadio;
        private GroupBox groupBox1;
        private RadioButton modeScanRadio;
        private RadioButton modeReadRadio;
        private GroupBox groupBox2;
    }
}