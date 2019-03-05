using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PheromoneHelper : MonoBehaviour {

	private static GameObject PheromonePrefab;
	private static GameObject cubeContainer;
	private static int pheromoneCount = 0;
	private static List<GameObject> cubes;

	public static GameObject MakePheromone(float x, float y, float z) {
		return MakePheromone(x, y, z, 1);
	}

	public static GameObject MakePheromone(float x, float y, float z, float size) {
		return MakePheromone(new Vector3(x, y, z), size);
	}

	private static GameObject GetPheromonePrefab() {
		if (PheromonePrefab == null)
			PheromonePrefab = Resources.Load("Pheromone") as GameObject;
		return PheromonePrefab;
	}

	public static GameObject MakePheromone(Vector3 position, float size) {
		pheromoneCount++;
		if (cubeContainer == null) {
			cubeContainer = new GameObject("cube container");
			cubes = new List<GameObject>();
		}

		GameObject pheromone = Instantiate(GetPheromonePrefab()) as GameObject;
		cubes.Add(pheromone);
		pheromone.transform.position = position;
		pheromone.transform.parent = cubeContainer.transform;
		pheromone.name = "Pheromone " + pheromoneCount;

		pheromone.transform.localScale = new Vector3(size, size, size);

		return pheromone;
	}
}