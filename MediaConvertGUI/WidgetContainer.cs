using System;
using Gtk;
using System.Collections.Generic;

namespace MediaConvertGUI
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WidgetContainer : Gtk.Bin
	{
		private bool _editable = false;
		private MediaInfo _info;
		private EventLock _eventLock = new EventLock();
	
		public WidgetContainer ()
		{
			this.Build ();
			
			Editable = false;
			_info = new MediaInfo();

			comboContainer.Changed+=delegate { OnAnyValueChanged(); };			
			eventBoxContainer.ButtonPressEvent += OnContainerEventBoxButtonPressEvent;
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
				
				SupportMethods.SetAvailability(comboContainer as Gtk.Widget,_editable);
				
				Fill();
			}
		}		
		
		public MediaInfo Info 
		{ 
			get
			{
				return _info;
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
		}
		
		public void Fill()
		{
			if (_eventLock.Lock())
			{
				if (Info != null)
				{				
					if (Editable)
					{
						SupportMethods.FillComboBox(comboContainer,typeof(ContainerEnum),Editable,(int)Info.TargetContainer);
					} else
					{
						SupportMethods.FillComboBox(comboContainer,new List<string>() {Info.TargetContainer.ToString()}, Editable,Info.TargetContainer.ToString());
					}					
					
				} else
				{			
					
					SupportMethods.ClearCombo(comboContainer);										
				}
				
				imageContainer.Visible = comboContainer.Active>0;		
				_eventLock.Unlock();
			}
		}
		
		protected void OnContainerEventBoxButtonPressEvent (object o, ButtonPressEventArgs args)
		{
			if (Editable && Info!= null && comboContainer.Active>0)
			{
				var container = (ContainerEnum)comboContainer.Active;
				if (MediaInfoBase.WikiContainerCodecsLinks.ContainsKey(container))
				{
					SupportMethods.ExecuteInShell(MediaInfoBase.WikiContainerCodecsLinks[container]);
				}
			}
		}
		
		
		private void OnAnyValueChanged()
		{
			if (Editable && Info != null)
			{
				if (_eventLock.Lock())
				{						
					Info.TargetContainer = (ContainerEnum)comboContainer.Active;
										
					_eventLock.Unlock();
					
					Fill();
				}
			}
		}		
		
	}
}

