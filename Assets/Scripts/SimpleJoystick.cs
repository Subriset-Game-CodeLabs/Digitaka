using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class SimpleJoystick : PersistentSingleton<SimpleJoystick>, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
public RectTransform joystickBackground;
    public RectTransform joystickHandle;
    public float handleRange = 50f;

    private Vector2 inputVector;
    private bool isDragging = false;

    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
         UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetJoystick();
    }

    public Vector2 GetInput() => inputVector;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Only start dragging if inside the joystick background
        if (RectTransformUtility.RectangleContainsScreenPoint(joystickBackground, eventData.position, eventData.pressEventCamera))
        {
            isDragging = true;
            OnDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground,
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {
            pos = Vector2.ClampMagnitude(pos, handleRange);
            joystickHandle.anchoredPosition = pos;
            inputVector = pos / handleRange;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        ResetJoystick();
    }

    private void ResetJoystick()
    {
        inputVector = Vector2.zero;
        if (joystickHandle != null)
            joystickHandle.anchoredPosition = Vector2.zero;
    }
}
