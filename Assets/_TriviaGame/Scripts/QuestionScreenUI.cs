using System.Collections;
using System.Collections.Generic;
using System.Web;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScreenUI : MonoBehaviour
{
    private Question questionsData;

    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI questionNumberText;
    [SerializeField] private TextMeshProUGUI categoryText;
    [SerializeField] private GameObject clickBlockPanel;
    [SerializeField] private Button nextQuestionButton;
    private TextMeshProUGUI nextQuestionButtonText;
    public List<Button> answerButtons;
    public List<TextMeshProUGUI> answerTexts;

    public Color correctAnswerColor;
    public Color wrongAnswerColor;
    public Color buttonStartColor;
    public Color answerTextStartColor;

    private void Start()
    {
        for (int i = 0; i < answerButtons.Count; i++)
        {
            answerTexts.Add(answerButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>());
        }
        nextQuestionButtonText= nextQuestionButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SendQuestionData(Question questions,int questionIndex)
    {
        questionsData=questions;
        PrintQuestionScreen(questionIndex);
    }
    public void PrintQuestionScreen(int questionIndex)
    {
        PrintCategory(questionIndex);
        PrintQuestionNumber(questionIndex);
        PrintQuestion(questionIndex);
        PrintAnswerTexts(questionIndex);
    }
    public void CorrectAnswer()
    {
        clickBlockPanel.gameObject.SetActive(true);
        answerTexts[3].color = Color.white;
        ChangeButtonColor(answerButtons[3], correctAnswerColor);
        ChangeButtonColor(nextQuestionButton, correctAnswerColor);
        nextQuestionButtonText.color = Color.white;
        nextQuestionButtonText.text = "Correct";
    }
    public void WrongAnswer(Button pressedButton, TextMeshProUGUI btnPressedText)
    {
        clickBlockPanel.gameObject.SetActive(true);
        ChangeButtonColor(pressedButton, wrongAnswerColor);
        btnPressedText.color = Color.white;
        ChangeButtonColor(answerButtons[3], correctAnswerColor);
        answerTexts[3].color = Color.white;
        ChangeButtonColor(nextQuestionButton, wrongAnswerColor);
        nextQuestionButtonText.color = Color.white;
        nextQuestionButtonText.text = "Wrong";
    }
    public void NextQuestionCountdownStart()
    {
        ChangeButtonColor(nextQuestionButton, buttonStartColor);
        nextQuestionButtonText.color = Color.gray;
        nextQuestionButtonText.text = "Next question After:" + 3 + " Seconds";
    }
    public void NextQuestionCountdownEnd()
    {
        clickBlockPanel.SetActive(false);
        foreach (var button in answerButtons)//reset colors before load new question
        {
            ChangeButtonColor(button, buttonStartColor);
            button.interactable = true;
        }
        foreach (var answerText in answerTexts)
        {
            answerText.color = answerTextStartColor;
        }
    }
    private void PrintCategory(int questionIndex)
    {
        categoryText.text = questionsData.results[questionIndex].category;
    }
    private void PrintQuestionNumber(int questionIndex)
    {
        questionNumberText.text = (questionIndex + 1) + "/10";
    }
    private void PrintQuestion(int questionIndex)
    {
        questionText.text = HttpUtility.HtmlDecode(questionsData.results[questionIndex].question);//using striphtml method for remove html entity codes
    }
    private void PrintAnswerTexts(int questionIndex)
    {
        answerTexts[0].text = HttpUtility.HtmlDecode(questionsData.results[questionIndex].incorrect_answers[0]);
        answerTexts[1].text = HttpUtility.HtmlDecode(questionsData.results[questionIndex].incorrect_answers[1]);
        answerTexts[2].text = HttpUtility.HtmlDecode(questionsData.results[questionIndex].incorrect_answers[2]);
        answerTexts[3].text = HttpUtility.HtmlDecode(questionsData.results[questionIndex].correct_answer);
    }
    private void ChangeButtonColor(Button btn, Color color)
    {
        ColorBlock colorVar = btn.colors;
        colorVar.disabledColor = color;
        btn.colors = colorVar;
    }
}
