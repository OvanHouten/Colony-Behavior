using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrating_Particles : MonoBehaviour {
	float agent_size = 1.0f;
	float personal_range = 3.0f;
	float flock_range = 6.0f;
	float stepsize = 0.1f;
	float pheromone_level;
	Vector3 movement;

	private void Start() {
		Rigidbody rbody = GetComponent<Rigidbody>();
		rbody.isKinematic = false;
		//rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}

	private void FixedUpdate() {
		Vector3 agent_position = transform.position;

		// Find other agents whithin range
		Collider[] AllColliders = Physics.OverlapSphere(agent_position, flock_range);
		List<Collider> agents_list = new List<Collider>();
		foreach (Collider c in AllColliders) {
			if (c.name.Contains("Agent") && c.name != name) {
				agents_list.Add(c);
			}
		}

		float distance;
		float min_pher = pheromone_level;
		float new_pher_level = 0;
		Vector3 new_position = agent_position;
		Vector3 new_direction;
		Vector3 best_position = agent_position;

		// dropping pheromones happens through collision detection.
		foreach (Collider other_agent in agents_list) {
			// calculate distance between this.agent and other agent.
			distance = Vector3.Distance(agent_position, other_agent.transform.position);

			// move away from other agent
			if (distance < personal_range) {
				new_direction = Vector3.Normalize(agent_position - other_agent.transform.position);
				new_position = new_position + new_direction * stepsize;
			}
			// move closer to other agent
			else if (distance < flock_range) {
				new_direction = Vector3.Normalize(other_agent.transform.position - agent_position);
				new_position = new_position + new_direction * stepsize;
			}

			// move to the position with the highest pheromones
			new_pher_level = other_agent.gameObject.GetComponent<Vibrating_Particles>().pheromone_level;
			if (new_pher_level < min_pher) {
				min_pher = new_pher_level;
				best_position = new_position;
			}
		}
		
		if (isAllowed(best_position)) {
			transform.position = best_position;
		}
		pheromone_level = 0f;
	}

	private bool isAllowed(Vector3 pos) {
		float radius = agent_size / 2;
		Vector3 worldBounds = GameObject.Find("BoundingBox").transform.lossyScale / 2;

		worldBounds.x = worldBounds.x - radius;
		worldBounds.y = worldBounds.y - radius;
		worldBounds.z = worldBounds.z - radius;

		if (pos.x > worldBounds.x || pos.x < -worldBounds.x) {
			return false;
		}
		if (pos.y > worldBounds.y || pos.y < -worldBounds.y) {
			return false;
		}
		if (pos.z > worldBounds.z || pos.z < -worldBounds.z) {
			return false;
		}

		return true;
	}

	public void setPheromoneLevel(float value) {
		pheromone_level += value;
	}

	//private void ondrawgizmos() {
	//	gizmos.color = color.red;
	//	//use the same vars you use to draw your overlap sphere to draw your wire sphere.
	//	gizmos.drawwiresphere(transform.position, flock_range);
	//}
}


