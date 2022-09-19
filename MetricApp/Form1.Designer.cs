﻿using System.Windows.Forms.DataVisualization.Charting;

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
            this.chartBox = new System.Windows.Forms.GroupBox();
            this.dataBox = new System.Windows.Forms.GroupBox();
            this.dataLoadBtn = new System.Windows.Forms.Button();
            this.dataInfoLabel = new System.Windows.Forms.Label();
            this.dataSaveBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.calibrationBox = new System.Windows.Forms.GroupBox();
            this.calibLoadBtn = new System.Windows.Forms.Button();
            this.calibSaveBtn = new System.Windows.Forms.Button();
            this.calibBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.calibReadInput = new System.Windows.Forms.NumericUpDown();
            this.offsetInput = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.scalingInput = new System.Windows.Forms.NumericUpDown();
            this.connectionBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.dataBox.SuspendLayout();
            this.calibrationBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calibReadInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scalingInput)).BeginInit();
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
            this.portLabel.Location = new System.Drawing.Point(45, 32);
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
            this.groupBox1.Size = new System.Drawing.Size(121, 125);
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
            this.groupBox2.Size = new System.Drawing.Size(88, 308);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Raw Data";
            // 
            // chartBox
            // 
            this.chartBox.Location = new System.Drawing.Point(12, 12);
            this.chartBox.Name = "chartBox";
            this.chartBox.Size = new System.Drawing.Size(537, 304);
            this.chartBox.TabIndex = 5;
            this.chartBox.TabStop = false;
            this.chartBox.Text = "Chart";
            // 
            // dataBox
            // 
            this.dataBox.Controls.Add(this.dataLoadBtn);
            this.dataBox.Controls.Add(this.dataInfoLabel);
            this.dataBox.Controls.Add(this.dataSaveBtn);
            this.dataBox.Controls.Add(this.clearBtn);
            this.dataBox.Location = new System.Drawing.Point(555, 279);
            this.dataBox.Name = "dataBox";
            this.dataBox.Size = new System.Drawing.Size(121, 172);
            this.dataBox.TabIndex = 6;
            this.dataBox.TabStop = false;
            this.dataBox.Text = "Data";
            // 
            // dataLoadBtn
            // 
            this.dataLoadBtn.Location = new System.Drawing.Point(16, 86);
            this.dataLoadBtn.Name = "dataLoadBtn";
            this.dataLoadBtn.Size = new System.Drawing.Size(94, 29);
            this.dataLoadBtn.TabIndex = 3;
            this.dataLoadBtn.Text = "Load";
            this.dataLoadBtn.UseVisualStyleBackColor = true;
            // 
            // dataInfoLabel
            // 
            this.dataInfoLabel.Location = new System.Drawing.Point(16, 23);
            this.dataInfoLabel.Name = "dataInfoLabel";
            this.dataInfoLabel.Size = new System.Drawing.Size(94, 25);
            this.dataInfoLabel.TabIndex = 2;
            // 
            // dataSaveBtn
            // 
            this.dataSaveBtn.Location = new System.Drawing.Point(16, 121);
            this.dataSaveBtn.Name = "dataSaveBtn";
            this.dataSaveBtn.Size = new System.Drawing.Size(94, 29);
            this.dataSaveBtn.TabIndex = 1;
            this.dataSaveBtn.Text = "Save";
            this.dataSaveBtn.UseVisualStyleBackColor = true;
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(16, 51);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(94, 29);
            this.clearBtn.TabIndex = 0;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // calibrationBox
            // 
            this.calibrationBox.Controls.Add(this.calibLoadBtn);
            this.calibrationBox.Controls.Add(this.calibSaveBtn);
            this.calibrationBox.Controls.Add(this.calibBtn);
            this.calibrationBox.Controls.Add(this.label3);
            this.calibrationBox.Controls.Add(this.label2);
            this.calibrationBox.Controls.Add(this.calibReadInput);
            this.calibrationBox.Controls.Add(this.offsetInput);
            this.calibrationBox.Controls.Add(this.label1);
            this.calibrationBox.Controls.Add(this.scalingInput);
            this.calibrationBox.Location = new System.Drawing.Point(12, 322);
            this.calibrationBox.Name = "calibrationBox";
            this.calibrationBox.Size = new System.Drawing.Size(423, 129);
            this.calibrationBox.TabIndex = 7;
            this.calibrationBox.TabStop = false;
            this.calibrationBox.Text = "Calibration";
            // 
            // calibLoadBtn
            // 
            this.calibLoadBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.calibLoadBtn.Location = new System.Drawing.Point(323, 28);
            this.calibLoadBtn.Name = "calibLoadBtn";
            this.calibLoadBtn.Size = new System.Drawing.Size(94, 27);
            this.calibLoadBtn.TabIndex = 15;
            this.calibLoadBtn.Text = "Load";
            this.calibLoadBtn.UseVisualStyleBackColor = true;
            this.calibLoadBtn.Click += new System.EventHandler(this.calibLoadBtn_Click);
            // 
            // calibSaveBtn
            // 
            this.calibSaveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.calibSaveBtn.Location = new System.Drawing.Point(323, 61);
            this.calibSaveBtn.Name = "calibSaveBtn";
            this.calibSaveBtn.Size = new System.Drawing.Size(94, 27);
            this.calibSaveBtn.TabIndex = 14;
            this.calibSaveBtn.Text = "Save";
            this.calibSaveBtn.UseVisualStyleBackColor = true;
            this.calibSaveBtn.Click += new System.EventHandler(this.calibSaveBtn_Click);
            // 
            // calibBtn
            // 
            this.calibBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.calibBtn.Location = new System.Drawing.Point(223, 94);
            this.calibBtn.Name = "calibBtn";
            this.calibBtn.Size = new System.Drawing.Size(94, 27);
            this.calibBtn.TabIndex = 13;
            this.calibBtn.Text = "Calibrate";
            this.calibBtn.UseVisualStyleBackColor = true;
            this.calibBtn.Click += new System.EventHandler(this.calibBtn_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Calib. Read [mm]";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Offset [mm]";
            // 
            // calibReadInput
            // 
            this.calibReadInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.calibReadInput.DecimalPlaces = 3;
            this.calibReadInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.calibReadInput.Location = new System.Drawing.Point(138, 94);
            this.calibReadInput.Name = "calibReadInput";
            this.calibReadInput.Size = new System.Drawing.Size(79, 27);
            this.calibReadInput.TabIndex = 11;
            // 
            // offsetInput
            // 
            this.offsetInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.offsetInput.DecimalPlaces = 3;
            this.offsetInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.offsetInput.Location = new System.Drawing.Point(138, 61);
            this.offsetInput.Name = "offsetInput";
            this.offsetInput.Size = new System.Drawing.Size(79, 27);
            this.offsetInput.TabIndex = 10;
            this.offsetInput.ValueChanged += new System.EventHandler(this.offsetInput_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Scaling";
            // 
            // scalingInput
            // 
            this.scalingInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.scalingInput.DecimalPlaces = 5;
            this.scalingInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.scalingInput.Location = new System.Drawing.Point(138, 28);
            this.scalingInput.Name = "scalingInput";
            this.scalingInput.Size = new System.Drawing.Size(79, 27);
            this.scalingInput.TabIndex = 0;
            this.scalingInput.ValueChanged += new System.EventHandler(this.scalingInput_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 463);
            this.Controls.Add(this.calibrationBox);
            this.Controls.Add(this.dataBox);
            this.Controls.Add(this.chartBox);
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
            this.dataBox.ResumeLayout(false);
            this.calibrationBox.ResumeLayout(false);
            this.calibrationBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calibReadInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scalingInput)).EndInit();
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
        private Chart dataChart;
        private GroupBox chartBox;
        private GroupBox dataBox;
        private Label dataInfoLabel;
        private Button dataSaveBtn;
        private Button clearBtn;
        private GroupBox calibrationBox;
        private Label label2;
        private NumericUpDown calibReadInput;
        private NumericUpDown offsetInput;
        private Label label1;
        private NumericUpDown scalingInput;
        private Button calibBtn;
        private Label label3;
        private Button calibSaveBtn;
        private Button dataLoadBtn;
        private Button calibLoadBtn;
    }
}