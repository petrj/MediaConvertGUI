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

		_fileTreeViewData = new TreeViewData(tree); 
		CreateGridColumns();
		widgetTargetMovieTrack.Editable = true;

		_proressWindow = new ProgressWin();
		_proressWindow.Hide();

		tree.Selection.Mode = SelectionMode.Multiple;

		widgetGenera.SchemeChanged += OnSchemeChanged;

		buttonAdd.Clicked+=OnButtonAddClicked;
		buttonAddFolder.Clicked+=OnButtonAddFolderClicked;
		buttonRemove.Clicked+=OnButtonRemoveClicked;
		buttonRemoveAll.Clicked+=OnButtonRemoveAllClicked;

		buttonApply.Clicked+=OnButtonApplyClicked;
		buttonGoConvert.Clicked+=OnButtonGoConvertClicked;

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
		sourceMovie.OpenFromFile(fName);

		if (sourceMovie.AudioTracks.Count>1 || sourceMovie.FirstVideoTrack!=null)
		{

			var targetMovie = new MediaInfo();
			sourceMovie.Copyto(targetMovie,true);

			// tawidgetGenerarack
			if (sourceMovie.AudioTracks.Count>0)
			{
				var audioTrack = new TrackInfo();
	                audioTrack.TrackType = "Audio";
	                audioTrack.Channels = 2;
	                audioTrack.Bitrate = 192000;
	                audioTrack.SamplingRateHz = 44100;
	                audioTrack.DurationMS = sourceMovie.DurationMS;
	                targetMovie.Tracks.Add(audioTrack);
			}

			MoviesInfo.Add(sourceMovie, targetMovie);

			FillTree();
		}	
	}

	#endregion

	#region progess

	/// <summary>
	/// Gets the last frame from convert log file.
	/// Parsing text "frame=15" to 15
	/// </summary>
	/// <returns>
	/// The last frame from convert log file (int).
	/// If value not found, returns -1
	/// </returns>
	/// <param name='_outputFileName'>
	/// _output file name.
	/// </param>
	private int GetLastFrameFromConvertLogFile(string _outputFileName)
	{
		var lastFrame = -1;

		if (File.Exists(_outputFileName))
			{
				using (var fs = new FileStream(_outputFileName,FileMode.Open,FileAccess.Read))
				{
					using (var sr = new StreamReader(fs)) 
					{					    
						string line;
										 while ((line = sr.ReadLine()) != null)
                                         {
                                                        if (line != null && line.Length>11 && line.StartsWith("frame="))
                                                        {
                                                                var lastFrameAsString = line.Substring(6,5).Trim();
                                                                if (SupportMethods.IsNumeric(lastFrameAsString))
                                                                        lastFrame = Convert.ToInt32(lastFrameAsString);
                                                        }
                                         }
					}
				}
			}

		return lastFrame;
	}

	public void ShowProgess()
        {
                        _proressWindow.Show();
                        _proressWindow.SetPercents(0,0,0,_processStartedAt);

                        while (_processthread != null && _processthread.IsAlive)
                        {                                        
                                if (_proressWindow.AbortRequest)
                                {
                                        SupportMethods.ExecuteAndReturnOutput("killall","ffmpeg");
                                        _processAbortRequest = true;
                                }

                                var percentsCurrentFilePass = 0d;
                                var percentsCurrentFile = 0d;
                                var percentsTotal = 0d;


                                var fName = String.Empty;
                                var passAsString = String.Empty;
                                var totalFilesAsString = String.Empty;

                                if (_currentConvertingMovie != null)
                                {
                                        fName = System.IO.Path.GetFileName(_currentConvertingMovie.FileName);

                                        var frames = Convert.ToDouble(_currentConvertingMovie.DurationMS/1000m*_currentConvertingMovie.FirstVideoTrack.FrameRate);

                                        // detecting progress from text file
                                        var lastFrame = GetLastFrameFromConvertLogFile(_outputFile);
                                        if ( (frames>0) && (lastFrame != -1))
                                        {
                                                percentsCurrentFilePass = Convert.ToInt32(lastFrame / (frames/(double)100));
                                        }

                                        // computing current file progress
                                        if (_currentPass>0)
                                        {        
                                                var correctedFrame = lastFrame;
                                                if (correctedFrame<0) correctedFrame = 0;

                                                passAsString = "Pass: " + _currentPass.ToString();

                                                var actualFrame = (_currentPass-1)*frames + correctedFrame;
                                                percentsCurrentFile = actualFrame/(frames*2/100d);                                
                                        }

                                }

                                if (_currentFileListNumber>=0 && _currentFileListCount>0)
                                {
                                        percentsTotal = Convert.ToDouble(_currentFileListNumber/(Convert.ToDouble(_currentFileListCount)/100d));

                                        totalFilesAsString = (_currentFileListNumber+1).ToString()+"/"+(_currentFileListCount).ToString();

                                        // adding actual file progress fraction
                                        if (percentsCurrentFile>0)
                                        {
                                                var onePart=1d/(double)_currentFileListCount;
                                                percentsTotal = percentsTotal + percentsCurrentFile*onePart;
                                        }

                                }

                                _proressWindow.SetPercents(Math.Round (percentsTotal,2),
                         Math.Round (percentsCurrentFile,2),
                         Math.Round (percentsCurrentFilePass,2),
                         _processStartedAt,
                         fName,
                         passAsString,
                         totalFilesAsString);
                                /*
                                _proressWindow.CurrentFilePassPercents = percentsCurrentFilePass;
                                _proressWindow.CurrentFilePercents = percentsCurrentFile;
                                _proressWindow.TotalPercents = percentsTotal;
                                */

                                while (GLib.MainContext.Iteration ());
                                Thread.Sleep(500);
                        }

                        _proressWindow.SetPercents(100,
                         100,
                         100,
                         _processStartedAt);

        }

	#endregion

	#region conversion

	public void RunCommandList()
	{
		_currentConvertingMovie = null;

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
			_currentFileListNumber++;

			_currentConvertingMovie = kvp.Value;

			_currentPass = 1;

			if (_processAbortRequest) break;

			var cmd1 =  MediaInfo.MakeFFMpegCommand(kvp.Key,kvp.Value,1);
			ExecutFFMpegCommand(cmd1, kvp.Value.FFMPEGOutputFileName );

			if (_processAbortRequest) break;

			_currentPass = 2;

			var cmd2 =  MediaInfo.MakeFFMpegCommand(kvp.Key,kvp.Value,2);
			ExecutFFMpegCommand(cmd2, kvp.Value.FFMPEGOutputFileName);
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

				var processToExecute = "sh";
				var parametres=String.Format("-c '" + cmd + " 2> {0} '",_outputFile);

				 using(var process = new Process
				    {
				        StartInfo = new ProcessStartInfo
				        {
				            FileName = processToExecute,
							Arguments = parametres,
				            UseShellExecute = true
				        }
				    })
					{
						    process.Start();
						    process.WaitForExit(); 
					}
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
			widgetGenera.TargetMovieInfo.TargetContainer = scheme.Container;
			widgetTargetMovieTrack.MovieInfo.TargetVideoCodec = scheme.VideoCodec;
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

				if (widgetGenera.TargetMovieInfo != null)
				{
					MoviesInfo[m.Value].TargetContainer = widgetGenera.TargetMovieInfo.TargetContainer;
					MoviesInfo[m.Value].SelectedScheme = widgetGenera.TargetMovieInfo.SelectedScheme;
				}


				MoviesInfo[m.Value].ClearTracks();
				widgetTargetMovieTrack.MovieInfo.AppendTracksTo(MoviesInfo[m.Value],"Video");
				widgetTargetAudioTracks.Info.AppendTracksTo(MoviesInfo[m.Value],"Audio");
			}
		//}

		FillTree();

		SelectRows(selectedMovies.Keys);
		OnTreeCursorChanged(this,null);

	}

	#endregion

}
