using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace MediaConvertGUI
{

	public class MediaInfo : MediaInfoBase
	{
		#region fileds && properties

		public bool EditResolution { get; set; }
		public bool EditAspect { get; set; }
		public bool EditBitRate { get; set; }
		public bool EditFrameRate { get; set; }

		private string _fileName;
		public string  FileName
		{ 
			get { return _fileName; }
			set {
					if (_fileName != value) NotifyChange("FileName",value);
					_fileName = value; 					
				}
		}

		private long _fileSize = 0;
		public long FileSize 
		{ 
			get { return _fileSize; }
			set 
			{ 
				if (_fileSize != value) NotifyChange("FileSize",value);
				_fileSize = value;
			}
		}

		public Dictionary<string,MediaInfoScheme> Schemes = new Dictionary<string,MediaInfoScheme> ();
		private string _selectedScheme = "none";
		public string SelectedScheme
		{
			get 
			{
				return _selectedScheme;
			}
			set 
			{
				if (_selectedScheme != value) NotifyChange("SelectedScheme",value);
				_selectedScheme = value;
			}
		}

		private List<TrackInfo> _tracks = new List<TrackInfo>();
		public List<TrackInfo> Tracks
		{
			get { return _tracks; }
			set 
			{ 
				if (_tracks != value) NotifyChange("Tracks",value);
				_tracks = value;
			}
		}

		#region properties-getters

		private TrackInfo _firstVideoTrack = null;
		public TrackInfo FirstVideoTrack
		{
			get
			{
				if (_firstVideoTrack!=null) 
					return _firstVideoTrack;

				foreach (var track in Tracks)
				{
					if (track.TrackType == "Video")
					{
						//_firstVideoTrack = track;
						return track;
					}
				}

				return null;
			}
		}

		private TrackInfo _firstAudioTrack = null;
		public TrackInfo FirstAudioTrack
		{
			get
			{
				if (_firstAudioTrack!=null) 
					return _firstAudioTrack;

				foreach (var track in Tracks)
				{
					if (track.TrackType == "Audio")
					{
						//_firstAudioTrack = track;
						return track;
					}
				}

				return null;
			}
		}

		public Dictionary<int,TrackInfo>AudioTracks
		{
			get
			{
				var res = new Dictionary<int,TrackInfo>();
				var i=1;
				foreach (var track in Tracks)
				{
					if (track.TrackType == "Audio")
					{
						res.Add(i,track);
						i++;
					}
				}

				return res;
			}
		}

		/// <summary>
		/// Gets the over all bit rate in ms
		/// Computed from DurationMS and FileSize
		/// </summary>
		/// <value>
		/// The over all bit rate.
		/// </value>
		public decimal OverAllBitRate
		{
			get
			{
				if (DurationMS == 0)
					return 0;

				decimal totalSeconds = Math.Round(DurationMS/(decimal)1000);
				decimal sizekBits = FileSize*8/(decimal)1000.00;

				return sizekBits / totalSeconds;
			}
		}

		public string HumanReadableOverAllBitRate
		{
			get
			{
				return Math.Round(OverAllBitRate) + " kpbs";
			}
		}

		// duration detected from tracks
		public decimal DurationMS
		{
			get
			{
				decimal dur = 0;
				foreach (var track in Tracks)
				{
					if (track.DurationMS>0) 
					{
						dur = track.DurationMS;
						break;
					}					
				}

				return dur;
			}
		}

		public string HuamReadableDuration
		{
			get
			{
				return SupportMethods.HuamReadableDuration(DurationMS);
			}
		}

		public string HumanReadableSize
		{
			get
			{
				return SupportMethods.HumanReadableSize(FileSize);
			}
		}

		#endregion

		public string RawMediaInfoOutput { get; set; }

		private VideoCodecEnum _targetVideoCodec  = VideoCodecEnum.none;
		public VideoCodecEnum TargetVideoCodec
		{ 
			get { return _targetVideoCodec; }
			set 
			{ 
				if (_targetVideoCodec != value) NotifyChange("TargetVideoCodec",value);
				_targetVideoCodec = value;
			}
		}

		private VideoContainerEnum _targetContainer = VideoContainerEnum.none;
		public VideoContainerEnum TargetContainer
		{ 
			get { return _targetContainer; }
			set 
			{ 
				if (_targetContainer != value) NotifyChange("TargetContainer",value);
				_targetContainer = value;
			}
		}

		public string FFMPEGOutputFileName { get; set; }
		public string FFMPEGPassLogFileName { get; set; }

		#endregion

		public MediaInfo ()
		{
			LoadSchemes();

			EditResolution = true;
			EditAspect = true;
			EditBitRate = true;
			EditFrameRate = true;
		}

		#region methods

		public override bool IsChanged ()
		{
			var anyTrackChanged = false;
			foreach (var track in Tracks)
			{
				if (track.IsChanged())
				{
					return true;
				}
			}

			return _changed;
		}

		public override void UnChanged()
		{
			_changed = false;
			foreach (var track in Tracks)
			{
				track.UnChanged();
			}
		}

		private void LoadSchemes()
		{
			Schemes.Clear();

			Schemes.Add( "Flash video", new MediaInfoScheme(VideoContainerEnum.flv, VideoCodecEnum.flv,AudioCodecEnum.mp3) );
			Schemes.Add( "Ogg video", new MediaInfoScheme(VideoContainerEnum.ogg, VideoCodecEnum.theora,AudioCodecEnum.vorbis) );
			Schemes.Add( "WebM video", new MediaInfoScheme(VideoContainerEnum.webm, VideoCodecEnum.vp8,AudioCodecEnum.vorbis) );
			Schemes.Add( "3GP (h264)", new MediaInfoScheme(VideoContainerEnum._3gp, VideoCodecEnum.h264,AudioCodecEnum.aac) );
		}

		public void Clear()
		{
			TargetVideoCodec = VideoCodecEnum.xvid;
			TargetContainer = VideoContainerEnum.avi;
			Tracks.Clear();
		}

		public void AppendTracksTo(MediaInfo mInfo,decimal durationMS, string onlyType="")
		{
			foreach(var track in Tracks)
			{
				if ((onlyType=="") || ((track.TrackType ==onlyType)) )				
				{
					var tr = new TrackInfo();
					track.CopyTo(tr);
					tr.DurationMS = durationMS;
					mInfo.Tracks.Add(tr);
				}
			}
		}

		public void Copyto(MediaInfo mInfo,bool videoOnly)
		{
			mInfo.ClearTracks();
			foreach(var track in Tracks)
			{
				if ((videoOnly==false) || ((track.TrackType =="Video")) )				
				{
					var tr = new TrackInfo();
					track.CopyTo(tr);
					mInfo.Tracks.Add(tr);
				}
			}
			mInfo.TargetVideoCodec = TargetVideoCodec;
			mInfo.TargetContainer = TargetContainer;
			mInfo.FileName = FileName;
			mInfo.FileSize = FileSize;
			mInfo.SelectedScheme = SelectedScheme;
			mInfo.EditAspect = EditAspect;
			mInfo.EditBitRate = EditBitRate;
			mInfo.EditFrameRate = EditFrameRate;
			mInfo.EditResolution = EditResolution;
		}

		public void ClearTracks()
		{
			Tracks.Clear();
		}

		/// <summary>
		/// Opens from file.
		/// </summary>
		/// <param name='fileName'>
		/// File name.
		/// </param>
		public bool OpenFromFile(string fileName)
		{
			try
			{
				if (!File.Exists(fileName))
					return false;

				FileName = fileName;
				var fi = new System.IO.FileInfo(fileName);
				FileSize = fi.Length;

				var raw = String.Empty;
				Tracks.Clear();

				TargetContainer = MediaInfoBase.DetectContainerByExt(fileName);

				var mediaInfoXML = SupportMethods.ExecuteAndReturnOutput("mediainfo","-f --Output=XML \"" + fileName + "\"");
				RawMediaInfoOutput = mediaInfoXML;			

				var xmlDoc = new System.Xml.XmlDocument();
				xmlDoc.LoadXml(mediaInfoXML);			        		
	       		 
				 var nodes =  xmlDoc.SelectNodes("Mediainfo/File/track");
				foreach (XmlNode node in nodes)
				{
					var track = new TrackInfo();
					track.ParseFromXmlNode(node);
					Tracks.Add(track);
				}

				return true;

			} catch (Exception ex)
			{
				Console.WriteLine ("Error:"+ex.ToString());
				return false;
			}		
		}

		#endregion
	}
}

