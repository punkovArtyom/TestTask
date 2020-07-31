using System;
using System.Collections;

using UnityEngine;

namespace AgentSystem
{
    /// <summary>
    /// AgentManager is a manager that checks and update agents detection status
    /// </summary>
    public class AgentsManager : MonoBehaviour
    {
        private static AgentsManager instance;

        [SerializeField] protected AgentManagerBasePreferences preferences;

        private IAgentsContainer agentsContainer;

        private bool isRunning;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError($"[{nameof(AgentsManager)}] Instance of {nameof(AgentsManager)} already exists");
                Destroy(this);

                return;
            }

            instance = this;
        }

        protected virtual void Start()
        {
            Initialize();

            if (preferences.StartOnPlay)
            {
                StartManager();
            }
        }

        /// <summary>
        /// Starts managers coroutine
        /// </summary>
        public void StartManager()
        {
            if (isRunning) return;

            ResetManager();
            StartCoroutine(ManagerRoutine());

            isRunning = true;
        }

        /// <summary>
        /// Resets agents and target
        /// </summary>
        public void ResetManager()
        {
            agentsContainer.FindAgents();
            agentsContainer.FindTarget();
        }

        /// <summary>
        /// Stops manager's coroutine
        /// </summary>
        public void StopManager()
        {
            if (!isRunning) return;

            StopCoroutine(ManagerRoutine());
            agentsContainer.Clear();

            isRunning = false;
        }

        /// <summary>
        /// Manager initialization
        /// </summary>
        protected virtual void Initialize()
        {
            if (preferences == null)
            {
                throw new NullReferenceException($"[{nameof(AgentsManager)}] {nameof(preferences)} is missing");
            }

            // Just for sake of simplification, should use factory and/or dependency injection
            agentsContainer = new AgentsContainer();
        }

        /// <summary>
        /// Combines all checks and send status update to agents
        /// </summary>
        protected virtual void CheckAgentsAgainstTarget()
        {
            var target = agentsContainer.Target;

            foreach (var agent in agentsContainer.Agents)
            {
                var isInDistance = CheckIfInDistance(agent, target);
                var isInFOV = CheckIfInFOV(agent, target);
                var isVisible = isInFOV == DetectionStatus.InFOV ? CheckIfVisible(agent, target) : 0;

                agent.UpdateDetectionStatus(isInDistance | isInFOV | isVisible);
            }
        }

        /// <summary>
        /// Checks if the target is near the agent
        /// </summary>
        /// <returns>DetectionStatus.InCloseDistance or 0</returns>
        protected DetectionStatus CheckIfInDistance(IAgent agent, IAgentTarget agentTarget)
        {
            return agent.DetectionDistance * agent.DetectionDistance >=
                   (agent.Position - agentTarget.Position).sqrMagnitude
                ? DetectionStatus.InCloseDistance
                : 0;
        }

        /// <summary>
        /// Checks if the target is in FOV of the agent
        /// </summary>
        /// <returns>DetectionStatus.InFOV or 0</returns>
        protected DetectionStatus CheckIfInFOV(IAgent agent, IAgentTarget agentTarget)
        {
            var direction = (agentTarget.Position - agent.Position).normalized;
            var forwardN = agent.Forward.normalized;

            var angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(direction, forwardN));

            return angle < agent.FOV / 2 ? DetectionStatus.InFOV : 0;
        }

        /// <summary>
        /// Checks if the target is visible for the agent
        /// </summary>
        /// <returns>DetectionStatus.Visible or 0</returns>
        protected DetectionStatus CheckIfVisible(IAgent agent, IAgentTarget agentTarget)
        {
            var direction = agentTarget.Position - agent.Position;
            if (Physics.Raycast(agent.Position, direction,
                out var hit, preferences.MaxVisibilityCheckDistance, preferences.VisibilityLayerMask))
            {
                if (hit.collider.GetComponent<IAgentTarget>() != null)
                {
                    return DetectionStatus.Visible;
                }
            }

            return 0;
        }

        private IEnumerator ManagerRoutine()
        {
            for (; ; )
            {
                CheckAgentsAgainstTarget();

                yield return null;
            }
        }
    }
}