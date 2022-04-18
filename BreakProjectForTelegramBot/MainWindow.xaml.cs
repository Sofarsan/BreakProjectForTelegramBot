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

        public MainWindow()
        {
            InitializeComponent();
            TextBox newTB = new TextBox();
            newTB.Height = 20;
            newTB.Width = 400;

            ListBoxQuestion.Items.Add(newTB);

            ComboBox_QuestionType.SelectedIndex = 1;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBox_QuestionType.SelectedIndex)
            {
                case 0:
                    {
                        Grid_optionAnswer.Visibility = Visibility.Hidden;
                    }
                    break;

                case 1:
                    {
                        Grid_optionAnswer.Visibility = Visibility.Visible;
                    }
                    break;
                case 2:
                    {
                        Grid_optionAnswer.Visibility = Visibility.Hidden;
                    }
                    break;
                case 3:
                    {
                        Grid_optionAnswer.Visibility = Visibility.Visible;
                    }
                    break;
                case 4:
                    {
                        Grid_optionAnswer.Visibility = Visibility.Visible;
                    }
                    break;
            }

        }

        private void Button_AddOptionAnswer_Click(object sender, RoutedEventArgs e)
        {
            TextBox newTB = new TextBox();
            newTB.Height = 20;
            newTB.Width = 400;

            //StackPanel_OptionAnswer.Children.Add(newTB);
            ListBoxQuestion.Items.Add(newTB);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (ComboBox_QuestionType.SelectedIndex)
            {
                case 0:
                    {

                        _actual.AddQuestion(new QuestionIWithoutOptionAnswer(
                            ComboBox_QuestionType.SelectedItem.ToString(),
                            TextBox_questionText.Text));


                    }
                    break;

                case 1:
                    {
                        List<string> questionTextList = new List<string>();

                        foreach (TextBox tb in ListBoxQuestion.Items)
                        {
                            questionTextList.Add(tb.Text);
                        }
                        _actual.AddQuestion(new QuestionIWithOptionAnswer(
                             ComboBox_QuestionType.SelectedItem.ToString(),
                             TextBox_questionText.Text, questionTextList));

                    }
                    break;
                case 2:
                    {
                        _actual.AddQuestion(new QuestionIWithoutOptionAnswer(
                            ComboBox_QuestionType.SelectedItem.ToString(),
                            TextBox_questionText.Text));
                    }
                    break;
                case 3:
                    {

                        List<string> questionTextList = new List<string>();

                        foreach (TextBox tb in ListBoxQuestion.Items)
                        {
                            questionTextList.Add(tb.Text);
                        }
                        _actual.AddQuestion(new QuestionIWithOptionAnswer(
                            ComboBox_QuestionType.SelectedItem.ToString(),
                            TextBox_questionText.Text, questionTextList));
                    }
                    break;
                case 4:
                    {

                        List<string> questionTextList = new List<string>();

                        foreach (TextBox tb in ListBoxQuestion.Items)
                        {
                            questionTextList.Add(tb.Text);
                        }
                        _actual.AddQuestion(new QuestionIWithOptionAnswer(
                            ComboBox_QuestionType.SelectedItem.ToString(),
                            TextBox_questionText.Text, questionTextList));
                    }
                    break;
            }

        }

        private void Button_DeleteOptionAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxQuestion.Items.Count - 1 > 0)
            {
                ListBoxQuestion.Items.RemoveAt(ListBoxQuestion.Items.Count - 1);

            }
        }

        public BindingList<User> _toAddUser;

        private void Window_User(object sender, RoutedEventArgs e)
        {
            _toAddUser = new BindingList<User>()
            {
                new User(){LastName ="Leto",Name="QQQ",Age=232},
                new User(){LastName ="Человек",Name="Который смеется ",Age=154},
                new User(){LastName ="Гранде",Name="Евгения",Age=14},
            };
            ListUser.ItemsSource = _toAddUser;
        }

        private void ListBoxQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddTitleButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_ChooseTest.Items.Add(TestNameTextBox.Text);
            ComboBox_ChooseTest.SelectedItem = TestNameTextBox.Text;

            _tests.Add(new Test(TestNameTextBox.Text));

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
    }
}
