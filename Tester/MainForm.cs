namespace MyApplication
{
	public partial class MainForm : System.Windows.Forms.Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			//Dtx.LoggerProxy.Utility.Log
			//	(Dtx.LoggerProxy.LogLevels.Trace, GetType(), "Trace...");

			//Dtx.LoggerProxy.Utility.Log
			//	(Dtx.LoggerProxy.LogLevels.Debug, GetType(), "Debug...");

			//Dtx.LoggerProxy.Utility.Log
			//	(Dtx.LoggerProxy.LogLevels.Info, GetType(), "Info...");

			//Dtx.LoggerProxy.Utility.Log
			//	(Dtx.LoggerProxy.LogLevels.Warn, GetType(), "Warn...");

			Dtx.LoggerProxy.Utility.Log
				(Dtx.LoggerProxy.LogLevels.Error, GetType(), "Error...");

			//Dtx.LoggerProxy.Utility.Log
			//	(Dtx.LoggerProxy.LogLevels.Fatal, GetType(), "Fatal...");

			System.Exception ex1;

			ex1 = new System.Exception(message: "Error Message (1)!");

			Dtx.LoggerProxy.Utility.Log(Dtx.LoggerProxy.LogLevels.Error, GetType(), exception: ex1);

			System.Exception ex2 = new System.Exception(message: "Error Message (2)!");
			ex1 = new System.Exception(message: "Error Message (1)!", innerException: ex2);

			Dtx.LoggerProxy.Utility.Log(Dtx.LoggerProxy.LogLevels.Error, GetType(), exception: ex1);

			System.Collections.Hashtable oList = new System.Collections.Hashtable();

			//oList.Add(null, null);
			//oList.Add(null, "Value1");
			oList.Add("Key1", null);
			oList.Add("Key2", "Value2");
			oList.Add("Key3", "Value3");

			Dtx.LoggerProxy.Utility.Log(Dtx.LoggerProxy.LogLevels.Error, GetType(), exception: ex1, parameters: oList);
		}
	}
}
