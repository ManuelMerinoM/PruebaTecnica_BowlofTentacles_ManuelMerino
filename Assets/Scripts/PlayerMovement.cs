using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Transform cameraTransform;

    public float aimspeed = 0.5f;
    public float camlimitmin = -80f;
    public float camlimitmax = 80f;

    private PlayerInputControl _inputAction;
    private CharacterController _characterController;

    private Vector2 _movement;
    private Vector2 _look;
    private Vector2 _speed;

    private float _currentRotationY;
    private void Awake()
    {
        _inputAction = new PlayerInputControl();   
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {

        Cursor.visible=false;
        Cursor.lockState = CursorLockMode.Locked;

        _inputAction.Player.Enable();

        _inputAction.Player.Move.performed += SetMovement;
        _inputAction.Player.Move.canceled += _=> _movement= Vector2.zero;

        _inputAction.Player.Look.performed += SetLook;
        _inputAction.Player.Look.canceled += _ => _look = Vector2.zero;


    }

    private void SetMovement(InputAction.CallbackContext obj)
    {
        _movement=obj.ReadValue<Vector2>();
    }

    private void SetLook(InputAction.CallbackContext obj)
    {
        _look = obj.ReadValue<Vector2>();
    }

    private void Update()
    {
        Movement();

        Look();

    }

    private void Look()
    {
        Vector2 mouseNormalized = _look * aimspeed;

        _currentRotationY = Mathf.Clamp(_currentRotationY - mouseNormalized.y, camlimitmin, camlimitmax);

        cameraTransform.localRotation = Quaternion.Euler(_currentRotationY, 0, 0);

        transform.Rotate(Vector3.up * mouseNormalized.x);
    }

    private void Movement()
    {
        Vector3 move = transform.right * _movement.x + transform.forward * _movement.y;
        _characterController.Move(move * movementSpeed * Time.deltaTime);

        _speed.y += -9.8f * Time.deltaTime;
        _characterController.Move(_speed * Time.deltaTime);
    }
}
