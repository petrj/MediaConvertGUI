
// This file has been generated by the GUI designer. Do not modify.
namespace MediaConvertGUI
{
	public partial class WidgetAudioTracks
	{
		private global::Gtk.Fixed @fixed;
		private global::Gtk.Label labelTrackInfo;
		private global::Gtk.ComboBox comboTracks;
		private global::Gtk.Label labelCodecInfo;
		private global::Gtk.ComboBox comboCodec;
		private global::Gtk.Frame frameAudioOptions;
		private global::Gtk.Alignment GtkAlignment;
		private global::Gtk.Fixed fixedAudioOptions;
		private global::Gtk.Label labelSampleRateInfo;
		private global::Gtk.Label labelTrackSizeInfo;
		private global::Gtk.ComboBox comboChannels;
		private global::Gtk.Label labelTrackSze;
		private global::Gtk.Label labelChannels;
		private global::Gtk.Label labelBitrate;
		private global::Gtk.Label labelKbps;
		private global::Gtk.ComboBoxEntry comboBitrate;
		private global::Gtk.Label labelKhz;
		private global::Gtk.ComboBoxEntry comboSampleRate;
		private global::Gtk.Label GtkLabel;
		private global::Gtk.EventBox eventBox;
		private global::Gtk.Image image;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MediaConvertGUI.WidgetAudioTracks
			global::Stetic.BinContainer.Attach (this);
			this.Name = "MediaConvertGUI.WidgetAudioTracks";
			// Container child MediaConvertGUI.WidgetAudioTracks.Gtk.Container+ContainerChild
			this.@fixed = new global::Gtk.Fixed ();
			this.@fixed.Name = "fixed";
			this.@fixed.HasWindow = false;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelTrackInfo = new global::Gtk.Label ();
			this.labelTrackInfo.Name = "labelTrackInfo";
			this.labelTrackInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("Track");
			this.@fixed.Add (this.labelTrackInfo);
			global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelTrackInfo]));
			w1.X = 5;
			w1.Y = 15;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.comboTracks = global::Gtk.ComboBox.NewText ();
			this.comboTracks.Name = "comboTracks";
			this.@fixed.Add (this.comboTracks);
			global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.comboTracks]));
			w2.X = 130;
			w2.Y = 10;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelCodecInfo = new global::Gtk.Label ();
			this.labelCodecInfo.Name = "labelCodecInfo";
			this.labelCodecInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("Codec");
			this.@fixed.Add (this.labelCodecInfo);
			global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelCodecInfo]));
			w3.X = 5;
			w3.Y = 45;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.comboCodec = global::Gtk.ComboBox.NewText ();
			this.comboCodec.Name = "comboCodec";
			this.@fixed.Add (this.comboCodec);
			global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.comboCodec]));
			w4.X = 130;
			w4.Y = 40;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.frameAudioOptions = new global::Gtk.Frame ();
			this.frameAudioOptions.Name = "frameAudioOptions";
			this.frameAudioOptions.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frameAudioOptions.Gtk.Container+ContainerChild
			this.GtkAlignment = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment.Name = "GtkAlignment";
			this.GtkAlignment.LeftPadding = ((uint)(12));
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			this.fixedAudioOptions = new global::Gtk.Fixed ();
			this.fixedAudioOptions.Name = "fixedAudioOptions";
			this.fixedAudioOptions.HasWindow = false;
			// Container child fixedAudioOptions.Gtk.Fixed+FixedChild
			this.labelSampleRateInfo = new global::Gtk.Label ();
			this.labelSampleRateInfo.Name = "labelSampleRateInfo";
			this.labelSampleRateInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("Sample rate");
			this.fixedAudioOptions.Add (this.labelSampleRateInfo);
			global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.fixedAudioOptions [this.labelSampleRateInfo]));
			w5.Y = 65;
			// Container child fixedAudioOptions.Gtk.Fixed+FixedChild
			this.labelTrackSizeInfo = new global::Gtk.Label ();
			this.labelTrackSizeInfo.Name = "labelTrackSizeInfo";
			this.labelTrackSizeInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("Track size");
			this.fixedAudioOptions.Add (this.labelTrackSizeInfo);
			global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.fixedAudioOptions [this.labelTrackSizeInfo]));
			w6.Y = 95;
			// Container child fixedAudioOptions.Gtk.Fixed+FixedChild
			this.comboChannels = global::Gtk.ComboBox.NewText ();
			this.comboChannels.Name = "comboChannels";
			this.fixedAudioOptions.Add (this.comboChannels);
			global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.fixedAudioOptions [this.comboChannels]));
			w7.X = 110;
			// Container child fixedAudioOptions.Gtk.Fixed+FixedChild
			this.labelTrackSze = new global::Gtk.Label ();
			this.labelTrackSze.Name = "labelTrackSze";
			this.labelTrackSze.LabelProp = global::Mono.Unix.Catalog.GetString ("0 MB");
			this.fixedAudioOptions.Add (this.labelTrackSze);
			global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.fixedAudioOptions [this.labelTrackSze]));
			w8.X = 110;
			w8.Y = 95;
			// Container child fixedAudioOptions.Gtk.Fixed+FixedChild
			this.labelChannels = new global::Gtk.Label ();
			this.labelChannels.Name = "labelChannels";
			this.labelChannels.LabelProp = global::Mono.Unix.Catalog.GetString ("Channels");
			this.fixedAudioOptions.Add (this.labelChannels);
			global::Gtk.Fixed.FixedChild w9 = ((global::Gtk.Fixed.FixedChild)(this.fixedAudioOptions [this.labelChannels]));
			w9.Y = 5;
			// Container child fixedAudioOptions.Gtk.Fixed+FixedChild
			this.labelBitrate = new global::Gtk.Label ();
			this.labelBitrate.Name = "labelBitrate";
			this.labelBitrate.LabelProp = global::Mono.Unix.Catalog.GetString ("Bitrate");
			this.fixedAudioOptions.Add (this.labelBitrate);
			global::Gtk.Fixed.FixedChild w10 = ((global::Gtk.Fixed.FixedChild)(this.fixedAudioOptions [this.labelBitrate]));
			w10.Y = 35;
			// Container child fixedAudioOptions.Gtk.Fixed+FixedChild
			this.labelKbps = new global::Gtk.Label ();
			this.labelKbps.Name = "labelKbps";
			this.labelKbps.LabelProp = global::Mono.Unix.Catalog.GetString ("kpbs");
			this.fixedAudioOptions.Add (this.labelKbps);
			global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.fixedAudioOptions [this.labelKbps]));
			w11.X = 215;
			w11.Y = 35;
			// Container child fixedAudioOptions.Gtk.Fixed+FixedChild
			this.comboBitrate = global::Gtk.ComboBoxEntry.NewText ();
			this.comboBitrate.WidthRequest = 100;
			this.comboBitrate.Name = "comboBitrate";
			this.fixedAudioOptions.Add (this.comboBitrate);
			global::Gtk.Fixed.FixedChild w12 = ((global::Gtk.Fixed.FixedChild)(this.fixedAudioOptions [this.comboBitrate]));
			w12.X = 110;
			w12.Y = 30;
			// Container child fixedAudioOptions.Gtk.Fixed+FixedChild
			this.labelKhz = new global::Gtk.Label ();
			this.labelKhz.Name = "labelKhz";
			this.labelKhz.LabelProp = global::Mono.Unix.Catalog.GetString ("Hz");
			this.fixedAudioOptions.Add (this.labelKhz);
			global::Gtk.Fixed.FixedChild w13 = ((global::Gtk.Fixed.FixedChild)(this.fixedAudioOptions [this.labelKhz]));
			w13.X = 300;
			w13.Y = 65;
			// Container child fixedAudioOptions.Gtk.Fixed+FixedChild
			this.comboSampleRate = global::Gtk.ComboBoxEntry.NewText ();
			this.comboSampleRate.Name = "comboSampleRate";
			this.fixedAudioOptions.Add (this.comboSampleRate);
			global::Gtk.Fixed.FixedChild w14 = ((global::Gtk.Fixed.FixedChild)(this.fixedAudioOptions [this.comboSampleRate]));
			w14.X = 110;
			w14.Y = 60;
			this.GtkAlignment.Add (this.fixedAudioOptions);
			this.frameAudioOptions.Add (this.GtkAlignment);
			this.GtkLabel = new global::Gtk.Label ();
			this.GtkLabel.Name = "GtkLabel";
			this.GtkLabel.UseMarkup = true;
			this.frameAudioOptions.LabelWidget = this.GtkLabel;
			this.@fixed.Add (this.frameAudioOptions);
			global::Gtk.Fixed.FixedChild w17 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameAudioOptions]));
			w17.X = 5;
			w17.Y = 70;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.eventBox = new global::Gtk.EventBox ();
			this.eventBox.WidthRequest = 25;
			this.eventBox.HeightRequest = 25;
			this.eventBox.Events = ((global::Gdk.EventMask)(256));
			this.eventBox.Name = "eventBox";
			// Container child eventBox.Gtk.Container+ContainerChild
			this.image = new global::Gtk.Image ();
			this.image.Name = "image";
			this.image.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-dialog-info", global::Gtk.IconSize.Menu);
			this.eventBox.Add (this.image);
			this.@fixed.Add (this.eventBox);
			global::Gtk.Fixed.FixedChild w19 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.eventBox]));
			w19.X = 99;
			w19.Y = 40;
			this.Add (this.@fixed);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.comboTracks.Changed += new global::System.EventHandler (this.OnComboTracksChanged);
			this.comboCodec.Changed += new global::System.EventHandler (this.OnComboCodecChanged);
			this.eventBox.ButtonPressEvent += new global::Gtk.ButtonPressEventHandler (this.OnEventBoxButtonPressEvent);
		}
	}
}
