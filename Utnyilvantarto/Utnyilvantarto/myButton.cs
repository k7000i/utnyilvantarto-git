using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utnyilvantarto
{
    class myButton : Button
    {

        public myButton()
        {
            
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.FromArgb(156, 21, 14);
            this.Font = new Font("Segoe WP", 12, FontStyle.Bold);
            this.ImageAlign = ContentAlignment.BottomRight;
        }

    }
    class loadForm : Form
    {
        private ProgressBar pBar = new ProgressBar();
        private Label loadLabel = new Label();
        private Timer timer = new Timer();
        public BackgroundWorker bw = new BackgroundWorker();

        public loadForm()
        {
            
            //form formázása
            this.Width = 250;
            this.Height = 100;
            this.Controls.Add(pBar);
            this.Controls.Add(loadLabel);
            this.ControlBox = false;
            this.ShowIcon = false;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;

            //form elemek formázása
            pBar.Location = new Point(47, 50);
            pBar.Width = 150;
            loadLabel.Text = "Töltés...";
            loadLabel.Font = new Font(loadLabel.Font.FontFamily, 14);
            loadLabel.TextAlign = ContentAlignment.MiddleCenter;
            loadLabel.Width = 150;
            loadLabel.Location = new Point(47, 18);

            //folyamatok
            timer.Tick += timer_Tick;
            StartBackgroundWork();
        }

        public void StartBackgroundWork()
        {
            if (Application.RenderWithVisualStyles)
                pBar.Style = ProgressBarStyle.Marquee;
            else
            {
                pBar.Style = ProgressBarStyle.Continuous;
                pBar.Maximum = 100;
                pBar.Value = 0;
                timer.Enabled = true;
            }
            bw.RunWorkerAsync();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (pBar.Value < pBar.Maximum)
                pBar.Increment(5);
            else
                pBar.Value = pBar.Minimum;
        }
    }
}
