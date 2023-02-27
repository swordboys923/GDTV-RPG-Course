using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
    {
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        Text text;
        // Start is called before the first frame update
        void Start()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = baseStats.GetLevel().ToString();
        }
    }

}
