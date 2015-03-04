using System;
using System.IO;
using System.Collections.Generic;

namespace MediaConvertGUI
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WidgetGeneralMediaInfo : Gtk.Bin
	{
		#region fileds && properties

		private EventLock _eventLock = new EventLock();

		public MediaInfo _sourceMovieInfo;

		public MediaInfo SourceMovieInfo 
		{ 
			get
			{
				return _sourceMovieInfo;
			}
		}

		#endregion

		public WidgetGeneralMediaInfo ()
		{
			this.Build ();
			_sourceMovieInfo = new MediaInfo();

		}

		#region methods

		public void FillFrom(MediaInfo sourceInfo)
		{
			if (sourceInfo != null) 
			{
				sourceInfo.Copyto(_sourceMovieInfo,false);
			} else
			{
				SourceMovieInfo.Clear();
			}

			Fill();
		}

		public void Fill()
		{
			if (_eventLock.Lock())
			{
				if (SourceMovieInfo != null)
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

				_eventLock.Unlock();
			}
		}

		#endregion

	}
}

