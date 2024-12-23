using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ToolManager : MonoBehaviour
{
    // public GameObject timelineCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // timelineCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //playe mode & continuous sound
        if(Input.GetKeyDown(KeyCode.V))
        {
            // timelineCanvas.SetActive(!timelineCanvas.activeSelf);
        }

        //current scene clear
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("UPD_test_1222");
        }
    }
}
