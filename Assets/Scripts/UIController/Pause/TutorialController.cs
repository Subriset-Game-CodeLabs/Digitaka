using UnityEngine;

namespace UIController.Pause
{
    public class TutorialController:MonoBehaviour
    {
        [SerializeField] private GameObject[] _tutorialPanels;
        private int _currentPanelIndex = 0;
        private void Start()
        {
            ShowCurrentPanel();
        }
        private void ShowCurrentPanel()
        {
            for (int i = 0; i < _tutorialPanels.Length; i++)
            {
                _tutorialPanels[i].SetActive(i == _currentPanelIndex);
            }
        }
        public void NextPanel()
        {
            if (_currentPanelIndex < _tutorialPanels.Length - 1)
            {
                _currentPanelIndex++;
                ShowCurrentPanel();
            }
            else
            {
                GameManager.Instance.CompleteTutorial();
            }
        }
        public void PreviousPanel()
        {
            if (_currentPanelIndex > 0)
            {
                _currentPanelIndex--;
                ShowCurrentPanel();
            }
        }
        public void CloseTutorial()
        {
            gameObject.SetActive(false); // Close the tutorial panel
        }
        
    }
}