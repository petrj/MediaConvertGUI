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
		}

		public MediaInfo TargetMovieInfo 
		{ 
			get
			{
				return _targetMovieInfo;
			}
		}

		public WidgetGeneralMediaInfo ()
		{
			this.Build ();
			_sourceMovieInfo = new MediaInfo();
			_targetMovieInfo = new MediaInfo();
			comboContainer.Changed+= OnAnyValueChanged;
		}

		protected void OnAnyValueChanged (object sender, EventArgs e)
		{
			if (TargetMovieInfo != null)
			{
				_targetMovieInfo.TargetContainer = (VideoContainerEnum)comboContainer.Active;
			}
		}

		public void FillFrom(MediaInfo sourceInfo, MediaInfo targetInfo)
		{
			if (sourceInfo != null) sourceInfo.Copyto(_sourceMovieInfo,false);
			if (targetInfo != null) targetInfo.Copyto(_targetMovieInfo,false);

			Fill();
		}

		public void Fill()
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

