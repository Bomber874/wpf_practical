using System;
using System.Collections.Generic;
using System.Globalization;
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
using wpf_practical.classes;

namespace wpf_practical.windows
{
    // TODO: Добавить проверку валидности данных, для дальнейшего отключения кнопки сохранения
    class NameVRule : ValidationRule
    {
        public NameVRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex r = new Regex(@"^[А-ЯЙЁа-яйё]+([ -][А-ЯЙЁа-яйё]+)?$");
            try
            {
                if (r.IsMatch(value as string))
                    return ValidationResult.ValidResult;
                return new ValidationResult(false, $"Допустимые символы: \"А-я, ,-\"");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Произошла ошибка: {e.Message}");
            }
        }
    }

    class PhoneVRule : ValidationRule
    {
        public PhoneVRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex r = new Regex(@"^((\+7)|8|7)\d{10,10}$");
            try
            {
                if (r.IsMatch(value as string))
                    return ValidationResult.ValidResult;
                return new ValidationResult(false, $"Код страны должен быть: +7|8|7. Дальше 10 цифр номера телефона");
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Произошла ошибка: {e.Message}");
            }
        }
    }

    /// <summary>
    /// Логика взаимодействия для NewClient.xaml
    /// </summary>
    public partial class NewClient : Window
    {
        public Client client { get; set; }
        dbModel db = dbModel.Instance;
        public NewClient()
        {
            InitializeComponent();
            client = new Client();
            client.BirthDay = DateTime.Today; // Иначе выводился бы день рождения Иисуса(), нам же этого не нужно, верно?
            DataContext = client;
        }
        // Добавил второй конструктор, чтобы не писать ещё одну форму для редактирования сущетсвующего клиента
        public NewClient(Client client)
        {
            InitializeComponent();
            this.Title = $"Редактирование клиента(id:{client.ID} {client.FName})";
            this.client = client;
            DataContext = this.client;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FailedValidation.Count != 0)
            {
                MessageBox.Show($"{FailedValidation.First().Value}\nВы ввели:{FailedValidation.First().Key.Text}");
                return;
            }
            this.DialogResult = true;
            Close();
        }
        Dictionary<TextBox, string> FailedValidation = new Dictionary<TextBox, string>();
        private void TextBoxValidation_Error(object sender, ValidationErrorEventArgs e)
        {
            // Вызывается дважды. Сначала ValidationErrorEventAction.Added, за тем-что-то другое
            if (e.Action == ValidationErrorEventAction.Added)
            {
                FailedValidation[e.Source as TextBox] = e.Error.ErrorContent.ToString();
            }
            else
                 if (!((BindingExpressionBase)e.Error.BindingInError).HasError) // Копипаста с гитхаба
            {
                FailedValidation.Remove(e.Source as TextBox);
            }
        }
    }
}
