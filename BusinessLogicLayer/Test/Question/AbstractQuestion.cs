using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class AbstractQuestion
    {
        public string _questionText;
        public QuestionType _type;
        public List<string> Answer { get; set; }
    }
}