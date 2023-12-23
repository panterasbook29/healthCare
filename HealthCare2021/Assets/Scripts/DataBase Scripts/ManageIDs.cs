using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class YourData
{
    public int ID;
}


public class ManageIDs : MonoBehaviour
{
    public TMP_InputField idInputField;
    public Button addButton;
    public Button saveButton;

    private List<YourData> dataList = new List<YourData>();

    private void Start()
    {
        // Attach the button click events
        addButton.onClick.AddListener(AddID);
        saveButton.onClick.AddListener(SaveIDs);
    }

    private void AddID()
    {
        // Create an instance of your data class
        YourData data = new YourData();

        // Parse the ID from the input field
        if (int.TryParse(idInputField.text, out int id))
        {
            // Set the ID
            data.ID = id;

            // Add data to the list
            dataList.Add(data);

            Debug.Log("ID " + id + " added to the list.");
        }
        else
        {
            Debug.LogError("Invalid ID. Please enter a valid integer.");
        }
    }

    private void SaveIDs()
    {
        // Convert list to JSON
        string json = JsonHelper.ToJson(dataList.ToArray());

        // Save JSON to a file (you can customize the file path)
        string filePath = "path/to/your/file.json";
        System.IO.File.WriteAllText(filePath, json);

        Debug.Log("All IDs saved to file: " + filePath);
    }
}



public static class JsonHelper
{
    // Custom JsonHelper class to serialize/deserialize arrays
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
