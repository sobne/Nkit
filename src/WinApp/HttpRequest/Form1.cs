﻿using Nkit.Net.Http;
using System;
using System.Windows.Forms;

namespace HttpRequest
{
    public partial class Form1 : Form
    {
        private UcKeyValue ucHeader;
        private UcKeyValue ucTab2FormData;
        private UcKeyValue ucTab2UrlEncoded;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbMethod.SelectedIndex = 0;
            cmbAuth.SelectedIndex = 0;
            cmbRaw.SelectedIndex = 0;

            //Header
            ucHeader = new UcKeyValue();
            ucHeader.Margin = new Padding(20);
            ucHeader.Dock = DockStyle.Fill;
            tabHeaders.Controls.Add(ucHeader);
            //Body form-data
            ucTab2FormData = new UcKeyValue();
            ucTab2FormData.Margin = new Padding(20);
            ucTab2FormData.Dock = DockStyle.Fill;
            tab2_form_data.Controls.Add(ucTab2FormData);
            //Body form-urlencoded
            ucTab2UrlEncoded = new UcKeyValue();
            ucTab2UrlEncoded.Margin = new Padding(20);
            ucTab2UrlEncoded.Dock = DockStyle.Fill;
            tab2_x_www_form_urlencoded.Controls.Add(ucTab2UrlEncoded);
        }

        private void cmbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var tpBody = tabControl1.TabPages[2];
            
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            txtResponse.Clear();
            txtResponse.AppendText("ucHeader:" + ucHeader.GetJson());
            txtResponse.AppendText(Environment.NewLine + "ucHeader:" + ucHeader.GetPraram());
            txtResponse.AppendText(Environment.NewLine + "ucTab2FormData:" + ucTab2FormData.GetJson());
            txtResponse.AppendText(Environment.NewLine + "ucTab2FormData:" + ucTab2FormData.GetPraram());
            txtResponse.AppendText(Environment.NewLine + "ucTab2UrlEncoded:" + ucTab2UrlEncoded.GetJson());
            txtResponse.AppendText(Environment.NewLine + "ucTab2UrlEncoded:" + ucTab2UrlEncoded.GetPraram());
            txtResponse.AppendText(Environment.NewLine);


            var url = txtUrl.Text;
            if (string.IsNullOrEmpty(url)) return;

            WebResponseParameter response = null;
            IHttpProvider httpProvider = new HttpProvider();

            var method = HttpMethod.GET;
            Enum.TryParse(cmbMethod.Text, out method);

            if(method.Equals(HttpMethod.GET))
                response = httpProvider.Get(txtUrl.Text);
            else if (method.Equals(HttpMethod.POST))
            {   
                var tab2Selected = tabControl2.SelectedTab.Name.ToLower();
                if (tab2Selected.Equals("tab2_form_data"))
                {
                    var kvp = ucTab2FormData.GetValues();
                    response = httpProvider.Post(txtUrl.Text, kvp);
                }
                else if (tab2Selected.Equals("tab2_x_www_form_urlencoded"))
                {
                    var kvp = ucTab2UrlEncoded.GetValues();
                    response = httpProvider.Post(txtUrl.Text, kvp);
                }
                else if (tab2Selected.Equals("tab2_raw"))
                {
                    var rawType = cmbRaw.Text.ToLower();
                    if (rawType.Equals("Text".ToLower()))
                    {

                    }
                    else if (rawType.Equals("Text(text/plain)".ToLower()))
                    {

                    }
                    else if (rawType.Equals("JSON(application/json)".ToLower()))
                    {
                        var json = txtRaw.Text;
                        if (!string.IsNullOrEmpty(json))
                        {
                            response = httpProvider.Post(txtUrl.Text, json);
                        }
                    }
                    else if (rawType.Equals("Javascript(application/javascript)".ToLower()))
                    {

                    }
                    else if (rawType.Equals("XML(text/xml)".ToLower()))
                    {

                    }
                    else if (rawType.Equals("HTML(text/html)".ToLower()))
                    {

                    }
                }
                else if (tab2Selected.Equals("tab2_binary"))
                {

                }
            }





            //switch (method)
            //{
            //    case HttpMethod.GET:
            //        response = httpProvider.Get(txtUrl.Text);
            //        break;
            //    case HttpMethod.POST:
            //        response = httpProvider.Post(txtUrl.Text,para);
            //        break;
            //    case HttpMethod.PUT:
            //        break;
            //    case HttpMethod.DELETE:
            //        break;
            //    case HttpMethod.HEAD:
            //        break;
            //    case HttpMethod.CONNECT:
            //        break;
            //    case HttpMethod.OPTIONS:
            //        break;
            //    case HttpMethod.TRACE:
            //        break;
            //    default:
            //        break;
            //}

            txtResponse.AppendText(response.Body);
        }
    }
}
