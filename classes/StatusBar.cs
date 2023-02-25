using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace wpf_practical
{
    public class StatusBar
    {
        // Знал бы я про синглтон раньше, этого бы не произошло
        RichTextBox _statusBar;
        public readonly string FileName;
        public StatusBar(RichTextBox richTextBox, string fileName)
        {
            _statusBar = richTextBox;
            Directory.CreateDirectory("logs");
            FileName = fileName;
        }
        public enum TYPE
        {
            ERROR,
            OK,
            INFO,
            SHUTDOWN
        }
        public void WriteLog(string message)
        {
            //File.AppendAllLines(FileName, new string[] { message });
            StreamWriter sw = File.AppendText(FileName);
            sw.WriteLine(message);
            sw.Close();
            sw.Dispose();
        }

        public void Show(string message, TYPE type)
        {
            if (message.Trim() == "")
                return;
            string prefix;
            Color color;
            switch (type)
            {
                case (TYPE.INFO):
                    prefix = "[INFO]";
                    color = Colors.Gray;
                    break;

                case (TYPE.ERROR):
                    prefix = "[ERROR]";
                    color = Colors.Red;
                    break;
                case (TYPE.OK):
                    prefix = "[OK]";
                    color = Colors.Green;
                    break;
                case (TYPE.SHUTDOWN):
                    prefix = "[SHUTDOWN]";
                    color = Colors.Green;
                    break;
                default:
                    color = Colors.Gray;
                    prefix = "";
                    break;
            }
            string ToShow = $"{DateTime.Now.ToLongTimeString()}{prefix}: {message}";
            TextRange doc = new TextRange(_statusBar.Document.ContentStart, _statusBar.Document.ContentEnd);
            doc.Text = ToShow;
            _statusBar.SelectAll();
            _statusBar.SelectionBrush = new SolidColorBrush(color);
            WriteLog(ToShow);
        }
    }
}
