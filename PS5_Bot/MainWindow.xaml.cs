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
                myThread.Start();
            }
            catch { }
        }

        private void Closebtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (myThread != null)
            {
                _browserLogic.CloseBrowser();
            }
        }

        public void Run()
        {
            bool run = true;

            _browserLogic.OpenPS5Screen();

            while (run.Equals(true))
            {
                _browserLogic.ReloadTab();
                run = _browserLogic.CheckIfProductIsAvailable();
                Thread.Sleep(DelayInSec(3));
            }

            infobox.Text = "Check if Product was bought correctly!";

            Thread.Sleep(DelayInSec(10));
            _browserLogic.CloseBrowser();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            if (myThread != null)
            {
                _browserLogic.CloseBrowser();
            }
        }
    }
}
