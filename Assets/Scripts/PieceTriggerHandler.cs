using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTriggerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject playerObj;
    private GameObject pdPatch;
    private bool OnStairCheck = false;

    void Start()
    {
        playerObj = GameObject.Find("Player");
        pdPatch = GameObject.Find("PlatformLibPdObject");
    }

    // Update is called once per frame
    void Update()
    {
        float currentPlayerY = playerObj.transform.position.y;

        if(OnStairCheck)
        {
            pdPatch.GetComponent<LibPdInstance>().SendFloat("Stair", currentPlayerY);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering object is the player or other valid object
        if (other.CompareTag("Player"))
        {
            // Print a unique message for each piece
            Debug.Log("trigger!!");
            OnStairCheck = true;
        }
    }
}
