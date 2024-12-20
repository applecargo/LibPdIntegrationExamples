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
            object[] args = {"player1", 1};
            pdPatch.SendList("player", args);
            Debug.Log("1번 키 입력: pdPatch.SendList() 호출됨");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            object[] args = {"player2", 1};
            pdPatch.SendList("player", args);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            object[] args = {"player3", 1};
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
            Debug.Log("Stair!!" + uBuild.stairCount);
            object[] args = {"Stair", 1, uBuild.stairCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Fluid"))
        {
            Debug.Log("Fluid!!");
            object[] args = {"Fluid", 1, uBuild.fluidCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Rubble"))
        {
            Debug.Log("Rubble!!");
            object[] args = {"Rubble", 1, uBuild.rubbleCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Slope"))
        {
            Debug.Log("Slope!!");
            object[] args = {"Slope", 1, uBuild.slopeCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("SpeedArea"))
        {
            Debug.Log("SpeedArea!!");
            object[] args = {"SpeedArea", 1, uBuild.speedAreaCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Terrain!!");
            object[] args = {"Terrain", 1, uBuild.terrainCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Fog"))
        {
            Debug.Log("Fog!!");
            object[] args = {"Fog", 1, uBuild.fogCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Grass"))
        {
            Debug.Log("Grass!!");
            object[] args = {"Grass", 1, uBuild.grassCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Tree"))
        {
            Debug.Log("Tree!!");
            object[] args = {"Tree", 1, uBuild.treeCount};
            pdPatch.SendList(receiver, args);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            Debug.Log("Excit!!");
            object[] args = {"Stair", 0, uBuild.stairCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Fluid"))
        {
            object[] args = {"Fluid", 0, uBuild.fluidCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Rubble"))
        {
            object[] args = {"Rubble", 0, uBuild.rubbleCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Slope"))
        {
            object[] args = {"Slope", 0};
            pdPatch.SendList(receiver, args, uBuild.slopeCount);
        }
        if (other.CompareTag("SpeedArea"))
        {
            object[] args = {"SpeedArea", 0, uBuild.speedAreaCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Terrain"))
        {
            object[] args = {"Terrain", 0, uBuild.terrainCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Fog"))
        {
            Debug.Log("Fog!!");
            object[] args = {"Fog", 0, uBuild.fogCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Grass"))
        {
            Debug.Log("Grass!!");
            object[] args = {"Grass", 0, uBuild.grassCount};
            pdPatch.SendList(receiver, args);
        }
        if (other.CompareTag("Tree"))
        {
            Debug.Log("Tree!!");
            object[] args = {"Tree", 0};
            pdPatch.SendList(receiver, args, uBuild.treeCount);
        }
    }
    
}
