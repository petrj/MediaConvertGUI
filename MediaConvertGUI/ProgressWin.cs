using System;

namespace MediaConvertGUI
{
	public partial class ProgressWin : Gtk.Window
	{
		public bool AbortRequest { get; set; }

		public ProgressWin () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			this.Shown += delegate { AbortRequest = false; buttonClose.Sensitive = false; buttonAbort.Sensitive = true; };
		}

		public double CurrentFilePercents
		{
			set
			{
				try
				{
				progressCurrentFile.Fraction = value/(double)100;
				labelCurrentFilePercents.Text = Convert.ToInt32(value).ToString()+" %";
				} catch (Exception)
				{
					labelCurrentFilePercents.Text = "";
				}
			}
		}

		public double CurrentFilePassPercents
		{
			set
			{
				progressCurrentPass.Fraction = value/(double)100;
				labelCurrentPassPercents.Text = Convert.ToInt32(value).ToString()+" %";

			}
		}

		public double TotalPercents
		{
			set
			{
				progressTotal.Fraction = value/(double)100;
				labelTotalPercents.Text = Convert.ToInt32(value).ToString()+" %";
			}
		}

		public static string TimeSpanAsHMS(TimeSpan value)
		{
			return  value.Hours.ToString().PadLeft(2,'0') + ":" + 
					value.Minutes.ToString().PadLeft(2,'0') + ":" +  
					value.Seconds.ToString().PadLeft(2,'0');

		}

		public void SetPercents(double total, double currentFile,double currentPass, DateTime startTime, string fName = "", string passString = "",string totalString = "")
		{
			TotalPercents = total;
			CurrentFilePercents = currentFile;
			CurrentFilePassPercents = currentPass;

			if (fName!=null && fName.Length>30)
			{
				fName = fName.Substring(0,15) + "......" + fName.Substring(fName.Length-15,15);
			}

			progressCurrentFile.Text = (fName != String.Empty) ? fName : String.Empty;
			progressCurrentPass.Text = (passString != String.Empty) ? passString : String.Empty;
			progressTotal.Text = (totalString != String.Empty) ? totalString : String.Empty;
			

			var timeElapsed = DateTime.Now - startTime;
			var finishTime = startTime;

			var totalTime = new TimeSpan();
			var remainTime = new TimeSpan();
			if (total>0)
			{
				//  3% ..... 50 s
				//  100% .....x s
				var totalSeconds =  timeElapsed.TotalSeconds*(100/total);
				finishTime = startTime.AddSeconds(totalSeconds);

				totalTime = finishTime - startTime;
				remainTime = finishTime - DateTime.Now;
			}

			labelTotal.Text = TimeSpanAsHMS(totalTime);
			labelElapsed.Text = TimeSpanAsHMS(timeElapsed);
			labelRemain.Text = TimeSpanAsHMS(remainTime);

			labelStart.Text = startTime.ToString("HH:mm:ss");
			labelFinish.Text = finishTime.ToString("HH:mm:ss");

			if (total==100)
			{
				buttonClose.Sensitive = true; 
				buttonAbort.Sensitive = false;
			}

		}	

		protected void OnButtonAbortClicked (object sender, EventArgs e)
		{
			if (Dialogs.ConfirmDialog("Are you sure to abort all?"))
			{
				AbortRequest = true;
			}
		}

		protected void OnButtonCloseClicked (object sender, EventArgs e)
		{
			Hide();
		}

	}
}

