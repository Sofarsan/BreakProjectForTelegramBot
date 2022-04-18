using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class QuestionIWithoutOptionAnswer : AbstractQuestion
    {
        public QuestionIWithoutOptionAnswer(string type, string questionText)
        {
            _type = type;
            _questionText = questionText;
        }
    }
}
