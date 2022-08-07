using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject categoryPanel;
    public GameObject menuPanel;
    public GameObject questionPanel;
    public void LoadCategoryPanel()
    {
        menuPanel.SetActive(false);
        categoryPanel.SetActive(true);
    }
    public void LoadQuestions()
    {
        categoryPanel.SetActive(false);
        questionPanel.SetActive(true);
    }
    public void BackToMenu()
    {
        menuPanel.SetActive(true);
        questionPanel.SetActive(false);
    }
}
