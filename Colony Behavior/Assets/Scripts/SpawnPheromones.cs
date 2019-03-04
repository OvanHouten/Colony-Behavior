using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPheromones : MonoBehaviour
{
	private int bound_x;
	private int bound_y;
	private int bound_z;

    // Start is called before the first frame update
    void Start() {
		for (int i = -5; i < 10; i++) {
			for (int j = -5; j < 10; j++) {
				for (int k= -5; k < 10; k++) {
					PheromoneHelper.MakePheromone(i, j, k);
				}
			}
		}
    }
}
