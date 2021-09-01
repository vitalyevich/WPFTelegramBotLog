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
using Newtonsoft.Json;
using Telegram.Bot;
using System.IO;
using System.Collections.ObjectModel;

namespace OPIS_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        TelegramBotClient client;
        UserHistory userHistory;


        public MainWindow()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new TelegramBotClient("1942447443:AAE-Sxtv7hO6hWuF5si9Wkrsm82F5U5JCM0");
            client.OnMessage += Client_OnMessage;
            client.StartReceiving();

            userHistory = JsonConvert.DeserializeObject<UserHistory>(File.ReadAllText("log.json"));

            userList.ItemsSource = userHistory.history;

        }

        private void Client_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var msg = e.Message;

            this.Dispatcher.Invoke(() =>
            {
                userHistory.AddMessage(msg.Chat.Id, msg.Text, msg.Chat.FirstName);
                userList.Items.Refresh();
            });

            string result = String.Empty;

            if (msg.Text.ToLower().IndexOf("сегодня") != -1) // test
            {
                result = "Сегодня 15 градусов тепла";
            }
            else if(msg.Text.ToLower().IndexOf("завтра") != -1)
                   {
                result = "Сегодня 15 градусов тепла";
            } 
            
            switch (msg.Text)
            {
                case "/log": result = userHistory.GetLog(msg.Chat.Id); break;
                case "/id": result = userHistory.GetID(msg.Chat.Id); break;
                case "/help": result = "Получение истории: /log\nПолучение id: /id"; break;
                    /* default: result = "Такой команды нет"; break;*/
            }

            client.SendTextMessageAsync(msg.Chat.Id, result);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // XlsxConvert.
            File.WriteAllText("log.xlsx", JsonConvert.SerializeObject(userHistory));
        }
    }
}
