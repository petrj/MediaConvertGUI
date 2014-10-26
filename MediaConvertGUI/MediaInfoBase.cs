using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace MediaConvertGUI
{
	#region audio && video enums

	public enum AudioCodecEnum
	{
		none,
		copy,
		mp2,
		mp3,
		vorbis,
		aac,
		flac,
		ac3
	}

	public enum VideoCodecEnum
	{
		none,
		copy,
		xvid,
		flv,
		h263,
		h264,
		mpeg,
		theora,
		vp8
	}

	public enum VideoContainerEnum
	{
		none,
		avi,
		flv,
		mp4,
		mkv,
		mpeg,
		ogg,
		webm,
		_3gp
	}

	#endregion

	public abstract class MediaInfoBase
	{
		#region static constants

		public static Dictionary<AudioCodecEnum,string> WikiAudioCodecsLinks = new Dictionary<AudioCodecEnum, string>()
		{
			{AudioCodecEnum.mp2 ,"http://en.wikipedia.org/wiki/MP2"},
			{AudioCodecEnum.mp3,"http://en.wikipedia.org/wiki/MP3"},
			{AudioCodecEnum.vorbis,"http://en.wikipedia.org/wiki/Vorbis"},
			{AudioCodecEnum.aac,"http://en.wikipedia.org/wiki/Aac"},
			{AudioCodecEnum.flac,"http://en.wikipedia.org/wiki/flac"},
			{AudioCodecEnum.ac3,"http://en.wikipedia.org/wiki/Dolby_AC-3"}
		};

		public static Dictionary<VideoCodecEnum,string> WikiVideoCodecsLinks = new Dictionary<VideoCodecEnum, string>()
		{		
			{VideoCodecEnum.xvid ,"http://en.wikipedia.org/wiki/xvid"},
			{VideoCodecEnum.flv,"http://en.wikipedia.org/wiki/Sorenson_Media#Sorenson_Spark"},
			{VideoCodecEnum.h263,"http://en.wikipedia.org/wiki/h263"},
			{VideoCodecEnum.h264,"http://en.wikipedia.org/wiki/h264"},
			{VideoCodecEnum.mpeg,"http://en.wikipedia.org/wiki/MPEG-1"},
			{VideoCodecEnum.theora,"http://en.wikipedia.org/wiki/theora"},
			{VideoCodecEnum.vp8,"http://en.wikipedia.org/wiki/vp8"}
		};

		public static Dictionary<VideoContainerEnum,string> WikiContainerCodecsLinks = new Dictionary<VideoContainerEnum, string>()
		{		
			{VideoContainerEnum.avi ,"http://en.wikipedia.org/wiki/Audio_Video_Interleave"},
			{VideoContainerEnum.flv ,"http://en.wikipedia.org/wiki/Flash_Video"},
			{VideoContainerEnum.mkv ,"http://en.wikipedia.org/wiki/Mkv"},
			{VideoContainerEnum.mp4 ,"http://en.wikipedia.org/wiki/Mp4"},
			{VideoContainerEnum.mpeg ,"http://en.wikipedia.org/wiki/Mp4"},
			{VideoContainerEnum.ogg ,"http://en.wikipedia.org/wiki/Ogg"},
			{VideoContainerEnum.webm ,"http://en.wikipedia.org/wiki/Webm"},
			{VideoContainerEnum._3gp ,"http://en.wikipedia.org/wiki/3gp"},
		};

		public static Dictionary<VideoCodecEnum,string> DefaultVideoCodecsDescriptions = new Dictionary<VideoCodecEnum, string>()
		{
			{VideoCodecEnum.xvid,"MPEG-4 ASP libxvid (MPEG-4 part 2)"},
			{VideoCodecEnum.flv,"Sorenson Spark / Sorenson H.263 (Flash Video)"},
			{VideoCodecEnum.h264,"MPEG-4 AVC libx264 (MPEG-4 part 10)"},
			{VideoCodecEnum.mpeg,"MPEG-1 video"},
			{VideoCodecEnum.theora,"Theora libtheora"},
			{VideoCodecEnum.vp8,"VP8 libvpx"},
		};


		public static Dictionary<VideoContainerEnum,string> VideoContainerToExtension = new Dictionary<VideoContainerEnum, string> () 
		{
			{VideoContainerEnum.avi,".avi"},
			{VideoContainerEnum.flv,".flv"},
			{VideoContainerEnum.mp4,".mp4"},
			{VideoContainerEnum.mpeg,".mpeg"},
			{VideoContainerEnum.ogg,".ogv"},
			{VideoContainerEnum.mkv,".mkv"},
			{VideoContainerEnum.webm,".webm"},
			{VideoContainerEnum._3gp,".3gp"},
		};

		public static Dictionary<VideoContainerEnum,string> VideoContainerToFFMpegContainer = new Dictionary<VideoContainerEnum, string> () 
		{
			{VideoContainerEnum.none,""},
			{VideoContainerEnum.avi,"avi"},
			{VideoContainerEnum.flv,"flv"},
			{VideoContainerEnum.mp4,"mp4"},
			{VideoContainerEnum.mpeg,"mpg"},
			{VideoContainerEnum.ogg,"ogg"},
			{VideoContainerEnum.mkv,"mkv"},
			{VideoContainerEnum.webm,"webm"},
			{VideoContainerEnum._3gp,"3gp"},
		};

		public static Dictionary<decimal,string> DefaultVideoBitRates = new Dictionary<decimal, string>()
		{
			{1500m,"VCD (1.5 Mb)"},
			{2000m,"Standard (2 Mb)"},
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

		#region static Methods
		
		public static List<string> MakeFFMpegCommands(Dictionary<MediaInfo,MediaInfo> MoviesInfo)
		{
			var commands = new List<string>();

			foreach (var kvp in MoviesInfo)
			{
				commands.Add(MakeFFMpegCommand(kvp.Key,kvp.Value,1));
				commands.Add(MakeFFMpegCommand(kvp.Key,kvp.Value,2));
			}

			return commands;
		}

		public static string MakeFFMpegCommandsAsString(Dictionary<MediaInfo,MediaInfo> moviesInfo)
		{
			var commands = new System.Text.StringBuilder();

			foreach (var cmd in MakeFFMpegCommands(moviesInfo))
			{
				commands.Append(cmd);
				commands.Append(System.Environment.NewLine);
			}

			return commands.ToString();
		}

		/// <summary>
		/// Makes the FF MPEG command.
		/// </summary>
		/// <returns>The FF MPEG command.</returns>
		/// <param name="sourceMovie">Source movie.</param>
		/// <param name="targetMovie">Target movie.</param>
		/// <param name="currentPass">Current pass.</param>
		public static string MakeFFMpegCommand(MediaInfo sourceMovie, MediaInfo targetMovie, int currentPass)
		{		
			var res= String.Empty;

			// single audio convert? 
			if ( (targetMovie.AudioTracks.Count > 0) && 
			    (targetMovie.FirstAudioTrack.TargetAudioCodec!= AudioCodecEnum.none) &&
			    ( (targetMovie.FirstVideoTrack == null) || (targetMovie.TargetVideoCodec == VideoCodecEnum.none)) &&
			    (currentPass>1))
			{
				return res;
			} 		

			// codec copy  - single pass
			if ( (targetMovie.FirstVideoTrack != null) && (targetMovie.TargetVideoCodec==VideoCodecEnum.copy) &&
			    (currentPass>1))
			{
				return res;
			} 	

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

				container = " -f " + MediaInfoBase.VideoContainerToFFMpegContainer [targetMovie.TargetContainer];
				ext = MediaInfoBase.VideoContainerToExtension[targetMovie.TargetContainer];

				targetFile = sourceMovie.FileName+".converted" + ext;

				videoSettings += container;

				var aspect = " -aspect " + targetMovie.FirstVideoTrack.Aspect;
				var	scale =   " -s " + targetMovie.FirstVideoTrack.Width.ToString() + "x"+targetMovie.FirstVideoTrack.Height.ToString();
				var	bitrate = " -b:v " + targetMovie.FirstVideoTrack.Bitrate;	
				var frameRate = " -r " + targetMovie.FirstVideoTrack.FrameRate.ToString().Replace(",","."); // TODO: invariant culture

				if (targetMovie.EditAspect)	videoSettings += aspect;
				if (targetMovie.EditResolution) videoSettings += scale;
				if (targetMovie.EditBitRate) videoSettings += bitrate;
				if (targetMovie.EditFrameRate) videoSettings += frameRate;

				if (targetMovie.TargetVideoCodec!=VideoCodecEnum.copy)
				{
					var pass = String.Format(" -pass {0} -passlogfile \"{1}\"",currentPass,targetFile + ".passlog");
					videoSettings += pass;
				}

				switch (targetMovie.TargetVideoCodec)
				{
					case VideoCodecEnum.copy: video = " -vcodec copy"+videoSettings; break;
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

			/*
			if (File.Exists(targetMovie.FFMPEGOutputFileName))
					File.Delete(targetMovie.FFMPEGOutputFileName);
			if (File.Exists(targetMovie.FFMPEGPassLogFileName))
					File.Delete(targetMovie.FFMPEGPassLogFileName);			
			*/

			targetFile = String.Format(" \"{0}\"",targetFile);

			res = "ffmpeg -y -dump " + sourceFile + map + video + audio + targetFile;

			return res;
		}

		public static VideoContainerEnum DetectContainerByExt(string fileName)
		{
			var res = VideoContainerEnum.none;
			var ext = System.IO.Path.GetExtension(fileName).ToLower();
			if (!String.IsNullOrEmpty(ext) && ext.Length>1)
			{
				ext = ext.Substring(1);
			}

			foreach (var cont in Enum.GetNames(typeof(VideoContainerEnum)))
			{
				if (cont == ext)
				{
					if (Enum.TryParse(cont,out res))
					{
						break;
					} 					
				}
			}

			return res;
		}

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
		public static int GetLastFrameFromConvertLogFile(string outputFileName)
		{
			var lastFrame = -1;

			if (File.Exists(outputFileName))
			{
				using (var fs = new FileStream(outputFileName,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
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

		/// <summary>
		/// Gets the last time from convert log file in seconds
		/// Parsing text "time=00:22:25.05" to 1345 (22*60++25+0.05) s
		/// </summary>
		/// <returns>
		/// The last frame time convert log file (int).
		/// If value not found, returns -1
		/// </returns>
		/// <param name='outputFileName'>
		/// log file name.
		/// </param>
		public static int GetLastTimeFromConvertLogFile(string outputFileName)
		{
			var lastTime = -1;

			if (File.Exists(outputFileName))
			{
				using (var fs = new FileStream(outputFileName,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
				{
					using (var sr = new StreamReader(fs)) 
					{					    
						string line;
						while ((line = sr.ReadLine()) != null)
						{
							if (line != null && line.Contains("time="))
							{
								var pos = line.IndexOf ("time=");
								if (line.Length > pos + 5 + 8) 
								{
									var lastTimeAsString = line.Substring (pos + 5, 8).Trim ();
									var hms = lastTimeAsString.Split (':');
									if (hms.Length == 3 &&
									    SupportMethods.IsNumeric (hms [0]) &&
									    SupportMethods.IsNumeric (hms [1]) &&
									    SupportMethods.IsNumeric (hms [2])) 
									{
										lastTime = Convert.ToInt32(hms[0])*3600+Convert.ToInt32(hms[1])*60+Convert.ToInt32(hms[2]); // ignoring ms
									}
								}							
							}
						}
					}
				}
			}
			return lastTime;
		}

		#endregion

		#region changed

		protected bool _changed = false;

		public virtual bool IsChanged()
		{
			return _changed;
		}

		protected virtual void NotifyChange(string name, object value)
		{
			_changed = true;
		}

		public virtual void UnChanged()
		{
			_changed = false;
		}

		#endregion
	}
}

