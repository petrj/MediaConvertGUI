using System;
using Gtk;

namespace MediaConvertGUI
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WidgetAudioTracks : Gtk.Bin
	{
		public MediaInfo _movieInfo;

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

		private bool _editable = false;

		public WidgetAudioTracks ()
		{
			this.Build ();

			comboChannels.AppendText("-");
			comboChannels.AppendText("1");
			comboChannels.AppendText("2");

			Editable = false;
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

				SupportMethods.SetAvailability(entryBitrate as Gtk.Widget,_editable);
				SupportMethods.SetAvailability(comboCodec as Gtk.Widget,_editable);
				SupportMethods.SetAvailability(comboChannels as Gtk.Widget,_editable);
				SupportMethods.SetAvailability(entrySampleRate as Gtk.Widget,_editable);
			}
		}

		private void Fill()
		{
			if (MovieInfo != null && MovieInfo.AudioTracks.Count>0)
			{
				foreach (var kvp in MovieInfo.AudioTracks)
				{
					var track = kvp.Value;
					comboTracks.AppendText("Track #"+kvp.Key.ToString());
				}
				comboTracks.Active = 0;
			} else
			{
				comboTracks.Model = new ListStore(typeof(string));
				OnComboTracksChanged(this,null);
			}
		}

		public TrackInfo SelectedTrack
		{
			get
			{
				if (MovieInfo == null)
					return null;

				var tracks = MovieInfo.AudioTracks;
				TrackInfo activeTrack = null;
				if (tracks.Count>0 && comboTracks.Active+1<=tracks.Count && tracks.ContainsKey(comboTracks.Active+1))
				{
					activeTrack = tracks[comboTracks.Active+1];
				}

				return activeTrack;
			}
		}

		protected void OnComboTracksChanged (object sender, EventArgs e)
		{
			var activeTrack = SelectedTrack;
						
			if (activeTrack!= null)
			{
				if ( (activeTrack.Channels == 1) || (activeTrack.Channels == 2))
				{
					comboChannels.Active = activeTrack.Channels;
				} else
				{
					comboChannels.Active = 0;
				}

				comboCodec.Model = new ListStore(typeof(string));

				entryBitrate.Text = activeTrack.BitrateKbps.ToString();
				labelTrackSze.Text = activeTrack.HumanReadableStreamSize;
				entrySampleRate.Text = activeTrack.SamplingRateHz.ToString();

				comboCodec.AppendText(activeTrack.Codec);
				comboCodec.Active = 0;

			} else
			{
				entryBitrate.Text = String.Empty;
				labelTrackSze.Text = String.Empty;
				comboCodec.Active = -1;
				entrySampleRate.Text = "";
			}
		}	

		protected void OnEntryBitrateChanged (object sender, EventArgs e)
		{
			if (Editable)
			{
				var activeTrack = SelectedTrack;
						
				if (activeTrack!= null)
				{
					int bitrate;
					if (int.TryParse(entryBitrate.Text,out bitrate))
					{
						activeTrack.Bitrate = bitrate*1024;
						activeTrack.ReComputeStreamSizeByBitrate();
						labelTrackSze.Text = activeTrack.HumanReadableStreamSize;
					}
				}			
			}
		}

	}
}

