
// This file has been generated by the GUI designer. Do not modify.
namespace MediaConvertGUI
{
	public partial class TextWin
	{
		private global::Gtk.ScrolledWindow scrolledwindow;
		private global::Gtk.TextView textView;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MediaConvertGUI.TextWin
			this.Name = "MediaConvertGUI.TextWin";
			this.Title = global::Mono.Unix.Catalog.GetString ("TextWin");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child MediaConvertGUI.TextWin.Gtk.Container+ContainerChild
			this.scrolledwindow = new global::Gtk.ScrolledWindow ();
			this.scrolledwindow.CanFocus = true;
			this.scrolledwindow.Name = "scrolledwindow";
			this.scrolledwindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child scrolledwindow.Gtk.Container+ContainerChild
			this.textView = new global::Gtk.TextView ();
			this.textView.CanFocus = true;
			this.textView.Name = "textView";
			this.scrolledwindow.Add (this.textView);
			this.Add (this.scrolledwindow);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 1024;
			this.DefaultHeight = 199;
			this.Show ();
		}
	}
}
