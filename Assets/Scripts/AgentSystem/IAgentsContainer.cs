using System.Collections.Generic;

namespace AgentSystem
{

    /// <summary>
    /// Interface for containers of agents and target
    /// </summary>
    public interface IAgentsContainer
    {
        IReadOnlyList<IAgent> Agents { get; }
        IAgentTarget Target { get; }

        /// <summary>
        /// Clears agents and target
        /// </summary>
        void Clear();

        /// <summary>
        /// Finds agents
        /// </summary>
        void FindAgents();

        /// <summary>
        /// Finds target
        /// </summary>
        void FindTarget();
    }
}