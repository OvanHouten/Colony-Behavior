using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_behavior_simple : MonoBehaviour
{
	Vector3 movement;
	
	private void Start() {
		movement = new Vector3(1f * Time.deltaTime, 0f, 0f);
		print("sldnfs");
	}
	// Update is called once per frame
	void Update()
    {
		transform.Translate(movement);
	}

	private void OnTriggerEnter(Collider other) {
		print("kjf");
		movement = -movement;
	}

}
