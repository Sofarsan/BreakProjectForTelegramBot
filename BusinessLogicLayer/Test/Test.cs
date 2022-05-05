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
        public Test GetClone()
        {
            List<Question> newQ = new List<Question>();
            foreach (Question question in questionList)
            {
                newQ.Add(question.Clone());
            }
            return new Test()
            {
                name = this.name,
                questionList = newQ,
                _duration = this._duration,
                _endTime = this._endTime
            };
        }
    }
}
