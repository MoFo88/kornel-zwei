using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KornelZwei.Logic;

namespace KornelZwei
{
    public partial class SimForm : Form
    {
        SocketControl socketControl;
        Scheduler scheduler;

        public SimForm(Scheduler s)
        {
            InitializeComponent();
            scheduler = s;
        }

        private void InitializeDgv(DataGridView dgv, List<Job> jobList)
        {
            foreach (var x in jobList)
            {
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewCell cell = new DataGridViewTextBoxCell();
                cell.Value = x.ToStr;
                row.Cells.Add(cell);
                row.DefaultCellStyle.BackColor = x.color;

                dgv.Rows.Add(row);
            }

            dgv.ClearSelection();
        }

        public void InitializeDgvQueue(int nr)
        {
            SocketControl cosCtrl = socketControl;
            DataGridView dgv = cosCtrl.Queue;
            dgv.Rows.Clear();

            cosCtrl.labebQueue.Text = cosCtrl.Socket.queue.ToString();

            List<Job> l = cosCtrl.Socket.queue.JobList;
            InitializeDgv(dgv, l);
        }

        public void InitializeDgvDevice(int socNr, int devNr)
        {
            SocketControl cosCtrl = socketControl;
            DataGridView dgv = cosCtrl.DeviceList[devNr];
            Device dev = (Device)dgv.Tag;

            cosCtrl.devlabels[devNr].Text = dev.ToString();

            Job job = dev.CurrentJob;
            dgv.Rows.Clear();

            List<Job> jobList = new List<Job>();
            if (job != null)
                jobList.Add(job);
            ;

            if (dev.IsWorking)
            {
                dgv.BackgroundColor = Color.Lime;
            }
            else if (dev.IsBusy)
            {
                dgv.BackgroundColor = Color.DarkGreen;
            }
            else
            {
                dgv.BackgroundColor = Color.WhiteSmoke;
            }

            InitializeDgv(dgv, jobList);
        }

        public void Notify(String message, int i = 1)
        {
            if (i == 1)
                rtbLog.Text = message + "\n" + rtbLog.Text;
            //if (checkBoxRefresh.Checked)
            //    richTextBox.Text = message + "\n" + richTextBox.Text;
        }

        public void OnLoad()
        {
            try
            {
                btnStop.Enabled = false;
                this.panelSockets.Controls.Clear();
                timer1.Interval = Const.TIME_INTERVAL;
                //socketControl = new SocketControl();

                //
                //create soc controls
                //foreach (Socket soc in scheduler.socketList)
                //{
                    //SocketControl cosContr = new SocketControl(scheduler.socket);
                    socketControl = new SocketControl(scheduler.socket);
                //}

                //foreach (var control in socketControlList)
                //{
                    socketControl.Location = new Point(socketControl.Socket.X, socketControl.Socket.Y);
                    this.panelSockets.Controls.Add(socketControl);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool firstStep = true;

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (firstStep)
            {
                //InitilizeExcel();
                firstStep = false;
            }

            timer1.Start();
            btnStop.Enabled = true;
            btnStart.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            scheduler.MakeStep();
            Refresh();
        }

        private void SimForm_Load(object sender, EventArgs e)
        {
            try
            {
                OnLoad();
                Refresh();

                //try
                //{
                //    excel1 = new Excel(5);
                //}
                //catch (Exception exWew)
                //{
                //    //nie zainstalowany excel
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Refresh()
        {
            //if (checkBoxRefresh.Checked)
            //{
                int jobCount = scheduler.jobList.Count();

                //InitializeDevicesStatistics();
                //InitializeQueuesStatistics();
                //InitializeGlobalStatistics();

                //for (int i = 0; i < socketControlList.Count; i++)
                //{
                    SocketControl socCtrl = socketControl;
                    InitializeDgvQueue(0);

                    for (int j = 0; j < socCtrl.DeviceList.Count; j++)
                    {
                        InitializeDgvDevice(0, j);
                    }
                //}
           // }

           // WriteExcel();
        }

        private void SimForm_Paint(object sender, PaintEventArgs e)
        {
            //
            //lines
            //foreach (var socketControl in socketControlList)
            ////{
            //Socket prev = socketControl.Socket;

            ////from 
            //int prevX = prev.X + Const.CONTROL_SOCKET_WIDTH;
            //int prevY = prev.Y + socketControl.Height / 2;

            //foreach (var next in prev.nextSockets)
            //{
            //    var nextSocketControl = socketControlList.SingleOrDefault(sc => sc.Socket == next);

            //    //next
            //    int nextX = next.X;
            //    int nextY = next.Y + nextSocketControl.Height / 2;


            //    //draw
            //    Graphics formGraphics = this.panelSockets.CreateGraphics();

            //    using (Brush aGradientBrush = new LinearGradientBrush(new Point(prevX, prevY), new Point(nextX, nextY), Color.Black, Color.Red))
            //    {
            //        using (Pen Pen = new Pen(aGradientBrush, 2))
            //        {
            //            formGraphics.DrawLine(Pen, prevX, prevY, nextX, nextY);
            //        }
            //        formGraphics.Dispose();
            //    }
            }
        


    }
}
