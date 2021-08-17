using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace StartShutdownTime
{
    public partial class Form1 : Form
    {
        static int index = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            StreamWriter file = new StreamWriter(@"\\192.168.0.102\\sharedwithothers\\output.csv");
            //StreamWriter file = new StreamWriter(@"Z:\\test\\output.csv");
            var csv = new StringBuilder();
            string first = "";
            string second = "";
            string eventLogName = "System";
            EventLog eventLog = new EventLog();
            eventLog.Log = eventLogName;
            foreach (EventLogEntry log in eventLog.Entries)
            {
                if (log.EventID == 13 )//shutdown
                {
                    string str ="Shutdown["+log.TimeGenerated.ToString()+"]\n";
                    first = "Shutdown";
                    second = log.TimeGenerated.ToString();
                    listBox1.Items.Insert(index, str);
                   // file.Write(str);
                    index++;
                }
                if (log.EventID == 12)//start
                {
                    first = "Start";
                    second = log.TimeGenerated.ToString();
                    string str = "Start [" + log.TimeGenerated.ToString() + "]\n";
                     listBox1.Items.Insert(index,str );
                    // file.Write(str);
                    index++;
                }
                var newLine = string.Format("{0},{1}", first, second);
                csv.AppendLine(newLine);
                file.WriteLine(newLine);
            }
            //file.Write(csv.ToString());
            file.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\test", true);
            //key.SetValue("tested", "TESTED");
            //ModifyRegistry myRegistry = new ModifyRegistry();
            //myRegistry.SubKey = "SOFTWARE\\RTF_SHARP_EDIT\\RECENTFILES";
            //myRegistry.ShowError = true;
            //myRegistry.DeleteSubKeyTree();

            // Create an instance of HKEY_LOCAL_MACHINE registry key
            RegistryKey parentKey = Registry.LocalMachine;

            // Open the SOFTWARE registry sub key under the HKEY_LOCAL_MACHINE parent key
            RegistryKey softwareKey = parentKey.OpenSubKey("SOFTWARE", true);

            // Create a DaveOnC# registry sub key under the SOFTWARE key
            RegistryKey subKey = softwareKey.CreateSubKey("mysubkey");

            // Add values to DaveOnC# sub key
            subKey.SetValue("Name", "test", RegistryValueKind.String);
        }
    }
}
