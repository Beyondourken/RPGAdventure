
using RPG.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
       
        void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
        }
        private void Update() {
           
            if (fighter.GetTarget() == null) {
                GetComponent<Text>().text = "N/A";
                return;
            } 
                Health health = fighter.GetTarget();
                //GetComponent<Text>().text = string.Format("{0:0}%", health.GetHealthPercentage());
                 GetComponent<Text>().text = string.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
            
            
        }

     
    }

}
