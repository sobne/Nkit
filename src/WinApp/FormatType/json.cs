﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace FormatStyler
{
    public partial class json : Form
    {
        public json()
        {
            InitializeComponent();
            textBox1.Text = "{\"name\":\"Xtools\",\"v1.0\":\"2021-01-01 16:05 工具上线\"}";
        }

        private void toolStripCompose_Click(object sender, EventArgs e)
        {
            textBox1.Text = FormatJsonString(textBox1.Text);
        }
        private string FormatJsonString(string str)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }

        private string Json2Xml(string str)
        {
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(str);
            //string jsonText = JsonConvert.SerializeXmlNode(doc);

            XmlDocument doc1 = JsonConvert.DeserializeXmlNode(str);
            return doc1.OuterXml;

        }
        private void toolStripJson2Xml_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = Json2Xml(textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("err:" + ex.Message);
                //throw;
            }
            
        }
    }
}
