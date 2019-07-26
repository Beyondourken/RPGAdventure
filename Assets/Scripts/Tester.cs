using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class Tester : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject(0))
        {

            print("Over UI");
           
        }
       print ("Not UI");
    }

    public void ButtonPressed() {
        print ("Button pressed");
    }
}
