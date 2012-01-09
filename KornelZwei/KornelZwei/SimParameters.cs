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
    public partial class SimParameters : Form
    {
        public Scheduler scheduler;

        public SimParameters(Scheduler s)
        {
            InitializeComponent();
            scheduler = s;
        }

        private void SimParameters_Load(object sender, EventArgs e)
        {
            //scheduler.form.Show();

            tbPb98Prob.Text = Const.PB98_PROB.ToString();
            tbPb95Prob.Text = Const.PB95_PROB.ToString();
            tbONProb.Text = Const.ON_PROB.ToString();

            tb10Qty.Text = Const.QTY_10_PROB.ToString();
            tb20Qty.Text = Const.QTY_20_PROB.ToString();
            tb40Qty.Text = Const.QTY_40_PROB.ToString();

            tb10QtyTime.Text = Const.QTY_10_TIME.ToString();
            tb20QtyTime.Text = Const.QTY_20_TIME.ToString();
            tb40QtyTime.Text = Const.QTY_40_TIME.ToString();

            tbPb98Profit.Text = Const.PB98_PROFIT.ToString();
            tbPb95Profit.Text = Const.PB95_PROFIT.ToString();
            tbONProfit.Text = Const.ON_PROFIT.ToString();

            tbCarFreq.Text = Const.CAR_FREQ.ToString();
            tbDeviceQty.Text = Const.DISTRIBUTOR_QTY.ToString();
            tbQueueSize.Text = Const.QUEUE_SIZE.ToString();
        }

        private void UpdateParameters()
        {
            Const.PB98_PROB = Int32.Parse(tbPb98Prob.Text);
            Const.PB95_PROB = Int32.Parse(tbPb95Prob.Text);
            Const.ON_PROB = Int32.Parse(tbONProb.Text);

            Const.QTY_10_PROB = Int32.Parse(tb10Qty.Text);
            Const.QTY_20_PROB = Int32.Parse(tb20Qty.Text);
            Const.QTY_40_PROB = Int32.Parse(tb40Qty.Text);

            Const.QTY_10_TIME = Int32.Parse(tb10QtyTime.Text);
            Const.QTY_20_TIME = Int32.Parse(tb20QtyTime.Text);
            Const.QTY_40_TIME = Int32.Parse(tb40QtyTime.Text);

            Const.PB98_PROFIT = Double.Parse(tbPb98Profit.Text);
            Const.PB95_PROFIT = Double.Parse(tbPb95Profit.Text);
            Const.ON_PROFIT = Double.Parse(tbONProfit.Text);

            Const.CAR_FREQ = Int32.Parse(tbCarFreq.Text);
            //Const.DISTRIBUTOR_QTY = Int32.Parse(tbDeviceQty.Text);
            //Const.QUEUE_SIZE = Int32.Parse(tbQueueSize.Text);
        }

        private void btnUpdateParam_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateParameters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowForm_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateParameters();
AddSocket();
                ResetScheduler();
                scheduler.form = new SimForm(scheduler);
                RefreshForm();
                
                scheduler.form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddSocket()
        {
            try
            {
                //ResetScheduler();

                int queueSize = Int32.Parse(tbQueueSize.Text);
                int X = 100;
                int Y = 100;
                int DevNr = Int32.Parse(tbDeviceQty.Text);
                bool isFirst = true;

                Queue queue;

                queue = new QueueFifo(this.scheduler, queueSize);

                Socket socket = new Socket(queue, this.scheduler, isFirst);

                socket.X = X;
                socket.Y = Y;

                //
                //add devices
                for (int i = 1; i <= DevNr; i++)
                {
                    Device dev = new Device();
                    socket.AddDevice(dev);

                }

                scheduler.socket = socket;

                RefreshForm();

                MessageBox.Show("Socket " + socket.ToString() + " added");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ResetScheduler()
        {
            Device.lastId = 0;
            Queue.lastId = 0;
            Socket.lastId = 0;

            scheduler.timestamp = 0;
            scheduler.queueSize = new List<QueueSize>();
            scheduler.killedJobsList = new List<Job>();
            scheduler.jobList = new List<Job>();
            scheduler.eventList = new List<Event>();
            //AddSocket();

            scheduler.socket.queue.JobList = new List<Job>();
            foreach (Device d in scheduler.socket.deviceList)
            {
                d.CurrentJob = null;
            }
        }

        public void RefreshForm()
        {
            bool open = false;
            foreach (Form s in Application.OpenForms)
            {
                if (s is SimForm)
                {
                    open = true;
                }
            }

            scheduler.form.OnLoad();

        }
    }
}
