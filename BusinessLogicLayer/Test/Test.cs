using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    [Serializable]
    public class Test
    {
        public string name { get; set; }
        public string _duration { get;  set; }
        public string _endTime { get;  set; }
        public List<Question> questionList { get; set; }

        public Test() { }
        public Test(string name)
        {
            this.name = name;
            questionList = new List<Question>();
        }
        public void AddQuestion(Question question)
        {
            questionList.Add(question);
        }
        public List<Question> GetListQuestion()
        {
            return questionList;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
