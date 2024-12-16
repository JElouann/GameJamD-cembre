using UnityEngine.InputSystem;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Vector2 _movement;

    [SerializeField]
    private Rigidbody _rb;

    public NetworkVariable<bool> CanMove = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public Transform cameraTransform;

    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if ((_rb != null && cameraTransform != null) && CanMove.Value)
        {
            // Get the camera's forward and right directions
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // Flatten the directions to ignore vertical movement
            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            // Calculate movement direction relative to the camera
            Vector3 movement = (forward * _movement.y + right * _movement.x) * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + movement);
        }
    }
}