namespace Dtx.LoggerProxy
{
	public static class Utility
	{
		static Utility()
		{
		}

		private static string GetErrorMessage(System.Exception exception)
		{
			System.Text.StringBuilder result = new System.Text.StringBuilder();

			System.Exception currentException = exception;

			while (currentException != null)
			{
				if (result.Length != 0)
				{
					result.Append(" - ");
				}

				result.Append(currentException.Message);

				currentException = currentException.InnerException;
			}

			return (result.ToString());
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
			System.Text.StringBuilder result = new System.Text.StringBuilder();

			if ((System.Web.HttpContext.Current != null) &&
				(System.Web.HttpContext.Current.Request != null))
			{
				string userHostAddress =
					System.Web.HttpContext.Current.Request.UserHostAddress;

				if (string.IsNullOrWhiteSpace(userHostAddress) == false)
				{
					result.Append(string.Format("<ip>{0}</ip>", userHostAddress));
				}

				string absoluteUri =
					System.Web.HttpContext.Current.Request.Url.AbsoluteUri;

				if (string.IsNullOrWhiteSpace(absoluteUri) == false)
				{
					result.Append(string.Format("<absoluteUri>{0}</absoluteUri>", absoluteUri));
				}

				string httpReferer =
					System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];

				if (string.IsNullOrWhiteSpace(httpReferer) == false)
				{
					result.Append(string.Format("<httpReferer>{0}</httpReferer>", httpReferer));
				}
			}

			if (exception == null)
			{
				result.Append(string.Format("<message>{0}</message>", message));
			}
			else
			{
				result.Append(string.Format("<errorMessages>{0}</errorMessages>", GetErrorMessage(exception)));
			}

			if ((parameters != null) && (parameters.Count != 0))
			{
				result.Append("<parameters>");

				foreach (System.Collections.DictionaryEntry currentEntry in parameters)
				{
					if (currentEntry.Key != null)
					{
						result.Append("<parameter>");

						result.Append("<key>");
						result.Append(currentEntry.Key);
						result.Append("</key>");

						result.Append("<value>");
						if (currentEntry.Value == null)
						{
							result.Append("NULL");
						}
						else
						{
							result.Append(currentEntry.Value);
						}
						result.Append("</value>");

						result.Append("</parameter>");
					}
				}

				result.Append("</parameters>");
			}

			NLog.Logger logger =
				NLog.LogManager.GetLogger(name: type.ToString());

			switch (level)
			{
				case LogLevels.Trace:
				{
					logger.Trace(exception, message: result.ToString());

					break;
				}

				case LogLevels.Debug:
				{
					logger.Debug(exception, message: result.ToString());

					break;
				}

				case LogLevels.Info:
				{
					logger.Info(exception, message: result.ToString());

					break;
				}

				case LogLevels.Warn:
				{
					logger.Warn(exception, message: result.ToString());

					break;
				}

				case LogLevels.Error:
				{
					logger.Error(exception, message: result.ToString());

					break;
				}

				case LogLevels.Fatal:
				{
					logger.Fatal(exception, message: result.ToString());

					break;
				}
			}
		}
	}
}
