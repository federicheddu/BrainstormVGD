using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameSlotController : MonoBehaviour
{
    public int slotIndex;
    public GameObject emptySlotContainer;
    public GameObject loadGameContainer;
    public Text levelText, checkpointText, weaponText;

    // variabile privata Save dove si tiene la copia del salvataggio
    private GameData save;
    // Start is called before the first frame update
    void Start()
    {
        save = SaveSystem.LoadGame(slotIndex);

        if(save == null || save.level == 0)
        {
            emptySlotContainer.SetActive(true);
            loadGameContainer.SetActive(false);
        }
        else
        {
            emptySlotContainer.SetActive(false);
            loadGameContainer.SetActive(true);
            levelText.text = save.level.ToString();
            checkpointText.text = save.checkpoint.ToString();
            string[] weapons = new string[] { "Pistol", "Assault", "LMG", "No Weapon" };
            weaponText.text = weapons[save.weapon];
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        Debug.Log("Carico il salvataggio");


        GameSettings.SetLevel(save.level);
        GameSettings.SetCheckpoint(save.checkpoint);
        GameSettings.SetWeapon(save.weapon);

        StartMenu.LoadLevel(gameObject, save.level, save.checkpoint);

    }


}
