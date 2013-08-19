namespace OlapWarehouseWinClient {
	partial class ConnectionForm {
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
			this.integratedSecurityCheckBox = new System.Windows.Forms.CheckBox();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.userLabel = new System.Windows.Forms.Label();
			this.userTextBox = new System.Windows.Forms.TextBox();
			this.serverTextBox = new System.Windows.Forms.TextBox();
			this.serverLabel = new System.Windows.Forms.Label();
			this.databaseLabel = new System.Windows.Forms.Label();
			this.databaseTextBox = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// integratedSecurityCheckBox
			// 
			this.integratedSecurityCheckBox.AutoSize = true;
			this.integratedSecurityCheckBox.Checked = true;
			this.integratedSecurityCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.integratedSecurityCheckBox.Location = new System.Drawing.Point(63, 74);
			this.integratedSecurityCheckBox.Margin = new System.Windows.Forms.Padding(0);
			this.integratedSecurityCheckBox.Name = "integratedSecurityCheckBox";
			this.integratedSecurityCheckBox.Size = new System.Drawing.Size(137, 17);
			this.integratedSecurityCheckBox.TabIndex = 52;
			this.integratedSecurityCheckBox.Text = "Use Integrated Security";
			this.integratedSecurityCheckBox.UseVisualStyleBackColor = true;
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.passwordTextBox.Enabled = false;
			this.passwordTextBox.Location = new System.Drawing.Point(63, 119);
			this.passwordTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.Size = new System.Drawing.Size(210, 20);
			this.passwordTextBox.TabIndex = 51;
			this.passwordTextBox.Text = "Welcome1";
			this.passwordTextBox.UseSystemPasswordChar = true;
			// 
			// passwordLabel
			// 
			this.passwordLabel.AutoSize = true;
			this.passwordLabel.Location = new System.Drawing.Point(7, 122);
			this.passwordLabel.Margin = new System.Windows.Forms.Padding(0);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(56, 13);
			this.passwordLabel.TabIndex = 50;
			this.passwordLabel.Text = "Password:";
			// 
			// userLabel
			// 
			this.userLabel.AutoSize = true;
			this.userLabel.Location = new System.Drawing.Point(31, 97);
			this.userLabel.Margin = new System.Windows.Forms.Padding(0);
			this.userLabel.Name = "userLabel";
			this.userLabel.Size = new System.Drawing.Size(32, 13);
			this.userLabel.TabIndex = 49;
			this.userLabel.Text = "User:";
			// 
			// userTextBox
			// 
			this.userTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.userTextBox.Enabled = false;
			this.userTextBox.Location = new System.Drawing.Point(63, 94);
			this.userTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.userTextBox.Name = "userTextBox";
			this.userTextBox.Size = new System.Drawing.Size(210, 20);
			this.userTextBox.TabIndex = 48;
			this.userTextBox.Text = "sa";
			// 
			// serverTextBox
			// 
			this.serverTextBox.AllowDrop = true;
			this.serverTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.serverTextBox.Location = new System.Drawing.Point(63, 9);
			this.serverTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.serverTextBox.Name = "serverTextBox";
			this.serverTextBox.Size = new System.Drawing.Size(210, 20);
			this.serverTextBox.TabIndex = 47;
			this.serverTextBox.Text = "(local)";
			// 
			// serverLabel
			// 
			this.serverLabel.AutoSize = true;
			this.serverLabel.Location = new System.Drawing.Point(22, 12);
			this.serverLabel.Margin = new System.Windows.Forms.Padding(0);
			this.serverLabel.Name = "serverLabel";
			this.serverLabel.Size = new System.Drawing.Size(41, 13);
			this.serverLabel.TabIndex = 46;
			this.serverLabel.Text = "Server:";
			// 
			// databaseLabel
			// 
			this.databaseLabel.AutoSize = true;
			this.databaseLabel.Location = new System.Drawing.Point(7, 37);
			this.databaseLabel.Margin = new System.Windows.Forms.Padding(0);
			this.databaseLabel.Name = "databaseLabel";
			this.databaseLabel.Size = new System.Drawing.Size(56, 13);
			this.databaseLabel.TabIndex = 45;
			this.databaseLabel.Text = "Database:";
			// 
			// databaseTextBox
			// 
			this.databaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.databaseTextBox.Location = new System.Drawing.Point(63, 35);
			this.databaseTextBox.Margin = new System.Windows.Forms.Padding(0);
			this.databaseTextBox.Name = "databaseTextBox";
			this.databaseTextBox.Size = new System.Drawing.Size(210, 20);
			this.databaseTextBox.TabIndex = 44;
			this.databaseTextBox.Text = "OlapWarehouse";
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(195, 155);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 53;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// ConnectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(282, 190);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.integratedSecurityCheckBox);
			this.Controls.Add(this.passwordTextBox);
			this.Controls.Add(this.passwordLabel);
			this.Controls.Add(this.userLabel);
			this.Controls.Add(this.userTextBox);
			this.Controls.Add(this.serverTextBox);
			this.Controls.Add(this.serverLabel);
			this.Controls.Add(this.databaseLabel);
			this.Controls.Add(this.databaseTextBox);
			this.Name = "ConnectionForm";
			this.Text = "Connection Settings";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox integratedSecurityCheckBox;
		private System.Windows.Forms.TextBox passwordTextBox;
		private System.Windows.Forms.Label passwordLabel;
		private System.Windows.Forms.Label userLabel;
		private System.Windows.Forms.TextBox userTextBox;
		private System.Windows.Forms.TextBox serverTextBox;
		private System.Windows.Forms.Label serverLabel;
		private System.Windows.Forms.Label databaseLabel;
		private System.Windows.Forms.TextBox databaseTextBox;
		private System.Windows.Forms.Button okButton;

	}
}