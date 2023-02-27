using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Health health;
        Fighter fighter;
        Text text;

        private void Awake() 
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            text = GetComponent<Text>();
        }

        private void Update() 
        {
            health = fighter.GetTarget();
            if(health == null)
            {
                text.text = "N/A";
            }
            else
            {
                text.text = string.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
            }
            
        }
    }
}
