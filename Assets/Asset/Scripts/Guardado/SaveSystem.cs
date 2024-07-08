using System.IO;
using UnityEngine;

public static class SaveSystem
{
    // Variables
    private static string fileName => "file";
    private static string fileExtension => "sav";

    //Static Methods
    /// <summary>
    /// Save data on a file.
    /// </summary>
    public static void Save(object data)
    {
        string jsonString = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + $"/{fileName}.{fileExtension}", jsonString);
    }

    /// <summary>
    /// Load the generic file as json and return in a value of generic type.
    /// </summary>
    public static T Load<T>() where T : new()
    {
        T data = new T();

        if (GetIfFileExists())
        {
            string raw = File.ReadAllText(Application.persistentDataPath + $"/{fileName}.{fileExtension}");
            JsonUtility.FromJsonOverwrite(raw, data);
        }

        return data;
    }

    /// <summary>
    /// Get if the file in the application persistent data path exists.
    /// </summary>
    /// <returns></returns>
    public static bool GetIfFileExists()
    {
        if (File.Exists(Application.persistentDataPath + $"/{fileName}.{fileExtension}")) return true;
        return false;
    }
}
