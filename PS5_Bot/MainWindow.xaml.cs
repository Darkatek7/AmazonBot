using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;

namespace PS5_Bot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BrowserLogic _browserLogic = new BrowserLogic();
        private Thread myThread;
        private int delay;
        private int defaultDelay = 4;
        private string product;

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
                if (myThread == null)
                {
                    myThread = new Thread(Run);
                    myThread.Start(); // creates new thread to run the BuyProductIfAvailable function
                }
                else
                {
                    myThread.Resume(); // resumes paused thread
                }
            }
            catch { }
        }

        private async void Closebtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (myThread != null)
            {
                try
                {
                    myThread.Suspend(); // pauses thread
                }
                catch { }
            }
        }

        public async void Run()
        {
            bool run = true;
            delay = DelayInSec(defaultDelay);

            await _browserLogic.OpenPS5Screen(); // opens your url

            while (run.Equals(true))
            {
                run = await _browserLogic.BuyProductIfAvailable(product, delay); // check if he product is available
                await _browserLogic.ReloadTab(); // reload tab of chrome browser
            }

            infobox.Text = "Check if Product was bought correctly!";
        }

        private async void Delaytxtbox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private async void Versioncombobox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            product = "PS5_Digital";

            try
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(() => { product = ((ComboBoxItem)versioncombobox.SelectedItem).Name; }));
            }
            catch { }
        }

        private async void Delaytxtbox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            delay = DelayInSec(defaultDelay);

            try
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    if (!string.IsNullOrEmpty(delaytxtbox.Text))
                        delay = DelayInSec(Convert.ToInt32(delaytxtbox.Text));
                    else delay = DelayInSec(defaultDelay);
                }));
            }
            catch
            {
                delay = DelayInSec(defaultDelay);
            }
        }

        private async void MainWindow_OnClosed(object sender, EventArgs e)
        {
            await _browserLogic.CloseBrowser();
            Environment.Exit(Environment.ExitCode); // exits program
        }
    }
}
