﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPheromones : MonoBehaviour {
	private int bound_x;
	private int bound_y;
	private int bound_z;

    // Start is called before the first frame update
    void Start() {
		// Bounds are calculated through the scale of the world.
		// This has not been made foolproof and therefor should be an even integer, otherwise it might break stuff
		bound_x = (int)(transform.lossyScale.x / 2);
		bound_y = (int)(transform.lossyScale.y / 2);
		bound_z = (int)(transform.lossyScale.z / 2);

		for (int i = -bound_x; i < bound_x; i++) {
			for (int j = -bound_y; j < bound_y; j++) {
				for (int k= -bound_z; k < bound_z; k++) {
					PheromoneHelper.MakePheromone(i + 0.5f, j + 0.5f, k + 0.5f);
				}
			}
		}
    }
}
