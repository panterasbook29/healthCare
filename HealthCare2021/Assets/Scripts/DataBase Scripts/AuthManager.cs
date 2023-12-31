using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    public static string globalPhoneNumber;


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

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.panterasbook.space/sqlconnect/registerPatient.php", form))
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
       StartCoroutine(LoginPatient());
    }

    IEnumerator LoginPatient()
    {
        WWWForm form = new WWWForm();
        form.AddField("phoneNumber", phoneNumberInput.text);
        form.AddField("password", passwordInput.text);

        UnityWebRequest www = UnityWebRequest.Post("http://www.panterasbook.space/sqlconnect/loginPatient.php", form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            if (www.downloadHandler.text[0] == '0')
            {
                DBManager.username = phoneNumberInput.text;
                Debug.Log(www.downloadHandler.text);
                globalPhoneNumber = phoneNumberInput.text;
                SceneManager.LoadScene("PatientMainApp");
                
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

    public void CallLogout()
    {
        globalPhoneNumber = "";
        SceneManager.LoadScene("MainMenuPatient");
    }


    public void VerifyInputs()
    {
         submitButtonRegister.interactable = (phoneNumberInput.text.Length == 10) ;
         submitButtonLogin.interactable = (phoneNumberInput.text.Length == 10);
    }
}
