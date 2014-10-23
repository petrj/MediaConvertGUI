
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
	private global::Gtk.Action openAction3;
	private global::Gtk.Action openAction4;
	private global::Gtk.Action openAction2;
	private global::Gtk.Action removeAction;
	private global::Gtk.Action removeAction1;
	private global::Gtk.Action removeAction2;
	private global::Gtk.Action executeAction;
	private global::Gtk.Action dialogInfoAction;
	private global::Gtk.Action goForwardAction;
	private global::Gtk.Action floppyAction;
	private global::Gtk.Action editAction;
	private global::Gtk.Action openAction5;
	private global::Gtk.Action copyAction;
	private global::Gtk.Fixed @fixed;
	private global::Gtk.Frame frameFileList;
	private global::Gtk.Alignment GtkAlignment10;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::Gtk.TreeView tree;
	private global::Gtk.Label GtkLabelFileList;
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
	private global::Gtk.Button buttonApply;
	private global::Gtk.Frame frameGeneral;
	private global::Gtk.Alignment GtkAlignment16;
	private global::MediaConvertGUI.WidgetGeneralMediaInfo widgetGenera;
	private global::Gtk.MenuBar menubar1;

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
		this.openAction3 = new global::Gtk.Action ("openAction3", global::Mono.Unix.Catalog.GetString ("File"), null, "gtk-open");
		this.openAction3.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.openAction3, null);
		this.openAction4 = new global::Gtk.Action ("openAction4", global::Mono.Unix.Catalog.GetString ("Open .."), null, "gtk-open");
		this.openAction4.ShortLabel = global::Mono.Unix.Catalog.GetString ("Open ..");
		w1.Add (this.openAction4, null);
		this.openAction2 = new global::Gtk.Action ("openAction2", global::Mono.Unix.Catalog.GetString ("Open directory .."), null, "gtk-open");
		this.openAction2.ShortLabel = global::Mono.Unix.Catalog.GetString ("Open directory ..");
		w1.Add (this.openAction2, null);
		this.removeAction = new global::Gtk.Action ("removeAction", global::Mono.Unix.Catalog.GetString ("Remove"), null, "gtk-remove");
		this.removeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Remove");
		w1.Add (this.removeAction, null);
		this.removeAction1 = new global::Gtk.Action ("removeAction1", global::Mono.Unix.Catalog.GetString ("Remove selected"), null, "gtk-remove");
		this.removeAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Remove selected");
		w1.Add (this.removeAction1, null);
		this.removeAction2 = new global::Gtk.Action ("removeAction2", global::Mono.Unix.Catalog.GetString ("Remove all"), null, "gtk-remove");
		this.removeAction2.ShortLabel = global::Mono.Unix.Catalog.GetString ("Remove all");
		w1.Add (this.removeAction2, null);
		this.executeAction = new global::Gtk.Action ("executeAction", global::Mono.Unix.Catalog.GetString ("Actions"), null, "gtk-execute");
		this.executeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Actions");
		w1.Add (this.executeAction, null);
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
		this.frameFileList = new global::Gtk.Frame ();
		this.frameFileList.WidthRequest = 340;
		this.frameFileList.HeightRequest = 350;
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
		this.tree.Name = "tree";
		this.tree.Reorderable = true;
		this.tree.HoverExpand = true;
		this.GtkScrolledWindow.Add (this.tree);
		this.GtkAlignment10.Add (this.GtkScrolledWindow);
		this.frameFileList.Add (this.GtkAlignment10);
		this.GtkLabelFileList = new global::Gtk.Label ();
		this.GtkLabelFileList.Name = "GtkLabelFileList";
		this.GtkLabelFileList.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>File list</b>");
		this.GtkLabelFileList.UseMarkup = true;
		this.frameFileList.LabelWidget = this.GtkLabelFileList;
		this.@fixed.Add (this.frameFileList);
		global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameFileList]));
		w5.Y = 32;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameTargetVideo = new global::Gtk.Frame ();
		this.frameTargetVideo.WidthRequest = 355;
		this.frameTargetVideo.HeightRequest = 305;
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
		this.GtkLabelTargetFrameLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Target video track</b>");
		this.GtkLabelTargetFrameLabel.UseMarkup = true;
		this.frameTargetVideo.LabelWidget = this.GtkLabelTargetFrameLabel;
		this.@fixed.Add (this.frameTargetVideo);
		global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameTargetVideo]));
		w8.X = 350;
		w8.Y = 32;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameVideo = new global::Gtk.Frame ();
		this.frameVideo.WidthRequest = 355;
		this.frameVideo.HeightRequest = 305;
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
		this.GtkLabelSourceVideoFrame.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Source video track</b>");
		this.GtkLabelSourceVideoFrame.UseMarkup = true;
		this.frameVideo.LabelWidget = this.GtkLabelSourceVideoFrame;
		this.@fixed.Add (this.frameVideo);
		global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameVideo]));
		w11.X = 700;
		w11.Y = 32;
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
		this.GtkLabelAidoTracks.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Source audio track(s)</b>");
		this.GtkLabelAidoTracks.UseMarkup = true;
		this.frameSourceAudioTracks.LabelWidget = this.GtkLabelAidoTracks;
		this.@fixed.Add (this.frameSourceAudioTracks);
		global::Gtk.Fixed.FixedChild w14 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameSourceAudioTracks]));
		w14.X = 700;
		w14.Y = 340;
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
		this.GtkLabelTargetaudio.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Target audio track</b>");
		this.GtkLabelTargetaudio.UseMarkup = true;
		this.frameTargetAudio.LabelWidget = this.GtkLabelTargetaudio;
		this.@fixed.Add (this.frameTargetAudio);
		global::Gtk.Fixed.FixedChild w17 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameTargetAudio]));
		w17.X = 350;
		w17.Y = 340;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.buttonApply = new global::Gtk.Button ();
		this.buttonApply.WidthRequest = 330;
		this.buttonApply.HeightRequest = 25;
		this.buttonApply.CanFocus = true;
		this.buttonApply.Name = "buttonApply";
		this.buttonApply.UseUnderline = true;
		// Container child buttonApply.Gtk.Container+ContainerChild
		global::Gtk.Alignment w18 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
		// Container child GtkAlignment.Gtk.Container+ContainerChild
		global::Gtk.HBox w19 = new global::Gtk.HBox ();
		w19.Spacing = 2;
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Image w20 = new global::Gtk.Image ();
		w20.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
		w19.Add (w20);
		// Container child GtkHBox.Gtk.Container+ContainerChild
		global::Gtk.Label w22 = new global::Gtk.Label ();
		w22.LabelProp = global::Mono.Unix.Catalog.GetString ("Apply");
		w22.UseUnderline = true;
		w19.Add (w22);
		w18.Add (w19);
		this.buttonApply.Add (w18);
		this.@fixed.Add (this.buttonApply);
		global::Gtk.Fixed.FixedChild w26 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.buttonApply]));
		w26.X = 5;
		w26.Y = 535;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.frameGeneral = new global::Gtk.Frame ();
		this.frameGeneral.WidthRequest = 330;
		this.frameGeneral.HeightRequest = 135;
		this.frameGeneral.Name = "frameGeneral";
		// Container child frameGeneral.Gtk.Container+ContainerChild
		this.GtkAlignment16 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
		this.GtkAlignment16.Name = "GtkAlignment16";
		this.GtkAlignment16.LeftPadding = ((uint)(5));
		this.GtkAlignment16.TopPadding = ((uint)(15));
		// Container child GtkAlignment16.Gtk.Container+ContainerChild
		this.widgetGenera = new global::MediaConvertGUI.WidgetGeneralMediaInfo ();
		this.widgetGenera.WidthRequest = 350;
		this.widgetGenera.Events = ((global::Gdk.EventMask)(256));
		this.widgetGenera.Name = "widgetGenera";
		this.GtkAlignment16.Add (this.widgetGenera);
		this.frameGeneral.Add (this.GtkAlignment16);
		this.@fixed.Add (this.frameGeneral);
		global::Gtk.Fixed.FixedChild w29 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.frameGeneral]));
		w29.X = 5;
		w29.Y = 390;
		// Container child fixed.Gtk.Fixed+FixedChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubar1'><menu name='openAction3' action='openAction3'><menuitem name='openAction4' action='openAction4'/><menuitem name='openAction2' action='openAction2'/></menu><menu name='removeAction' action='removeAction'><menuitem name='removeAction1' action='removeAction1'/><menuitem name='removeAction2' action='removeAction2'/></menu><menu name='executeAction' action='executeAction'><menuitem name='floppyAction' action='floppyAction'/><menuitem name='copyAction' action='copyAction'/><separator/><menuitem name='dialogInfoAction' action='dialogInfoAction'/><menuitem name='editAction' action='editAction'/><separator/><menuitem name='goForwardAction' action='goForwardAction'/></menu></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.@fixed.Add (this.menubar1);
		global::Gtk.Fixed.FixedChild w30 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.menubar1]));
		w30.X = 5;
		this.Add (this.@fixed);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 1080;
		this.DefaultHeight = 604;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.newAction.Activated += new global::System.EventHandler (this.OnButtonAddClicked);
		this.openAction.Activated += new global::System.EventHandler (this.OnButtonAddFolderClicked);
		this.closeAction.Activated += new global::System.EventHandler (this.OnButtonRemoveClicked);
		this.cancelAction.Activated += new global::System.EventHandler (this.OnButtonRemoveAllClicked);
		this.indexAction.Activated += new global::System.EventHandler (this.OnPreviwButtonClicked);
		this.applyAction.Activated += new global::System.EventHandler (this.OnButtonGoConvertClicked);
		this.openAction4.Activated += new global::System.EventHandler (this.OnButtonAddClicked);
		this.openAction2.Activated += new global::System.EventHandler (this.OnButtonAddFolderClicked);
		this.removeAction1.Activated += new global::System.EventHandler (this.OnButtonRemoveClicked);
		this.removeAction2.Activated += new global::System.EventHandler (this.OnButtonRemoveAllClicked);
		this.dialogInfoAction.Activated += new global::System.EventHandler (this.OnPreviwButtonClicked);
		this.goForwardAction.Activated += new global::System.EventHandler (this.OnButtonGoConvertClicked);
		this.floppyAction.Activated += new global::System.EventHandler (this.OnButtonSaveSchemeActivated);
		this.editAction.Activated += new global::System.EventHandler (this.OnShowLogActivated);
		this.copyAction.Activated += new global::System.EventHandler (this.OnCopyActionActivated);
	}
}
