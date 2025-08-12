using UnityEngine;
using UnityEngine.InputSystem;

public class JumpInputDebugger : MonoBehaviour
{
    public InputActionReference jump;

    void OnEnable()
    {
        if (jump == null) return;
        jump.action.started += ctx => Debug.Log("Jump started");
        jump.action.performed += ctx => Debug.Log("Jump performed");
        jump.action.canceled += ctx => Debug.Log("Jump canceled");
        jump.action.Enable();
    }

    void OnDisable()
    {
        if (jump == null) return;
        jump.action.Disable();
    }
}
