using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 40;
    public Transform _cam; // Le Transform du joueur (pour la rotation Yaw)

    private Vector2 mouseDelta;

    private float xRotation;
    private float yRotation;

    [SerializeField] private bool _lockX;
    [SerializeField] private bool _lockY;

    public NetworkVariable<bool> CanMoveCamera = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public void OnMouseLookPerformed(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
        if (context.canceled)
        {
            mouseDelta = Vector2.zero;
        }
    }

    private void Start()
    {
        xRotation = transform.rotation.x;
        yRotation = transform.rotation.y;
    }

    private void Update()
    {
        if (CanMoveCamera.Value)
        {
            RotateView();
        }
    }

    private void RotateView()
    {
        // Appliquer la rotation de la souris à la caméra
        float mouseX = mouseDelta.x * sensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * sensitivity * Time.deltaTime;

        if (!_lockX) xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limite pour éviter de regarder "à l'envers"
        if (!_lockY) yRotation += mouseX;
        //cam
        _cam.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //player
        //transform.rotation = Quaternion.Euler(0, yRotation, 0f);
    }
}
