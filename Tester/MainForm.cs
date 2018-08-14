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
			// **************************************************
			Dtx.LoggerProxy.Utility.Log
				(Dtx.LoggerProxy.LogLevel.Trace, GetType(), "Trace...");

			Dtx.LoggerProxy.Utility.Log
				(Dtx.LoggerProxy.LogLevel.Debug, GetType(), "Debug...");

			Dtx.LoggerProxy.Utility.Log
				(Dtx.LoggerProxy.LogLevel.Info, GetType(), "Info...");

			Dtx.LoggerProxy.Utility.Log
				(Dtx.LoggerProxy.LogLevel.Warn, GetType(), "Warn...");

			Dtx.LoggerProxy.Utility.Log
				(Dtx.LoggerProxy.LogLevel.Error, GetType(), "Error...");

			Dtx.LoggerProxy.Utility.Log
				(Dtx.LoggerProxy.LogLevel.Fatal, GetType(), "Fatal...");
			// **************************************************

			// **************************************************
			try
			{
				// ...
			}
			catch (System.Exception ex)
			{
				Dtx.LoggerProxy.Utility.Log(Dtx.LoggerProxy.LogLevel.Error, typeof(MainForm), exception: ex);
			}
			// **************************************************

			System.Exception exception;

			// **************************************************
			exception = new System.Exception(message: "Error Message (1)!");
			Dtx.LoggerProxy.Utility.Log(Dtx.LoggerProxy.LogLevel.Error, GetType(), exception: exception);
			// **************************************************

			// **************************************************
			System.Exception innerException = new System.Exception(message: "Error Message (2)!");
			exception = new System.Exception(message: "Error Message (1)!", innerException: innerException);

			Dtx.LoggerProxy.Utility.Log(Dtx.LoggerProxy.LogLevel.Error, GetType(), exception: exception);
			// **************************************************

			// **************************************************
			System.Collections.Hashtable keyList = new System.Collections.Hashtable();

			//list.Add(null, null);
			//list.Add(null, "Value1");
			keyList.Add("Key1", null);
			keyList.Add("Key2", "Value2");
			keyList.Add("Key3", "Value3");

			Dtx.LoggerProxy.Utility.Log(Dtx.LoggerProxy.LogLevel.Error, GetType(), exception: exception, parameters: keyList);
			// **************************************************
		}
	}
}
