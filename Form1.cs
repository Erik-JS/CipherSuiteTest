using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CipherSuiteTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = Environment.OSVersion.ToString() + " | " + (Environment.Is64BitOperatingSystem ? "64-bit" : "32-Bit");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateListBox();
            var c1 = listBox1.Items.Contains("TLS_RSA_WITH_RC4_128_SHA");
            var c2 = listBox1.Items.Contains("TLS_RSA_WITH_RC4_128_MD5");
            MessageBox.Show("TLS_RSA_WITH_RC4_128_SHA " + (c1 ? "is present": "is missing") + "\n" + "TLS_RSA_WITH_RC4_128_MD5 " + (c2 ? "is present" : "is missing"));
        }

        private void UpdateListBox()
        {
            var cipherList = CipherSuiteControl.GetCipherSuiteList();
            listBox1.Items.Clear();
            foreach (var ciphersuite in cipherList)
                listBox1.Items.Add(ciphersuite);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int v1 = CipherSuiteControl.AddCipherSuite("TLS_RSA_WITH_RC4_128_SHA");
            int v2 = CipherSuiteControl.AddCipherSuite("TLS_RSA_WITH_RC4_128_MD5");
            MessageBox.Show(String.Format("Add TLS_RSA_WITH_RC4_128_SHA = {0:X8}\nAdd TLS_RSA_WITH_RC4_128_MD5 = {1:X8}", v1, v2));
            UpdateListBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int v1 = CipherSuiteControl.RemoveCipherSuite("TLS_RSA_WITH_RC4_128_SHA");
            int v2 = CipherSuiteControl.RemoveCipherSuite("TLS_RSA_WITH_RC4_128_MD5");
            MessageBox.Show(String.Format("Remove TLS_RSA_WITH_RC4_128_SHA = {0:X8}\nRemove TLS_RSA_WITH_RC4_128_MD5 = {1:X8}", v1, v2));
            UpdateListBox();
        }
    }
}
