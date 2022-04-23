using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100.0f;

    Transform playerTransform;

    float angleY = 0.0f;

    void Start()
    {
        playerTransform = transform.parent.transform;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        angleY -= mouseY;
        angleY = Mathf.Clamp(angleY, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(angleY, 0.0f, 0.0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }
}
