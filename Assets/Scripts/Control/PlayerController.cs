﻿using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Resources;
using UnityEngine.EventSystems;
using UnityEngine.AI;
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
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float MaxNavPathLength = 40f;

        Health health;

        public float MaxNavMeshProjectionDistance { get => maxNavMeshProjectionDistance; set => maxNavMeshProjectionDistance = value; }
        public int MaxPathLength { get; private set; }

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
                
               return true;
               
           }
           return false;
        }


       

        private bool InteractWithMovement()
        {
            
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);
            if (hasHit)
            {
                if (Input.GetMouseButton(0)) {
                GetComponent<Mover>().StartMoveAction(target,1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            target = new Vector3();
            if (!hasHit) return false;
            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;
            target = navMeshHit.position;
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position,target,NavMesh.AllAreas,path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > MaxNavPathLength) return false;
            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;
           for (int i = 0; i < path.corners.Length-1; i++)
           {
                total += Vector3.Distance(path.corners[i], path.corners[i+1]);
               
           }
            return total;
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