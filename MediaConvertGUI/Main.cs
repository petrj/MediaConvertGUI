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
				bool forceQuit = false;
				 
				if (args.Length>0)
				{
					var nextParamIsConfig = false;
					foreach (var arg in args)
					{
						if (
							(arg.ToLower() == "-h")  ||
							(arg.ToLower() == "-help")  ||
							(arg.ToLower() == "--help") 
							)
						{
							ShowHelp();
							forceQuit = true;
							break;
						}

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

				if (!forceQuit)
				{
					MainWindow win = new MainWindow (configFileName);
					foreach (var fName in filesOrDirectoriesToAdd) 
					{
						win.AddMediaInfo(fName);
					}

					win.Show ();
					Application.Run ();
				}

			} catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				throw;
			}
		}

		public static void ShowHelp()
		{
			Console.WriteLine ("MediaConvertGUI");
			Console.WriteLine ("ffmpeg frontend");
			Console.WriteLine ("");
			Console.WriteLine ("usage: ");
			Console.WriteLine ("");
			Console.WriteLine("MediaConvertGUI.exe [-config file.xml] [movieOrVideoOrFolder]");	
			Console.WriteLine("");
			Console.WriteLine ("examples: ");
			Console.WriteLine("");
			Console.WriteLine("MediaConvertGUI.exe movie.mpg");
			Console.WriteLine("MediaConvertGUI.exe audio.mp3");
			Console.WriteLine ("MediaConvertGUI.exe /mnt/movies/");
			Console.WriteLine ("MediaConvertGUI.exe -config alternativeConfig.xml");
			Console.WriteLine ("MediaConvertGUI.exe -c alternativeConfig.xml movie.mpg");
		}
	}
}
