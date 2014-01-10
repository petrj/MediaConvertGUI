
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.Fixed @fixed;
	private global::Gtk.Frame frameFileList;
	private global::Gtk.Alignment GtkAlignment3;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::Gtk.TreeView tree;
	private global::Gtk.Label GtkLabelFileList;
	private global::Gtk.Frame frameGeneral;
	private global::Gtk.Alignment GtkAlignment1;
	private global::MediaConvertGUI.WidgetGeneralMediaInfo widgetGenera;
	private global::Gtk.Frame frameVideo;
	private global::Gtk.Alignment GtkAlignment2;
	private global::MediaConvertGUI.WidgetMovieTrack widgetSourceMovieTrack;
	private global::Gtk.Label GtkLabelSourceVideoFrame;
	private global::Gtk.Frame frameSourceAudioTracks;
	private global::Gtk.Alignment GtkAlignment5;
	private global::MediaConvertGUI.WidgetAudioTracks widgetSourceAudioTracks;
	private global::Gtk.Label GtkLabelAidoTracks;
	private global::Gtk.Frame frameTargetVideo;
	private global::Gtk.Alignment GtkAlignment6;
	private global::MediaConvertGUI.WidgetMovieTrack widgetTargetMovieTrack;
	private global::Gtk.Label GtkLabelTargetFrameLabel;
	private global::Gtk.Button buttonAdd;
	private global::Gtk.Button buttonGoConvert;
	private global::Gtk.Button buttonRemove;
	private global::Gtk.Button buttonAddFolder;
	private global::Gtk.Button buttonRemoveAll;
	private global::Gtk.Button buttonApply;
	private global::Gtk.Frame frameTargetAudio;
	private global::Gtk.Alignment GtkAlignment14;
	private global::MediaConvertGUI.WidgetAudioTracks widgetTargetAudioTracks;
	private global::Gtk.Label GtkLabelTargetaudio;
	private global::Gtk.Button buttonPreview;
	
	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.HeightRequest = 770;
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("Media Convert");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.@fixed = new global::Gtk.Fixed ();
		this.@fixed.WidthRequest = 350;
		this.@fixed.Name = "fixed";
		this.@fixed.HasWindow = false;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameFileList = new global::Gtk.Frame ();
		this.frameFileList.WidthRequest = 640;
		this.frameFileList.HeightRequest = 130;
		this.frameFileList.Name = "frameFileList";
		this.frameFileList.ShadowType = ((global::Gtk.ShadowType)(0));
		// Container child frameFileList.Gtk.Container+ContainerChild
		this.GtkAlignment3 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment3.Name = "GtkAlignment3";
		this.GtkAlignment3.LeftPadding = ((uint)(12));
		// Container child GtkAlignment3.Gtk.Container+ContainerChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.tree = new global::Gtk.TreeView ();
		this.tree.WidthRequest = 300;
		this.tree.CanFocus = true;
		this.tree.Name = "tree";
		this.GtkScrolledWindow.Add (this.tree);
		this.GtkAlignment3.Add (this.GtkScrolledWindow);
		this.frameFileList.Add (this.GtkAlignment3);
		this.GtkLabelFileList = new global::Gtk.Label ();
		this.GtkLabelFileList.Name = "GtkLabelFileList";
		this.GtkLabelFileList.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>File list</b>");
		this.GtkLabelFileList.UseMarkup = true;
		this.frameFileList.LabelWidget = this.GtkLabelFileList;
		this.@fixed.Add (this.frameFileList);
		global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameFileList]));
		w5.X = 5;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameGeneral = new global::Gtk.Frame ();
		this.frameGeneral.Name = "frameGeneral";
		// Container child frameGeneral.Gtk.Container+ContainerChild
		this.GtkAlignment1 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment1.WidthRequest = 635;
		this.GtkAlignment1.HeightRequest = 65;
		this.GtkAlignment1.Name = "GtkAlignment1";
		this.GtkAlignment1.LeftPadding = ((uint)(12));
		// Container child GtkAlignment1.Gtk.Container+ContainerChild
		this.widgetGenera = new global::MediaConvertGUI.WidgetGeneralMediaInfo ();
		this.widgetGenera.Events = ((global::Gdk.EventMask)(256));
		this.widgetGenera.Name = "widgetGenera";
		this.GtkAlignment1.Add (this.widgetGenera);
		this.frameGeneral.Add (this.GtkAlignment1);
		this.@fixed.Add (this.frameGeneral);
		global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameGeneral]));
		w8.X = 5;
		w8.Y = 130;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameVideo = new global::Gtk.Frame ();
		this.frameVideo.WidthRequest = 355;
		this.frameVideo.HeightRequest = 305;
		this.frameVideo.Name = "frameVideo";
		// Container child frameVideo.Gtk.Container+ContainerChild
		this.GtkAlignment2 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment2.Name = "GtkAlignment2";
		this.GtkAlignment2.LeftPadding = ((uint)(12));
		// Container child GtkAlignment2.Gtk.Container+ContainerChild
		this.widgetSourceMovieTrack = new global::MediaConvertGUI.WidgetMovieTrack ();
		this.widgetSourceMovieTrack.Events = ((global::Gdk.EventMask)(256));
		this.widgetSourceMovieTrack.Name = "widgetSourceMovieTrack";
		this.widgetSourceMovieTrack.Editable = false;
		this.GtkAlignment2.Add (this.widgetSourceMovieTrack);
		this.frameVideo.Add (this.GtkAlignment2);
		this.GtkLabelSourceVideoFrame = new global::Gtk.Label ();
		this.GtkLabelSourceVideoFrame.Name = "GtkLabelSourceVideoFrame";
		this.GtkLabelSourceVideoFrame.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Source video track</b>");
		this.GtkLabelSourceVideoFrame.UseMarkup = true;
		this.frameVideo.LabelWidget = this.GtkLabelSourceVideoFrame;
		this.@fixed.Add (this.frameVideo);
		global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameVideo]));
		w11.X = 5;
		w11.Y = 205;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameSourceAudioTracks = new global::Gtk.Frame ();
		this.frameSourceAudioTracks.WidthRequest = 355;
		this.frameSourceAudioTracks.HeightRequest = 220;
		this.frameSourceAudioTracks.Name = "frameSourceAudioTracks";
		// Container child frameSourceAudioTracks.Gtk.Container+ContainerChild
		this.GtkAlignment5 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment5.Name = "GtkAlignment5";
		this.GtkAlignment5.LeftPadding = ((uint)(12));
		// Container child GtkAlignment5.Gtk.Container+ContainerChild
		this.widgetSourceAudioTracks = new global::MediaConvertGUI.WidgetAudioTracks ();
		this.widgetSourceAudioTracks.Events = ((global::Gdk.EventMask)(256));
		this.widgetSourceAudioTracks.Name = "widgetSourceAudioTracks";
		this.widgetSourceAudioTracks.Editable = false;
		this.GtkAlignment5.Add (this.widgetSourceAudioTracks);
		this.frameSourceAudioTracks.Add (this.GtkAlignment5);
		this.GtkLabelAidoTracks = new global::Gtk.Label ();
		this.GtkLabelAidoTracks.Name = "GtkLabelAidoTracks";
		this.GtkLabelAidoTracks.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Source audio track(s)</b>");
		this.GtkLabelAidoTracks.UseMarkup = true;
		this.frameSourceAudioTracks.LabelWidget = this.GtkLabelAidoTracks;
		this.@fixed.Add (this.frameSourceAudioTracks);
		global::Gtk.Fixed.FixedChild w14 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameSourceAudioTracks]));
		w14.X = 7;
		w14.Y = 515;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameTargetVideo = new global::Gtk.Frame ();
		this.frameTargetVideo.WidthRequest = 355;
		this.frameTargetVideo.HeightRequest = 305;
		this.frameTargetVideo.Name = "frameTargetVideo";
		// Container child frameTargetVideo.Gtk.Container+ContainerChild
		this.GtkAlignment6 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment6.Name = "GtkAlignment6";
		this.GtkAlignment6.LeftPadding = ((uint)(12));
		// Container child GtkAlignment6.Gtk.Container+ContainerChild
		this.widgetTargetMovieTrack = new global::MediaConvertGUI.WidgetMovieTrack ();
		this.widgetTargetMovieTrack.Events = ((global::Gdk.EventMask)(256));
		this.widgetTargetMovieTrack.Name = "widgetTargetMovieTrack";
		this.widgetTargetMovieTrack.Editable = false;
		this.GtkAlignment6.Add (this.widgetTargetMovieTrack);
		this.frameTargetVideo.Add (this.GtkAlignment6);
		this.GtkLabelTargetFrameLabel = new global::Gtk.Label ();
		this.GtkLabelTargetFrameLabel.Name = "GtkLabelTargetFrameLabel";
		this.GtkLabelTargetFrameLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Target video track</b>");
		this.GtkLabelTargetFrameLabel.UseMarkup = true;
		this.frameTargetVideo.LabelWidget = this.GtkLabelTargetFrameLabel;
		this.@fixed.Add (this.frameTargetVideo);
		global::Gtk.Fixed.FixedChild w17 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameTargetVideo]));
		w17.X = 380;
		w17.Y = 205;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.buttonAdd = new global::Gtk.Button ();
		this.buttonAdd.WidthRequest = 35;
		this.buttonAdd.HeightRequest = 30;
		this.buttonAdd.CanFocus = true;
		this.buttonAdd.Name = "buttonAdd";
		this.buttonAdd.UseUnderline = true;
		// Container child buttonAdd.Gtk.Container+ContainerChild
		global::Gtk.Alignment w18 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w19 = new global::Gtk.HBox ();
		w19.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w20 = new global::Gtk.Image ();
		w20.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Menu);
		w19.Add (w20);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w22 = new global::Gtk.Label ();
		w19.Add (w22);
		w18.Add (w19);
		this.buttonAdd.Add (w18);
		this.@fixed.Add (this.buttonAdd);
		global::Gtk.Fixed.FixedChild w26 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.buttonAdd]));
		w26.X = 650;
		w26.Y = 15;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.buttonGoConvert = new global::Gtk.Button ();
		this.buttonGoConvert.WidthRequest = 85;
		this.buttonGoConvert.HeightRequest = 30;
		this.buttonGoConvert.CanFocus = true;
		this.buttonGoConvert.Name = "buttonGoConvert";
		this.buttonGoConvert.UseUnderline = true;
		// Container child buttonGoConvert.Gtk.Container+ContainerChild
		global::Gtk.Alignment w27 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w28 = new global::Gtk.HBox ();
		w28.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w29 = new global::Gtk.Image ();
		w29.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-media-play", global::Gtk.IconSize.Menu);
		w28.Add (w29);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w31 = new global::Gtk.Label ();
		w31.LabelProp = global::Mono.Unix.Catalog.GetString ("Go");
		w31.UseUnderline = true;
		w28.Add (w31);
		w27.Add (w28);
		this.buttonGoConvert.Add (w27);
		this.@fixed.Add (this.buttonGoConvert);
		global::Gtk.Fixed.FixedChild w35 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.buttonGoConvert]));
		w35.X = 650;
		w35.Y = 165;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.buttonRemove = new global::Gtk.Button ();
		this.buttonRemove.WidthRequest = 35;
		this.buttonRemove.HeightRequest = 30;
		this.buttonRemove.CanFocus = true;
		this.buttonRemove.Name = "buttonRemove";
		this.buttonRemove.UseUnderline = true;
		// Container child buttonRemove.Gtk.Container+ContainerChild
		global::Gtk.Alignment w36 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w37 = new global::Gtk.HBox ();
		w37.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w38 = new global::Gtk.Image ();
		w38.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-remove", global::Gtk.IconSize.Menu);
		w37.Add (w38);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w40 = new global::Gtk.Label ();
		w37.Add (w40);
		w36.Add (w37);
		this.buttonRemove.Add (w36);
		this.@fixed.Add (this.buttonRemove);
		global::Gtk.Fixed.FixedChild w44 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.buttonRemove]));
		w44.X = 650;
		w44.Y = 50;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.buttonAddFolder = new global::Gtk.Button ();
		this.buttonAddFolder.WidthRequest = 45;
		this.buttonAddFolder.HeightRequest = 30;
		this.buttonAddFolder.CanFocus = true;
		this.buttonAddFolder.Name = "buttonAddFolder";
		this.buttonAddFolder.UseUnderline = true;
		// Container child buttonAddFolder.Gtk.Container+ContainerChild
		global::Gtk.Alignment w45 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w46 = new global::Gtk.HBox ();
		w46.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w47 = new global::Gtk.Image ();
		w47.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Menu);
		w46.Add (w47);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w49 = new global::Gtk.Label ();
		w49.LabelProp = global::Mono.Unix.Catalog.GetString ("Dir");
		w49.UseUnderline = true;
		w46.Add (w49);
		w45.Add (w46);
		this.buttonAddFolder.Add (w45);
		this.@fixed.Add (this.buttonAddFolder);
		global::Gtk.Fixed.FixedChild w53 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.buttonAddFolder]));
		w53.X = 690;
		w53.Y = 15;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.buttonRemoveAll = new global::Gtk.Button ();
		this.buttonRemoveAll.WidthRequest = 45;
		this.buttonRemoveAll.HeightRequest = 30;
		this.buttonRemoveAll.CanFocus = true;
		this.buttonRemoveAll.Name = "buttonRemoveAll";
		this.buttonRemoveAll.UseUnderline = true;
		// Container child buttonRemoveAll.Gtk.Container+ContainerChild
		global::Gtk.Alignment w54 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w55 = new global::Gtk.HBox ();
		w55.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w56 = new global::Gtk.Image ();
		w56.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-remove", global::Gtk.IconSize.Menu);
		w55.Add (w56);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w58 = new global::Gtk.Label ();
		w58.LabelProp = global::Mono.Unix.Catalog.GetString ("All");
		w58.UseUnderline = true;
		w55.Add (w58);
		w54.Add (w55);
		this.buttonRemoveAll.Add (w54);
		this.@fixed.Add (this.buttonRemoveAll);
		global::Gtk.Fixed.FixedChild w62 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.buttonRemoveAll]));
		w62.X = 690;
		w62.Y = 50;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.buttonApply = new global::Gtk.Button ();
		this.buttonApply.WidthRequest = 355;
		this.buttonApply.HeightRequest = 25;
		this.buttonApply.CanFocus = true;
		this.buttonApply.Name = "buttonApply";
		this.buttonApply.UseUnderline = true;
		// Container child buttonApply.Gtk.Container+ContainerChild
		global::Gtk.Alignment w63 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w64 = new global::Gtk.HBox ();
		w64.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w65 = new global::Gtk.Image ();
		w65.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
		w64.Add (w65);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w67 = new global::Gtk.Label ();
		w67.LabelProp = global::Mono.Unix.Catalog.GetString ("Apply");
		w67.UseUnderline = true;
		w64.Add (w67);
		w63.Add (w64);
		this.buttonApply.Add (w63);
		this.@fixed.Add (this.buttonApply);
		global::Gtk.Fixed.FixedChild w71 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.buttonApply]));
		w71.X = 380;
		w71.Y = 740;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameTargetAudio = new global::Gtk.Frame ();
		this.frameTargetAudio.WidthRequest = 355;
		this.frameTargetAudio.HeightRequest = 220;
		this.frameTargetAudio.Name = "frameTargetAudio";
		// Container child frameTargetAudio.Gtk.Container+ContainerChild
		this.GtkAlignment14 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment14.Name = "GtkAlignment14";
		this.GtkAlignment14.LeftPadding = ((uint)(12));
		// Container child GtkAlignment14.Gtk.Container+ContainerChild
		this.widgetTargetAudioTracks = new global::MediaConvertGUI.WidgetAudioTracks ();
		this.widgetTargetAudioTracks.Events = ((global::Gdk.EventMask)(256));
		this.widgetTargetAudioTracks.Name = "widgetTargetAudioTracks";
		this.widgetTargetAudioTracks.Editable = true;
		this.GtkAlignment14.Add (this.widgetTargetAudioTracks);
		this.frameTargetAudio.Add (this.GtkAlignment14);
		this.GtkLabelTargetaudio = new global::Gtk.Label ();
		this.GtkLabelTargetaudio.Name = "GtkLabelTargetaudio";
		this.GtkLabelTargetaudio.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Target audio track</b>");
		this.GtkLabelTargetaudio.UseMarkup = true;
		this.frameTargetAudio.LabelWidget = this.GtkLabelTargetaudio;
		this.@fixed.Add (this.frameTargetAudio);
		global::Gtk.Fixed.FixedChild w74 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameTargetAudio]));
		w74.X = 380;
		w74.Y = 515;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.buttonPreview = new global::Gtk.Button ();
		this.buttonPreview.WidthRequest = 85;
		this.buttonPreview.HeightRequest = 30;
		this.buttonPreview.CanFocus = true;
		this.buttonPreview.Name = "buttonPreview";
		this.buttonPreview.UseUnderline = true;
		// Container child buttonPreview.Gtk.Container+ContainerChild
		global::Gtk.Alignment w75 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w76 = new global::Gtk.HBox ();
		w76.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w77 = new global::Gtk.Image ();
		w77.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-justify-fill", global::Gtk.IconSize.Menu);
		w76.Add (w77);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w79 = new global::Gtk.Label ();
		w79.LabelProp = global::Mono.Unix.Catalog.GetString ("Preview");
		w79.UseUnderline = true;
		w76.Add (w79);
		w75.Add (w76);
		this.buttonPreview.Add (w75);
		this.@fixed.Add (this.buttonPreview);
		global::Gtk.Fixed.FixedChild w83 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.buttonPreview]));
		w83.X = 650;
		w83.Y = 130;
		this.Add (this.@fixed);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 744;
		this.DefaultHeight = 795;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
	}
}
