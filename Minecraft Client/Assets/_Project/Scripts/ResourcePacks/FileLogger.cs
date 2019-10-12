// Created by Those45Ninjas.
using UnityEngine;
using System.IO;
using System;

public class FileLogger : ILogHandler
{
	public TextWriter Writer;
	public bool NeedsFlush = false;
	public readonly string LogFile;
	
	public FileLogger(string file)
	{
		// Create the Logs directory if needed.
		if(!Directory.Exists("Logs"))
			Directory.CreateDirectory("Logs");

		// Create or overwrite the log file.
		LogFile = Path.Combine("Logs", file + ".log");
		Writer = File.CreateText(LogFile);
	}

	// Log an exception by calling Log format with the exception as the message.
	public void LogException(Exception exception, UnityEngine.Object context)
	{
		LogFormat(LogType.Exception, context, exception.ToString());

		// Just in case the game crashes, flush on every exception that comes in.
		Writer.Flush();
	}

	public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
	{
		// Log the date, type and message.
		Writer.Write(DateTime.Now.ToString().PadRight(23));
		Writer.Write(logType.ToString().PadRight(11));
		Writer.WriteLine(format, args);

		// Update the needs flush flag.
		NeedsFlush = true;
	}

	// Call this every fixed update to keep the log file up to date without incomplete lines.
	public void Flush()
	{
		// Only flush if something was changed.
		if(!NeedsFlush)
			return;

		Writer.Flush();
		NeedsFlush = false;
	}

	// Close the file when this object is cleared.
	~FileLogger()
	{
		Writer.Close();
	}
}