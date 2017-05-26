using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace MediaConvertGUI
{

	public abstract class MediaInfoBase
	{
		#region static constants

		public static Dictionary<decimal,string> DefaultVideoBitRates = new Dictionary<decimal, string>()
		{
			{1500m,"VCD (1.5 Mb)"},
			{2000m,"Standard (2 Mb)"},
			{3500m,"TV  (3.5 Mb)"},	
			{5000m,"High (5 Mb)"},
			{9000m,"DVD (9 Mb)"},
			{15000m,"HDTV (15 Mb)"},
			{30000m,"HD DVD (30 Mb)"}
		};
		
		public static Dictionary<decimal,string> DefaultRotationAngles = new Dictionary<decimal, string>()
		{
			{0,"0"},
			{90,"90"},
			{180,"180"},	
			{270,"270"}
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
			{192m,"192"},
		};

		#endregion

		#region static Methods

		public static string MakeFFMpegScreenShot(MediaInfo sourceMovie, int secondsDelay = 0)
		{
			if (sourceMovie.FirstVideoTrack != null && File.Exists(sourceMovie.FileName))
			{
				var ffmpegCommandArgs = String.Empty;

				var timeSpan = TimeSpan.FromSeconds(secondsDelay);
				//var timeAsStrig = timeSpan.ToString("hh:mm:ss");
				var timeAsString = string.Format("{0}:{1}", 
				                                 timeSpan.Minutes.ToString().PadLeft(2,'0'),
				                                 timeSpan.Seconds.ToString().PadLeft(2,'0'));

				var picFileName = sourceMovie.FileName + ".jpg";

				var counter = 0;
				while (File.Exists(picFileName))
				{
					counter++;
					picFileName = sourceMovie.FileName + "." + counter.ToString().PadLeft(2,'0')+".jpg";
				}

				// https://trac.ffmpeg.org/wiki/Create%20a%20thumbnail%20image%20every%20X%20seconds%20of%20the%20video
				// ffmpeg -i input.flv -ss 00:00:14.435 -f image2 -vframes 1 out.png

				ffmpegCommandArgs = " -i {filename} -ss {time} -f image2 -vframes 1 {pic}";

				ffmpegCommandArgs = ffmpegCommandArgs.Replace("{filename}","\"" + sourceMovie.FileName + "\"");
				ffmpegCommandArgs = ffmpegCommandArgs.Replace("{time}",timeAsString);
				ffmpegCommandArgs = ffmpegCommandArgs.Replace("{pic}","\"" + picFileName + "\"");

				SupportMethods.ExecuteAndReturnOutput(MediaConvertGUIConfiguration.FFMpegPath,ffmpegCommandArgs);

				return picFileName;
			}

			return null;
		}
		
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
			    (targetMovie.FirstAudioTrack.TargetAudioCodec.Name != "none") &&
			    ( 	(targetMovie.FirstVideoTrack == null) || 
			 (targetMovie.TargetVideoCodec == MediaConvertGUIConfiguration.GetVideoCodecByName("none")) || 
			    	(targetMovie.TargetContainer == null) || 	
			 (targetMovie.TargetContainer.Name == "none")			    	
			    ) &&
			    (currentPass>1))
			{
				return res;
			} 		

			// codec copy  - single pass
			if ( (targetMovie.FirstVideoTrack != null) && (targetMovie.TargetVideoCodec==MediaConvertGUIConfiguration.GetVideoCodecByName("copy")) &&
			    (currentPass>1)) 
			{
				return res;
			} 	

			var sourceFile = " -i \"" + sourceMovie.FileName+"\"";				
			var ext = System.IO.Path.GetExtension(sourceMovie.FileName);
			var targetFile = sourceMovie.FileName+".converted" + ext;
			var video = " -vn";  // disable video								

			var hwaccel = String.Empty;

			var audio = " -an "; // disable audio				

			var map = String.Empty;

			if (targetMovie.FirstVideoTrack != null && targetMovie.TargetVideoCodec!=MediaConvertGUIConfiguration.GetVideoCodecByName("none") &&
				targetMovie.TargetContainer != null && targetMovie.TargetContainer.Name != "none")
			{
				var videoSettings= String.Empty;					
				var container = String.Empty;														

				container = " -f " + targetMovie.TargetContainer.Name;
				ext = targetMovie.TargetContainer.Extension;

				targetFile = sourceMovie.FileName+".converted" + ext;

				videoSettings += container;

				var aspect = " -aspect " + targetMovie.FirstVideoTrack.Aspect;
				var	scale =   " -s " + targetMovie.FirstVideoTrack.Width.ToString() + "x"+targetMovie.FirstVideoTrack.Height.ToString();
				var	bitrate = " -b:v " + targetMovie.FirstVideoTrack.Bitrate;	
				var frameRate = " -r " + targetMovie.FirstVideoTrack.FrameRate.ToString().Replace(",","."); // TODO: invariant culture								
				
				// auto rotation
				var rotation90AnglesCount = Convert.ToInt32(sourceMovie.FirstVideoTrack.RotatationAngle);
				var autoRotate = " -vf ";
				
				if (rotation90AnglesCount == 90) autoRotate += " transpose=2"; // 90CounterClockwise
				else
				if (rotation90AnglesCount == 180) autoRotate += " transpose=1,transpose=1"; // 2*90Clockwise
				else
				if (rotation90AnglesCount == 270) autoRotate += " transpose=1"; // 90Clockwise				
				else 
				autoRotate = "";  // unsupported transposition
					
				var rotationAngle = " -metadata:s:v:0 rotate=" + Convert.ToInt32(targetMovie.FirstVideoTrack.RotatationAngle).ToString(); 

				if (targetMovie.EditAspect)	videoSettings += aspect;
				if (targetMovie.EditResolution) videoSettings += scale;
				if (targetMovie.EditBitRate) videoSettings += bitrate;
				if (targetMovie.EditFrameRate) videoSettings += frameRate;
				if (targetMovie.EditRotation) videoSettings += rotationAngle;
				if (targetMovie.AutoRotate) videoSettings += autoRotate;

				if (targetMovie.TargetVideoCodec!=MediaConvertGUIConfiguration.GetVideoCodecByName("copy"))
				{
					var pass = String.Format(" -pass {0} -passlogfile \"{1}\"",currentPass,targetFile + ".passlog");
					videoSettings += pass;
				}

				video = targetMovie.TargetVideoCodec.Command;
				hwaccel = String.IsNullOrEmpty (targetMovie.TargetVideoCodec.HWAcceleration) ? "" : " -hwaccel " +targetMovie.TargetVideoCodec.HWAcceleration;
			}

			// only first Audio Track!
			if (targetMovie.AudioTracks.Count>0)
			{
				var targetAudioTrack = targetMovie.FirstAudioTrack;

				var audioQuality = String.Format(" -ac {0} -ar {1} -ab {2}",	
				                                 targetAudioTrack.Channels,
				                                 targetAudioTrack.SamplingRateHz,
				                                 targetAudioTrack.Bitrate);

				audio = " " + targetAudioTrack.TargetAudioCodec.Command + " ";
				if (targetAudioTrack.TargetAudioCodec.Name != "copy")
					audio += " " + audioQuality;


				if ( (targetMovie.FirstVideoTrack == null) || 
					 (targetMovie.TargetVideoCodec == MediaConvertGUIConfiguration.GetVideoCodecByName("none")) ||					 
					 (targetMovie.TargetContainer.Name == "none")
					)									
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


			res = MediaConvertGUIConfiguration.FFMpegPath+" " + hwaccel + " -y -dump " + sourceFile + map + " " +video + audio + targetFile;

			return res;
		}

		public static MediaContainer DetectContainerByExt(string fileName)
		{
			var res = MediaConvertGUIConfiguration.GetContainerByName ("none");

			var ext = System.IO.Path.GetExtension(fileName).ToLower();
			if (!String.IsNullOrEmpty(ext) && ext.Length>1)
			{
				ext = ext.Substring(1);
			}

			var cont = MediaConvertGUIConfiguration.GetContainerByExt (ext);
			if (cont != null) 
				res = cont;

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

