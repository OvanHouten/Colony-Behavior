using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// UNFINISHED: It has not had the performance optimizations Vibrating Particles have, and confidence is being updated ate any point
// in the algorithm, meaning it stays at 0 the whole time
public class Bird_Flock : MonoBehaviour {
	public static float agent_size;
	public static float personal_range_slider;
	public static float personal_range;
	public static float flock_range_slider;
	public static float flock_range;
	public static float stepsize;
	public float confidence = 0; // to be calculated, starts at zero, is public so other agents can communicate it to eachother
	float pheromone_level;
	Vector3 movement;

	// Default settings Taken from the paper, this means its not optimized for 3D
	private void Start() {
		agent_size = 1.0f;
		personal_range = agent_size + 5.0f;
		flock_range = personal_range + 5.0f;
		stepsize = 1.0f;
	}

	// Main update loop for agents using the Vibrating particle model
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
		Collider most_confident = this.GetComponent<Collider>(); // is most confident in itself at the start

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

		// After we found the most confident agent (that isnt the agent self) we move closer to thsi agent
		if (most_confident != this.GetComponent<Collider>()) {
			new_direction = Vector3.Normalize(agent_position - most_confident.transform.position);
			new_position = new_position + new_direction * stepsize;
		}

		// We want to make sure the agents dont move out of bounds, if this is the case, move the agent
		if (IsAllowed(new_position)) {
			transform.position = new_position;
			// TODO
			// build confidence based on pheromone levels at agent_position
			// build up extra confidence for being able to move
		}

		// The pheromone level will be recalculated next frame
		pheromone_level = 0f;
	}

	// Control the agents so they dont go out of bounds
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
		personal_range = agent_size + personal_range_slider;
		flock_range = personal_range + flock_range_slider;
	}

	// Change range of personal radius through a slider
	public void SetPersonalRange(float new_range) {
		personal_range_slider = new_range;
		personal_range = agent_size + personal_range_slider;
		flock_range = personal_range + flock_range_slider;
	}

	// Change range of flock radius through a slider
	public void SetFlockRange(float new_range) {
		flock_range_slider = new_range;
		flock_range = personal_range + flock_range_slider;
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
