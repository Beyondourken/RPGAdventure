using RPG.Resources;
using UnityEngine;
namespace RPG.Combat
{
    

    public class Projectile : MonoBehaviour
    {
        
        [SerializeField] float speed = 1;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifetime = 10;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 1;

        Health target = null;
        GameObject instigator = null;
        float damage = 0;
        
        private void Start() {
            transform.LookAt(GetAimLocation());
        }
        // Update is called once per frame
        void Update()
        {
        if (target == null) return; 
            if (isHoming && !target.IsDead()) {transform.LookAt(GetAimLocation());}
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget (Health target, GameObject instigator, float damage) {
            this.target = target;
            this .damage = damage;
            this.instigator = instigator;

            Destroy(gameObject,maxLifetime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
            if (targetCollider == null) return target.transform.position;
            //aim projectile at center of target's body
            return target.transform.position + Vector3.up * (targetCollider.height / 2);
        }

        private void OnTriggerEnter(Collider collider) {
        
            if (collider.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;
            speed = 0;
            target.TakeDamage(instigator, damage);
            if (hitEffect != null) {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }
        foreach(GameObject toDestroy in destroyOnHit){
            Destroy(toDestroy);
        }
            Destroy(gameObject, lifeAfterImpact);
            
        }
    }
}