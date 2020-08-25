using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Boo.Lang;

public static class SaveSystem
{
    public static void SaveGames(GameData gameData, int slot)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameData" + slot + ".vgd";

        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    public static GameData LoadGame(int slot)
    {
        string path = Application.persistentDataPath + "/gameData" + slot + ".vgd";
        
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;

        } else
        {
            Debug.LogError("File di salvataggio non trovato in " + path);
            return null;
        }
    }

    public static GameData[] LoadGames()
    {
        GameData[] games = new GameData[6];

        for(int slot = 0; slot<6; slot++)
        {
            string path = Application.persistentDataPath + "gameData" + slot + ".vgd";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                GameData data = formatter.Deserialize(stream) as GameData;
                stream.Close();

                games[slot] = data;

            }
            else
            {
                GameData data = new GameData(0, 0, 0);
                games[slot] = data;
            }
        }

        return games;
    }
}
