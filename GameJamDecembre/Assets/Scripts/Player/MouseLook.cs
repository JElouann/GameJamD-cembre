using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 40;
    public Transform playerBody; // Le Transform du joueur (pour la rotation Yaw)

    private Vector2 mouseDelta;

    private float xRotation = 0f;
    private float yRotation = 0f;

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
        // Appliquer la rotation de la souris à la caméra
        float mouseX = mouseDelta.x * sensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limite pour éviter de regarder "à l'envers"
        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        //playerBody.Rotate(new Vector3(mouseX, mouseY, 0));
    }
}
