namespace ImportWDI {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.mainPanel = new System.Windows.Forms.Panel();
			this.inputGroupBox = new System.Windows.Forms.GroupBox();
			this.inputFileButton = new System.Windows.Forms.Button();
			this.inputFileTextBox = new System.Windows.Forms.TextBox();
			this.outputGroupBox = new System.Windows.Forms.GroupBox();
			this.integratedSecurityCheckBox = new System.Windows.Forms.CheckBox();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.userLabel = new System.Windows.Forms.Label();
			this.userTextBox = new System.Windows.Forms.TextBox();
			this.serverTextBox = new System.Windows.Forms.TextBox();
			this.serverLabel = new System.Windows.Forms.Label();
			this.databaseLabel = new System.Windows.Forms.Label();
			this.databaseTextBox = new System.Windows.Forms.TextBox();
			this.controlsGroupBox = new System.Windows.Forms.GroupBox();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.progressLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.jobBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.stopButton = new System.Windows.Forms.Button();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.showCountriesButton = new System.Windows.Forms.Button();
			this.showIndicatorsButton = new System.Windows.Forms.Button();
			this.showYearsButton = new System.Windows.Forms.Button();
			this.transferDataButton = new System.Windows.Forms.Button();
			this.transferFactsButton = new System.Windows.Forms.Button();
			this.transferDimensionsButton = new System.Windows.Forms.Button();
			this.mainPanel.SuspendLayout();
			this.inputGroupBox.SuspendLayout();
			this.outputGroupBox.SuspendLayout();
			this.controlsGroupBox.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.Controls.Add(this.inputGroupBox);
			this.mainPanel.Controls.Add(this.outputGroupBox);
			this.mainPanel.Controls.Add(this.controlsGroupBox);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(434, 249);
			this.mainPanel.TabIndex = 18;
			// 
			// inputGroupBox
			// 
			this.inputGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.inputGroupBox.Controls.Add(this.inputFileButton);
			this.inputGroupBox.Controls.Add(this.inputFileTextBox);
			this.inputGroupBox.Location = new System.Drawing.Point(15, 10);
			this.inputGroupBox.Name = "inputGroupBox";
			this.inputGroupBox.Size = new System.Drawing.Size(405, 55);
			this.inputGroupBox.TabIndex = 39;
			this.inputGroupBox.TabStop = false;
			this.inputGroupBox.Text = "Input File";
			// 
			// inputFileButton
			// 
			this.inputFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.inputFileButton.Location = new System.Drawing.Point(339, 20);
			this.inputFileButton.Margin = new System.Windows.Forms.Padding(0);
			this.inputFileButton.Name = "inputFileButton";
			this.inputFileButton.Size = new System.Drawing.Size(50, 22);
			this.inputFileButton.TabIndex = 27;
			this.inputFileButton.Text = "Find";
			this.inputFileButton.UseVisualStyleBackColor = true;
			this.inputFileButton.Click += new System.EventHandler(this.btnInputFile_Click);
			// 
			// inputFileTextBox
			// 
			this.inputFileTextBox.AllowDrop = true;
			this.inputFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.inputFileTextBox.Location = new System.Drawing.Point(15, 20);
			this.inputFileTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.inputFileTextBox.Name = "inputFileTextBox";
			this.inputFileTextBox.Size = new System.Drawing.Size(319, 20);
			this.inputFileTextBox.TabIndex = 28;
			this.inputFileTextBox.TextChanged += new System.EventHandler(this.tbInputFile_TextChanged);
			// 
			// outputGroupBox
			// 
			this.outputGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.outputGroupBox.Controls.Add(this.integratedSecurityCheckBox);
			this.outputGroupBox.Controls.Add(this.passwordTextBox);
			this.outputGroupBox.Controls.Add(this.passwordLabel);
			this.outputGroupBox.Controls.Add(this.userLabel);
			this.outputGroupBox.Controls.Add(this.userTextBox);
			this.outputGroupBox.Controls.Add(this.serverTextBox);
			this.outputGroupBox.Controls.Add(this.serverLabel);
			this.outputGroupBox.Controls.Add(this.databaseLabel);
			this.outputGroupBox.Controls.Add(this.databaseTextBox);
			this.outputGroupBox.Location = new System.Drawing.Point(15, 72);
			this.outputGroupBox.Name = "outputGroupBox";
			this.outputGroupBox.Size = new System.Drawing.Size(250, 160);
			this.outputGroupBox.TabIndex = 38;
			this.outputGroupBox.TabStop = false;
			this.outputGroupBox.Text = "Output Database";
			// 
			// integratedSecurityCheckBox
			// 
			this.integratedSecurityCheckBox.AutoSize = true;
			this.integratedSecurityCheckBox.Checked = true;
			this.integratedSecurityCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.integratedSecurityCheckBox.Location = new System.Drawing.Point(90, 85);
			this.integratedSecurityCheckBox.Margin = new System.Windows.Forms.Padding(0);
			this.integratedSecurityCheckBox.Name = "integratedSecurityCheckBox";
			this.integratedSecurityCheckBox.Size = new System.Drawing.Size(137, 17);
			this.integratedSecurityCheckBox.TabIndex = 43;
			this.integratedSecurityCheckBox.Text = "Use Integrated Security";
			this.integratedSecurityCheckBox.UseVisualStyleBackColor = true;
			this.integratedSecurityCheckBox.CheckedChanged += new System.EventHandler(this.integratedSecurityCheckBox_CheckedChanged);
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.passwordTextBox.Enabled = false;
			this.passwordTextBox.Location = new System.Drawing.Point(90, 130);
			this.passwordTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.Size = new System.Drawing.Size(150, 20);
			this.passwordTextBox.TabIndex = 42;
			this.passwordTextBox.Text = "Welcome1";
			this.passwordTextBox.UseSystemPasswordChar = true;
			// 
			// passwordLabel
			// 
			this.passwordLabel.AutoSize = true;
			this.passwordLabel.Location = new System.Drawing.Point(34, 133);
			this.passwordLabel.Margin = new System.Windows.Forms.Padding(0);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(56, 13);
			this.passwordLabel.TabIndex = 41;
			this.passwordLabel.Text = "Password:";
			// 
			// userLabel
			// 
			this.userLabel.AutoSize = true;
			this.userLabel.Location = new System.Drawing.Point(58, 108);
			this.userLabel.Margin = new System.Windows.Forms.Padding(0);
			this.userLabel.Name = "userLabel";
			this.userLabel.Size = new System.Drawing.Size(32, 13);
			this.userLabel.TabIndex = 40;
			this.userLabel.Text = "User:";
			// 
			// userTextBox
			// 
			this.userTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.userTextBox.Enabled = false;
			this.userTextBox.Location = new System.Drawing.Point(90, 105);
			this.userTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.userTextBox.Name = "userTextBox";
			this.userTextBox.Size = new System.Drawing.Size(150, 20);
			this.userTextBox.TabIndex = 39;
			this.userTextBox.Text = "sa";
			// 
			// serverTextBox
			// 
			this.serverTextBox.AllowDrop = true;
			this.serverTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.serverTextBox.Location = new System.Drawing.Point(90, 20);
			this.serverTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.serverTextBox.Name = "serverTextBox";
			this.serverTextBox.Size = new System.Drawing.Size(150, 20);
			this.serverTextBox.TabIndex = 38;
			this.serverTextBox.Text = "(local)";
			// 
			// serverLabel
			// 
			this.serverLabel.AutoSize = true;
			this.serverLabel.Location = new System.Drawing.Point(49, 23);
			this.serverLabel.Margin = new System.Windows.Forms.Padding(0);
			this.serverLabel.Name = "serverLabel";
			this.serverLabel.Size = new System.Drawing.Size(41, 13);
			this.serverLabel.TabIndex = 37;
			this.serverLabel.Text = "Server:";
			// 
			// databaseLabel
			// 
			this.databaseLabel.AutoSize = true;
			this.databaseLabel.Location = new System.Drawing.Point(34, 48);
			this.databaseLabel.Margin = new System.Windows.Forms.Padding(0);
			this.databaseLabel.Name = "databaseLabel";
			this.databaseLabel.Size = new System.Drawing.Size(56, 13);
			this.databaseLabel.TabIndex = 36;
			this.databaseLabel.Text = "Database:";
			// 
			// databaseTextBox
			// 
			this.databaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.databaseTextBox.Location = new System.Drawing.Point(90, 46);
			this.databaseTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.databaseTextBox.Name = "databaseTextBox";
			this.databaseTextBox.Size = new System.Drawing.Size(150, 20);
			this.databaseTextBox.TabIndex = 35;
			this.databaseTextBox.Text = "OlapWarehouse";
			// 
			// controlsGroupBox
			// 
			this.controlsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.controlsGroupBox.Controls.Add(this.showIndicatorsButton);
			this.controlsGroupBox.Controls.Add(this.showYearsButton);
			this.controlsGroupBox.Controls.Add(this.transferDataButton);
			this.controlsGroupBox.Controls.Add(this.showCountriesButton);
			this.controlsGroupBox.Controls.Add(this.transferFactsButton);
			this.controlsGroupBox.Controls.Add(this.transferDimensionsButton);
			this.controlsGroupBox.Enabled = false;
			this.controlsGroupBox.Location = new System.Drawing.Point(280, 72);
			this.controlsGroupBox.Name = "controlsGroupBox";
			this.controlsGroupBox.Padding = new System.Windows.Forms.Padding(0);
			this.controlsGroupBox.Size = new System.Drawing.Size(140, 160);
			this.controlsGroupBox.TabIndex = 36;
			this.controlsGroupBox.TabStop = false;
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.progressLabel,
            this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 249);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(434, 22);
			this.statusStrip.TabIndex = 40;
			this.statusStrip.Text = "statusStrip1";
			// 
			// progressBar
			// 
			this.progressBar.ForeColor = System.Drawing.SystemColors.Desktop;
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(50, 16);
			this.progressBar.Step = 1;
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar.Visible = false;
			// 
			// progressLabel
			// 
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.Size = new System.Drawing.Size(36, 17);
			this.progressLabel.Text = "0/100";
			this.progressLabel.Visible = false;
			// 
			// statusLabel
			// 
			this.statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(150, 17);
			this.statusLabel.Text = "Start by selecting input file.";
			// 
			// jobBackgroundWorker
			// 
			this.jobBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.jobBackgroundWorker_DoWork);
			this.jobBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.jobBackgroundWorker_RunWorkerCompleted);
			// 
			// stopButton
			// 
			this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.stopButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.stopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.stopButton.Location = new System.Drawing.Point(290, 250);
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(120, 20);
			this.stopButton.TabIndex = 41;
			this.stopButton.Text = "Stop";
			this.stopButton.UseVisualStyleBackColor = false;
			this.stopButton.Visible = false;
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(23, 23);
			// 
			// showCountriesButton
			// 
			this.showCountriesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.showCountriesButton.Location = new System.Drawing.Point(2, 13);
			this.showCountriesButton.Margin = new System.Windows.Forms.Padding(0);
			this.showCountriesButton.Name = "showCountriesButton";
			this.showCountriesButton.Size = new System.Drawing.Size(136, 20);
			this.showCountriesButton.TabIndex = 70;
			this.showCountriesButton.Text = "Show Countries";
			this.showCountriesButton.UseVisualStyleBackColor = true;
			this.showCountriesButton.Click += new System.EventHandler(this.showCountriesButton_Click);
			// 
			// showIndicatorsButton
			// 
			this.showIndicatorsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.showIndicatorsButton.Location = new System.Drawing.Point(2, 53);
			this.showIndicatorsButton.Margin = new System.Windows.Forms.Padding(0);
			this.showIndicatorsButton.Name = "showIndicatorsButton";
			this.showIndicatorsButton.Size = new System.Drawing.Size(136, 20);
			this.showIndicatorsButton.TabIndex = 68;
			this.showIndicatorsButton.Text = "Show Indicators";
			this.showIndicatorsButton.UseVisualStyleBackColor = true;
			this.showIndicatorsButton.Click += new System.EventHandler(this.showIndicatorsButton_Click);
			// 
			// showYearsButton
			// 
			this.showYearsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.showYearsButton.Location = new System.Drawing.Point(2, 33);
			this.showYearsButton.Margin = new System.Windows.Forms.Padding(0);
			this.showYearsButton.Name = "showYearsButton";
			this.showYearsButton.Size = new System.Drawing.Size(136, 20);
			this.showYearsButton.TabIndex = 72;
			this.showYearsButton.Text = "Show Years";
			this.showYearsButton.UseVisualStyleBackColor = true;
			this.showYearsButton.Click += new System.EventHandler(this.showYearsButton_Click);
			// 
			// transferDataButton
			// 
			this.transferDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.transferDataButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.transferDataButton.Location = new System.Drawing.Point(2, 91);
			this.transferDataButton.Margin = new System.Windows.Forms.Padding(0);
			this.transferDataButton.Name = "transferDataButton";
			this.transferDataButton.Size = new System.Drawing.Size(136, 20);
			this.transferDataButton.TabIndex = 73;
			this.transferDataButton.Text = "Transfer Data";
			this.transferDataButton.UseVisualStyleBackColor = true;
			this.transferDataButton.Click += new System.EventHandler(this.transferDataButton_Click);
			// 
			// transferFactsButton
			// 
			this.transferFactsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.transferFactsButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.transferFactsButton.Location = new System.Drawing.Point(2, 133);
			this.transferFactsButton.Margin = new System.Windows.Forms.Padding(0);
			this.transferFactsButton.Name = "transferFactsButton";
			this.transferFactsButton.Size = new System.Drawing.Size(136, 20);
			this.transferFactsButton.TabIndex = 71;
			this.transferFactsButton.Text = "Transfer Facts";
			this.transferFactsButton.UseVisualStyleBackColor = true;
			this.transferFactsButton.Click += new System.EventHandler(this.transferFactsButton_Click);
			// 
			// transferDimensionsButton
			// 
			this.transferDimensionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.transferDimensionsButton.AutoSize = true;
			this.transferDimensionsButton.BackColor = System.Drawing.SystemColors.Control;
			this.transferDimensionsButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
			this.transferDimensionsButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.transferDimensionsButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.transferDimensionsButton.Location = new System.Drawing.Point(2, 111);
			this.transferDimensionsButton.Margin = new System.Windows.Forms.Padding(0);
			this.transferDimensionsButton.Name = "transferDimensionsButton";
			this.transferDimensionsButton.Size = new System.Drawing.Size(136, 22);
			this.transferDimensionsButton.TabIndex = 69;
			this.transferDimensionsButton.Text = "Transfer Dimensions";
			this.transferDimensionsButton.UseVisualStyleBackColor = false;
			this.transferDimensionsButton.Click += new System.EventHandler(this.transferDimensionsButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(434, 271);
			this.Controls.Add(this.stopButton);
			this.Controls.Add(this.mainPanel);
			this.Controls.Add(this.statusStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1000, 310);
			this.MinimumSize = new System.Drawing.Size(450, 310);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ImportWDI";
			this.TransparencyKey = System.Drawing.Color.SeaShell;
			this.mainPanel.ResumeLayout(false);
			this.inputGroupBox.ResumeLayout(false);
			this.inputGroupBox.PerformLayout();
			this.outputGroupBox.ResumeLayout(false);
			this.outputGroupBox.PerformLayout();
			this.controlsGroupBox.ResumeLayout(false);
			this.controlsGroupBox.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.GroupBox controlsGroupBox;
		private System.Windows.Forms.GroupBox inputGroupBox;
		private System.Windows.Forms.Button inputFileButton;
		private System.Windows.Forms.TextBox inputFileTextBox;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripProgressBar progressBar;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.ComponentModel.BackgroundWorker jobBackgroundWorker;
		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.GroupBox outputGroupBox;
		private System.Windows.Forms.CheckBox integratedSecurityCheckBox;
		private System.Windows.Forms.TextBox passwordTextBox;
		private System.Windows.Forms.Label passwordLabel;
		private System.Windows.Forms.Label userLabel;
		private System.Windows.Forms.TextBox userTextBox;
		private System.Windows.Forms.TextBox serverTextBox;
		private System.Windows.Forms.Label serverLabel;
		private System.Windows.Forms.Label databaseLabel;
		private System.Windows.Forms.TextBox databaseTextBox;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel progressLabel;
		private System.Windows.Forms.Button showIndicatorsButton;
		private System.Windows.Forms.Button showYearsButton;
		private System.Windows.Forms.Button transferDataButton;
		private System.Windows.Forms.Button showCountriesButton;
		private System.Windows.Forms.Button transferFactsButton;
		private System.Windows.Forms.Button transferDimensionsButton;
	}
}

