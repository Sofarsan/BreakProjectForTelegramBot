namespace BusinessLogicLayer
{
    public class OngoingTest
    {
        public int _currentQuestion { get; private set; } = 0;
        public Test test { get; set; }

        public OngoingTest(Test test)
        {
            this.test = test;
        }
    }
}
