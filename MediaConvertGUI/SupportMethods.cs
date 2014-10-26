using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Gtk;
using System.Diagnostics;


namespace MediaConvertGUI
{
	public static class SupportMethods
	{
		#region OS

		public enum RunningPlatformEnum
		{
			Unix = 0,
			Windows = 1
		}

		public static RunningPlatformEnum RunningPlatform
		{
			get
			{
				if (Environment.OSVersion.Platform.ToString() == "Unix")
				{
					return RunningPlatformEnum.Unix;
				}
				else
				{
					return RunningPlatformEnum.Windows;
				}
			}
		}

		#endregion

		#region path

		public static string AppPath
		{
			get
			{
				return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			}
		}

		#endregion

		#region GTK 

		public static void ClearCombo(Gtk.ComboBox combo)
		{
			// clear combo
			combo.Model = new ListStore(typeof(string));
			combo.Active = -1;

			if (combo is ComboBoxEntry) 
			{
				(combo as ComboBoxEntry).Entry.Text = String.Empty;
			}
		}
			
		/// <summary>
		/// Fills the combo box.
		/// </summary>
		/// <param name='combo'>
		/// Combo.
		/// </param>
		/// <param name='items'>
		/// Items.
		/// </param>
		/// <param name='editable'>
		/// Editable.
		/// </param>
		/// <param name='currentValue'>
		/// Current value.
		/// </param>
		public static void FillComboBox(Gtk.ComboBox combo, List<string> items, bool editable, string currentValue)
		{
			// clear combo
			combo.Model = new ListStore(typeof(string));

			// adding all items
			var index=0;
			if (editable)
			{
				foreach (var item in items)
				{					
					combo.AppendText(item);
					if (item == currentValue)
					combo.Active = index;

					index++;
				}
			}
			else				
			{
				combo.AppendText(currentValue);
				combo.Active = 0;
			}
		}

		/// <summary>
		/// Fills the combo box.
		/// </summary>
		/// <param name='combo'>
		/// Combo.
		/// </param>
		/// <param name='items'>
		/// Items.
		/// </param>
		/// <param name='editable'>
		/// Editable.
		/// </param>
		/// <param name='currentValue'>
		/// Current value.
		/// </param>
		public static void FillComboBox(Gtk.ComboBox combo, Type enumType, bool editable, int currentValue)
		{
			// clear combo
			combo.Model = new ListStore(typeof(string));

			// adding all items
			var index=0;
				foreach (var item in Enum.GetNames(enumType))
				{	
					if (editable || (index == currentValue))
					{
						combo.AppendText(item);

						if (!editable) combo.Active = 0; 
						else
						if (index == currentValue) combo.Active = index;
						
					}
					index++;

				}
		}

		/// <summary>
		/// Fills the combo box entry.
		/// </summary>
		/// <param name='combo'>
		/// Combo.
		/// </param>
		/// <param name='items'>
		/// Items.
		/// </param>
		/// <param name='currentValue'>
		/// Current value.
		/// </param>
		/// <param name='editable'>
		/// Editable.
		/// </param>
		public static void FillComboBoxEntry(Gtk.ComboBoxEntry combo, List<string> items, string currentValue,bool isDecimal,bool editable)
		{
			combo.Model = new ListStore(typeof(string));

			var resultItems = new List<string>();
			if (editable)				
			{	
					// searching for value					
					var present = false;
					foreach (var value in items)
					{
						if (
								(isDecimal && (SupportMethods.ToDecimal(value) == SupportMethods.ToDecimal(currentValue))) 
								||
							   	(!isDecimal && (value == currentValue))
						   )
						{
							present = true;
							break;
						}
					}

				// adding missing to first pos
				if (!present)
				{
					resultItems.Add(currentValue);
				}			
				
				foreach (var value in items)				
					resultItems.Add(value);
			} else
			{
					resultItems.Add(currentValue);
			}
				
			var index = 0;
			foreach (var value in resultItems)
			{
				combo.AppendText(value);

				if (
								(isDecimal && (SupportMethods.ToDecimal(value) == SupportMethods.ToDecimal(currentValue))) 
								||
							   	(!isDecimal && (value == currentValue))
					)
				{
					combo.Active = index;
				}
				index++;
			}	

		}

		public static void FillComboBoxEntry(Gtk.ComboBoxEntry combo, Dictionary<decimal,string> items, decimal currentValue,bool editable)
		{
			combo.Model = new ListStore(typeof(string));

			var resultItems = new Dictionary<decimal,string>();

			if (editable)				
			{	
				// adding missing to first pos
				if (!items.ContainsKey(currentValue))
				{
					resultItems.Add(currentValue,currentValue.ToString());
				}			
				
				foreach (var kvp in items)				
					resultItems.Add(kvp.Key,kvp.Value);
			} else
			{
					resultItems.Add(currentValue,currentValue.ToString());
			}
				
			var index = 0;
			foreach (var kvp in resultItems)
			{
				combo.AppendText(kvp.Value);

				if (kvp.Key == currentValue)
				{
					combo.Active = index;
				}
				index++;
			}			
				
		}

		public static void SetAvailability(Gtk.Widget widget, bool editable = false)
		{
			var entryBackgroundColor = editable ? new Gdk.Color(255,255,255) : new Gdk.Color(214,210,208);
			if (widget is Gtk.Entry)
			{
				(widget as Gtk.Entry).IsEditable = editable;
				(widget as Gtk.Entry).ModifyBase(StateType.Normal, entryBackgroundColor);
			}

			if (widget is Gtk.ComboBoxEntry)
			{
				(widget as Gtk.ComboBoxEntry).Entry.IsEditable = editable;
				(widget as Gtk.ComboBoxEntry).Entry.ModifyBase(StateType.Normal, entryBackgroundColor);
			}
		}

		#endregion 

		#region numeric/decimal parse

		public static bool IsNumeric(this string s)
	    {
	        decimal output;
			var sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
			s = s.Replace(".",sep).Replace(",",sep);
	        return decimal.TryParse(s, out output);
	    }

		public static decimal ToDecimal(this string s)
	    {
	        decimal output;
			var sep = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
			s = s.Replace(".",sep).Replace(",",sep);
			return decimal.Parse(s);
	    }

		public static bool IsInt(this string s)
	    {
	        int output;
	        return Int32.TryParse(s, out output);
	    }

		public static decimal ParseDecimalValueFromValue(string value, Dictionary<decimal,string> dict)
		{
				var res = 0m;

				if (SupportMethods.IsNumeric( value))
				{
					res = SupportMethods.ToDecimal(value);
				} else
				{
					foreach (var  kvp in dict)
					{
						if (kvp.Value == value)
						{
							res = kvp.Key;
						}
					}
				}
			
				return res;
		}

		#endregion 

		#region execution

		public static void ExecuteInShell(string command)
		{
			/*
			ProcessStartInfo psi = new ProcessStartInfo(command);
	        psi.UseShellExecute = true;
	        Process.Start(psi);
	        */

			Process.Start(command);
		}

		public static void Execute(string command,string arguments)
		{
			ExecuteAndReturnOutputAsList(command,arguments);
		}

		public static string ExecuteAndReturnOutput(string command,string arguments)
		{
			return (String.Join(String.Empty,ExecuteAndReturnOutputAsList(command,arguments)));
		}

		public static List<string> ExecuteAndReturnOutputAsList(string command,string arguments)
		{
			var lines = new List<string>();
			var info = new System.Diagnostics.ProcessStartInfo (command, arguments) 
			{
				WindowStyle = ProcessWindowStyle.Hidden,
				UseShellExecute = false,
				ErrorDialog = false,
				CreateNoWindow = true,
				RedirectStandardOutput = true
			};

			var p = new Process ();
			p.StartInfo = info;
			p.Start ();

			//p.WaitForExit();
				using (var /*  System.IO.StreamReader*/  sReader = p.StandardOutput)
				{		

					while (!sReader.EndOfStream)
					{
						lines.Add(sReader.ReadLine());
					}    			
					sReader.Close(); 
				}

			return lines;			
		}

		#endregion 

		#region human-readable strings conversion

		/// <summary>
		/// Returns size (long) as string like "2 GB"
		/// </summary>
		public static string HumanReadableSize(long sizeInBytes)
		{
				var res = "0 MB";

				var sizeMB = sizeInBytes/(1000.00*1000.00);
				if (sizeMB>1000)
				{
					var sizeGB = sizeMB/(1000.00);
					sizeGB = Math.Round(sizeGB,1);
					res = sizeGB.ToString()+" GB";
				} else
				{
					sizeMB = Math.Round(sizeMB,1);
					res = sizeMB.ToString()+" MB";
				}

				return res;
		}

		/// <summary>
		/// Returns duration (decimal) in miliseconds as string like "hh:mm:ss"
		/// </summary>
		public static string HuamReadableDuration(decimal miliSeconds)
		{
				var res = "00:00:00";

				var totalSeconds = Math.Round(miliSeconds/(decimal)1000);
				var hours = Math.Truncate(totalSeconds/(decimal)(60*60));
				var minutes = Math.Truncate( (totalSeconds - hours*60*60)/60);
				var seconds = Math.Truncate( (totalSeconds - hours*60*60 -minutes*60 ));

				res = minutes.ToString().PadLeft(2,'0') + ":"+ minutes.ToString().PadLeft(2,'0') + ":" + seconds.ToString().PadLeft(2,'0');

				return res;
		}

		#endregion 
	}

	public class StringEventArgs : EventArgs
	{
		private string stringData;

		public StringEventArgs(string _data)
		{
			stringData = _data;
		}

		public string StringData {get{return stringData; } }
	}
}

