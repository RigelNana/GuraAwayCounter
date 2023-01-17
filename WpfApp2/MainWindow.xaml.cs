using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private const string ApiUrl = "https://script.google.com/macros/s/AKfycbwMWX_dh4P2vVjMY40T1TetbUAUFgy_4NHL9xvMhvTeZcRL_1RPzwxeDs21_EWAde3F/exec";
        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            
            var proxyText = TextBox1.Text;
            var liveresponse = Url.GetLiveResponse(ApiUrl,proxyText);
            if (liveresponse != null)
            {
                MessageBox.Show(liveresponse);
            }
            else
            {
                MessageBox.Show("Error");
            }            
        }
    }

    class Url
    {
        public static string UrlResponse(string url,string proxyurl)
        {
            var options = new RestClientOptions(url)
            {
                Proxy = GetProxy(proxyurl),
                ThrowOnAnyError = true,

            };
            var client = new RestClient(options);
            var request = new RestRequest();
            
            RestResponse response = client.Get(request);
            var content = response.Content;
            try
            {
                return content;
            }
            catch
            {
                throw new Exception();
            }
            
        }

        public static string GetLiveResponse(string url,string proxyurl)
        {
            string liveresponse = UrlResponse(url,proxyurl);
            return liveresponse;

        }

        public static WebProxy GetProxy(string proxyurl)
        {
            var proxy = new WebProxy()
            {
                Address = new Uri(proxyurl),
                BypassProxyOnLocal = false,
                Credentials = CredentialCache.DefaultNetworkCredentials
            };
            return proxy;

        }
    }
    
}
