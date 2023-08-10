using System;
using UnityEngine;

public static class EventSystem
{
    public static event Action<MovementInputType, float> OnMovementInput;
    public static event Action<Vector3, Vector3> OnAirshipVelocityChange; 
    public static event Action<float, float> OnCameraInput;

    public static void InvokeControlsInput(MovementInputType movementInput, float degree)
    {
        OnMovementInput?.Invoke(movementInput, degree);
    }

    public static void InvokeAirshipVelocityChange(Vector3 velocity, Vector3 angularVelocity)
    {
        OnAirshipVelocityChange?.Invoke(velocity, angularVelocity);
    }

    public static void InvokeCameraInput(float x, float y)
    {
        OnCameraInput?.Invoke(x, y);
    }
}
