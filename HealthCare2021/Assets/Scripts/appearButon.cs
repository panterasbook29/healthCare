using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class appearButon : MonoBehaviour
{
    string[] tasks = new string[5];

    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public GameObject buttonMain;

    float buttonHeight = 100f;
    float spacing = 200f;

    public void apareButon()
    {
        if(buttonMain.active == false)
        {
            buttonMain.SetActive(true);
        }
        else
            createButton();
    }
    void createButton()
    {
        for (int i = 0; i < tasks.Length; i++)
        {
                GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
                RectTransform newButtonRectTransform = newButton.GetComponent<RectTransform>();


                float yPos = i - (buttonHeight + spacing);
                newButtonRectTransform.anchoredPosition = new Vector2(0f, yPos);
                buttonHeight += 100;
                spacing += 200;
                Text buttonText = newButton.GetComponentInChildren<Text>();

                buttonText.text = tasks[i];
        }
    }
}

