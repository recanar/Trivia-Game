using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject categoryPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject questionPanel;

    [SerializeField] private GameObject persistedCategoryButton;
    [SerializeField] private Button randomCategoryButton;
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
        randomCategoryButton.onClick.AddListener(() => RandomCategoryButtonClick());
        selectedCategoryText =persistedCategoryButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("selectedCategoryId"))
        {
            persistedCategoryButton.SetActive(true);
            persistedCategoryButton.GetComponent<Button>().onClick.AddListener(() =>PersistCategoryButton());
            selectedCategoryText.text = "Play "+ PlayerPrefs.GetString("selectedCategoryName");
        }
    }
    public void LoadCategoryPanel()//shows categories after clicked choose category button
    {
        menuPanel.SetActive(false);
        categoryPanel.SetActive(true);
    }
    public void LoadQuestions(int id)//Load questions after choose category
    {
        QuestionManager.instance.GenerateQuestions(id);
        menuPanel.SetActive(false);
        categoryPanel.SetActive(false);
        questionPanel.SetActive(true);
    }
    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        questionPanel.SetActive(false);
        if (PlayerPrefs.HasKey("selectedCategoryId"))
        {
            persistedCategoryButton.SetActive(true);
            persistedCategoryButton.GetComponent<Button>().onClick.AddListener(() => PersistCategoryButton());
            selectedCategoryText.text = PlayerPrefs.GetString("selectedCategoryName");
        }
    }
    public void PersistCategoryButton()
    {
        LoadQuestions(PlayerPrefs.GetInt("selectedCategoryId"));
    }
    public void RandomCategoryButtonClick()
    {
        LoadQuestions(-1);
    }
}
