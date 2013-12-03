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
		#region filling Combo
			
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

		#endregion 

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

		public static string ExecuteAndReturnOutput(string command,string arguments)
		{
			return (String.Join(String.Empty,ExecuteAndReturnOutputAsList(command,arguments)));
		}

		public static List<string> ExecuteAndReturnOutputAsList(string command,string arguments)
		{
			var lines = new List<string>();
			var info = new System.Diagnostics.ProcessStartInfo(command,arguments);
				info.UseShellExecute = false; 
				info.ErrorDialog = true; 
				info.CreateNoWindow = true; 
				info.RedirectStandardOutput = true; 
			
			 	Process p = System.Diagnostics.Process.Start(info); 
				p.WaitForExit();
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
	}
}

