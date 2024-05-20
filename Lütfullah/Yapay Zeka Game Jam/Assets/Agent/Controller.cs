using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float escapeSpeed = 10f;
    public float escapeDistance = 5f;
    public Transform enemyTransform;
    private Rigidbody playerRb;
    private Vector3 movementBoundsMin;
    private Vector3 movementBoundsMax;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        // Define the bounds of the movement area
        movementBoundsMin = new Vector3(-10f, 0, -10f); // Alanı genişlettik
        movementBoundsMax = new Vector3(10f, 0, 10f);   // Alanı genişlettik
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;

        float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);

        if (distanceToEnemy < escapeDistance)
        {
            moveVector *= (escapeSpeed / moveSpeed); // Increase escape speed
        }

        Vector3 newPosition = transform.position + moveVector;
        newPosition = new Vector3(
            Mathf.Clamp(newPosition.x, movementBoundsMin.x, movementBoundsMax.x),
            newPosition.y,
            Mathf.Clamp(newPosition.z, movementBoundsMin.z, movementBoundsMax.z)
        );

        playerRb.MovePosition(newPosition);
    }

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(Random.Range(movementBoundsMin.x, movementBoundsMax.x), 0.5f, Random.Range(movementBoundsMin.z, movementBoundsMax.z));
        playerRb.velocity = Vector3.zero;
    }
}
