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

			ReloadSchemes();

			comboScheme.Changed+=OnSchemeComboValueChanged;		
		}

		#region methods

		public void ReloadSchemes(string selectedScheme = "none")
		{
			var schemesPath = System.IO.Path.Combine(SupportMethods.AppPath,"Schemes/");
			var schemes = Directory.GetFiles(schemesPath,"*.xml");

			var schemeStrings = new List<string>();
			schemeStrings.Add("none");

			foreach (var sch in schemes)
			{
				schemeStrings.Add(System.IO.Path.GetFileNameWithoutExtension(sch));
			}

			SupportMethods.FillComboBox(comboScheme,schemeStrings,true, selectedScheme);
		}

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

		#region events

		protected void OnSchemeComboValueChanged (object sender, EventArgs e)
		{
			if (_eventLock.Lock())
			{
				OnSchemeChanged(new StringEventArgs(comboScheme.ActiveText));

				_eventLock.Unlock();

				Fill();
			}
		}

		public delegate void  ChangedSchemeEventHandler(object sender, EventArgs e);
	  	public event ChangedSchemeEventHandler SchemeChanged;

		protected virtual void OnSchemeChanged(StringEventArgs e) 
      	{
        	 if (SchemeChanged != null)
            	SchemeChanged(this, e);
      	}

		#endregion
	}
}

