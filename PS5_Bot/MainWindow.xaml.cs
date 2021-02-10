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
        private Random rnd = new Random();

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
                    bool run = true;

                    await _browserLogic.OpenPS5Screen();

                    while (run.Equals(true))
                    {
                        await _browserLogic.ReloadTab();
                        run = _browserLogic.CheckIfProductIsAvailable();
                        Thread.Sleep(DelayInSec(5));
                    }

                    infobox.Text = "Check if Product was bought correctly!";
                    await _browserLogic.CloseBrowser();
            }
            catch { }
        }

        private void Closebtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
