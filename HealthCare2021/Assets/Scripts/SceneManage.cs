using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void toDoctor()
    {
        SceneManager.LoadScene("MainMenuDoctor");
    }
    public void toPatient()
    {
        SceneManager.LoadScene("MainMenuPatient");
    }
}
