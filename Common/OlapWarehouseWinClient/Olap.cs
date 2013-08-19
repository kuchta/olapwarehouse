using Microsoft.AnalysisServices;
using System;
using System.Text;


namespace ImportWDI {
	class Olap {
		public static void ServerConnect(IntPtr handle) {

			StringBuilder result = new System.Text.StringBuilder();

			Server server = new Server();
			server.Connect("Data Source=http://localhost:8210;UserName=admin");

			////Connect to the local server
			//using (AdomdConnection conn = new AdomdConnection("Provider=MSOLAP.4;Persist Security Info=True;User ID=admin;Password=;Data Source=localhost:8210;Initial Catalog=WDI")) {
			//	conn.Open();
			//	var cubes = conn.Cubes;
			//	foreach (var cube in cubes) {
			//		Console.WriteLine("cube type: " + cube.GetType());
			//	}

			//}

			//dynamic ret;
			//var servers = new Servers();
			//ret = servers.ServerConnect("", "", "");
			////ret = servers.ServersCount();
			//Console.WriteLine("ret: " + ret);
			//MdsInit(handle);

			//CMDSClient client = new CMDSClient();
			//CServer server = client.ConnectToServer("localhost", "admin", "");
			//foreach (string name in server.Hypercubes.ItemNames) {
			//	Console.WriteLine("name: {0}", name);
		}
	}
}
