using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ArenaManager : MonoBehaviour
    {
        [SerializeField] Transform spawnPoint;
        [SerializeField] GameObject enemy;
        public static int NoOfEnemies = 0;
        int round = 0;
        int multiplier = 5;


        void Update()
        {
            if (NoOfEnemies <= 0)
            {
                round++;
                int NoToSpawn = round * multiplier;
                for (int i = 0; i < NoToSpawn; i++)
                {
                    Instantiate(enemy, spawnPoint.position, Quaternion.identity);
                    NoOfEnemies++;
                }
                
            }
        }

        
    }
}


