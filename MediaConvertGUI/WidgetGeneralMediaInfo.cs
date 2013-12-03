using System;
using System.Collections.Generic;

namespace MediaConvertGUI
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WidgetGeneralMediaInfo : Gtk.Bin
	{
		public MediaInfo _sourceMovieInfo;
		public MediaInfo _targetMovieInfo;

		public MediaInfo SourceMovieInfo 
		{ 
			get
			{
				return _sourceMovieInfo;
			}
			set
			{
				_sourceMovieInfo = value;
				Fill();
			}
		}

		public MediaInfo TargetMovieInfo 
		{ 
			get
			{
				return _targetMovieInfo;
			}
			set
			{
				_targetMovieInfo = value;
				Fill();
			}
		}

		public WidgetGeneralMediaInfo ()
		{
			this.Build ();
			comboContainer.Changed+= OnAnyValueChanged;
		}

		protected void OnAnyValueChanged (object sender, EventArgs e)
		{
			if (TargetMovieInfo != null)
			{
				TargetMovieInfo.TargetContainer = (VideoContainerEnum)comboContainer.Active;
			}
		}

		private void Fill()
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
			} else
			{
				SupportMethods.FillComboBox(comboContainer,new List<string>() {},false,null);
			}
		}
	}
}

