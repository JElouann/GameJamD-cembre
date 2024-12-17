using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class BodyRotation : MonoBehaviour
{
    public float sensitivity = 15;
    public Transform _cam; // Le Transform du joueur (pour la rotation Yaw)

    private Vector2 mouseDelta;

    private float xRotation = 0f;
    private float yRotation = 0f;

    [SerializeField] private bool _lockX;
    [SerializeField] private bool _lockY;

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
        RotateView();
    }

    private void RotateView()
    {
        // Appliquer la rotation de la souris à la caméra
        float mouseX = mouseDelta.x * sensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * sensitivity * Time.deltaTime;

        yRotation += mouseX;
        //cam
        transform.LookAt(_cam, Vector3.up);
        //player
        //_cam.rotation = Quaternion.Euler(0, yRotation, 0f);
    }
}
