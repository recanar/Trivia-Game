using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

public enum Categories
{
    Random_Questions,
    General_Knowledge,
    Books,
    Film,
    MusicalandTheatres,
    Television,
    Video_Games,
    Board_Games,
    Computers
}
public static class ApiHelper
{ 
    public static Question GetQuestions(Categories category)//fetch questions depend on category
    {
        HttpWebRequest request=null;
        if (category==Categories.Random_Questions)
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&type=multiple");
        }
        else if (category == Categories.General_Knowledge)
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&category=9&type=multiple");
        }
        else if (category == Categories.Books)
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&category=10&type=multiple");
        }
        else if (category == Categories.Film)
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&category=11&type=multiple");
        }
        else if (category == Categories.MusicalandTheatres)
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&category=12&type=multiple");
        }
        else if (category == Categories.Television)
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&category=13&type=multiple");
        }
        else if (category == Categories.Video_Games)
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&category=14&type=multiple");
        }
        else if (category == Categories.Board_Games)
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&category=15&type=multiple");
        }
        else if (category == Categories.Computers)
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&category=16&type=multiple");
        }
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<Question>(json);
    }
}
