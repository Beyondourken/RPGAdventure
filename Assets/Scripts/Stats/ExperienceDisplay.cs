﻿
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
             
        void Awake()
        {
            experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
        }
       
        private void Update() {
          

           GetComponent<Text>().text =  string.Format("{0:0}", experience.GetExperiencePoints());
        }

     
    }

}
