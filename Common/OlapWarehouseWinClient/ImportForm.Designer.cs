namespace OlapWarehouseWinClient {
	partial class ImportForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
			this.mainPanel = new System.Windows.Forms.Panel();
			this.importColumnsLabel = new System.Windows.Forms.Label();
			this.importColumnsListBox = new System.Windows.Forms.CheckedListBox();
			this.parentSeparatorComboBox = new System.Windows.Forms.ComboBox();
			this.parentSeparatorLabel = new System.Windows.Forms.Label();
			this.captionColumnComboBox = new System.Windows.Forms.ComboBox();
			this.captionColumnLabel = new System.Windows.Forms.Label();
			this.parentColumnComboBox = new System.Windows.Forms.ComboBox();
			this.parentColumnLabel = new System.Windows.Forms.Label();
			this.idColumnComboBox = new System.Windows.Forms.ComboBox();
			this.idColumnLabel = new System.Windows.Forms.Label();
			this.sheetComboBox = new System.Windows.Forms.ComboBox();
			this.sheetLabel = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.cancelButton = new System.Windows.Forms.Button();
			this.startButton = new System.Windows.Forms.Button();
			this.jobBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.mainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.Controls.Add(this.importColumnsLabel);
			this.mainPanel.Controls.Add(this.importColumnsListBox);
			this.mainPanel.Controls.Add(this.parentSeparatorComboBox);
			this.mainPanel.Controls.Add(this.parentSeparatorLabel);
			this.mainPanel.Controls.Add(this.captionColumnComboBox);
			this.mainPanel.Controls.Add(this.captionColumnLabel);
			this.mainPanel.Controls.Add(this.parentColumnComboBox);
			this.mainPanel.Controls.Add(this.parentColumnLabel);
			this.mainPanel.Controls.Add(this.idColumnComboBox);
			this.mainPanel.Controls.Add(this.idColumnLabel);
			this.mainPanel.Controls.Add(this.sheetComboBox);
			this.mainPanel.Controls.Add(this.sheetLabel);
			this.mainPanel.Controls.Add(this.flowLayoutPanel1);
			this.mainPanel.Controls.Add(this.cancelButton);
			this.mainPanel.Controls.Add(this.startButton);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(384, 361);
			this.mainPanel.TabIndex = 18;
			// 
			// importColumnsLabel
			// 
			this.importColumnsLabel.AutoSize = true;
			this.importColumnsLabel.Location = new System.Drawing.Point(84, 159);
			this.importColumnsLabel.Name = "importColumnsLabel";
			this.importColumnsLabel.Size = new System.Drawing.Size(82, 13);
			this.importColumnsLabel.TabIndex = 68;
			this.importColumnsLabel.Text = "Import Columns:";
			// 
			// importColumnsListBox
			// 
			this.importColumnsListBox.Enabled = false;
			this.importColumnsListBox.FormattingEnabled = true;
			this.importColumnsListBox.Location = new System.Drawing.Point(172, 159);
			this.importColumnsListBox.Name = "importColumnsListBox";
			this.importColumnsListBox.Size = new System.Drawing.Size(150, 139);
			this.importColumnsListBox.TabIndex = 67;
			this.importColumnsListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.importColumnsListBox_ItemCheck);
			// 
			// parentSeparatorComboBox
			// 
			this.parentSeparatorComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.parentSeparatorComboBox.Enabled = false;
			this.parentSeparatorComboBox.FormattingEnabled = true;
			this.parentSeparatorComboBox.Items.AddRange(new object[] {
            "/",
            ":"});
			this.parentSeparatorComboBox.Location = new System.Drawing.Point(172, 104);
			this.parentSeparatorComboBox.MaxLength = 1;
			this.parentSeparatorComboBox.Name = "parentSeparatorComboBox";
			this.parentSeparatorComboBox.Size = new System.Drawing.Size(150, 21);
			this.parentSeparatorComboBox.TabIndex = 66;
			this.parentSeparatorComboBox.SelectedIndexChanged += new System.EventHandler(this.parentSeparatorComboBox_SelectedIndexChanged);
			this.parentSeparatorComboBox.TextChanged += new System.EventHandler(this.parentSeparatorComboBox_TextChanged);
			// 
			// parentSeparatorLabel
			// 
			this.parentSeparatorLabel.AutoSize = true;
			this.parentSeparatorLabel.Location = new System.Drawing.Point(78, 107);
			this.parentSeparatorLabel.Name = "parentSeparatorLabel";
			this.parentSeparatorLabel.Size = new System.Drawing.Size(88, 13);
			this.parentSeparatorLabel.TabIndex = 65;
			this.parentSeparatorLabel.Text = "Parent separator:";
			// 
			// captionColumnComboBox
			// 
			this.captionColumnComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.captionColumnComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.captionColumnComboBox.Enabled = false;
			this.captionColumnComboBox.FormattingEnabled = true;
			this.captionColumnComboBox.Location = new System.Drawing.Point(172, 131);
			this.captionColumnComboBox.Name = "captionColumnComboBox";
			this.captionColumnComboBox.Size = new System.Drawing.Size(150, 21);
			this.captionColumnComboBox.TabIndex = 64;
			this.captionColumnComboBox.SelectedIndexChanged += new System.EventHandler(this.captionColumnComboBox_SelectedIndexChanged);
			// 
			// captionColumnLabel
			// 
			this.captionColumnLabel.AutoSize = true;
			this.captionColumnLabel.Location = new System.Drawing.Point(83, 134);
			this.captionColumnLabel.Name = "captionColumnLabel";
			this.captionColumnLabel.Size = new System.Drawing.Size(83, 13);
			this.captionColumnLabel.TabIndex = 63;
			this.captionColumnLabel.Text = "Caption column:";
			// 
			// parentColumnComboBox
			// 
			this.parentColumnComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.parentColumnComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.parentColumnComboBox.Enabled = false;
			this.parentColumnComboBox.FormattingEnabled = true;
			this.parentColumnComboBox.Location = new System.Drawing.Point(172, 77);
			this.parentColumnComboBox.Name = "parentColumnComboBox";
			this.parentColumnComboBox.Size = new System.Drawing.Size(150, 21);
			this.parentColumnComboBox.TabIndex = 62;
			this.parentColumnComboBox.SelectedIndexChanged += new System.EventHandler(this.parentColumnComboBox_SelectedIndexChanged);
			// 
			// parentColumnLabel
			// 
			this.parentColumnLabel.AutoSize = true;
			this.parentColumnLabel.Location = new System.Drawing.Point(88, 80);
			this.parentColumnLabel.Name = "parentColumnLabel";
			this.parentColumnLabel.Size = new System.Drawing.Size(78, 13);
			this.parentColumnLabel.TabIndex = 61;
			this.parentColumnLabel.Text = "Parent column:";
			// 
			// idColumnComboBox
			// 
			this.idColumnComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.idColumnComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.idColumnComboBox.Enabled = false;
			this.idColumnComboBox.FormattingEnabled = true;
			this.idColumnComboBox.Location = new System.Drawing.Point(172, 50);
			this.idColumnComboBox.Name = "idColumnComboBox";
			this.idColumnComboBox.Size = new System.Drawing.Size(150, 21);
			this.idColumnComboBox.TabIndex = 60;
			this.idColumnComboBox.SelectedIndexChanged += new System.EventHandler(this.idColumnComboBox_SelectedIndexChanged);
			// 
			// idColumnLabel
			// 
			this.idColumnLabel.AutoSize = true;
			this.idColumnLabel.Location = new System.Drawing.Point(110, 53);
			this.idColumnLabel.Name = "idColumnLabel";
			this.idColumnLabel.Size = new System.Drawing.Size(56, 13);
			this.idColumnLabel.TabIndex = 59;
			this.idColumnLabel.Text = "Id column:";
			// 
			// sheetComboBox
			// 
			this.sheetComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sheetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.sheetComboBox.Enabled = false;
			this.sheetComboBox.FormattingEnabled = true;
			this.sheetComboBox.Location = new System.Drawing.Point(172, 23);
			this.sheetComboBox.Name = "sheetComboBox";
			this.sheetComboBox.Size = new System.Drawing.Size(150, 21);
			this.sheetComboBox.TabIndex = 53;
			this.sheetComboBox.SelectedIndexChanged += new System.EventHandler(this.sheetComboBox_SelectedIndexChanged);
			// 
			// sheetLabel
			// 
			this.sheetLabel.AutoSize = true;
			this.sheetLabel.Location = new System.Drawing.Point(128, 26);
			this.sheetLabel.Name = "sheetLabel";
			this.sheetLabel.Size = new System.Drawing.Size(38, 13);
			this.sheetLabel.TabIndex = 58;
			this.sheetLabel.Text = "Sheet:";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(384, 0);
			this.flowLayoutPanel1.TabIndex = 59;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(172, 329);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(100, 23);
			this.cancelButton.TabIndex = 55;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// startButton
			// 
			this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.startButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.startButton.Location = new System.Drawing.Point(275, 329);
			this.startButton.Margin = new System.Windows.Forms.Padding(0);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(100, 23);
			this.startButton.TabIndex = 54;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(23, 23);
			// 
			// ImportForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(384, 361);
			this.Controls.Add(this.mainPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(800, 400);
			this.MinimumSize = new System.Drawing.Size(400, 250);
			this.Name = "ImportForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Import Excel";
			this.TransparencyKey = System.Drawing.Color.SeaShell;
			this.mainPanel.ResumeLayout(false);
			this.mainPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel mainPanel;
		private System.ComponentModel.BackgroundWorker jobBackgroundWorker;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.ComboBox sheetComboBox;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label sheetLabel;
		private System.Windows.Forms.Label idColumnLabel;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.ComboBox parentSeparatorComboBox;
		private System.Windows.Forms.Label parentSeparatorLabel;
		private System.Windows.Forms.ComboBox captionColumnComboBox;
		private System.Windows.Forms.Label captionColumnLabel;
		private System.Windows.Forms.ComboBox parentColumnComboBox;
		private System.Windows.Forms.Label parentColumnLabel;
		private System.Windows.Forms.ComboBox idColumnComboBox;
		private System.Windows.Forms.Label importColumnsLabel;
		private System.Windows.Forms.CheckedListBox importColumnsListBox;
	}
}

