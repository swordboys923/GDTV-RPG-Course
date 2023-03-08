namespace RPG.Combat {
    using UnityEngine;
    
    public class Health : MonoBehaviour {
        [SerializeField] float healthPoints = 100f;

        Animator animator;

        bool isDead = false;
        public bool IsDead() {
            return isDead;
        }

        private void Awake() {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage) {
            healthPoints = Mathf.Max(healthPoints - damage,0);
            print(healthPoints);
            if (healthPoints == 0) {
                Die();
            }
        }

        private void Die() {
            if (isDead) return;
            isDead = true;
            animator.SetTrigger("die");
        }
    }
}