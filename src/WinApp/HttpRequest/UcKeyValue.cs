using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpRequest
{
    public partial class UcKeyValue : UserControl
    {
        public UcKeyValue()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.RowCount - 1)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                if (column is DataGridViewButtonColumn)
                {
                    this.dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        public Dictionary<string, string> GetValues()
        {
            var dic = new Dictionary<string, string>();
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                var row = dataGridView1.Rows[i];
                if (row.Cells["Key"].Value == null || string.IsNullOrEmpty(row.Cells["Key"].Value.ToString()))
                    continue;
                if (dic.ContainsKey(row.Cells["Key"].Value.ToString())) continue;
                dic.Add(row.Cells["Key"].Value.ToString(), row.Cells["Value"].Value == null ? "" : row.Cells["Value"].Value.ToString());
            }
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (row.Cells["Key"].Value==null || string.IsNullOrEmpty(row.Cells["Key"].Value.ToString()))
            //        break;
            //    //var keyValue = new KeyValuePair<string, string>(row.Cells["Key"].Value.ToString(), row.Cells["Value"].Value.ToString());
            //    dic.Add(row.Cells["Key"].Value.ToString(), row.Cells["Key"].Value == null?"":row.Cells["Value"].Value.ToString());
            //}
            return dic;
        }
        public string GetJson()
        {
            var keyValues = GetValues();
            if (!keyValues.Any()) return "";
            var list = new List<string>();
            foreach (var kvp in keyValues)
            {
                list.Add($"\"{kvp.Key}\":\"{kvp.Value}\"");
            }
            var str = string.Join(",", list.ToArray());
            return "{" + str + "}";
        }
        public string GetPraram()
        {
            var keyValues = GetValues();
            if (!keyValues.Any()) return "";
            var list = new List<string>();
            foreach (var kvp in keyValues)
            {
                list.Add($"{kvp.Key}={kvp.Value}");
            }
            var str = string.Join("&", list.ToArray());
            return str;
        }
    }
}
