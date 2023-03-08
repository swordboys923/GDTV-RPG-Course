using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {
    public class Fighter : MonoBehaviour, IAction {
        
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        Health target;
        float timeSinceLastAttack;
        Mover mover;
        Animator animator;
        ActionScheduler actionScheduler;

        private void Awake() {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            
            if (!GetIsInRange()) {
                mover.MoveTo(target.transform.position);
            }
            else {
                mover.Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior() {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack >= timeBetweenAttacks) {
                // This will trigger the Hit() event,
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }
        //Animation Event
        void Hit() {
            if (target != null && target.TryGetComponent<Health>(out Health targetHealth)) {
                targetHealth.TakeDamage(weaponDamage);
            }
        }

        private bool GetIsInRange() {
            return Mathf.Abs(Vector3.Distance(transform.position, target.transform.position)) < weaponRange;
        }

        public void Attack(CombatTarget target) {
            actionScheduler.StartAction(this);
            this.target = target.GetComponent<Health>();;
        }

        public void Cancel() {
            animator.SetTrigger("stopAttack");
            target = null;
        }

        public bool CanAttack(CombatTarget combatTarget) {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return (targetToTest != null && !targetToTest.IsDead());
        }
    }
}
