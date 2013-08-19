namespace OlapWarehouseWinClient
{
    partial class TreeForm
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
			this.mainPanel = new System.Windows.Forms.Panel();
			this.treeView = new System.Windows.Forms.TreeView();
			this.mainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.AutoSize = true;
			this.mainPanel.Controls.Add(this.treeView);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(384, 561);
			this.mainPanel.TabIndex = 8;
			// 
			// treeView
			// 
			this.treeView.BackColor = System.Drawing.SystemColors.Control;
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.ShowNodeToolTips = true;
			this.treeView.Size = new System.Drawing.Size(384, 561);
			this.treeView.TabIndex = 11;
			// 
			// TreeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 561);
			this.Controls.Add(this.mainPanel);
			this.Name = "TreeForm";
			this.Text = "Hierarchy";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TreeForm_FormClosing);
			this.mainPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TreeView treeView;

    }
}