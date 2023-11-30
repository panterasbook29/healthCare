using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UserInfo
{
    public string name;
}

public class AccountManager : MonoBehaviour
{

    public TMP_InputField nameInput;
    public TMP_InputField passwordInput;
    public TMP_Text nameTXT;

    private void Start()
    {
        StartCoroutine(GetUserName());
    }

    IEnumerator GetUserName()
    {
        WWWForm form = new WWWForm();
        form.AddField("phoneNumber", AuthManager.globalPhoneNumber);

        UnityWebRequest www = UnityWebRequest.Post("http://www.panterasbook.space/sqlconnect/getCurrentName.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            // Parse JSON data
            string jsonData = www.downloadHandler.text;

            // Check for errors or handle the data as needed
            if (jsonData.StartsWith("1:") || jsonData.StartsWith("2:") || jsonData.StartsWith("5:") || jsonData.StartsWith("6:"))
            {
                // Handle server-side errors
                Debug.LogError("Server error: " + jsonData);
            }
            else
            {
                // Process the JSON data as needed
                UserInfo userInfo = JsonUtility.FromJson<UserInfo>(jsonData);

                // Now 'userInfo' contains information about the user, including the name
                // Debug.Log($"User Name: {userInfo.name}");
                nameTXT.text = "Current Name: " + userInfo.name;
                // Use the userInfo.name variable in your Unity application
            }
        }
    }


    public void CallChangeName()
    {
        StartCoroutine(ChangeName());
    }

    IEnumerator ChangeName()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameInput.text);
        form.AddField("phoneNumber", AuthManager.globalPhoneNumber);

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.panterasbook.space/sqlconnect/nameChange.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Server response: " + www.downloadHandler.text);
                StartCoroutine(GetUserName());
            }
            else
            {
                Debug.Log("User creation failed with error: " + www.error);
            }
        }
    }


    public void CallChangePassword()
    {
        StartCoroutine(ChangePassword());
    }

    IEnumerator ChangePassword()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameInput.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.panterasbook.space/sqlconnect/passwordChange.php", form))
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
}
