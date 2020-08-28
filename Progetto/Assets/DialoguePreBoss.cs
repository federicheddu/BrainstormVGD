using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePreBoss : MonoBehaviour
{
    public GameObject objToActivate; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        objToActivate.SetActive(true);
        this.enabled = false;
        Destroy(gameObject);
    }
}
