using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Websockets;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Squad.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatPage : ContentPage
	{
        private bool _echo, _failed;
        private readonly IWebSocketConnection _connection;

       // ChatPageViewModel vm;
        public ChatPage ()
		{
            //InitializeComponent ();
            //         BindingContext = vm = new ChatPageViewModel("Lil Pump");
            InitializeComponent();

            // Get a websocket from your PCL library via the factory
            _connection = WebSocketFactory.Create();
            _connection.OnOpened += Connection_OnOpened;
            _connection.OnMessage += Connection_OnMessage;
            _connection.OnClosed += Connection_OnClosed;
            _connection.OnError += Connection_OnError;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _echo = _failed = false;
            _connection.Open("ws://10.0.2.2:5000/");

            while (!_connection.IsOpen && !_failed)
            {
                await Task.Delay(10);
            }

            ScreenMessage.Focus();
        }

        private static void Connection_OnClosed()
        {
            Debug.WriteLine("Closed !");
        }
        private void Connection_OnError(string obj)
        {
            _failed = true;
            Debug.WriteLine("ERROR " + obj);
        }

        private void Connection_OnOpened()
        {
            Debug.WriteLine("Opened !");
        }

        private void Connection_OnMessage(string obj)
        {
            _echo = true;
            Device.BeginInvokeOnMainThread(() =>
            {
                ReceivedData.Children.Add(new Label { Text = obj });
            });
        }

        private async void BtnSend_OnClicked(object sender, EventArgs e)
        {
            _echo = false;
            _connection.Send(ScreenMessage.Text);
            ScreenMessage.Text = "";

            while (!_echo && !_failed)
            {
                await Task.Delay(10);
            }

            ScreenMessage.Focus();
        }
    }
}