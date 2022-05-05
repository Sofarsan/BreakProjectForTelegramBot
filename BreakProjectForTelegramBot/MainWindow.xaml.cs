using BusinessLogicLayer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading; //для таймера
using System;
using BusinessLogicLayer.Telegram;
using System.Collections.ObjectModel;
using BusinessLogicLayer.GroupsSerialize;

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
        private ObservableCollection<Test> _tests = new ObservableCollection<Test>();
        private Test _actual;
        private DispatcherTimer _timer;
        private UserGroup _add;
        private List<AnswersUser> _answersUser = new List<AnswersUser>();

        private ObservableCollection<UserGroup> groups = new ObservableCollection<UserGroup>()
        {
        };

        public MainWindow()
        {
            _telega = new Telega(_token, OnMessages);
            groups = GroupsSerialize.LoadGroupsObservableCollectionDecerialize();
            _telega.groups = groups;
            _labels = new List<string>();
            InitializeComponent();
            LoadTestFromJson();

            ComboBoxGroup.ItemsSource = groups;
            ComboBoxTest.ItemsSource = _tests;
            WriteNamenewGroup.ItemsSource = groups;

            BaseSerialize.LoadUserDictionary();
            DataGridListUser.ItemsSource = BaseBot.NameBase;
            WriteNamenewGroup.DataContext = this; //разобраться че это

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTick;
            _timer.Start();

            ComboBox_QuestionType.SelectedIndex = 1;
        }

        /// <summary>
        /// Refresh list of users in the UI according to the users currently connected
        /// to our bot
        /// </summary>

        public void LoadTestFromJson()
        {
            _tests = BaseSerialize.LoadTestsDictionary();
            foreach (Test test in _tests)
            {
                ComboBox_ChooseTest.Items.Add(test.name);
            }
        }
        public void LoadUserGroupsFromJson()
        {

            ObservableCollection<UserGroup> group = GroupsSerialize.LoadGroupsObservableCollectionDecerialize();
            foreach (Test test in _tests)
            {
                ComboBox_ChooseTest.Items.Add(test.name);
            }
        }

        public void OnMessages(string s)
        {
            _labels.Add(s);
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            _telega.Start();
        }

        private void OnTick(object sender, EventArgs e)
        {

        }

        private void ListQuestionsUpdate() //попробовать переделать
        {
            ListQuestions.Items.Clear();
            if (_actual is not null && (_actual.GetListQuestion() is not null))
            {
                List<Question> AqList = _actual.GetListQuestion();

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
        }

        private void newBtn_Click(object sender, RoutedEventArgs e) //поменять имя
        {
            ListQuestions.SelectedItem = sender;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (ComboBox_QuestionType.SelectedIndex + 1 == (int)QuestionType.QuestionSingleSelect)
            {
                foreach (WrapPanel wrap in ListBoxQuestion.Items)
                {

                    CheckBox cBox = (CheckBox)wrap.Children[0];
                    if (cBox == chk)
                    {
                        continue;
                    }
                    else
                    {
                        cBox.IsChecked = false;
                    }
                }
            }
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
            newTB.Width = 350;
            newTB.AcceptsReturn = true;
            newTB.TextWrapping = TextWrapping.Wrap;

            WrapPanel listItem = new WrapPanel();
            CheckBox checkBox = new CheckBox();
            checkBox.Height = 30;
            checkBox.Width = 40;
            ScaleTransform scale = new ScaleTransform(1.3, 1.3);
            checkBox.RenderTransformOrigin = new Point(-0.5, -0.5);
            checkBox.RenderTransform = scale;

            checkBox.Checked += CheckBox_Checked;
            listItem.Children.Add(checkBox);
            listItem.Children.Add(newTB);

            ListBoxQuestion.Items.Add(listItem);
        }

        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            if (_actual != null)
            {
                List<OptionAnswer> questionTextList = new List<OptionAnswer>();

                foreach (WrapPanel tb in ListBoxQuestion.Items)
                {
                    TextBox tBox = (TextBox)tb.Children[1];
                    CheckBox cBox = (CheckBox)tb.Children[0];

                    bool? value = cBox.IsChecked;
                    OptionAnswer oAnswer = new OptionAnswer(tBox.Text, value.Value);
                    questionTextList.Add(oAnswer);
                }
                _actual.AddQuestion(new Question(
                     ((ComboBoxItem)ComboBox_QuestionType.SelectedValue).Content.ToString(),
                     TextBox_questionText.Text, questionTextList));
                BaseSerialize.SaveTestsObservableCollection(_tests);
                ListQuestionsUpdate();
            }
            else
            {
                MessageBox_Warning();
            }

        }

        private void Button_DeleteOptionAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxQuestion.Items.Count - 1 > 0)
            {
                ListBoxQuestion.Items.RemoveAt(ListBoxQuestion.Items.Count - 1);
            }
        }

        private void AddTitleButton_Click(object sender, RoutedEventArgs e) //переименовать в AddTitleTestButton_Click
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
            int minutes = Convert.ToInt32(((ComboBoxItem)ComboBoxTimer.SelectedValue).Content.ToString());

            DateTime now = DateTime.Now;
            TimeSpan ts = TimeSpan.FromMinutes(minutes);
            DateTime endTime = now.Add(ts);
            _actual._endTime = endTime.ToShortTimeString();
        }


        private void ComboBox_ChooseTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Test test in _tests)
            {
                if (test.name == ComboBox_ChooseTest.SelectedItem)
                {
                    _actual = test;
                }
            }
            ListQuestionsUpdate();
        }
        private void ComboBoxTimer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //    foreach (Test test in _tests)
            //    {
            //        if (test._timer == ComboBoxTimer.SelectedItem)
            //        {
            //            _actual = test;
            //        }
            //    }
            //    ListQuestionsUpdate();
        }

        private void ListQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListQuestions.SelectedIndex != -1)
            {
                List<Question> AqList = _actual.GetListQuestion();
                ComboBox_QuestionType.Text = AqList[ListQuestions.SelectedIndex]._type.ToString();
                ListBoxQuestion.Items.Clear();
                Question qwoa = (Question)AqList[ListQuestions.SelectedIndex];
                TextBox_questionText.Text = AqList[ListQuestions.SelectedIndex]._questionText;

                foreach (OptionAnswer oAnswer in qwoa._optionAnswer)
                {
                    TextBox newTB = new TextBox();
                    newTB.Height = 30;
                    newTB.Width = 400;
                    newTB.AcceptsReturn = true;
                    newTB.TextWrapping = TextWrapping.Wrap;
                    newTB.Text = oAnswer.Text;

                    WrapPanel listItem = new WrapPanel();
                    CheckBox checkBox = new CheckBox();
                    checkBox.Height = 30;
                    checkBox.Width = 40;
                    ScaleTransform scale = new ScaleTransform(1.3, 1.3);
                    checkBox.RenderTransformOrigin = new Point(-0.5, -0.5);
                    checkBox.RenderTransform = scale;
                    checkBox.IsChecked = oAnswer.IsValid;

                    checkBox.Checked += CheckBox_Checked;
                    listItem.Children.Add(checkBox);
                    listItem.Children.Add(newTB);

                    ListBoxQuestion.Items.Add(listItem);
                }
            }
        }

        private void WriteNamenewGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserGroup groupOfUser = (UserGroup)WriteNamenewGroup.SelectedItem;
            if (groupOfUser == null || groupOfUser.Users.Count == 0)
            {
                UsersInGroup.ItemsSource = null;
            }
            else
            {
                UsersInGroup.ItemsSource = groupOfUser.Users;
            }

        }

        private void AddNewGroup_Click(object sender, RoutedEventArgs e)
        {
            if (Group.Text == "")
            {
                MessageBox.Show("Введите название группы");
            }

            _add = new UserGroup(Group.Text);
            groups.Add(_add);
            _telega.groups = groups;

            GroupsSerialize.SaveGroupsObservableCollection(groups);


            UserGroup userNewGroup = new UserGroup(Group.Text);
            WriteNamenewGroup.Items.Refresh();
        }
        private void DeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (((UserGroup)WriteNamenewGroup.SelectedItem).NameGroup != "Другие")
            {
                foreach (User user in ((UserGroup)WriteNamenewGroup.SelectedItem).Users)
                {
                    groups[0].AddUser(user);
                }
                groups.Remove((UserGroup)WriteNamenewGroup.SelectedItem);
                WriteNamenewGroup.Items.Refresh();
            }
            else
            {
                MessageBox_Warning();
            }
        }


        private void ChangeNameGroup_Click(object sender, RoutedEventArgs e)
        {
            UserGroup ug = (UserGroup)WriteNamenewGroup.SelectedItem;

            // Update storage
            foreach (UserGroup group in groups)
            {
                if (group.NameGroup == ug.NameGroup)
                {
                    group.NameGroup = NewNameGroup.Text;
                    break;
                }
            }
            GroupsSerialize.SaveGroupsObservableCollection(groups);

            // Update UI
            ug.NameGroup = NewNameGroup.Text;
            WriteNamenewGroup.Items.Refresh();
            NewNameGroup.Clear();
        }

        private void ChangeUserName_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)UsersInGroup.SelectedItem;
            user.LastName = WriteLastName.Text;
            user.FirstName = WriteFirstName.Text;

            BaseBot.NameBase[user.Id].FirstName = user.FirstName;
            BaseBot.NameBase[user.Id].LastName = user.LastName;

            UsersInGroup.Items.Refresh();
            DataGridListUser.Items.Refresh();

            BaseSerialize.SaveUserDictionary(BaseBot.NameBase);
            GroupsSerialize.SaveGroupsObservableCollection(groups);
        }

        private void AddNewUserInGroup_Click(object sender, RoutedEventArgs e)
        {
            int index = WriteNamenewGroup.SelectedIndex;
            KeyValuePair<long, User> selectedItem = ((KeyValuePair<long, User>)DataGridListUser.SelectedItem);
            User user = new User(selectedItem.Value.FirstName, selectedItem.Value.LastName, selectedItem.Key);
            if (index < 0 || groups[index].Users.Contains(user))
            {
                return;
            }
            groups[index].Users.Add(selectedItem.Value);
            _telega.groups = groups;

            GroupsSerialize.SaveGroupsObservableCollection(groups);

            UsersInGroup.Items.Refresh();
        }

        private void UsersInGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersInGroup.SelectedItem != null)
            {
                WriteFirstName.IsEnabled = true;
                WriteLastName.IsEnabled = true;

                User user = (User)UsersInGroup.SelectedItem;
                WriteLastName.Text = user.LastName;
                WriteFirstName.Text = user.FirstName;
            }
        }

        private void Button_SaveQ_Click(object sender, RoutedEventArgs e)
        {
            if (ListQuestions.SelectedIndex == -1)
            {
                return;
            }
            List<Question> AqList = _actual.GetListQuestion();

            ComboBox_QuestionType.Text = AqList[ListQuestions.SelectedIndex]._type.ToString();
            if (AqList[ListQuestions.SelectedIndex]._type == QuestionType.QuestionInput ||
                AqList[ListQuestions.SelectedIndex]._type == QuestionType.QuestionYesNo)
            {
                AqList[ListQuestions.SelectedIndex]._questionText = TextBox_questionText.Text;
            }
            else
            {
                Question qwoa = (Question)AqList[ListQuestions.SelectedIndex];
                AqList[ListQuestions.SelectedIndex]._questionText = TextBox_questionText.Text;

                qwoa._optionAnswer.Clear();


                foreach (WrapPanel wPanel in ListBoxQuestion.Items)
                {
                    TextBox tBox = (TextBox)wPanel.Children[1];
                    CheckBox cBox = (CheckBox)wPanel.Children[0];

                    bool? value = cBox.IsChecked;

                    OptionAnswer oAnswer = new OptionAnswer(tBox.Text, value);
                    qwoa._optionAnswer.Add(oAnswer);
                }

            }
            BaseSerialize.SaveTestsObservableCollection(_tests);
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
            MessageBox.Show("Are u stupid?", "Stop it", MessageBoxButton.OK, MessageBoxImage.Warning); //поменять
        }

        public async void Button_SendQuestion_Click(object sender, RoutedEventArgs e)
        {
            _telega.SendQuestion(QuestionMock.getQuestion()); //УДАЛИТЬ
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            DataGridListUser.Items.Refresh();
            //RefreshListOfUsers();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            // Remove the group from our storage
            User selectedUser = (User)UsersInGroup.SelectedItem;
            foreach (UserGroup group in groups)
            {
                if (group.Users.Contains(selectedUser))
                {
                    group.Users.Remove(selectedUser);
                    break;
                }
            }
            _telega.groups = groups;

            GroupsSerialize.SaveGroupsObservableCollection(groups);

            // Remove from the UI as well
            IEditableCollectionView items = UsersInGroup.Items;
            if (items.CanRemove)
            {
                items.Remove(UsersInGroup.SelectedItem);
            }
            UsersInGroup.Items.Refresh();
        }

        private void SendTestButton_Click(object sender, RoutedEventArgs e)
        {
            UserGroup group = (UserGroup)ComboBoxGroup.SelectedItem;
            Test test = (Test)ComboBoxTest.SelectedItem;

            foreach (User user in group.Users)
            {
                OngoingTest ongoingTest = new OngoingTest(test);
                user.ongoingTest = ongoingTest;
                BaseSerialize.SaveUserDictionary(BaseBot.NameBase);
                _telega.AskConfirmation(user);
            }
        }
    }
}
