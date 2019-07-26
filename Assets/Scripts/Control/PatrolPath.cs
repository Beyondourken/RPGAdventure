using System;
using UnityEngine;
namespace RPG.Combat
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos() {
            Gizmos.color = Color.white;
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), .2f);
                Gizmos.DrawLine(GetWaypoint(i),GetWaypoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
           int j;
           if (i+1 == transform.childCount) {
               j = 0;
           } else {
               j = i+1;
           }

            return j;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
