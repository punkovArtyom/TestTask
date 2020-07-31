using AgentSystem;
using UnityEngine;

public class TestAgent : MonoBehaviour, IAgent
{
    [SerializeField] private float detectionDistance;
    [SerializeField] private float _FOV;

    private bool near;
    private bool visible;
    private bool inFOV;

    public Vector3 Position => transform.position;
    public Vector3 Forward => transform.forward;
    public float DetectionDistance => detectionDistance;
    public float FOV => _FOV;

    public void UpdateDetectionStatus(DetectionStatus detectionStatus)
    {
        near = (detectionStatus & DetectionStatus.InCloseDistance) == DetectionStatus.InCloseDistance;
        visible = (detectionStatus & DetectionStatus.Visible) == DetectionStatus.Visible;
        inFOV = (detectionStatus & DetectionStatus.InFOV) == DetectionStatus.InFOV;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = near ? Color.red : Color.white;
        Gizmos.DrawCube(new Vector3(-0.5f, 0.8f, 0f), Vector3.one * 0.2f);

        Gizmos.color = visible ? Color.blue : Color.white;
        Gizmos.DrawCube(new Vector3(0f, 0.8f, 0f), Vector3.one * 0.2f);

        Gizmos.color = inFOV ? Color.green : Color.white;
        Gizmos.DrawCube(new Vector3(0.5f, 0.8f, 0f), Vector3.one * 0.2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(Vector3.zero, Quaternion.Euler(0f, -FOV / 2f, 0f) * Vector3.forward * 2);
        Gizmos.DrawLine(Vector3.zero, Quaternion.Euler(0f, FOV / 2f, 0f) * Vector3.forward * 2);

        Gizmos.DrawLine(Vector3.zero, Quaternion.Euler(-FOV / 2f, 0f, 0f) * Vector3.forward * 2);
        Gizmos.DrawLine(Vector3.zero, Quaternion.Euler(FOV / 2f, 0f, 0f) * Vector3.forward * 2);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Vector3.zero, detectionDistance);
    }
#endif
}
