using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToUser : MonoBehaviour
{
    public void goBackToUser()
    {
        SceneManager.LoadScene("UserTypeChoose");
    }
}
