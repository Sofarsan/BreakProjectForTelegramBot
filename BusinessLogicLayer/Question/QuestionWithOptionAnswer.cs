﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class QuestionWithOptionAnswer : AbstractQuestion
    {
        
       public List<string> _optionAnswer;

        public QuestionWithOptionAnswer(string type, string questionText, List<string> optionAnswer)
        {
            _type = type;
            _questionText = questionText;
            _optionAnswer = optionAnswer;
        }
    }
}
