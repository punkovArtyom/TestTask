using AgentSystem;

using UnityEngine;

public class TestAgentTarget : MonoBehaviour, IAgentTarget
{
    [SerializeField] [Min(0.01f)] private float speed;
    [SerializeField] [Min(0.01f)] private float rotationSpeed;

    public Vector3 Position => transform.position;

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        var horizontal = Vector3.ClampMagnitude(transform.right * Input.GetAxis("Horizontal"), 1f);
        var vertical = Vector3.ClampMagnitude(transform.forward * Input.GetAxis("Vertical"), 1f);

        transform.position += (horizontal + vertical) * speed * Time.deltaTime;
    }

    private void Rotate()
    {
        var rotation = Input.GetAxis("Mouse X");

        transform.Rotate(transform.up, rotation * rotationSpeed * Time.deltaTime);
    }
}
