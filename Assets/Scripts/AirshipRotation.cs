using UnityEngine;

public class AirshipRotation : MonoBehaviour
{
    private Vector3 _velocity, _angularVelocity;
    [SerializeField] private float
        maxPitchAngle,
        maxRollAngle,
        pitchCoefficient,
        rollCoefficient;

    private void Awake()
    {
        EventSystem.OnAirshipVelocityChange += SaveVelocity;
    }

    private void OnDisable()
    {
        EventSystem.OnAirshipVelocityChange -= SaveVelocity;
    }

    private void SaveVelocity(Vector3 velocity, Vector3 angularVelocity)
    {
        _velocity = velocity;
        _angularVelocity = angularVelocity;
    }

    private void Update()
    {
        if (_velocity != Vector3.zero)
            RotateAirship();
    }

    private void RotateAirship()
    {
        float pitchAngle = CalculateAngle(_velocity.y, maxPitchAngle, pitchCoefficient);
        float rollAngle = CalculateAngle(_angularVelocity.y, maxRollAngle, rollCoefficient);
        ApplyRotation(pitchAngle, rollAngle);
    }

    private static float CalculateAngle (float velocity, float maxAngle, float coefficient)
    {
        float angle = velocity * maxAngle * coefficient * -1f;
        angle = Mathf.Clamp(angle, -maxAngle, maxAngle);
        return angle;
    }

    private void ApplyRotation(float pitch, float roll)
    {
        Quaternion rotationQuaternion = Quaternion.Euler(new Vector3(pitch, 0 ,roll));
        transform.localRotation = rotationQuaternion;
    }
}
