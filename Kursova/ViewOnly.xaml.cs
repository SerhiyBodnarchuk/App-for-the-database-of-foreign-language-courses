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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Kursova
{
    /// <summary>
    /// Interaction logic for ViewOnly.xaml
    /// </summary>
    public partial class ViewOnly : Window
    {
        List<ListBox> ListOfListBox = new List<ListBox>();
        List<Button> ListOfButton = new List<Button>();

        static string path = @"Data Source =.\SQLEXPRESS;Initial Catalog = English Courses; Integrated Security = True";
        SqlConnection con = new SqlConnection(path);

        public ViewOnly()
        {
            InitializeComponent();
            ListOfListBox.AddRange(new ListBox[] { ListBox_Teacher, ListBox_Customer, ListBox_Studing, ListBox_Schedule, ListBox_Group, ListBox_Exam, ListBox_Level, ListBox_Language, ListBox_Payment });
            ListOfButton.AddRange(new Button[] { FindBy_Teacher, FindBy_Customer, FindBy_Studing, FindBy_Schedule, FindBy_Group, FindBy_Exam, FindBy_Level, FindBy_Language, FindBy_Payment });
        }

        private void Change_user_Click(object sender, RoutedEventArgs e)//Змінюємо користувача
        {
             MainWindow main = new MainWindow();
             main.Show();
             this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)//Вихід
        {
            Close();
        }

        private void Information_Click(object sender, RoutedEventArgs e)//Інформація про розробника
        {
            MessageBox.Show("This application was made by Serhiy Bodnarchuk, as a coursework.\nHe was born in Terebovlya, in Ternopil` region on May 4 , 2000.\n\t© Serhiy Bodnarchuk All rights reserved", "Information about developer");
        }

        /*                               //////////////////////////                    SELECT MENUITEM           ////////////////////////////                            */

        private void Teacher_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Teacher
        {
            Hide_Elements();
            ListBox_Teacher.Visibility = Visibility.Visible;
            Select("Teacher", ListBox_Teacher);
        }

        private void Customer_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Customer
        {
            Select("Customer", ListBox_Customer);
            Hide_Elements();
            ListBox_Customer.Visibility = Visibility.Visible;
        }

        private void Studing_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Studing
        {
            Hide_Elements();
            ListBox_Studing.Visibility = Visibility.Visible;
            Select("Studing", ListBox_Studing);
        }

        private void Schedule_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Schedule
        {
            Hide_Elements();
            ListBox_Schedule.Visibility = Visibility.Visible;
            Select("Schedule", ListBox_Schedule);
        }

        private void Group_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Group
        {
            Hide_Elements();
            ListBox_Group.Visibility = Visibility.Visible;
            Select("Group", ListBox_Group);
        }

        private void Exam_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Exam
        {
            Hide_Elements();
            ListBox_Exam.Visibility = Visibility.Visible;
            Select("Exam", ListBox_Exam);
        }

        private void Level_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Level
        {
            Hide_Elements();
            ListBox_Level.Visibility = Visibility.Visible;
            Select("Level", ListBox_Level);
        }

        private void Language_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Language
        {
            Hide_Elements();
            ListBox_Language.Visibility = Visibility.Visible;
            Select("Language",ListBox_Language);
        }

        private void Payment_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Payment
        {
            Hide_Elements();
            ListBox_Payment.Visibility = Visibility.Visible;
            Select("Payment", ListBox_Payment);
        }

        /*                               //////////////////////////                   ФУНКЦІЇ           ////////////////////////////                            */

        private void Hide_Elements()//Функція , щоб ховати всі ListBox
        {
            foreach(ListBox l in ListOfListBox)
            {
                l.Visibility = Visibility.Hidden;
            }

            foreach (Button b in ListOfButton)
            {
                b.Visibility = Visibility.Hidden;
            }

            MyRectangle1.Visibility = Visibility.Hidden;
            MyRectangle2.Visibility = Visibility.Hidden;
            DataGridView.Visibility = Visibility.Hidden;
            MyCalendar.Visibility = Visibility.Hidden;
            ComboBox.Visibility = Visibility.Hidden;
        }

        private void DataGrid_Change_Size(string command)
        {
            if (command == "Select")
            {
                DataGridView.Height = 312;
                DataGridView.Width = 325;
                DataGridView.Margin = new Thickness(437, 72, 0, 0);
            }
            else
            {
                DataGridView.Height = 312;
                DataGridView.Width = 269;
                DataGridView.Margin = new Thickness(496, 72, 0, 0);
            }
        }

        private void Select(string Table,ListBox list)//Функція для заповнення datagrid
        {
            DataGridView.Visibility = Visibility.Visible;
            string str = "SELECT * ";
            foreach (CheckBox c in list.Items)
            {
                if (c.IsChecked == true)
                {
                    str = str.Replace('*', ' ');
                    str +="["+ c.Content+"] ,";
                }
            }
            str = str.Remove(str.Length - 1, 1);
            str +=" FROM[English courses].[dbo].[" + Table + "]; ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(Table);
            sda.Fill(dt);
            DataGridView.ItemsSource = dt.DefaultView;

        }

        private void Inst_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.instagram.com/serhiy_bodnarchuk/");
        }

        /*                               //////////////////////////                    SELECT CHECKED/UNCHEKED         ////////////////////////////                            */

        private void Teacher_CheckBox(object sender, RoutedEventArgs e)//Оновлює datagrid коли ставимо / забираємо галочку в ListBox_Teacher
        {
            Teacher_Select_Click(sender, e);
        }

        private void Customer_Checked(object sender, RoutedEventArgs e)//Оновлює datagrid коли ставимо / забираємо галочку в ListBox_Customer
        {
            Customer_Select_Click(sender, e);
        }

        private void Studing_Checked(object sender, RoutedEventArgs e)//Оновлює datagrid коли ставимо / забираємо галочку в ListBox_Studing
        {
            Studing_Select_Click(sender, e);
        }

        private void Schedule_Checked(object sender, RoutedEventArgs e)//Оновлює datagrid коли ставимо / забираємо галочку в ListBox_Schedule
        {
            Schedule_Select_Click(sender, e);
        }

        private void Group_Checked(object sender, RoutedEventArgs e)//Оновлює datagrid коли ставимо / забираємо галочку в ListBox_Group
        {
            Group_Select_Click(sender, e);
        }

        private void Exam_Checked(object sender, RoutedEventArgs e)//Оновлює datagrid коли ставимо / забираємо галочку в ListBox_Exam
        {
            Exam_Select_Click(sender, e);
        }

        private void Level_Checked(object sender, RoutedEventArgs e)//Оновлює datagrid коли ставимо / забираємо галочку в ListBox_Level
        {
            Level_Select_Click(sender, e);
        }

        private void Language_Checked(object sender, RoutedEventArgs e)//Оновлює datagrid коли ставимо / забираємо галочку в ListBox_Language
        {
            Language_Select_Click(sender, e);
        }

        private void Payment_Checked(object sender, RoutedEventArgs e)//Оновлює datagrid коли ставимо / забираємо галочку в ListBox_Payment
        {
            Payment_Select_Click(sender,e);
        }

        /*                               //////////////////////////                   FINDBY MENUITEM         ////////////////////////////                            */

        private void Teacher_FindBy_Click(object sender, RoutedEventArgs e)//Обробник кліку для FindBy->Teacher
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label1.Content = "Day_of_Birth";
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            FindBy_Teacher.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_teacher");
            ComboBox.Items.Add("PIB");
            ComboBox.Items.Add("Day_of_Birth");
            Select("Teacher", ListBox_Teacher);
            DataGrid_Change_Size("FindBy");
        }

        private void Customer_FindBy_Click(object sender, RoutedEventArgs e)//Обробник кліку для FindBy->Customer
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label1.Content = "Day_of_Birth";
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            FindBy_Customer.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_customer");
            ComboBox.Items.Add("PIB");
            ComboBox.Items.Add("Day_of_Birth");
            ComboBox.Items.Add("id_payment1");
            Select("Customer", ListBox_Customer);
            DataGrid_Change_Size("FindBy");
        }

        private void Studing_FindBy_Click(object sender, RoutedEventArgs e)//Обробник кліку для FindBy->Studing
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            FindBy_Studing.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_studing");
            ComboBox.Items.Add("id_teacher1");
            ComboBox.Items.Add("id_customer1");
            ComboBox.Items.Add("id_group1");
            ComboBox.Items.Add("id_level1");
            Select("Studing", ListBox_Studing);
            DataGrid_Change_Size("FindBy");
        }

        private void Schedule_FindBy_Click(object sender, RoutedEventArgs e)//Обробник кліку для FindBy->Schedule
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            FindBy_Schedule.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_schedule");
            ComboBox.Items.Add("Start_Time");
            ComboBox.Items.Add("End_Time");
            ComboBox.Items.Add("Day");
            ComboBox.Items.Add("id_group1");
            Select("Schedule", ListBox_Schedule);
            DataGrid_Change_Size("FindBy");
        }

        private void Group_FindBy_Click(object sender, RoutedEventArgs e)//Обробник кліку для FindBy->Group
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            FindBy_Group.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_group");
            ComboBox.Items.Add("Num_of_students");
            ComboBox.Items.Add("Duration_of_lesson");
            ComboBox.Items.Add("Audience");
            Select("Group", ListBox_Group);
            DataGrid_Change_Size("FindBy");
        }

        private void Exam_FindBy_Click(object sender, RoutedEventArgs e)//Обробник кліку для FindBy->Exam
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label1.Content = "Day_of_Birth";
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            FindBy_Exam.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_exam");
            ComboBox.Items.Add("Day");
            ComboBox.Items.Add("Grade");
            ComboBox.Items.Add("Start");
            ComboBox.Items.Add("id_level1");
            Select("Exam", ListBox_Exam);
            DataGrid_Change_Size("FindBy");
        }

        private void Level_FindBy_Click(object sender, RoutedEventArgs e)//Обробник кліку для FindBy->Level
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            FindBy_Level.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_level");
            ComboBox.Items.Add("Name_of_level");
            ComboBox.Items.Add("Time_of_studing");
            ComboBox.Items.Add("Price");
            ComboBox.Items.Add("id_language1");
            Select("Level", ListBox_Level);
            DataGrid_Change_Size("FindBy");
        }

        private void Language_FindBy_Click(object sender, RoutedEventArgs e)//Обробник кліку для FindBy->Language
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            FindBy_Language.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_language");
            ComboBox.Items.Add("Name_of_language");
            Select("Language", ListBox_Language);
            DataGrid_Change_Size("FindBy");
        }

        private void Payment_FindBy_Click(object sender, RoutedEventArgs e)//Обробник кліку для FindBy->Payment
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label1.Content = "Day_of_Birth";
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            FindBy_Payment.Visibility = Visibility.Visible;
            TextBox1.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_payment");
            ComboBox.Items.Add("Date");
            ComboBox.Items.Add("Paid");
            Select("Payment", ListBox_Payment);
            DataGrid_Change_Size("FindBy");
        }

        /*                               //////////////////////////                   FINDBY BUTTONS         ////////////////////////////                            */

        private void FindBy_Teacher_Click(object sender, RoutedEventArgs e)//Обробник кнопки FindBy_Teacher
        {
            try
            {
                string str;
                if (ComboBox.Text == "PIB")
                {
                    str = "'" + TextBox1.Text + "'";
                }
                else if (ComboBox.Text == "Day_of_Birth")
                {
                    str = "'" + MyCalendar.SelectedDate.Value.ToString("dd.MM.yyyy") + "'";
                }
                else
                {
                    str = TextBox1.Text;
                }
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Teacher] where [" + ComboBox.Text + "]=" + str + ";", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Teacher");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    MessageBox.Show("There is no teacher with the given parameters! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("Select * from [English courses].[dbo].[Teacher] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Teacher");
                    sda2.Fill(dt2);
                    DataGridView.ItemsSource = dt2.DefaultView;
                    MessageBox.Show("You can see the result on the right side of the window in DataGrid!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindBy_Customer_Click(object sender, RoutedEventArgs e)//Обробник кнопки FindBy_Customer
        {
            try
            {
                string str;
                if (ComboBox.Text == "PIB")
                {
                    str = "'" + TextBox1.Text + "'";
                }
                else if (ComboBox.Text == "Day_of_Birth")
                {
                    str = "'" + MyCalendar.SelectedDate.Value.ToString("dd.MM.yyyy") + "'";
                }
                else
                {
                    str = TextBox1.Text;
                }
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Customer] where [" + ComboBox.Text + "]=" + str + ";", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Customer");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    MessageBox.Show("There is no customer with the given parameters! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("Select * from [English courses].[dbo].[Customer] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Customer");
                    sda2.Fill(dt2);
                    DataGridView.ItemsSource = dt2.DefaultView;
                    MessageBox.Show("You can see the result on the right side of the window in DataGrid!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindBy_Studing_Click(object sender, RoutedEventArgs e)//Обробник кнопки FindBy_Studing
        {
            try
            {
                string str = TextBox1.Text;
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Studing] where [" + ComboBox.Text + "]=" + str + ";", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Customer");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    MessageBox.Show("There is no studing with the given parameters! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("Select * from [English courses].[dbo].[Studing] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Studing");
                    sda2.Fill(dt2);
                    DataGridView.ItemsSource = dt2.DefaultView;
                    MessageBox.Show("You can see the result on the right side of the window in DataGrid!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindBy_Schedule_Click(object sender, RoutedEventArgs e)//Обробник кнопки FindBy_Schedule
        {
            try
            {
                string str;
                if (ComboBox.Text == "id_schedule" || ComboBox.Text == "id_group1")
                {
                    str = TextBox1.Text;
                }
                else
                {
                    str = "'" + TextBox1.Text + "'";
                }
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Schedule] where [" + ComboBox.Text + "]=" + str + ";", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Schedule");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    MessageBox.Show("There is no schedule with the given parameters! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("Select * from [English courses].[dbo].[Schedule] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Schedule");
                    sda2.Fill(dt2);
                    DataGridView.ItemsSource = dt2.DefaultView;
                    MessageBox.Show("You can see the result on the right side of the window in DataGrid!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindBy_Group_Click(object sender, RoutedEventArgs e)//Обробник кнопки FindBy_Group
        {
            try
            {
                string str = TextBox1.Text;
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Group] where [" + ComboBox.Text + "]=" + str + ";", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Group");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    MessageBox.Show("There is no group with the given parameters! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("Select * from [English courses].[dbo].[Group] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Group");
                    sda2.Fill(dt2);
                    DataGridView.ItemsSource = dt2.DefaultView;
                    MessageBox.Show("You can see the result on the right side of the window in DataGrid!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindBy_Exam_Click(object sender, RoutedEventArgs e)//Обробник кнопки FindBy_Exam
        {
            try
            {
                string str;
                if (ComboBox.Text == "Day")
                {
                    str = "'" + MyCalendar.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                }
                else if (ComboBox.Text == "Start")
                {
                    str = "'" + TextBox1.Text + "'";
                }
                else
                {
                    str = TextBox1.Text;
                }
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Exam] where [" + ComboBox.Text + "]=" + str + ";", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Exam");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    MessageBox.Show("There is no exam with the given parameters! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("Select * from [English courses].[dbo].[Exam] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Exam");
                    sda2.Fill(dt2);
                    DataGridView.ItemsSource = dt2.DefaultView;
                    MessageBox.Show("You can see the result on the right side of the window in DataGrid!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindBy_Level_Click(object sender, RoutedEventArgs e)//Обробник кнопки FindBy_Level
        {
            try
            {
                string str;
                if (ComboBox.Text == "Name_of_level")
                {
                    str = "'" + TextBox1.Text + "'";
                }
                else
                {
                    str = TextBox1.Text;
                }
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Level] where [" + ComboBox.Text + "]=" + str + ";", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Level");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    MessageBox.Show("There is no level with the given parameters! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("Select * from [English courses].[dbo].[Level] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Level");
                    sda2.Fill(dt2);
                    DataGridView.ItemsSource = dt2.DefaultView;
                    MessageBox.Show("You can see the result on the right side of the window in DataGrid!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindBy_Language_Click(object sender, RoutedEventArgs e)//Обробник кнопки FindBy_Language
        {
            try
            {
                string str;
                if (ComboBox.Text == "Name_of_language")
                {
                    str = "'" + TextBox1.Text + "'";
                }
                else
                {
                    str = TextBox1.Text;
                }
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Language] where [" + ComboBox.Text + "]=" + str + ";", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Language");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    MessageBox.Show("There is no language with the given parameters! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("Select * from [English courses].[dbo].[Language] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Language");
                    sda2.Fill(dt2);
                    DataGridView.ItemsSource = dt2.DefaultView;
                    MessageBox.Show("You can see the result on the right side of the window in DataGrid!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindBy_Payment_Click(object sender, RoutedEventArgs e)//Обробник кнопки FindBy_Paymet
        {
            try
            {
                string str;
                if (ComboBox.Text == "Date")
                {
                    str = "'" + MyCalendar.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                }
                else
                {
                    str = TextBox1.Text;
                }
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Payment] where [" + ComboBox.Text + "]=" + str + ";", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Payment");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    MessageBox.Show("There is no payment with the given parameters! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("Select * from [English courses].[dbo].[Payment] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Payment");
                    sda2.Fill(dt2);
                    DataGridView.ItemsSource = dt2.DefaultView;
                    MessageBox.Show("You can see the result on the right side of the window in DataGrid!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
