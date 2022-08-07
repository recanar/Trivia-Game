using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCategoryScript : MonoBehaviour
{
    [SerializeField] private Categories category;

    public void StartGame()
    {
        GameManager.instance.LoadQuestions(category);
    }
}
