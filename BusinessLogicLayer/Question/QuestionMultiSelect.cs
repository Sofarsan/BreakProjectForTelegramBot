using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class QuestionMultiSelect : AbstractQuestion
    {
        List<string> OptionAnswer;

        public QuestionMultiSelect(string questionText)
        {
            _questionText = questionText;
            
        }
    }
}
