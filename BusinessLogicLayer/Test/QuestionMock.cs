using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public  class QuestionMock
    {
        public static QuestionWithOptionAnswer getQuestion()
        {
            string _questionTextMoq = "2+2*2 = ?";
            String _typeMoq = "QuestionSingleSelect";
            List<OptionAnswer> _optionAnswerMoq = new List<OptionAnswer>() { new("1"), new("2"), new("3"), new("4") };
            return new QuestionWithOptionAnswer(_typeMoq, _questionTextMoq, _optionAnswerMoq);
        }

    }
}
