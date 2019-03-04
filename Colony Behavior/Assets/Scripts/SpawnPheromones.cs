using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPheromones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
		for (int i = -4; i < 5; i++) {
			for (int j = -4; j < 5; j++) {
				for (int k= -4; k < 5; k++) {
					PheromoneHelper.MakePheromone(i, j, k);
				}
			}
		}
        
    }
}
