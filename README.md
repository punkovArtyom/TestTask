# Test task

## Overview
Unity 2019.3.0f6
This is FOV-based visibility system with one target, many agents and one common manager. It's capable of detecting if target is in the proximity range of an agent, if it's in agent's FOV and if it's obscured by obstacle.

## How to use
The usage is dead simple:
1. Add <b>AgentsManager.cs</b> script to desired game object that'll act as a manager container;
2. Add <b>AgentManagerBasePreferences.cs</b> to AgentsManager script. It contains base setting for visibility system: start on play, maximum visibility check distance and obstacle mask;
3. Add <b>IAgentTarget</b> interface to any script on any game object and implement it. This'll describe this object as a detectable target for agents.*
4. Add <b>IAgent</b> interface to any game object and implement it. This'll turn this object into agent that's able to detect our target. **
5. Configure agent by specifying <b>DetectionDistance</b> and <b>FOV</b>. 

<b>*</b>: For the sake of presentation you can simply add <b>TestAgentTarget.cs</b> to desired game object. This script already contains <IAgentTarget</b> interface implementation and adds basic movement for you to play with.

<b>**</b>: You can add <b>TestAgent.cs</b> script to desired game objects. This one also comes with <b>IAgent</b> interface implementation and gizmos to add some visualization. Don't forget to enable gizmos in editor!
