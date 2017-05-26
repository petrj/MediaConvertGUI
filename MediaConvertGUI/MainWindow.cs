using System;
using System.Xml;
using System.IO;
using System.Threading;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Gtk;
using MediaConvertGUI;
using Grid;

public partial class MainWindow: Gtk.Window
{	
	public Dictionary<MediaInfo,MediaInfo> MoviesInfo = new Dictionary<MediaInfo,MediaInfo>();

	#region private fields

	private TreeViewData _fileTreeViewData;

	private Thread _processthread;
	private DateTime _processStartedAt;
	private string _outputFile = String.Empty;
	private bool _processAbortRequest = false;
	private MediaInfo _currentConvertingMovie;
	private int _currentPass = 1;
	private int _currentFileListCount = 0;
	private int _currentFileListNumber = -1;

	private ProgressWin _proressWindow;

	#endregion

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		// Loading configuration
		MediaConvertGUIConfiguration.Load("config.xml");


		Build ();

		// iapp icon
		var sep = System.IO.Path.DirectorySeparatorChar;
		var buffer = System.IO.File.ReadAllBytes (System.IO.Path.Combine(SupportMethods.AppPath+sep+"Icons" + sep + "ico.ico"));

		var pixbuf = new Gdk.Pixbuf (buffer);
		Icon = pixbuf;


		TestPrerequisites();

		_fileTreeViewData = new TreeViewData(tree); 
		CreateGridColumns();
		widgetTargetMovieTrack.Editable = true;
		widgetTargetContainer.Editable = true;
		widgetSourceContainer.Editable = false;

		_proressWindow = new ProgressWin();
		_proressWindow.Hide();

		tree.Selection.Mode = SelectionMode.Multiple;

		tree.CursorChanged += OnTreeCursorChanged;

		tree.ButtonPressEvent+=OnTreeButtonPressEvent;

		FillTree();
	}

	#region selection

	private List<MediaInfo> GetSelectedMediaFiles()
	{
		var selected = new List<MediaInfo>();

		foreach(Gtk.TreePath selectedItem in _fileTreeViewData.Tree.Selection.GetSelectedRows())
		{
			var indicies = selectedItem.Indices;
			if (indicies.Length>0)
			{
				var selectedIndex = indicies[0];
				if (MoviesInfo.Count>selectedIndex)
				{
					var index = 0;
					foreach (var key in MoviesInfo.Keys)
					{
						if (index == selectedIndex)
						{
							selected.Add(key);
						}
						index++;
					}
				}
			}
		}

		return selected;
	}

	private Dictionary<int,MediaInfo> GetSelectedMediaFilesWithIndices()
	{
		var selected = new Dictionary<int,MediaInfo>();

		foreach(Gtk.TreePath selectedItem in _fileTreeViewData.Tree.Selection.GetSelectedRows())
		{
			var indicies = selectedItem.Indices;
			if (indicies.Length>0)
			{
				var selectedIndex = indicies[0];
				if (MoviesInfo.Count>selectedIndex)
				{
					var index = 0;
					foreach (var key in MoviesInfo.Keys)
					{
						if (index == selectedIndex)
						{
							selected.Add(index,key);
						}
						index++;
					}
				}
			}
		}

		return selected;
	}

	public MediaInfo FirstSelectedMediaFile
	{
		get
		{

			var selected = GetSelectedMediaFiles();
			if (selected != null && selected.Count>0)
			{
				return selected[0];
			}

			return null;
		}
	}

	public void SelectRow(int rowIndex)
	{
		SelectRows(new List<int>() { rowIndex });
	}

	public void SelectRows(IEnumerable<int> rows)
	{
		if (rows == null)
			return;

		tree.Selection.UnselectAll();

		foreach (var row in rows)
		{
			if (row>=0 && row<=MoviesInfo.Count-1)		
			{
				tree.Selection.SelectIter( _fileTreeViewData.TreeIters[row]);;
			}
		}
		ScrollToFirstSelected();
	}

	public void ScrollToFirstSelected()
	{
		// scroll to first selected ?
		TreePath[] treePaths = tree.Selection.GetSelectedRows();

		if (treePaths != null &&
		    treePaths.Length > 0)
		{
			tree.ScrollToCell(treePaths[0],tree.Columns[0],false,(float)0,(float)0);
		}
	}

	#endregion

	#region methods

	private void TestPrerequisites()
	{
		var ffmpeg = false;
		var mediaInfo = false;

		try
		{
			var ffmpegOutput = SupportMethods.ExecuteAndReturnOutput (MediaConvertGUIConfiguration.FFMpegPath, "-version");
			if (!string.IsNullOrEmpty(ffmpegOutput))
			{
				if (ffmpegOutput.Contains("version "))
				{
					ffmpeg = true;
				}
			}
		} catch(Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}

		try
		{
			var mediaInfoOutput = SupportMethods.ExecuteAndReturnOutput (MediaConvertGUIConfiguration.MediaInfoPath, "--version");
			if (!string.IsNullOrEmpty(mediaInfoOutput))
			{
				if (mediaInfoOutput.Contains("MediaInfo Command line"))
				{
					 mediaInfo = true;
				}
			}
		} catch(Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}

		if (!ffmpeg)
		{
			Dialogs.InfoDialog("ffmpeg not detected!",MessageType.Warning);
		}

		if (!mediaInfo)
		{
			Dialogs.InfoDialog("MediaInfo command line not detected!",MessageType.Warning);
		}
	}

	private void CreateGridColumns()
	{
		_fileTreeViewData.Data.Clear();
		_fileTreeViewData.Columns.Clear();

		_fileTreeViewData.AppendStringColumn("Filename", null, false); 
		_fileTreeViewData.AppendStringColumn("Container", null, false);
		_fileTreeViewData.AppendStringColumn("V.codec", null, false);
		_fileTreeViewData.AppendStringColumn("A.codec", null, false);
				
		_fileTreeViewData.AppendStringColumn("Bitrate", null, false);
		_fileTreeViewData.AppendStringColumn("Duration", null, false);
		_fileTreeViewData.AppendStringColumn("Size", null, false);		
		
		_fileTreeViewData.CreateTreeViewColumns();
		
	}

    public void FillTree()
    {
		_fileTreeViewData.Data.Clear();

		// fill tree
		foreach(var info in MoviesInfo)
		{		
			var name = System.IO.Path.GetFileName (info.Key.FileName);
			var codec = info.Value.TargetVideoCodec.Name;
			var cont = info.Value.TargetContainer.Name;
			var audio = info.Value.FirstAudioTrack != null ? info.Value.FirstAudioTrack.TargetAudioCodec.ToString() : "none";
				info.Value.TargetContainer.ToString();
				
			var duration = info.Value.HuamReadableDuration;
			var size = info.Value.HumanReadableSize;
			var bitrate = info.Value.HumanReadableOverAllBitRate;
				
			_fileTreeViewData.AppendData(new List<object>{name,cont,codec,audio,bitrate,duration,size});
		}
  	   
		tree.Model = _fileTreeViewData.CreateTreeViewListStore();

		if ((MoviesInfo.Count>0) && (GetSelectedMediaFiles().Count == 0))
		{
			SelectRow(0);
		}

		OnTreeCursorChanged(this,null);

     }

	public void AddMediaInfo(string fName)
	{
		var sourceMovie = new MediaInfo();
		if (sourceMovie.OpenFromFile (fName)) 
		{
			var firstVideoTrack =  sourceMovie.FirstVideoTrack;

			if (sourceMovie.AudioTracks.Count > 0 || firstVideoTrack != null) 
			{

				var targetMovie = new MediaInfo ();
				sourceMovie.Copyto (targetMovie, false);

				if (firstVideoTrack!=null)
				{
					targetMovie.TargetContainer = MediaConvertGUIConfiguration.GetContainerByName("avi");
					targetMovie.TargetVideoCodec = MediaConvertGUIConfiguration.GetVideoCodecByName ("copy");
				} 

				// leaving only first audio track
				while (targetMovie.AudioTracks.Count>1)
				{
					TrackInfo lastAudioTrack =  null;
					foreach (var track in targetMovie.Tracks)
					{
						if (track.TrackType == "Audio")
						{
							lastAudioTrack = track;
						}
					}
					if (targetMovie.Tracks.Contains(lastAudioTrack)) 
					{
						targetMovie.Tracks.Remove(lastAudioTrack);
					} else 
					{
						break;
					}
				}

				if (targetMovie.AudioTracks.Count>0)
				{
					if (firstVideoTrack!=null)
					{
						targetMovie.FirstAudioTrack.TargetAudioCodec = MediaConvertGUIConfiguration.GetAudioCodecByName("copy");
					} else
					{
						targetMovie.FirstAudioTrack.TargetAudioCodec = MediaConvertGUIConfiguration.GetAudioCodecByName("mp3");
					}
				}

				MoviesInfo.Add (sourceMovie, targetMovie);

				FillTree ();
			}
		}	
	}

	#endregion

	#region progress

	public void ShowProgress()
    {
        _proressWindow.Show();
        _proressWindow.SetPercents(0,0,0,_processStartedAt);

		while (_processthread != null && _processthread.IsAlive) 
		{                                        
			if (_proressWindow.AbortRequest) 
			{
				if (SupportMethods.RunningPlatform == SupportMethods.RunningPlatformEnum.Unix) 
				{
					SupportMethods.ExecuteAndReturnOutput ("killall", "ffmpeg");
				} else
				{
					SupportMethods.ExecuteAndReturnOutput ("taskkill", "/f /im ffmpeg.exe");
				}
				_processAbortRequest = true;
			}

			var percentsCurrentFilePass = 0d;
			var percentsCurrentFile = 0d;
			var percentsTotal = 0d;

			var fName = String.Empty;
			var passAsString = String.Empty;
			var totalFilesAsString = String.Empty;

			if (_currentConvertingMovie != null && _currentPass>0)
			{
				decimal totalTime = _currentConvertingMovie.DurationMS/1000m;
				percentsCurrentFile = 0;
				fName = System.IO.Path.GetFileName (_currentConvertingMovie.FileName);								

				if (_currentConvertingMovie.FirstVideoTrack != null) 
				{
					decimal time = MediaInfoBase.GetLastTimeFromConvertLogFile (_outputFile);

					if (time > 0) 
					{
						percentsCurrentFilePass = percentsCurrentFile = Convert.ToDouble (time / (totalTime / 100m));
						percentsCurrentFile =  percentsCurrentFilePass/2;
						percentsCurrentFile += (_currentPass-1)*50;
					}
					/*
					var frames = Convert.ToDouble (_currentConvertingMovie.DurationMS / 1000m * _currentConvertingMovie.FirstVideoTrack.FrameRate);

					// detecting progress from text file
					var lastFrame = MediaInfoBase.GetLastFrameFromConvertLogFile (_outputFile);
					if ((frames > 0) && (lastFrame != -1)) 
					{
						percentsCurrentFilePass = Convert.ToInt32 (lastFrame / (frames / (double)100));
					}

					// computing current file progress
					if (_currentPass > 0) 
					{        

						var correctedFrame = lastFrame;
						if (correctedFrame < 0)
							correctedFrame = 0;


						passAsString = "Pass: " + _currentPass.ToString ();

						//var actualFrame = (_currentPass - 1) * frames + correctedFrame;
						percentsCurrentFile =  actualFrame / (frames * 2 / 100d);                                
					}
					*/
				} else if (_currentConvertingMovie.FirstAudioTrack != null)
				{
					// only audio convert
					decimal time = MediaInfoBase.GetLastTimeFromConvertLogFile (_outputFile);

					if (time > 0) 
					{
						percentsCurrentFilePass = percentsCurrentFile = Convert.ToDouble (time / (totalTime / 100m));
					}
				}							

				if (_currentFileListNumber >= 0 && _currentFileListCount > 0)
				{
					percentsTotal = Convert.ToDouble (_currentFileListNumber / (Convert.ToDouble (_currentFileListCount) / 100d));

					totalFilesAsString = (_currentFileListNumber + 1).ToString () + "/" + (_currentFileListCount).ToString ();

					// adding actual file progress fraction
					if (percentsCurrentFile > 0) 
					{
						var onePart = 1d / (double)_currentFileListCount;
						percentsTotal = percentsTotal + percentsCurrentFile * onePart;
					}
				}

				_proressWindow.SetPercents (Math.Round (percentsTotal, 2),
					Math.Round (percentsCurrentFile, 2),
					Math.Round (percentsCurrentFilePass, 2),
					_processStartedAt,
					fName,
					passAsString,
					totalFilesAsString);

				while (GLib.MainContext.Iteration ());
				Thread.Sleep (500);
			}
		}

		_proressWindow.SetPercents (100,
			100,
			100,
			_processStartedAt);

	}

	#endregion

	#region conversion

	public void RunCommandList()
	{
		_currentConvertingMovie = null;
		_processAbortRequest = false;

		_processthread = new Thread(ThreadMethod);
		_processStartedAt = DateTime.Now;
		_processthread.Start();

		applyAction.Sensitive = false;

	}

	private void ThreadMethod ()
    {
		 _currentFileListCount = MoviesInfo.Count;
		 _currentFileListNumber = -1;

		foreach (var kvp in MoviesInfo)
		{
			if (kvp.Value==null)
				break;

			_currentConvertingMovie = kvp.Value;
			var info = _currentConvertingMovie.FirstVideoTrack;

			_currentFileListNumber++;

			_currentPass = 1;

			if (_processAbortRequest) break;

			var cmd1 =  MediaInfoBase.MakeFFMpegCommand(kvp.Key,kvp.Value,1);
			ExecutFFMpegCommand(cmd1, kvp.Value.FFMPEGOutputFileName );

			if (info!=null &&
			    _currentConvertingMovie.TargetVideoCodec!=MediaConvertGUIConfiguration.GetVideoCodecByName("none"))
			{
				// converting 2 pass video
				var cmd2 =  MediaInfoBase.MakeFFMpegCommand(kvp.Key,kvp.Value,2);

				if (_processAbortRequest) break;

				_currentPass = 2;

				if (!String.IsNullOrEmpty(cmd2))
				{
					ExecutFFMpegCommand(cmd2, kvp.Value.FFMPEGOutputFileName);
				}
			}
		}

		Application.Invoke((_,__) =>
		{
			applyAction.Sensitive = true;
			widgetTargetMovieTrack.Editable = true;
			widgetTargetContainer.Editable = true;
			widgetSourceContainer.Editable = true;
			_processthread.Join();
			_processthread = null;
		});
    }

    public void ExecutFFMpegCommand(string cmd, string FFMPEGOutputFileName)
    {			
		_outputFile = FFMPEGOutputFileName;				
			
		string processToExecute;
		string parametres;

		if (SupportMethods.RunningPlatform == SupportMethods.RunningPlatformEnum.Unix) 
		{
			processToExecute = "sh";
			parametres = String.Format ("-c '" + cmd + " 2> \"{0}\" '", _outputFile);
		} else 
		{
			processToExecute = "cmd";
			parametres = String.Format ("/C " + cmd + " 2> \"{0}\" ", _outputFile);
		}		

		SupportMethods.ExecuteAndReturnOutput (processToExecute, parametres);
	}        

	#endregion
     
	#region events

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}	

	protected void OnButtonAddClicked (object sender, EventArgs e)
	{
		var fName = Dialogs.OpenFileDialog("Open file") ;
		if (fName != null)
		{
			AddMediaInfo(fName);
		}
	}	

	protected void OnTreeCursorChanged (object sender, EventArgs e)
	{
		var source = FirstSelectedMediaFile;

		widgetSourceContainer.FillFrom(source);
		widgetSourceMovieTrack.FillFrom(source);
		widgetSourceAudioTracks.FillFrom(source);

		MediaInfo target;
		target = source !=null && MoviesInfo.ContainsKey(source) ? MoviesInfo[source] : null;
		
		widgetTargetContainer.FillFrom(target);		
		widgetTargetContainer.Editable =  target != null;						
		widgetTargetMovieTrack.FillFrom(target);
		widgetTargetAudioTracks.FillFrom(target);		
		
	}

	protected void OnButtonGoConvertClicked (object sender, EventArgs e)
	{
		RunCommandList();
		ShowProgress();
	}	

	protected void OnPreviwButtonClicked (object sender, EventArgs e)
	{
		// creating preview string

		using (var tw  = new TextWin())
		{
			tw.Title = "FFmpeg command preview";
			tw.Text = MediaInfoBase.MakeFFMpegCommandsAsString(MoviesInfo);
			tw.Show();
		}
	}	

	
	protected void OnPlaySelectedInShelButtonClicked (object sender, EventArgs e)
	{
		var selectedMovies = GetSelectedMediaFiles();
		if (selectedMovies.Count == 0)
			return;

		foreach (var m in selectedMovies)
		{
			m.PlayInShell();
		}
	}

	protected void OnButtonRemoveClicked (object sender, EventArgs e)
	{
		var selectedMovies = GetSelectedMediaFiles();
		if (selectedMovies.Count == 0)
			return;

		foreach (var m in selectedMovies)
		{
			MoviesInfo.Remove(m);
		}

		FillTree();
	}

	protected void OnButtonAddFolderClicked (object sender, EventArgs e)
	{
		var dirName = Dialogs.OpenDirectoryDialog("Open all files in directory:");
		if (dirName != null)
		{
			foreach (var fName in Directory.GetFiles(dirName))
			{
				if (File.Exists(fName))
				{
						AddMediaInfo(fName);
				}
			}
		}
	}

	protected void OnButtonRemoveAllClicked (object sender, EventArgs e)
	{
		MoviesInfo.Clear();
		FillTree();
	}


	protected void OnScreenShotClicked (object sender, EventArgs e)
	{
		var selectedMovies = GetSelectedMediaFiles();
		if (selectedMovies.Count == 0)
			return;

		foreach (var m in selectedMovies)
		{
			m.ScreenShot();
		}
	}

	protected void OnButtonApplyClicked (object sender, EventArgs e)
	{
		var selectedMovies = GetSelectedMediaFilesWithIndices();
		if (selectedMovies.Count == 0)
			return;

		//if (Dialogs.ConfirmDialog("Save changes ?  "+System.Environment.NewLine+String.Format("(selected files: {0})",selectedMovies.Count.ToString())))
		//{
			foreach (KeyValuePair<int,MediaInfo> m in selectedMovies)
			{
				MoviesInfo[m.Value].TargetVideoCodec = widgetTargetMovieTrack.MovieInfo.TargetVideoCodec;
				//MoviesInfo[m.Value].TargetContainer = widgetTargetMovieTrack.MovieInfo.TargetContainer;
				MoviesInfo[m.Value].TargetContainer = widgetTargetContainer.Info.TargetContainer;

				var tmpDuration = MoviesInfo[m.Value].DurationMS;
				MoviesInfo[m.Value].ClearTracks();
				
				widgetTargetMovieTrack.MovieInfo.AppendTracksTo(MoviesInfo[m.Value],tmpDuration,"Video");
				widgetTargetAudioTracks.Info.AppendTracksTo(MoviesInfo[m.Value],tmpDuration,"Audio");

				MoviesInfo [m.Value].EditAspect = widgetTargetMovieTrack.MovieInfo.EditAspect;
				MoviesInfo [m.Value].EditResolution = widgetTargetMovieTrack.MovieInfo.EditResolution;
				MoviesInfo [m.Value].EditFrameRate = widgetTargetMovieTrack.MovieInfo.EditFrameRate;
				MoviesInfo [m.Value].EditBitRate = widgetTargetMovieTrack.MovieInfo.EditBitRate;
				MoviesInfo [m.Value].EditRotation = widgetTargetMovieTrack.MovieInfo.EditRotation;
				MoviesInfo [m.Value].AutoRotate = widgetTargetMovieTrack.MovieInfo.AutoRotate;
			}
		//}

		FillTree();

		SelectRows(selectedMovies.Keys);
		OnTreeCursorChanged(this,null);

	}

	protected void OnShowLogActivated (object sender, EventArgs e)
	{
		var allLogs = "No log found";

		if (MoviesInfo.Count != 0) 
		{
			allLogs = "";

			foreach (var kvp in MoviesInfo) 
			{
				var fName = kvp.Key.FileName;
				var logFileName = kvp.Key.FileName + ".converted" + kvp.Value.TargetContainer.Extension + ".log";

				if (File.Exists (logFileName)) 
				{
					// found log fname
					using (var file = File.OpenText(logFileName)) 
					{					
						allLogs += file.ReadToEnd ();
						allLogs += Environment.NewLine;
						file.Close ();
					}
				}
			}
		}

		
		using (var tw  = new TextWin())
		{
			tw.Title = "FFmpeg logs";
			tw.Text = allLogs;
			tw.Show();
		}
	}

	protected void OnButtonSaveSchemeActivated(object sender, EventArgs e)
	{
		Gtk.FileChooserDialog fc=
			new Gtk.FileChooserDialog("Choose filename to save",
			                          this,
			                          FileChooserAction.Save,
			                          "Cancel",ResponseType.Cancel,
			                          "Save",ResponseType.Ok);

		fc.SetCurrentFolder(SupportMethods.AppPath);

		if ((fc.Run() == (int)ResponseType.Ok))
		{
			widgetTargetMovieTrack.MovieInfo.SaveAsSchemeToXML(fc.Filename);
		}
		fc.Destroy();
	}

	protected void OnImportShcemeActionActivated (object sender, EventArgs e)
	{

			Gtk.FileChooserDialog fc=
				new Gtk.FileChooserDialog("Choose filename to import",
				                          this,
				                          FileChooserAction.Open,
				                          "Cancel",ResponseType.Cancel,
				                          "Import",ResponseType.Ok);

			if ((fc.Run() == (int)ResponseType.Ok))
			{
			if (Dialogs.QuestionDialog("Are you sure to import scheme from " + System.IO.Path.GetFileName(fc.Filename)+"?") == ResponseType.Ok)
				{				
					widgetTargetMovieTrack.MovieInfo.OpenSchemeFromXML(fc.Filename);					
					widgetTargetMovieTrack.Fill();
					
					widgetTargetContainer.Info.OpenSchemeFromXML(fc.Filename);
					widgetTargetContainer.Fill();					
					
					widgetTargetAudioTracks.Info.OpenSchemeFromXML(fc.Filename);
					widgetTargetAudioTracks.Fill();
				}
				fc.Destroy();

			}
	}


	protected void OnFrameFileListButtonPressEvent (object o, ButtonPressEventArgs args)
	{
	}	

	private void CreatePopupMenu()
	{
		// creating popup menu 

		Gtk.Menu popupMenu = new Gtk.Menu();

		// Add 

		Dialogs.AddImageMenuButton("Add file...","add.png",popupMenu, OnButtonAddClicked);
		Dialogs.AddImageMenuButton("Add folder...","add.png",popupMenu, OnButtonAddFolderClicked);

		// play and open with buttons for first selected media

		var selectedMovies = GetSelectedMediaFiles();
		if (selectedMovies.Count >0)			
		{
			popupMenu.Append(new SeparatorMenuItem());

			// getting first selected media
			var cmd = String.Empty;
			foreach (var f in selectedMovies)
			{
				cmd += "\"" + f.FileName + "\"";
				break; // only first 
			}

			Gtk.Menu openWithSubMenu = new Gtk.Menu();

			foreach (var app in MediaConvertGUIConfiguration.OpenWithApplications)
			{
				var btn = new MenuItem(app);
				openWithSubMenu.Add(btn);
				btn.Activated+= delegate { SupportMethods.Execute(app,cmd); };
			}

			MenuItem menuitemOpenWith = new MenuItem("Open with.");
			menuitemOpenWith.Submenu = openWithSubMenu;

			popupMenu.Add(menuitemOpenWith);

			Dialogs.AddImageMenuButton("Play...","play.png",popupMenu, OnPlaySelectedInShelButtonClicked);
			Dialogs.AddImageMenuButton("Export movie screenshot","screenshot.png",popupMenu, OnScreenShotClicked);
			Dialogs.AddImageMenuButton("Preview ffmpeg batch ...","preview.png",popupMenu, OnPreviwButtonClicked);
			
			Dialogs.AddImageMenuButton("Apply","apply.png",popupMenu, OnButtonApplyClicked);
			
			Dialogs.AddImageMenuButton("Go convert...","run.png",popupMenu, OnButtonGoConvertClicked);
			Dialogs.AddImageMenuButton("Show ffmpeg log ...","log.png",popupMenu, OnShowLogActivated);
		}
		
		
		// scheme

		if (MoviesInfo.Count>0)
		{
			popupMenu.Append(new SeparatorMenuItem());

			var schemeSubMenu = new Gtk.Menu();			
			
			var menuItemScheme = Dialogs.AddImageMenuButton("Scheme..",null,popupMenu, null);

			menuItemScheme.Submenu = schemeSubMenu;
			
			// loading schemes:
			var schemesPath = System.IO.Path.Combine(SupportMethods.AppPath,"Schemes/");
			var schemes = Directory.GetFiles(schemesPath,"*.xml");	
			
			foreach (var sch in schemes)
			{
				Dialogs.AddImageMenuButton(System.IO.Path.GetFileNameWithoutExtension(sch),null,schemeSubMenu, OnSchemeChange, sch);
			}			
			
			schemeSubMenu.Append(new SeparatorMenuItem());

			Dialogs.AddImageMenuButton("Import scheme from xml",null,schemeSubMenu, OnImportShcemeActionActivated);
			Dialogs.AddImageMenuButton("Save scheme to xml",null,schemeSubMenu, OnButtonSaveSchemeActivated);
		}				

		// remove 

		if (MoviesInfo.Count>0)
		{
			popupMenu.Append(new SeparatorMenuItem());

			var removeSubMenu = new Gtk.Menu();
			var menuItemRemove = Dialogs.AddImageMenuButton("Remove","remove.png",popupMenu, null);

			menuItemRemove.Submenu = removeSubMenu;

			if (selectedMovies.Count >0)			
			{
				Dialogs.AddImageMenuButton("Selected",null,removeSubMenu, OnButtonRemoveClicked);
			}

			Dialogs.AddImageMenuButton("All",null,removeSubMenu, OnButtonRemoveAllClicked);
		}


		popupMenu.ShowAll();
		popupMenu.Popup();
	}
	
	private int OnSchemeChange(string schemeFileName)
	{
		var selectedMovies = GetSelectedMediaFiles();
		if (selectedMovies.Count == 0)
		{
			return 0;
		}

		widgetTargetMovieTrack.MovieInfo.OpenSchemeFromXML(schemeFileName);
		widgetTargetMovieTrack.Fill();
		
		widgetTargetContainer.Info.OpenSchemeFromXML(schemeFileName);
		widgetTargetContainer.Fill();

		widgetTargetAudioTracks.Info.OpenSchemeFromXML(schemeFileName);
		widgetTargetAudioTracks.Fill();		
	
		return 1;
	}

	[GLib.ConnectBefore]
	protected void OnTreeButtonPressEvent (object o, ButtonPressEventArgs args)
	{
		if(args.Event.Button == 3) 
		{
			CreatePopupMenu();
		}

		base.OnButtonPressEvent(args.Event);
	}


	protected void OnMenuActionActivated (object sender, EventArgs e)
	{

	}

	protected void OnBtnMenuClicked (object sender, EventArgs e)
	{
		CreatePopupMenu();
	}


	protected void OnActionApplyActivated (object sender, EventArgs e)
	{
		OnButtonApplyClicked(this,null);
	}

	protected void OnActionQuitActivated (object sender, EventArgs e)	
	{
		Application.Quit();
	}
	
	protected void OnActionAddFileActivated (object sender, EventArgs e)
	{
		OnButtonAddClicked(this,null);
	}
	
	protected void OnActionAddFolderActivated (object sender, EventArgs e)
	{
		OnButtonAddFolderClicked(this,null);
	}	

	protected void OnActionPlayActivated (object sender, EventArgs e)
	{
		OnButtonApplyClicked(this,null);
	}


	protected void OnActionGoConvertActivated (object sender, EventArgs e)
	{
		OnButtonGoConvertClicked(this,null);
	}	
	
	protected void OnActionAdvancedActivated (object sender, EventArgs e)
	{
		CreatePopupMenu();
	}
	#endregion

}
