using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrating_Particles : MonoBehaviour {
	public static float agent_size;
	public static float personal_range_slider;
	public static float personal_range;
	public static float flock_range_slider;
	public static float flock_range;
	public static float stepsize;
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
		Vector3 new_position = agent_position;
		Vector3 best_position = agent_position;
		Vector3 new_direction;
		float distance;
		float min_pher = pheromone_level;
		float new_pher_level;

		// We loop through all agents, but ignore the agents outside flock range
		foreach (GameObject other_agent in AgentHelper.agents) {
			// calculate distance between this.agent and other agent.
			distance = Vector3.Distance(agent_position, other_agent.transform.position);

			if (distance < flock_range) {
				// move away from other agent
				if (distance < personal_range) {
					new_direction = Vector3.Normalize(agent_position - other_agent.transform.position);
					new_position = new_position + new_direction * stepsize;
				}
				// move closer to other agent
				else {
					new_direction = Vector3.Normalize(other_agent.transform.position - agent_position);
					new_position = new_position + new_direction * stepsize;
				}

				// The best position is wherever the pheromones are lowest, this is where the agent will end up moving.
				new_pher_level = other_agent.gameObject.GetComponent<Vibrating_Particles>().pheromone_level;
				if (new_pher_level < min_pher) {
					min_pher = new_pher_level;
					best_position = new_position;
				}
			}
		}

		// We want to make sure the agents dont move out of bounds, if this is the case, move the agent
		if (IsAllowed(best_position)) {
			transform.position = best_position;
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
