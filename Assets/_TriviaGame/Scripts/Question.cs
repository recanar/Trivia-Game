using System.Collections.Generic;

[System.Serializable]
public class Question
{
    public int response_code;
    public List<Result> results;
    [System.Serializable]
    public class Result
    {
        public string category;
        public string type;
        public string difficulty;
        public string question;
        public string correct_answer;
        public List<string> incorrect_answers;
    }
}
