using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private GameObject _airship;
    private Vector3 _airshipPosition;
    
    [SerializeField] private float cameraSpeed;
    
    private void Awake()
    {
        EventSystem.OnCameraInput += RotateCamera;
    }

    private void Start()
    {
        _airship = transform.parent.gameObject;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        EventSystem.OnCameraInput -= RotateCamera;
        Cursor.visible = true;
    }

    private void Update()
    {
        _airshipPosition = _airship.transform.position;
    }

    private void RotateCamera(float x, float y)
    {
        transform.RotateAround(_airshipPosition, Vector3.down, x * cameraSpeed);
        transform.RotateAround(_airshipPosition, Vector3.right, y * cameraSpeed);
        transform.LookAt(_airshipPosition);
    }
}
