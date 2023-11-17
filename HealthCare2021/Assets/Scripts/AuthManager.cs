using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine.Networking;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField phoneNumberInput;
    public TMP_InputField passwordInput;

    public Button submitButton;

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

    public void VerifyInputs()
    {
        submitButton.interactable = (phoneNumberInput.text.Length >= 8 && passwordInput.text.Length >= 4) ;
    }
}
