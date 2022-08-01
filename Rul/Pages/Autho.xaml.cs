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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Rul.Entities; //Подключение папки с моделью БД

namespace Rul.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        private int countUnsuccessful = 0; //Количество неверных попыток входа
        public Autho()
        {
            InitializeComponent();

            txtCaptcha.Visibility = Visibility.Hidden;       // Скрываем надпись и 
            textBlockCaptcha.Visibility = Visibility.Hidden; // поле для ввода капчи
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null)); //Переход на страницу клиента для неавторизованного пользователя
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim(); //Объявляем переменную, в которую будет записываться значения с TextBlock'а логина
            string password = txtPassword.Text.Trim(); //Объявляем переменную, в которую будет записываться значения с TextBlock'а пароля

            User user = new User(); //создаем пустой объект пользователя

            user = RulEntities.GetContext().User.Where(p => p.UserLogin == login && p.UserPassword == password).FirstOrDefault(); //Условие на нахождение пользователя с введенными логином и паролем
            int userCount = RulEntities.GetContext().User.Where(p => p.UserLogin == login && p.UserPassword == password).Count(); //Находим количество пользователей

            if (countUnsuccessful < 1) 
            {
                if (userCount > 0)                                                          //Если количество пользователей с веденными данными больше 0,
                {
                    MessageBox.Show("Вы вошли под: " + user.Role.RoleName.ToString());    //Пояляется окно информации
                    LoadForm(user.Role.RoleName.ToString(), user);                             //И передается роль в метод загрузки страниц по ролям 
                }
                else
                {
                    MessageBox.Show("Вы ввели неверно логин или пароль!");
                    countUnsuccessful++;
                    if (countUnsuccessful == 1) //Если количество неверных попыток равно 1,
                        GenerateCaptcha();     // генерируем капчу
                }
            }
            else
            {
                if (userCount > 0 && textBlockCaptcha.Text == txtCaptcha.Text)
                {
                    MessageBox.Show("Вы вошли под: " + user.Role.RoleName.ToString());
                    LoadForm(user.Role.RoleName.ToString(), user);
                }
                else
                {
                    MessageBox.Show("Введите данные заново!");
                }
            }
        }

        private void GenerateCaptcha()
        {
            txtCaptcha.Visibility = Visibility.Visible;         // Показываем надпись и 
            textBlockCaptcha.Visibility = Visibility.Visible;   // поле для ввода капчи

            Random random = new Random();   
            int randmNum = random.Next(0, 3); //Генерируем случайное число от 1 до 3

            switch(randmNum)
            {
                case 1:
                    textBlockCaptcha.Text = "ju2sT8Cbs"; //Если случайное число равно 1, выводим капчу в TextBlock
                    textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 2:
                    textBlockCaptcha.Text = "iNwK2cl";  //Если случайное число равно 2, выводим капчу в TextBlock
                    textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 3:
                    textBlockCaptcha.Text = "uOozGk95"; //Если случайное число равно 3, выводим капчу в TextBlock
                    textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
            }
        }
        public void LoadForm(string _role, User user)
        {
            switch (_role)
            {
                case "Клиент":
                    NavigationService.Navigate(new Client(user)); //Если роль пользователя "Клиент", переходим на страницу клиента
                    break;
                case "Менеджер":
                    NavigationService.Navigate(new Client(user));
                    break;
                case "Администратор":
                    NavigationService.Navigate(new Admin(user));
                    break;
            }
        }
    }
}
