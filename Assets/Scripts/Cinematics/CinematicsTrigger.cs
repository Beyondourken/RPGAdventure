﻿using UnityEngine;
using UnityEngine.Playables;
namespace RPG.Cinematics

{
    public class CinematicsTrigger : MonoBehaviour
    {
        bool alreadyTriggered = false;
        private void OnTriggerEnter(Collider other) {
           
            if (!alreadyTriggered && other.tag == "Player") {
                GetComponent<PlayableDirector>().Play();
                alreadyTriggered = true;
            }
           
        }

    }

}

