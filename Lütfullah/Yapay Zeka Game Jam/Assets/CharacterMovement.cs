using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f; // Hareket hızı

    void Update()
    {
        // Yatay Hareket
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = transform.right * horizontalInput;

        // İleri/Geri Hareket
        float verticalInput = Input.GetAxis("Vertical");
        movement += transform.forward * verticalInput;

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
