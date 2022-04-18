using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class QuestionIWithOptionAnswer : AbstractQuestion
    {
        
        List<string> _optionAnswer;

        public QuestionIWithOptionAnswer(string type, string questionText,List<string> optionAnswer)
        {
            _type = type;
            _questionText = questionText;
            _optionAnswer = optionAnswer;
        }
    }
}
