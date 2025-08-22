using UnityEngine;
using DG.Tweening; 

namespace UIController
{
    public class InteractController : MonoBehaviour
    {
        [SerializeField] private GameObject MarkerInteract;
        [SerializeField] private float markerScale = 0.25f; // Scale to which the marker will animate
        private Tween markerTween;

        private void Start()
        {
            if (MarkerInteract != null)
            {
                MarkerInteract.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (MarkerInteract != null)
                {
                    MarkerInteract.SetActive(true);

                    markerTween?.Kill();
                    MarkerInteract.transform.localScale = MarkerInteract.transform.localScale;
                    markerTween = MarkerInteract.transform
                        .DOScale(markerScale, 0.5f) 
                        .SetEase(Ease.InOutSine)
                        .SetLoops(-1, LoopType.Yoyo);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (MarkerInteract != null)
                {
                    markerTween?.Kill();
                    MarkerInteract.transform.localScale = MarkerInteract.transform.localScale;
                    MarkerInteract.SetActive(false);
                }
            }
        }
    }
}