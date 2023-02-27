using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 8f;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] float lifeAfterImpact = 2f;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;
        
        [SerializeField] GameObject [] destroyOnHit = null;
        Health target;
        GameObject instigator = null;
        float damage = 0f;

        private void Start() 
        {
            transform.LookAt(GetAimLocation());
        }
        private void Update()
        {
            if (target == null) return;        
            if(isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);      
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsuleCollider = target.GetComponent<CapsuleCollider>();
            if (targetCapsuleCollider == null) 
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * (targetCapsuleCollider.height / 2);
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.GetComponent<Health>() != target) return;
            if(target.IsDead()) return;
            target.TakeDamage(instigator, damage);
            speed = 0f;
            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }
}
