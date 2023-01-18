using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zamba.Insert.Components;
using Zamba.Core;

namespace ThreadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartThreadService();
        }

        ThreadPoolService TPS = new ThreadPoolService();
        private void StartThreadService()
        {
            Decimal MaxThreads = 10;
            Decimal MinThreads = 1;
            Decimal Interval = 2000;
            MaxThreads = Decimal.Parse(Zamba.Core.UserPreferences.getValue("MaxThreads", UserPreferences.Sections.InsertPreferences, "10"));
            MinThreads = Decimal.Parse(UserPreferences.getValue("MinThreads", UserPreferences.Sections.InsertPreferences, "1"));
            Interval = Decimal.Parse(UserPreferences.getValue("Interval", UserPreferences.Sections.InsertPreferences, "10000"));

            TPS.StartThreadService(MaxThreads, MinThreads, Interval);
        }
    }
}
