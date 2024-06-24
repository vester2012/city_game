using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CcMovemnet : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _mouseSensetivity = 100;
    [SerializeField] private float _gravity = -9.81f;

    private Animator _animator;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private float _rotationY = 0;
    private float _rotationX = 0;

    [SerializeField] private Transform _cameraTransform;

    private bool _isMoving;
    private Vector3 _previousPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        
        _characterController.skinWidth = 0;
        Cursor.lockState = CursorLockMode.Locked;

        _previousPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        RotateCharacter();
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    private void RotateCharacter()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensetivity * Time.deltaTime;

        _rotationY += mouseX;
        transform.localRotation = Quaternion.Euler(0f, _rotationY, 0f);

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -60f, 60f);

        _cameraTransform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        _characterController.Move(move * (_speed * Time.deltaTime));
        
        if (_previousPosition != transform.position)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        
        _previousPosition = transform.position;
        
        _animator.SetBool("IsMove", _isMoving);
        Debug.Log(_isMoving);
    }
}
