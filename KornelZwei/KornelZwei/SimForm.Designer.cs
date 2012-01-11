namespace KornelZwei
{
    partial class SimForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimForm));
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.panelSockets = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnNextStep = new System.Windows.Forms.Button();
            this.btnNextEvent = new System.Windows.Forms.Button();
            this.tbTimeInterval = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStepInterval = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbSimulationSteps = new System.Windows.Forms.TextBox();
            this.checkBoxRefresh = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dgvStats = new System.Windows.Forms.DataGridView();
            this.Nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wartość = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStatsQueue = new System.Windows.Forms.DataGridView();
            this.QuId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AvgTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sumT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avgQCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStatsDevice = new System.Windows.Forms.DataGridView();
            this.devId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avgW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avgB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatsQueue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatsDevice)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(348, 12);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(535, 156);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // panelSockets
            // 
            this.panelSockets.Location = new System.Drawing.Point(12, 12);
            this.panelSockets.Name = "panelSockets";
            this.panelSockets.Size = new System.Drawing.Size(330, 348);
            this.panelSockets.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 366);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(111, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 395);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(111, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Pauza";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnNextStep
            // 
            this.btnNextStep.Location = new System.Drawing.Point(12, 424);
            this.btnNextStep.Name = "btnNextStep";
            this.btnNextStep.Size = new System.Drawing.Size(111, 23);
            this.btnNextStep.TabIndex = 4;
            this.btnNextStep.Text = "Następny krok";
            this.btnNextStep.UseVisualStyleBackColor = true;
            this.btnNextStep.Click += new System.EventHandler(this.btnNextStep_Click);
            // 
            // btnNextEvent
            // 
            this.btnNextEvent.Location = new System.Drawing.Point(12, 453);
            this.btnNextEvent.Name = "btnNextEvent";
            this.btnNextEvent.Size = new System.Drawing.Size(111, 23);
            this.btnNextEvent.TabIndex = 5;
            this.btnNextEvent.Text = "Następne zdarzenie";
            this.btnNextEvent.UseVisualStyleBackColor = true;
            this.btnNextEvent.Click += new System.EventHandler(this.btnNextEvent_Click);
            // 
            // tbTimeInterval
            // 
            this.tbTimeInterval.Location = new System.Drawing.Point(6, 19);
            this.tbTimeInterval.Name = "tbTimeInterval";
            this.tbTimeInterval.Size = new System.Drawing.Size(75, 20);
            this.tbTimeInterval.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStepInterval);
            this.groupBox1.Controls.Add(this.tbTimeInterval);
            this.groupBox1.Location = new System.Drawing.Point(130, 367);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 51);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wizualizacja";
            // 
            // btnStepInterval
            // 
            this.btnStepInterval.Location = new System.Drawing.Point(88, 15);
            this.btnStepInterval.Name = "btnStepInterval";
            this.btnStepInterval.Size = new System.Drawing.Size(116, 23);
            this.btnStepInterval.TabIndex = 7;
            this.btnStepInterval.Text = "Zmień długość kroku";
            this.btnStepInterval.UseVisualStyleBackColor = true;
            this.btnStepInterval.Click += new System.EventHandler(this.btnStepInterval_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.tbSimulationSteps);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(130, 425);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 70);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Symulacja";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(88, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Rozpocznij symulację";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbSimulationSteps
            // 
            this.tbSimulationSteps.Location = new System.Drawing.Point(6, 19);
            this.tbSimulationSteps.Name = "tbSimulationSteps";
            this.tbSimulationSteps.Size = new System.Drawing.Size(75, 20);
            this.tbSimulationSteps.TabIndex = 6;
            // 
            // checkBoxRefresh
            // 
            this.checkBoxRefresh.AutoSize = true;
            this.checkBoxRefresh.Checked = true;
            this.checkBoxRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRefresh.Location = new System.Drawing.Point(13, 483);
            this.checkBoxRefresh.Name = "checkBoxRefresh";
            this.checkBoxRefresh.Size = new System.Drawing.Size(86, 17);
            this.checkBoxRefresh.TabIndex = 8;
            this.checkBoxRefresh.Text = "Odświeżanie";
            this.checkBoxRefresh.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 48);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(107, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Zapisz do Excela";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // dgvStats
            // 
            this.dgvStats.AllowUserToAddRows = false;
            this.dgvStats.AllowUserToDeleteRows = false;
            this.dgvStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nazwa,
            this.Wartość});
            this.dgvStats.Location = new System.Drawing.Point(348, 174);
            this.dgvStats.Name = "dgvStats";
            this.dgvStats.RowHeadersVisible = false;
            this.dgvStats.Size = new System.Drawing.Size(535, 135);
            this.dgvStats.TabIndex = 40;
            // 
            // Nazwa
            // 
            this.Nazwa.HeaderText = "Nazwa";
            this.Nazwa.Name = "Nazwa";
            this.Nazwa.ReadOnly = true;
            // 
            // Wartość
            // 
            this.Wartość.HeaderText = "Wartość";
            this.Wartość.Name = "Wartość";
            this.Wartość.ReadOnly = true;
            // 
            // dgvStatsQueue
            // 
            this.dgvStatsQueue.AllowUserToAddRows = false;
            this.dgvStatsQueue.AllowUserToDeleteRows = false;
            this.dgvStatsQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvStatsQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStatsQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatsQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QuId,
            this.AvgTime,
            this.sumT,
            this.maxT,
            this.maxCount,
            this.avgQCount});
            this.dgvStatsQueue.Location = new System.Drawing.Point(348, 408);
            this.dgvStatsQueue.Name = "dgvStatsQueue";
            this.dgvStatsQueue.RowHeadersVisible = false;
            this.dgvStatsQueue.Size = new System.Drawing.Size(535, 87);
            this.dgvStatsQueue.TabIndex = 42;
            // 
            // QuId
            // 
            this.QuId.HeaderText = "ID Kolejki";
            this.QuId.Name = "QuId";
            // 
            // AvgTime
            // 
            this.AvgTime.HeaderText = "Śr. czas";
            this.AvgTime.Name = "AvgTime";
            // 
            // sumT
            // 
            this.sumT.HeaderText = "Łączny czas";
            this.sumT.Name = "sumT";
            this.sumT.ReadOnly = true;
            // 
            // maxT
            // 
            this.maxT.HeaderText = "Maks. czas";
            this.maxT.Name = "maxT";
            this.maxT.ReadOnly = true;
            // 
            // maxCount
            // 
            this.maxCount.HeaderText = "Maks. liczba zadań";
            this.maxCount.Name = "maxCount";
            // 
            // avgQCount
            // 
            this.avgQCount.HeaderText = "Śr. liczba zadań";
            this.avgQCount.Name = "avgQCount";
            this.avgQCount.ReadOnly = true;
            // 
            // dgvStatsDevice
            // 
            this.dgvStatsDevice.AllowUserToAddRows = false;
            this.dgvStatsDevice.AllowUserToDeleteRows = false;
            this.dgvStatsDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvStatsDevice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStatsDevice.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvStatsDevice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatsDevice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.devId,
            this.allW,
            this.allB,
            this.avgW,
            this.avgB});
            this.dgvStatsDevice.Location = new System.Drawing.Point(348, 315);
            this.dgvStatsDevice.Name = "dgvStatsDevice";
            this.dgvStatsDevice.RowHeadersVisible = false;
            this.dgvStatsDevice.Size = new System.Drawing.Size(535, 87);
            this.dgvStatsDevice.TabIndex = 41;
            // 
            // devId
            // 
            this.devId.HeaderText = "ID maszyny";
            this.devId.MaxInputLength = 100;
            this.devId.Name = "devId";
            this.devId.ReadOnly = true;
            // 
            // allW
            // 
            this.allW.HeaderText = "Łączny czas pracy";
            this.allW.MaxInputLength = 5;
            this.allW.Name = "allW";
            // 
            // allB
            // 
            this.allB.HeaderText = "Łączny czas blok.";
            this.allB.MaxInputLength = 5;
            this.allB.Name = "allB";
            // 
            // avgW
            // 
            this.avgW.HeaderText = "Śr. czas pracy";
            this.avgW.MaxInputLength = 5;
            this.avgW.Name = "avgW";
            // 
            // avgB
            // 
            this.avgB.HeaderText = "Śr. czas blok.";
            this.avgB.MaxInputLength = 5;
            this.avgB.Name = "avgB";
            // 
            // SimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 507);
            this.Controls.Add(this.dgvStatsQueue);
            this.Controls.Add(this.dgvStatsDevice);
            this.Controls.Add(this.dgvStats);
            this.Controls.Add(this.checkBoxRefresh);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnNextEvent);
            this.Controls.Add(this.btnNextStep);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.panelSockets);
            this.Controls.Add(this.rtbLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SimForm";
            this.Text = "SimForm";
            this.Load += new System.EventHandler(this.SimForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatsQueue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatsDevice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Panel panelSockets;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnNextStep;
        private System.Windows.Forms.Button btnNextEvent;
        private System.Windows.Forms.TextBox tbTimeInterval;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStepInterval;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbSimulationSteps;
        private System.Windows.Forms.CheckBox checkBoxRefresh;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridView dgvStats;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wartość;
        private System.Windows.Forms.DataGridView dgvStatsQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuId;
        private System.Windows.Forms.DataGridViewTextBoxColumn AvgTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn sumT;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxT;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn avgQCount;
        private System.Windows.Forms.DataGridView dgvStatsDevice;
        private System.Windows.Forms.DataGridViewTextBoxColumn devId;
        private System.Windows.Forms.DataGridViewTextBoxColumn allW;
        private System.Windows.Forms.DataGridViewTextBoxColumn allB;
        private System.Windows.Forms.DataGridViewTextBoxColumn avgW;
        private System.Windows.Forms.DataGridViewTextBoxColumn avgB;
    }
}