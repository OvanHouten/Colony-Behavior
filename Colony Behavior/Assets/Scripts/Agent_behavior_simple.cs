using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_behavior_simple : MonoBehaviour
{
	Vector3 movement;
	
	private void Start() {
		movement = new Vector3(1f * Time.deltaTime, 0f, 0f);
	}
	
	void Update()
    {
		transform.Translate(movement);
	}

	private void OnTriggerStay(Collider other) {
		if (other.name == "Pheromone") {
			return;
		}
		
		//TODO find path of least resistance ultiple collisions!
		movement = -movement;
	}

}
