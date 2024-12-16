using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : NetworkBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            JumpAction();
        }
    }

    public void JumpAction()
    {
        if (!IsOwner) { return; }
        _rb.AddForce(Vector3.up * 5);
    }
}
