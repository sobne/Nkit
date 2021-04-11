using Newtonsoft.Json;
using Nkit.Core.Utils;
using System;
using System.Windows.Forms;
using System.Xml;

namespace FormatStyler
{
    public partial class xmlFrm : Form
    {
        public xmlFrm()
        {
            InitializeComponent();
            textBox1.Text = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><root><child name=\"Xtools\">Xtools</child></root>";
        }
        private void toolStripCompose_Click(object sender, EventArgs e)
        {
            var str = XMLHelper.Format(textBox1.Text);
            textBox1.Text = str;
        }
        private string Xml2Json(string str)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(str);
            return JsonConvert.SerializeXmlNode(doc);

        }
        private void toolStripXml2Json_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = Xml2Json(textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("err:" + ex.Message);
                //throw;
            }

        }
    }
}
