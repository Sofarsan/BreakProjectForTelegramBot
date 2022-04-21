using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BusinessLogicLayer;


namespace BreakProjectForTelegramBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Test> _tests = new List<Test>();
        private Test _actual;

        UserGroup groupOne = UsersMock.GetGroupNumberOne();
        UserGroup groupTwo = UsersMock.GetGroupNumberTwo();
        UserGroup groupTree = UsersMock.GetGroupNumberTree();
        List<UserGroup> groups = new List<UserGroup>();
        


        public MainWindow()
        {
            InitializeComponent();

            ComboBox_QuestionType.SelectedIndex = 1;

            groups.Add(groupOne);
            groups.Add(groupTwo);
            groups.Add(groupTree);

            WriteNamenewGroup.ItemsSource = groups;   
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_QuestionType.SelectedIndex < 2)
            {
                Grid_optionAnswer.Visibility = Visibility.Hidden;
            }
            else
            {
                Grid_optionAnswer.Visibility = Visibility.Visible;
            }
        }

        private void Button_AddOptionAnswer_Click(object sender, RoutedEventArgs e)
        {
            TextBox newTB = new TextBox();
            newTB.Height = 30;
            newTB.Width = 400;
            newTB.AcceptsReturn = true;
            newTB.TextWrapping = TextWrapping.Wrap;

            

            ListBoxQuestion.Items.Add(newTB);

        }

        

        private void Button_DeleteOptionAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxQuestion.Items.Count - 1 > 0)
            {
                ListBoxQuestion.Items.RemoveAt(ListBoxQuestion.Items.Count - 1);

            }
        }

        private BindingList<User> _toAddUser;

        private void Window_User(object sender, RoutedEventArgs e)
        {
            _toAddUser = new BindingList<User>()
            {
                //new User(){LastName ="Leto",Name="QQQ",Age=232},
                //new User(){LastName ="Человек",Name="Который смеется ",Age=154},
                //new User(){LastName ="Гранде",Name="Евгения",Age=14},
            };
            ListUser.ItemsSource = _toAddUser;
        }


        private void AddTitleButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_ChooseTest.Items.Add(TestNameTextBox.Text);
            ComboBox_ChooseTest.SelectedItem = TestNameTextBox.Text;
            _actual = new Test(TestNameTextBox.Text);
            _tests.Add(_actual);

        }

        private void ComboBox_ChooseTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Test test in _tests)
            {
                if (test._name == ComboBox_ChooseTest.SelectedItem)
                {
                    _actual = test;

                }
            }
        }

        private void WriteNamenewGroup_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddNewGroup_Click(object sender, RoutedEventArgs e)
        {
            if(Group.Text == "")
            {
                MessageBox.Show("Введите название группы");
            }
            
        }

        private void WriteNamenewGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserGroup groupOfUser = (UserGroup)WriteNamenewGroup.SelectedItem;
            if (groupOfUser == null || groupOfUser.Users.Count == 0)
            {
                UsersinGroup.ItemsSource = null;
            }
            else
            {
               UsersinGroup.ItemsSource = groupOfUser.Users;
            }
            
        }

        private void UsersinGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersinGroup.SelectedItem != null)
            {
                WriteName.IsEnabled = true;
            }
        }
    }
}
