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
            _type = type;
            _questionText = questionText;
            _optionAnswer = optionAnswer;
        }
    }
}
