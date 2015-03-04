using System;

namespace MediaConvertGUI
{
	public partial class TextWin : Gtk.Window
	{
		public TextWin () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}

		public string Text
		{
			set
			{
				textView.Buffer.Text = value;
			}
		}
	}
}

