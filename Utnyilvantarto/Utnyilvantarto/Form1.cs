using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utnyilvantarto
{
    public partial class Form1 : Form
    {
        private int X, Y, currX, currY;
        bool move = false;
        ProgressBar pBar = new ProgressBar();
        loadForm lF;
        partnerForm pForm;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e) //exitgomb
        {
            System.Windows.Forms.Application.Exit();
        }
        private void savePoz(MouseEventArgs e) //egérpozíció elmentése
        {
            move = true;
            X = Cursor.Position.X;
            Y = Cursor.Position.Y;
            currX = e.X;
            currY = e.Y;
        }
        private void movePoz(MouseEventArgs e) //mozgatás egérpozíció alapján
        {
            if (move)
            {
                this.Location = new Point(X, Y);
                X = Cursor.Position.X - currX;
                Y = Cursor.Position.Y - currY;
            }
        }
        private void stopPoz() //stop move
        {
            move = false;
        }

        //fejlec panel mozgatasa(panel1)
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            savePoz(e);
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            movePoz(e);
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            stopPoz();
        }
        //fejlec label mozgatasa(label1)
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            savePoz(e);
        }
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            movePoz(e);
        }
        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            stopPoz();
        }


        private void myButton1_Click(object sender, EventArgs e)
        {
            partnerForm pf = new partnerForm();
            pf.Show();
        }



        private void myButton2_Click(object sender, EventArgs e)
        {
            string Api_key = "";
            string origin = "46.184697,18.957354";
            string dest = "Budapest";

            //https://maps.googleapis.com/maps/api/directions/json?origin=Baja&destination=Fels%C5%91szentiv%C3%A1n&key= 
            WebClient c = new WebClient();
            var data = c.DownloadString(string.Format("https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&key={2}",origin,dest,Api_key));
            //Console.WriteLine(data);
            JObject o = JObject.Parse(data);
            Console.WriteLine(o["routes"][0]["legs"][0]["distance"]["value"]);
        }

        private void myButton3_Click(object sender, EventArgs e)
        {
            partnerForm pf = new partnerForm();
            pf.mainTabControl.SelectedIndex = 1;
            pf.Show();
        }

    }
}
