using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 15;
    public Transform Head; // Le Transform du joueur (pour la rotation Yaw)
    public Transform Body;

    private Vector2 mouseDelta;

    private float xRotation = 0f;
    private float yRotation = 0f;

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

    private void Update()
    {
        if (CanMoveCamera.Value)
        {
            RotateView();
        }
    }

    private void RotateView()
    {
        // Appliquer la rotation de la souris � la cam�ra
        float mouseX = mouseDelta.x * sensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limite pour �viter de regarder "� l'envers"
        yRotation += mouseX;

        Head.transform.rotation = Quaternion.Euler(xRotation, 0f, 0f);
        //Body.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        //player
        //_cam.rotation = Quaternion.Euler(0, yRotation, 0f);
    }
}
