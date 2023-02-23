using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace wpf_practical
{
    /// <summary>
    /// Логика взаимодействия для ViewLogWindow.xaml
    /// </summary>
    public partial class ViewLogWindow : Window
    {
        string[] logText;
        public ViewLogWindow(string[] log)
        {
            InitializeComponent();
            logText = log;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Regex error = new Regex(@"^\d?\d:\d\d:\d\d\[ERROR\]:.+$");
            Regex ok = new Regex(@"^\d?\d:\d\d:\d\d\[OK\]:.+$");    // Если бы нужна была необходимость подсветки больших типов сообщений, сделал бы массив
            foreach (string _line in logText)
            {
                string line = _line+'\n';
                if (ok.IsMatch(line))   // Было бы больше, написал бы метод(я не лентяй)
                {
                    //richTextBox1.SelectionStart = richTextBox1.TextLength;
                    //MessageBox.Show(richTextBox1.SelectionStart+"");
                    //richTextBox1.AppendText(line + Environment.NewLine);

                    TextRange tr = new TextRange(richTextBox1.Document.ContentEnd, richTextBox1.Document.ContentEnd);
                    tr.Text = line;
                    tr.ApplyPropertyValue(ForegroundProperty, Brushes.Green);
                    //MessageBox.Show(richTextBox1.SelectionStart + "");
                    //richTextBox1.SelectionLength = line.Length;
                    //richTextBox1.SelectionColor = Color.Green;
                    //MessageBox.Show(richTextBox1.SelectionStart+" "+richTextBox1.SelectionLength);
                }
                else if (error.IsMatch(line))
                {
                    TextRange tr = new TextRange(richTextBox1.Document.ContentEnd, richTextBox1.Document.ContentEnd);
                    tr.Text = line;
                    tr.ApplyPropertyValue(ForegroundProperty, Brushes.Red);
                }
                else
                {
                    TextRange tr = new TextRange(richTextBox1.Document.ContentEnd, richTextBox1.Document.ContentEnd);
                    tr.Text = line;
                    tr.ApplyPropertyValue(ForegroundProperty, Brushes.Black);
                }
            }
            //richTextBox1.Select(0, 0);
        }
    }
}
