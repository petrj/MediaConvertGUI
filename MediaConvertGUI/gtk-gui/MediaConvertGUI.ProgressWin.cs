
// This file has been generated by the GUI designer. Do not modify.
namespace MediaConvertGUI
{
	public partial class ProgressWin
	{
		private global::Gtk.Fixed @fixed;
		
		private global::Gtk.Label labelTimeElapsed;
		
		private global::Gtk.Label labelTimeRemain;
		
		private global::Gtk.Label labelTimeTotal;
		
		private global::Gtk.Label labelElapsed;
		
		private global::Gtk.Label labelRemain;
		
		private global::Gtk.Label labelTotal;
		
		private global::Gtk.Label labelStartTime;
		
		private global::Gtk.Label labelFinishTime;
		
		private global::Gtk.Label labelStart;
		
		private global::Gtk.Label labelFinish;
		
		private global::Gtk.Label labelCurrentFilePercents;
		
		private global::Gtk.Button buttonAbort;
		
		private global::Gtk.Button buttonClose;
		
		private global::Gtk.Label labelAll;
		
		private global::Gtk.Label labelCurrentFile;
		
		private global::Gtk.Label labelCurrentPass;
		
		private global::Gtk.ProgressBar progressTotal;
		
		private global::Gtk.ProgressBar progressCurrentFile;
		
		private global::Gtk.ProgressBar progressCurrentPass;
		
		private global::Gtk.Label labelCurrentPassPercents;
		
		private global::Gtk.Label labelTotalPercents;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MediaConvertGUI.ProgressWin
			this.Name = "MediaConvertGUI.ProgressWin";
			this.Title = global::Mono.Unix.Catalog.GetString ("Progress");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child MediaConvertGUI.ProgressWin.Gtk.Container+ContainerChild
			this.@fixed = new global::Gtk.Fixed ();
			this.@fixed.Name = "fixed";
			this.@fixed.HasWindow = false;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelTimeElapsed = new global::Gtk.Label ();
			this.labelTimeElapsed.Name = "labelTimeElapsed";
			this.labelTimeElapsed.LabelProp = global::Mono.Unix.Catalog.GetString ("Time elapsed");
			this.@fixed.Add (this.labelTimeElapsed);
			global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelTimeElapsed]));
			w1.X = 15;
			w1.Y = 110;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelTimeRemain = new global::Gtk.Label ();
			this.labelTimeRemain.Name = "labelTimeRemain";
			this.labelTimeRemain.LabelProp = global::Mono.Unix.Catalog.GetString ("Time remain");
			this.@fixed.Add (this.labelTimeRemain);
			global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelTimeRemain]));
			w2.X = 15;
			w2.Y = 130;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelTimeTotal = new global::Gtk.Label ();
			this.labelTimeTotal.Name = "labelTimeTotal";
			this.labelTimeTotal.LabelProp = global::Mono.Unix.Catalog.GetString ("Time total");
			this.@fixed.Add (this.labelTimeTotal);
			global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelTimeTotal]));
			w3.X = 15;
			w3.Y = 150;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelElapsed = new global::Gtk.Label ();
			this.labelElapsed.Name = "labelElapsed";
			this.labelElapsed.LabelProp = global::Mono.Unix.Catalog.GetString ("00:00:00");
			this.@fixed.Add (this.labelElapsed);
			global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelElapsed]));
			w4.X = 120;
			w4.Y = 110;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelRemain = new global::Gtk.Label ();
			this.labelRemain.Name = "labelRemain";
			this.labelRemain.LabelProp = global::Mono.Unix.Catalog.GetString ("00:00:00");
			this.@fixed.Add (this.labelRemain);
			global::Gtk.Fixed.FixedChild w5 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelRemain]));
			w5.X = 120;
			w5.Y = 130;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelTotal = new global::Gtk.Label ();
			this.labelTotal.Name = "labelTotal";
			this.labelTotal.LabelProp = global::Mono.Unix.Catalog.GetString ("00:00:00");
			this.@fixed.Add (this.labelTotal);
			global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelTotal]));
			w6.X = 120;
			w6.Y = 150;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelStartTime = new global::Gtk.Label ();
			this.labelStartTime.Name = "labelStartTime";
			this.labelStartTime.LabelProp = global::Mono.Unix.Catalog.GetString ("Started");
			this.@fixed.Add (this.labelStartTime);
			global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelStartTime]));
			w7.X = 15;
			w7.Y = 90;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelFinishTime = new global::Gtk.Label ();
			this.labelFinishTime.Name = "labelFinishTime";
			this.labelFinishTime.LabelProp = global::Mono.Unix.Catalog.GetString ("Finish");
			this.@fixed.Add (this.labelFinishTime);
			global::Gtk.Fixed.FixedChild w8 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelFinishTime]));
			w8.X = 15;
			w8.Y = 170;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelStart = new global::Gtk.Label ();
			this.labelStart.Name = "labelStart";
			this.labelStart.LabelProp = global::Mono.Unix.Catalog.GetString ("00:00:00");
			this.@fixed.Add (this.labelStart);
			global::Gtk.Fixed.FixedChild w9 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelStart]));
			w9.X = 120;
			w9.Y = 90;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelFinish = new global::Gtk.Label ();
			this.labelFinish.Name = "labelFinish";
			this.labelFinish.LabelProp = global::Mono.Unix.Catalog.GetString ("00:00:00");
			this.@fixed.Add (this.labelFinish);
			global::Gtk.Fixed.FixedChild w10 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelFinish]));
			w10.X = 120;
			w10.Y = 170;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelCurrentFilePercents = new global::Gtk.Label ();
			this.labelCurrentFilePercents.Name = "labelCurrentFilePercents";
			this.labelCurrentFilePercents.LabelProp = global::Mono.Unix.Catalog.GetString ("0%");
			this.@fixed.Add (this.labelCurrentFilePercents);
			global::Gtk.Fixed.FixedChild w11 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelCurrentFilePercents]));
			w11.X = 410;
			w11.Y = 35;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.buttonAbort = new global::Gtk.Button ();
			this.buttonAbort.CanFocus = true;
			this.buttonAbort.Name = "buttonAbort";
			this.buttonAbort.UseUnderline = true;
			this.buttonAbort.Label = global::Mono.Unix.Catalog.GetString ("Abort");
			global::Gtk.Image w12 = new global::Gtk.Image ();
			w12.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-stop", global::Gtk.IconSize.Menu);
			this.buttonAbort.Image = w12;
			this.@fixed.Add (this.buttonAbort);
			global::Gtk.Fixed.FixedChild w13 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.buttonAbort]));
			w13.X = 310;
			w13.Y = 180;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.buttonClose = new global::Gtk.Button ();
			this.buttonClose.CanFocus = true;
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.UseUnderline = true;
			this.buttonClose.Label = global::Mono.Unix.Catalog.GetString ("Close");
			global::Gtk.Image w14 = new global::Gtk.Image ();
			w14.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-close", global::Gtk.IconSize.Menu);
			this.buttonClose.Image = w14;
			this.@fixed.Add (this.buttonClose);
			global::Gtk.Fixed.FixedChild w15 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.buttonClose]));
			w15.X = 380;
			w15.Y = 180;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelAll = new global::Gtk.Label ();
			this.labelAll.Name = "labelAll";
			this.labelAll.LabelProp = global::Mono.Unix.Catalog.GetString ("Overall");
			this.@fixed.Add (this.labelAll);
			global::Gtk.Fixed.FixedChild w16 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelAll]));
			w16.X = 15;
			w16.Y = 10;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelCurrentFile = new global::Gtk.Label ();
			this.labelCurrentFile.Name = "labelCurrentFile";
			this.labelCurrentFile.LabelProp = global::Mono.Unix.Catalog.GetString ("Current file");
			this.@fixed.Add (this.labelCurrentFile);
			global::Gtk.Fixed.FixedChild w17 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelCurrentFile]));
			w17.X = 15;
			w17.Y = 35;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelCurrentPass = new global::Gtk.Label ();
			this.labelCurrentPass.Name = "labelCurrentPass";
			this.labelCurrentPass.LabelProp = global::Mono.Unix.Catalog.GetString ("Current pass");
			this.@fixed.Add (this.labelCurrentPass);
			global::Gtk.Fixed.FixedChild w18 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelCurrentPass]));
			w18.X = 15;
			w18.Y = 60;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.progressTotal = new global::Gtk.ProgressBar ();
			this.progressTotal.WidthRequest = 280;
			this.progressTotal.HeightRequest = 15;
			this.progressTotal.Name = "progressTotal";
			this.@fixed.Add (this.progressTotal);
			global::Gtk.Fixed.FixedChild w19 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.progressTotal]));
			w19.X = 120;
			w19.Y = 10;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.progressCurrentFile = new global::Gtk.ProgressBar ();
			this.progressCurrentFile.WidthRequest = 280;
			this.progressCurrentFile.HeightRequest = 15;
			this.progressCurrentFile.Name = "progressCurrentFile";
			this.@fixed.Add (this.progressCurrentFile);
			global::Gtk.Fixed.FixedChild w20 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.progressCurrentFile]));
			w20.X = 120;
			w20.Y = 35;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.progressCurrentPass = new global::Gtk.ProgressBar ();
			this.progressCurrentPass.WidthRequest = 280;
			this.progressCurrentPass.HeightRequest = 15;
			this.progressCurrentPass.Name = "progressCurrentPass";
			this.@fixed.Add (this.progressCurrentPass);
			global::Gtk.Fixed.FixedChild w21 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.progressCurrentPass]));
			w21.X = 120;
			w21.Y = 60;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelCurrentPassPercents = new global::Gtk.Label ();
			this.labelCurrentPassPercents.Name = "labelCurrentPassPercents";
			this.labelCurrentPassPercents.LabelProp = global::Mono.Unix.Catalog.GetString ("0%");
			this.@fixed.Add (this.labelCurrentPassPercents);
			global::Gtk.Fixed.FixedChild w22 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelCurrentPassPercents]));
			w22.X = 410;
			w22.Y = 60;
			// Container child fixed.Gtk.Fixed+FixedChild
			this.labelTotalPercents = new global::Gtk.Label ();
			this.labelTotalPercents.Name = "labelTotalPercents";
			this.labelTotalPercents.LabelProp = global::Mono.Unix.Catalog.GetString ("0%");
			this.@fixed.Add (this.labelTotalPercents);
			global::Gtk.Fixed.FixedChild w23 = ((global::Gtk.Fixed.FixedChild)(this.@fixed [this.labelTotalPercents]));
			w23.X = 410;
			w23.Y = 10;
			this.Add (this.@fixed);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 451;
			this.DefaultHeight = 223;
			this.Show ();
			this.buttonAbort.Clicked += new global::System.EventHandler (this.OnButtonAbortClicked);
			this.buttonClose.Clicked += new global::System.EventHandler (this.OnButtonCloseClicked);
		}
	}
}
