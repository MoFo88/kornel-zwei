using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KornelZwei.Logic
{
    public class Const
    {
        //wybór paliwa
        public static int PB98_PROB = 50;
        public static int PB95_PROB = 30;
        public static int ON_PROB = 20;

        //ilość paliwa
        public static int QTY_10_PROB = 40;
        public static int QTY_20_PROB = 20;
        public static int QTY_40_PROB = 40;

        //czas tankowania
        public static int QTY_10_TIME = 4;
        public static int QTY_20_TIME = 6;
        public static int QTY_40_TIME = 10;

        //zysk na litrze
        public static double PB98_PROFIT = 0.5;
        public static double PB95_PROFIT = 0.4;
        public static double ON_PROFIT = 0.45;

        //pozostałe parametry
        public static int CAR_FREQ = 10;
        public static int DISTRIBUTOR_QTY = 2;
        public static int QUEUE_SIZE = 2;

        //Forma symulacji
        public static int TIME_INTERVAL = 500;
        public static int SIMULATION_STEPS = 10000;

        public static int ROW_HEIGHT = 30;
        public static int ROW_WIDTH = 80;

        public static int CONTROL_SOCKET_HEIGHT = 180;
        public static int CONTROL_SOCKET_WIDTH = 310;


        public static int ROW_HEIGHT_QUEUE = 25;
    }
}
