using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace MediaConvertGUI
{

	#region audio && video enums

	public enum AudioCodecEnum
	{
		none = 0,
		MP3 = 1,
		vorbis = 2
	}

	public enum VideoCodecEnum
	{
		none = 0,
		xvid = 1,
		flv = 2,
		h264 = 3,
		mpeg = 4,
		theora = 5,
		vp8 = 6
	}

	public enum VideoContainerEnum
	{
		avi = 0,
		flv = 1,
		mp4 = 2,
		mkv = 3,
		mpeg = 4,
		ogg = 5,
		webm = 6
	}

	#endregion

	public class MediaInfo
	{
		public static string MakeFFMpegCommand(MediaInfo sourceMovie, MediaInfo targetMovie, int currentPass)
		{		
				var res= String.Empty;
				if (targetMovie.FirstVideoTrack != null)
				{
					var video = " -vcodec copy";
					var container = String.Empty;
					var ext = String.Empty;

					switch (targetMovie.TargetContainer)
					{
						case  VideoContainerEnum.avi : container = " -f avi"; ext = ".avi"; break;
						case  VideoContainerEnum.flv : container = " -f flv"; ext = ".flv"; break;
						case  VideoContainerEnum.mp4 : container = " -f mp4"; ext = ".mp4"; break;
						case  VideoContainerEnum.mpeg : container = " -f mpeg"; ext = ".mpeg"; break;
						case  VideoContainerEnum.ogg : container = " -f ogg"; ext = ".ogv"; break;
						case  VideoContainerEnum.mkv : container = " "; ext = ".mkv"; break;
						case  VideoContainerEnum.webm : container = " -f webm "; ext = ".webm"; break;
					}

					var videContainer = String.Empty;
					var aspect = String.Empty;
					var scale =   String.Empty;
					var bitrate = String.Empty;					

					if (targetMovie.TargetVideoCodec != VideoCodecEnum.none)
					{
						aspect = " -aspect " + targetMovie.FirstVideoTrack.Aspect;
						scale =   " -s " + targetMovie.FirstVideoTrack.Width.ToString() + "x"+targetMovie.FirstVideoTrack.Height.ToString();
						bitrate = " -b:v " + targetMovie.FirstVideoTrack.Bitrate;

						switch (targetMovie.TargetVideoCodec)
						{
							case VideoCodecEnum.xvid: video = " -vcodec libxvid"; break;
							case VideoCodecEnum.flv: video = " -vcodec flv"; break;
							case VideoCodecEnum.h264: video = " -vcodec h264"; break;
							case VideoCodecEnum.mpeg: video = " -vcodec mpeg1video"; break;
							case VideoCodecEnum.theora: video = " -vcodec theora"; break;
							case VideoCodecEnum.vp8: video = " -vcodec libvpx"; break;
						}						

						video += aspect + scale + bitrate;
					}			

					
					
					var sourceFile = " -i \"" + sourceMovie.FileName+"\"";
					var targetFile = String.Format(" \"{0}\"",sourceMovie.FileName+".converted" + ext);
					
					targetMovie.FFMPEGOutputFileName = String.Format("{0}",sourceMovie.FileName+".converted" + ext + ".log");
					targetMovie.FFMPEGPassLogFileName = String.Format(" \"{0}\"",sourceMovie.FileName+".converted" + ext + ".passlog");					
					targetMovie.FileName = String.Format("{0}",sourceMovie.FileName+".converted" + ext);;
					
					if (File.Exists(targetMovie.FFMPEGPassLogFileName))
					{
							File.Delete(targetMovie.FFMPEGPassLogFileName);
					}

					var audio = " -acodec copy";

					if ( (targetMovie.AudioTracks.Count>0) && 
				    	 (targetMovie.FirstAudioTrack != null) &&
				    	 (targetMovie.FirstAudioTrack.TargetAudioCodec != AudioCodecEnum.none))
						{
							var targetAudioTrack = targetMovie.FirstAudioTrack;
							switch (targetAudioTrack.TargetAudioCodec)
							{
								case AudioCodecEnum.MP3: audio = String.Format(" -acodec libmp3lame"); break;
								case AudioCodecEnum.vorbis: audio = String.Format(" -acodec libvorbis"); break;
							}

							audio += String.Format(" -ac {0} -ar {1} -ab {2}",
							                          targetAudioTrack.Channels,
							                          targetAudioTrack.SamplingRateHz,
							                          targetAudioTrack.Bitrate);
					}

					// more audio tracks? supporting only the first one
					var map = String.Empty;
					if (sourceMovie.AudioTracks.Count > 1)
					{
						map = " -map 0:0 -map 0:1";
					} 				

					var pass = String.Format(" -pass {0} -passlogfile {1}",currentPass,targetMovie.FFMPEGPassLogFileName);

					res = "ffmpeg -y -dump " + sourceFile + map + video + container + audio + pass + targetFile;					
				}	

			return res;
		}

		#region static constants

		public static Dictionary<VideoCodecEnum,string> DefaultVideoCodecsDescriptions = new Dictionary<VideoCodecEnum, string>()
		{
			{VideoCodecEnum.xvid,"MPEG-4 ASP libxvid (MPEG-4 part 2)"},
			{VideoCodecEnum.flv,"Sorenson Spark / Sorenson H.263 (Flash Video)"},
			{VideoCodecEnum.h264,"MPEG-4 AVC libx264 (MPEG-4 part 10)"},
			{VideoCodecEnum.mpeg,"MPEG-1 video"},
			{VideoCodecEnum.theora,"Theora libtheora"},
			{VideoCodecEnum.vp8,"VP8 libvpx"},
		};

		public static Dictionary<decimal,string> DefaultVideoBitRates = new Dictionary<decimal, string>()
		{
			{1500m,"VCD (1.5 Mb)"},
			{3500m,"TV  (3.5 Mb)"},		
			{9000m,"DVD (9 Mb)"},
			{15000m,"HDTV (15 Mb)"},
			{30000m,"HD DVD (30 Mb)"}
		};

		public static Dictionary<decimal,string> DefaultSamplingRates = new Dictionary<decimal, string>()
		{

			{08000m,"Telephone (8 kHz)"},
			{11025m,"1/4 Audio-CD (11 kHz)"},
			{22050m,"1/2 Audio-CD (22 kHz)"},
			{44100m,"Audio-CD (44 kHz)"},
			{48000m,"TV (48 kHz)"},		
			{96000m,"DVD-Audio (96 kHz)"}
		};

		public static Dictionary<decimal,string> DefaultAudioBitRates = new Dictionary<decimal, string>()
		{
			{32m,"32"},
			{64m,"64"},		
			{128m,"128"},
		};

		#endregion

		#region fileds && properties

		public string FileName { get; set; }
		public long FileSize { get; set; }

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
				_selectedScheme = value;
			}
		}

		public List<TrackInfo> Tracks  { get; set; }

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

		#region properties-getters
			
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
				var res = "00:00";

				var totalSeconds = Math.Round(DurationMS/(decimal)1000);
				var hours = Math.Truncate(totalSeconds/(decimal)(60*60));
				var minutes = Math.Truncate( (totalSeconds - hours*60*60)/60);
				var seconds = Math.Truncate( (totalSeconds - hours*60*60 -minutes*60 ));

				res = minutes.ToString().PadLeft(2,'0') + ":"+ minutes.ToString().PadLeft(2,'0') + ":" + seconds.ToString().PadLeft(2,'0');

				return res;
			}
		}

		public string HumanReadableSize
		{
			get
			{
				return ShowHumanReadableSize(FileSize);
			}
		}

		#endregion

		public static string ShowHumanReadableSize(long sizeInBytes)
		{
				var res = "0 MB";

				var sizeMB = sizeInBytes/(1000.00*1000.00);
				if (sizeMB>1000)
				{
					var sizeGB = sizeMB/(1000.00);
					sizeGB = Math.Round(sizeGB,1);
					res = sizeGB.ToString()+" GB";
				} else
				{
					sizeMB = Math.Round(sizeMB,1);
					res = sizeMB.ToString()+" MB";
				}

				return res;
		}

		public string RawMediaInfoOutput { get; set; }

		public VideoCodecEnum TargetVideoCodec { get; set; }
		public VideoContainerEnum TargetContainer { get; set; }

		public string FFMPEGOutputFileName { get; set; }
		public string FFMPEGPassLogFileName { get; set; }

		#endregion

		public MediaInfo ()
		{
			Tracks = new List<TrackInfo>();
			TargetVideoCodec = VideoCodecEnum.xvid;
			TargetContainer = VideoContainerEnum.avi;
			LoadSchemes();
		}

		#region methods

		private void LoadSchemes()
		{
			Schemes.Clear();

			Schemes.Add( "Flash video", new MediaInfoScheme(VideoContainerEnum.flv, VideoCodecEnum.flv,AudioCodecEnum.MP3) );
			Schemes.Add( "Ogg video", new MediaInfoScheme(VideoContainerEnum.ogg, VideoCodecEnum.theora,AudioCodecEnum.vorbis) );
			Schemes.Add( "WebM video", new MediaInfoScheme(VideoContainerEnum.webm, VideoCodecEnum.vp8,AudioCodecEnum.vorbis) );
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

	public class TrackInfo
	{
		#region fileds && properties

		public AudioCodecEnum TargetAudioCodec { get; set; }

		public string TrackType { get; set; }
		public string Codec { get; set; }
		public int Channels { get; set; }

		public decimal FrameRate { get; set; }

		public int Width { get; set; }
		public decimal PixelAspect { get; set; }
		public int RealWidth
		{ 
			get
			{
				return Convert.ToInt32(PixelAspect * Width);
			}
		}

		public int Height  { get; set; }
		public string Aspect  { get; set; }
		public decimal AspectAsNumber 
		{
			get
			{
				if (!String.IsNullOrEmpty(Aspect))
						{
							var aspectWidthAndHeightStringArray = Aspect.Split( new char[] {':','/'});
							if ((aspectWidthAndHeightStringArray != null) && (aspectWidthAndHeightStringArray.Length == 2))
							{
								if ((SupportMethods.IsNumeric(aspectWidthAndHeightStringArray[0])) &&  (SupportMethods.IsNumeric(aspectWidthAndHeightStringArray[1])))
								{
									return SupportMethods.ToDecimal(aspectWidthAndHeightStringArray[0]) / SupportMethods.ToDecimal(aspectWidthAndHeightStringArray[1]);
								}
							}
						}

				return -1;
			}
		}
		public string Duration  { get; set; }
		public decimal DurationMS  { get; set; }

		public decimal SamplingRateHz  { get; set; }
		public decimal SamplingRateKHz
		{				
			get
			{
				return Math.Round(SamplingRateHz/1004);
			}
		}


		public long StreamSize  { get; set; }

		public decimal Bitrate { get; set; }
		public decimal BitrateKbps 
		{ 
			get
			{
				return Math.Round(Bitrate/1024);
			}
		}
		public decimal BitrateMbps 
		{ 
			get
			{
				return Math.Round((Bitrate/1024)/1024);
			}
		}

		public void ReComputeStreamSizeByBitrate()
		{
			var bitratebps = BitrateKbps*1024;
			var bitrateBps = bitratebps/(decimal)8;

			var durationS = DurationMS/(decimal)1000.00;

			StreamSize = Convert.ToInt64(bitrateBps*durationS);
		}

		public string HumanReadableBitRate
		{
			get
			{
				return Math.Round(BitrateKbps) + " kpbs";
			}
		}

		public string HumanReadableStreamSize
		{
			get
			{
				return MediaInfo.ShowHumanReadableSize(StreamSize);
			}
		}

		#endregion

		public void CopyTo(TrackInfo track)
		{
			track.Aspect = Aspect;
			track.Bitrate = Bitrate;
			track.Codec = Codec;
			track.Channels = Channels;
			track.TrackType = TrackType;
			track.FrameRate = FrameRate;
			track.SamplingRateHz = SamplingRateHz;
			track.StreamSize = StreamSize;

			track.Width = RealWidth;
			track.PixelAspect = 1;

			track.Height = Height;
			track.DurationMS = DurationMS;
			track.TargetAudioCodec = TargetAudioCodec;
		}

		public void Clear()
		{
			Codec = String.Empty;

			Aspect = "0x0";
			Bitrate = 0;

			Channels = 0;
			TrackType = String.Empty;
			FrameRate = 0;
			SamplingRateHz = 0;

			Width = RealWidth;
			PixelAspect = 1;

			Height = Height;
			DurationMS = DurationMS;


			TargetAudioCodec = AudioCodecEnum.none;
		}	

		public void ParseFromXmlNode(XmlNode node)
		{
			foreach (XmlNode subNode in node.ChildNodes)
			{
			   	if (subNode.Name == "Overall_bit_rate" && (SupportMethods.IsNumeric(subNode.InnerText)))
					Bitrate = SupportMethods.ToDecimal(subNode.InnerText);

				if (subNode.Name == "Frame_rate" && (SupportMethods.IsNumeric(subNode.InnerText)))
					FrameRate = SupportMethods.ToDecimal(subNode.InnerText);

				if (subNode.Name == "Sampling_rate" && (SupportMethods.IsNumeric(subNode.InnerText)))
					SamplingRateHz = SupportMethods.ToDecimal(subNode.InnerText);

				if (subNode.Name == "Bit_rate" && (SupportMethods.IsNumeric(subNode.InnerText)))
					Bitrate = decimal.Parse(subNode.InnerText);

				if (subNode.Name == "Stream_size" && (SupportMethods.IsNumeric(subNode.InnerText)))
					StreamSize = long.Parse(subNode.InnerText);

				if (subNode.Name == "Channel_s_" && (SupportMethods.IsNumeric(subNode.InnerText)))
					Channels = Int32.Parse(subNode.InnerText);

				if (subNode.Name == "Pixel_aspect_ratio" && (SupportMethods.IsNumeric(subNode.InnerText)))
					PixelAspect = SupportMethods.ToDecimal(subNode.InnerText);

				if (subNode.Name == "Width" && (SupportMethods.IsInt(subNode.InnerText)))
					Width = Int32.Parse(subNode.InnerText);
				if (subNode.Name == "Height" && (SupportMethods.IsInt(subNode.InnerText)))
					Height = Int32.Parse(subNode.InnerText);

				if (subNode.Name == "Display_aspect_ratio" && (subNode.InnerText.Contains(":")))
					Aspect = subNode.InnerText;

				if (subNode.Name == "Codec")
					Codec = subNode.InnerText;

				if (subNode.Name == "Duration" && (subNode.InnerText.Contains(":")))
					Duration = subNode.InnerText;

			   	if (subNode.Name == "Duration" && (SupportMethods.IsNumeric(subNode.InnerText)))
					DurationMS = SupportMethods.ToDecimal(subNode.InnerText);

			}

			TrackType = node.Attributes.GetNamedItem("type").Value;
		}
	}
}

