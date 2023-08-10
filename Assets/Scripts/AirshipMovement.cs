using System;
using UnityEngine;

public class AirshipMovement : MonoBehaviour
{
    [SerializeField] private float
        maxThrustForce,
        maxSteeringForce,
        maxBrakingForce,
        maxHeightForce;

    private Rigidbody _rb;
    private Vector3 _localVelocity, _localAngularVelocity;
    private Quaternion _inversedRotation;

    private void Awake()
    {
        EventSystem.OnMovementInput += ReadInput;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        EventSystem.OnMovementInput -= ReadInput;
    }

    private void FixedUpdate()
    {
        CalculateState();

        if (!_localVelocity.Equals(Vector3.zero))
            InvokeVelocityChange(_localVelocity, _localAngularVelocity);
    }

    private void CalculateState()
    {
        _inversedRotation = Quaternion.Inverse(_rb.rotation);
        _localVelocity = _inversedRotation * _rb.velocity;
        _localAngularVelocity = _inversedRotation * _rb.angularVelocity;
    }

    private void ReadInput(MovementInputType movementInput, float degree)
    {
        switch (movementInput)
        {
            case MovementInputType.Thrust:
                Thrust(degree);
                break;
            case MovementInputType.Brake:
                Brake(degree);
                break;
            case MovementInputType.Pitch:
                ChangeHeight(degree);
                break;
            case MovementInputType.Steer:
                Steer(degree);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(movementInput), movementInput, null);
        }
    }

    private void Thrust(float degree)
    {
        Vector3 thrustForce = Vector3.forward * (degree * maxThrustForce);
        _rb.AddRelativeForce(thrustForce);
    }
    
    private void Brake(float degree)
    {
        if (_localVelocity.z <= 0)
            return;
        
        Vector3 brakingForce = Vector3.back * (degree * maxBrakingForce * _localVelocity.magnitude);
        _rb.AddRelativeForce(brakingForce);
    }

    private void ChangeHeight(float degree)
    {
        Vector3 verticalForce = Vector3.up * (degree * maxHeightForce);
        _rb.AddForce(verticalForce);
    }
    
    private void Steer(float degree)
    {
        Vector3 rotationForce = Vector3.up * (degree * Mathf.Deg2Rad * maxSteeringForce);
        _rb.AddRelativeTorque(rotationForce);
    }

    private static void InvokeVelocityChange(Vector3 velocity, Vector3 angularVelocity)
    {
        EventSystem.InvokeAirshipVelocityChange(velocity, angularVelocity);
    }
}
