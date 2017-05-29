using System;
using System.Collections.Generic;
using Gtk;

namespace MediaConvertGUI
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WidgetAudioTracks : Gtk.Bin
	{
		#region fileds && properties

		private MediaInfo _info;
		private bool _editable = false;
		private EventLock _eventLock = new EventLock();

		public MediaInfo Info 
		{ 
			get
			{
				return _info;
			}		
		}

		public WidgetAudioTracks ()
		{
			this.Build ();

			_info = new MediaInfo();

			Editable = false;

			comboChannels.Changed+= delegate { OnAnyValuechanged(); };
			comboBitrate.Changed+= delegate { OnAnyValuechanged(); };
			comboSampleRate.Changed+= delegate { OnAnyValuechanged(); };

		}

		public bool Editable 
		{ 
			get
			{
				return _editable;
		 	}
			set
			{
				_editable = value;

				SupportMethods.SetAvailability(comboCodec as Gtk.Widget,_editable);
				SupportMethods.SetAvailability(comboBitrate as Gtk.Widget,_editable);
				SupportMethods.SetAvailability(comboSampleRate as Gtk.Widget,_editable);
				SupportMethods.SetAvailability(comboChannels as Gtk.Widget,_editable);
			}
		}

		public TrackInfo SelectedTrack
		{
			get
			{
				if (Info == null)
					return null;

				var tracks = Info.AudioTracks;
				TrackInfo activeTrack = null;
				if (tracks.Count>0 && comboTracks.Active+1<=tracks.Count && tracks.ContainsKey(comboTracks.Active+1))
				{
					activeTrack = tracks[comboTracks.Active+1];
				}

				return activeTrack;
			}
		}

		public MediaCodec SelectedAudioCodec
		{
			get
			{
				var res = MediaConvertGUIConfiguration.GetAudioCodecByName ("none");

				if (comboCodec.Active > 0)
				{
					return MediaConvertGUIConfiguration.GetAudioCodecByName (comboCodec.ActiveText);
				}

				return res;
			}

		}

		public decimal BitRateTypedValue
		{
			get
			{
				var res = 0m;

				if (SupportMethods.IsNumeric( comboBitrate.Entry.Text))
				{
					res = SupportMethods.ToDecimal(comboBitrate.Entry.Text);
				} else
				{
					foreach (var  kvp in MediaConvertGUIConfiguration.DefaultAudioBitrates)
					{
						if (kvp.Value == comboBitrate.Entry.Text)
						{
							res = kvp.Key;
						}
					}
				}
			
				return res;
			}
		}

		#endregion

		#region methods

		public void SelectFirstAudioTrack()
		{
			if (_info!=null && 
			    _info.AudioTracks != null &&
			    _info.AudioTracks.Count>0   )
			{
				comboTracks.Active = 0;
				Fill();
			}
		}

		public void Fill()
		{
			if (_eventLock.Lock())
			{
				var activeTrack = SelectedTrack;						
				var activeTrackAsString = String.Empty;

				// filling tracks combo
				var trackStrings = new List<string>();
				if (Info != null && Info.AudioTracks.Count>0)
				{	 
					foreach (var kvp in Info.AudioTracks)
					{
						trackStrings.Add(kvp.Key.ToString());
						if ( (activeTrack == kvp.Value)) activeTrackAsString = kvp.Key.ToString();
					}
				}
				SupportMethods.FillComboBox(comboTracks, trackStrings,true, activeTrackAsString);

				// filling selected track
				if (activeTrack!= null)
				{
					// channels 
					var channelsStrings = new List<string>(){"1","2"};
					var activeChannelAsString = "";

					if ( (activeTrack.Channels == 1) || (activeTrack.Channels == 2))
					{
						activeChannelAsString = activeTrack.Channels.ToString();
					} 
					SupportMethods.FillComboBox(comboChannels, channelsStrings, Editable, activeChannelAsString);

					// codec
					if (Editable)
					{
						frameAudioOptions.Visible = (activeTrack.TargetAudioCodec.Name != "none") && 
													(activeTrack.TargetAudioCodec.Name != "copy");

						SupportMethods.FillComboBox(comboCodec,MediaConvertGUIConfiguration.AudioCodecsAsList(),true,activeTrack.TargetAudioCodec.Name);
					} else
					{
						frameAudioOptions.Visible = true;
						SupportMethods.FillComboBox(comboCodec,new List<string>(){activeTrack.Codec},false,activeTrack.Codec);
					}

					SupportMethods.FillComboBoxEntry(comboSampleRate,MediaConvertGUIConfiguration.DefaultSamplingRates,activeTrack.SamplingRateHz,Editable);
					SupportMethods.FillComboBoxEntry(comboBitrate,MediaConvertGUIConfiguration.DefaultAudioBitrates,activeTrack.BitrateKbps,Editable);

					labelTrackSze.Text = activeTrack.HumanReadableStreamSize;
				} else
				{
					SupportMethods.ClearCombo(comboChannels);
					SupportMethods.ClearCombo(comboCodec);

					SupportMethods.ClearCombo(comboBitrate);					
					SupportMethods.ClearCombo(comboSampleRate);

					frameAudioOptions.Visible = false;

					labelTrackSze.Text = String.Empty;
				}

				image.Visible = comboCodec.Active>0;

				_eventLock.Unlock();
			} 
		}

		public void FillFrom(MediaInfo mInfo)
		{
			if (mInfo != null)
			{
				mInfo.Copyto(_info,false);
			} else
				_info.ClearTracks();

			Fill();
			SelectFirstAudioTrack();
		}

		#endregion

		#region events

		private void OnAnyValuechanged()
		{
			if (_eventLock.Lock() && Editable)
			{			
				var activeTrack = SelectedTrack;
						
				if (activeTrack!= null)
				{
					activeTrack.TargetAudioCodec = SelectedAudioCodec;

					activeTrack.Bitrate = BitRateTypedValue*1000;
					activeTrack.ReComputeStreamSizeByBitrate();

					activeTrack.Channels = Convert.ToInt32(comboChannels.ActiveText);

					var samplingRateTypedValue = SupportMethods.ParseDecimalValueFromValue(comboSampleRate.ActiveText,MediaConvertGUIConfiguration.DefaultSamplingRates);
					activeTrack.SamplingRateHz = samplingRateTypedValue;
				}

				_eventLock.Unlock();
			};

			Fill();
		}

		protected void OnComboTracksChanged (object sender, EventArgs e)
		{
			Fill();
		}	

		protected void OnComboCodecChanged (object sender, EventArgs e)
		{
			OnAnyValuechanged();
		}

		protected void OnEntryBitrateChanged (object sender, EventArgs e)
		{
			OnAnyValuechanged();
		}		

		protected void OnEntrySampleRateChanged (object sender, EventArgs e)
		{
			OnAnyValuechanged();
		}	


		protected void OnEventBoxButtonPressEvent (object o, ButtonPressEventArgs args)
		{
			var activeTrack = SelectedTrack;
			if (activeTrack!= null)
			{
				var codec = SelectedAudioCodec;
				if (!String.IsNullOrEmpty(codec.Link))
				{
					SupportMethods.ExecuteInShell(codec.Link);
				}
			}
		}

		#endregion

	}
}

