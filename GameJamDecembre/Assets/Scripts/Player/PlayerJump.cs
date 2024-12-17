using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerJump : NetworkBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private LayerMask _groundLayer1;

    [SerializeField]
    private Transform _groundCheck;

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            if (!IsOwner) { return; }
            Debug.Log("Jump");
            _rb.AddForce(Vector3.up * 400);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, 0.8f, _groundLayer1);
    }

}
