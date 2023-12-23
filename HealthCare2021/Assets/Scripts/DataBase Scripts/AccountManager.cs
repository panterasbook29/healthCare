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
    public string patientID;
}

public class AccountManager : MonoBehaviour
{
    public static string globalPatientID;

    public TMP_InputField nameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField notesInput;
    public TMP_Text name_TXT;
    public TMP_Text patientID_TXT;

    private void Start()
    {
        StartCoroutine(GetUserInfo());
    }

    IEnumerator GetUserInfo()
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

                // Now 'userInfo' contains information about the user, including the name and patientID
                name_TXT.text = "Name: " + userInfo.name;
                patientID_TXT.text = "Patient ID: " + userInfo.patientID;
                globalPatientID = userInfo.patientID;
                // Use the userInfo.name and userInfo.patientID variables in your Unity application
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
                StartCoroutine(GetUserInfo());
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
        form.AddField("password", passwordInput.text);
        form.AddField("phoneNumber", AuthManager.globalPhoneNumber);

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


    public void CallChangeNotes()
    {
        StartCoroutine(ChangeNotes());
    }

    IEnumerator ChangeNotes()
    {
        WWWForm form = new WWWForm();
        form.AddField("notes", notesInput.text);
        form.AddField("phoneNumber", AuthManager.globalPhoneNumber);

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.panterasbook.space/sqlconnect/notesChange.php", form))
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
