using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    public static void SaveData(PlayerInfo playerInfo)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerinfo.bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerInfo data = new PlayerInfo(playerInfo);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerInfo LoadData()
    {
        string path = Application.persistentDataPath + "/playerinfo.bin";
        if (!File.Exists(path)) 
        { 
            Debug.LogError("File not found: " + path);
            return null;
        } else 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerInfo data = formatter.Deserialize(stream) as PlayerInfo;
            stream.Close();
            return data;
        }
    }

}
