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
            

            SP.Children.Add(newTB);
        }
    }
}
