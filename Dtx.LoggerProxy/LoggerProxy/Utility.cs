namespace Dtx.LoggerProxy
{
	public static class Utility
	{
		static Utility()
		{
		}

		private static string GetErrorMessage(System.Exception exception)
		{
			System.Text.StringBuilder oResult = new System.Text.StringBuilder();

			System.Exception oException = exception;

			while (oException != null)
			{
				if (oResult.Length != 0)
				{
					oResult.Append(" - ");
				}

				oResult.Append(oException.Message);

				oException = oException.InnerException;
			}

			return (oResult.ToString());
		}

		public static void Log
			(LogLevels level, System.Type type, string message, System.Collections.Hashtable parameters = null)
		{
			Log(level: level, type: type, message: message, exception: null, parameters: parameters);
		}

		public static void Log
			(LogLevels level, System.Type type, System.Exception exception, System.Collections.Hashtable parameters = null)
		{
			Log(level: level, type: type, message: string.Empty, exception: exception, parameters: parameters);
		}

		private static void Log
			(LogLevels level, System.Type type, string message, System.Exception exception = null, System.Collections.Hashtable parameters = null)
		{
			System.Text.StringBuilder oResult = new System.Text.StringBuilder();

			if ((System.Web.HttpContext.Current != null) &&
				(System.Web.HttpContext.Current.Request != null))
			{
				string strUserHostAddress =
					System.Web.HttpContext.Current.Request.UserHostAddress;

				if (string.IsNullOrEmpty(strUserHostAddress) == false)
				{
					oResult.Append(string.Format("<ip>{0}</ip>", strUserHostAddress));
				}

				string strAbsoluteUri =
					System.Web.HttpContext.Current.Request.Url.AbsoluteUri;

				if (string.IsNullOrEmpty(strAbsoluteUri) == false)
				{
					oResult.Append(string.Format("<absoluteUri>{0}</absoluteUri>", strAbsoluteUri));
				}

				string strHttpReferer =
					System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];

				if (string.IsNullOrEmpty(strHttpReferer) == false)
				{
					oResult.Append(string.Format("<httpReferer>{0}</httpReferer>", strHttpReferer));
				}
			}

			if (exception == null)
			{
				oResult.Append(string.Format("<message>{0}</message>", message));
			}
			else
			{
				oResult.Append(string.Format("<errorMessages>{0}</errorMessages>", GetErrorMessage(exception)));
			}

			if ((parameters != null) && (parameters.Count != 0))
			{
				oResult.Append("<parameters>");

				foreach (System.Collections.DictionaryEntry oEntry in parameters)
				{
					if (oEntry.Key != null)
					{
						oResult.Append("<parameter>");

						oResult.Append("<key>");
						oResult.Append(oEntry.Key);
						oResult.Append("</key>");

						oResult.Append("<value>");
						if (oEntry.Value == null)
						{
							oResult.Append("NULL");
						}
						else
						{
							oResult.Append(oEntry.Value);
						}
						oResult.Append("</value>");

						oResult.Append("</parameter>");
					}
				}

				oResult.Append("</parameters>");
			}

			NLog.Logger oLogger =
				NLog.LogManager.GetLogger(type.ToString());

			switch (level)
			{
				case LogLevels.Trace:
				{
					oLogger.Trace(exception: exception, message: oResult.ToString());

					break;
				}

				case LogLevels.Debug:
				{
					oLogger.Debug(exception: exception, message: oResult.ToString());

					break;
				}

				case LogLevels.Info:
				{
					oLogger.Info(exception: exception, message: oResult.ToString());

					break;
				}

				case LogLevels.Warn:
				{
					oLogger.Warn(exception: exception, message: oResult.ToString());

					break;
				}

				case LogLevels.Error:
				{
					oLogger.Error(exception: exception, message: oResult.ToString());

					break;
				}

				case LogLevels.Fatal:
				{
					oLogger.Fatal(exception: exception, message: oResult.ToString());

					break;
				}
			}
		}
	}
}
