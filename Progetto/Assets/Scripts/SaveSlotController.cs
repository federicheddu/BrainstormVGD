using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotController : MonoBehaviour
{
    public GameObject emptyContainer;
    public GameObject saveDetails;
    public bool isEmpty;

    // Start is called before the first frame update
    void Start()
    {
        // modo per capire se il salvataggio c'è o meno e settare:
        // se il salvataggio c'è emptyContainer e isEmpty a false e saveDetails a true
    }

    public void Save()
    {
        // codice per recuperare i dati del salvataggio
        // codice per salvare
        Debug.Log("SALVANDOOOO");

        emptyContainer.SetActive(false);
        saveDetails.SetActive(true);
        isEmpty = false;
    }

    public void DeleteSave()
    {
        // codice per cancellare il salvataggio

        // ...... 
        emptyContainer.SetActive(true);
        saveDetails.SetActive(false);
        isEmpty = true;
    }
}