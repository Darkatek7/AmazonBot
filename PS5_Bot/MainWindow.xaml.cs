using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PS5_Bot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BrowserLogic _browserLogic = new BrowserLogic();
        private Thread myThread;

        private int DelayInSec(int delay)
        {
            int delayInSec = delay * 1000;
            return delayInSec;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Buybtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                myThread = new Thread(Run);
                myThread.Start(); // creates new thread to run the checkproduct function
            }
            catch { }
        }

        private void Closebtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (myThread != null)
            {
                _browserLogic.CloseBrowser(); // calls close browser function
            }
        }

        public void Run()
        {
            bool run = true;

            _browserLogic.OpenPS5Screen(); // opens your url

            while (run.Equals(true))
            {
                _browserLogic.ReloadTab(); // reload tab of chrome browser
                run = _browserLogic.CheckIfProductIsAvailable(); // check if he product is available
                Thread.Sleep(DelayInSec(3)); // adjust the number to get a larger or shorter delay between refreshes.
            }

            infobox.Text = "Check if Product was bought correctly!";
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            //if (myThread != null)
            //{
            //    _browserLogic.CloseBrowser(); // calls close function
            //}
        }
    }
}
