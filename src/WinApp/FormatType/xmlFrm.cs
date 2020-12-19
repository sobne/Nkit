using Newtonsoft.Json;
using Nkit.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FormatType
{
    public partial class xmlFrm : Form
    {
        public xmlFrm()
        {
            InitializeComponent();
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
