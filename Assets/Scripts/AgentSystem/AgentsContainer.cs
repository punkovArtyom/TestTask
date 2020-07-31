using System.Collections.Generic;
using System.Linq;

using UnityEngine.SceneManagement;

namespace AgentSystem
{

    public sealed class AgentsContainer : IAgentsContainer
    {
        private List<IAgent> agents;

        public void FindAgents()
        {
            agents = SceneManager.GetActiveScene()
                .GetRootGameObjects()
                .SelectMany(root => root.GetComponentsInChildren<IAgent>())
                .ToList();
        }

        public void FindTarget()
        {
            Target = null;
            foreach (var root in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                foreach (var componentInChild in root.GetComponentsInChildren<IAgentTarget>())
                {
                    Target = componentInChild;
                    break;
                }

                if (Target != null)
                {
                    break;
                }
            }
        }

        public void Clear()
        {
            Target = null;
            agents.Clear();
        }

        public IReadOnlyList<IAgent> Agents => agents;

        public IAgentTarget Target { get; private set; }
    }
}