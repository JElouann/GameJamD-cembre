using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector2 _movement;
    [SerializeField] private float _speed;
    [SerializeField]
    private Rigidbody _rb;

    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector3 truc = new Vector3(_movement.x, 0, _movement.y) * _speed * Time.deltaTime;
        transform.Translate(truc);
    }
}
