using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;

public class SaveSystem
{
    private static readonly string path = Application.persistentDataPath + "/profile.data";

    public static void SaveProfile (ProfileData profileData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new(path, FileMode.Create);

        SaveData data = new(profileData);
        Debug.Log("Data on Save : " + data);
        Debug.Log("Timer on Save : " + data.timer);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadProfile ()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log("Data on Load : " + data);
            Debug.Log("Timer on Load : " + data.timer);
            return data;
        } else
        {
            Debug.LogError("Save file not found in " + Application.persistentDataPath);
            return null;
        }
    }
}
