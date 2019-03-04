using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrating_Particles : MonoBehaviour
{
	float agent_size = 1.0f;
	float personal_range = 3.0f;
	float flock_range = 6.0f;
	float stepsize = 0.01f;
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
			if (!c.name.Contains("Pheromone") && c.name != name) {
				agents_list.Add(c);
			}
		}

		float distance;
		float min_pheromones;
		Vector3 new_position;
		Vector3 new_direction;
		Vector3 best_position;
		
		//drop pheromones (happens within Pheromone code through collisions)
		new_position = agent_position;
		best_position = new_position;
		//min_pheromones = ; //value of pheromones at agent position
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

		//if (best_position is allowed) { //new position is in bounding box (handled through collision right now)
			transform.position = new_position;
		//}
	}

	//private void ondrawgizmos() {
	//	gizmos.color = color.red;
	//	//use the same vars you use to draw your overlap sphere to draw your wire sphere.
	//	gizmos.drawwiresphere(transform.position, flock_range);
	//}
}


