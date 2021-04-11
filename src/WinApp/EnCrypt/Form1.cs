using Nkit.Security;
using System;
using System.Windows.Forms;

namespace EnCrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (radAes.Checked)
        //    {
        //        var aes = new Aes();
        //        try
        //        {
        //            txtOutput.Text = aes.Encrypt(txtInput.Text);
        //        }
        //        catch (Exception)
        //        {
        //            txtOutput.Text = aes.Decrypt(txtInput.Text);
        //        }
        //    }
        //    if (radBase64.Checked)
        //    {
        //        var b64 = new Base64();
        //        try
        //        {
        //            txtOutput.Text = b64.Encode(txtInput.Text);
        //        }
        //        catch (Exception)
        //        {
        //            txtOutput.Text = b64.Decode(txtInput.Text);
        //        }
        //    }
        //    if (radDes.Checked)
        //    {
        //        var des = new Des();
        //        try
        //        {
        //            txtOutput.Text = des.Encode(txtInput.Text);
        //        }
        //        catch (Exception)
        //        {
        //            txtOutput.Text = des.Decode(txtInput.Text);
        //        }
        //    }
        //    if (radShift.Checked)
        //    {
        //        var shift = new Shift();
        //        try
        //        {
        //            txtOutput.Text = shift.Encrypt(txtInput.Text);
        //        }
        //        catch (Exception)
        //        {
        //            txtOutput.Text = shift.Decrypt(txtInput.Text);
        //        }
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            if (radAes.Checked)
            {
                var aes = new Aes();
                txtOutput.Text = aes.Encrypt(txtInput.Text);
            }
            if (radBase64.Checked)
            {
                var b64 = new Base64();
                txtOutput.Text = b64.Encode(txtInput.Text);
            }
            if (radDes.Checked)
            {
                var des = new Des();
                txtOutput.Text = des.Encode(txtInput.Text);
            }
            if (radShift.Checked)
            {
                var shift = new Shift();
                txtOutput.Text = shift.Encrypt(txtInput.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radAes.Checked)
            {
                var aes = new Aes();
                txtOutput.Text = aes.Decrypt(txtInput.Text);
            }
            if (radBase64.Checked)
            {
                var b64 = new Base64();
                txtOutput.Text = b64.Decode(txtInput.Text);
            }
            if (radDes.Checked)
            {
                var des = new Des();
                txtOutput.Text = des.Decode(txtInput.Text);
            }
            if (radShift.Checked)
            {
                var shift = new Shift();
                txtOutput.Text = shift.Decrypt(txtInput.Text);
            }
        }
    }
}
