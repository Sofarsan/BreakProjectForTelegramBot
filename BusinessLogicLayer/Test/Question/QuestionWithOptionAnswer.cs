using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class QuestionWithOptionAnswer : AbstractQuestion
    {

        // todo: create class OptionAnswer with isValid parameter and text
        // public List<OptionAnswer> _optionAnswer;
        public List<OptionAnswer> _optionAnswer;

        public QuestionWithOptionAnswer(string type, string questionText, List<OptionAnswer> optionAnswer)
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
