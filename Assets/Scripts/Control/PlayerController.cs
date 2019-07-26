using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Resources;
using UnityEngine.EventSystems;
using System;

namespace RPG.Control

{
    public class PlayerController : MonoBehaviour
    {

        
        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        [SerializeField] CursorMapping[] cursormappings = null;

        Health health;

      
        void Awake()
        {
        health = GetComponent<Health>();
        }

       
        void Update()
        {
           if (InteractWithUI()) return;
            
            if (health.IsDead()) {
                SetCursor(CursorType.None);
                return;
            }
            if (InteractWithComponent()) return;
            //if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            SetCursor(CursorType.None);
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits  = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this)) {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted() {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];
         for (int i = 0; i < hits.Length; i++)
         {
             distances[i] = hits[i].distance;
         }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool InteractWithUI()
        {
            
         
            if (EventSystem.current.IsPointerOverGameObject())
                {  
               SetCursor(CursorType.UI);
                print("true");
               return true;
               
           }
           return false;
        }

        // private bool InteractWithCombat()
        // {
        //    RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        //    foreach (RaycastHit hit in hits) {
        //         CombatTarget target = hit.transform.GetComponent<CombatTarget>();
        //         if (target ==null) continue;
        //         if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;
        //         if (Input.GetMouseButton(0)) {
        //             GetComponent<Fighter>().Attack(target.gameObject);
        //         }
        //         SetCursor(CursorType.Combat);
        //         return true;

        //    }
        //    return false;
        // }

       

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0)) {
                GetComponent<Mover>().StartMoveAction(hit.point,1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }
        private CursorMapping GetCursorMapping(CursorType type) {
            foreach (CursorMapping mapping in cursormappings)
            {
                if (mapping.type == type)
                return mapping;
            }
            return cursormappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}