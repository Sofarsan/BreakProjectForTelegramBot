using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    [Serializable]
    public class Question 
    {
        public string _questionText { get; set; }
        public QuestionType _type { get; set; }
        public List<OptionAnswer> _optionAnswer{ get; set; }
        public Question(){ }
        public Question(string type, string questionText, List<OptionAnswer> optionAnswer)
        {
            _type = (QuestionType)Enum.Parse(typeof(QuestionType), type, true);
            _questionText = questionText;
            _optionAnswer = optionAnswer;
        }
        public List<String> GetOptionAnswerStringList()
        {
            List<string> OptionAnswerList = new List<string>();
            foreach(var optionAnswer in _optionAnswer)
            {
                OptionAnswerList.Add(optionAnswer.Text);
            }
            return OptionAnswerList;
        }
    }
}
