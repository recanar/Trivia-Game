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
    public static Question GetQuestions(Categories category)
    {
        HttpWebRequest request=null;
        if (category==Categories.Random_Questions)
        {
            request = (HttpWebRequest)WebRequest.Create("https://opentdb.com/api.php?amount=10&type=multiple");
        }
        else if (category == Categories.General_Knowledge)
        {

        }
        else if (category == Categories.Books)
        {

        }
        else if (category == Categories.Film)
        {

        }
        else if (category == Categories.MusicalandTheatres)
        {

        }
        else if (category == Categories.Television)
        {

        }
        else if (category == Categories.Video_Games)
        {

        }
        else if (category == Categories.Board_Games)
        {

        }
        else if (category == Categories.Computers)
        {

        }
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<Question>(json);
    }
}
