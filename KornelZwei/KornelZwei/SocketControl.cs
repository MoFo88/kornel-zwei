using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KornelZwei.Logic;

namespace KornelZwei
{
    public partial class SocketControl : UserControl
    {
        public List<DataGridView> DeviceList;
        public DataGridView Queue;
        public Socket Socket;

        public Label labebQueue;
        public List<Label> devlabels;

        public SocketControl(Socket socket)
        {
            InitializeComponent();
            Socket = socket;

            labebQueue = new Label();
            labebQueue.Text = "lblQueue";
            labebQueue.Location = new Point(2,1);
            labebQueue.Width = Const.ROW_WIDTH + 5;

            Queue = new DataGridView();
            DataGridViewCell cellQ = new DataGridViewTextBoxCell();
            DataGridViewColumn collQ = new DataGridViewColumn(cellQ);
            Queue.Columns.Add(collQ);
            Queue.Height = Const.ROW_HEIGHT_QUEUE * socket.queue.Size;
            Queue.Width = Const.ROW_WIDTH;
            Queue.RowHeadersVisible = false;
            Queue.ColumnHeadersVisible = false;
            Queue.Location = new Point(2,25);
            Queue.ScrollBars = ScrollBars.Vertical;
            Queue.AllowUserToAddRows = false;

            int count = 0;
            DeviceList = new List<DataGridView>();
            devlabels = new List<Label>();
            foreach (Device dev in socket.deviceList)
            {
                DataGridView d = new DataGridView();
                DataGridViewCell cell = new DataGridViewTextBoxCell();
                DataGridViewColumn coll = new DataGridViewColumn(cell);
                d.ColumnHeadersVisible = false;
                d.RowHeadersVisible = false;
                d.AllowUserToAddRows = false;
                d.Columns.Add(coll);
                d.ScrollBars = ScrollBars.None;
                d.Height = Const.ROW_HEIGHT;
                d.Width = Const.ROW_WIDTH;
                d.Location = new Point(Const.ROW_WIDTH + 20, 2+count * (Const.ROW_HEIGHT+30) + 20);
                d.Tag = dev;
                
                DeviceList.Add(d);

                Label l = new Label();
                l.Text = "dev" + count;
                l.Location = new Point(Const.ROW_WIDTH + 5, 2 + count * (Const.ROW_HEIGHT + 30));
                l.Width = Const.ROW_WIDTH;
                devlabels.Add(l);

                count ++;
            }

            this.Height = Math.Max(Queue.Size.Height+40 , (DeviceList.Count) * (Const.ROW_HEIGHT+32));
            this.Width = Const.CONTROL_SOCKET_WIDTH;

            //add to control
            this.Controls.Add(labebQueue);
            this.Controls.Add(Queue);

            foreach (DataGridView dev in DeviceList)
            {
                this.Controls.Add(dev);
            }

            foreach (Label l in devlabels)
            {
                this.Controls.Add(l);
            }
        }
    }
}
