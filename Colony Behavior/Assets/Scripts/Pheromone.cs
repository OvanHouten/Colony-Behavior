﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pheromone : MonoBehaviour {
	public static float evaporation_rate;
	public static float drop_rate;
	MeshRenderer meshRenderer;
	float value;

	// Start is called before the first frame update, default settings from paper
    void Start() {
		value = 0f;
		evaporation_rate = 0.3f;
		drop_rate = 15f;
		meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.material.SetColor("_Color", new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
		meshRenderer.enabled = false; // When a pheromone has not been visited yet by an agent, we dont render it.
	}

	// Update is called once per frame
	void FixedUpdate() {
		float a_value;
		if (value < 1f) {
			a_value = 0.05f; // This is so you can still see a bit of the visited pheromones
		}
		else {
			a_value = (value / 100f);
		}

		meshRenderer.material.SetColor("_Color", new Vector4(0.0f, 0.0f, 0.0f, a_value));

		value -= value * evaporation_rate;
		if (value < 0.0f) {
			value = 0.0f;
		}
	}

	// Only render the pheromone when it has been rendered for the first time
	private void OnTriggerEnter(Collider other) {
		if (!meshRenderer.enabled) {
			StatisticManager.Instance.AddVisitedCell();
		}
		meshRenderer.enabled = true;
	}

	// Update value of pheromone when in contact with agent
	private void OnTriggerStay(Collider other) {
		value = value + drop_rate;

		if (value > 100.0f) {
			value = 100.0f;
		}

		other.gameObject.GetComponent<Vibrating_Particles>().SetPheromoneLevel(value);
	}

	// Change amount of evaporation per time unit
	public void SetEvaporationRate(float new_evaporation_rate) {
		evaporation_rate = new_evaporation_rate / 100;
	}

	// change amount of pheromones dropped each time unit per agent
	public void SetDropRate(float new_droprate) {
		drop_rate = new_droprate;
	}
}
