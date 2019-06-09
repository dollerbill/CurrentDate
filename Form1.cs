using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dateApp
{
public partial class Form1 : Form
    {
        DateTimeOffset newYears = new DateTimeOffset(DateTime.Now.Year, 1, 1, 0, 0, 0, TimeSpan.Zero);
        public Form1()
        {
            InitializeComponent();
        }

        private int getDay()
        {
            DateTimeOffset newYears = new DateTimeOffset(DateTime.Now.Year, 1, 1, 0, 0, 0, TimeSpan.Zero);
            int today = (DateTime.Today - newYears).Days + 1;
            return today;
        }

        private string getPrefix()
        {
            var last = getDay() % 10;
            var prefix = prefixFinder(last);
            return prefix;
        }

        private string prefixFinder(int day)
        {
            if (day == 1)
            {
                return "st";
            } else if (day == 2)
            {
                return "nd";
            } else if (day == 3)
            {
                return "rd";
            } else
            {
                return "th";
            }
        }

            private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(getDay().ToString());
            MessageBox.Show($"Today is the {getDay().ToString()}{getPrefix()} day of the year. Copied to the clipboard.",
                "CurrentDay");
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            notifyIcon1.Visible = false;

            Application.Exit();
        }
    }
}
