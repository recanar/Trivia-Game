using System.Collections.Generic;

[System.Serializable]
public class Category
{
    public List<TriviaCategory> trivia_categories;
    [System.Serializable]
    public class TriviaCategory
    {
        public int id;
        public string name;
    }
}
