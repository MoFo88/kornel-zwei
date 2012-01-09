using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KornelZwei.Logic;

namespace KornelZwei
{
    static class Program
    {
        public static Scheduler scheduler;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            scheduler = new Scheduler();
            Application.Run(new SimParameters(scheduler));
        }
    }
}
