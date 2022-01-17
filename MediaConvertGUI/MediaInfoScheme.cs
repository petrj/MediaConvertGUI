using System;

namespace MediaConvertGUI
{
	public class MediaInfoScheme
	{
		public VideoContainerEnum Container { get; set; }
		public VideoCodecEnum VideoCodec { get; set; }
		public AudioCodecEnum AudioCodec { get; set; }

		public MediaInfoScheme ()
		{
			VideoCodec = VideoCodecEnum.none;
			AudioCodec = AudioCodecEnum.none;
			Container = VideoContainerEnum.avi;
		}

		public MediaInfoScheme (VideoContainerEnum container, VideoCodecEnum vCodec,AudioCodecEnum aCodec)
		{
			VideoCodec = vCodec;
			AudioCodec = aCodec;
			Container = container;
		}
	}
}

