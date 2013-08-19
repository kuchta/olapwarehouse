using OlapWarehouseApi;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ImportWDI {
	public partial class TreeForm : Form, IDisposable {

		public TreeForm() {
			InitializeComponent();
		}

		public static void Show(Dimension dimension, bool modalWindow = true) {
			TreeForm instance = new TreeForm();

			instance.Text = dimension.Name;

			CreateTreeNode(instance.treeView.Nodes, dimension);

			foreach (var element in dimension.Elements) {
				
			}

			if (modalWindow) {
				instance.ShowDialog();
			} else {
				instance.Show();
			}
		}

		private static void CreateTreeNode(TreeNodeCollection nodes, IDictionary<string, Element> elements) {
			foreach (var element in elements) {
				var node = nodes.Add(element.Key);
				CreateTreeNode(node.Nodes, element.Value);
			}
		}

		public static void ShowDialog(Dimension dimension) {
			Show(dimension, true);
		}

		private void TreeForm_FormClosing(object sender, FormClosingEventArgs e) {
			treeView.Nodes.Clear();
			Console.WriteLine("TreeForm " + Text + " was closed");
		}
	}
}