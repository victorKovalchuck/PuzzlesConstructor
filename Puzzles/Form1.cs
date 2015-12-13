using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XnaFan.ImageComparison;
using Core;

namespace Puzzles
{
    public partial class Puzzles : Form
    {
        BuildImage puzzlesOrder;
        string[] arrAllFiles;
        Screen myScreens;
        bool clicked;
        public Puzzles()
        {
            InitializeComponent();
            InitControls();           
        }

        public void InitControls()
        {
            myScreens = Screen.FromControl(this);   
            puzzlesOrder = new BuildImage(UnorderPictureBox, OrderedPictureBox);            
            OrderedPictureBox.Location = new Point(myScreens.WorkingArea.Width - OrderedPictureBox.Width - 10, 10);
            ClickLabal.Location = new Point(UnorderPictureBox.Location.X + (UnorderPictureBox.Width - ClickLabal.Width) / 2, UnorderPictureBox.Location.Y + UnorderPictureBox.Height / 2 - ClickLabal.Height);
            Loading.Location = new Point((myScreens.WorkingArea.Width - Construct.Width + Loading.Width-5) / 2, 320);
        }

        public void GetImagesFromFolder()
        {

            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sFileName = openFileDialog1.FileName;
                arrAllFiles = openFileDialog1.FileNames;
                if (arrAllFiles != null)
                {
                    if (puzzlesOrder.ConstructUnorderedPicture(arrAllFiles))
                    {
                        Construct.Location = new Point((myScreens.WorkingArea.Width - Construct.Width) / 2, (500 - Construct.Height) / 2);
                        Construct.Visible = true;

                        ClickLabal.Visible = false;
                    }
                    else
                    {
                        ClickLabal.Text = "Please insert valid images";
                    }
                }
            }
        }
      

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!clicked)
            {
                clicked = true;
            }
            else
            {
                UnorderPictureBox.Image = null;            
              
                UnorderPictureBox.Size = new System.Drawing.Size(300, 300);       
                UnorderPictureBox.BorderStyle = BorderStyle.FixedSingle;
                ClickLabal.Visible = true;
                ClickLabal.Text = "Click here to load images";
                InitControls();
            }
            GetImagesFromFolder();
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            Loading.Visible = true;
            this.Update();
            puzzlesOrder.ConstructOrderedPicture(arrAllFiles);
            Loading.Visible = false;
            OrderedPictureBox.Location = new Point(myScreens.WorkingArea.Width - OrderedPictureBox.Image.Width - 10,10);
            OrderedPictureBox.BorderStyle = BorderStyle.FixedSingle;
        }

    }
}
