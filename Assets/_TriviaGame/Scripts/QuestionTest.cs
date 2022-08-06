using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class QuestionTest : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI categoryText;
    public TextMeshProUGUI[] answerTexts;

    public void Start()
    {
        Question question=ApiHelper.GetQuestions(Categories.Random_Questions);
        foreach (var item in question.results)
        {
            Debug.Log(item.category);
            print(item.question);
            Debug.Log(item.correct_answer);
        }
        questionText.text = StripHTML(question.results[0].question);//use method for remove html entity codes
        categoryText.text = question.results[0].category.ToString();
        answerTexts[0].text = question.results[0].incorrect_answers[0].ToString();
        answerTexts[1].text = question.results[0].incorrect_answers[1].ToString();
        answerTexts[2].text = question.results[0].incorrect_answers[2].ToString();
        answerTexts[3].text = question.results[0].correct_answer.ToString();
    }
    public string StripHTML(string input)
    {
        if (input == null)
        {
            return string.Empty;
        }
        return Regex.Replace(input, "&.*?;", String.Empty);

    }
}