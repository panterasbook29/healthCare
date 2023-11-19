using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject smokingQuestion;
    public GameObject showSmoking;
    public GameObject showSavings;
    public TMP_InputField savings_input;
   // public TMP_Text savings_txt;

    public void removeQuestion()
    {
        smokingQuestion.SetActive(false);
        showSmoking.SetActive(true);
    }

    public void show_Savings()
    {
        showSavings.SetActive(true);
       /* if( savings_input.text == "10")
        {
            savings_txt = ""
        }
        else if( savings_input.text == "20")
        {
            
        }*/
    }
}
