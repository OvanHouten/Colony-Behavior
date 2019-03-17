using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHelper : MonoBehaviour {
	private static GameObject agentPrefab;
	private static GameObject agentContainer;
	private static int agentCount = 0;
	public static List<GameObject> agents;

	// Get the prefab, so we can copy the same pheromone cube many times over.
	private static GameObject GetAgentPrefab() {
		if (agentPrefab == null)
			agentPrefab = Resources.Load("AI_Agent") as GameObject;
		return agentPrefab;
	}

	public static GameObject MakeAgent(float x, float y, float z, float size) {
		return MakeAgent(new Vector3(x, y, z), size);
	}

	public static GameObject MakeAgent(Vector3 position) {
		return MakeAgent(position, 1);
	}

	// All MakePheromone functions boil down to this. Pretty self explainatory.
	public static GameObject MakeAgent(Vector3 position, float size) {
		agentCount++;
		if (agentContainer == null) {
			agentContainer = new GameObject("agent container");
			agents = new List<GameObject>();
		}

		GameObject agent = Instantiate(GetAgentPrefab()) as GameObject;
		agents.Add(agent);
		agent.transform.position = position;
		agent.transform.parent = agentContainer.transform;
		agent.name = "Agent " + agentCount;

		agent.transform.localScale = new Vector3(size, size, size);
		agent.GetComponent<Vibrating_Particles>().SetAgentSize(size);

		return agent;
	}
}
