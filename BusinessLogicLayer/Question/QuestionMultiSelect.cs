using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class QuestionMultiSelect : AbstractQuestion
    {
        List<string> _optionAnswer;

        public QuestionMultiSelect(string questionText, List<string> optionAnswer)
        {
            _questionText = questionText;
            _optionAnswer = optionAnswer;
        }
    }
}
