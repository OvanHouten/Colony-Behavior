using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_behavior_simple : MonoBehaviour
{
	float agent_size = 1.0f;
	float personal_range = 3.0f;
	float flock_range = 6.0f;
	float stepsize = 0.1f;
	Vector3 movement;

	private void Start() {
		//movement = new Vector3(1f * Time.deltaTime, 0f, 0f);
	}

	private void Update() {
		Vector3 agent_position = transform.position;

		// Find other agents in range
		Collider[] AllColliders = Physics.OverlapSphere(agent_position, flock_range);
		List<Collider> agents_list = new List<Collider>();
		foreach (Collider c in AllColliders) {
			if (c.name != "Pheromone" && c.name != name) {
				agents_list.Add(c);
			}
		}

		float distance;
		Vector3 new_position;
		Vector3 new_direction;
		
		//drop pheromones
		new_position = agent_position;
		foreach (Collider other_agent in agents_list) {
			//calculate distance between this.agent and other agent.
			distance = Vector3.Distance(agent_position, other_agent.transform.position);

			// move away from other agent
			if (distance < personal_range) {
				new_direction = Vector3.Normalize(agent_position - other_agent.transform.position);
				new_position = new_position + new_direction * stepsize;
			}
			// move closer to other agent
			else if (distance < flock_range) {
				new_direction = Vector3.Normalize(other_agent.transform.position - agent_position);
				new_position = new_position + new_direction *stepsize;
			}
		}

		//if (new_position is allowed) {}
		transform.position = new_position;
		//transform.Translate(movement);
	}

	//private void ondrawgizmos() {
	//	gizmos.color = color.red;
	//	//use the same vars you use to draw your overlap sphere to draw your wire sphere.
	//	gizmos.drawwiresphere(transform.position, flock_range);
	//}
}


