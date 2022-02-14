using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace ObjectDetection
{
    public partial class MainFrm : Form
    {
        Bitmap bt;
        public static string filename = "";
        public static int nnflag = 0;
        Color crr = new Color();
        public MainFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Select input Image file 
                //openFileDialog1.ShowDialog();
                //pictureBox1.BackgroundImage = Image.FromFile(openFileDialog1.FileName);  // Display input image in picture box 
                //button2.Enabled = true;
                //grayscaleToolStripMenuItem.Enabled = true;
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            bt = new Bitmap(pictureBox1.BackgroundImage);
            grayscale(bt);// grayscale fuction call
            button4.Enabled = true;
            noiseRemovalToolStripMenuItem.Enabled = true;
            Cursor.Current = Cursors.Default;
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bt = new Bitmap(pictureBox1.BackgroundImage);
            binarization(bt);// Binarization function call
          //  noise(new Bitmap(pictureBox4.Image));
        }
        public void grayscale(Bitmap bt)
        {

            for (int i = 0; i < bt.Width; i++)
            {
                for (int j = 0; j < bt.Height; j++)
                {
                    Color c = bt.GetPixel(i, j); //Get Color Value of current pixel
                    int   r = c.R;
                    int  g = c.G;
                    int  b = c.B;

                    //r = r * 0.21;
                    //g = g * 0.71;
                    //b = b * 0.07;

                  

                    int  avg = (r+g+b)/3; //Calculate AVG
                   
                    bt.SetPixel(i, j, Color.FromArgb(avg,avg,avg)); //Assign AVG value


                }
            }
            pictureBox2.Image = bt;
        }
      
             public void noise(Bitmap bt)
        {
            for (int i = 0; i < bt.Height-5; i++)
            {
                for (int j = 0; j < bt.Width-5; j++)
                {
                    int f = 0;
                          
                    for (int w1 = i; w1 < i + 5; w1++)
                    {
                        for (int w2 = j; w2 < j + 5; w2++)
                        {
                            Color c1 = bt.GetPixel(w2, w1);
                            if (c1.R > 250)
                            {
                                bt.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                            }
                        }
                    }
                }
            }
            pictureBox4.BackgroundImage = bt;
        
        }
        public void noiseremoval(Bitmap bt)
        {
            for (int i = 0; i < bt.Width; i++)
            {
                for (int j = 0; j < bt.Height; j++)
                {
                    Color c = bt.GetPixel(i, j);
                    int r = c.R;
                    int g = c.G;
                    int b = c.B;

                    if (r < 200) // Check Color Pixel value
                    {
                       bt.SetPixel(i, j, Color.FromArgb(r, g, b));
                    }
                    else
                    {
                        bt.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                }
            }
            //pictureBox3.BackgroundImage = bt;
        }
        public void binarization(Bitmap bt)
        {
            for (int i = 0; i < bt.Width; i++)
            {
                for (int j = 0; j < bt.Height; j++)
                {
                    Color c = bt.GetPixel(i, j);
                    int r = c.R;
                    int g = c.G;
                    int b = c.B;

                    if (r < 200)
                    {
                        bt.SetPixel(i, j, Color.FromArgb(0, 0, 0)); // Set Pixel Value to pure black
                    }
                    else
                    {
                        bt.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                }
            }
            pictureBox4.Image  = bt;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(null, null);
        }

        private void noiseRemovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click(null, null);
        }

        private void binarizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_Click(null, null);
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (Directory.Exists(Application.StartupPath + "\\word\\"))
            {
                Directory.Delete(Application.StartupPath + "\\word\\",true );
            }
            if (Directory.Exists(Application.StartupPath + "\\char\\"))
            {
                Directory.Delete(Application.StartupPath + "\\char\\", true);
            }

            if (Directory.Exists(Application.StartupPath + "\\segment\\"))
            {
                Directory.Delete(Application.StartupPath + "\\segment\\",true);
            }

            Directory.CreateDirectory(Application.StartupPath + "\\word\\");
            Directory.CreateDirectory(Application.StartupPath + "\\segment\\");
            Directory.CreateDirectory(Application.StartupPath + "\\char\\");

            int miny = 0;
            int maxy = 0;
            int flg = 0;
            int flg1 = 0;
            int temp = 0;
            Bitmap bt = new Bitmap (pictureBox4.Image );

            for (int i = 0; i < bt.Height; i++)
            {
                for (int j = 0; j < bt.Width; j++)
                {
                    Color cr = bt.GetPixel(j,i);

                    if (flg1 == 0)
                    {
                        if (cr.R < 255)
                        {
                            flg1 = 1;
                        }
                        if (cr.R < 255 && flg != 1)
                        {
                            miny = i;
                            flg = 1;

                        }

                       
                    }

                    
                }
                if (flg1 != 1 && flg != 0)
                {
                    maxy = i;

                    if (maxy != 0 && miny != 0 && (maxy - miny)>25)
                    {
                        Bitmap newbt = new Bitmap(bt.Width ,maxy -miny );

                        int xx = 0;
                        int yy = 0;
                        for (int x = 0; x < bt.Width; x++)
                        {
                            yy = 0;
                            for (int y = miny; y < maxy; y++)
                            {
                               Color cr =  bt.GetPixel(x,y);
                               
                                newbt.SetPixel(xx,yy,cr);
                                yy++;
                            }
                            xx++;
                        }
                        newbt.Save(Application.StartupPath +"\\segment\\segment"+temp+".jpg");
                        temp++;
                        
                       
                    }
                    miny = 0;
                    maxy = 0;
                    flg = 0;
                    flg1 = 0;
                }
                flg1 = 0;
            }


            int minx = 0;
            int maxx = 0;
             flg = 0;
             flg1 = 0;
             temp = 0;
            string []images = Directory.GetFiles (Application.StartupPath +"\\segment");

            for( int l=0;l<images.Length ;l++)
            {
                Bitmap btt = new Bitmap (images[l]);

                for( int i=0;i<btt.Width ;i++)
                {
                    for( int j=0;j<btt.Height ;j++)
                    {
                        Color cr = btt.GetPixel (i,j);

                        if (flg1 == 0)
                        {
                            if (cr.R < 255)
                            {
                                flg1 = 1;
                            }
                        if (cr.R < 255 && flg != 1)
                        {
                            minx = i;
                            flg = 1;

                        }
                        }

                    }
                    if (flg1 != 1 && flg != 0)
                    {
                        maxx= i;

                        if (maxx != 0 && minx != 0 && (maxx -minx)>25)
                        {
                            Bitmap newbt = new Bitmap( maxx - minx, btt.Height);



                            int xx = 0;
                            int yy = 0;
                            for (int x = minx; x <maxx; x++)
                            {
                                yy = 0;
                                for (int y =0; y < btt.Height; y++)
                                {
                                    Color cr = btt.GetPixel(x, y);

                                    newbt.SetPixel(xx, yy, cr);
                                    yy++;
                                }
                                xx++;
                            }
                            newbt.Save(Application.StartupPath + "\\word\\" + temp + ".jpg");
                            temp++;
                          

                        }
                        minx = 0;
                        maxx = 0;
                        flg = 0;
                        flg1 = 0;

                    }
                    flg1 = 0;
                }

            

            }
            wordprocess();
           // button6_Click(null, null);
            MessageBox.Show("segment are saved in bin folder");

        }
        public void wordprocess()
        {
            if (Directory.Exists(Application.StartupPath + "\\wordp\\"))
            {
                Directory.Delete(Application.StartupPath + "\\wordp\\", true);
            }

            Directory.CreateDirectory(Application.StartupPath + "\\wordp\\");

              string[] words = Directory.GetFiles(Application .StartupPath +"\\word");
            int temp = 0;
            for (int l = 0; l < words.Length; l++)
            {
                string pth = Application.StartupPath + "\\word\\" + l + ".jpg";
                Bitmap btt = new Bitmap(pth);
                int miny = btt.Height-1;
                int maxy = 0;
                for (int x = 0; x < btt.Width; x++)
                {
                    for (int y = 0; y < btt.Height; y++)
                    {
                        Color cr = btt.GetPixel(x, y);
                        if (cr.R < 255)
                        {
                            if (miny > y)
                            {
                                miny = y;
                            }
                            if (maxy < y)
                            {
                                maxy = y;
                            }
                        }
                    }
                }
                int h = Math.Abs(maxy - miny);
                Rectangle rect = new Rectangle(0, miny, btt.Width - 1, h);
                AForge.Imaging.Filters.Crop cp = new AForge.Imaging.Filters.Crop(rect);
                Bitmap newbt = cp.Apply(btt);
                newbt.Save(Application.StartupPath + "\\wordp\\" + l + ".jpg");
                            
            }
        }

        public void wordtochar()
        {
            string[] words = Directory.GetFiles(Application.StartupPath + "\\wordp");
            for (int l = 0; l < words.Length; l++)
            {
                if (Directory.Exists(Application.StartupPath + "\\char"+l+"\\"))
                {
                    Directory.Delete(Application.StartupPath + "\\char" + l + "\\", true);
                }
                Directory.CreateDirectory(Application.StartupPath + "\\char" + l + "\\");


            }

            for (int l = 0; l < words.Length; l++)
            {
                int x1=0;
                int x2=0;
                string pth = Application.StartupPath + "\\wordp\\" + l + ".jpg";
             
                Bitmap btt = new Bitmap(pth);
                int start = (int)(btt.Height / 2);
                int f1 = 0;
                int h=btt.Height;
                int cnt = 0;
                int pos = 0;
                int maxcnt = 0;
                int prev = 0;
                int lcnt = 0;
                pos = (int)(btt.Height / 2);
                  
                for (int i = 0; i < btt.Height; i++)
                {
                    lcnt = 0;
                     for (int j = 0; j < btt.Width; j++)
                    {
                        Color c = btt.GetPixel(j, i);
                        if (c.R < 10 && prev == 0)
                        {
                            lcnt++;

                        }
                        if (c.R < 10)
                        {
                            prev = 0;

                        }
                        else
                        {
                            prev = 1;
                        }
                    }

                    if (maxcnt < lcnt)
                    {
                        maxcnt = lcnt;
                        pos = i;
                    }
                }

                start = pos+10;
                for (int x = 0; x < btt.Width; x++)
                {
                    f1 = 0;

                    for (int y = start; y < btt.Height; y++)
                    {
                        Color c = btt.GetPixel(x, y);
                        if (c.R < 10)
                        {
                            f1 = 1;
                            x2 = x;
                        }
                    }
                    int diff = Math.Abs(x2 - x1);
                    if (f1 == 0 && diff > 3)
                    {
                        Rectangle rect = new Rectangle(x1, 0, diff, h);
                        AForge.Imaging.Filters.Crop cp = new AForge.Imaging.Filters.Crop(rect);
                        Bitmap btout = cp.Apply(btt);
                        btout.Save(Application.StartupPath + "\\char" + l + "\\" + cnt + ".jpg");
                        btout.Save(Application.StartupPath + "\\chardata\\b" + l + cnt+ ".jpg");
                        cnt++;
                        x1 = x2;
                    }
                }
              
                /*
                int minx = -1;
                int maxx = -1;
                int flg = 0;
                int flg1 = 0;


                for (int i = 0; i < btt.Width; i++)
                {
                    for (int j = start; j < btt.Height; j++)
                    {
                        Color cr = btt.GetPixel(i, j);

                        if (flg1 == 0)
                        {
                            if (cr.R < 255)
                            {
                                flg1 = 1;
                            }
                            if (cr.R < 255 && flg != 1)
                            {
                                minx = i;
                                flg = 1;

                            }
                        }

                    }
                    if (flg1 != 1 && flg != 0)
                    {
                        maxx = i;
                        int diff = Math.Abs(maxx - minx);
                        if (maxx != -1 && minx != -1)
                        {
                            Rectangle rect = new Rectangle(minx, 0, diff, h);
                            AForge.Imaging.Filters.Crop cp = new AForge.Imaging.Filters.Crop(rect);
                            Bitmap btout = cp.Apply(btt);
                            btout.Save(Application.StartupPath + "\\char" + l + "\\" + cnt + ".jpg");
                            cnt++;
                           
                        }

                        minx = 0;
                        maxx = 0;
                        flg = 0;
                        flg1 = 0;
                    }

                    flg1 = 0;
                }
            */
            }
            MessageBox.Show("word to char coversion finished");
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string[] words = Directory.GetFiles(Application .StartupPath +"\\wordp");
            int temp = 0;
            for (int l = 0; l < words.Length; l++)
            {
                int minx = -1;
                int maxx = -1;
                int flg = 0;
                int flg1 = 0;
               
                Bitmap btt = new Bitmap(words[l]);
                int start = btt.Height/2;
               // Bitmap btt = new Bitmap(im[l]);

                for (int i = 0; i < btt.Width; i++)
                {
                    for (int j =start ; j < btt.Height; j++)
                    {
                        Color cr = btt.GetPixel(i, j);

                        if (flg1 == 0)
                        {
                            if (cr.R < 255)
                            {
                                flg1 = 1;
                            }
                            if (cr.R < 255 && flg != 1)
                            {
                                minx = i;
                                flg = 1;

                            }
                        }

                    }
                    if (flg1 != 1 && flg != 0)
                    {
                        maxx = i;

                        if (maxx != -1 && minx != -1 )
                        {
                            Bitmap newbt = new Bitmap(maxx - minx, btt.Height);



                            int xx = 0;
                            int yy = 0;
                            for (int x = minx; x < maxx; x++)
                            {
                                yy = 0;
                                for (int y = 0; y < btt.Height; y++)
                                {
                                    Color cr = btt.GetPixel(x, y);

                                    newbt.SetPixel(xx, yy, cr);
                                    yy++;
                                }
                                xx++;
                            }
                            newbt.Save(Application.StartupPath + "\\char\\" + temp + ".jpg");
                            temp++;
                           

                        }
                        minx = 0;
                        maxx = 0;
                        flg = 0;
                        flg1 = 0;
                    }
                    flg1 = 0;
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int miny = 0;
            int maxy = 0;
            int flg = 0;
            int flg1 = 0;
            int temp = 0;

            string[] segment = Directory.GetFiles(Application.StartupPath +"\\word");

            for (int l = 0; l < segment.Length; l++)
            {

                Bitmap bt = new Bitmap(segment[l]);

                for (int i = 0; i < bt.Height; i++)
                {
                    for (int j = 0; j < bt.Width; j++)
                    {
                        Color cr = bt.GetPixel(j, i);

                        if (flg1 == 0)
                        {
                            if (cr.R < 255)
                            {
                                flg1 = 1;
                            }
                            if (cr.R < 255 && flg != 1)
                            {
                                miny = i;
                                flg = 1;

                            }


                        }


                    }
                    if (flg1 != 1 && flg != 0)
                    {
                        maxy = i;

                        if (maxy != 0 && miny != 0 )
                        {
                            Bitmap newbt = new Bitmap(bt.Width, maxy - miny);

                            int xx = 0;
                            int yy = 0;
                            for (int x = 0; x < bt.Width; x++)
                            {
                                yy = 0;
                                for (int y = miny; y < maxy; y++)
                                {
                                    Color cr = bt.GetPixel(x, y);

                                    newbt.SetPixel(xx, yy, cr);
                                    yy++;
                                }
                                xx++;
                            }
                            newbt.Save(Application.StartupPath + "\\filter\\" + temp + ".jpg");
                           

                        }
                        temp++;
                        miny = 0;
                        maxy = 0;
                        flg = 0;
                        flg1 = 0;
                    }
                    flg1 = 0;
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            button6_Click(null, null);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
          
        }

        private void button7_Click_2(object sender, EventArgs e)
        {  
            
            //NNExtract n = new NNExtract();
            //n.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            wordtochar();
            Cursor.Current = Cursors.Default;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Image.FromFile("D:\\capture.bmp");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
