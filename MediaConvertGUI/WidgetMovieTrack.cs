using System;
using System.Collections.Generic;
using Gtk;

namespace MediaConvertGUI
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WidgetMovieTrack : Gtk.Bin
	{
		private MediaInfo _movieInfo;
		private bool _editable = false;
		private EventLock _eventLock = new EventLock();

		#region properties

		public MediaInfo MovieInfo 
		{ 
			get
			{
				return _movieInfo;
			}
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

				SupportMethods.SetAvailability(entryWidth as Gtk.Widget,_editable);
				SupportMethods.SetAvailability(entryHeight as Gtk.Widget,_editable);

				SupportMethods.SetAvailability(comboBitRate as Gtk.Widget,_editable);
				SupportMethods.SetAvailability(comboCodec as Gtk.Widget,_editable);
				SupportMethods.SetAvailability(comboAspect as Gtk.Widget,_editable);
				SupportMethods.SetAvailability(comboFrameRate as Gtk.Widget,_editable);

				SupportMethods.SetAvailability(entryRealWidth as Gtk.Widget,false);
				SupportMethods.SetAvailability(entryPixelAspect as Gtk.Widget,false);

				checkKeep.Sensitive = _editable;

				Fill();
			}
		}

		#endregion

		public WidgetMovieTrack ()
		{
			this.Build ();
			Editable = false;
			_movieInfo = new MediaInfo();
		}

		public void FillFrom(MediaInfo mInfo)
		{
			if (mInfo != null)
			{
				mInfo.Copyto(_movieInfo,false);
			}
			Fill();
		}

		public void Fill()
		{
			if (_eventLock.Lock())
			{
				var defaultAspects = new List<string>{"16:9","4:3"};
				var frameRates = new List<string>{"23.976","25"};

				//textviewRawOutput.Buffer.Text = MovieInfo.RawMediaInfoOutput;
				if (MovieInfo != null && MovieInfo.FirstVideoTrack != null)
				{
					var m = MovieInfo.FirstVideoTrack;

					entryWidth.Text = m.Width.ToString();
					entryRealWidth.Text = m.RealWidth.ToString();
					entryHeight.Text = m.Height.ToString();
					entryPixelAspect.Text = m.PixelAspect.ToString();

					// fill frame rate combo
					SupportMethods.FillComboBoxEntry(comboFrameRate,frameRates,m.FrameRate.ToString(),true,Editable);

					// fill aspect ratio combo
					SupportMethods.FillComboBoxEntry(comboAspect,defaultAspects,m.Aspect,false,Editable);

					SupportMethods.FillComboBoxEntry(comboBitRate,MediaInfo.DefaultVideoBitRates,m.BitrateKbps,Editable);

					if (Editable)
					{
						SupportMethods.FillComboBox(comboCodec,typeof(VideoCodecEnum),Editable,(int)MovieInfo.TargetVideoCodec);
					} else
					{
						SupportMethods.FillComboBox(comboCodec,new List<string>() {m.Codec}, Editable,m.Codec);
					}

					frameVideooptions.Visible = 
						(MovieInfo != null) && 
						(MovieInfo.FirstVideoTrack!=null) && 
						( ((Editable) && (comboCodec.Active > 0)) || !Editable );
					
					m.ReComputeStreamSizeByBitrate();
					labelTrackSize.Text = m.HumanReadableStreamSize;
				} else
				{
					entryWidth.Text = String.Empty;
					entryRealWidth.Text = String.Empty;
					entryHeight.Text = String.Empty;
					entryPixelAspect.Text = String.Empty;

					if (MovieInfo!=null) 
					{
						SupportMethods.FillComboBox(comboCodec,new List<string>() {String.Empty}, false,String.Empty);	
					} else
					{
						comboCodec.Model = new ListStore(typeof(string));
						comboCodec.Active = 0;
						labelTrackSize.Text = String.Empty;
					}

					SupportMethods.FillComboBoxEntry(comboBitRate,MediaInfo.DefaultVideoBitRates,0,false);
					SupportMethods.FillComboBoxEntry(comboAspect,defaultAspects,"",false,false);
					SupportMethods.FillComboBoxEntry(comboFrameRate,frameRates,"",false,false);				 
				}
				_eventLock.Unlock();
			}
		}

		#region events

		protected void OnComboBitRateChanged (object sender, EventArgs e)
		{
			OnAnyValueChanged();
		}		

		protected void OnEntryWidthChanged (object sender, EventArgs e)
		{
			if (checkKeep.Active && 
			    MovieInfo.FirstVideoTrack != null && 
			    SupportMethods.IsNumeric(entryWidth.Text))
			{
				if (_eventLock.Lock())
				{

					var width = SupportMethods.ToDecimal(entryWidth.Text);

					var aspectRatio = MovieInfo.FirstVideoTrack.AspectAsNumber;
					if (aspectRatio != -1)
					{	
						entryHeight.Text = Convert.ToInt32( width / aspectRatio ).ToString();
					}
					_eventLock.Unlock();

					OnAnyValueChanged();
				}
			}
		}		

		protected void OnEntryHeightChanged (object sender, EventArgs e)
		{
			if (checkKeep.Active && 
			    MovieInfo != null && 
			    MovieInfo.FirstVideoTrack != null && 
			    SupportMethods.IsNumeric(entryHeight.Text))
			{

				if (_eventLock.Lock())
				{
					var height = SupportMethods.ToDecimal(entryHeight.Text);
					var aspectRatio = MovieInfo.FirstVideoTrack.AspectAsNumber;
					if (aspectRatio != -1)
					{						
						entryWidth.Text = Convert.ToInt32( height * aspectRatio ).ToString();						
					}	

					_eventLock.Unlock();					
					OnAnyValueChanged();
				}
			}
		}

		protected void OnComboAspectChanged (object sender, EventArgs e)
		{
			OnAnyValueChanged();
		}

		protected void OnComboFrameRateChanged (object sender, EventArgs e)
		{
			OnAnyValueChanged();
		}

		private void OnAnyValueChanged()
		{
			if (Editable && MovieInfo != null && MovieInfo.FirstVideoTrack != null)
			{
				if (_eventLock.Lock())
				{
					var m = MovieInfo.FirstVideoTrack;

					var bitRateTypedValue = SupportMethods.ParseDecimalValueFromValue(comboBitRate.ActiveText,MediaInfo.DefaultVideoBitRates);
					m.Bitrate = bitRateTypedValue*1024;

					if (SupportMethods.IsNumeric(entryHeight.Text))					
					m.Height = Convert.ToInt32(entryHeight.Text);

					if (SupportMethods.IsNumeric(entryWidth.Text))					
					m.Width = Convert.ToInt32(entryWidth.Text);

					m.Aspect = comboAspect.ActiveText;

					if (SupportMethods.IsNumeric(comboFrameRate.ActiveText))					
					m.FrameRate = SupportMethods.ToDecimal(comboFrameRate.ActiveText);

					MovieInfo.TargetVideoCodec = (VideoCodecEnum)comboCodec.Active;

					comboCodec.TooltipText = String.Empty;
					if (comboCodec.Active > 0)
					{
						VideoCodecEnum selcodec;
						if (Enum.TryParse(comboCodec.ActiveText,out selcodec))
						{
							if (MediaInfo.DefaultVideoCodecsDescriptions.ContainsKey(selcodec))
							{
								comboCodec.TooltipText = MediaInfo.DefaultVideoCodecsDescriptions[selcodec];
							}
						}
					}

					_eventLock.Unlock();
				}
				Fill();
			}
		}		

		protected void OnComboCodecChanged (object sender, EventArgs e)
		{
			OnAnyValueChanged();
		}

		protected void OnComboExtChanged (object sender, EventArgs e)
		{
			OnAnyValueChanged();
		}

		protected void OnCheckKeepToggled (object sender, EventArgs e)
		{
			OnAnyValueChanged();
		}

		protected void OnButtonCodecsInfoClicked (object sender, EventArgs e)
		{

		}
		#endregion

	}
}

