using System;
using System.IO;
using System.Web.Mail;

namespace Program
{
	/// <summary>
	/// Simple Logger class
	///  Usage:
	///
	///			Logger logger = new Logger();
	///		
	///			logger.LogName = "name_of_log";
	///			logger.WriteToConsole = true;
	///			logger.NotificationList = "somone@someplace.com";
	///			logger.LogFile = "path to file"; 
	///			logger.SmtpServer = "smtp server";				
	///			logger.FromEmail = "noreply@somwhere.com";
	///			
	///			logger.StartLog();
	///			
	///			logger.LogEntry("somethign happened");
	///			
	///			logger.EmailLog();
	///
	///			logger.EndLog();
	///
	/// </summary>
	public class Logger : IDisposable
	{
		TextWriter _tw;

		private string _smtpServer = "localhost";
		private string _notificationList = "";
		private string _fromEmail = "someon@something.com";
		private string _logFile = "";
		private string _LogName = "Batch Program";
		private bool _writeToConsole = false;

		public bool WriteToConsole 
		{
			get { return _writeToConsole; } 
			set { _writeToConsole = value; }
		}

		public string LogFile 
		{
			get 
			{ 
				if(_logFile.Length == 0) 
				{
					_logFile = GetDateBasedLogFileName();
				} 
				return _logFile; 
			}
			set { _logFile = value; }
		}

		public string LogName 
		{
			get { return _LogName; }
			set { _LogName = value; }
		}

		public string SmtpServer 
		{
			get { return _smtpServer; }
			set { _smtpServer = value; }
		}
		
		public string NotificationList 
		{
			get { return _notificationList; }
			set { _notificationList = value; }
		}

		public string FromEmail 
		{
			get { return _fromEmail; }
			set { _fromEmail = value; }
		}


		public static string GetDateBasedLogFileName() 
		{
			string logName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".log";
			return logName;
		}
		
		/// <summary>
		/// Open's the text file and logs the start time
		/// </summary>
		public void StartLog() 
		{

			// create a writer and open the file
			_tw = new StreamWriter(LogFile);

			LogEntry("************************************");
			LogEntry(LogName + " Start Time:" + DateTime.Now);
			LogEntry("------------------------------------\n");
		}

		/// <summary>
		/// Writes end time and then closes the text file
		/// </summary>
		public void EndLog() 
		{
			try 
			{
				LogEntry(LogName + " End Time:" + DateTime.Now);
				LogEntry("************************************");
			} 
			finally 
			{
				if(_tw != null) 
				{
					_tw.Close();
					_tw = null;
				}
			}
		}

		/// <summary>
		/// Records the message to the log file and the console based upon values
		/// </summary>
		/// <param name="message"></param>
		public void LogEntry(string message) 
		{

			// write a line of text to the file
			if(_tw != null) 
			{
				_tw.WriteLine(DateTime.Now + " " + message);
				_tw.Flush();
			}
			if(WriteToConsole) 
			{
				Console.WriteLine(DateTime.Now + " " + message);
			}

		}	

		/// <summary>
		/// Emails the log file to the user
		/// </summary>
		public void EmailLog()
		{
			MailMessage mail =  new MailMessage();

			string subject = _LogName + " Log";
			string body = "See attachment.";
	

			mail.Subject = subject;
			mail.Body = body;

			mail.To = NotificationList;
			mail.From = FromEmail;
			SmtpMail.SmtpServer = SmtpServer;

			mail.Attachments.Add(new MailAttachment(LogFile));

			
			SmtpMail.Send(mail);
		}

		#region IDisposable Members

		public void Dispose()
		{
			if(_tw != null) 
			{
				_tw.Close();
			}
		}

		#endregion
	}
}
