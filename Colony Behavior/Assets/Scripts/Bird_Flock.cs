using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Flock : MonoBehaviour {
	public static float agent_size = 1.0f;
	public static float personal_range = 3.0f;
	public static float flock_range = 6.0f;
	public static float stepsize = 1f;
	public float confidence = 0; // to be calculated, starts at zero, is public so other agents can communicate it to eachother
	float pheromone_level;
	Vector3 movement;

	private void FixedUpdate() {
		Vector3 agent_position = transform.position;

		// Find other agents whithin range
		Collider[] all_colliders = Physics.OverlapSphere(agent_position, flock_range);
		List<Collider> agents_list = new List<Collider>();
		foreach (Collider c in all_colliders) {
			if (c.name.Contains("Agent") && c.name != name) {
				agents_list.Add(c);
			}
		}

		float distance;
		Vector3 new_position = agent_position;
		Vector3 new_direction;
		Collider most_confident = this.GetComponent<Collider>(); // is most confident as itself at the start

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
				if (confidence < other_agent.gameObject.GetComponent<Bird_Flock>().confidence) {
					most_confident = other_agent;
				}
			}
		}

		if (most_confident != this.GetComponent<Collider>()) {
			new_direction = Vector3.Normalize(agent_position - most_confident.transform.position);
			new_position = new_position + new_direction * stepsize;
		}

		if (IsAllowed(new_position)) {
			transform.position = new_position;
			// build confidence based on pheromone levels at agent_position
			// build up extra confidence for being able to move
		}

		pheromone_level = 0f;
	}

	// control the agents so they dont go out of bounds
	private bool IsAllowed(Vector3 pos) {
		float radius = agent_size / 2;
		Vector3 world_bounds = GameObject.Find("BoundingBox").transform.lossyScale / 2;

		world_bounds.x = world_bounds.x - radius;
		world_bounds.y = world_bounds.y - radius;
		world_bounds.z = world_bounds.z - radius;

		if (pos.x > world_bounds.x || pos.x < -world_bounds.x) {
			return false;
		}
		if (pos.y > world_bounds.y || pos.y < -world_bounds.y) {
			return false;
		}
		if (pos.z > world_bounds.z || pos.z < -world_bounds.z) {
			return false;
		}

		return true;
	}

	// Change size of agent through a slider
	public void SetAgentSize(float new_size) {
		agent_size = new_size;
		// update size in rendering?
	}

	// Change range of personal radius through a slider
	public void SetPersonalRange(float new_range) {
		personal_range = new_range;
	}

	// Change range of flock radius through a slider
	public void SetFlockRange(float new_range) {
		flock_range = new_range;
	}

	// Change size of steps taken by agents every frame
	public void SetStepSize(float new_stepsize) {
		stepsize = new_stepsize;
	}

	// Set Pheromone level of the agents, used by pheromones to update the value of the agent
	public void SetPheromoneLevel(float value) {
		pheromone_level += value;
	}
}
