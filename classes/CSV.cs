using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace wpf_practical
{
    static public class CSV
    {
        /// <summary>
        /// Сохраняет данные из компонента DataGridView в формате CSV
        /// Если файл с данным именем существует, он будет перезаписан
        /// </summary>
        /// <param name="filename">Название файла</param>
        /// <returns></returns>
        static public bool Save(string filename, DataGrid dataGridView)
        {
            StreamWriter streamWriter = new StreamWriter(filename, false, Encoding.UTF8);
            try
            {
                foreach(DataGridColumn column in dataGridView.Columns)
                {
                    streamWriter.Write((column.Header != null ? column.Header : "null") + ";");
                }
                streamWriter.Write(Environment.NewLine);
                foreach(Order order in dataGridView.Items)
                {
                    foreach(string info in order.ToArray())
                    {
                        streamWriter.Write(info+';');
                    }
                    streamWriter.Write(Environment.NewLine);
                }
                streamWriter.Close();
                streamWriter.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                streamWriter.Close();
                streamWriter.Dispose();
                return false;
            }
        }
    }
}
