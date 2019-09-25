using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundWorkerExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        struct DataParameter
        {
            public int Process;
            public int Delay;
        }
        private DataParameter _intputparameter;
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
            lblPercent.Text = string.Format("Processing ....{0}%", e.ProgressPercentage);
            progress.Update();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Process has been completed.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int process = ((DataParameter)e.Argument).Process;
            int delay = ((DataParameter)e.Argument).Delay;
            int index = 1;
            try
            {
                for(int i=0;i<process;i++)
                {
                    if(!backgroundWorker.CancellationPending)
                    {
                        backgroundWorker.ReportProgress(index++ * 100 / process, string.Format("Process data {0}", i));
                        Thread.Sleep(delay);
                        //add here your code
                    }
                }
            }
            catch(Exception ex)
            {
                backgroundWorker.CancelAsync();
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(!backgroundWorker.IsBusy)
            {
                _intputparameter.Delay = 10;
                _intputparameter.Process = 1000;
                backgroundWorker.RunWorkerAsync(_intputparameter);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
                backgroundWorker.CancelAsync();
        }
    }
}
