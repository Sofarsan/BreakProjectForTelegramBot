

using BusinessLogicLayer;

Test test = new Test();

string questionText = "test 1";
List<string> optionAnswer = new List<string>() { "option1", "option2", "option3" };

AbstractQuestion question1 = new QuestionMultiSelect(questionText, optionAnswer);
AbstractQuestion question2 = new QuestionSingleSelect(questionText, optionAnswer);

test.AddQuestion(question1);
test.AddQuestion(question2);

//test.SendToTelegram()