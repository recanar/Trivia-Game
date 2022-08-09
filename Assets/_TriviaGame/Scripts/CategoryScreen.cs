using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CategoryScreen : MonoBehaviour
{
    public GameObject button;
    private void Start()
    {
        Category categories = ApiHelper.GetCategories();
        foreach (var categoryName in categories.trivia_categories)
        {
            var btnObject= Instantiate(button,transform);
            btnObject.GetComponent<Button>().onClick.AddListener(() => CategoryButton(categoryName.id,categoryName.name));
            btnObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = categoryName.name;//give name of button to button text
        }
    }
    private void CategoryButton(int id,string name)
    {
        PlayerPrefs.SetInt("selectedCategoryId", id);
        PlayerPrefs.SetString("selectedCategoryName", name);
        GameManager.instance.LoadQuestions(id);
    }

}
