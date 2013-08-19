using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OlapWarehouseWinClient {
	public partial class ConnectionForm : Form {
		public string Server { get; set; }
		public string Database { get; set; }
		public bool IntegratedSecurity { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }

		public ConnectionForm() {
			InitializeComponent();

			Server = serverTextBox.Text;
			Database = databaseTextBox.Text;
			IntegratedSecurity = integratedSecurityCheckBox.Checked;
			if (!IntegratedSecurity) {
				UserName = userTextBox.Text;
				Password = passwordTextBox.Text;
			}
		}

		private void okButton_Click(object sender, EventArgs e) {
			Server = serverTextBox.Text;
			Database = databaseTextBox.Text;
			IntegratedSecurity = integratedSecurityCheckBox.Checked;
			if (!IntegratedSecurity) {
				UserName = userTextBox.Text;
				Password = passwordTextBox.Text;
			}

			Close();
		}
	}
}
