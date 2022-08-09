using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject categoryPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject questionPanel;

    [SerializeField] private GameObject selectedCategoryButton;
    private TextMeshProUGUI selectedCategoryText;
    public static GameManager instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        selectedCategoryText =selectedCategoryButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("selectedCategory"))
        {
            selectedCategoryButton.SetActive(true);
            selectedCategoryText.text = "Play Category " +((Categories)PlayerPrefs.GetInt("selectedCategory")).ToString();
        }
    }
    public void LoadCategoryPanel()//shows categories after clicked choose category button
    {
        menuPanel.SetActive(false);
        categoryPanel.SetActive(true);
    }
    public void LoadQuestions(Categories category)//Load questions after choose category
    {
        PlayerPrefs.SetInt("selectedCategory",(int)category);
        QuestionManager.instance.GenerateQuestions(category);
        menuPanel.SetActive(false);
        categoryPanel.SetActive(false);
        questionPanel.SetActive(true);
    }
    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        questionPanel.SetActive(false);
    }
}
