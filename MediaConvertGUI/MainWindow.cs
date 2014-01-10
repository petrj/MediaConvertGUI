using System;
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
		Build ();

		TestPrerequisites();

		_fileTreeViewData = new TreeViewData(tree); 
		CreateGridColumns();
		widgetTargetMovieTrack.Editable = true;

		_proressWindow = new ProgressWin();
		_proressWindow.Hide();

		tree.Selection.Mode = SelectionMode.Multiple;

		widgetGenera.SchemeChanged += OnSchemeChanged;

		tree.CursorChanged += OnTreeCursorChanged;

		buttonAdd.Clicked+=OnButtonAddClicked;
		buttonAddFolder.Clicked+=OnButtonAddFolderClicked;
		buttonRemove.Clicked+=OnButtonRemoveClicked;
		buttonRemoveAll.Clicked+=OnButtonRemoveAllClicked;

		buttonApply.Clicked+=OnButtonApplyClicked;
		buttonGoConvert.Clicked+=OnButtonGoConvertClicked;
		buttonPreview.Clicked += OnPreviwButtonClicked;

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
			var ffmpegOutput = SupportMethods.ExecuteAndReturnOutput ("ffmpeg", "-version");
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
			var mediaInfoOutput = SupportMethods.ExecuteAndReturnOutput ("mediainfo", "--version");
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

		_fileTreeViewData.AppendStringColumn("File Name", null, false); 
		_fileTreeViewData.AppendStringColumn("Target Codec", null, false);
		_fileTreeViewData.AppendStringColumn("Container", null, false);
		_fileTreeViewData.AppendStringColumn("Audio", null, false);
		_fileTreeViewData.CreateTreeViewColumns();
		
	}

    public void FillTree()
    {
	   _fileTreeViewData.Data.Clear();

		foreach(var info in MoviesInfo.Keys)
		{
			var name = info.FileName; 
			var codec = MoviesInfo[info].TargetVideoCodec.ToString();
			var cont = MoviesInfo[info].TargetContainer.ToString();
			var audio = MoviesInfo[info].FirstAudioTrack != null ?MoviesInfo[info].FirstAudioTrack.TargetAudioCodec.ToString() : "none";
				MoviesInfo[info].TargetContainer.ToString();
			_fileTreeViewData.AppendData(new List<object>{name,codec,cont,audio});
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
					targetMovie.TargetContainer = VideoContainerEnum.avi;
					targetMovie.TargetVideoCodec = VideoCodecEnum.xvid;
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
						targetMovie.FirstAudioTrack.TargetAudioCodec = AudioCodecEnum.copy;
					} else
					{
						targetMovie.FirstAudioTrack.TargetAudioCodec = AudioCodecEnum.mp3;
					}
				}

				MoviesInfo.Add (sourceMovie, targetMovie);

				FillTree ();
			}
		}	
	}

	#endregion

	#region progess

	public void ShowProgess()
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

		public static string MakeFFMpegCommand(MediaInfo sourceMovie, MediaInfo targetMovie, int currentPass)
		{		
				// single audio convert? 
				if ( (targetMovie.AudioTracks.Count > 0) && 
		    		 (targetMovie.FirstAudioTrack.TargetAudioCodec!= AudioCodecEnum.none) &&
		    		 ( (targetMovie.FirstVideoTrack == null) || (targetMovie.TargetVideoCodec == VideoCodecEnum.none)) &&
		      		  (currentPass>1))
				{
					return string.Empty;					
				} 		

				var res= String.Empty;

				var sourceFile = " -i \"" + sourceMovie.FileName+"\"";				
				var ext = System.IO.Path.GetExtension(sourceMovie.FileName);
				var targetFile = sourceMovie.FileName+".converted" + ext;
				var video = " -vn";  // disable video								

				var audio = " -an "; // disable audio				

				var map = String.Empty;

				if (targetMovie.FirstVideoTrack != null && targetMovie.TargetVideoCodec!=VideoCodecEnum.none)
				{
					var videoSettings= String.Empty;					
					var container = String.Empty;														

					switch (targetMovie.TargetContainer)
					{
						case  VideoContainerEnum.avi : container = " -f avi"; ext = ".avi"; break;
						case  VideoContainerEnum.flv : container = " -f flv"; ext = ".flv"; break;
						case  VideoContainerEnum.mp4 : container = " -f mp4"; ext = ".mp4"; break;
						case  VideoContainerEnum.mpeg : container = " -f mpeg"; ext = ".mpeg"; break;
						case  VideoContainerEnum.ogg : container = " -f ogg"; ext = ".ogv"; break;
						case  VideoContainerEnum.mkv : container = " "; ext = ".mkv"; break;
						case  VideoContainerEnum.webm : container = " -f webm "; ext = ".webm"; break;
						case  VideoContainerEnum._3gp : container = " -f 3gp "; ext = ".3gp"; break;
					}
					targetFile = sourceMovie.FileName+".converted" + ext;
					
					var aspect = " -aspect " + targetMovie.FirstVideoTrack.Aspect;
					var	scale =   " -s " + targetMovie.FirstVideoTrack.Width.ToString() + "x"+targetMovie.FirstVideoTrack.Height.ToString();
					var	bitrate = " -b:v " + targetMovie.FirstVideoTrack.Bitrate;	

					var pass = String.Format(" -pass {0} -passlogfile \"{1}\"",currentPass,targetFile + ".passlog");

					videoSettings = aspect + scale + bitrate + container + pass;	
					
					switch (targetMovie.TargetVideoCodec)
					{
						case VideoCodecEnum.copy: video = " -vcodec copy"; break;
						case VideoCodecEnum.xvid: video = " -vcodec libxvid"+videoSettings; break;
						case VideoCodecEnum.flv: video = " -vcodec flv"+videoSettings; break;
						case VideoCodecEnum.h263: video = " -vcodec h263"+videoSettings; break;
						case VideoCodecEnum.h264: video = " -vcodec h264"+videoSettings; break;
						case VideoCodecEnum.mpeg: video = " -vcodec mpeg1video"+videoSettings; break;
						case VideoCodecEnum.theora: video = " -vcodec theora"+videoSettings; break;
						case VideoCodecEnum.vp8: video = " -vcodec libvpx"+videoSettings; break;
					}
				}


			// only first Audio Track!
			if (targetMovie.AudioTracks.Count>0)
			{
				var targetAudioTrack = targetMovie.FirstAudioTrack;

				var audioQuality = String.Format(" -ac {0} -ar {1} -ab {2}",	
					                          targetAudioTrack.Channels,
					                          targetAudioTrack.SamplingRateHz,
					                          targetAudioTrack.Bitrate);
							

					switch (targetAudioTrack.TargetAudioCodec)
					{
						case AudioCodecEnum.copy:audio = " -acodec copy "; break;
						case AudioCodecEnum.mp3: audio = String.Format(" -acodec libmp3lame"+audioQuality); ext = ".mp3"; break;
						case AudioCodecEnum.vorbis: audio = String.Format(" -acodec libvorbis "+audioQuality); ext = ".ogg"; break;
						case AudioCodecEnum.aac: audio = String.Format(" -acodec libfaac "+audioQuality); ext = ".aac"; break;
						case AudioCodecEnum.flac: audio = String.Format(" -acodec flac "+audioQuality); ext = ".flac"; break;
						case AudioCodecEnum.ac3: audio = String.Format(" -acodec ac3 "+audioQuality); ext = ".ac3"; break;
						default: audio = " -an "; break;
					}


					if ( (targetMovie.FirstVideoTrack == null) || (targetMovie.TargetVideoCodec == VideoCodecEnum.none))
					{
						// converting single audio
						targetFile = sourceMovie.FileName+".converted" + ext;
					}
			}		

			// more audio tracks? supporting only the first one					
			if (sourceMovie.AudioTracks.Count > 1 && sourceMovie.FirstVideoTrack != null)
			{
				map = " -map 0:0 -map 0:1";
			} 		

			targetMovie.FFMPEGOutputFileName = targetFile + ".log";
			targetMovie.FFMPEGPassLogFileName = targetFile + ".passlog";					
			targetMovie.FileName = targetFile;

			if (File.Exists(targetMovie.FFMPEGOutputFileName))
					File.Delete(targetMovie.FFMPEGOutputFileName);
			if (File.Exists(targetMovie.FFMPEGPassLogFileName))
					File.Delete(targetMovie.FFMPEGPassLogFileName);			

			targetFile = String.Format(" \"{0}\"",targetFile);

			res = "ffmpeg -y -dump " + sourceFile + map + video + audio + targetFile;

			return res;
		}
	

	public void RunCommandList()
	{
		_currentConvertingMovie = null;
		_processAbortRequest = false;

		_processthread = new Thread(ThreadMethod);
		_processStartedAt = DateTime.Now;
		_processthread.Start();

 		buttonGoConvert.Sensitive = false;

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

			var cmd1 =  MakeFFMpegCommand(kvp.Key,kvp.Value,1);
			ExecutFFMpegCommand(cmd1, kvp.Value.FFMPEGOutputFileName );

			if (info!=null &&
			    _currentConvertingMovie.TargetVideoCodec!=VideoCodecEnum.none)
			{
				// converting 2 pass video
				var cmd2 =  MakeFFMpegCommand(kvp.Key,kvp.Value,2);

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
			buttonGoConvert.Sensitive = true;
			widgetTargetMovieTrack.Editable = true;
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

			var output = SupportMethods.ExecuteAndReturnOutput (processToExecute, parametres);
		}        

	#endregion
     
	#region events

	protected void OnSchemeChanged(object sender, EventArgs e)
	{

		if (widgetGenera.TargetMovieInfo == null || 
		    widgetTargetAudioTracks.Info == null || 
		    widgetTargetMovieTrack.MovieInfo == null)
			return;	

		var selectedScheme = widgetGenera.TargetMovieInfo.SelectedScheme;
		if (widgetGenera.TargetMovieInfo.SelectedScheme != "none" && widgetGenera.TargetMovieInfo.Schemes.ContainsKey(widgetGenera.TargetMovieInfo.SelectedScheme))
		{
			var scheme = widgetGenera.TargetMovieInfo.Schemes[widgetGenera.TargetMovieInfo.SelectedScheme];
			widgetTargetMovieTrack.MovieInfo.TargetVideoCodec = scheme.VideoCodec;
			widgetTargetMovieTrack.MovieInfo.TargetContainer = scheme.Container;
			widgetGenera.TargetMovieInfo.SelectedScheme = widgetGenera.TargetMovieInfo.SelectedScheme;
			if (widgetTargetAudioTracks.Info.FirstAudioTrack != null)
			{
				widgetTargetAudioTracks.Info.FirstAudioTrack.TargetAudioCodec = scheme.AudioCodec;
			}

			OnButtonApplyClicked(this,null);
		}
	}

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

		widgetSourceMovieTrack.FillFrom(source);
		widgetSourceAudioTracks.FillFrom(source);

		MediaInfo target;
		target = source !=null && MoviesInfo.ContainsKey(source) ? MoviesInfo[source] : null;
		widgetTargetMovieTrack.FillFrom(target);
		widgetTargetAudioTracks.FillFrom(target);

		widgetGenera.FillFrom(source,target);
	}

	protected void OnButtonGoConvertClicked (object sender, EventArgs e)
	{
		RunCommandList();
		ShowProgess();
	}	

	protected void OnPreviwButtonClicked (object sender, EventArgs e)
	{
		// creating preview string
		var commands = new StringBuilder();

		foreach (var kvp in MoviesInfo)
		{
			commands.Append(MakeFFMpegCommand(kvp.Key,kvp.Value,1));
			commands.Append(System.Environment.NewLine);

			commands.Append(MakeFFMpegCommand(kvp.Key,kvp.Value,2));
			commands.Append(System.Environment.NewLine);
		}

		using (var tw  = new TextWin())
		{
			tw.Title = "FFmpeg command preview";
			tw.Text = commands.ToString();
			tw.Show();
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
		// creating supported extensions dictionary
		var supportedExtensions = new Dictionary<string,string>();
		foreach (var val in Enum.GetNames(typeof(VideoContainerEnum))) 
				supportedExtensions.Add("." + val.ToUpper(),null);

		var dirName = Dialogs.OpenDirectoryDialog("Open all files in directory:");
		if (dirName != null)
		{
			foreach (var fName in Directory.GetFiles(dirName))
			{
				if (File.Exists(fName))
				{
					var ext = System.IO.Path.GetExtension(fName).ToUpper();
					if (supportedExtensions.ContainsKey(ext))
					{
						AddMediaInfo(fName);
					}
				}
			}
		}
	}

	protected void OnButtonRemoveAllClicked (object sender, EventArgs e)
	{
		MoviesInfo.Clear();
		FillTree();
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
				MoviesInfo[m.Value].TargetContainer = widgetTargetMovieTrack.MovieInfo.TargetContainer;

				if (widgetGenera.TargetMovieInfo != null)
				{
					MoviesInfo[m.Value].SelectedScheme = widgetGenera.TargetMovieInfo.SelectedScheme;
				}

				var tmpDuration = MoviesInfo[m.Value].DurationMS;
				MoviesInfo[m.Value].ClearTracks();
				widgetTargetMovieTrack.MovieInfo.AppendTracksTo(MoviesInfo[m.Value],tmpDuration,"Video");
				widgetTargetAudioTracks.Info.AppendTracksTo(MoviesInfo[m.Value],tmpDuration,"Audio");
			}
		//}

		FillTree();

		SelectRows(selectedMovies.Keys);
		OnTreeCursorChanged(this,null);

	}

	#endregion

}
