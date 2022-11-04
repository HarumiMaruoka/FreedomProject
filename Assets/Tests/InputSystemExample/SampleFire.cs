
using UnityEngine;
using UnityEngine.InputSystem;

public class SampleFire : MonoBehaviour
{
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Fire");
        }
    }

    public void OnLeftFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("LeftFire!");
        }
    }

    public void OnRightFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("RightFire!");
        }
    }
}