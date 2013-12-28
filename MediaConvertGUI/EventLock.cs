using System;

namespace MediaConvertGUI
{
	public class EventLock
	{
		private bool _locked = false;

		public bool Locked
		{
			get
			{
				return _locked;
			}
		}

		public bool Lock()
		{
			if (_locked)
				return false;

			_locked = true;

			return true;
		}

		public void Unlock()
		{
			_locked = false;
		}
	}
}

