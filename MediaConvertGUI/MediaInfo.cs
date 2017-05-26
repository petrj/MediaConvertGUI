using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace MediaConvertGUI
{

	public class MediaInfo : MediaInfoBase
	{
		#region fileds && properties

		public bool EditResolution { get; set; }
		public bool EditRotation { get; set; }
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
		
		public bool AutoRotate { get; set; }

		public string RawMediaInfoOutput { get; set; }

		private MediaCodec _targetVideoCodec=  MediaConvertGUIConfiguration.GetVideoCodecByName("none");
		public MediaCodec TargetVideoCodec
		{ 
			get { return _targetVideoCodec == null ? MediaConvertGUIConfiguration.GetVideoCodecByName("none") : _targetVideoCodec; }
			set 
			{ 
				if (_targetVideoCodec != value) NotifyChange("TargetVideoCodec",value);
				_targetVideoCodec = value;
			}
		}

		private MediaContainer _targetContainer = MediaConvertGUIConfiguration.GetContainerByName("none");
		public MediaContainer TargetContainer
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
			EditResolution = true;
			EditRotation = true;
			EditAspect = true;
			EditBitRate = true;
			EditFrameRate = true;
		}

		#region methods

		public override bool IsChanged ()
		{
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

		public void Clear()
		{
			TargetVideoCodec = MediaConvertGUIConfiguration.GetVideoCodecByName("copy");
			TargetContainer = MediaConvertGUIConfiguration.GetContainerByName ("avi");
			AutoRotate = false;
			Tracks.Clear();
			FileSize = 0;
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

		public void ScreenShot()
		{
			if (File.Exists(FileName))
			{
				var fName = MediaInfoBase.MakeFFMpegScreenShot(this);

				if (fName != null)
				{
					SupportMethods.ExecuteInShell("\"" + fName + "\"");
				} 
			}
		}

		public void PlayInShell()
		{
			if (File.Exists(FileName))
			{
				SupportMethods.ExecuteInShell("\"" + FileName + "\"");
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
			mInfo.EditAspect = EditAspect;
			mInfo.EditBitRate = EditBitRate;
			mInfo.EditFrameRate = EditFrameRate;
			mInfo.EditResolution = EditResolution;
			mInfo.EditRotation = EditRotation;
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

				Tracks.Clear();

				TargetContainer = MediaInfoBase.DetectContainerByExt(fileName);

				var mediaInfoXML = SupportMethods.ExecuteAndReturnOutput(MediaConvertGUIConfiguration.MediaInfoPath,"-f --Output=XML \"" + fileName + "\"");
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




		public void OpenSchemeFromXML(string fileName)
		{
			// http://stackoverflow.com/questions/243022/parsing-through-xml-elements-in-xmlreader

			var xmlDoc = new XmlDocument();
			xmlDoc.Load(fileName);
			var xmlRoot = xmlDoc.DocumentElement;

			foreach (XmlNode item in xmlRoot.SelectNodes(@"/MultimediaScheme"))
			{
				MediaContainer container;

				var node = item.SelectSingleNode("Container");
				if ((node != null) &&
				    (node.FirstChild != null)) 
				{
					container = MediaConvertGUIConfiguration.GetContainerByName (node.FirstChild.Value);
					if (container != null) TargetContainer =  container;
				}
			}			

			var firstVideoTrack =  FirstVideoTrack;

			if (firstVideoTrack != null) 
			{
				foreach (XmlNode item in xmlRoot.SelectNodes(@"/MultimediaScheme/Video"))
				{
					int width;
					var widthNode = item.SelectSingleNode("Width");
					if ((widthNode!= null) && (widthNode.FirstChild != null))
					{
						if (int.TryParse(widthNode.FirstChild.Value,out width))					
							firstVideoTrack.Width = width;
					}

					int height;
					var heightNode = item.SelectSingleNode("Height");
					if ((heightNode!= null) && (heightNode.FirstChild != null))
					{
						if (int.TryParse(heightNode.FirstChild.Value,out height))
							firstVideoTrack.Height = height;
					}

					int bitrate;
					var bitrateNode = item.SelectSingleNode("Bitrate");
					if ((bitrateNode!= null) && (bitrateNode.FirstChild != null))
					{
						if (int.TryParse(bitrateNode.FirstChild.Value,out bitrate))
							firstVideoTrack.Bitrate = bitrate;
					}

					decimal framerate;
					var framerateNode = item.SelectSingleNode("Framerate");
					if ((framerateNode!= null) && (framerateNode.FirstChild != null))
					{
						if (decimal.TryParse(framerateNode.FirstChild.Value,out framerate))
							firstVideoTrack.FrameRate = framerate;
					}

					var aspectNode = item.SelectSingleNode("Aspect");
					if ((aspectNode!= null) && (aspectNode.FirstChild != null))
					{
						if (Regex.IsMatch(aspectNode.FirstChild.Value,"^[0-9]+:[0-9]+$"))
						{
							firstVideoTrack.Aspect = aspectNode.FirstChild.Value;
						}
					}

					var codecNode = item.SelectSingleNode("Codec");
					if ((codecNode!= null) && (codecNode.FirstChild != null))
					{
						var codec = MediaConvertGUIConfiguration.GetVideoCodecByName (codecNode.FirstChild.Value);

						if (codec != null)
							this.TargetVideoCodec = codec;
					}
				}
			}

			var firstAudioTrack =  FirstAudioTrack;

			if (this.AudioTracks.Count>0) 
			{
				var actualTrackIndex = 1;
				foreach (XmlNode trackNode in xmlRoot.SelectNodes(@"/MultimediaScheme/Audio/Track"))
				{
					var codecNode = trackNode.SelectSingleNode("Codec");
					if ((codecNode!= null) && (codecNode.FirstChild != null))
					{
						var aCodec = MediaConvertGUIConfiguration.GetAudioCodecByName (codecNode.FirstChild.Value);
						if (aCodec != null)
							AudioTracks[actualTrackIndex].TargetAudioCodec = aCodec;
					}

					int channels;
					var channelsNode = trackNode.SelectSingleNode("Channels");
					if ((channelsNode!= null) && (channelsNode.FirstChild != null))
					{
						if (int.TryParse(channelsNode.FirstChild.Value,out channels))
							AudioTracks[actualTrackIndex].Channels = channels;
					}

					int bitrate;
					var bitrateNode = trackNode.SelectSingleNode("Bitrate");
					if ((bitrateNode!= null) && (bitrateNode.FirstChild != null))
					{
						if (int.TryParse(bitrateNode.FirstChild.Value,out bitrate))
							AudioTracks[actualTrackIndex].Bitrate = bitrate;
					}

					decimal sRate;
					var sRateNode = trackNode.SelectSingleNode("SamplingRate");
					if ((sRateNode!= null) && (sRateNode.FirstChild != null))
					{
						if (decimal.TryParse(sRateNode.FirstChild.Value,out sRate))
							AudioTracks[actualTrackIndex].SamplingRateHz = sRate;
					}

					actualTrackIndex ++;
					if (actualTrackIndex > AudioTracks.Count)
					{
						break;
					}

				}
			}

		}

		public void SaveAsSchemeToXML(string fileName)
		{
			// Create a new XmlTextWriter instance
			XmlTextWriter writer = new 
				XmlTextWriter(fileName, Encoding.UTF8);

			writer.Formatting = Formatting.Indented;
			writer.Indentation = 4;

			// start writing!
			writer.WriteStartDocument();
			writer.WriteStartElement("MultimediaScheme");

			writer.WriteElementString("Container", TargetContainer.Name);				

			// Video

			writer.WriteStartElement("Video");						

				writer.WriteElementString("Codec", TargetVideoCodec.ToString());

				if  (TargetVideoCodec != MediaConvertGUIConfiguration.GetVideoCodecByName("none"))
				{
					var firstVideoTrack =  FirstVideoTrack;

					if (firstVideoTrack != null) 
					{
						if (EditResolution) 
						{
							writer.WriteElementString("Width", firstVideoTrack.Width.ToString());
							writer.WriteElementString("Height", firstVideoTrack.Height.ToString());
						}				    

						if (EditAspect) writer.WriteElementString("Aspect", firstVideoTrack.Aspect);
						if (EditBitRate) writer.WriteElementString("Bitrate", firstVideoTrack.Bitrate.ToString());
						if (EditFrameRate) writer.WriteElementString("Framerate", firstVideoTrack.FrameRate.ToString(System.Globalization.CultureInfo.InvariantCulture));
					}
				}

			writer.WriteEndElement();

			// Audio

			writer.WriteStartElement("Audio");

				foreach (var track in Tracks)
				{
					if (track.TrackType == "Audio")
					{
						writer.WriteStartElement("Track");

							writer.WriteElementString("Codec", track.TargetAudioCodec.ToString());

							if  ((track.TargetAudioCodec.Name != "none") && (track.TargetAudioCodec.Name != "copy") )
							{
								writer.WriteElementString("Channels", track.Channels.ToString());
								writer.WriteElementString("Bitrate", track.Bitrate.ToString());
								writer.WriteElementString("SamplingRate", track.SamplingRateHz.ToString());
							}
							
						writer.WriteEndElement();
					}
				}


			writer.WriteEndElement();

			writer.WriteEndDocument();
			writer.Close();    
		}

		#endregion
	}
}

