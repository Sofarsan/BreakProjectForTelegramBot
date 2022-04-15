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
        

        public MainWindow()
        {
            InitializeComponent();
            TextBox newTB = new TextBox();
            newTB.Height = 20;
            newTB.Width = 400;

            StackPanel_OptionAnswer.Children.Add(newTB);

           
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

            StackPanel_OptionAnswer.Children.Add(newTB);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (ComboBox_QuestionType.SelectedIndex)
            {
                case 0:
                    {
                        QuestionInput qi = new QuestionInput(TextBox_questionText.Text);

                    }
                    break;

                case 1:
                    {
                        List<string> questionTextList = new List<string>();
                       
                       foreach (TextBox tb in StackPanel_OptionAnswer.Children)
                        {
                            questionTextList.Add(tb.Text);
                        }
                        QuestionMultiSelect qms = new QuestionMultiSelect(TextBox_questionText.Text,questionTextList);

                    }
                    break;
                case 2:
                    {
                        QuestionYesNo qyn = new QuestionYesNo(TextBox_questionText.Text);
                    }
                    break;
                case 3:
                    {
                        
                        List<string> questionTextList = new List<string>();

                        foreach (TextBox tb in StackPanel_OptionAnswer.Children)
                        {
                            questionTextList.Add(tb.Text);
                        }
                        QuestionSingleSelect qms = new QuestionSingleSelect(TextBox_questionText.Text, questionTextList);
                    }
                    break;
                case 4:
                    {
                        
                        List<string> questionTextList = new List<string>();

                        foreach (TextBox tb in StackPanel_OptionAnswer.Children)
                        {
                            questionTextList.Add(tb.Text);
                        }
                        QuestionSort qms = new QuestionSort(TextBox_questionText.Text, questionTextList);
                    }
                    break;
            }

        }

        private void Button_DeleteOptionAnswer_Click(object sender, RoutedEventArgs e)
        {
            if(StackPanel_OptionAnswer.Children.Count - 1 > 0)
            {
                StackPanel_OptionAnswer.Children.RemoveAt(StackPanel_OptionAnswer.Children.Count - 1);

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
    }
}
