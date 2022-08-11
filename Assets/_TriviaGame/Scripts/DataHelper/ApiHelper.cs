using UnityEngine;
using System.Net;
using System.IO;
using System.Linq;

public static class ApiHelper
{ 
    public static Question GetQuestions(int id)//fetch questions depend on category
    {
        HttpWebRequest request=null;
        if (id==-1)//random category id
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&type=multiple");
        }
        else
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&category=" + id + "&type=multiple");
        }
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<Question>(json);
    }
    public static Category GetCategories()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api_category.php");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        var categories= JsonUtility.FromJson<Category>(json);
        var sortedList= categories.trivia_categories.OrderBy(category=>category.name);//sort by name
        categories.trivia_categories = sortedList.ToList();
        return categories;
    }
}