using Input;
using UnityEngine;

namespace UIController.Pause
{
    public class TutorialController:MonoBehaviour
    {
        void Start()
        {
            InputManager.Instance.UIMode();
       }
    }
}