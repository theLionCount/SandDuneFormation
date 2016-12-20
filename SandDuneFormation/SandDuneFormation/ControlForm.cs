using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.IO;
using System.Threading;

namespace SandDuneFormation
{
    public partial class ControlForm : Form
    {
        Main main;
        float windSpeed;
        Vector2 winddir;
        public ControlForm(Main main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void ControlForm_Load(object sender, EventArgs e)
        {
            windspeedBar.Value = 400;
            windSpeedBox.Text = "4";
            windSpeed = 4;
            winddir = new Vector2(1, 0);
            main.setProbability(0.3f);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            windDirPanel.Left = e.X;
            windDirPanel.Top = e.Y;
            winddir = new Vector2((float)e.X, (float)e.Y);
            winddir.Normalize();
            main.setWindDir(winddir*windSpeed);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            windSpeed = windspeedBar.Value / 100f;
            windSpeedBox.Text = windSpeed.ToString();
            main.setWindDir(winddir * windSpeed);
        }

        private void windSpeedBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                windSpeed = float.Parse(windSpeedBox.Text);
                windspeedBar.Value = (int)(windSpeed * 100);
                main.setWindDir(winddir * windSpeed);
            }
            catch (Exception ex) 
            {
                windSpeedBox.Text = windSpeed.ToString();
            }
            finally { }
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            if (File.Exists(fileNameBox.Text))
                main.loadNewHeightMap(fileNameBox.Text);
            else
                MessageBox.Show("Invalid filename!");
            //openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            fileNameBox.Invoke(new MethodInvoker(delegate { fileNameBox.Text = openFileDialog1.FileName; }));
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(tt => openFileDialog1.ShowDialog());
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        float maxheight=16;
        private void mxHBox_TextChanged(object sender, EventArgs e)
        {
            try 
            {
                maxheight = float.Parse(maxHBox.Text);
            }
            catch (Exception ex) 
            {
                maxHBox.Text = maxheight.ToString();
            }
            main.setConditions(maxheight, cellheight, avheight);
        }

        float cellheight=8;
        private void cellHBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cellheight = float.Parse(cellHBox.Text);
            }
            catch (Exception ex)
            {
                cellHBox.Text = cellheight.ToString();
            }
            main.setConditions(maxheight, cellheight, avheight);
        }

        float avheight = 2;
        private void avalancheHBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                avheight = float.Parse(avalancheHBox.Text);
            }
            catch (Exception ex)
            {
                avalancheHBox.Text = avheight.ToString();
            }
            main.setConditions(maxheight, cellheight, avheight);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(tt => saveFileDialog1.ShowDialog());
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            main.saveHMap(saveFileDialog1.FileName);
        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {
            main.Pause();
        }

        private void resumeBtn_Click(object sender, EventArgs e)
        {
            main.Resume();
        }

        int blur;
        private void blurBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                blur = int.Parse(blurBox1.Text);
            }
            catch (Exception ex)
            {
                blurBox1.Text = blur.ToString();
            }
            main.setBlur(blur);
        }

        private void setBtn_Click(object sender, EventArgs e)
        {
            try
            {
                float cp = float.Parse(pBox.Text);
                if (cp > 0 && cp <= 1)
                    main.setProbability(cp);
            }
            catch(Exception ex){}
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            
        }
    }
}
