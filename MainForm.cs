using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;

using AForge.Video;
using AForge.Video.DirectShow;

using System.Runtime.InteropServices;

namespace ObjectDetection
{
    public partial class MainForm : Form
    {
        int framecount = 0;
        private VideoCaptureDevice device; //Current chosen device(camera) 
        private Dictionary<string, string> cameraDict = new Dictionary<string, string>();
        private const int CameraWidth = 640;  // constant Width
        private const int CameraHeight = 480; // constant Height
        private FilterInfoCollection cameras; //Collection of Cameras that connected to PC
        private int frameCounter = 0;
        [DllImport("user32.Dll")]
        public static extern int keybd_event(byte ch, byte scan, int flag, int info);
        public MainForm()
        {
            InitializeComponent();
            this.cameras = new FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);
            int i = 1;
            foreach (AForge.Video.DirectShow.FilterInfo camera in this.cameras)
            {
                if (!this.cameraDict.ContainsKey(camera.Name))
                    this.cameraDict.Add(camera.Name, camera.MonikerString);
                else
                {
                    this.cameraDict.Add(camera.Name + "-" + i.ToString(), camera.MonikerString);
                    i++;
                }
            }
            this.cbCamera.DataSource = new List<string>(cameraDict.Keys); //Bind camera names to combobox

            if (this.cbCamera.Items.Count == 0)
                button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bt = new Bitmap(pictureBox1.Image);
            bt.Save("D:\\capture.bmp");
        }
        private void videoNewFrame(object sender, NewFrameEventArgs args)
        {

            Bitmap temp = args.Frame.Clone() as Bitmap;

            framecount++;
            //  label2.Text = framecount.ToString();
            this.pictureBox1.Image = ResizeBitmap(temp);
        }
        private Bitmap ResizeBitmap(Bitmap bmp)
        {
            ResizeBilinear resizer = new ResizeBilinear(pictureBox1.Width, pictureBox1.Height);

            return resizer.Apply(bmp);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "start")
            {
                this.button1.Text = "Stop";
                this.device = new VideoCaptureDevice(this.cameraDict[cbCamera.SelectedItem.ToString()]);
                this.device.NewFrame += new NewFrameEventHandler(videoNewFrame);
                this.device.DesiredFrameSize = new Size(CameraWidth, CameraHeight);

                device.Start(); //Start Device
            }
            else
            {
                label2.Text = framecount.ToString();
                this.StopCamera();
                button1.Text = "start";
                //this.pictureBox1.Image = null;
            }
        }
        private void StopCamera()
        {
            if (device != null && device.IsRunning)
            {
                device.SignalToStop(); //stop device
                device.WaitForStop();
                device = null;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cbCamera_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
