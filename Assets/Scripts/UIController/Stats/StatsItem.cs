using UnityEngine;

namespace UIController.Stats
{
    public class StatsItem:MonoBehaviour
    {
        [SerializeField] private GameObject _fill;
        [SerializeField] private GameObject _halfFill;
        [SerializeField] private GameObject _empty;
        public void SetFill()
        {
            _fill.SetActive(true);
            _empty.SetActive(false);
            _halfFill.SetActive(false);
        }
        public void SetEmpty()
        {
            _empty.SetActive(true);
            _fill.SetActive(false);
            _halfFill.SetActive(false);
        }
        public void SetHalfFill()
        {
            _halfFill.SetActive(true);
            _empty.SetActive(false);
            _fill.SetActive(false);
        }
    }
}