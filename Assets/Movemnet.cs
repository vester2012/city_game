using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class MovemnetController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jump;
    [SerializeField] private LayerMask _groundLayer;

    private bool _isGrounded;

    private Rigidbody _rb;

    private Vector3 _movementVector3
    {
        get
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            return new Vector3(horizontal, 0, vertical);
        }
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationZ;
        if (_groundLayer == gameObject.layer)
        {
            Debug.LogError("Pkayer SortingLayer must be diffirent from Ground SortingLayer");
        }
    }

    private void FixedUpdate()
    {
        MovemonetLogic();
        JumpLogic();
        
    }

    private void MovemonetLogic()
    {
        _rb.AddForce(_movementVector3 * _speed, ForceMode.Impulse);
    }

    private void JumpLogic()
    {
        if (_isGrounded && (Input.GetAxis("Jump") > 0))
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * _jump, ForceMode.Impulse);
            }
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        IsGroundedUpdate(other, true);
    }

    private void OnCollisionExit(Collision other)
    {
        IsGroundedUpdate(other, false);
    }

    private void IsGroundedUpdate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = value;
        }
    }
}