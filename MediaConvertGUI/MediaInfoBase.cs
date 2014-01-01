using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace MediaConvertGUI
{
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

