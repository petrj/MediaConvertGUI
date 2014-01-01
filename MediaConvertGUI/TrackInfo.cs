using System;
using System.Xml;

namespace MediaConvertGUI
{
	public class TrackInfo : MediaInfoBase
	{
		#region fileds && properties

		private AudioCodecEnum _targetAudioCodec = AudioCodecEnum.MP3;
		public AudioCodecEnum TargetAudioCodec
		{ 
			get { return _targetAudioCodec; }
			set 
			{ 
				if (_targetAudioCodec != value) NotifyChange("TargetAudioCodec",value);
				_targetAudioCodec = value;
			}
		}

		public string _trackType;
		public string TrackType
		{ 
			get { return _trackType; }
			set 
			{ 
				if (_trackType != value) NotifyChange("TrackType",value);
				_trackType = value;
			}
		}

		private string _codec;
		public string Codec
		{ 
			get { return _codec; }
			set 
			{ 
				if (_codec != value) NotifyChange("Codec",value);
				_codec = value;
			}
		}

		public int _channels;
		public int Channels
		{ 
			get { return _channels; }
			set 
			{ 
				if (_channels != value) NotifyChange("Channels",value);
				_channels = value;
			}
		}

		private decimal _frameRate;
		public decimal FrameRate
		{ 
			get { return _frameRate; }
			set 
			{ 
				if (_frameRate != value) NotifyChange("FrameRate",value);
				_frameRate = value;
			}
		}

		private int _width;
		public int Width 
		{ 
			get { return _width; }
			set 
			{ 
				if (_width != value) NotifyChange("Width",value);
				_width = value;
			}
		}

		public decimal _pixelAspect;
		public decimal PixelAspect
		{ 
			get { return _pixelAspect; }
			set 
			{ 
				if (_pixelAspect != value) NotifyChange("PixelAspect",value);
				_pixelAspect = value;
			}
		}

		public int RealWidth
		{ 
			get
			{
				return Convert.ToInt32(PixelAspect * Width);
			}
		}

		private int _height;
		public int Height
		{ 
			get { return _height; }
			set 
			{ 
				if (_height != value) NotifyChange("Height",value);
				_height = value;
			}
		}

		private string _aspect;
		public string Aspect
		{ 
			get { return _aspect; }
			set 
			{ 
				if (_aspect != value) NotifyChange("Aspect",value);
				_aspect = value;
			}
		}

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

		private string _duration;
		public string Duration
		{ 
			get { return _duration; }
			set 
			{ 
				if (_duration != value) NotifyChange("Duration",value);
				_duration = value;
			}
		}
		private decimal _durationMS;
		public decimal DurationMS
		{ 
			get { return _durationMS; }
			set 
			{ 
				if (_durationMS != value) NotifyChange("DurationMS",value);
				_durationMS = value;
			}
		}

		public decimal _samplingRateHz;
		public decimal SamplingRateHz
		{ 
			get { return _samplingRateHz; }
			set 
			{ 
				if (_samplingRateHz != value) NotifyChange("SamplingRateHz",value);
				_samplingRateHz = value;
			}
		}
		public decimal SamplingRateKHz
		{				
			get
			{
				return Math.Round(SamplingRateHz/1024);
			}
		}

		public long _streamSize;
		public long StreamSize
		{ 
			get { return _streamSize; }
			set 
			{ 
				if (_streamSize != value) NotifyChange("StreamSize",value);
				_streamSize = value;
			}
		}

		public decimal _bitrate;
		public decimal Bitrate
		{ 
			get { return _bitrate; }
			set 
			{ 
				if (_bitrate != value) NotifyChange("Bitrate",value);
				_bitrate = value;
			}
		}

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
				return SupportMethods.HumanReadableSize(StreamSize);
			}
		}

		#endregion

		#region methods

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

		#endregion
	}
}

