using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTriggerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public LibPdInstance pdPatch;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float posY = gameObject.transform.position.y;
        pdPatch.SendFloat("posY", posY);
        // Debug.Log(posY);

        // pdPatch.SendFloat("Stair", currentPlayerY);
        // float currentPlayerY = playerObj.transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            Debug.Log("Stair!!");
            pdPatch.SendSymbol("platform", "Stair");
        }
        if (other.CompareTag("Fluid"))
        {
            Debug.Log("Fluid!!");
            pdPatch.SendSymbol("platform", "Fluid");
        }
        if (other.CompareTag("Rubble"))
        {
            Debug.Log("Rubble!!");
            pdPatch.SendSymbol("platform", "Rubble");
        }
        if (other.CompareTag("Slope"))
        {
            Debug.Log("Slope!!");
            pdPatch.SendSymbol("platform", "Slope");
        }
        if (other.CompareTag("SpeedArea"))
        {
            Debug.Log("SpeedArea!!");
            pdPatch.SendSymbol("platform", "SpeedArea");
        }
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Terrain!!");
            pdPatch.SendSymbol("platform", "Terrain");
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
    
}
