using UnityEngine;

namespace AgentSystem
{
    /// <summary>
    /// Base preferences for AgentsManager
    /// </summary>
    [CreateAssetMenu(fileName = "AgentManagerPreferences", menuName = "AgentSystem/AgentManagerPreferences")]
    public class AgentManagerBasePreferences : ScriptableObject
    {
        [SerializeField] private bool startOnPlay;
        [SerializeField] private float maxVisibilityCheckDistance;
        [SerializeField] private LayerMask visibilityLayerMask;

        /// <summary>
        /// Should manager be started on play
        /// </summary>
        public bool StartOnPlay {
            get => startOnPlay;
            set => startOnPlay = value;
        }

        /// <summary>
        /// Max distance of raycast for visibility check
        /// </summary>
        public float MaxVisibilityCheckDistance {
            get => maxVisibilityCheckDistance;
            set => maxVisibilityCheckDistance = value;
        }

        /// <summary>
        /// LayerMask of raycast for visibility check
        /// </summary>
        public LayerMask VisibilityLayerMask {
            get => visibilityLayerMask;
            set => visibilityLayerMask = value;
        }
    }
}