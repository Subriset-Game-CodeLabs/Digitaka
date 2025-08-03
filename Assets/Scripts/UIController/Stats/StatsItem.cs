using UnityEngine;

namespace UIController.Stats
{
    public class StatsItem:MonoBehaviour
    {
        [SerializeField] private GameObject _fill;
        [SerializeField] private GameObject _empty;
        public void SetFill(bool isFilled)
        {
            _fill.SetActive(isFilled);
            _empty.SetActive(!isFilled);
        }
    }
}