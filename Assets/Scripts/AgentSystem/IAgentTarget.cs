using UnityEngine;

namespace AgentSystem
{
    /// <summary>
    /// AgentTarget interface used in AgentsManager 
    /// </summary>
    public interface IAgentTarget
    {
        Vector3 Position { get; }
    }
}