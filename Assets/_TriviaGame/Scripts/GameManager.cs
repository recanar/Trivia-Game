using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject categoryPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private QuestionGenerator questionGenerator;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void LoadCategoryPanel()
    {
        menuPanel.SetActive(false);
        categoryPanel.SetActive(true);
    }
    public void LoadQuestions(Categories category)
    {
        questionGenerator.GenerateQuestion(category);
        menuPanel.SetActive(false);
        categoryPanel.SetActive(false);
        questionPanel.SetActive(true);
        //persist category
    }
    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        questionPanel.SetActive(false);
    }
}
