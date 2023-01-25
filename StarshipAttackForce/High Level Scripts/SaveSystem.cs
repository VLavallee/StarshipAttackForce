using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer(GameSession gameSession)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        
        string path = Application.persistentDataPath + "/gameSession.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(gameSession);
        formatter.Serialize(stream, data);
        Debug.Log("Game Saved Sucessfully");
        stream.Close();
    }
    

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/gameSession.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            Debug.Log("Save file found and loaded!");
            return data;
            
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
