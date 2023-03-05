﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StopWatch_3
{
    public partial class Form1 : Form
    {
        private int count = 0;
        private bool isRunning = false;
        private List<int> savedTimes = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Saved Time", 200, HorizontalAlignment.Center);
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                count = 0;
                timer1.Start();
                btnStop.Enabled = true;
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                btnStop.Text = "Tiếp Tục";
                timer1.Stop();
                savedTimes.Add(count);
                ListViewItem item = new ListViewItem();
                listView1.View = View.Details;
                item.UseItemStyleForSubItems = false;
                item.Text = TimeSpan.FromSeconds(count).ToString(@"hh\:mm\:ss");
                listView1.Items.Add(item);
                isRunning = true;
            }
            else
            {
                timer1.Start();
                btnStop.Text = "Tạm dừng";
                isRunning = false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            isRunning = false;
            timer1.Stop();
            listView1.Items.Clear();
            savedTimes.Clear();
            count = 0;
            btnStop.Text = "Tạm dừng";
            lblTime.Text = "00:00:00";
            btnStop.Enabled = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            TimeSpan time = TimeSpan.FromSeconds(count);
            lblTime.Text = time.ToString(@"hh\:mm\:ss");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnReset_Click(sender, e);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one item to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (ListViewItem item in listView1.SelectedItems)
            {
                savedTimes.Remove((int)TimeSpan.Parse(item.Text).TotalSeconds);
                listView1.Items.Remove(item);
            }
        }
    }
}
