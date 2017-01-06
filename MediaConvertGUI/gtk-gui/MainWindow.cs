
// This file has been generated by the GUI designer. Do not modify.
public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.Action FiAction;
	private global::Gtk.Action FileAction;
	private global::Gtk.Action newAction;
	private global::Gtk.Action openAction;
	private global::Gtk.Action closeAction;
	private global::Gtk.Action cancelAction;
	private global::Gtk.Action ActionsAction;
	private global::Gtk.Action indexAction;
	private global::Gtk.Action applyAction;
	private global::Gtk.Action addAction;
	private global::Gtk.Action addAction1;
	private global::Gtk.Action addAction2;
	private global::Gtk.Action removeAction;
	private global::Gtk.Action removeAction3;
	private global::Gtk.Action removeAction4;
	private global::Gtk.Action SchemeAction;
	private global::Gtk.Action dialogInfoAction;
	private global::Gtk.Action goForwardAction;
	private global::Gtk.Action floppyAction;
	private global::Gtk.Action editAction;
	private global::Gtk.Action openAction5;
	private global::Gtk.Action copyAction;
	private global::Gtk.Action applyAction1;
	private global::Gtk.Action applyAction2;
	private global::Gtk.Action saveAction;
	private global::Gtk.Action MenuAction;
	private global::Gtk.Fixed @fixed;
	private global::Gtk.Frame frameTargetVideo;
	private global::Gtk.Alignment GtkAlignment8;
	private global::MediaConvertGUI.WidgetMovieTrack widgetTargetMovieTrack;
	private global::Gtk.Label GtkLabelTargetFrameLabel;
	private global::Gtk.Frame frameVideo;
	private global::Gtk.Alignment GtkAlignment7;
	private global::MediaConvertGUI.WidgetMovieTrack widgetSourceMovieTrack;
	private global::Gtk.Label GtkLabelSourceVideoFrame;
	private global::Gtk.Frame frameSourceAudioTracks;
	private global::Gtk.Alignment GtkAlignment6;
	private global::MediaConvertGUI.WidgetAudioTracks widgetSourceAudioTracks;
	private global::Gtk.Label GtkLabelAidoTracks;
	private global::Gtk.Frame frameTargetAudio;
	private global::Gtk.Alignment GtkAlignment14;
	private global::MediaConvertGUI.WidgetAudioTracks widgetTargetAudioTracks;
	private global::Gtk.Label GtkLabelTargetaudio;
	private global::Gtk.Frame frameGeneral;
	private global::Gtk.Alignment GtkAlignment16;
	private global::MediaConvertGUI.WidgetGeneralMediaInfo widgetGeneral;
	private global::Gtk.Frame frameFileList;
	private global::Gtk.Alignment GtkAlignment10;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::Gtk.TreeView tree;
	private global::Gtk.Label GtkLabelFileList;
	private global::Gtk.MenuBar menubarScheme;
	private global::Gtk.ComboBox comboScheme1;
	private global::Gtk.Button btnMenu;
	private global::Gtk.Frame frameTargetContainer;
	private global::Gtk.Alignment GtkAlignment5;
	private global::MediaConvertGUI.WidgetContainer widgetTargetContainer;
	private global::Gtk.Label GtkLabelTargetContainer;
	private global::Gtk.Frame frameSourceContainer;
	private global::Gtk.Alignment GtkAlignment9;
	private global::MediaConvertGUI.WidgetContainer widgetSourceContainer;
	private global::Gtk.Label GtkLabelSourceContainer;
	private global::Gtk.Button buttonApply;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.FiAction = new global::Gtk.Action ("FiAction", global::Mono.Unix.Catalog.GetString ("Fi"), null, null);
		this.FiAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FiAction, null);
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FileAction, null);
		this.newAction = new global::Gtk.Action ("newAction", global::Mono.Unix.Catalog.GetString ("Add file"), null, "gtk-new");
		this.newAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Add file");
		w1.Add (this.newAction, null);
		this.openAction = new global::Gtk.Action ("openAction", global::Mono.Unix.Catalog.GetString ("Add folder"), null, "gtk-open");
		this.openAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Add folder");
		w1.Add (this.openAction, null);
		this.closeAction = new global::Gtk.Action ("closeAction", global::Mono.Unix.Catalog.GetString ("Remove selected files"), null, "gtk-close");
		this.closeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Remove selected files");
		w1.Add (this.closeAction, null);
		this.cancelAction = new global::Gtk.Action ("cancelAction", global::Mono.Unix.Catalog.GetString ("Remove all files"), null, "gtk-cancel");
		this.cancelAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Remove all files");
		w1.Add (this.cancelAction, null);
		this.ActionsAction = new global::Gtk.Action ("ActionsAction", global::Mono.Unix.Catalog.GetString ("Actions"), null, null);
		this.ActionsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Actions");
		w1.Add (this.ActionsAction, null);
		this.indexAction = new global::Gtk.Action ("indexAction", global::Mono.Unix.Catalog.GetString ("Preview .."), null, "gtk-index");
		this.indexAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Preview ..");
		w1.Add (this.indexAction, null);
		this.applyAction = new global::Gtk.Action ("applyAction", global::Mono.Unix.Catalog.GetString ("Convert ..."), null, "gtk-apply");
		this.applyAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Convert ...");
		w1.Add (this.applyAction, null);
		this.addAction = new global::Gtk.Action ("addAction", global::Mono.Unix.Catalog.GetString ("Add"), null, "gtk-add");
		this.addAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Add");
		w1.Add (this.addAction, null);
		this.addAction1 = new global::Gtk.Action ("addAction1", global::Mono.Unix.Catalog.GetString ("Add file ..."), null, "gtk-add");
		this.addAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Open ..");
		w1.Add (this.addAction1, null);
		this.addAction2 = new global::Gtk.Action ("addAction2", global::Mono.Unix.Catalog.GetString ("Add folder ..."), null, "gtk-add");
		this.addAction2.ShortLabel = global::Mono.Unix.Catalog.GetString ("Open directory ..");
		w1.Add (this.addAction2, null);
		this.removeAction = new global::Gtk.Action ("removeAction", global::Mono.Unix.Catalog.GetString ("Remove"), null, "gtk-remove");
		this.removeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Remove");
		w1.Add (this.removeAction, null);
		this.removeAction3 = new global::Gtk.Action ("removeAction3", global::Mono.Unix.Catalog.GetString ("Selected"), null, "gtk-remove");
		this.removeAction3.ShortLabel = global::Mono.Unix.Catalog.GetString ("Remove selected");
		w1.Add (this.removeAction3, null);
		this.removeAction4 = new global::Gtk.Action ("removeAction4", global::Mono.Unix.Catalog.GetString ("All"), null, "gtk-remove");
		this.removeAction4.ShortLabel = global::Mono.Unix.Catalog.GetString ("Remove all");
		w1.Add (this.removeAction4, null);
		this.SchemeAction = new global::Gtk.Action ("SchemeAction", global::Mono.Unix.Catalog.GetString ("Scheme:"), null, null);
		this.SchemeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Scheme:");
		w1.Add (this.SchemeAction, null);
		this.dialogInfoAction = new global::Gtk.Action ("dialogInfoAction", global::Mono.Unix.Catalog.GetString ("Preview ..."), null, "gtk-dialog-info");
		this.dialogInfoAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Preview");
		w1.Add (this.dialogInfoAction, null);
		this.goForwardAction = new global::Gtk.Action ("goForwardAction", global::Mono.Unix.Catalog.GetString ("Go convert"), null, "gtk-go-forward");
		this.goForwardAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Go convert");
		w1.Add (this.goForwardAction, null);
		this.floppyAction = new global::Gtk.Action ("floppyAction", global::Mono.Unix.Catalog.GetString ("Save convert scheme to xml ..."), null, "gtk-floppy");
		this.floppyAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Save convert scheme to xml");
		w1.Add (this.floppyAction, null);
		this.editAction = new global::Gtk.Action ("editAction", global::Mono.Unix.Catalog.GetString ("Show log ..."), null, "gtk-edit");
		this.editAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Show log ...");
		w1.Add (this.editAction, null);
		this.openAction5 = new global::Gtk.Action ("openAction5", global::Mono.Unix.Catalog.GetString ("Import shceme from xml"), null, "gtk-open");
		this.openAction5.ShortLabel = global::Mono.Unix.Catalog.GetString ("Import shceme from xml");
		w1.Add (this.openAction5, null);
		this.copyAction = new global::Gtk.Action ("copyAction", global::Mono.Unix.Catalog.GetString ("Import scheme from xml ..."), null, "gtk-copy");
		this.copyAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Import scheme from xml ...");
		w1.Add (this.copyAction, null);
		this.applyAction1 = new global::Gtk.Action ("applyAction1", global::Mono.Unix.Catalog.GetString ("Apply changes"), null, "gtk-apply");
		this.applyAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Apply changes");
		w1.Add (this.applyAction1, null);
		this.applyAction2 = new global::Gtk.Action ("applyAction2", global::Mono.Unix.Catalog.GetString ("Apply"), null, "gtk-apply");
		this.applyAction2.ShortLabel = global::Mono.Unix.Catalog.GetString ("Apply");
		w1.Add (this.applyAction2, null);
		this.saveAction = new global::Gtk.Action ("saveAction", global::Mono.Unix.Catalog.GetString ("Export screenshot"), null, "gtk-save");
		this.saveAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Export Screenshot");
		w1.Add (this.saveAction, null);
		this.MenuAction = new global::Gtk.Action ("MenuAction", global::Mono.Unix.Catalog.GetString ("Menu"), null, null);
		this.MenuAction.IsImportant = true;
		this.MenuAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Menu");
		w1.Add (this.MenuAction, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("Media Convert");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.@fixed = new global::Gtk.Fixed ();
		this.@fixed.WidthRequest = 350;
		this.@fixed.Name = "fixed";
		this.@fixed.HasWindow = false;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameTargetVideo = new global::Gtk.Frame ();
		this.frameTargetVideo.WidthRequest = 355;
		this.frameTargetVideo.HeightRequest = 270;
		this.frameTargetVideo.Name = "frameTargetVideo";
		// Container child frameTargetVideo.Gtk.Container+ContainerChild
		this.GtkAlignment8 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment8.Name = "GtkAlignment8";
		this.GtkAlignment8.LeftPadding = ((uint)(5));
		// Container child GtkAlignment8.Gtk.Container+ContainerChild
		this.widgetTargetMovieTrack = new global::MediaConvertGUI.WidgetMovieTrack ();
		this.widgetTargetMovieTrack.Events = ((global::Gdk.EventMask)(256));
		this.widgetTargetMovieTrack.Name = "widgetTargetMovieTrack";
		this.widgetTargetMovieTrack.Editable = false;
		this.GtkAlignment8.Add (this.widgetTargetMovieTrack);
		this.frameTargetVideo.Add (this.GtkAlignment8);
		this.GtkLabelTargetFrameLabel = new global::Gtk.Label ();
		this.GtkLabelTargetFrameLabel.Name = "GtkLabelTargetFrameLabel";
		this.GtkLabelTargetFrameLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Video</b>");
		this.GtkLabelTargetFrameLabel.UseMarkup = true;
		this.frameTargetVideo.LabelWidget = this.GtkLabelTargetFrameLabel;
		this.@fixed.Add (this.frameTargetVideo);
		global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameTargetVideo]));
		w4.X = 750;
		w4.Y = 50;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameVideo = new global::Gtk.Frame ();
		this.frameVideo.WidthRequest = 355;
		this.frameVideo.HeightRequest = 270;
		this.frameVideo.Name = "frameVideo";
		// Container child frameVideo.Gtk.Container+ContainerChild
		this.GtkAlignment7 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment7.Name = "GtkAlignment7";
		this.GtkAlignment7.LeftPadding = ((uint)(5));
		// Container child GtkAlignment7.Gtk.Container+ContainerChild
		this.widgetSourceMovieTrack = new global::MediaConvertGUI.WidgetMovieTrack ();
		this.widgetSourceMovieTrack.Events = ((global::Gdk.EventMask)(256));
		this.widgetSourceMovieTrack.Name = "widgetSourceMovieTrack";
		this.widgetSourceMovieTrack.Editable = false;
		this.GtkAlignment7.Add (this.widgetSourceMovieTrack);
		this.frameVideo.Add (this.GtkAlignment7);
		this.GtkLabelSourceVideoFrame = new global::Gtk.Label ();
		this.GtkLabelSourceVideoFrame.Name = "GtkLabelSourceVideoFrame";
		this.GtkLabelSourceVideoFrame.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Video</b>");
		this.GtkLabelSourceVideoFrame.UseMarkup = true;
		this.frameVideo.LabelWidget = this.GtkLabelSourceVideoFrame;
		this.@fixed.Add (this.frameVideo);
		global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameVideo]));
		w7.X = 5;
		w7.Y = 50;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameSourceAudioTracks = new global::Gtk.Frame ();
		this.frameSourceAudioTracks.WidthRequest = 355;
		this.frameSourceAudioTracks.HeightRequest = 220;
		this.frameSourceAudioTracks.Name = "frameSourceAudioTracks";
		// Container child frameSourceAudioTracks.Gtk.Container+ContainerChild
		this.GtkAlignment6 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment6.Name = "GtkAlignment6";
		this.GtkAlignment6.LeftPadding = ((uint)(5));
		// Container child GtkAlignment6.Gtk.Container+ContainerChild
		this.widgetSourceAudioTracks = new global::MediaConvertGUI.WidgetAudioTracks ();
		this.widgetSourceAudioTracks.Events = ((global::Gdk.EventMask)(256));
		this.widgetSourceAudioTracks.Name = "widgetSourceAudioTracks";
		this.widgetSourceAudioTracks.Editable = false;
		this.GtkAlignment6.Add (this.widgetSourceAudioTracks);
		this.frameSourceAudioTracks.Add (this.GtkAlignment6);
		this.GtkLabelAidoTracks = new global::Gtk.Label ();
		this.GtkLabelAidoTracks.Name = "GtkLabelAidoTracks";
		this.GtkLabelAidoTracks.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Audio</b>");
		this.GtkLabelAidoTracks.UseMarkup = true;
		this.frameSourceAudioTracks.LabelWidget = this.GtkLabelAidoTracks;
		this.@fixed.Add (this.frameSourceAudioTracks);
		global::Gtk.Fixed.FixedChild w10 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameSourceAudioTracks]));
		w10.X = 5;
		w10.Y = 325;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameTargetAudio = new global::Gtk.Frame ();
		this.frameTargetAudio.WidthRequest = 355;
		this.frameTargetAudio.HeightRequest = 220;
		this.frameTargetAudio.Name = "frameTargetAudio";
		// Container child frameTargetAudio.Gtk.Container+ContainerChild
		this.GtkAlignment14 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment14.Name = "GtkAlignment14";
		this.GtkAlignment14.LeftPadding = ((uint)(5));
		// Container child GtkAlignment14.Gtk.Container+ContainerChild
		this.widgetTargetAudioTracks = new global::MediaConvertGUI.WidgetAudioTracks ();
		this.widgetTargetAudioTracks.Events = ((global::Gdk.EventMask)(256));
		this.widgetTargetAudioTracks.Name = "widgetTargetAudioTracks";
		this.widgetTargetAudioTracks.Editable = true;
		this.GtkAlignment14.Add (this.widgetTargetAudioTracks);
		this.frameTargetAudio.Add (this.GtkAlignment14);
		this.GtkLabelTargetaudio = new global::Gtk.Label ();
		this.GtkLabelTargetaudio.Name = "GtkLabelTargetaudio";
		this.GtkLabelTargetaudio.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Audio</b>");
		this.GtkLabelTargetaudio.UseMarkup = true;
		this.frameTargetAudio.LabelWidget = this.GtkLabelTargetaudio;
		this.@fixed.Add (this.frameTargetAudio);
		global::Gtk.Fixed.FixedChild w13 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameTargetAudio]));
		w13.X = 750;
		w13.Y = 325;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameGeneral = new global::Gtk.Frame ();
		this.frameGeneral.WidthRequest = 370;
		this.frameGeneral.HeightRequest = 100;
		this.frameGeneral.Name = "frameGeneral";
		// Container child frameGeneral.Gtk.Container+ContainerChild
		this.GtkAlignment16 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment16.Name = "GtkAlignment16";
		this.GtkAlignment16.LeftPadding = ((uint)(5));
		this.GtkAlignment16.TopPadding = ((uint)(15));
		// Container child GtkAlignment16.Gtk.Container+ContainerChild
		this.widgetGeneral = new global::MediaConvertGUI.WidgetGeneralMediaInfo ();
		this.widgetGeneral.WidthRequest = 350;
		this.widgetGeneral.Events = ((global::Gdk.EventMask)(256));
		this.widgetGeneral.Name = "widgetGeneral";
		this.GtkAlignment16.Add (this.widgetGeneral);
		this.frameGeneral.Add (this.GtkAlignment16);
		this.@fixed.Add (this.frameGeneral);
		global::Gtk.Fixed.FixedChild w16 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameGeneral]));
		w16.X = 370;
		w16.Y = 415;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameFileList = new global::Gtk.Frame ();
		this.frameFileList.WidthRequest = 370;
		this.frameFileList.HeightRequest = 380;
		this.frameFileList.Name = "frameFileList";
		this.frameFileList.ShadowType = ((global::Gtk.ShadowType)(0));
		// Container child frameFileList.Gtk.Container+ContainerChild
		this.GtkAlignment10 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment10.Name = "GtkAlignment10";
		this.GtkAlignment10.LeftPadding = ((uint)(5));
		// Container child GtkAlignment10.Gtk.Container+ContainerChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.tree = new global::Gtk.TreeView ();
		this.tree.WidthRequest = 300;
		this.tree.CanFocus = true;
		this.tree.Events = ((global::Gdk.EventMask)(3840));
		this.tree.Name = "tree";
		this.tree.Reorderable = true;
		this.tree.HoverExpand = true;
		this.GtkScrolledWindow.Add (this.tree);
		this.GtkAlignment10.Add (this.GtkScrolledWindow);
		this.frameFileList.Add (this.GtkAlignment10);
		this.GtkLabelFileList = new global::Gtk.Label ();
		this.GtkLabelFileList.Name = "GtkLabelFileList";
		this.GtkLabelFileList.LabelProp = global::Mono.Unix.Catalog.GetString ("<b></b>");
		this.GtkLabelFileList.UseMarkup = true;
		this.frameFileList.LabelWidget = this.GtkLabelFileList;
		this.@fixed.Add (this.frameFileList);
		global::Gtk.Fixed.FixedChild w20 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameFileList]));
		w20.X = 370;
		w20.Y = 30;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubarScheme'><menu name='SchemeAction' action='SchemeAction'><menuitem name='floppyAction' action='floppyAction'/><menuitem name='copyAction' action='copyAction'/></menu><menuitem/></menubar></ui>");
		this.menubarScheme = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubarScheme")));
		this.menubarScheme.Name = "menubarScheme";
		this.@fixed.Add (this.menubarScheme);
		global::Gtk.Fixed.FixedChild w21 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.menubarScheme]));
		w21.X = 370;
		w21.Y = 520;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.comboScheme1 = global::Gtk.ComboBox.NewText ();
		this.comboScheme1.Name = "comboScheme1";
		this.@fixed.Add (this.comboScheme1);
		global::Gtk.Fixed.FixedChild w22 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.comboScheme1]));
		w22.X = 465;
		w22.Y = 520;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.btnMenu = new global::Gtk.Button ();
		this.btnMenu.CanFocus = true;
		this.btnMenu.Name = "btnMenu";
		this.btnMenu.UseUnderline = true;
		this.btnMenu.Label = global::Mono.Unix.Catalog.GetString ("Menu");
		this.@fixed.Add (this.btnMenu);
		global::Gtk.Fixed.FixedChild w23 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.btnMenu]));
		w23.X = 373;
		w23.Y = 10;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameTargetContainer = new global::Gtk.Frame ();
		this.frameTargetContainer.WidthRequest = 355;
		this.frameTargetContainer.HeightRequest = 45;
		this.frameTargetContainer.Name = "frameTargetContainer";
		// Container child frameTargetContainer.Gtk.Container+ContainerChild
		this.GtkAlignment5 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment5.Name = "GtkAlignment5";
		this.GtkAlignment5.LeftPadding = ((uint)(5));
		// Container child GtkAlignment5.Gtk.Container+ContainerChild
		this.widgetTargetContainer = new global::MediaConvertGUI.WidgetContainer ();
		this.widgetTargetContainer.Events = ((global::Gdk.EventMask)(256));
		this.widgetTargetContainer.Name = "widgetTargetContainer";
		this.widgetTargetContainer.Editable = false;
		this.GtkAlignment5.Add (this.widgetTargetContainer);
		this.frameTargetContainer.Add (this.GtkAlignment5);
		this.GtkLabelTargetContainer = new global::Gtk.Label ();
		this.GtkLabelTargetContainer.Name = "GtkLabelTargetContainer";
		this.GtkLabelTargetContainer.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Target</b>");
		this.GtkLabelTargetContainer.UseMarkup = true;
		this.frameTargetContainer.LabelWidget = this.GtkLabelTargetContainer;
		this.@fixed.Add (this.frameTargetContainer);
		global::Gtk.Fixed.FixedChild w26 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameTargetContainer]));
		w26.X = 750;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameSourceContainer = new global::Gtk.Frame ();
		this.frameSourceContainer.WidthRequest = 355;
		this.frameSourceContainer.HeightRequest = 45;
		this.frameSourceContainer.Name = "frameSourceContainer";
		// Container child frameSourceContainer.Gtk.Container+ContainerChild
		this.GtkAlignment9 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment9.Name = "GtkAlignment9";
		this.GtkAlignment9.LeftPadding = ((uint)(5));
		// Container child GtkAlignment9.Gtk.Container+ContainerChild
		this.widgetSourceContainer = new global::MediaConvertGUI.WidgetContainer ();
		this.widgetSourceContainer.Events = ((global::Gdk.EventMask)(256));
		this.widgetSourceContainer.Name = "widgetSourceContainer";
		this.widgetSourceContainer.Editable = false;
		this.GtkAlignment9.Add (this.widgetSourceContainer);
		this.frameSourceContainer.Add (this.GtkAlignment9);
		this.GtkLabelSourceContainer = new global::Gtk.Label ();
		this.GtkLabelSourceContainer.Name = "GtkLabelSourceContainer";
		this.GtkLabelSourceContainer.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Source</b>");
		this.GtkLabelSourceContainer.UseMarkup = true;
		this.frameSourceContainer.LabelWidget = this.GtkLabelSourceContainer;
		this.@fixed.Add (this.frameSourceContainer);
		global::Gtk.Fixed.FixedChild w29 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameSourceContainer]));
		w29.X = 5;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.buttonApply = new global::Gtk.Button ();
		this.buttonApply.WidthRequest = 355;
		this.buttonApply.HeightRequest = 25;
		this.buttonApply.CanFocus = true;
		this.buttonApply.Name = "buttonApply";
		this.buttonApply.UseUnderline = true;
		// Container child buttonApply.Gtk.Container+ContainerChild
		global::Gtk.Alignment w30 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w31 = new global::Gtk.HBox ();
		w31.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w32 = new global::Gtk.Image ();
		w32.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
		w31.Add (w32);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w34 = new global::Gtk.Label ();
		w34.LabelProp = global::Mono.Unix.Catalog.GetString ("Apply");
		w34.UseUnderline = true;
		w31.Add (w34);
		w30.Add (w31);
		this.buttonApply.Add (w30);
		this.@fixed.Add (this.buttonApply);
		global::Gtk.Fixed.FixedChild w38 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.buttonApply]));
		w38.X = 749;
		w38.Y = 555;
		this.Add (this.@fixed);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 1111;
		this.DefaultHeight = 587;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.newAction.Activated += new global::System.EventHandler (this.OnButtonAddClicked);
		this.openAction.Activated += new global::System.EventHandler (this.OnButtonAddFolderClicked);
		this.closeAction.Activated += new global::System.EventHandler (this.OnButtonRemoveClicked);
		this.cancelAction.Activated += new global::System.EventHandler (this.OnButtonRemoveAllClicked);
		this.indexAction.Activated += new global::System.EventHandler (this.OnPreviwButtonClicked);
		this.applyAction.Activated += new global::System.EventHandler (this.OnButtonGoConvertClicked);
		this.addAction1.Activated += new global::System.EventHandler (this.OnButtonAddClicked);
		this.addAction2.Activated += new global::System.EventHandler (this.OnButtonAddFolderClicked);
		this.removeAction3.Activated += new global::System.EventHandler (this.OnButtonRemoveClicked);
		this.removeAction4.Activated += new global::System.EventHandler (this.OnButtonRemoveAllClicked);
		this.dialogInfoAction.Activated += new global::System.EventHandler (this.OnPreviwButtonClicked);
		this.goForwardAction.Activated += new global::System.EventHandler (this.OnButtonGoConvertClicked);
		this.floppyAction.Activated += new global::System.EventHandler (this.OnButtonSaveSchemeActivated);
		this.editAction.Activated += new global::System.EventHandler (this.OnShowLogActivated);
		this.copyAction.Activated += new global::System.EventHandler (this.OnImportShcemeActionActivated);
		this.applyAction2.Activated += new global::System.EventHandler (this.OnButtonApplyClicked);
		this.MenuAction.Activated += new global::System.EventHandler (this.OnMenuActionActivated);
		this.btnMenu.Clicked += new global::System.EventHandler (this.OnBtnMenuClicked);
	}
}
