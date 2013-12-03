using System;
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
				MainWindow win = new MainWindow ();

				if (args.Length>0)
				{
					foreach (var arg in args)
					{
						if (File.Exists(arg))
						{
							win.AddMediaInfo(arg);
						}
					}
				}

				win.Show ();
				Application.Run ();

			} catch (Exception ex)
			{
				throw;
			}
		}
	}
}
