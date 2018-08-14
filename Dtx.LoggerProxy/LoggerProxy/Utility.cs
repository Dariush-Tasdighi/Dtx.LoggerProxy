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
			(LogLevel level, System.Type type, string message, System.Collections.Hashtable parameters = null)
		{
			Log(level: level, type: type, message: message, exception: null, parameters: parameters);
		}

		public static void Log
			(LogLevel level, System.Type type, System.Exception exception, System.Collections.Hashtable parameters = null)
		{
			Log(level: level, type: type, message: string.Empty, exception: exception, parameters: parameters);
		}

		private static void Log
			(LogLevel level, System.Type type, string message, System.Exception exception = null, System.Collections.Hashtable parameters = null)
		{
			System.Text.StringBuilder result = new System.Text.StringBuilder();

			if ((System.Web.HttpContext.Current != null) &&
				(System.Web.HttpContext.Current.Request != null))
			{
				string userHostAddress =
					System.Web.HttpContext.Current.Request.UserHostAddress;

				if (string.IsNullOrWhiteSpace(userHostAddress) == false)
				{
					result.Append($"<ip>{ userHostAddress }</ip>");
				}

				System.Uri Uri =
					System.Web.HttpContext.Current.Request.Url;

				if (Uri != null)
				{
					string absoluteUri = Uri.AbsoluteUri;

					if (string.IsNullOrWhiteSpace(absoluteUri) == false)
					{
						result.Append($"<absoluteUri>{ absoluteUri }</absoluteUri>");
					}
				}

				System.Uri uriReferrer =
					System.Web.HttpContext.Current.Request.UrlReferrer;

				if (uriReferrer != null)
				{
					string httpReferrer = uriReferrer.AbsolutePath;

					if (string.IsNullOrWhiteSpace(httpReferrer) == false)
					{
						result.Append($"<httpReferrer>{ httpReferrer }</httpReferrer>");
					}
				}
			}

			if (string.IsNullOrWhiteSpace(message) == false)
			{
				result.Append($"<message>{ message }</message>");
			}

			if (exception != null)
			{
				result.Append($"<errorMessages>{ GetErrorMessage(exception) }</errorMessages>");
			}

			if ((parameters != null) && (parameters.Count != 0))
			{
				result.Append("<parameters>");

				foreach (System.Collections.DictionaryEntry currentEntry in parameters)
				{
					if (currentEntry.Key != null)
					{
						result.Append("<parameter>");

						result.Append($"<key>{ currentEntry.Key }</key>");

						if (currentEntry.Value == null)
						{
							result.Append($"<value>NULL</value>");
						}
						else
						{
							result.Append($"<value>{ currentEntry.Value }</value>");
						}

						result.Append("</parameter>");
					}
				}

				result.Append("</parameters>");
			}

			NLog.Logger logger =
				NLog.LogManager.GetLogger(name: type.ToString());

			switch (level)
			{
				case LogLevel.Trace:
				{
					logger.Trace(exception, message: result.ToString());

					break;
				}

				case LogLevel.Debug:
				{
					logger.Debug(exception, message: result.ToString());

					break;
				}

				case LogLevel.Info:
				{
					logger.Info(exception, message: result.ToString());

					break;
				}

				case LogLevel.Warn:
				{
					logger.Warn(exception, message: result.ToString());

					break;
				}

				case LogLevel.Error:
				{
					logger.Error(exception, message: result.ToString());

					break;
				}

				case LogLevel.Fatal:
				{
					logger.Fatal(exception, message: result.ToString());

					break;
				}
			}
		}
	}
}
