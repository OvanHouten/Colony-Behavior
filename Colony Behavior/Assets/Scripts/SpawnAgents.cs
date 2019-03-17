using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAgents : MonoBehaviour {
	static int numAgents = 30; // default setting from paper
	static int spawnRadius = 5;
	static float agent_size = 1.0f; //default setting from paper

	// Start is called before the first frame update
	void Start() {
		Vector3 position;
		Vector3 offset = new Vector3(0, 0, 0); // for if spawn shouldnt be in the middle. WARNING: DOES NOT CHECK IF AGENTS SPAWN OUT OF BOUNDS

		for (int i = 0; i < numAgents; i++) {
			position = (spawnRadius * Random.insideUnitSphere) + offset;
			AgentHelper.MakeAgent(position, agent_size).SetActive(true);
		}
    }
}
