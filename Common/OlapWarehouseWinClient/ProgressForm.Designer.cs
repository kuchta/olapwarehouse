namespace OlapWarehouseWinClient {
	partial class ProgressForm {
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
			this.stopButton = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.closeButton = new System.Windows.Forms.Button();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.progressLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.jobBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// stopButton
			// 
			this.stopButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.stopButton.Enabled = false;
			this.stopButton.Location = new System.Drawing.Point(146, 34);
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(100, 23);
			this.stopButton.TabIndex = 60;
			this.stopButton.Text = "Stop";
			this.stopButton.UseVisualStyleBackColor = true;
			this.stopButton.Visible = false;
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(12, 8);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(360, 20);
			this.progressBar.TabIndex = 59;
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.closeButton.Location = new System.Drawing.Point(272, 66);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(100, 23);
			this.closeButton.TabIndex = 58;
			this.closeButton.Text = "Close";
			this.closeButton.UseVisualStyleBackColor = true;
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressLabel,
            this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 92);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(384, 22);
			this.statusStrip.TabIndex = 61;
			this.statusStrip.Text = "statusStrip1";
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
			this.statusLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// jobBackgroundWorker
			// 
			this.jobBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.jobBackgroundWorker_DoWork);
			this.jobBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.jobBackgroundWorker_RunWorkerCompleted);
			// 
			// ProgressForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 114);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.stopButton);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.closeButton);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProgressForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Progress";
			this.Shown += new System.EventHandler(this.ProgressForm_Shown);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel progressLabel;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.ComponentModel.BackgroundWorker jobBackgroundWorker;
	}
}