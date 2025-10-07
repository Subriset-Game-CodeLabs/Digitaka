using System;
using Input;
using UnityEngine;
using UnityEngine.UI;

namespace MapSystem
{
    public class MapUIController: MonoBehaviour
    {
        [SerializeField] private GameObject _chapter1;
        [SerializeField] private GameObject _chapter2;
        [SerializeField] private GameObject _chapter3;
        [SerializeField] private Button _closeButton;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _closeButton.onClick.AddListener(HideMap);
            HideMap();
        }

        private void ShowMap()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            InputManager.Instance.UIMode();
        }
        public void HideMap()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            InputManager.Instance.PlayerMode();
        }
        public void ShowMapChapter(int chapter)
        {
            GameObject mapChapter = null;
            if (chapter == 1)
            {
                mapChapter = _chapter1;
                ShowMapChapter1();
            }
            else if (chapter == 2)
            {
                mapChapter = _chapter2;
                ShowMapChapter2();
            }
            else if (chapter == 3)
            {
                mapChapter = _chapter3;
                ShowMapChapter3();
            }
            
            if (mapChapter == null)
                return;
            Transform _mapTiles = mapChapter.transform.Find("MapTiles");
            mapChapter.gameObject.SetActive(true);
            foreach (Transform item in _mapTiles)
            {
                MapTile mapTile = item.gameObject.GetComponent<MapTile>();
                if (mapTile.MapName == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
                {
                    mapTile.ActivePointer();
                }
                else
                {
                    mapTile.DeactivePointer();
                }
            }
            ShowMap();
        }
        public void ShowMapChapter1()
        {
            _chapter1.SetActive(true);
            _chapter2.SetActive(false);
            _chapter3.SetActive(false);
        }
        public void ShowMapChapter2()
        {
            _chapter1.SetActive(false);
            _chapter2.SetActive(true);
            _chapter3.SetActive(false);
        }
        public void ShowMapChapter3()
        {
            _chapter1.SetActive(false);
            _chapter2.SetActive(false);
            _chapter3.SetActive(true);
        }
    }
}