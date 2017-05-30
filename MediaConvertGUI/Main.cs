using System;
using System.Collections.Generic;
using Gtk;
using System.IO;

namespace MediaConvertGUI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try
			{
				Application.Init ();

				var filesOrDirectoriesToAdd = new List<string>();
				string configFileName = "config.xml";
				 
				if (args.Length>0)
				{
					var nextParamIsConfig = false;
					foreach (var arg in args)
					{
						if (nextParamIsConfig)
						{
							configFileName = arg;
							nextParamIsConfig = false;
							continue;
						} else
						if (
								(arg.ToLower() == "-c")  ||
								(arg.ToLower() == "-config")  ||
								(arg.ToLower() == "--config") 
							)
						{
							nextParamIsConfig = true;
							continue;
						}

						if (Directory.Exists(arg))
						{
							foreach (var fName in Directory.GetFiles(arg)) 
							{
								filesOrDirectoriesToAdd.Add(fName);
							}
						} else
						if (File.Exists(arg))
						{
							filesOrDirectoriesToAdd.Add(arg);
						};
					}
				}

				MainWindow win = new MainWindow (configFileName);
				foreach (var fName in filesOrDirectoriesToAdd) 
				{
					win.AddMediaInfo(fName);
				}

				win.Show ();
				Application.Run ();

			} catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				throw;
			}
		}
	}
}
