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
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Kursova
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        List<ListBox> ListOfListBox = new List<ListBox>();
        List<TextBox> ListOfTextBox = new List<TextBox>();
        List<Button> ListOfButton = new List<Button>();
        List<Label> ListOfLabel = new List<Label>();
        List<ComboBox> ListOfComboBox = new List<ComboBox>();

        static string PatternPIB = @"^([\w]+)\s([\w]+)\s([\w]+)$";
        static string PatternTime = @"^[1][0-9]:[0-5][0-9]$";
        static string PatternTimeExam = @"^[8-9]:[0-5][0-9]$";
        static string PatternLevel = @"^[A-C][1-2]$";
        static string PatternLanguage = @"^[a-zA-Z]+$";

        static string SelectedTable = "";

        static string path = @"Data Source =.\SQLEXPRESS;Initial Catalog = English Courses; Integrated Security = True";
        SqlConnection con = new SqlConnection(path);

        public AdminWindow()
        {
            InitializeComponent();
            ListOfListBox.AddRange(new ListBox[] { ListBox_Teacher, ListBox_Customer, ListBox_Studing, ListBox_Schedule, ListBox_Group, ListBox_Exam, ListBox_Level, ListBox_Language, ListBox_Payment });
            ListOfTextBox.AddRange(new TextBox[] { TextBox1, TextBox2, TextBox3, TextBox4, TextBox5,TextBox6 });
            ListOfButton.AddRange(new Button[] { Insert_Teacher, Insert_Customer, Insert_Studing, Insert_Schedule, Insert_Group, Insert_Exam, Insert_Level, Insert_Language, Insert_Payment });
            ListOfButton.AddRange(new Button[] { Delete_Teacher, Delete_Customer, Delete_Studing, Delete_Schedule, Delete_Group, Delete_Exam, Delete_Level, Delete_Language, Delete_Payment });
            ListOfButton.AddRange(new Button[] { Update_Teacher, Update_Customer, Update_Studing, Update_Schedule, Update_Group, Update_Exam, Update_Level, Update_Language, Update_Payment });
            ListOfButton.AddRange(new Button[] { FindBy_Teacher, FindBy_Customer, FindBy_Studing, FindBy_Schedule, FindBy_Group, FindBy_Exam, FindBy_Level, FindBy_Language, FindBy_Payment });
            ListOfLabel.AddRange(new Label[] { Label1, Label2, Label3, Label4, Label5,Label6,Label7,Label8 });
           // ListOfComboBox.AddRange(new ComboBox[] { ComboBox_teacher });
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

        private void Inst_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.instagram.com/serhiy_bodnarchuk/");
        }


        /*                               //////////////////////////                    SELECT MENUITEM           ////////////////////////////                            */

        private void Teacher_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Teacher 
        {
            Hide_Elements();
            ListBox_Teacher.Visibility = Visibility.Visible;
            Select("Teacher", ListBox_Teacher);
            DataGrid_Change_Size("Select");
        }

        private void Customer_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Customer
        {
            Hide_Elements();
            ListBox_Customer.Visibility = Visibility.Visible;
            Select("Customer", ListBox_Customer);
            DataGrid_Change_Size("Select");
        }

        private void Studing_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Studing
        {
            Hide_Elements();
            ListBox_Studing.Visibility = Visibility.Visible;
            Select("Studing", ListBox_Studing);
            DataGrid_Change_Size("Select");
        }

        private void Schedule_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Schedule
        {
            Hide_Elements();
            ListBox_Schedule.Visibility = Visibility.Visible;
            Select("Schedule", ListBox_Schedule);
            DataGrid_Change_Size("Select");
        }

        private void Group_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Group
        {
            Hide_Elements();
            ListBox_Group.Visibility = Visibility.Visible;
            Select("Group", ListBox_Group);
            DataGrid_Change_Size("Select");
        }

        private void Exam_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Exam
        {
            Hide_Elements();
            ListBox_Exam.Visibility = Visibility.Visible;
            Select("Exam", ListBox_Exam);
            DataGrid_Change_Size("Select");
        }

        private void Level_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Level
        {
            Hide_Elements();
            ListBox_Level.Visibility = Visibility.Visible;
            Select("Level", ListBox_Level);
            DataGrid_Change_Size("Select");
        }

        private void Language_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Language
        {
            Hide_Elements();
            ListBox_Language.Visibility = Visibility.Visible;
            Select("Language", ListBox_Language);
            DataGrid_Change_Size("Select");
        }

        private void Payment_Select_Click(object sender, RoutedEventArgs e)//Обробник кліку для MenuItem Payment
        {
            Hide_Elements();
            ListBox_Payment.Visibility = Visibility.Visible;
            Select("Payment", ListBox_Payment);
            DataGrid_Change_Size("Select");
        }

        /*                               //////////////////////////                   ФУНКЦІЇ           ////////////////////////////                            */

        private void Hide_Elements()//Функція , щоб ховати всі елементи з списку
        {
            SelectedTable = "";

            foreach (ListBox l in ListOfListBox)
            {
                l.Visibility = Visibility.Hidden;
            }

            foreach (TextBox t in ListOfTextBox)
            {
                t.Visibility = Visibility.Hidden;
                t.IsReadOnly = false;
                t.Clear();
            }

            foreach (Button b in ListOfButton)
            {
                b.Visibility = Visibility.Hidden;
            }

            foreach(Label l in ListOfLabel)
            {
                l.Visibility = Visibility.Hidden;
                
            }

            MyRectangle1.Visibility = Visibility.Hidden;
            MyRectangle2.Visibility = Visibility.Hidden;
            DataGridView.Visibility = Visibility.Hidden;
            MyCalendar.Visibility = Visibility.Hidden;
            ComboBox.Visibility = Visibility.Hidden;            
        }

        private void Select(string Table, ListBox list)//Функція для заповнення datagrid
        {
            DataGridView.Visibility = Visibility.Visible;
            string str = "SELECT * ";
            foreach (CheckBox c in list.Items)
            {
                if (c.IsChecked == true)
                {
                    str = str.Replace('*', ' ');
                    str += "[" + c.Content + "] ,";
                }
            }
            str = str.Remove(str.Length - 1, 1);
            str += " FROM[English courses].[dbo].[" + Table + "]; ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(Table);
            sda.Fill(dt);
            DataGridView.ItemsSource = dt.DefaultView;

        }

        private void DataGrid_Change_Size(string command)
        {
            if (command == "Select")
            {
                DataGridView.Height = 312;
                DataGridView.Width = 325;
                DataGridView.Margin= new Thickness (437, 72, 0, 0);
            }
            else
            {
                DataGridView.Height = 312;
                DataGridView.Width = 269;
                DataGridView.Margin = new Thickness(496, 72, 0, 0);
            }
        }

        private void DataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataRowView dataRowView = dataGrid.SelectedItem as DataRowView;
            if (SelectedTable == "Teacher")
            {
                if (dataRowView != null)
                {
                    TextBox1.Text = dataRowView[0].ToString();
                    TextBox2.Text = dataRowView[1].ToString();
                    MyCalendar.DisplayDate = Convert.ToDateTime(dataRowView[2].ToString());
                    MyCalendar.SelectedDate = Convert.ToDateTime(dataRowView[2].ToString());
                }
            }
            else if (SelectedTable == "Customer")
            {
                if (dataRowView != null)
                {
                    TextBox1.Text = dataRowView[0].ToString();
                    TextBox2.Text = dataRowView[1].ToString();
                    MyCalendar.DisplayDate = Convert.ToDateTime(dataRowView[2].ToString());
                    MyCalendar.SelectedDate = Convert.ToDateTime(dataRowView[2].ToString());
                    TextBox3.Text = dataRowView[3].ToString();
                }
            }
            else if (SelectedTable == "Studing" || SelectedTable == "Schedule" || SelectedTable == "Level")
            {
                if (dataRowView != null)
                {
                    TextBox1.Text = dataRowView[0].ToString();
                    TextBox2.Text = dataRowView[1].ToString();
                    TextBox3.Text = dataRowView[2].ToString();
                    TextBox4.Text = dataRowView[3].ToString();
                    TextBox5.Text = dataRowView[4].ToString();
                }
            }
            else if (SelectedTable == "Group")
            {
                TextBox1.Text = dataRowView[0].ToString();
                TextBox2.Text = dataRowView[1].ToString();
                TextBox3.Text = dataRowView[2].ToString();
                TextBox4.Text = dataRowView[3].ToString();
            }
            else if (SelectedTable == "Exam")
            {
                TextBox1.Text = dataRowView[0].ToString();
                MyCalendar.DisplayDate = Convert.ToDateTime(dataRowView[1].ToString());
                MyCalendar.SelectedDate = Convert.ToDateTime(dataRowView[1].ToString());
                TextBox2.Text = dataRowView[2].ToString();
                TextBox3.Text = dataRowView[3].ToString();
                TextBox4.Text = dataRowView[4].ToString();
            }
            else if (SelectedTable == "Language")
            {
                TextBox1.Text = dataRowView[0].ToString();
                TextBox2.Text = dataRowView[1].ToString();
            }
            else if (SelectedTable == "Payment")
            {
                TextBox1.Text = dataRowView[0].ToString();
                MyCalendar.DisplayDate = Convert.ToDateTime(dataRowView[1].ToString());
                MyCalendar.SelectedDate = Convert.ToDateTime(dataRowView[1].ToString());
                TextBox2.Text = dataRowView[2].ToString();
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
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
            Payment_Select_Click(sender, e);
        }

        /*                               //////////////////////////                    INSERT MENUITEM         ////////////////////////////                            */

        private void Teacher_Insert_Click(object sender, RoutedEventArgs e)//Обробка кліку для Insert->Teacher
        {
            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            Insert_Teacher.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            Label1.Content = "id_teacher";
            Label2.Content = "PIB";
            Label6.Content = "Day of Birth";
            Select("Teacher", ListBox_Teacher);
            DataGrid_Change_Size("Insert");
        }

        private void Customer_Insert_Click(object sender, RoutedEventArgs e)//Обробка кліку для Insert->Customer
        {
            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            Insert_Customer.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            Label1.Content = "id_customer";
            Label2.Content = "PIB";
            Label3.Content = "id_payment1";
            Label6.Content = "Day of Birth";
            Select("Customer", ListBox_Customer);
            DataGrid_Change_Size("Insert");
        }

        private void Studing_Insert_Click(object sender, RoutedEventArgs e)//Обробка кліку для Insert->Studing
        {
            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;
            TextBox5.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Label5.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            Insert_Studing.Visibility = Visibility.Visible;
            Label1.Content = "id_studing";
            Label2.Content = "id_teacher1";
            Label3.Content = "id_customer1";
            Label4.Content = "id_group1";
            Label5.Content = "id_level1";
            Select("Studing", ListBox_Studing);
            DataGrid_Change_Size("Insert");
        }

        private void Schedule_Insert_Click(object sender, RoutedEventArgs e)//Обробка кліку для Insert->Schedule
        {
            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;
            TextBox5.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Label5.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            Insert_Schedule.Visibility = Visibility.Visible;
            Label1.Content = "id_schedule";
            Label2.Content = "Start time";
            Label3.Content = "End time";
            Label4.Content = "Day of the week";
            Label5.Content = "id_group1";
            Select("Schedule", ListBox_Schedule);
            DataGrid_Change_Size("Insert");
        }

        private void Group_Insert_Click(object sender, RoutedEventArgs e)//Обробка кліку для Insert->Group
        {
            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Insert_Group.Visibility = Visibility.Visible;
            Label1.Content = "id_group";
            Label2.Content = "Number of students";
            Label3.Content = "Duration of a lesson";
            Label4.Content = "Audience";
            Select("Group", ListBox_Group);
            DataGrid_Change_Size("Insert");
        }

        private void Exam_Insert_Click(object sender, RoutedEventArgs e)//Обробка кліку для Insert->Exam
        {
            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            Insert_Exam.Visibility = Visibility.Visible;
            Label1.Content = "id_exam";
            Label2.Content = "Grade";
            Label3.Content = "Start time";
            Label4.Content = "id_level1";
            Label6.Content = "Exam day";
            Select("Exam", ListBox_Exam);
            DataGrid_Change_Size("Insert");
        }

        private void Level_Insert_Click(object sender, RoutedEventArgs e)//Обробка кліку для Insert->Level
        {
            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;
            TextBox5.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Label5.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            Insert_Level.Visibility = Visibility.Visible;
            Label1.Content = "id_level";
            Label2.Content = "Name of level";
            Label3.Content = "Time of studing";
            Label4.Content = "Price";
            Label5.Content = "id_language1";
            Select("Level", ListBox_Level);
            DataGrid_Change_Size("Insert");
        }

        private void Language_Insert_Click(object sender, RoutedEventArgs e)//Обробка кліку для Insert->Language
        {
            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            Insert_Language.Visibility = Visibility.Visible;
            Label1.Content = "id_language";
            Label2.Content = "Name of language";
            Select("Language", ListBox_Language);
            DataGrid_Change_Size("Insert");
        }

        private void Payment_Insert_Click(object sender, RoutedEventArgs e)//Обробка кліку для Insert->Payment
        {
            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            Insert_Payment.Visibility = Visibility.Visible;
            Label1.Content = "id_payment";
            Label2.Content = "Paid";
            Label6.Content = "Date";
            Select("Payment", ListBox_Payment);
            DataGrid_Change_Size("Insert");
        }

        /*                               //////////////////////////                   INSERT BUTTONS        ////////////////////////////                            */

        private void Insert_Teacher_Click(object sender, RoutedEventArgs e)//Обробник кнопки Insert_Teacher
        {
            Regex PIB = new Regex(PatternPIB);
            try
            {
                string str = MyCalendar.SelectedDate.Value.ToString("dd.MM.yyyy");
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Teacher] where PIB=" + "'" + TextBox2.Text + "' and Day_of_Birth= '" + str + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Teacher");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (!PIB.IsMatch(TextBox2.Text))
                {
                    throw new Exception("Invalid PIB");
                }
                if ((DateTime.Today.Subtract(MyCalendar.SelectedDate.Value)).TotalDays<6574)
                {
                    throw new Exception("Teacher must be at least 18 years old");
                }
                if (count != 0)
                {
                    throw new Exception("Teacher with the same PIB and day of birth is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Insert_Teacher] " + TextBox1.Text + ", '" + TextBox2.Text + "', '" + str + "'", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Teacher");
                    sda2.Fill(dt2);
                    MessageBox.Show("Teacher with PIB: " + TextBox2.Text + " was added to the table!");
                    Teacher_Insert_Click(sender, e);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Customer_Click(object sender, RoutedEventArgs e)//Обробник кнопки Insert_Customer
        {
            Regex PIB = new Regex(PatternPIB);
            try
            {
                string str = MyCalendar.SelectedDate.Value.ToString("dd.MM.yyyy");
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Customer] where PIB=" + "'" + TextBox2.Text + "' and Day_of_Birth= '" + str + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Customer");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (!PIB.IsMatch(TextBox2.Text))
                {
                    throw new Exception("Invalid PIB");
                }
                if ((DateTime.Today.Subtract(MyCalendar.SelectedDate.Value)).TotalDays < 3652)
                {
                    throw new Exception("Customer must be at least 10 years old");
                }
                if (count != 0)
                {
                    throw new Exception("Customer with the same PIB and day of birth is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Insert_Customer] " + TextBox1.Text + ", '" + TextBox2.Text + "', '" + str + "'," + TextBox3.Text, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Customer");
                    sda2.Fill(dt2);
                    MessageBox.Show("Customer with PIB: " + TextBox2.Text + " was added to the table!");
                    Customer_Insert_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Insert_Studing_Click(object sender, RoutedEventArgs e)//Обробник кнопки Insert_Studing
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Studing] where id_teacher1=" + TextBox2.Text + " and id_customer1= " + TextBox3.Text + " and id_group1= " + TextBox4.Text + " and id_level1= " + TextBox5.Text, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Studing");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (count != 0)
                {
                    throw new Exception("Studing with the same parameters is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Insert_Studing] " + TextBox1.Text + ", " + TextBox2.Text + ", " + TextBox3.Text + ", " + TextBox4.Text + ", " + TextBox5.Text, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Studing");
                    sda2.Fill(dt2);
                    MessageBox.Show("Studing with id_studing: " + TextBox1.Text + " was added to the table!");
                    Studing_Insert_Click(sender, e);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Schedule_Click(object sender, RoutedEventArgs e)//Обробник кнопки Insert_Schedule
        {
            Regex r = new Regex(PatternTime);
            string[] StartTime = new string[2];
            string[] EndTime = new string[2];
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Schedule] where Start_Time='" + TextBox2.Text + "' and End_Time= '" + TextBox3.Text + "' and [Day]= '" + TextBox4.Text + "' and id_group1= " + TextBox5.Text, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Schedule");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (!r.IsMatch(TextBox2.Text) || !r.IsMatch(TextBox3.Text))
                {
                    throw new Exception("Lessons must begin and end from 10:00 to 19:59!");
                }
                StartTime = TextBox2.Text.Split(':');
                EndTime = TextBox3.Text.Split(':');
                int[] MasTime = new int[] { Convert.ToInt32(StartTime[0]), Convert.ToInt32(StartTime[1]), Convert.ToInt32(EndTime[0]), Convert.ToInt32(EndTime[1]) };
                if (MasTime[0]>MasTime[2] ||(MasTime[0]==MasTime[2] && MasTime[1]>=MasTime[3]))
                {
                    throw new Exception("The lesson can't end without ever having begun");
                }
                int minutes = (MasTime[2] - MasTime[0]) * 60;
                minutes += MasTime[3] - MasTime[1];
                if(minutes>80 || minutes < 40)
                {
                    throw new Exception("The lesson should go on at least 40 minutes and a maximum of 80 minutes");
                }
                if(TextBox4.Text!="Понеділок" && TextBox4.Text != "Вівторок" && TextBox4.Text != "Середа" && TextBox4.Text != "Четвер" && TextBox4.Text != "П'ятниця")
                {
                    throw new Exception("Learning takes place only on weekdays");
                }
                if (count != 0)
                {
                    throw new Exception("Schedule with the same parameters is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Insert_Schedule] " + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "'," + TextBox5.Text, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Schedule");
                    sda2.Fill(dt2);
                    MessageBox.Show("Schedule with start time " + TextBox2.Text + " and end time " + TextBox3.Text + " was added to the table!");
                    Schedule_Insert_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Group_Click(object sender, RoutedEventArgs e)//Обробник кнопки Insert_Group
        {
            try
            {
                int NumOfStudents = Convert.ToInt32(TextBox2.Text);
                int DurationOfLesson = Convert.ToInt32(TextBox3.Text);
                int AudienceFloor = Convert.ToInt32(TextBox4.Text.Substring(0, 1));
                int AudienceNum = Convert.ToInt32(TextBox4.Text.Substring(1, 2));
                if(NumOfStudents>20 || NumOfStudents < 5)
                {
                    throw new Exception("The number of pupils should be between 5 and 20!");
                }
                if(DurationOfLesson>80 || DurationOfLesson < 40)
                {
                    throw new Exception("The lesson should go on at least 40 minutes and a maximum of 80 minutes");
                }
                if (AudienceFloor>3 || AudienceFloor<1)
                {
                    throw new Exception("Lessons should be on 1, 2, 3 floors");
                }
                if(AudienceNum>20 || AudienceNum < 1)
                {
                    throw new Exception("The audience number should be between 1 and 20");
                }
                SqlCommand cmd = new SqlCommand("EXEC [English courses].[dbo].[Insert_Group] " + TextBox1.Text + ", " + TextBox2.Text + ", " + TextBox3.Text + ", " + TextBox4.Text, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Group");
                sda.Fill(dt);
                MessageBox.Show("Group with id_group: " + TextBox1.Text + " was added to the table!");
                Group_Insert_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Exam_Click(object sender, RoutedEventArgs e)//Обробник кнопки Insert_Exam
        {
            Regex ExamTime = new Regex(PatternTimeExam);
            try
            {

                string str = MyCalendar.SelectedDate.Value.ToString("yyyy-MM-dd");
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Exam] where [Day]='" + str + "' and Grade=" + TextBox2.Text + " and Start= '" + TextBox3.Text + "' and id_level1= " + TextBox4.Text, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Exam");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if ((MyCalendar.SelectedDate.Value.Subtract(DateTime.Today)).TotalDays < 7)
                {
                    throw new Exception("The exam must take place at least 7 days from today");
                }
                int grade = Convert.ToInt32(TextBox2.Text);
                if(grade > 5 || grade < 0)
                {
                    throw new Exception("The grade should be between 0 and 5");
                }
                if (!ExamTime.IsMatch(TextBox3.Text))
                {
                    throw new Exception("Exam must begin from 8:00 to 9:59!");
                }
                if (count != 0)
                {
                    throw new Exception("Exam with the same parameters is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Insert_Exam] " + TextBox1.Text + ", '" + str + "', " + TextBox2.Text + ", '" + TextBox3.Text + "', " + TextBox4.Text, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Exam");
                    sda2.Fill(dt2);
                    MessageBox.Show("Exam with date : " + str + " was added to the table\n");
                    Exam_Insert_Click(sender, e);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Level_Click(object sender, RoutedEventArgs e)//Обробник кнопки Insert_Level
        {
            Regex Level = new Regex(PatternLevel);
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Level] where Name_of_level='" + TextBox2.Text + "' and Time_of_studing=" + TextBox3.Text + " and Price= " + TextBox4.Text + " and id_language1= " + TextBox5.Text, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Level");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (!Level.IsMatch(TextBox2.Text))
                {
                    throw new Exception("Invalid name of level.\nYou can use only: A1, A2, B1, B2, C1, C2.");
                }
                int TimeOfStuding = Convert.ToInt32(TextBox3.Text);
                if(TimeOfStuding>12 || TimeOfStuding < 1)
                {
                    throw new Exception("Time of studing should be between 1 and 12(months).");
                }
                int price = Convert.ToInt32(TextBox4.Text);
                if (price>30000 || price<1000)
                {
                    throw new Exception("Price should be between 1000 and 30000.");
                }
                if (count != 0)
                {
                    throw new Exception("Level with the same parameters is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Insert_Level] " + TextBox1.Text + ", " + TextBox2.Text + ", " + TextBox3.Text + ", " + TextBox4.Text + ", " + TextBox5.Text, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Level");
                    sda2.Fill(dt2);
                    MessageBox.Show("Level with name " + TextBox2.Text + " and price : " + TextBox4.Text + " was added to the table!)");
                    Level_Insert_Click(sender, e);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Language_Click(object sender, RoutedEventArgs e)//Обробник кнопки Insert_Language
        {
            Regex Language = new Regex(PatternLanguage);
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Language] where Name_of_language='" + TextBox2.Text+"'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Language");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (!Language.IsMatch(TextBox2.Text))
                {
                    throw new Exception("Invalid language!");
                }
                if (count != 0)
                {
                    throw new Exception("Language with the same name is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Insert_Language] " + TextBox1.Text + ", '" + TextBox2.Text + "'", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Language");
                    sda2.Fill(dt2);
                    MessageBox.Show("Language : " + TextBox2.Text + " was added to the table!");
                    Language_Insert_Click(sender, e);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Payment_Click(object sender, RoutedEventArgs e)//Обробник кнопки Insert_Payment 
        {
            try 
            {
                string str = MyCalendar.SelectedDate.Value.ToString("yyyy-MM-dd");
                if ((MyCalendar.SelectedDate.Value.Subtract(DateTime.Today)).TotalDays < 0)
                {
                    throw new Exception("Invalid date.You must select today's date or future date.");
                }
                int paid = Convert.ToInt32(TextBox2.Text);
                if (paid > 30000 || paid < 0)
                {
                    throw new Exception("Pupil pay amount between 0 and 30000.");
                }
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Insert_Payment] " + TextBox1.Text + ", '" + str + "'," + TextBox2.Text, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Payment");
                    sda2.Fill(dt2);
                    MessageBox.Show("Payment with paid : " + TextBox2.Text + " was added to the table!");
                    Payment_Insert_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*                               //////////////////////////                   DELETE BUTTONS        ////////////////////////////                            */

        private void Delete_Teacher_Click(object sender, RoutedEventArgs e)//Обробник кнопки Delete_Teacher
        {
            try
            {
                string str;
                if (ComboBox.Text == "PIB" )
                {
                    str = "'" + TextBox6.Text + "'";
                }
                else if(ComboBox.Text == "Day_of_Birth")
                {
                    str = "'"+MyCalendar.SelectedDate.Value.ToString("dd.MM.yyyy")+"'";
                }
                else
                {
                    str = TextBox6.Text;
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
                    SqlCommand cmd2 = new SqlCommand("Delete from [English courses].[dbo].[Teacher] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Teacher");
                    sda2.Fill(dt2);
                    MessageBox.Show("Teacher with " + ComboBox.Text + " : " + str + " was removed from the table!");
                    Teacher_Delete_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Customer_Click(object sender, RoutedEventArgs e)//Обробник кнопки Delete_Customer
        {
            try
            {
                string str;
                if (ComboBox.Text == "PIB")
                {
                    str = "'" + TextBox6.Text + "'";
                }
                else if (ComboBox.Text == "Day_of_Birth")
                {
                    str = "'" + MyCalendar.SelectedDate.Value.ToString("dd.MM.yyyy") + "'";
                }
                else
                {
                    str = TextBox6.Text;
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
                    SqlCommand cmd2 = new SqlCommand("Delete from [English courses].[dbo].[Customer] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Customer");
                    sda2.Fill(dt2);
                    MessageBox.Show("Customer with " + ComboBox.Text + " : " + str + " was removed from the table!");
                    Customer_Delete_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Studing_Click(object sender, RoutedEventArgs e)//Обробник кнопки Delete_Studing
        {
            try
            {
                string str= TextBox6.Text;
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
                    SqlCommand cmd2 = new SqlCommand("Delete from [English courses].[dbo].[Studing] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Studing");
                    sda2.Fill(dt2);
                    MessageBox.Show("Studing with " + ComboBox.Text + " : " + str + " was removed from the table!\n(Studing deleted if there is studing with the same " + ComboBox.Text + " in the table)");
                    Studing_Delete_Click(sender, e);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Schedule_Click(object sender, RoutedEventArgs e)//Обробник кнопки Delete_Schedule
        {
            try
            {
                string str;
                if (ComboBox.Text == "id_schedule" || ComboBox.Text == "id_group1")
                {
                    str =TextBox6.Text;
                }
                else
                {
                    str = "'" + TextBox6.Text + "'";
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
                    SqlCommand cmd2 = new SqlCommand("Delete from [English courses].[dbo].[Schedule] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Schedule");
                    sda2.Fill(dt2);
                    MessageBox.Show("Schedule with " + ComboBox.Text + " : " + str + " was removed from the table!");
                    Schedule_Delete_Click(sender, e);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Group_Click(object sender, RoutedEventArgs e)//Обробник кнопки Delete_Group
        {
            try
            {
                string str = TextBox6.Text;
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
                    SqlCommand cmd2 = new SqlCommand("Delete from [English courses].[dbo].[Group] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Group");
                    sda2.Fill(dt2);
                    MessageBox.Show("Group with " + ComboBox.Text + " : " + str + " was removed from the table!");
                    Group_Delete_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Exam_Click(object sender, RoutedEventArgs e)//Обробник кнопки Delete_Exam
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
                    str = "'" + TextBox6.Text + "'";
                }
                else
                {
                    str = TextBox6.Text;
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
                    SqlCommand cmd2 = new SqlCommand("Delete from [English courses].[dbo].[Exam] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Exam");
                    sda2.Fill(dt2);
                    MessageBox.Show("Exam with " + ComboBox.Text + " : " + str + " was removed from the table!");
                    Exam_Delete_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Level_Click(object sender, RoutedEventArgs e)//Обробник кнопки Delete_Level
        {
            try
            {
                string str;
                if (ComboBox.Text == "Name_of_level")
                {
                    str = "'" + TextBox6.Text + "'";
                }
                else
                {
                    str = TextBox6.Text;
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
                    SqlCommand cmd2 = new SqlCommand("Delete from [English courses].[dbo].[Level] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Level");
                    sda2.Fill(dt2);
                    MessageBox.Show("Level with " + ComboBox.Text + " : " + str + " was removed from the table!");
                    Level_Delete_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Language_Click(object sender, RoutedEventArgs e)//Обробник кнопки Delete_Language
        {
            try
            {
                string str;
                if (ComboBox.Text == "Name_of_language")
                {
                    str = "'" + TextBox6.Text + "'";
                }
                else
                {
                    str = TextBox6.Text;
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
                    SqlCommand cmd2 = new SqlCommand("Delete from [English courses].[dbo].[Language] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Language");
                    sda2.Fill(dt2);
                    MessageBox.Show("Language with " + ComboBox.Text + " : " + str + " was removed from the table!");
                    Language_Delete_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Payment_Click(object sender, RoutedEventArgs e)//Обробник кнопки Delete_Paymet
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
                    str = TextBox6.Text;
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
                    SqlCommand cmd2 = new SqlCommand("Delete from [English courses].[dbo].[Payment] where [" + ComboBox.Text + "]=" + str + ";", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Payment");
                    sda2.Fill(dt2);
                    MessageBox.Show("Payment with " + ComboBox.Text + " : " + str + " was removed from the table!");
                    Payment_Delete_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*                               //////////////////////////                   DELETE MENUITEM         ////////////////////////////                            */

        private void Teacher_Delete_Click(object sender, RoutedEventArgs e)//Обробник кліку для Delete->Teacher
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            Label6.Content = "Day_of_Birth";
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            Delete_Teacher.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_teacher");
            ComboBox.Items.Add("PIB");
            ComboBox.Items.Add("Day_of_Birth");
            Select("Teacher", ListBox_Teacher);
            DataGrid_Change_Size("Delete");
        }

        private void Customer_Delete_Click(object sender, RoutedEventArgs e)//Обробник кліку для Delete->Customer
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            Label6.Content = "Day_of_Birth";
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            Delete_Customer.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
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
            DataGrid_Change_Size("Delete");
        }

        private void Studing_Delete_Click(object sender, RoutedEventArgs e)//Обробник кліку для Delete->Studing
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            Delete_Studing.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_studing");
            ComboBox.Items.Add("id_teacher1");
            ComboBox.Items.Add("id_customer1");
            ComboBox.Items.Add("id_group1");
            ComboBox.Items.Add("id_level1");
            Select("Studing", ListBox_Studing);
            DataGrid_Change_Size("Delete");
        }

        private void Schedule_Delete_Click(object sender, RoutedEventArgs e)//Обробник кліку для Delete->Schedule
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            Delete_Schedule.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_schedule");
            ComboBox.Items.Add("Start_Time");
            ComboBox.Items.Add("End_Time");
            ComboBox.Items.Add("Day");
            ComboBox.Items.Add("id_group1");
            Select("Schedule", ListBox_Schedule);
            DataGrid_Change_Size("Delete");
        }

        private void Group_Delete_Click(object sender, RoutedEventArgs e)//Обробник кліку для Delete->Group
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            Delete_Group.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_group");
            ComboBox.Items.Add("Num_of_students");
            ComboBox.Items.Add("Duration_of_lesson");
            ComboBox.Items.Add("Audience");
            Select("Group", ListBox_Group);
            DataGrid_Change_Size("Delete");
        }

        private void Exam_Delete_Click(object sender, RoutedEventArgs e)//Обробник кліку для Delete->Exam
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            Label6.Content = "Day_of_Birth";
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            Delete_Exam.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
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
            DataGrid_Change_Size("Delete");
        }

        private void Level_Delete_Click(object sender, RoutedEventArgs e)//Обробник кліку для Delete->Level
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            Delete_Level.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_level");
            ComboBox.Items.Add("Name_of_level");
            ComboBox.Items.Add("Time_of_studing");
            ComboBox.Items.Add("Price");
            ComboBox.Items.Add("id_language1");
            Select("Level", ListBox_Level);
            DataGrid_Change_Size("Delete");
        }

        private void Language_Delete_Click(object sender, RoutedEventArgs e)//Обробник кліку для Delete->Language
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            Delete_Language.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_language");
            ComboBox.Items.Add("Name_of_language");
            Select("Language", ListBox_Language);
            DataGrid_Change_Size("Delete");
        }

        private void Payment_Delete_Click(object sender, RoutedEventArgs e)//Обробник кліку для Delete->Payment
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            Label6.Content = "Day_of_Birth";
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            Delete_Payment.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
            ComboBox.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            ComboBox.Items.Clear();
            ComboBox.Items.Add("id_payment");
            ComboBox.Items.Add("Date");
            ComboBox.Items.Add("Paid");
            Select("Payment", ListBox_Payment);
            DataGrid_Change_Size("Delete");
        }

        /*                               //////////////////////////                   UPDATE MENUITEM         ////////////////////////////                            */

        private void Teacher_Update_Click(object sender, RoutedEventArgs e)//Обробник кліку для Update->Teacher
        {
            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox1.IsReadOnly=true;
            MyCalendar.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            Update_Teacher.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            Label1.Content = "id_teacher";
            Label2.Content = "PIB";
            Label6.Content = "Day of Birth";
            Select("Teacher", ListBox_Teacher);
            DataGrid_Change_Size("Update");
            SelectedTable = "Teacher";
        }

        private void Customer_Update_Click(object sender, RoutedEventArgs e)//Обробник кліку для Update->Customer
        {

            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox1.IsReadOnly = true;
            MyCalendar.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            Update_Customer.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            Label1.Content = "id_customer";
            Label2.Content = "PIB";
            Label3.Content = "id_payment1";
            Label6.Content = "Day of Birth";
            Select("Customer", ListBox_Customer);
            DataGrid_Change_Size("Update");
            SelectedTable = "Customer";
        }

        private void Studing_Update_Click(object sender, RoutedEventArgs e)//Обробник кліку для Update->Studing
        {

            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;
            TextBox5.Visibility = Visibility.Visible;
            TextBox1.IsReadOnly = true;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Label5.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            Update_Studing.Visibility = Visibility.Visible;
            Label1.Content = "id_studing";
            Label2.Content = "id_teacher1";
            Label3.Content = "id_customer1";
            Label4.Content = "id_group1";
            Label5.Content = "id_level1";
            Select("Studing", ListBox_Studing);
            DataGrid_Change_Size("Update");
            SelectedTable = "Studing";
        }

        private void Schedule_Update_Click(object sender, RoutedEventArgs e)//Обробник кліку для Update->Schedule
        {

            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;
            TextBox5.Visibility = Visibility.Visible;
            TextBox1.IsReadOnly = true;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Label5.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            Update_Schedule.Visibility = Visibility.Visible;
            Label1.Content = "id_schedule";
            Label2.Content = "Start time";
            Label3.Content = "End time";
            Label4.Content = "Day of the week";
            Label5.Content = "id_group1";
            Select("Schedule", ListBox_Schedule);
            DataGrid_Change_Size("Update");
            SelectedTable = "Schedule";
        }

        private void Group_Update_Click(object sender, RoutedEventArgs e)//Обробник кліку для Update->Group
        {

            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;
            TextBox1.IsReadOnly = true;
            MyRectangle1.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Update_Group.Visibility = Visibility.Visible;
            Label1.Content = "id_group";
            Label2.Content = "Number of students";
            Label3.Content = "Duration of a lesson";
            Label4.Content = "Audience";
            Select("Group", ListBox_Group);
            DataGrid_Change_Size("Update");
            SelectedTable = "Group";
        }

        private void Exam_Update_Click(object sender, RoutedEventArgs e)//Обробник кліку для Update->Exam
        {

            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;
            TextBox1.IsReadOnly = true;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            Update_Exam.Visibility = Visibility.Visible;
            Label1.Content = "id_exam";
            Label2.Content = "Grade";
            Label3.Content = "Start time";
            Label4.Content = "id_level1";
            Label6.Content = "Exam day";
            Select("Exam", ListBox_Exam);
            DataGrid_Change_Size("Update");
            SelectedTable = "Exam";
        }

        private void Level_Update_Click(object sender, RoutedEventArgs e)//Обробник кліку для Update->Level
        {

            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox3.Visibility = Visibility.Visible;
            TextBox4.Visibility = Visibility.Visible;
            TextBox5.Visibility = Visibility.Visible;
            TextBox1.IsReadOnly = true;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label3.Visibility = Visibility.Visible;
            Label4.Visibility = Visibility.Visible;
            Label5.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            Update_Level.Visibility = Visibility.Visible;
            Label1.Content = "id_level";
            Label2.Content = "Name of level";
            Label3.Content = "Time of studing";
            Label4.Content = "Price";
            Label5.Content = "id_language1";
            Select("Level", ListBox_Level);
            DataGrid_Change_Size("Update");
            SelectedTable = "Level";
        }

        private void Language_Update_Click(object sender, RoutedEventArgs e)//Обробник кліку для Update->Language
        {

            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox1.IsReadOnly = true;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            Update_Language.Visibility = Visibility.Visible;
            Label1.Content = "id_language";
            Label2.Content = "Name of language";
            Select("Language", ListBox_Language);
            DataGrid_Change_Size("Update");
            SelectedTable = "Language";
        }

        private void Payment_Update_Click(object sender, RoutedEventArgs e)//Обробник кліку для Update->Payment
        {

            Hide_Elements();
            TextBox1.Visibility = Visibility.Visible;
            TextBox2.Visibility = Visibility.Visible;
            TextBox1.IsReadOnly = true;
            Label1.Visibility = Visibility.Visible;
            Label2.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            MyRectangle1.Visibility = Visibility.Visible;
            MyRectangle2.Visibility = Visibility.Visible;
            MyCalendar.Visibility = Visibility.Visible;
            Update_Payment.Visibility = Visibility.Visible;
            Label1.Content = "id_payment";
            Label2.Content = "Paid";
            Label6.Content = "Date";
            Select("Payment", ListBox_Payment);
            DataGrid_Change_Size("Update");
            SelectedTable = "Payment";
        }

        /*                               //////////////////////////                   UPDATE BUTTONS         ////////////////////////////                            */

        private void Update_Teacher_Click(object sender, RoutedEventArgs e)//Обробник кнопки Update_Teacher
        {
            Regex PIB = new Regex(PatternPIB);
            try
            {
                string str = MyCalendar.SelectedDate.Value.ToString("dd.MM.yyyy");
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Teacher] where PIB=" + "'" + TextBox2.Text + "' and Day_of_Birth= '" + str + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Teacher");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (!PIB.IsMatch(TextBox2.Text))
                {
                    throw new Exception("Invalid PIB");
                }
                if ((DateTime.Today.Subtract(MyCalendar.SelectedDate.Value)).TotalDays < 6574)
                {
                    throw new Exception("Teacher must be at least 18 years old");
                }
                if (count != 0)
                {
                    throw new Exception("Teacher with the same PIB and day of birth is currently exist! (Input new values)");
                }
                else {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Update_Teacher] " + TextBox1.Text + ", '" + TextBox2.Text + "', '" + str + "'", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Teacher");
                    sda2.Fill(dt2);
                    MessageBox.Show("Teacher with id_teacher: " + TextBox1.Text + " was updated!");
                    Teacher_Update_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Customer_Click(object sender, RoutedEventArgs e)//Обробник кнопки Update_Customer
        {
            Regex PIB = new Regex(PatternPIB);
            try
            {
                string str = MyCalendar.SelectedDate.Value.ToString("dd.MM.yyyy");
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Customer] where PIB=" + "'" + TextBox2.Text + "' and Day_of_Birth= '" + str + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Customer");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (!PIB.IsMatch(TextBox2.Text))
                {
                    throw new Exception("Invalid PIB");
                }
                if ((DateTime.Today.Subtract(MyCalendar.SelectedDate.Value)).TotalDays < 3652)
                {
                    throw new Exception("Customer must be at least 10 years old");
                }
                if (count != 0)
                {
                    throw new Exception("Customer with the same PIB and day of birth is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Update_Customer] " + TextBox1.Text + ", '" + TextBox2.Text + "', '" + str + "'," + TextBox3.Text, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Customer");
                    sda2.Fill(dt);
                    MessageBox.Show("Customer with id_customer: " + TextBox1.Text + " was updated!");
                    Customer_Update_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Studing_Click(object sender, RoutedEventArgs e)//Обробник кнопки Update_Studing
        {
            try
            {

                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Studing] where id_teacher1=" + TextBox2.Text + " and id_customer1= " + TextBox3.Text + " and id_group1= " + TextBox4.Text + " and id_level1= " + TextBox5.Text, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Studing");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (count != 0)
                {
                    throw new Exception("Studing with the same parameters is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Update_Studing] " + TextBox1.Text + ", " + TextBox2.Text + ", " + TextBox3.Text + ", " + TextBox4.Text + ", " + TextBox5.Text, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Studing");
                    sda2.Fill(dt2);
                    MessageBox.Show("Studing with id_studing: " + TextBox1.Text + " was updated !");
                    Studing_Update_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Schedule_Click(object sender, RoutedEventArgs e)//Обробник кнопки Update_Schedule
        {
            Regex r = new Regex(PatternTime);
            string[] StartTime = new string[2];
            string[] EndTime = new string[2];
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Schedule] where Start_Time='" + TextBox2.Text + "' and End_Time= '" + TextBox3.Text + "' and [Day]= '" + TextBox4.Text + "' and id_group1= " + TextBox5.Text, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Schedule");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (!r.IsMatch(TextBox2.Text) || !r.IsMatch(TextBox3.Text))
                {
                    throw new Exception("Lessons must begin and end from 10:00 to 19:59!");
                }
                StartTime = TextBox2.Text.Split(':');
                EndTime = TextBox3.Text.Split(':');
                int[] MasTime = new int[] { Convert.ToInt32(StartTime[0]), Convert.ToInt32(StartTime[1]), Convert.ToInt32(EndTime[0]), Convert.ToInt32(EndTime[1]) };
                if (MasTime[0] > MasTime[2] || (MasTime[0] == MasTime[2] && MasTime[1] >= MasTime[3]))
                {
                    throw new Exception("The lesson can't end without ever having begun");
                }
                int minutes = (MasTime[2] - MasTime[0]) * 60;
                minutes += MasTime[3] - MasTime[1];
                if (minutes > 80 || minutes < 40)
                {
                    throw new Exception("The lesson should go on at least 40 minutes and a maximum of 80 minutes");
                }
                if (TextBox4.Text != "Понеділок" && TextBox4.Text != "Вівторок" && TextBox4.Text != "Середа" && TextBox4.Text != "Четвер" && TextBox4.Text != "П'ятниця")
                {
                    throw new Exception("Learning takes place only on weekdays");
                }
                if (count != 0)
                {
                    throw new Exception("Schedule with the same parameters is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Update_Schedule] " + TextBox1.Text + ",'" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "'," + TextBox5.Text, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Schedule");
                    sda2.Fill(dt2);
                    MessageBox.Show("Schedule with id_schedule " + TextBox1.Text + " was updated !\n(Schedule updated if there is no schedule with the same start time, end time, day and id_level in the table)");
                    Schedule_Update_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Group_Click(object sender, RoutedEventArgs e)//Обробник кнопки Update_Group
        {
            try
            {
                int NumOfStudents = Convert.ToInt32(TextBox2.Text);
                int DurationOfLesson = Convert.ToInt32(TextBox3.Text);
                int AudienceFloor = Convert.ToInt32(TextBox4.Text.Substring(0, 1));
                int AudienceNum = Convert.ToInt32(TextBox4.Text.Substring(1, 2));
                if (NumOfStudents > 20 || NumOfStudents < 5)
                {
                    throw new Exception("The number of pupils should be between 5 and 20!");
                }
                if (DurationOfLesson > 80 || DurationOfLesson < 40)
                {
                    throw new Exception("The lesson should go on at least 40 minutes and a maximum of 80 minutes");
                }
                if (AudienceFloor > 3 || AudienceFloor < 1)
                {
                    throw new Exception("Lessons should be on 1, 2, 3 floors");
                }
                if (AudienceNum > 20 || AudienceNum < 1)
                {
                    throw new Exception("The audience number should be between 1 and 20");
                }
                SqlCommand cmd = new SqlCommand("EXEC [English courses].[dbo].[Update_Group] " + TextBox1.Text + ", " + TextBox2.Text + ", " + TextBox3.Text + ", " + TextBox4.Text, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Group");
                sda.Fill(dt);
                MessageBox.Show("Group with id_group: " + TextBox1.Text + " was updated!");
                Group_Update_Click(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Exam_Click(object sender, RoutedEventArgs e)//Обробник кнопки Update_Exam
        {
            Regex ExamTime = new Regex(PatternTimeExam);
            try
            {

                string str = MyCalendar.SelectedDate.Value.ToString("yyyy-MM-dd");
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Exam] where [Day]='" + str + "' and Grade=" + TextBox2.Text + " and Start= '" + TextBox3.Text + "' and id_level1= " + TextBox4.Text, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Exam");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if ((MyCalendar.SelectedDate.Value.Subtract(DateTime.Today)).TotalDays < 7)
                {
                    throw new Exception("The exam must take place at least 7 days from today");
                }
                int grade = Convert.ToInt32(TextBox2.Text);
                if (grade > 5 || grade < 0)
                {
                    throw new Exception("The grade should be between 0 and 5");
                }
                if (!ExamTime.IsMatch(TextBox3.Text))
                {
                    throw new Exception("Exam must begin from 8:00 to 9:59!");
                }
                if (count != 0)
                {
                    throw new Exception("Exam with the same parameters is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Update_Exam] " + TextBox1.Text + ", '" + str + "', " + TextBox2.Text + ", '" + TextBox3.Text + "', " + TextBox4.Text, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Exam");
                    sda2.Fill(dt2);
                    MessageBox.Show("Exam with id_exam : " + TextBox1.Text + " was updated!");
                    Exam_Update_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Level_Click(object sender, RoutedEventArgs e)//Обробник кнопки Update_Level
        {
            Regex Level = new Regex(PatternLevel);
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Level] where Name_of_level='" + TextBox2.Text + "' and Time_of_studing=" + TextBox3.Text + " and Price= " + TextBox4.Text + " and id_language1= " + TextBox5.Text, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Level");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (!Level.IsMatch(TextBox2.Text))
                {
                    throw new Exception("Invalid name of level.\nYou can use only: A1, A2, B1, B2, C1, C2.");
                }
                int TimeOfStuding = Convert.ToInt32(TextBox3.Text);
                if (TimeOfStuding > 12 || TimeOfStuding < 1)
                {
                    throw new Exception("Time of studing should be between 1 and 12(months).");
                }
                int price = Convert.ToInt32(TextBox4.Text);
                if (price > 30000 || price < 1000)
                {
                    throw new Exception("Price should be between 1000 and 30000.");
                }
                if (count != 0)
                {
                    throw new Exception("Level with the same parameters is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Update_Level] " + TextBox1.Text + ", " + TextBox2.Text + ", " + TextBox3.Text + ", " + TextBox4.Text + ", " + TextBox5.Text, con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Level");
                    sda2.Fill(dt2);
                    MessageBox.Show("Level with id_level " + TextBox1.Text + " was updated!");
                    Level_Update_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Language_Click(object sender, RoutedEventArgs e)//Обробник кнопки Update_Language
        {
            Regex Language = new Regex(PatternLanguage);
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from [English courses].[dbo].[Language] where Name_of_language='" + TextBox2.Text + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Language");
                sda.Fill(dt);
                int count = dt.Rows.Count;
                if (!Language.IsMatch(TextBox2.Text))
                {
                    throw new Exception("Invalid language!");
                }
                if (count != 0)
                {
                    throw new Exception("Language with the same name is currently exist! (Input new values)");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("EXEC [English courses].[dbo].[Update_Language] " + TextBox1.Text + ", '" + TextBox2.Text + "'", con);
                    SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable("Language");
                    sda2.Fill(dt2);
                    MessageBox.Show("Language with id_language: " + TextBox1.Text + " was updated!");
                    Language_Update_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Payment_Click(object sender, RoutedEventArgs e)//Обробник кнопки Update_Paymet
        {
            try
            {
                if ((MyCalendar.SelectedDate.Value.Subtract(DateTime.Today)).TotalDays < 0)
                {
                    throw new Exception("Invalid date.You must select today's date or future date.");
                }
                int paid = Convert.ToInt32(TextBox2.Text);
                if (paid > 30000 || paid < 0)
                {
                    throw new Exception("Pupil pay amount between 0 and 30000.");
                }
                string str = MyCalendar.SelectedDate.Value.ToString("yyyy-MM-dd");
                SqlCommand cmd = new SqlCommand("EXEC [English courses].[dbo].[Update_Payment] " + TextBox1.Text + ", '" + str + "'," + TextBox2.Text, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Payment");
                sda.Fill(dt);
                MessageBox.Show("Payment with id_payment : " + TextBox1.Text + " was updated!");
                Payment_Update_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*                               //////////////////////////                   FINDBY MENUITEM         ////////////////////////////                            */

        private void Teacher_FindBy_Click(object sender, RoutedEventArgs e)//Обробник кліку для FindBy->Teacher
        {
            Hide_Elements();
            MyRectangle1.Visibility = Visibility.Visible;
            Label6.Visibility = Visibility.Visible;
            Label6.Content = "Day_of_Birth";
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            FindBy_Teacher.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
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
            Label6.Visibility = Visibility.Visible;
            Label6.Content = "Day_of_Birth";
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            FindBy_Customer.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
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
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            FindBy_Studing.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
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
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            FindBy_Schedule.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
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
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            FindBy_Group.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
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
            Label6.Visibility = Visibility.Visible;
            Label6.Content = "Day_of_Birth";
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            FindBy_Exam.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
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
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            FindBy_Level.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
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
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            FindBy_Language.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
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
            Label6.Visibility = Visibility.Visible;
            Label6.Content = "Day_of_Birth";
            Label7.Visibility = Visibility.Visible;
            Label8.Visibility = Visibility.Visible;
            FindBy_Payment.Visibility = Visibility.Visible;
            TextBox6.Visibility = Visibility.Visible;
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
                    str = "'" + TextBox6.Text + "'";
                }
                else if (ComboBox.Text == "Day_of_Birth")
                {
                    str = "'" + MyCalendar.SelectedDate.Value.ToString("dd.MM.yyyy") + "'";
                }
                else
                {
                    str = TextBox6.Text;
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
                    str = "'" + TextBox6.Text + "'";
                }
                else if (ComboBox.Text == "Day_of_Birth")
                {
                    str = "'" + MyCalendar.SelectedDate.Value.ToString("dd.MM.yyyy") + "'";
                }
                else
                {
                    str = TextBox6.Text;
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
                string str = TextBox6.Text;
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
                    str = TextBox6.Text;
                }
                else
                {
                    str = "'" + TextBox6.Text + "'";
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
                string str = TextBox6.Text;
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
                    str = "'" + TextBox6.Text + "'";
                }
                else
                {
                    str = TextBox6.Text;
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
                    str = "'" + TextBox6.Text + "'";
                }
                else
                {
                    str = TextBox6.Text;
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
                    str = "'" + TextBox6.Text + "'";
                }
                else
                {
                    str = TextBox6.Text;
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
                    str = TextBox6.Text;
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

