using BusinessLogicLayer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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

            ComboBox_QuestionType.SelectedIndex = 1;
        }

        private void ListQuestionsUpdate()
        {
            ListQuestions.Items.Clear();
            if (_actual is not null && (_actual.GetListQuestion() is not null))
            {
                List<AbstractQuestion> AqList = _actual.GetListQuestion();

                for (int i = 0; i < AqList.Count; i++)
                {
                    Button BListQ = new Button();
                    BListQ.Content = i;
                    ListQuestions.Items.Add(BListQ);
                }
            }
;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_QuestionType.SelectedIndex < 2)
            {

                _actual.AddQuestion(new QuestionIWithoutOptionAnswer(
                    ComboBox_QuestionType.SelectedItem.ToString(),
                    TextBox_questionText.Text));
            }
            else
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
            ListQuestionsUpdate();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ButtonEdit.Content = "Save";
        }
    }
}
