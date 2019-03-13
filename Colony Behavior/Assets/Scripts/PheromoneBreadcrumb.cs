using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PheromoneBreadcrumb : MonoBehaviour
{
	MeshRenderer meshRenderer;
	float value;
	float evaporation_rate;
	float drop_rate;

	// Start is called before the first frame update
    void Start()
    {
		value = 0f;
		evaporation_rate = 0.1f;
		drop_rate = 1f;
		meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.material.SetColor("_Color", new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
		meshRenderer.enabled = false;
	}

	// Update is called once per frame
	void Update() {
		float a_value = (value / 100f);
		meshRenderer.material.SetColor("_Color", new Vector4(0.0f, 0.0f, 0.0f, a_value));

		value -= evaporation_rate;
		if (value < 0.0f) {
			value = 0.0f;
		}
	}

	// Dont render when pheromone has not been active
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
}
