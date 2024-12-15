using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPdController : MonoBehaviour
{
 //   public FirstPersonController firstPC;
    public LibPdInstance pdPatch;
 
    // Start is called before the first frame update
    void Start()
    {
        pdPatch.SendBang("PlayOn");

    }

    // Update is called once per frame
    void Update()
    {

        //if(firstPC.isCrouched == true)
        //{
        //    Debug.Log("crouch!!");
        //    pdPatch.SendBang("LowTone");

            
        //}else{
        //    pdPatch.SendBang("HighTone");

        //}
    }
}
