using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    private enum AnswerStates
    {
        NoAnswer,
        WrongAnswer,
        CorrectAnswer
    }
    [System.Serializable]
    private class Answers 
    {
        public List<Button> answerButtons;
        [HideInInspector] public List<TextMeshProUGUI> answerTexts;
        public Color correctAnswerColor;
        public Color wrongAnswerColor;
        public Color buttonStartColor;
        public Color answerTextStartColor;
    }

    public static QuestionManager instance;

    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI categoryText;
    [SerializeField] private TextMeshProUGUI questionNumberText;
    [SerializeField] private GameObject quitPanel;

    [SerializeField] private GameObject showAnswerPanel;

    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private Answers answers;


    private int currentQuestionIndex;
    private Question question;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        
    }
    private void Start()
    {
        foreach (var button in answers.answerButtons)
        {
            var btnText = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            answers.answerTexts.Add(btnText);//get buttons' texts
            button.onClick.AddListener(() => AnswerButtonClicked(button,btnText));//add click event to answer buttons
        }
    }
    [HideInInspector]public void GenerateQuestions(int id)
    {
        currentQuestionIndex = 0;//make current index 0 at start quiz
        question=ApiHelper.GetQuestions(id);
        ShowNextQuestion();
    }
    private void ShowNextQuestion()
    {
        questionNumberText.text = (currentQuestionIndex+1)+"/10";
        questionText.text = StripHTML(question.results[currentQuestionIndex].question);//using striphtml method for remove html entity codes
        categoryText.text = question.results[currentQuestionIndex].category.ToString();
        answers.answerTexts[0].text = question.results[currentQuestionIndex].incorrect_answers[0].ToString();
        answers.answerTexts[1].text = question.results[currentQuestionIndex].incorrect_answers[1].ToString();
        answers.answerTexts[2].text = question.results[currentQuestionIndex].incorrect_answers[2].ToString();
        answers.answerTexts[3].text = question.results[currentQuestionIndex].correct_answer.ToString();
        
    }
    public void AnswerButtonClicked(Button pressedButton ,TextMeshProUGUI btnPressedText)
    {
        showAnswerPanel.SetActive(true);
        pressedButton.interactable = false;//deselect button
        pressedButton.interactable = true;
        if (btnPressedText.text == question.results[currentQuestionIndex].correct_answer.ToString())//compare chosen answer and correct answer
        {
            ChangeButtonColor(pressedButton, answers.correctAnswerColor);
            btnPressedText.color = Color.white;
            StartCoroutine(NextQuestionRoutine(AnswerStates.CorrectAnswer));
        }
        else
        {
            ChangeButtonColor(pressedButton,answers.wrongAnswerColor);
            btnPressedText.color = Color.white;
            StartCoroutine(NextQuestionRoutine(AnswerStates.WrongAnswer));
            for (int i = 0; i < answers.answerTexts.Count; i++)
            {
                if (answers.answerTexts[i].text == question.results[currentQuestionIndex].correct_answer.ToString())
                {
                    ChangeButtonColor(answers.answerButtons[i], answers.correctAnswerColor);
                    answers.answerTexts[i].color = Color.white;
                }
            }
        }
    }
    public void QuitButtonClicked()
    {
        quitPanel.SetActive(true);
    }
    public void CloseQuitPanel()
    {
        quitPanel.SetActive(false);
    }
    private IEnumerator NextQuestionRoutine(AnswerStates answerState)
    {
        if (answerState==AnswerStates.WrongAnswer)
        {
        }
        yield return new WaitForSeconds(1);
        countdownPanel.SetActive(true);
        countdownText.text = "Next question After:\n"+3+" Seconds";
        yield return new WaitForSeconds(1);
        countdownText.text = "Next question After:\n" + 2 + " Seconds";
        yield return new WaitForSeconds(1);
        countdownText.text = "Next question After:\n" + 1 + " Seconds";
        yield return new WaitForSeconds(1);
        showAnswerPanel.SetActive(false);
        countdownPanel.SetActive(false);
        currentQuestionIndex++;
        foreach(var button in answers.answerButtons)//reset colors before load new question
        {
            ChangeButtonColor(button, answers.buttonStartColor);
        }
        foreach (var answerText in answers.answerTexts)
        {
            answerText.color = answers.answerTextStartColor;
        }

        if (currentQuestionIndex == question.results.Count)
        {
            currentQuestionIndex = 0;
            GameManager.instance.BackToMenu();//game end back to menu
        }
        else
        {
            ShowNextQuestion();
        }
    }
    public string StripHTML(string input)
    {
        if (input == null)
        {
            return string.Empty;
        }
        return Regex.Replace(input, "&.*?;", String.Empty);

    }
    private void ChangeButtonColor(Button btn, Color color)
    {
        ColorBlock colorVar = btn.colors;
        colorVar.normalColor = color;
        btn.colors = colorVar;
    }
}