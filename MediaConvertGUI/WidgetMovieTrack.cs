using System;
using System.Collections.Generic;
using Gtk;

namespace MediaConvertGUI
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WidgetMovieTrack : Gtk.Bin
	{
		#region fileds && properties

		private MediaInfo _movieInfo;
		private bool _editable = false;
		private EventLock _eventLock = new EventLock();

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

			comboContainer.Changed+=delegate { OnAnyValueChanged(); };
			eventBoxCodec.ButtonPressEvent+= OnCodecEventBoxButtonPressEvent;
			eventBocContainer.ButtonPressEvent += OnContainerEventBoxButtonPressEvent;
		}

		#region methods

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

				var chBoxesVisible = false;

				//textviewRawOutput.Buffer.Text = MovieInfo.RawMediaInfoOutput;
				if (MovieInfo != null && MovieInfo.FirstVideoTrack != null)
				{
					var m = MovieInfo.FirstVideoTrack;

					chBoxResolution.Active = MovieInfo.EditResolution;
					chBoxAspect.Active = MovieInfo.EditAspect;
					chBoxBitRate.Active = MovieInfo.EditBitRate;
					chBoxFrameRate.Active = MovieInfo.EditFrameRate;

					if (MovieInfo.EditResolution) 
					{
						entryWidth.Text = m.Width.ToString ();		
						entryHeight.Text = m.Height.ToString();
						entryPixelAspect.Text = m.PixelAspect.ToString();
						entryRealWidth.Text = m.RealWidth.ToString();
					}
					else 
					{
						entryHeight.Text = String.Empty;
						entryPixelAspect.Text = String.Empty;
						entryWidth.Text = String.Empty;
						entryRealWidth.Text = String.Empty;
					}
					entryWidth.Sensitive = entryHeight.Sensitive = MovieInfo.EditResolution;

					if (MovieInfo.EditAspect) 
					{
						// fill aspect ratio combo
						SupportMethods.FillComboBoxEntry (comboAspect, defaultAspects, m.Aspect, false, Editable);
					} else 
					{
						SupportMethods.ClearCombo(comboAspect);
					}
					comboAspect.Sensitive = MovieInfo.EditAspect;

					if (MovieInfo.EditFrameRate) 
					{
						// fill frame rate combo
						SupportMethods.FillComboBoxEntry(comboFrameRate,frameRates,m.FrameRate.ToString(),true,Editable);
					} else 
					{
						SupportMethods.ClearCombo(comboFrameRate);
					}
					comboFrameRate.Sensitive = MovieInfo.EditFrameRate;


					if (MovieInfo.EditBitRate) 
					{
						SupportMethods.FillComboBoxEntry(comboBitRate,MediaInfo.DefaultVideoBitRates,m.BitrateKbps,Editable);
					} else 
					{
						SupportMethods.ClearCombo(comboBitRate);
					}
					comboBitRate.Sensitive = MovieInfo.EditBitRate;

					SupportMethods.FillComboBox(comboContainer,typeof(VideoContainerEnum),Editable,(int)MovieInfo.TargetContainer);

					if (Editable)
					{
						chBoxesVisible = true;

						SupportMethods.FillComboBox(comboCodec,typeof(VideoCodecEnum),Editable,(int)MovieInfo.TargetVideoCodec);
					} else
					{
						SupportMethods.FillComboBox(comboCodec,new List<string>() {m.Codec}, Editable,m.Codec);
					}
					
					m.ReComputeStreamSizeByBitrate();
					labelTrackSize.Text = m.HumanReadableStreamSize;
				} else
				{				
					entryWidth.Text = String.Empty;
					entryRealWidth.Text = String.Empty;
					entryHeight.Text = String.Empty;
					entryPixelAspect.Text = String.Empty;


					SupportMethods.ClearCombo(comboCodec);
					SupportMethods.ClearCombo(comboContainer);
					labelTrackSize.Text = String.Empty;

					SupportMethods.ClearCombo(comboBitRate);
     				SupportMethods.ClearCombo(comboAspect);
					SupportMethods.ClearCombo(comboFrameRate);			 
				}

				imageCodec.Visible = comboCodec.Active>0;
				imageContainer.Visible = comboContainer.Active>0;

				frameVideooptions.Visible = 
					(MovieInfo != null) && 
					(MovieInfo.FirstVideoTrack!=null) && 
					( ((Editable) && (comboCodec.Active > 0)) || !Editable );

				chBoxAspect.Visible = chBoxResolution.Visible = chBoxBitRate.Visible = chBoxFrameRate.Visible = chBoxesVisible;

				_eventLock.Unlock();
			}
		}

		#endregion

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
				}
			}

			OnAnyValueChanged();
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
				}
			}

			OnAnyValueChanged();
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

					// reactivating disabled?
					if (chBoxResolution.Active && !MovieInfo.EditResolution) 
					{
						entryWidth.Text = m.Width.ToString ();
						entryHeight.Text = m.Height.ToString ();
					}
					if (chBoxBitRate.Active && !MovieInfo.EditBitRate)
						comboBitRate.Entry.Text = (m.BitrateKbps).ToString ();
					if (chBoxAspect.Active && !MovieInfo.EditAspect)
						comboAspect.Entry.Text = m.Aspect;
					if (chBoxFrameRate.Active && !MovieInfo.EditFrameRate)
						comboFrameRate.Entry.Text = m.FrameRate.ToString ();


					MovieInfo.EditResolution = chBoxResolution.Active;
					MovieInfo.EditAspect = chBoxAspect.Active;
					MovieInfo.EditBitRate = chBoxBitRate.Active;
					MovieInfo.EditFrameRate = chBoxFrameRate.Active;

					if (chBoxBitRate.Active) 
					{
						var bitRateTypedValue = SupportMethods.ParseDecimalValueFromValue (comboBitRate.ActiveText, MediaInfo.DefaultVideoBitRates);
						m.Bitrate = bitRateTypedValue * 1000;
					}

					if (chBoxResolution.Active) 
					{
						if (SupportMethods.IsNumeric(entryWidth.Text))					
							m.Width = Convert.ToInt32(entryWidth.Text);

						if (SupportMethods.IsNumeric (entryHeight.Text))					
							m.Height = Convert.ToInt32 (entryHeight.Text);
					}

					if (chBoxBitRate.Active) m.Aspect = comboAspect.ActiveText;

					if (chBoxFrameRate.Active) 
					{
						if (SupportMethods.IsNumeric (comboFrameRate.ActiveText))					
							m.FrameRate = SupportMethods.ToDecimal (comboFrameRate.ActiveText);
					}

					MovieInfo.TargetVideoCodec = (VideoCodecEnum)comboCodec.Active;
					MovieInfo.TargetContainer = (VideoContainerEnum)comboContainer.Active;

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
					Fill();
				}
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


		protected void OnCodecEventBoxButtonPressEvent (object o, ButtonPressEventArgs args)
		{
			if (Editable && MovieInfo!= null && comboCodec.Active>0)
			{
				var codec = (VideoCodecEnum)comboCodec.Active;
				if (MediaInfoBase.WikiVideoCodecsLinks.ContainsKey(codec))
				{
					SupportMethods.ExecuteInShell(MediaInfoBase.WikiVideoCodecsLinks[codec]);
				}
			}
		}

		protected void OnContainerEventBoxButtonPressEvent (object o, ButtonPressEventArgs args)
		{
			if (Editable && MovieInfo!= null && comboContainer.Active>0)
			{
				var container = (VideoContainerEnum)comboContainer.Active;
				if (MediaInfoBase.WikiContainerCodecsLinks.ContainsKey(container))
				{
					SupportMethods.ExecuteInShell(MediaInfoBase.WikiContainerCodecsLinks[container]);
				}
			}
		}

		protected void OnChBoxResolutionClicked (object sender, EventArgs e)
		{	
			OnAnyValueChanged();
		}


		protected void OnChBoxAspectClicked (object sender, EventArgs e)
		{
			OnAnyValueChanged();
		}

		protected void OnChBoxBitRateClicked (object sender, EventArgs e)
		{
			OnAnyValueChanged();
		}

		protected void OnChBoxFrameRateClicked (object sender, EventArgs e)
		{
			OnAnyValueChanged();
		}

		#endregion
	}
}

