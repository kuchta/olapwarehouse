using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OlapWarehouseApi;
using Infor.BI.Applications.OlapApi;

namespace OlapWarehouseWinClient {
	public partial class TreeForm : Form, IDisposable {

		public TreeForm() {
			InitializeComponent();
		}

		public static void Show(IDictionary<string, Element> dimension, bool modalWindow = true) {
			TreeForm instance = new TreeForm();

			instance.Text = dimension.ToString();

			CopyTree(instance.treeView.Nodes, dimension);

			if (modalWindow) {
				instance.ShowDialog();
			} else {
				instance.Show();
			}
		}

		public static void Show(OlapServer server) {
			TreeForm instance = new TreeForm();

			instance.Text = server.ToString();

			var serverNode = instance.treeView.Nodes.Add(server.Name);

			foreach (OlapCube cube in server.Cubes) {
				var cubeNode = serverNode.Nodes.Add(cube.Name);
				foreach (OlapDimension dimension in cube.Dimensions) {
					var dimensionNode = cubeNode.Nodes.Add(dimension.Name);
					foreach (OlapElement el in dimension.Elements) {
						var elementNode = dimensionNode.Nodes.Add(el.Name);
					}
				}
			}

			instance.Show();
		}


		public static void ShowDialog(IDictionary<string, Element> dimension) {
			Show(dimension, true);
		}

		private void TreeForm_FormClosing(object sender, FormClosingEventArgs e) {
			treeView.Nodes.Clear();
		}

		private static void CopyTree(TreeNodeCollection treeNodes, IDictionary<string, Element> node) {
			foreach (var pair in node) {
				var treeNode = treeNodes.Add(pair.Key, pair.Value.ToString());
				CopyTree(treeNode.Nodes, pair.Value);
			}
		}
	}
}