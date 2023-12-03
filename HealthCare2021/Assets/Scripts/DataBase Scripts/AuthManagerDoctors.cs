using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AuthManagerDoctors : MonoBehaviour
{
    public static string globalDoctorCNP;

    public TMP_InputField cnpInput;
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
        form.AddField("cnp", cnpInput.text);
        form.AddField("password", passwordInput.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.panterasbook.space/sqlconnect/registerDoctor.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Server response: " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("Doctor creation failed with error: " + www.error);
            }
        }
    }


    public void CallLogin()
    {
        StartCoroutine(LoginDoctor());
    }

    IEnumerator LoginDoctor()
    {
        WWWForm form = new WWWForm();
        form.AddField("cnp", cnpInput.text);
        form.AddField("password", passwordInput.text);

        UnityWebRequest www = UnityWebRequest.Post("http://www.panterasbook.space/sqlconnect/loginDoctor.php", form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            if (www.downloadHandler.text[0] == '0')
            {
                AuthManagerDoctors.globalDoctorCNP = cnpInput.text; // Set globalDoctorCNP here
                DBManager.username = cnpInput.text;
                Debug.Log(www.downloadHandler.text);
                SceneManager.LoadScene("DoctorMainApp");

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
        submitButtonRegister.interactable = (cnpInput.text.Length >= 6 && passwordInput.text.Length >= 4);
        submitButtonLogin.interactable = (cnpInput.text.Length >= 6 && passwordInput.text.Length >= 4);
    }
}
