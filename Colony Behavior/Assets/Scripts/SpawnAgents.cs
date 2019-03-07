using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAgents : MonoBehaviour {
	int numAgents = 5;
	int spawnRadius = 5;
	float agent_size = 1;

	void Start() {
		print("SLEBBA");
		AgentHelper.MakeAgent(0f, 0f, 0f, 1).SetActive(true);
		for (int i = 0; i < numAgents; i++) {
			AgentHelper.MakeAgent(spawnRadius * Random.insideUnitSphere, agent_size).SetActive(true);
		}
    }

	//Called when updating size
	void Update() {
		//get agent list from agents helper
		//for loop through list
	}
}
