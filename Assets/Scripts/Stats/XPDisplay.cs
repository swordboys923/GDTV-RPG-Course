using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
    {
    public class XPDisplay : MonoBehaviour
    {
        Experience experience;
        Text text;
        // Start is called before the first frame update
        void Start()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = experience.GetPoints().ToString();
        }
    }

}
