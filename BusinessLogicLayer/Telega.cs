using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace BusinessLogicLayer
{
    public class Telega
    {
        private TelegramBotClient _client;
        private Action<string> _onMessage;
        private List<long> _id;

        public Telega(string token, Action<string> OnMessege)
        {
            _client = new TelegramBotClient(token);
            _onMessage = OnMessege;
            _id = new List<long>();
        }

        public void Start()
        {
            _client.StartReceiving(HandleResive, HandleError);
        }

        public async void Send(string s)
        {
            foreach (var id in _id)
            {
                ReplyKeyboardMarkup replyKeyboard = new ReplyKeyboardMarkup(
                      new[]
                      {
                            new []
                            {
                                new KeyboardButton("Yes"),
                            },
                            new []
                            {
                                 new KeyboardButton("No"),                       
                            }
                      });
                await _client.SendTextMessageAsync(new ChatId(id), s, replyMarkup: replyKeyboard);
            }
        }

        private async Task HandleResive(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {   
            if (update.Message != null && update.Message.Text != null)
            {
                
                if (!_id.Contains(update.Message.Chat.Id))
                {
                    _id.Add(update.Message.Chat.Id);
                }
                string s = update.Message.Chat.FirstName + " "
                    + update.Message.Chat.LastName + " "
                    + update.Message.Text;
                _onMessage(s);
            }
        }
        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        { 
            return Task.CompletedTask;
        }
    }

}
