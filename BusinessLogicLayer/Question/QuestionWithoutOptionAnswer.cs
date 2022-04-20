using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class QuestionWithoutOptionAnswer : AbstractQuestion
    {
        public QuestionWithoutOptionAnswer(string type, string questionText)
        {
            _type = type;
            _questionText = questionText;
        }
    }
}
