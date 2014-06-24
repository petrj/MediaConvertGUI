
// This file has been generated by the GUI designer. Do not modify.
namespace MediaConvertGUI
{
	public partial class WidgetGeneralMediaInfo
	{
		private global::Gtk.Fixed @fixed;
		private global::Gtk.Label labelBitRateInfo;
		private global::Gtk.Label labelBitRate;
		private global::Gtk.Label labelDurationInfo;
		private global::Gtk.Label labelDuration;
		private global::Gtk.Label labelScheme;
		private global::Gtk.ComboBox comboScheme;
		private global::Gtk.Label labelSizeInfo;
		private global::Gtk.Label labelSize;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MediaConvertGUI.WidgetGeneralMediaInfo
			global::Stetic.BinContainer.Attach (this);
			this.WidthRequest = 350;
			this.Name = "MediaConvertGUI.WidgetGeneralMediaInfo";
			// Container child MediaConvertGUI.WidgetGeneralMediaInfo.Gtk.Container+ContainerChild
			this.@fixed = new global::Gtk.Fixed ();
			this.@fixed.Name = "fixed";
			this.@fixed.HasWindow = false;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelBitRateInfo = new global::Gtk.Label ();
			this.labelBitRateInfo.Name = "labelBitRateInfo";
			this.labelBitRateInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("Overall bitrate:");
			this.@fixed.Add (this.labelBitRateInfo);
			global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelBitRateInfo]));
			w1.X = 5;
			w1.Y = 5;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelBitRate = new global::Gtk.Label ();
			this.labelBitRate.Name = "labelBitRate";
			this.labelBitRate.LabelProp = global::Mono.Unix.Catalog.GetString ("0 kpbs");
			this.@fixed.Add (this.labelBitRate);
			global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelBitRate]));
			w2.X = 110;
			w2.Y = 5;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelDurationInfo = new global::Gtk.Label ();
			this.labelDurationInfo.Name = "labelDurationInfo";
			this.labelDurationInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("Duration:");
			this.@fixed.Add (this.labelDurationInfo);
			global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelDurationInfo]));
			w3.X = 5;
			w3.Y = 30;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelDuration = new global::Gtk.Label ();
			this.labelDuration.Name = "labelDuration";
			this.labelDuration.LabelProp = global::Mono.Unix.Catalog.GetString ("00:00:00");
			this.@fixed.Add (this.labelDuration);
			global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelDuration]));
			w4.X = 110;
			w4.Y = 30;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelScheme = new global::Gtk.Label ();
			this.labelScheme.Name = "labelScheme";
			this.labelScheme.LabelProp = global::Mono.Unix.Catalog.GetString ("Scheme");
			this.@fixed.Add (this.labelScheme);
			global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelScheme]));
			w5.X = 5;
			w5.Y = 85;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.comboScheme = global::Gtk.ComboBox.NewText ();
			this.comboScheme.Name = "comboScheme";
			this.@fixed.Add (this.comboScheme);
			global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.comboScheme]));
			w6.X = 110;
			w6.Y = 77;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelSizeInfo = new global::Gtk.Label ();
			this.labelSizeInfo.Name = "labelSizeInfo";
			this.labelSizeInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("Size:");
			this.@fixed.Add (this.labelSizeInfo);
			global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelSizeInfo]));
			w7.X = 5;
			w7.Y = 55;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelSize = new global::Gtk.Label ();
			this.labelSize.Name = "labelSize";
			this.labelSize.LabelProp = global::Mono.Unix.Catalog.GetString ("0 MB");
			this.@fixed.Add (this.labelSize);
			global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelSize]));
			w8.X = 110;
			w8.Y = 55;
			this.Add (this.@fixed);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}
