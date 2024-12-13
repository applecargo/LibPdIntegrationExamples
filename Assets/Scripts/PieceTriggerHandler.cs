using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTriggerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public LibPdInstance pdPatch;
    private string receiver = "platform";

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
            object[] args = {"Stair", 1};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Fluid"))
        {
            Debug.Log("Fluid!!");
            object[] args = {"Fluid", 1};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Rubble"))
        {
            Debug.Log("Rubble!!");
            object[] args = {"Rubble", 1};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Slope"))
        {
            Debug.Log("Slope!!");
            object[] args = {"Slope", 1};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("SpeedArea"))
        {
            Debug.Log("SpeedArea!!");
            object[] args = {"SpeedArea", 1};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Terrain!!");
            object[] args = {"Terrain", 1};
            pdPatch.SendList(receiver, args);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            Debug.Log("Excit!!");
            object[] args = {"Stair", 0};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Fluid"))
        {
            object[] args = {"Fluid", 0};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Rubble"))
        {
            object[] args = {"Rubble", 0};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Slope"))
        {
            object[] args = {"Slope", 0};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("SpeedArea"))
        {
            object[] args = {"SpeedArea", 0};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Terrain"))
        {
            object[] args = {"Terrain", 0};
            pdPatch.SendList(receiver, args);
        }
    }
    
}
