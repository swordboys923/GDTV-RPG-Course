using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour 
    {
        float deltaAlpha;
        CanvasGroup canvasGroup;
        private void Awake() 
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1f;
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1f)
            {
                deltaAlpha = Time.deltaTime / time;
                canvasGroup.alpha += deltaAlpha;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0f)
            {
                deltaAlpha = Time.deltaTime / time;
                canvasGroup.alpha -= deltaAlpha;
                yield return null;
            }
        }
    }
}
