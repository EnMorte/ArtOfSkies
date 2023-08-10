using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private static InputSystem _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        //Thrust
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            InvokeThrust(1f);
        else if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            InvokeBrake(1f);
        
        //Steering
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            InvokeSteer(-1f);
        else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            InvokeSteer(1f);
        
        //Pitch
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            InvokePitch(1f);
        else if (!Input.GetMouseButton(0) && Input.GetMouseButton(1))
            InvokePitch(-1f);

        //Camera
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            InvokeCamera(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    private static void InvokeThrust(float degree)
    {
        EventSystem.InvokeControlsInput(MovementInputType.Thrust, degree);
    }
    
    private static void InvokeBrake(float degree)
    {
        EventSystem.InvokeControlsInput(MovementInputType.Brake, degree);
    }

    private static void InvokeSteer(float degree)
    {
        EventSystem.InvokeControlsInput(MovementInputType.Steer, degree);
    }

    private static void InvokePitch(float degree)
    {
        EventSystem.InvokeControlsInput(MovementInputType.Pitch, degree);
    }

    private static void InvokeCamera(float x, float y)
    {
        EventSystem.InvokeCameraInput(x, y);
    }
}
