using System;
using System.Windows;

namespace Registrator
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

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            acCity.Text = string.Empty;
        }

        private void NewUser_OnNeedClearEvent(object sender, EventArgs e)
        {
            acCity.Text = string.Empty;

        }
    }
}
