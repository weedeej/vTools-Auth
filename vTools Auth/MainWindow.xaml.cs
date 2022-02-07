using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using vTools_Auth.src;
namespace vTools_Auth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private String[] regionList = { "AP", "KR", "NA", "EU", "BR" };
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            ((Button)sender).Content = "Authenticating...";
            LoginWithClient session = new LoginWithClient();
            if (!session.isSessionReady() || field_discordid.Text.Length < 18 || field_discordid.Text.Length > 18 || combo_region.SelectedIndex == -1)
            {
                await this.ShowMessageAsync("E R R O R", "Hey! You are using this wrong. Read below to see how to!");
                return;
            }
            session.obtainSession();
            session.sess.id = field_discordid.Text;
            session.sess.shard = regionList[combo_region.SelectedIndex];
            SessionHandler handler = new SessionHandler(session.sess);
            String task = await Task.Run(handler.SaveSession);
            String status = "Auth is successful!";
            if (task.Contains("error")) status = "E R R O R";
            else await PostHandling.RemoveLocalSession();
            await this.ShowMessageAsync(status, task);
            ((Button)sender).IsEnabled = true;
            ((Button)sender).Content = "Auth with Riot Client";

        }

        private void btnDiscordId_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "https://github.com/weedeej/vTools-Auth/blob/master/DiscordID.md#1-open-discord-browser-or-desktop-app-doesnt-matter-and-head-to-settings");
        }
    }
}
