using UnityEngine;

public class CameraMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;

    private float xRotation = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Mouse imlecini kilitle
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Dikey eksende bakış açısını sınırlayın
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        // Kamerayı dikey olarak döndür
        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);

        // Yatay eksende karakteri döndür
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
