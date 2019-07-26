
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
       
        void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }
        private void Update() {
            //GetComponent<Text>().text = string.Format("{0:0}%",health.GetHealthPercentage());
            GetComponent<Text>().text = string.Format("{0:0}/{1:0}", health.GetHealthPoints(),health.GetMaxHealthPoints());
        }

     
    }

}
