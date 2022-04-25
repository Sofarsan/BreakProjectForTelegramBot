using BusinessLogicLayer;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading; //для таймера
using BusinessLogicLayer;
using System;

namespace BreakProjectForTelegramBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Telega _telega;
        private const string _token = "5331081992:AAEmEzmU2lWqKLn9mgYCYbcNnPSLVDEHHQM";
        private List<string> _labels;

        private List<Test> _tests = new List<Test>();
        private Test _actual;
        private DispatcherTimer _timer;

        UserGroup groupOne = UsersMock.GetGroupNumberOne();
        UserGroup groupTwo = UsersMock.GetGroupNumberTwo();
        UserGroup groupTree = UsersMock.GetGroupNumberTree();
        List<UserGroup> groups = new List<UserGroup>();

        public MainWindow()
        {
            _telega = new Telega(_token, OnMessages);
            _labels = new List<string>();
            InitializeComponent();
            ListBox_BotMessages.ItemsSource = _labels;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTick;
            _timer.Start();

            ComboBox_QuestionType.SelectedIndex = 1;

            groups.Add(groupOne);
            groups.Add(groupTwo);
            groups.Add(groupTree);

            WriteNamenewGroup.ItemsSource = groups;
        }

        public void OnMessages(string s)
        {
            _labels.Add(s);
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            _telega.Start();
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            _telega.Send(TextBox_Send.Text);
        }

        private void OnTick(object sender, EventArgs e)
        {
            ListBox_BotMessages.Items.Refresh();
        }

        private void ListQuestionsUpdate()
        {
            ListQuestions.Items.Clear();
            if (_actual is not null && (_actual.GetListQuestion() is not null))
            {
                List<AbstractQuestion> AqList = _actual.GetListQuestion();

                for (int i = 0; i < AqList.Count; i++)
                {
                    Button newBtn = new Button();
                    newBtn.Click += newBtn_Click;
                    newBtn.Content = AqList[i]._questionText;
                    newBtn.Height = 30;
                    newBtn.Width = 200;
                    ListQuestions.Items.Add(newBtn);
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

        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_QuestionType.SelectedIndex < 2)
            {
                List<string> questionTextList = new List<string>();

                foreach (TextBox tb in ListBoxQuestion.Items)
                {
                    questionTextList.Add(tb.Text);
                }
                _actual.AddQuestion(new QuestionWithOptionAnswer(
                     ((ComboBoxItem)ComboBox_QuestionType.SelectedValue).Content.ToString(),
                     TextBox_questionText.Text, questionTextList));
                ListQuestionsUpdate();
            }
            else
            {
                MessageBox_Warning();
            }
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
                //new User(){LastName ="Leto",Name="QQQ",Age=232},
                //new User(){LastName ="Человек",Name="Который смеется ",Age=154},
                //new User(){LastName ="Гранде",Name="Евгения",Age=14},
            };
            ListUser.ItemsSource = _toAddUser;
        }


        private void AddTitleButton_Click(object sender, RoutedEventArgs e)
        {
            if (TestNameTextBox.Text.Length > 0)
            {
                ComboBox_ChooseTest.Items.Add(TestNameTextBox.Text);
                _actual = new Test(TestNameTextBox.Text);
                _tests.Add(_actual);
                ComboBox_ChooseTest.SelectedItem = TestNameTextBox.Text;
            }
            else
            {
                MessageBox_Warning();
            }

        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            ListQuestions.SelectedItem = sender;
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

        private void ListQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListQuestions.SelectedIndex!=-1)
            {
                List<AbstractQuestion> AqList = _actual.GetListQuestion();
                ComboBox_QuestionType.Text = AqList[ListQuestions.SelectedIndex]._type;
                ListBoxQuestion.Items.Clear();
                QuestionWithOptionAnswer qwoa = (QuestionWithOptionAnswer)AqList[ListQuestions.SelectedIndex];
                TextBox_questionText.Text = AqList[ListQuestions.SelectedIndex]._questionText;

                foreach (string str in qwoa._optionAnswer)
                {
                    TextBox newTB = new TextBox();
                    newTB.Height = 30;
                    newTB.Width = 400;
                    newTB.AcceptsReturn = true;
                    newTB.TextWrapping = TextWrapping.Wrap;
                    newTB.Text = str;
                    ListBoxQuestion.Items.Add(newTB);
                }
            }
            
            //if (AqList[ListQuestions.SelectedIndex]._type == "QuestionInput" ||
            //    AqList[ListQuestions.SelectedIndex]._type == "QuestionYesNo")
            //{
            //    TextBox_questionText.Text = AqList[ListQuestions.SelectedIndex]._questionText;

            //}
            //else
            //{
                
            //}
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

        private void AddNewGroup_Click(object sender, RoutedEventArgs e)
        {
            if (Group.Text == "")
            {
                MessageBox.Show("Введите название группы");
            }

        }

        private void ChangeUserName_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewUserinGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UsersinGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersinGroup.SelectedItem != null)
            {
                WriteName.IsEnabled = true;
            }
        }

        private void Button_SaveQ_Click(object sender, RoutedEventArgs e)
        {
            if (ListQuestions.SelectedIndex == -1)
            {
                return;
            }
            List<AbstractQuestion> AqList = _actual.GetListQuestion();
            ComboBox_QuestionType.Text = AqList[ListQuestions.SelectedIndex]._type;
            if (AqList[ListQuestions.SelectedIndex]._type == "QuestionInput" ||
                AqList[ListQuestions.SelectedIndex]._type == "QuestionYesNo")
            {
                AqList[ListQuestions.SelectedIndex]._questionText = TextBox_questionText.Text;
            }
            else
            {
                QuestionWithOptionAnswer qwoa = (QuestionWithOptionAnswer)AqList[ListQuestions.SelectedIndex];
                AqList[ListQuestions.SelectedIndex]._questionText = TextBox_questionText.Text;

                qwoa._optionAnswer.Clear();
                foreach (TextBox str in ListBoxQuestion.Items)
                {
                    qwoa._optionAnswer.Add(str.Text);
                }
            }
            ListQuestionsUpdate();
        }

        private void TestNameTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TestNameTextBox.Text == "Write test name...")
                TestNameTextBox.Text = "";
        }

        private void TextBox_questionText_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TextBox_questionText.Text == "Write the question...")
                TextBox_questionText.Text = "";
        }

        private void MessageBox_Warning()
        {
            MessageBox.Show("Ты что дурачек ?", "Прекрати", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
