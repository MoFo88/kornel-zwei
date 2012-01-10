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
        Excl.Excel excel1;

        public SimForm(Scheduler s)
        {
            InitializeComponent();
            scheduler = s;
            tbSimulationSteps.Text = Const.SIMULATION_STEPS.ToString();
            tbTimeInterval.Text = Const.TIME_INTERVAL.ToString();
            excel1 = null;
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
            if (i == 0)
                rtbLog.Text = message + "\n" + rtbLog.Text;
            if (checkBoxRefresh.Checked)
                rtbLog.Text = message + "\n" + rtbLog.Text;
        }

        public void OnLoad()
        {
            try
            {
                btnStop.Enabled = false;
                this.panelSockets.Controls.Clear();
                timer1.Interval = Const.TIME_INTERVAL;

                //create soc controls

                socketControl = new SocketControl(scheduler.socket);

                socketControl.Location = new Point(socketControl.Socket.X, socketControl.Socket.Y);
                this.panelSockets.Controls.Add(socketControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool firstStep = true;

        //Excel
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

        private void CreateExcel()
        {
            try
            {
                excel1 = new Excl.Excel();
            }
            catch (Exception exWew)
            {
                //nie zainstalowany excel
            }
        }

        private void SimForm_Load(object sender, EventArgs e)
        {
            try
            {
                OnLoad();
                Refresh();
                //CreateExcel();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Refresh()
        {
            if (checkBoxRefresh.Checked)
            {
                int jobCount = scheduler.jobList.Count();

                InitializeDevicesStatistics();
                InitializeQueuesStatistics();
                InitializeGlobalStatistics();

                SocketControl socCtrl = socketControl;
                InitializeDgvQueue(0);

                for (int j = 0; j < socCtrl.DeviceList.Count; j++)
                {
                    InitializeDgvDevice(0, j);
                }
            }

            // WriteExcel();
        }

        private void InitializeDevicesStatistics()
        {
            dgvStatsDevice.Rows.Clear();

            Socket s = scheduler.socket;
            foreach (Device dev in s.deviceList)
            {
                dgvStatsDevice.Rows.Add
                    (
                        dev.Id,
                        scheduler.AllWorkTimeOnDevice(dev),
                        scheduler.AllBusyTimeOnDevice(dev),
                        scheduler.AvgWorkTimeOnDevice(dev),
                        scheduler.AvgBusyTimeOnDevice(dev)
                    );
            }
        }

        private void InitializeGlobalStatistics()
        {
            dgvStats.Rows.Clear();

            dgvStats.Rows.Add("liczba klientów", scheduler.jobList.Count());
            dgvStats.Rows.Add("liczba nieobsłużonych klientów", scheduler.killedJobsList.Count());
            dgvStats.Rows.Add("max czas w systemie", scheduler.MaxTimeInSystem());
            dgvStats.Rows.Add("min czas w systemie", scheduler.MinTimeInSystem());
            dgvStats.Rows.Add("średni czas w systemie", scheduler.AvgTimeInSystem());
            dgvStats.Rows.Add("odch. st. czasu w systemie", scheduler.StdVarTimeInSystem());
            dgvStats.Rows.Add("max czas pracy zadania", scheduler.MaxWorkTime());
            dgvStats.Rows.Add("min czas pracy zadania", scheduler.MinWorkTime());
            dgvStats.Rows.Add("średni czas pracy zadania", scheduler.AvgWorkingTime());
            dgvStats.Rows.Add("odch. st. czasu pracy zadania", scheduler.StdVarWorkTime());
            dgvStats.Rows.Add("max czas bezczynnosci", scheduler.MaxWastedTime());
            dgvStats.Rows.Add("min czas bezczynnosci", scheduler.MinWastedTime());
            dgvStats.Rows.Add("średni czas bezczynnosci", scheduler.AvgWastedTime());
            dgvStats.Rows.Add("odch. st. czasu bezczynnosci", scheduler.StdVarWastedTime());
            dgvStats.Rows.Add("ilość tankowań Pb98", scheduler.SumForFuelType(FuelType.PB98));
            dgvStats.Rows.Add("ilość tankowań Pb95", scheduler.SumForFuelType(FuelType.PB95));
            dgvStats.Rows.Add("ilość tankowań ON", scheduler.SumForFuelType(FuelType.ON));
            dgvStats.Rows.Add("ilość zatankowanego Pb98 [l]", scheduler.GetFuelQty(FuelType.PB98));
            dgvStats.Rows.Add("ilość zatankowanego Pb95 [l]", scheduler.GetFuelQty(FuelType.PB95));
            dgvStats.Rows.Add("ilość zatankowanego ON [l]", scheduler.GetFuelQty(FuelType.ON));
            dgvStats.Rows.Add("zarobek na Pb98", scheduler.GetProfitForFuelType(FuelType.PB98));
            dgvStats.Rows.Add("zarobek na Pb95", scheduler.GetProfitForFuelType(FuelType.PB95));
            dgvStats.Rows.Add("zarobek na ON", scheduler.GetProfitForFuelType(FuelType.ON));
            dgvStats.Rows.Add("zarobek na paliwie", scheduler.GetOverallProfit());
        }

        private void InitializeQueuesStatistics()
        {
            dgvStatsQueue.Rows.Clear();

            Socket s = scheduler.socket;
            dgvStatsQueue.Rows.Add
                (
                    s.queue.Name,
                    scheduler.avgQueueTime(s.queue),
                    scheduler.sumQueueTime(s.queue),
                    scheduler.maxQueueTime(s.queue),
                    scheduler.maxQueueCount(s.queue),
                    scheduler.avgQueueCount(s.queue)
                );
        }

        private void btnNextStep_Click(object sender, EventArgs e)
        {
            if (firstStep)
            {
                //InitilizeExcel();
                firstStep = false;
            }

            scheduler.MakeStep();
            Refresh();
        }

        private void btnNextEvent_Click(object sender, EventArgs e)
        {
            if (firstStep)
            {
                //InitilizeExcel();
                firstStep = false;
            }

            scheduler.MakeEventStep();
            Refresh();
        }

        private void btnStepInterval_Click(object sender, EventArgs e)
        {
            Const.TIME_INTERVAL = Int32.Parse(tbTimeInterval.Text);
            timer1.Interval = Const.TIME_INTERVAL;
        }

        //public void ResetScheduler()
        //{
        //    scheduler.timestamp = 0;
        //    scheduler.queueSize = new List<QueueSize>();
        //    scheduler.killedJobsList = new List<Job>();
        //    scheduler.jobList = new List<Job>();
        //    scheduler.eventList = new List<Event>();

        //    Socket s = scheduler.socket;

        //    s.queue.JobList = new List<Job>();
        //    foreach (Device d in s.deviceList)
        //    {
        //        d.CurrentJob = null;
        //    }


        //}

        public void InitializeExcel()
        {
            if (excel1 == null)
            {
                try
                {
                    excel1 = new Excl.Excel();
                    List<string> str = new List<string>();

                    str.Add("liczba klientów");
                    str.Add("liczba nieobsłużonych klientów");
                    str.Add("max czas w systemie");
                    str.Add("min czas w systemie");
                    str.Add("średni czas w systemie");
                    str.Add("odch. st. czasu w systemie");
                    str.Add("max czas pracy zadania");
                    str.Add("min czas pracy zadania");
                    str.Add("średni czas pracy zadania");
                    str.Add("odch. st. czasu pracy zadania");
                    str.Add("max czas bezczynnosci");
                    str.Add("min czas bezczynnosci");
                    str.Add("średni czas bezczynnosci");
                    str.Add("odch. st. czasu bezczynnosci");
                    str.Add("ilość tankowań Pb98");
                    str.Add("ilość tankowań Pb95");
                    str.Add("ilość tankowań ON");
                    str.Add("ilość zatankowanego Pb98 [l]");
                    str.Add("ilość zatankowanego Pb95 [l]");
                    str.Add("ilość zatankowanego ON [l]");
                    str.Add("zarobek na Pb98");
                    str.Add("zarobek na Pb95");
                    str.Add("zarobek na ON");
                    str.Add("zarobek na paliwie");

                    excel1.WriteRow(str);
                }
                catch (Exception exWew)
                {
                    //nie zainstalowany excel
                    excel1 = null;
                }
            }

            //if (excel1 != null)
            //{

            //    //excel initialize

            //}
        }

        public void SaveToExcel()
        {
            if (excel1 != null)
            {
                List<string> str = new List<string>();

                //blad z locale biblioteki do Excela dlatego Replace
                str.Add(scheduler.jobList.Count().ToString().Replace(",", "."));
                str.Add(scheduler.killedJobsList.Count().ToString().Replace(",", "."));
                str.Add(scheduler.MaxTimeInSystem().ToString().Replace(",", "."));
                str.Add(scheduler.MinTimeInSystem().ToString().Replace(",", "."));
                str.Add(scheduler.AvgTimeInSystem().ToString().Replace(",", "."));
                str.Add(scheduler.StdVarTimeInSystem().ToString().Replace(",", "."));
                str.Add(scheduler.MaxWorkTime().ToString().Replace(",", "."));
                str.Add(scheduler.MinWorkTime().ToString().Replace(",", "."));
                str.Add(scheduler.AvgWorkingTime().ToString().Replace(",", "."));
                str.Add(scheduler.StdVarWorkTime().ToString().Replace(",", "."));
                str.Add(scheduler.MaxWastedTime().ToString().Replace(",", "."));
                str.Add(scheduler.MinWastedTime().ToString().Replace(",", "."));
                str.Add(scheduler.AvgWastedTime().ToString().Replace(",", "."));
                str.Add(scheduler.StdVarWastedTime().ToString().Replace(",", "."));
                str.Add(scheduler.SumForFuelType(FuelType.PB98).ToString().Replace(",", "."));
                str.Add(scheduler.SumForFuelType(FuelType.PB95).ToString().Replace(",", "."));
                str.Add(scheduler.SumForFuelType(FuelType.ON).ToString().Replace(",", "."));
                str.Add(scheduler.GetFuelQty(FuelType.PB98).ToString().Replace(",", "."));
                str.Add(scheduler.GetFuelQty(FuelType.PB95).ToString().Replace(",", "."));
                str.Add(scheduler.GetFuelQty(FuelType.ON).ToString().Replace(",", "."));
                str.Add(scheduler.GetProfitForFuelType(FuelType.PB98).ToString().Replace(",", "."));
                str.Add(scheduler.GetProfitForFuelType(FuelType.PB95).ToString().Replace(",", "."));
                str.Add(scheduler.GetProfitForFuelType(FuelType.ON).ToString().Replace(",", "."));
                str.Add(scheduler.GetOverallProfit().ToString().Replace(",", "."));

                excel1.WriteRow(str);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                scheduler.Reset();
                if (excel1 != null)
                {
                    excel1 = null;
                }

                if (firstStep)
                {
                    //InitilizeExcel();
                    firstStep = false;
                }

                int steps = Int32.Parse(tbSimulationSteps.Text);

                for (int i = 0; i < steps; i++)
                {
                    scheduler.MakeStep();
                    Refresh();

                    Application.DoEvents();
                    System.Threading.Thread.Sleep(1);
                }

                //finished
                int tempCarFreq = Const.CAR_FREQ;
                Const.CAR_FREQ = -1;

                while (!scheduler.SystemEmpty)
                {
                    scheduler.MakeStep();
                    Refresh();
                }

                if (checkBox1.Checked)
                {
                    InitializeExcel();
                    SaveToExcel();
                }

                MessageBox.Show("Symulacja ukończona: " + scheduler.timestamp);

                scheduler.Reset();
                if (excel1 != null)
                {
                    excel1 = null;
                }
                Const.CAR_FREQ = tempCarFreq;

                //MessageBox.Show("Symulacja ukończona: " + scheduler.timestamp);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
