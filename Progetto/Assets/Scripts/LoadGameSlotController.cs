using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameSlotController : MonoBehaviour
{
    public GameObject emptySlotContainer;
    public GameObject loadGameContainer;
    public int slotIndex;

    // variabile privata Save dove si tiene la copia del salvataggio

    // Start is called before the first frame update
    void Start()
    {
        SaveSlotsContainerController saves = GetComponentInParent<SaveSlotsContainerController>();
        if (true) // cambiare la condizione con "se la cella iesima dell'array non è null" -> se il salvataggio è presente
        {
            // save = saves[i]
            emptySlotContainer.SetActive(false);
            loadGameContainer.SetActive(true);
        } else
        {
            // saves = null
            emptySlotContainer.SetActive(true);
            loadGameContainer.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        Debug.Log("Carico il salvataggio");
        // if save != null
            // codice per caricare il salvataggio
                // GameSetting.SetLevel(save.level)
                // GameSetting.SetCheckpoint(save.checkpoint)
                // GameSetting.SetScore(save.score)
                // Caricamento della scena corrispondente
    }
}
