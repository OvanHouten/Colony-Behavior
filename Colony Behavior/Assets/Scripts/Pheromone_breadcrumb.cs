using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pheromone_breadcrumb : MonoBehaviour
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
		drop_rate = 1;
		meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.material.SetColor("_Color", new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
		meshRenderer.enabled = false;
	}

	// Update is called once per frame
	void Update() {
		float a_value = (value / 100f);
		//float rgb_value = 1 - a_value;
		meshRenderer.material.SetColor("_Color", new Vector4(0.0f, 0.0f, 0.0f, a_value));

		value -= evaporation_rate;
		if (value < 0.0f) {
			value = 0.0f;
		}
	}

	private void OnTriggerStay(Collider other) {
		meshRenderer.enabled = true;
		value = value + drop_rate;

		if (value > 100.0f) {
			value = 100.0f;
		}
	}

	//Test function, remove later with button
	public void AddPheromones() {
		value += 10;
		if (value > 100) {
			value = 100;
		}
	}
}
