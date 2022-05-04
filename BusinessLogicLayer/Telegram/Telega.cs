using BusinessLogicLayer.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;



namespace BusinessLogicLayer
{
    public class Telega
    {
        private TelegramBotClient _client;
        private Action<string> _onMessage;
        //public List<User> UserList { get; private set; } //?

        public Telega(string token, Action<string> OnMessege)
        {
            _client = new TelegramBotClient(token);
            _onMessage = OnMessege;
        }

        public void Start()
        {
            _client.StartReceiving(HandleReceive, HandleError);
        }

        public async void StartingButton(long id, string firstName, string lastName)
        { 
            if (!BaseBot.NameBase.ContainsKey(id))
            {
                BaseBot.NameBase.Add(id, new List<string> { firstName, lastName });
                BaseSerialize.SaveUserDictionary(BaseBot.NameBase);

                await _client.SendTextMessageAsync(new ChatId(id), "Hello");
            }
        }

        public async void Send(string s)
        {
            //foreach (User user in UserList)
            //{
            //    ReplyKeyboardMarkup replyKeyboard = new ReplyKeyboardMarkup(
            //          new[]
            //          {
            //                new []
            //                {
            //                    new KeyboardButton("Yes"),
            //                },
            //                new []
            //                {
            //                     new KeyboardButton("No"),
            //                }
            //          });
            //    await _client.SendTextMessageAsync(new ChatId(user.Id), s, replyMarkup: replyKeyboard);
            //}
        }

        public async void SendQuestion(Question question)
        {
            //List<String> oA = question.GetOptionAnswerStringList();
            //List<KeyboardButton> optionAnswers = new List<KeyboardButton>();


            //foreach (string str in oA)
            //{
            //    optionAnswers.Add(str);
            //}

            //foreach (User user in UserList)
            //{

            //    ReplyKeyboardMarkup replyKeyboard = new ReplyKeyboardMarkup(optionAnswers);

            //    await _client.SendTextMessageAsync(new ChatId(user.Id), question._questionText, replyMarkup: replyKeyboard);
            //}
        }

        private async Task HandleReceive(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message != null && update.Message.Text != null)
            {
                List<string> users = new List<string>();

                // todo: append to the list of users
                // if message = "/register" => append to the list of users
                StartingButton(update.Message.Chat.Id, update.Message.Chat.LastName, update.Message.Chat.FirstName);

                //if (UserList.Where(u => u.Id == update.Message.Chat.Id).ToArray().Length == 0)
                //{
                //    User user = new User(update.Message.Chat.LastName, update.Message.Chat.FirstName, update.Message.Chat.Id);
                //    UserList.Add(user);
                //}
                //string s = update.Message.Chat.FirstName + " "
                //    + update.Message.Chat.LastName + " "
                //    + update.Message.Text;
                //_onMessage(s);
            }
        }


        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }



        public async void AskConfirmation(User user)

        {
            string name = user.ongoingTest.test.name;
            int count = user.ongoingTest.test.GetListQuestion().Count;
            var duration = user.ongoingTest.test._duration;
            var endTime = user.ongoingTest.test._endTime;

            string message = $"Имя теста : {name} \n Количество вопросов: {count} \n Время прохождения: {duration}";

            ReplyKeyboardMarkup replyKeyboard = new ReplyKeyboardMarkup(

                  new[]

                  {
                       new KeyboardButton("Start "),

                  });

            await _client.SendTextMessageAsync(new ChatId(user.Id), message, replyMarkup: replyKeyboard);

        }
    }


}
