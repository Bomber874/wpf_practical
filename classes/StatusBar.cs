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
        private static StatusBar _instance;
        StatusBar(RichTextBox richTextBox, string fileName)
        {
            _statusBar = richTextBox;
            Directory.CreateDirectory("logs");
            FileName = fileName;
        }
        /// <summary>
        /// Точка доступа к объекту StatusBar
        /// Перед использованием, объект должен быть создан, методом StatusBar.GenerateObject
        /// </summary>
        public static StatusBar Instance {
            get
            {
                return _instance;
            }
        }
        /// <summary>
        /// Создаёт объект StatusBar, для дайнейшего доступа через StatusBar.Instance
        /// </summary>
        /// <param name="richTextBox">RichTextBox, в который будет выводиться последнее действие</param>
        /// <param name="fileName">Путь на файла логов</param>
        public static void GenerateObject(RichTextBox richTextBox, string fileName)
        {
            _instance = new StatusBar(richTextBox, fileName);
        }
        public enum TYPE
        {
            ERROR,
            OK,
            INFO,
            SHUTDOWN
        }
        private void WriteLogToFile(string message)
        {
            //File.AppendAllLines(FileName, new string[] { message });
            StreamWriter sw = File.AppendText(FileName);
            sw.WriteLine(message);
            sw.Close();
            sw.Dispose();
        }
        /// <summary>
        /// Сохраняет в строку состояния и лог-файл сообщение
        /// </summary>
        /// <param name="message">Сообщение для сохранения в лог</param>
        /// <param name="type">StatusBar.TYPE</param>
        public void Log(string message, TYPE type)
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
            WriteLogToFile(ToShow);
        }
    }
}
