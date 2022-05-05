using BusinessLogicLayer.Telegram;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Xml;
using System.IO;


namespace BusinessLogicLayer
{
    public class Telega
    {
        private TelegramBotClient _client;
        private Action<string> _onMessage;
        public List<User> UserList { get; private set; } //?
        public ObservableCollection<UserGroup> groups;

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
            foreach (UserGroup group in groups)
            {
                foreach (User user in group.Users)
                {
                    SendQuestion(question, user);
                }
            }
        }

        public async void SendQuestion(Question question, User user)
        {


            long chatId = 0;
            foreach (long id in BaseBot.NameBase.Keys)
            {
                var fio = BaseBot.NameBase[id];
                if (fio[1] == user.FirstName && fio[0] == user.LastName)
                    chatId = id;

            }
            if (chatId != 0)
            {
                switch (question._type)
                {
                    case QuestionType.QuestionInput:
                        {
                            await _client.SendTextMessageAsync(new ChatId(chatId), question._questionText, replyMarkup: new ReplyKeyboardRemove());
                        }
                        break;
                    case QuestionType.QuestionSingleSelect:
                        {
                            List<String> oA = question.GetOptionAnswerStringList();
                            List<KeyboardButton> optionAnswers = new List<KeyboardButton>();

                            foreach (string str in oA)
                            {
                                optionAnswers.Add(str);
                            }
                            ReplyKeyboardMarkup replyKeyboard = new ReplyKeyboardMarkup(optionAnswers);
                            await _client.SendTextMessageAsync(new ChatId(chatId), question._questionText, replyMarkup: replyKeyboard);
                        }
                        break;
                    case QuestionType.QuestionYesNo:
                        {
                            List<KeyboardButton> optionAnswers = new List<KeyboardButton>();
                            optionAnswers.Add("Yes");
                            optionAnswers.Add("No");
                            ReplyKeyboardMarkup replyKeyboard = new ReplyKeyboardMarkup(optionAnswers);
                            await _client.SendTextMessageAsync(new ChatId(chatId), question._questionText, replyMarkup: replyKeyboard);
                        }
                        break;
                    case QuestionType.QuestionSort:
                        {
                            List<String> oA = question.GetOptionAnswerStringList();
                            List<KeyboardButton> optionAnswers = new List<KeyboardButton>();

                            foreach (string str in oA)
                            {
                                optionAnswers.Add(str);
                            }
                            ReplyKeyboardMarkup replyKeyboard = new ReplyKeyboardMarkup(optionAnswers);
                            await _client.SendTextMessageAsync(new ChatId(chatId), question._questionText, replyMarkup: replyKeyboard);
                        }
                        break;
                    case QuestionType.QuestionMultiSelect:
                        {
                            List<String> oA = question.GetOptionAnswerStringList();
                            List<KeyboardButton> optionAnswers = new List<KeyboardButton>();

                            foreach (string str in oA)
                            {
                                optionAnswers.Add(str);
                            }
                            optionAnswers.Add("Ready");
                            ReplyKeyboardMarkup replyKeyboard = new ReplyKeyboardMarkup(optionAnswers);
                            await _client.SendTextMessageAsync(new ChatId(chatId), question._questionText, replyMarkup: replyKeyboard);
                        }
                        break;
                    default:
                        break;
                }

            }
        }
        public async void SendNextQuestion(User user)
        {
            user.ongoingTest._currentQuestion += 1;
            if (user.ongoingTest._currentQuestion >= user.ongoingTest.test.questionList.Count)
            {
                saveTestResult(user);
                await _client.SendTextMessageAsync(new ChatId(user.Id), "Test finished!", replyMarkup: new ReplyKeyboardRemove());
                return;
            }
            SendQuestion(user.ongoingTest.test.questionList[user.ongoingTest._currentQuestion], user);
        }

        public void saveTestResult(User user)
        {
            //save answers to json
            XmlDocument doc = new XmlDocument();
            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                // Do this directly 
                writer.WriteStartDocument();
                writer.WriteStartElement("test");
                for(int i = 0; i < user.ongoingTest.test.questionList.Count; i++)
                {
                    writer.WriteStartElement("question" + i.ToString());

                    writer.WriteStartElement("text");
                    writer.WriteValue(user.ongoingTest.test.questionList[i]._questionText);
                    writer.WriteEndElement();

                    writer.WriteStartElement("answer");
                    writer.WriteValue(string.Join(";", user.ongoingTest._answers[i]));
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            doc.Save(user.FirstName + "_" + user.LastName + "_" + user.ongoingTest.test.name + ".xml");
        }

        private async Task HandleReceive(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                if (update.Message != null && update.Message.Text != null)
                {
                    List<string> users = new List<string>();

                    // todo: append to the list of users
                    // if message = "/register" => append to the list of users
                    StartingButton(update.Message.Chat.Id, update.Message.Chat.FirstName, update.Message.Chat.LastName);

                //if (UserList.Where(u => u.Id == update.Message.Chat.Id).ToArray().Length == 0)
                //{
                //    User user = new User(update.Message.Chat.LastName, update.Message.Chat.FirstName, update.Message.Chat.Id);
                //    UserList.Add(user);
                //}
                //string s = update.Message.Chat.FirstName + " "
                //    + update.Message.Chat.LastName + " "
                //    + update.Message.Text;
                //_onMessage(s);
                foreach(UserGroup group in groups)
                {
                    foreach(User user in group.Users)
                    {
                        if (user.Id == update.Message.Chat.Id)
                        {
                            if (user.ongoingTest == null)
                                return;
                            if(update.Message.Text == "Start")
                            {
                                if (user.ongoingTest._currentQuestion > user.ongoingTest.test.questionList.Count)
                                {
                                    await _client.SendTextMessageAsync(new ChatId(user.Id), "Test finished!", replyMarkup: new ReplyKeyboardRemove());
                                    saveTestResult(user);
                                    return;
                                }
                                SendQuestion(user.ongoingTest.test.questionList[user.ongoingTest._currentQuestion], user);
                                return;
                            }
                            Question question = user.ongoingTest.test.questionList[user.ongoingTest._currentQuestion];
                            if (user.ongoingTest._answers.Count <= user.ongoingTest._currentQuestion)
                            {
                                user.ongoingTest._answers.Add(new List<string>());
                                
                            }
                            user.ongoingTest._answers[user.ongoingTest._answers.Count - 1].Add(update.Message.Text);
                            switch (question._type)
                            {
                                case QuestionType.QuestionInput:
                                    {
                                        SendNextQuestion(user);
                                    }
                                    break;
                                case QuestionType.QuestionSingleSelect:
                                    {
                                        SendNextQuestion(user);
                                    }
                                    break;
                                case QuestionType.QuestionYesNo:
                                    {
                                        SendNextQuestion(user);
                                    }
                                    break;
                                case QuestionType.QuestionSort:
                                    {
                                        if (user.ongoingTest._answers.Last().Count == question._optionAnswer.Count)
                                            SendNextQuestion(user);
                                    }
                                    break;
                                case QuestionType.QuestionMultiSelect:
                                    {
                                        if (update.Message.Text == "Ready")
                                        {
                                            user.ongoingTest._answers[user.ongoingTest._answers.Count - 1].RemoveAt(user.ongoingTest._answers[user.ongoingTest._answers.Count - 1].Count - 1);
                                            SendNextQuestion(user);
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }    
                return;        
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

