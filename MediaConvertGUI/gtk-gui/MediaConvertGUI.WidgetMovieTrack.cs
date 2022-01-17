
// This file has been generated by the GUI designer. Do not modify.
namespace MediaConvertGUI
{
	public partial class WidgetMovieTrack
	{
		private global::Gtk.Fixed @fixed;
		private global::Gtk.Label labelCodecInfo;
		private global::Gtk.ComboBox comboCodec;
		private global::Gtk.Frame frameVideooptions;
		private global::Gtk.Alignment GtkAlignment;
		private global::Gtk.Fixed fixedVideoptions;
		private global::Gtk.Label labelWidth;
		private global::Gtk.Label labelRealWidth;
		private global::Gtk.Label labelAspect;
		private global::Gtk.Label labelBitRate;
		private global::Gtk.Label labelFrameRate;
		private global::Gtk.Label labelTrackSizeInfo;
		private global::Gtk.ComboBoxEntry comboBitRate;
		private global::Gtk.ComboBoxEntry comboAspect;
		private global::Gtk.Entry entryPixelAspect;
		private global::Gtk.Entry entryRealWidth;
		private global::Gtk.CheckButton checkKeep;
		private global::Gtk.Entry entryWidth;
		private global::Gtk.Entry entryHeight;
		private global::Gtk.Label labelX;
		private global::Gtk.Label labelkbps;
		private global::Gtk.ComboBoxEntry comboFrameRate;
		private global::Gtk.Label labelFps;
		private global::Gtk.Label labelTrackSize;
		private global::Gtk.Label labelPixelAspect;
		private global::Gtk.CheckButton chBoxResolution;
		private global::Gtk.CheckButton chBoxAspect;
		private global::Gtk.CheckButton chBoxFrameRate;
		private global::Gtk.CheckButton chBoxBitRate;
		private global::Gtk.ComboBoxEntry comboRotation;
		private global::Gtk.Label labelAngle;
		private global::Gtk.Label labelRotationAngle;
		private global::Gtk.CheckButton chBoxRotation;
		private global::Gtk.CheckButton checkAutorotate;
		private global::Gtk.Label GtkLabelVideoOptions;
		private global::Gtk.EventBox eventBoxCodec;
		private global::Gtk.Image imageCodec;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MediaConvertGUI.WidgetMovieTrack
			global::Stetic.BinContainer.Attach (this);
			this.Name = "MediaConvertGUI.WidgetMovieTrack";
			// Container child MediaConvertGUI.WidgetMovieTrack.Gtk.Container+ContainerChild
			this.@fixed = new global::Gtk.Fixed ();
			this.@fixed.Name = "fixed";
			this.@fixed.HasWindow = false;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelCodecInfo = new global::Gtk.Label ();
			this.labelCodecInfo.Name = "labelCodecInfo";
			this.labelCodecInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("Codec");
			this.@fixed.Add (this.labelCodecInfo);
			global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelCodecInfo]));
			w1.X = 35;
			w1.Y = 10;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.comboCodec = global::Gtk.ComboBox.NewText ();
			this.comboCodec.Name = "comboCodec";
			this.@fixed.Add (this.comboCodec);
			global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.comboCodec]));
			w2.X = 145;
			w2.Y = 5;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.frameVideooptions = new global::Gtk.Frame ();
			this.frameVideooptions.WidthRequest = 315;
			this.frameVideooptions.Name = "frameVideooptions";
			this.frameVideooptions.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frameVideooptions.Gtk.Container+ContainerChild
			this.GtkAlignment = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment.Name = "GtkAlignment";
			this.GtkAlignment.LeftPadding = ((uint)(12));
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			this.fixedVideoptions = new global::Gtk.Fixed ();
			this.fixedVideoptions.Name = "fixedVideoptions";
			this.fixedVideoptions.HasWindow = false;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelWidth = new global::Gtk.Label ();
			this.labelWidth.Name = "labelWidth";
			this.labelWidth.LabelProp = global::Mono.Unix.Catalog.GetString ("Width x Height");
			this.fixedVideoptions.Add (this.labelWidth);
			global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelWidth]));
			w3.Y = 5;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelRealWidth = new global::Gtk.Label ();
			this.labelRealWidth.Name = "labelRealWidth";
			this.labelRealWidth.LabelProp = global::Mono.Unix.Catalog.GetString ("Real width ");
			this.fixedVideoptions.Add (this.labelRealWidth);
			global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelRealWidth]));
			w4.Y = 35;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelAspect = new global::Gtk.Label ();
			this.labelAspect.Name = "labelAspect";
			this.labelAspect.LabelProp = global::Mono.Unix.Catalog.GetString ("Aspect");
			this.fixedVideoptions.Add (this.labelAspect);
			global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelAspect]));
			w5.Y = 65;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelBitRate = new global::Gtk.Label ();
			this.labelBitRate.Name = "labelBitRate";
			this.labelBitRate.LabelProp = global::Mono.Unix.Catalog.GetString ("Bitrate");
			this.fixedVideoptions.Add (this.labelBitRate);
			global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelBitRate]));
			w6.Y = 95;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelFrameRate = new global::Gtk.Label ();
			this.labelFrameRate.Name = "labelFrameRate";
			this.labelFrameRate.LabelProp = global::Mono.Unix.Catalog.GetString ("Frame rate");
			this.fixedVideoptions.Add (this.labelFrameRate);
			global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelFrameRate]));
			w7.Y = 125;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelTrackSizeInfo = new global::Gtk.Label ();
			this.labelTrackSizeInfo.Name = "labelTrackSizeInfo";
			this.labelTrackSizeInfo.LabelProp = global::Mono.Unix.Catalog.GetString ("Track Size");
			this.fixedVideoptions.Add (this.labelTrackSizeInfo);
			global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelTrackSizeInfo]));
			w8.Y = 185;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.comboBitRate = global::Gtk.ComboBoxEntry.NewText ();
			this.comboBitRate.WidthRequest = 140;
			this.comboBitRate.Name = "comboBitRate";
			this.fixedVideoptions.Add (this.comboBitRate);
			global::Gtk.Fixed.FixedChild w9 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.comboBitRate]));
			w9.X = 110;
			w9.Y = 90;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.comboAspect = global::Gtk.ComboBoxEntry.NewText ();
			this.comboAspect.WidthRequest = 100;
			this.comboAspect.Name = "comboAspect";
			this.fixedVideoptions.Add (this.comboAspect);
			global::Gtk.Fixed.FixedChild w10 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.comboAspect]));
			w10.X = 110;
			w10.Y = 60;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.entryPixelAspect = new global::Gtk.Entry ();
			this.entryPixelAspect.WidthRequest = 50;
			this.entryPixelAspect.CanFocus = true;
			this.entryPixelAspect.Name = "entryPixelAspect";
			this.entryPixelAspect.IsEditable = true;
			this.entryPixelAspect.InvisibleChar = '●';
			this.fixedVideoptions.Add (this.entryPixelAspect);
			global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.entryPixelAspect]));
			w11.X = 250;
			w11.Y = 30;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.entryRealWidth = new global::Gtk.Entry ();
			this.entryRealWidth.WidthRequest = 50;
			this.entryRealWidth.CanFocus = true;
			this.entryRealWidth.Name = "entryRealWidth";
			this.entryRealWidth.IsEditable = true;
			this.entryRealWidth.InvisibleChar = '●';
			this.fixedVideoptions.Add (this.entryRealWidth);
			global::Gtk.Fixed.FixedChild w12 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.entryRealWidth]));
			w12.X = 110;
			w12.Y = 30;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.checkKeep = new global::Gtk.CheckButton ();
			this.checkKeep.CanFocus = true;
			this.checkKeep.Name = "checkKeep";
			this.checkKeep.Label = global::Mono.Unix.Catalog.GetString ("keep");
			this.checkKeep.Active = true;
			this.checkKeep.DrawIndicator = true;
			this.checkKeep.UseUnderline = true;
			this.fixedVideoptions.Add (this.checkKeep);
			global::Gtk.Fixed.FixedChild w13 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.checkKeep]));
			w13.X = 215;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.entryWidth = new global::Gtk.Entry ();
			this.entryWidth.WidthRequest = 40;
			this.entryWidth.CanFocus = true;
			this.entryWidth.Name = "entryWidth";
			this.entryWidth.IsEditable = true;
			this.entryWidth.InvisibleChar = '●';
			this.fixedVideoptions.Add (this.entryWidth);
			global::Gtk.Fixed.FixedChild w14 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.entryWidth]));
			w14.X = 110;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.entryHeight = new global::Gtk.Entry ();
			this.entryHeight.WidthRequest = 40;
			this.entryHeight.CanFocus = true;
			this.entryHeight.Name = "entryHeight";
			this.entryHeight.IsEditable = true;
			this.entryHeight.InvisibleChar = '●';
			this.fixedVideoptions.Add (this.entryHeight);
			global::Gtk.Fixed.FixedChild w15 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.entryHeight]));
			w15.X = 170;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelX = new global::Gtk.Label ();
			this.labelX.Name = "labelX";
			this.labelX.LabelProp = global::Mono.Unix.Catalog.GetString ("x");
			this.fixedVideoptions.Add (this.labelX);
			global::Gtk.Fixed.FixedChild w16 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelX]));
			w16.X = 155;
			w16.Y = 5;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelkbps = new global::Gtk.Label ();
			this.labelkbps.Name = "labelkbps";
			this.labelkbps.LabelProp = global::Mono.Unix.Catalog.GetString ("kbps");
			this.fixedVideoptions.Add (this.labelkbps);
			global::Gtk.Fixed.FixedChild w17 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelkbps]));
			w17.X = 260;
			w17.Y = 95;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.comboFrameRate = global::Gtk.ComboBoxEntry.NewText ();
			this.comboFrameRate.WidthRequest = 100;
			this.comboFrameRate.Name = "comboFrameRate";
			this.fixedVideoptions.Add (this.comboFrameRate);
			global::Gtk.Fixed.FixedChild w18 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.comboFrameRate]));
			w18.X = 110;
			w18.Y = 120;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelFps = new global::Gtk.Label ();
			this.labelFps.Name = "labelFps";
			this.labelFps.LabelProp = global::Mono.Unix.Catalog.GetString ("fps");
			this.fixedVideoptions.Add (this.labelFps);
			global::Gtk.Fixed.FixedChild w19 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelFps]));
			w19.X = 220;
			w19.Y = 125;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelTrackSize = new global::Gtk.Label ();
			this.labelTrackSize.Name = "labelTrackSize";
			this.labelTrackSize.LabelProp = global::Mono.Unix.Catalog.GetString ("0 MB");
			this.fixedVideoptions.Add (this.labelTrackSize);
			global::Gtk.Fixed.FixedChild w20 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelTrackSize]));
			w20.X = 115;
			w20.Y = 185;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelPixelAspect = new global::Gtk.Label ();
			this.labelPixelAspect.Name = "labelPixelAspect";
			this.labelPixelAspect.LabelProp = global::Mono.Unix.Catalog.GetString ("Pixel aspect");
			this.fixedVideoptions.Add (this.labelPixelAspect);
			global::Gtk.Fixed.FixedChild w21 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelPixelAspect]));
			w21.X = 165;
			w21.Y = 35;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.chBoxResolution = new global::Gtk.CheckButton ();
			this.chBoxResolution.CanFocus = true;
			this.chBoxResolution.Name = "chBoxResolution";
			this.chBoxResolution.Label = "";
			this.chBoxResolution.Active = true;
			this.chBoxResolution.DrawIndicator = true;
			this.chBoxResolution.UseUnderline = true;
			this.fixedVideoptions.Add (this.chBoxResolution);
			global::Gtk.Fixed.FixedChild w22 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.chBoxResolution]));
			w22.X = -25;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.chBoxAspect = new global::Gtk.CheckButton ();
			this.chBoxAspect.CanFocus = true;
			this.chBoxAspect.Name = "chBoxAspect";
			this.chBoxAspect.Label = "";
			this.chBoxAspect.DrawIndicator = true;
			this.chBoxAspect.UseUnderline = true;
			this.fixedVideoptions.Add (this.chBoxAspect);
			global::Gtk.Fixed.FixedChild w23 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.chBoxAspect]));
			w23.X = -25;
			w23.Y = 60;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.chBoxFrameRate = new global::Gtk.CheckButton ();
			this.chBoxFrameRate.CanFocus = true;
			this.chBoxFrameRate.Name = "chBoxFrameRate";
			this.chBoxFrameRate.Label = "";
			this.chBoxFrameRate.DrawIndicator = true;
			this.chBoxFrameRate.UseUnderline = true;
			this.fixedVideoptions.Add (this.chBoxFrameRate);
			global::Gtk.Fixed.FixedChild w24 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.chBoxFrameRate]));
			w24.X = -25;
			w24.Y = 120;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.chBoxBitRate = new global::Gtk.CheckButton ();
			this.chBoxBitRate.CanFocus = true;
			this.chBoxBitRate.Name = "chBoxBitRate";
			this.chBoxBitRate.Label = "";
			this.chBoxBitRate.DrawIndicator = true;
			this.chBoxBitRate.UseUnderline = true;
			this.fixedVideoptions.Add (this.chBoxBitRate);
			global::Gtk.Fixed.FixedChild w25 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.chBoxBitRate]));
			w25.X = -25;
			w25.Y = 90;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.comboRotation = global::Gtk.ComboBoxEntry.NewText ();
			this.comboRotation.WidthRequest = 70;
			this.comboRotation.Name = "comboRotation";
			this.fixedVideoptions.Add (this.comboRotation);
			global::Gtk.Fixed.FixedChild w26 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.comboRotation]));
			w26.X = 111;
			w26.Y = 150;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelAngle = new global::Gtk.Label ();
			this.labelAngle.Name = "labelAngle";
			this.labelAngle.LabelProp = global::Mono.Unix.Catalog.GetString ("°");
			this.fixedVideoptions.Add (this.labelAngle);
			global::Gtk.Fixed.FixedChild w27 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelAngle]));
			w27.X = 185;
			w27.Y = 153;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.labelRotationAngle = new global::Gtk.Label ();
			this.labelRotationAngle.Name = "labelRotationAngle";
			this.labelRotationAngle.LabelProp = global::Mono.Unix.Catalog.GetString ("Rotation angle");
			this.fixedVideoptions.Add (this.labelRotationAngle);
			global::Gtk.Fixed.FixedChild w28 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.labelRotationAngle]));
			w28.Y = 155;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.chBoxRotation = new global::Gtk.CheckButton ();
			this.chBoxRotation.CanFocus = true;
			this.chBoxRotation.Name = "chBoxRotation";
			this.chBoxRotation.Label = "";
			this.chBoxRotation.DrawIndicator = true;
			this.chBoxRotation.UseUnderline = true;
			this.fixedVideoptions.Add (this.chBoxRotation);
			global::Gtk.Fixed.FixedChild w29 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.chBoxRotation]));
			w29.X = -25;
			w29.Y = 152;
			// Container child fixedVideoptions.Gtk.Fixed+FixedChild
			this.checkAutorotate = new global::Gtk.CheckButton ();
			this.checkAutorotate.CanFocus = true;
			this.checkAutorotate.Name = "checkAutorotate";
			this.checkAutorotate.Label = global::Mono.Unix.Catalog.GetString ("Fix angle\n(Auto rotate)");
			this.checkAutorotate.DrawIndicator = true;
			this.checkAutorotate.UseUnderline = true;
			this.fixedVideoptions.Add (this.checkAutorotate);
			global::Gtk.Fixed.FixedChild w30 = ((global::Gtk.Fixed.FixedChild)(this.fixedVideoptions [this.checkAutorotate]));
			w30.X = 195;
			w30.Y = 150;
			this.GtkAlignment.Add (this.fixedVideoptions);
			this.frameVideooptions.Add (this.GtkAlignment);
			this.GtkLabelVideoOptions = new global::Gtk.Label ();
			this.GtkLabelVideoOptions.Name = "GtkLabelVideoOptions";
			this.GtkLabelVideoOptions.LabelProp = global::Mono.Unix.Catalog.GetString ("<b></b>");
			this.GtkLabelVideoOptions.UseMarkup = true;
			this.frameVideooptions.LabelWidget = this.GtkLabelVideoOptions;
			this.@fixed.Add (this.frameVideooptions);
			global::Gtk.Fixed.FixedChild w33 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameVideooptions]));
			w33.X = 20;
			w33.Y = 35;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.eventBoxCodec = new global::Gtk.EventBox ();
			this.eventBoxCodec.WidthRequest = 30;
			this.eventBoxCodec.HeightRequest = 30;
			this.eventBoxCodec.Events = ((global::Gdk.EventMask)(256));
			this.eventBoxCodec.Name = "eventBoxCodec";
			// Container child eventBoxCodec.Gtk.Container+ContainerChild
			this.imageCodec = new global::Gtk.Image ();
			this.imageCodec.WidthRequest = 25;
			this.imageCodec.HeightRequest = 25;
			this.imageCodec.Name = "imageCodec";
			this.imageCodec.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-dialog-info", global::Gtk.IconSize.Menu);
			this.eventBoxCodec.Add (this.imageCodec);
			this.@fixed.Add (this.eventBoxCodec);
			global::Gtk.Fixed.FixedChild w35 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.eventBoxCodec]));
			w35.X = 110;
			w35.Y = 5;
			this.Add (this.@fixed);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.eventBoxCodec.Hide ();
			this.Hide ();
		}
	}
}
