using UnityEngine;
using UnityEngine.UI;

public class SaveSlotController : MonoBehaviour
{
    public int slotIntex;
    public GameObject emptyContainer;
    public GameObject saveDetails;
    public Text level, checkpoint, weapon;

    private bool isEmpty;
    private GameData save;

    // Start is called before the first frame update
    void Start()
    {
        save = SaveSystem.LoadGame(slotIntex);

        UpdateContainers();


    }

    public void Save()
    {
        // Creo nuovo salvataggio
        save = new GameData(
            GameSettings.GetLevel(),
            GameSettings.GetCheckpoint(),
            (int) GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PickWeapon>().getCurrentWeapon()
        );
        // Aggiorno lo slot
        SaveSlotsContainerController.UpdateSlot(slotIntex, save);
        // Aggiorno i container della UI
        UpdateContainers();
    }

    public void DeleteSave()
    {
        save = null;
        SaveSlotsContainerController.UpdateSlot(slotIntex, save);
        UpdateContainers();
    }

    private void UpdateContainers()
    {
        isEmpty = save == null;
        emptyContainer.SetActive(isEmpty);
        saveDetails.SetActive(!isEmpty);

        if (!isEmpty)
        {
            level.text = save.level.ToString();
            checkpoint.text = save.checkpoint.ToString();
            string[] weapons = new string[] { "Pistol", "Assault", "LMG", "No Weapon"};
            weapon.text = weapons[save.weapon];
        }
    }
}