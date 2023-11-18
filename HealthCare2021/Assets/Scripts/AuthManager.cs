using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField phoneNumberInput;
    public TMP_InputField passwordInput;

    public Button submitButtonRegister;
    public Button submitButtonLogin;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("phoneNumber", phoneNumberInput.text);
        form.AddField("password", passwordInput.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Server response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("User creation failed with error: " + www.error);
            }
        }
    }


    public void CallLogin()
    {
       StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("phoneNumber", phoneNumberInput.text);
        form.AddField("password", passwordInput.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            if (www.downloadHandler.text[0] == '0')
            {
                DBManager.username = phoneNumberInput.text;
                Debug.Log(www.downloadHandler.text);
                SceneManager.LoadScene("MainApp");
                
            }
            else
            {
                Debug.Log("User Login failed with error #" + www.downloadHandler.text);
            }
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

    public void VerifyInputs()
    {
        submitButtonRegister.interactable = (phoneNumberInput.text.Length >= 8 && passwordInput.text.Length >= 4) ;
        submitButtonLogin.interactable = (phoneNumberInput.text.Length >= 8 && passwordInput.text.Length >= 4);
    }
}
