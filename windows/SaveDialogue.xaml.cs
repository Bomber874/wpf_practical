using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace wpf_practical
{
    /// <summary>
    /// Логика взаимодействия для SaveDialogue.xaml
    /// </summary>
    public partial class SaveDialogue : Window
    {
        public bool SaveFile = false;
        public string FileName;
        public SaveDialogue()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Parser.File(fileNameInput.Text))
            {
                MessageBox.Show($"Некорректное название файла\nВы ввели:{fileNameInput.Text}");
                return;
            }
            if (!fileNameInput.Text.EndsWith(".csv"))
            {
                fileNameInput.Text = fileNameInput.Text + ".csv";
            }
            FileName = fileNameInput.Text;
            SaveFile = true;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
