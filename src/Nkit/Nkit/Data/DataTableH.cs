using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Nkit.Data
{
    public class DataTableH
    {
        /// <summary>
        /// 交换DataTable中的行列位置
        /// </summary>
        /// <param name="inputDT">要交换的DataTable</param>
        /// <returns>交换后的DataTable</returns>
        private static DataTable SwapDTCR(DataTable inputDT)
        {
            DataTable outputDT = new DataTable();

            //标题的位置不变
            outputDT.Columns.Add(inputDT.Columns[0].ColumnName.ToString());

            foreach (DataRow inRow in inputDT.Rows)
            {
                string newColName = inRow[0].ToString();
                outputDT.Columns.Add(newColName);
            }

            for (int rCount = 1; rCount <= inputDT.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputDT.NewRow();

                newRow[0] = inputDT.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputDT.Rows.Count - 1; cCount++)
                {
                    string colValue = inputDT.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputDT.Rows.Add(newRow);
            }
            return outputDT;
        }
        /// <summary>
        /// 过滤行(从第0行开始)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DataTable tableFilter(DataTable dt, int start, int end)
        {
            DataTable dtnew = new DataTable();
            dtnew = dt.Clone();
            for (int i = start; i < end; i++)
            {
                dtnew.Rows.Add(dt.Rows[i].ItemArray);
            }
            return dtnew;
        }


        public static List<T> ToList<T>(DataTable dt) where T : class, new()
        {
            return null;
        }
    }
}
