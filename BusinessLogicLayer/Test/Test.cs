using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Test
    {
        List<AbstractQuestion> questionList { get; set; }

        public Test()
        {
            questionList = new List<AbstractQuestion>();
        }
        public void AddQuestion(AbstractQuestion question)
        {
            questionList.Add(question);
        }

    }
}
