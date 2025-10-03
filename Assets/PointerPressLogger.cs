using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PointerPressLogger : PersistentSingleton<PointerPressLogger>
{
    private PointerEventData pointerData;
    private List<RaycastResult> results = new List<RaycastResult>();

    void Update()
    {

        Vector2 pos = Vector2.zero;
        bool pressed = false;

        // Check mouse
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            pos = Mouse.current.position.ReadValue();
            pressed = true;
        }

        // Check touchscreen
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            pos = Touchscreen.current.primaryTouch.position.ReadValue();
            pressed = true;
        }

        if (pressed)
        {
            // Build event data
            pointerData = new PointerEventData(EventSystem.current)
            {
                position = pos
            };

            results.Clear();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                Debug.Log("Pointer Debug @ " + pos);
                foreach (var r in results)
                {
                    Debug.Log($"- Hit: {r.gameObject.name} (module: {r.module})");
                }

                Debug.Log(">>> Top candidate (pointerPress equivalent): " + results[0].gameObject.name);
            }
            else
            {
                Debug.Log("Pointer hit nothing @ " + pos);
            }
        }


    }
}



