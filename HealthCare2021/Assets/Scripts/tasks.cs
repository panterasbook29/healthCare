using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tasks : MonoBehaviour
{
    public GameObject task1;
    public GameObject task2;
    public GameObject task3;

    public void refreshTask()
    {
        if (task1.activeSelf == false)
        {
            task1.SetActive(true);
        }
        if (task2.activeSelf == false)
        {
            task2.SetActive(true);
        }
        if (task3.activeSelf == false)
        {
            task3.SetActive(true);
        }
    }
}
