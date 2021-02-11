using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace dateApp
{
    public partial class Form1 : Form
    {
        private DateTimeOffset newYears = new DateTimeOffset(DateTime.Now.Year, 1, 1, 0, 0, 0, TimeSpan.Zero);

        public Form1()
        {
            InitializeComponent();
        }

        private int getDay()
        {
            int today = (DateTime.Today - newYears).Days + 1;
            return today;
        }

        private string getPrefix()
        {
            var last = getDay() % 10;
            var prefix = prefixFinder(last);
            return prefix;
        }

        public string prefixFinder(int day)
        {
            switch (day)
            {
                case 1:
                    return "st";

                case 2:
                    return "nd";

                case 3:
                    return "rd";

                default:
                    return "th";
            }
        }

        public string getCustom(string custom)
        {
            string[] customNew = custom.Select(c => c.ToString()).ToArray();
            string customDate = "";

            if (Regex.IsMatch(custom, @"[a-zA-Z]"))
            {
                MessageBox.Show("Invalid date entry", "Error");
                return "";
            }
            else if (custom == "") // exiting the popup
            {
                return "";
            }
            else if (custom.Length == 2) // no dash single digit month and day
            {
                customDate = $"0{custom[0]}-0{custom[1]}";
            }
            else if (customNew[1] == "-" && custom.Length == 3) // single digit month and day
            {
                customDate = $"0{custom[0]}-0{custom[2]}";
            }
            else if (custom.Length == 3) // single digit month and two digit day
            {
                customDate = $"0{custom[0]}-{custom.Substring(1, 2)}";
            }
            else if (customNew[1] == "-" && custom.Length == 4) // single digit month
            {
                customDate = $"0{custom}";
            }
            else if (customNew[2] == "-" && custom.Length == 4) // single digit day
            {
                customDate = $"{custom.Substring(0, 2)}-0{custom[3]}";
            }
            else if (customNew[2] == "-" && custom.Length == 5) // double digit month and day
            {
                customDate = custom;
            }
            else if (custom.Length == 4) // no dash double digit month and day
            {
                customDate = $"{custom.Substring(0, 2)}-{custom.Substring(2, 2)}";
            }
            else MessageBox.Show("Invalid date entry", "Error");
            return customDate;
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

        private void Custom(object sender, EventArgs e)
        {
            string custom = Interaction.InputBox("Enter a custom Date", "Custom Date", "MM-DD");
            string customText;
            int customDays;
            var newDate = getCustom(custom);

            DateTimeOffset customDate = new DateTimeOffset(DateTime.Now.Year, Convert.ToInt32(newDate.Substring(0, 2)), Convert.ToInt32(newDate.Substring(3, 2)), 0, 0, 0, TimeSpan.Zero);
            if (customDate > DateTime.Today)
            {
                customDays = (customDate - DateTime.Today).Days + 1;
                customText = $"It's {customDays} days until {newDate}-{DateTime.Now.Year}.";
            }
            else
            {
                customDays = (DateTime.Today - customDate).Days + 1;
                customText = $"It's been {customDays} days since {newDate}-{DateTime.Now.Year}.";
            }
            Clipboard.SetText(customDays.ToString());
            MessageBox.Show(customText, "Custom Date");
        }

        private void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            notifyIcon1.Visible = false;
            Application.Exit();
        }
    }
}