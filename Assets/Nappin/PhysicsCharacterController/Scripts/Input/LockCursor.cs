﻿using UnityEngine;


namespace PhysicsCharacterController
{
    public class LockCursor : MonoBehaviour
    {
        public bool lockCursor = false;


        /**/


        private void Awake()
        {
            if(lockCursor) Cursor.lockState = CursorLockMode.Locked;
        }
    }
}