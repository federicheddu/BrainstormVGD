using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static int N_SLOTS = 6;

    public static void SaveGame(GameData gameData, int slot)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameData" + slot + ".vgd";

        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        if (gameData == null)
            gameData = new GameData(0, 0, GunType.None);

        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    public static void SaveGames(GameData[] gameData)
    {
        for(int i=0; i<N_SLOTS; i++)
        {
            SaveGame(gameData[i], i);
        }
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

            if (data.level == 0)
                return null;

            return data;

        } else
        {
            Debug.Log("File di salvataggio non trovato in " + path);
            return null;
        }
    }

    public static GameData[] LoadGames()
    {
        GameData[] games = new GameData[N_SLOTS];

        for(int slot = 0; slot<N_SLOTS; slot++)
        {
            /*
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
                games[slot] = null;
            }*/
            games[slot] = LoadGame(slot);
        }

        return games;
    }
}
