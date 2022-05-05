using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace BusinessLogicLayer.Telegram
{
    public static class BaseBot
    {
        public static Dictionary<long, User> NameBase { get; set; } = new Dictionary<long, User>();
        public static Dictionary<long, List<string>> AnswerBase { get; set; } = new Dictionary<long, List<string>>();
    }
    public class AnswersUser //перенести
    {
        public string _idquestions { get; private set; }
        public string _answer { get; private set; }
        List<AnswersUser> answerlist { get; set; }

        public AnswersUser(string answer)
        {
            _answer = answer;
            answerlist = new List<AnswersUser>();
        }
    }
}
