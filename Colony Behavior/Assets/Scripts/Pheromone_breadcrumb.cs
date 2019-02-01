using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pheromone_breadcrumb : MonoBehaviour
{
	int value;

	// Start is called before the first frame update
    void Start()
    {
		value = 0;
		//print("crumb = " + value);
    }

    // Update is called once per frame
    void Update()
    {
	}

	private void OnTriggerEnter(Collider other) {
		value = value + 1;
		//print("crumb = " + value);
	}
}
