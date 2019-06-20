using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Microsoft.VisualBasic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Clipboard.SetText(getDay().ToString());
                MessageBox.Show($"Today is the {getDay().ToString()}{getPrefix()} day of the year. Copied to the clipboard.",
                    "CurrentDay");
            }
        }

        //private void Custom(object sender, EventArgs e)
        //{
        //    string custom = Interaction.InputBox("Enter a custom Date", "Custom Date", "MM-DD");
        //    string customText;
        //    int customDays;
        //    string[] customNew = custom.Select(c => c.ToString()).ToArray();
        //    string customMonth;
        //    string customDay;
        //    if (Regex.IsMatch(custom, @"[a-zA-Z]"))
        //    {
        //        MessageBox.Show("Invalid date entry", "Error");
        //        return;
        //    }
        //    if (custom.Length > 1)
        //    {
        //        if (customNew[1] == "-" && custom.Length == 4)
        //        {/* single digit month */ } else if (customNew[2] == "-" && custom.Length == 4)
        //        {/* single digit day */ }
        //        customMonth = custom.Substring(0, 2);
        //        customDay = custom.Substring(3, 2);
        //        DateTimeOffset customDate = new DateTimeOffset(DateTime.Now.Year, Convert.ToInt32(customMonth), Convert.ToInt32(customDay), 0, 0, 0, TimeSpan.Zero);
        //        if (customDate > DateTime.Today)
        //        {
        //            customDays = (customDate - DateTime.Today).Days + 1;
        //            customText = $"It's {customDays} days until {custom}.";
        //        }
        //        else
        //        {
        //            customDays = (DateTime.Today - customDate).Days + 1;
        //            customText = $"It's been {customDays} days since {custom}.";
        //        }
        //        Clipboard.SetText(customDays.ToString());
        //        MessageBox.Show(customText, "Custom Date");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid date entry", "Error");
        //    }
        //}

        private void Custom(object sender, EventArgs e)
        {
            string custom = Interaction.InputBox("Enter a custom Date", "Custom Date", "MM-DD");
            string customText;
            int customDays;
            string[] customNew = custom.Select(c => c.ToString()).ToArray();
            var customMonth = "";
            var customDay = "";
            if (Regex.IsMatch(custom, @"[a-zA-Z]"))
            {
                MessageBox.Show("Invalid date entry", "Error");
                return;
            }
            if (custom.Length == 2) // no dash single digit month and day
            {
                customMonth = custom.Substring(0, 1);
                customDay = custom.Substring(1, 1);
            }
            else if (customNew[1] == "-" && custom.Length == 3) // single digit month and day
            {
                customMonth = custom.Substring(0, 1);
                customDay = custom.Substring(2, 1);
            }
            else if (customNew[1] == "-" && custom.Length == 4) // single digit month
            {
                customMonth = custom.Substring(0, 1);
                customDay = custom.Substring(2, 2);
            }
            else if (customNew[2] == "-" && custom.Length == 4) // single digit day
            {
                customMonth = custom.Substring(0, 2);
                customDay = custom.Substring(3, 1);
            }
            else if (customNew[2] == "-" && custom.Length == 5)
            {
                customMonth = custom.Substring(0, 2);
                customDay = custom.Substring(3, 2);
            }
            else if (custom.Length == 4) // no dash double digit month and day
            {
                customMonth = custom.Substring(0, 2);
                customDay = custom.Substring(2, 2);
            }
            else MessageBox.Show("Invalid date entry", "Error");
            DateTimeOffset customDate = new DateTimeOffset(DateTime.Now.Year, Convert.ToInt32(customMonth), Convert.ToInt32(customDay), 0, 0, 0, TimeSpan.Zero);
            if (customDate > DateTime.Today)
            {
                customDays = (customDate - DateTime.Today).Days + 1;
                customText = $"It's {customDays} days until {custom}.";
            }
            else
            {
                customDays = (DateTime.Today - customDate).Days + 1;
                customText = $"It's been {customDays} days since {custom}.";
            }
            Clipboard.SetText(customDays.ToString());
            MessageBox.Show(customText, "Custom Date");
        }

void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            notifyIcon1.Visible = false;

            Application.Exit();
        }
    }
}
