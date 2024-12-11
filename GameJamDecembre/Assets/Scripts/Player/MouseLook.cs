//using System.Globalization;
//using Unity.Netcode;
//using UnityEngine;
//using UnityEngine.InputSystem;


//public class MouseLook : NetworkBehaviour
//{
//    [SerializeField] float _turnSpeed;
//    [SerializeField] Vector2 _lookDelta;
//    [SerializeField] GameObject _camera;
//    [SerializeField] GameObject _player;

//    public void OnLook(InputAction.CallbackContext callbackContext)
//    {
//        _lookDelta = callbackContext.ReadValue<Vector2>();
//    }

//    private void Update()
//    {
//        if (!IsOwner) { return; }

//        if (_camera != null && _player != null)
//        {
//            _camera.transform.rotation = Quaternion.Euler(_turnSpeed * Time.deltaTime * new Vector3(_camera.transform.rotation.x, Mathf.Clamp(_camera.transform.rotation.y + _lookDelta.y, 0, 360), _camera.transform.rotation.z));
//            //_player.transform.rotation = Quaternion.Euler(_turnSpeed * Time.deltaTime * new Vector3(_player.transform.rotation.x + _lookDelta.x, _player.transform.rotation.y, _player.transform.rotation.z));
//            print($"somme rot : {Mathf.Clamp(_camera.transform.rotation.y + _lookDelta.y, 0, 360)}, rot camera : {_camera.transform.rotation.y}, delta : {_lookDelta.y}");
//        }
//    }
//}

using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 40;
    public Transform playerBody; // Le Transform du joueur (pour la rotation Yaw)

    private Vector2 mouseDelta;

    private float xRotation = 0f;
    private float yRotation = 0f;

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

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limite pour éviter de regarder "à l'envers"
        yRotation += mouseX;
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        //playerBody.Rotate(new Vector3(mouseX, mouseY, 0));
    }
}
