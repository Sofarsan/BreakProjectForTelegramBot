using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Test
    {
        public string _name { get; private set; }
        List<AbstractQuestion> questionList { get; set; }

        public Test(string name)
        {
            _name = name;
            questionList = new List<AbstractQuestion>();
        }
        public void AddQuestion(AbstractQuestion question)
        {
            questionList.Add(question);
        }
        public List<AbstractQuestion> GetListQuestion()
        {
          return questionList;
        }

    }
}
