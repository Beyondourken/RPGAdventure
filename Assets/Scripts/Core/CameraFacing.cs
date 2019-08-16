using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Control
{
    public class CameraFacing : MonoBehaviour
    {
        
      
        void Update()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}

