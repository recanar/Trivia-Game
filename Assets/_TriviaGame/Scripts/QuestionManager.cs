using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [System.Serializable]
    private class Answers
    {
        public List<Button> answerButtons;
        [HideInInspector] public List<TextMeshProUGUI> answerTexts;
    }
    [SerializeField] private Answers answers;

    public static QuestionManager instance;//singleton
    public QuestionScreenUI questionScreen;

    [SerializeField] private GameObject quitPanel;
    [SerializeField] private GameObject countdownPanel;

    private int currentQuestionIndex;
    private Question question;
    private Coroutine noAnswerRoutine;//stop if question got answered

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        foreach (var button in answers.answerButtons)
        {
            var btnText = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            answers.answerTexts.Add(btnText);//get buttons' texts and add to list
            button.onClick.AddListener(() => AnswerButtonClicked(button, btnText));//add click event to answer buttons
        }
    }
    [HideInInspector]
    public void GenerateQuestions(int id)
    {
        currentQuestionIndex = 0;//make current index 0 at start quiz
        question = ApiHelper.GetQuestions(id);//get data from api

        questionScreen.SendQuestionData(question,currentQuestionIndex);//send data to UI manager and question Ui
        noAnswerRoutine = StartCoroutine(NoAnswerCheckRoutine());//start question countdown after question appeared
    }
    private IEnumerator NoAnswerCheckRoutine()
    {
        yield return new WaitForSeconds(60);
        StartCoroutine(NextQuestionRoutine());
    }
    public void AnswerButtonClicked(Button pressedButton, TextMeshProUGUI btnPressedText)
    {
        StopCoroutine(noAnswerRoutine);
        foreach (var btn in answers.answerButtons)
        {
            btn.interactable = false;
        }
        if (btnPressedText.text == question.results[currentQuestionIndex].correct_answer.ToString())//compare chosen answer and correct answer
        {
            StartCoroutine(NextQuestionRoutine());
            countdownPanel.SetActive(true);
            questionScreen.CorrectAnswer();//correct answer UI 
        }
        else
        {
            StartCoroutine(NextQuestionRoutine());
            countdownPanel.SetActive(true);
            questionScreen.WrongAnswer(pressedButton,btnPressedText);//wrong answer UI
        }
    }
    private IEnumerator NextQuestionRoutine()
    {
        yield return new WaitForSeconds(1);
        questionScreen.NextQuestionCountdownStart();//UI
        yield return new WaitForSeconds(3);
        countdownPanel.SetActive(false);
        currentQuestionIndex++;
        questionScreen.NextQuestionCountdownEnd();//UI
        if (currentQuestionIndex == question.results.Count)
        {
            currentQuestionIndex = 0;
            GameManager.instance.BackToMenu();//game end back to menu
        }
        else
        {
            questionScreen.PrintQuestionScreen(currentQuestionIndex);//if quiz haven't finished go next question 
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
}