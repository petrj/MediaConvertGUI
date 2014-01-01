using System;
using System.Collections.Generic;

namespace MediaConvertGUI
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WidgetGeneralMediaInfo : Gtk.Bin
	{
		#region fileds && properties

		private EventLock _eventLock = new EventLock();

		public MediaInfo _sourceMovieInfo;
		public MediaInfo _targetMovieInfo;

		public MediaInfo SourceMovieInfo 
		{ 
			get
			{
				return _sourceMovieInfo;
			}
		}

		public MediaInfo TargetMovieInfo 
		{ 
			get
			{
				return _targetMovieInfo;
			}
		}

		#endregion

		public WidgetGeneralMediaInfo ()
		{
			this.Build ();
			_sourceMovieInfo = new MediaInfo();
			_targetMovieInfo = new MediaInfo();

			comboContainer.Changed+= OnAnyValueChanged;
			comboScheme.Changed+=OnSchemeComboValueChanged;		
		}

		#region methods

		public void FillFrom(MediaInfo sourceInfo, MediaInfo targetInfo)
		{
			if (sourceInfo != null) sourceInfo.Copyto(_sourceMovieInfo,false);
			if (targetInfo != null) targetInfo.Copyto(_targetMovieInfo,false);

			Fill();
		}

		public void Fill()
		{
			if (_eventLock.Lock())
			{
				if (SourceMovieInfo != null && SourceMovieInfo.FirstVideoTrack != null)
				{
					labelDuration.Text = SourceMovieInfo.HuamReadableDuration;
					labelSize.Text = SourceMovieInfo.HumanReadableSize;
					labelBitRate.Text = SourceMovieInfo.HumanReadableOverAllBitRate;
				} else
				{			
					labelDuration.Text = String.Empty;
					labelSize.Text =  String.Empty;
					labelBitRate.Text = String.Empty;
				}

				if (TargetMovieInfo!= null)
				{
					SupportMethods.FillComboBox(comboContainer,typeof(VideoContainerEnum),true,(int)TargetMovieInfo.TargetContainer);

					var schemeStrings = new List<string>();
					schemeStrings.Add("none");
					foreach (var scheme in TargetMovieInfo.Schemes.Keys)
					{
						schemeStrings.Add(scheme);
					}
					SupportMethods.FillComboBox(comboScheme,schemeStrings,true,TargetMovieInfo.SelectedScheme);
				} else
				{
					SupportMethods.ClearCombo(comboContainer);
					SupportMethods.ClearCombo(comboScheme);
				}

				_eventLock.Unlock();
			}
		}

		#endregion

		#region events

		protected void OnSchemeComboValueChanged (object sender, EventArgs e)
		{
			if (_eventLock.Lock())
			{
				_targetMovieInfo.SelectedScheme = comboScheme.ActiveText;

				OnSchemeChanged(EventArgs.Empty);

				_eventLock.Unlock();

				Fill();
			}
		}

		protected void OnAnyValueChanged (object sender, EventArgs e)
		{
			if (_eventLock.Lock())
			{
				if (TargetMovieInfo != null)
				{
					_targetMovieInfo.TargetContainer = (VideoContainerEnum)comboContainer.Active;
				}

				_eventLock.Unlock();
			}
		}

  		public delegate void  ChangedSchemeEventHandler(object sender, EventArgs e);
	  	public event ChangedSchemeEventHandler SchemeChanged;

	  	protected virtual void OnSchemeChanged(EventArgs e) 
      	{
        	 if (SchemeChanged != null)
            	SchemeChanged(this, e);
      	}

		#endregion
	}
}

