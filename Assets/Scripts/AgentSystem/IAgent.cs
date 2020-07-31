using UnityEngine;

namespace AgentSystem
{
    /// <summary>
    /// Agent interface used in AgentsManager
    /// </summary>
    public interface IAgent
    {
        Vector3 Position { get; }
        Vector3 Forward { get; }
        float DetectionDistance { get; }
        float FOV { get; }

        /// <summary>
        /// Updates detection status
        /// </summary>
        void UpdateDetectionStatus(DetectionStatus detectionStatus);
    }
}