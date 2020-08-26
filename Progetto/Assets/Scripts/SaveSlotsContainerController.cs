using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotsContainerController : MonoBehaviour
{

    private static GameData[] saves;
    // Start is called before the first frame update
    void Start()
    {
        saves = SaveSystem.LoadGames();
    }

    public static GameData GetSave(int slotIndex)
    {
        return saves[slotIndex];
    }

    public static void UpdateSlot(int slotIndex, GameData gameData)
    {
        saves[slotIndex] = gameData;
        SaveSystem.SaveGame(gameData, slotIndex);
    }
}
