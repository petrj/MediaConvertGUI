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

