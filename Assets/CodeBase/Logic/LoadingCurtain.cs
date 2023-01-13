using UnityEngine;
using System.Collections;

namespace CodeBase.Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        public CanvasGroup Curtain;

        private float _alphaValue = 0.03f;
        private void Awake() =>
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1f;
        }

        public void Hide() =>
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= _alphaValue;
                yield return new WaitForSeconds(_alphaValue);
            }

            gameObject.SetActive(false);
        }
    }
}