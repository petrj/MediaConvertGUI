using System;
using Gtk;
using System.Collections.Generic;

namespace MediaConvertGUI
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WidgetTargetAudioTrack : Gtk.Bin
	{
		public MediaInfo _movieInfo;
		private bool _disableChangeEvent = false;

		public MediaInfo MovieInfo 
		{ 
			get
			{
				return _movieInfo;
			}
			set
			{
				_movieInfo = value;
				Fill();
			}
		}

		public WidgetTargetAudioTrack ()
		{
			this.Build ();

			comboChannels.AppendText("1");
			comboChannels.AppendText("2");
		}

		public decimal BitRateTypedValue
		{
			get
			{
				var res = 0m;

				if (SupportMethods.IsNumeric( comboBitRate.Entry.Text))
				{
					res = SupportMethods.ToDecimal(comboBitRate.Entry.Text);
				} else
				{
					foreach (var  kvp in MediaInfo.DefaultAudioBitRates)
					{
						if (kvp.Value == comboBitRate.Entry.Text)
						{
							res = kvp.Key;
						}
					}
				}
			
				return res;
			}
		}

		private void Fill()
		{
			_disableChangeEvent = true;

			if (MovieInfo != null && MovieInfo.FirstAudioTrack != null)
			{
				var track = MovieInfo.FirstAudioTrack;

				track.ReComputeStreamSizeByBitrate();

				SupportMethods.FillComboBoxEntry(comboBitRate,MediaInfo.DefaultAudioBitRates,track.BitrateKbps,true);

				SupportMethods.FillComboBox(comboAudio,typeof(AudioCodecEnum),true,(int)track.TargetAudioCodec);

				if (track.Channels == 1)				
					comboChannels.Active = 0;				
				else comboChannels.Active = 1;

				comboAudio.Active = (int)track.TargetAudioCodec;

				comboAudio.Sensitive = true;
				frameAudio.Visible = track.TargetAudioCodec != AudioCodecEnum.none;

				// Sample Rate Combo
				SupportMethods.FillComboBoxEntry(comboSampleRate,new List<string>(),track.SamplingRateHz.ToString(),true,true);

				labelTrackSize.Text = track.HumanReadableStreamSize;
			} else
			{
				SupportMethods.FillComboBoxEntry(comboBitRate,MediaInfo.DefaultAudioBitRates,0,false);
				comboChannels.Active = 0;				
				comboAudio.Active = 0;
				comboSampleRate.Model = new ListStore(typeof(string));

				//comboAudio.Sensitive = false;
				frameAudio.Visible = false;

				labelTrackSize.Text = String.Empty;
			}

			_disableChangeEvent = false;
		}

		protected void OnComboAudioChanged (object sender, EventArgs e)
		{
			if (!_disableChangeEvent) OnAnyValueChanged();
		}		

		protected void OnEntryBitrateChanged (object sender, EventArgs e)
		{
			if (!_disableChangeEvent) OnAnyValueChanged();
		}

		protected void OnComboChannelsChanged (object sender, EventArgs e)
		{
			if (!_disableChangeEvent) OnAnyValueChanged();
		}		

		protected void OnComboSampleRateChanged (object sender, EventArgs e)
		{
			if (!_disableChangeEvent) OnAnyValueChanged();
		}

		public AudioCodecEnum SelectedAudioCodec
		{
			get
			{
				var res = AudioCodecEnum.none;

				if ( (comboAudio.Active > 0) && (comboAudio.Active < (Enum.GetNames((typeof(AudioCodecEnum))).Length)))
				{
					return (AudioCodecEnum)comboAudio.Active;
				}

				return res;
			}

		}
		
		private void OnAnyValueChanged()
		{
			if (MovieInfo != null )
			{
				var track = MovieInfo.FirstAudioTrack;
				if (track != null)
				{
					track.Bitrate = BitRateTypedValue*1024;
					track.Channels = Convert.ToInt32(comboChannels.ActiveText);

					if (SupportMethods.IsNumeric(comboSampleRate.ActiveText))
					track.SamplingRateHz = SupportMethods.ToDecimal(comboSampleRate.ActiveText);

					track.TargetAudioCodec = SelectedAudioCodec;

					Fill();
				}				
			}
		}		

		protected void OnComboBitRateChanged (object sender, EventArgs e)
		{
			if (!_disableChangeEvent) OnAnyValueChanged();
		}	
	}
}

