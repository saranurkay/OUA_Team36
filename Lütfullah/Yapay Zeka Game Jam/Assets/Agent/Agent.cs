using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class ChaseAgent : Agent
{
    public Transform playerTransform;
    public float moveSpeed = 5f;
    private Rigidbody agentRb;
    private Vector3 movementBoundsMin = new Vector3(-10f, 0, -10f); // Alanı genişlettik
    private Vector3 movementBoundsMax = new Vector3(10f, 0, 10f);   // Alanı genişlettik

    public override void Initialize()
    {
        agentRb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset agent and player positions at the start of each episode
        transform.localPosition = new Vector3(Random.Range(movementBoundsMin.x, movementBoundsMax.x), 0.5f, Random.Range(movementBoundsMin.z, movementBoundsMax.z));
        playerTransform.localPosition = new Vector3(Random.Range(movementBoundsMin.x, movementBoundsMax.x), 0.5f, Random.Range(movementBoundsMin.z, movementBoundsMax.z));
        agentRb.velocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent's position
        sensor.AddObservation(transform.localPosition);

        // Player's position
        sensor.AddObservation(playerTransform.localPosition);

        // Direction to player
        Vector3 directionToPlayer = (playerTransform.localPosition - transform.localPosition).normalized;
        sensor.AddObservation(directionToPlayer);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Get movement values from actions
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        // Normalize movement vector
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized * moveSpeed * Time.deltaTime;

        // Move agent
        Vector3 newPosition = transform.position + move;
        newPosition = new Vector3(
            Mathf.Clamp(newPosition.x, movementBoundsMin.x, movementBoundsMax.x),
            newPosition.y,
            Mathf.Clamp(newPosition.z, movementBoundsMin.z, movementBoundsMax.z)
        );

        // Eğer ajan sınırdaysa rastgele bir yön değişikliği yap
        if (newPosition.x == movementBoundsMin.x || newPosition.x == movementBoundsMax.x ||
            newPosition.z == movementBoundsMin.z || newPosition.z == movementBoundsMax.z)
        {
            // Negatif ödül ver ve yön değiştir
            AddReward(-0.1f);
            move = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * moveSpeed * Time.deltaTime;
            newPosition = transform.position + move;
            newPosition = new Vector3(
                Mathf.Clamp(newPosition.x, movementBoundsMin.x, movementBoundsMax.x),
                newPosition.y,
                Mathf.Clamp(newPosition.z, movementBoundsMin.z, movementBoundsMax.z)
            );
        }

        agentRb.MovePosition(newPosition);

        // Reward the agent for moving closer to the player
        float distanceToPlayer = Vector3.Distance(transform.localPosition, playerTransform.localPosition);
        if (distanceToPlayer < 1.5f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        else
        {
            // Zaman cezası
            AddReward(-0.01f);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
