using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class PieceTriggerHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public LibPdInstance pdPatch;
    public uBuildManager uBuild;
    public TimelineGenerator timelineGenerator;
    private string receiver = "platform";

    // 여러 개의 RawImage 프리팹을 저장할 리스트
    public List<GameObject> rawImages = new List<GameObject>(9);
    // RawImage가 생성될 위치에 있는 게임오브젝트들 (씬에 배치되어 있어야 함)
    // public List<Transform> spawnLocations = new List<Transform>(9);

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
            Debug.Log("Stair!!" + uBuild.stairCount);
            object[] args = {"Stair", 1, uBuild.stairCount};
            pdPatch.SendList(receiver, args);
            // Debug.Log(uBuild.pieces[2].image);

            // RawImage someRawImage = uBuild.pieces[2].image;
            // AddRawImage(someRawImage);
            timelineGenerator.AddRawImage(rawImages[2]);
            // Debug.Log(rawImages[1]);
            // Debug.Log(rawImages[1].name);
        }
        if (other.CompareTag("Fluid"))
        {
            Debug.Log("Fluid!!");
            object[] args = {"Fluid", 1, uBuild.fluidCount};
            pdPatch.SendList(receiver, args);
            timelineGenerator.AddRawImage(rawImages[1]);

        }
        if (other.CompareTag("Rubble"))
        {
            Debug.Log("Rubble!!");
            object[] args = {"Rubble", 1, uBuild.rubbleCount};
            pdPatch.SendList(receiver, args);
            timelineGenerator.AddRawImage(rawImages[5]);

        }
        if (other.CompareTag("Slope"))
        {
            Debug.Log("Slope!!");
            object[] args = {"Slope", 1, uBuild.slopeCount};
            pdPatch.SendList(receiver, args);
            timelineGenerator.AddRawImage(rawImages[0]);

        }
        if (other.CompareTag("SpeedArea"))
        {
            Debug.Log("SpeedArea!!");
            object[] args = {"SpeedArea", 1, uBuild.speedAreaCount};
            pdPatch.SendList(receiver, args);
            timelineGenerator.AddRawImage(rawImages[3]);

        }
        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Terrain!!");
            object[] args = {"Terrain", 1, uBuild.terrainCount};
            pdPatch.SendList(receiver, args);
            timelineGenerator.AddRawImage(rawImages[4]);
        }
        if (other.CompareTag("Fog"))
        {
            Debug.Log("Fog!!");
            object[] args = {"Fog", 1, uBuild.fogCount};
            pdPatch.SendList(receiver, args);
            timelineGenerator.AddRawImage(rawImages[8]);
        }
        if (other.CompareTag("Grass"))
        {
            Debug.Log("Grass!!");
            object[] args = {"Grass", 1, uBuild.grassCount};
            pdPatch.SendList(receiver, args);
            timelineGenerator.AddRawImage(rawImages[7]);
        }
        if (other.CompareTag("Tree"))
        {
            Debug.Log("Tree!!");
            object[] args = {"Tree", 1, uBuild.treeCount};
            pdPatch.SendList(receiver, args);
            timelineGenerator.AddRawImage(rawImages[6]);
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
            object[] args = {"Slope", 0, uBuild.slopeCount};
            pdPatch.SendList(receiver, args);
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
            object[] args = {"Tree", 0, uBuild.treeCount};
            pdPatch.SendList(receiver, args);
        }
    }
    
}
