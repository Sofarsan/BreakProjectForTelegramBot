namespace BusinessLogicLayer
{
    public class OngoingTest
    {
        public int _currentQuestion { get; set; } = 0;
        public Test test { get; set; }
        public List<List<string>> _answers = new List<List<string>>();

        public OngoingTest(Test test)
        {
            this.test = test;
        }
    }
}
