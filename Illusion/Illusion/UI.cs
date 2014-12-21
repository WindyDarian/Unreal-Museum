using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Kinect;
using Microsoft.Xna.Framework;

namespace Illusion
{
    /// <summary>
    /// 2011.12.29 黎健成
    /// 修改 范若余 2011/12/31
    /// </summary>
    public partial class UI : Form
    {
        KinectSensor kinect;

        Vector3 handR = new Vector3();
        Vector3 temp = new Vector3(1, 1, 1);
        Vector3 left = new Vector3(0, 0, 0);
        Vector3 center = new Vector3(0, 0, 0);

        DateTime now;
        DateTime past;

        int state = 0;

        public UI()
        {
            InitializeComponent();
        }

        private void UI_Load(object sender, EventArgs e)
        {
            this.textBox2.Text = ((float)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / (float)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height).ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton4.Checked = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IllusionMain.kinectbool = checkBox1.Checked;
            try
            {
                IllusionMain.ScreenWidth = Convert.ToSingle(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("请正确输入屏幕宽度!");
                return;
            }
            if (IllusionMain.kinectbool)
            {

                try
                {
                    KinectSupport.KinectComponent.KinectOffset.X = Convert.ToSingle(posx.Text);
                }
                catch
                {
                    MessageBox.Show("请正确输入X值!");
                    return;
                }
                try
                {
                    KinectSupport.KinectComponent.KinectOffset.Y = Convert.ToSingle(posy.Text);
                }
                catch
                {
                    MessageBox.Show("请正确输入Y值!");
                    return;
                }
                try
                {
                    KinectSupport.KinectComponent.KinectOffset.Z = Convert.ToSingle(posz.Text);
                }
                catch
                {
                    MessageBox.Show("请正确输入Z值!");
                    return;
                }
                KinectSupport.KinectComponent.LeftRotateBool = radioButton2.Checked;
                KinectSupport.KinectComponent.RightRotateBool = radioButton3.Checked;
                KinectSupport.KinectComponent.UpsideBool = radioButton4.Checked;
            }
            try
            {
                IllusionMain.AspectRatio = Convert.ToSingle(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("请正确输入宽高比!");
                return;
            }

            Program.gamebool = true;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                posx.Enabled = false;
                posy.Enabled = false;
                posz.Enabled = false;
                button1.Enabled = false;
            }
            else
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;
                posx.Enabled = true;
                posy.Enabled = true;
                posz.Enabled = true;
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kinect = KinectSensor.KinectSensors.First();
            if (kinect.Status == KinectStatus.Connected)
            {
                state = 0;
                kinect.SkeletonStream.Enable();
                kinect.Start();
                kinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady);
                info.Text = "Put your right hand on the screen center for 3 seconds.";
                CenterPoint.Visible = true;
            }
            else
            {
                info.Text = "Please plug in Kinect!";
            }
        }

        private bool check(Vector3 a, Vector3 b)
        {
            if (Math.Abs(a.X - b.X) < 0.05 && Math.Abs(a.Y - b.Y) < 0.05 && Math.Abs(a.Z - b.Z) < 0.05) return true;
            return false;
        }

        void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    Skeleton[] skeletons = new Skeleton[frame.SkeletonArrayLength];
                    frame.CopySkeletonDataTo(skeletons);
                    var skeleton = skeletons.Where(s => s.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();
                    if (skeleton != null)
                    {
                        var pos = skeleton.Joints[JointType.HandRight].Position;
                        handR = new Vector3(pos.X, pos.Y, pos.Z);
                        if (radioButton1.Checked == true)
                        {
                            temp.X = -handR.X;
                            temp.Y = -handR.Y;
                            temp.Z = handR.Z;
                        }
                        else if (radioButton2.Checked == true)
                        {
                            temp.X = -handR.Z;
                            temp.Y = -handR.Y;
                            temp.Z = handR.X;
                        }
                        else if (radioButton3.Checked == true)
                        {
                            temp.X = handR.Z;
                            temp.Y = -handR.Y;
                            temp.Z = -handR.X;
                        }
                        else if (radioButton4.Checked == true)
                        {
                            temp.X = handR.X;
                            temp.Y = -handR.Y;
                            temp.Z = handR.Z;
                        }
                        test.Text = "RightHand: " + temp.X.ToString() + " , " + temp.Y.ToString() + " , " + temp.Z.ToString();
                        switch (state)
                        {
                            case 0:
                                now = System.DateTime.Now;
                                if (!check(temp, center))
                                {
                                    past = now;
                                    center = temp;
                                }
                                else
                                {
                                    if ((now - past).TotalSeconds > 2)
                                    {
                                        posx.Text = center.X.ToString();
                                        posy.Text = center.Y.ToString();
                                        posz.Text = center.Z.ToString();
                                        //KinectSupport.KinectComponent.KinectOffset = center;
                                        state++;
                                        CenterPoint.Visible = false;
                                        info.Text = "把你的右手放在屏幕的左下角3秒。";
                                        //info.Text = "Put your right hand on the lower left corner of the screen for 3 seconds.";
                                    }
                                }
                                break;
                            case 1:
                                now = System.DateTime.Now;
                                if (!check(temp, left))
                                {
                                    past = now;
                                    left = temp;
                                }
                                else
                                {
                                    if ((now - past).TotalSeconds > 2)
                                    {
                                        float t = (center.X - left.X) * 2;
                                        textBox1.Text = t.ToString();
                                        t = (left.X - center.X) / (left.Y - center.Y);
                                        textBox2.Text = t.ToString();
                                        //IllusionMain.ScreenWidth = (center.X - left.X) * 2;
                                        //IllusionMain.AspectRatio = (left.X - center.X) / (left.Y - center.Y);
                                        info.Text = "";
                                        test.Text = "";
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

    }
}
