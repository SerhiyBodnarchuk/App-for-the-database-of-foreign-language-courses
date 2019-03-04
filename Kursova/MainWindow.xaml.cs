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
using System.Data.SqlClient;

namespace Kursova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static string path = @"Data Source =.\SQLEXPRESS;Initial Catalog = English Courses; Integrated Security = True";
        SqlConnection con = new SqlConnection(path);

        private static string Log = " ";
        private static string Pass = " ";


        public MainWindow()
        {
            InitializeComponent();
            con.Open();
            BitmapImage bit = new BitmapImage();
            bit.BeginInit();
            bit.UriSource = new Uri("Image/Hide.jpg", UriKind.Relative);
            bit.EndInit();
            Show_Hide.Stretch = Stretch.Fill;
            Show_Hide.Source = bit;
            PasswordShow.Text = Password.Password;
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)//Підказка(Image), чи існує логін в базі даних
        {
            BitmapImage bit = new BitmapImage();
            bit.BeginInit();
            if (CheckLog("Customer", "id_customer"))
            {
                bit.UriSource = new Uri("Image/Green.png", UriKind.Relative);
            }
            else if (CheckLog("Teacher", "id_teacher"))
            {
                bit.UriSource = new Uri("Image/Green.png", UriKind.Relative);
            }
            else if (Login.Text == "Admin")
            {
                bit.UriSource = new Uri("Image/Green.png", UriKind.Relative);
            }
            else
            {
                bit.UriSource = new Uri("Image/Red.png", UriKind.Relative);
            }
            bit.EndInit();
            Check1.Stretch = Stretch.Fill;
            Check1.Source = bit;
        }

        private void Log_in_Click(object sender, RoutedEventArgs e)//Вхід в робочу область
        {

            CheckAll("Teacher", "id_teacher");
            CheckAll("Customer", "id_customer");
            if (Login.Text == Log && Password.Password == Pass)
            {
                ViewOnly viewonly = new ViewOnly();
                viewonly.Show();
                con.Close();
                this.Close();
            }
            else if (Login.Text == "Admin" && Password.Password == "1234")
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                con.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("You input incorrect login or password!\nHint : Password is your day of birth.", "Warning!");
            }
        }

        private bool CheckLog(string Table, string id_)//Перевіряю чи логін існує в базі даних
        {
            string Log_local = "";
            for (int id = 1; id <= 100; id++)
            {
                SqlCommand Command1 = con.CreateCommand();
                Command1.CommandText = @"SELECT [PIB] FROM [English courses].[dbo].[" + Table + "] where " + id_ + "=" + id.ToString() + ";";
                SqlDataReader Reader1 = Command1.ExecuteReader();
                while (Reader1.Read())
                {
                    Log_local = Reader1["PIB"].ToString();
                }
                Reader1.Close();
                if (Login.Text == Log_local)
                {
                    Log = Log_local;
                    return true;
                }
            }
            return false;
        }

        private void CheckAll(string Table, string id_)//Перевіряю чи логін і пароль існує в базі даних
        {
            string Log_local = "", Pass_local = "";
            for (int id = 1; id <= 100; id++)
            {
                SqlCommand Command1 = con.CreateCommand();
                Command1.CommandText = @"SELECT [PIB] FROM [English courses].[dbo].[" + Table + "] where " + id_ + "=" + id.ToString() + ";";
                SqlDataReader Reader1 = Command1.ExecuteReader();
                while (Reader1.Read())
                {
                    Log_local = Reader1["PIB"].ToString();
                }
                Reader1.Close();

                SqlCommand Command2 = con.CreateCommand();
                Command2.CommandText = @"SELECT [Day_of_Birth] FROM [English courses].[dbo].[" + Table + "] where " + id_ + "=" + id.ToString() + ";";
                SqlDataReader Reader2 = Command2.ExecuteReader();
                while (Reader2.Read())
                {
                    Pass_local = Reader2["Day_of_Birth"].ToString();
                }
                Reader2.Close();
                if (Login.Text == Log_local && Password.Password == Pass_local)
                {
                    Log = Log_local;
                    Pass = Pass_local;
                    break;
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)//Вихід через кнопку
        {
            Close();
        }

        void PasswordChangedHandler(Object sender, RoutedEventArgs args)//Оновлення при змінні в паролі
        {
            PasswordShow.Text = Password.Password;
        }

        void Show_left(Object sender, RoutedEventArgs args)//Показуєм / ховаєм пароль
        {
            
            if (Password.IsVisible == false)
            {
                BitmapImage bit = new BitmapImage();
                bit.BeginInit();
                bit.UriSource = new Uri("Image/Hide.jpg", UriKind.Relative);
                bit.EndInit();
                Show_Hide.Stretch = Stretch.Fill;
                Show_Hide.Source = bit;
                Password.Visibility = Visibility.Visible;
                PasswordShow.Visibility = Visibility.Hidden;
            }
            else
            {
                BitmapImage bit = new BitmapImage();
                bit.BeginInit();
                bit.UriSource = new Uri("Image/Show.jpg", UriKind.Relative);
                bit.EndInit();
                Show_Hide.Stretch = Stretch.Fill;
                Show_Hide.Source = bit;
                Password.Visibility = Visibility.Hidden;
                PasswordShow.Visibility = Visibility.Visible;
            }
        }
    }
}
