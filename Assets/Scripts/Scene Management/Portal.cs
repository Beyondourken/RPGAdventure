using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System;
using RPG.Saving;


namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier {
            A,B,C,D
        }
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 0.5f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;

        private void OnTriggerEnter(Collider other) {
           
            if (other.tag == "Player") {
              
              
                StartCoroutine(Transition());
           }
        }

        private IEnumerator Transition() {
            if (sceneToLoad < 0) {
                Debug.Log("scene to load not set");
                yield break;
            }
            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper savingwrapper = FindObjectOfType<SavingWrapper>();
            yield return(fader.FadeOut(fadeOutTime));
            savingwrapper.Save();
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            savingwrapper.Load();   
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            savingwrapper.Save();
            yield return new WaitForSeconds(fadeWaitTime);
            yield return (fader.FadeIn(fadeInTime));
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
           foreach(Portal portal in FindObjectsOfType<Portal>()) {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
           }
           return null;
        }
    }
}

