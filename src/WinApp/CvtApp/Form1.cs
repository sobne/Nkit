﻿using System;
using System.Windows.Forms;
using Nkit.Core;

namespace CvtApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtStr_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txtStr.Text)) return;
            try
            {
                byte[] bits = Convertor.String2Bytes(txtStr.Text);
                txtAscii.Text = Convertor.Bytes2AsciiStr(bits);
                txtHex.Text = Convertor.Bytes2Hex(bits);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void txtAscii_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtHex_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txtHex.Text)) return;
            try
            {
                byte[] bits = Convertor.Hex2Bytes(txtHex.Text);
                txtStr.Text = Convertor.Bytes2String(bits);
                txtAscii.Text = Convertor.Bytes2AsciiStr(bits);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private void txtBin_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBin.Text)) return;
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class BitConvertor
    {
        public string Str { get; set; }
        public string Hex { get; set; }
        public string Asc { get; set; }
        public void BitConverter(byte[] bits)
        {
            Str = Convertor.Bytes2String(bits);
            Asc = Convertor.Bytes2AsciiStr(bits);
            Hex = Convertor.Bytes2Hex(bits);
        }
    }
}
