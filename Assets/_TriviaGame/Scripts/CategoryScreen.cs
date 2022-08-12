using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CategoryScreen : MonoBehaviour
{
    public GameObject button;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<TextMeshProUGUI> buttonTexts;
    [SerializeField] private TMP_InputField inputText;
    private void Awake()
    {
        Category categories = ApiHelper.GetCategories();
        foreach (var categoryName in categories.trivia_categories)
        {
            var btnObject= Instantiate(button,transform);
            buttons.Add(btnObject.GetComponent<Button>());
            buttonTexts.Add(btnObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
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
    public void SearchCategory()
    {
        for (int i = 0; i < buttonTexts.Count; i++)
        {
            if (!(buttonTexts[i].text.Contains(inputText.text)))
            {
                buttons[i].gameObject.SetActive(false);
            }
            else
            {
                buttons[i].gameObject.SetActive(true);
            }
        }
    }
}
