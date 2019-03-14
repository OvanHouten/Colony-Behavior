using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAgents : MonoBehaviour {
	int numAgents = 10;
	int spawnRadius = 5;
	float agent_size = 1;

	void Start() {
		Vector3 position;
		Vector3 offset = new Vector3(0, 0, 0); // for if spawn shouldnt be in the middle. WARNING: DOES NOT CHECK IF AGENTS SPAWN OUT OF BOUNDS

		for (int i = 0; i < numAgents; i++) {
			position = (spawnRadius * Random.insideUnitSphere) + offset;
			AgentHelper.MakeAgent(position, agent_size).SetActive(true);
		}
    }
}
