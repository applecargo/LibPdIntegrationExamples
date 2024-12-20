using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTriggerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public LibPdInstance pdPatch;
    public uBuildManager uBuild;
    private string receiver = "platform";

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //player switch
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            object[] args = {"playersource", 1};
            pdPatch.SendList("player", args);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            object[] args = {"playersource", 2};
            pdPatch.SendList("player", args);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            object[] args = {"playersource", 3};
            pdPatch.SendList("player", args);
        }


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
            Debug.Log("Enter Stair!! [" + uBuild.stairCount + "]");
            object[] args = {"Stair", 1, uBuild.stairCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Fluid"))
        {
            Debug.Log("Enter Fluid!!");
            object[] args = {"Fluid", 1, uBuild.fluidCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Rubble"))
        {
            Debug.Log("Enter Rubble!!");
            object[] args = {"Rubble", 1, uBuild.rubbleCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Slope"))
        {
            Debug.Log("Enter Slope!!");
            object[] args = {"Slope", 1, uBuild.slopeCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("SpeedArea"))
        {
            Debug.Log("Enter SpeedArea!!");
            object[] args = {"SpeedArea", 1, uBuild.speedAreaCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Enter Terrain!!");
            object[] args = {"Terrain", 1, uBuild.terrainCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Fog"))
        {
            Debug.Log("Enter Fog!!");
            object[] args = {"Fog", 1, uBuild.fogCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Grass"))
        {
            Debug.Log("Enter Grass!!");
            object[] args = {"Grass", 1, uBuild.grassCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Tree"))
        {
            Debug.Log("Enter Tree!!");
            object[] args = {"Tree", 1, uBuild.treeCount};
            pdPatch.SendList(receiver, args);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            Debug.Log("Exit Stair!!");
            object[] args = {"Stair", 0, uBuild.stairCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Fluid"))
        {
            Debug.Log("Exit Fluid!!");
            object[] args = {"Fluid", 0, uBuild.fluidCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Rubble"))
        {
            Debug.Log("Exit Rubble!!");
            object[] args = {"Rubble", 0, uBuild.rubbleCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Slope"))
        {
            Debug.Log("Exit Slope!!");
            object[] args = {"Slope", 0, uBuild.slopeCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("SpeedArea"))
        {
            Debug.Log("Exit SpeedArea!!");
            object[] args = {"SpeedArea", 0, uBuild.speedAreaCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Exit Terrain!!");
            object[] args = {"Terrain", 0, uBuild.terrainCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Fog"))
        {
            Debug.Log("Exit Fog!!");
            object[] args = {"Fog", 0, uBuild.fogCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Grass"))
        {
            Debug.Log("Exit Grass!!");
            object[] args = {"Grass", 0, uBuild.grassCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Tree"))
        {
            Debug.Log("Exit Tree!!");
            object[] args = {"Tree", 0};
            pdPatch.SendList(receiver, args, uBuild.treeCount);
        }
    }
    
}
