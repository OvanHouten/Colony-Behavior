﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PheromoneHelper : MonoBehaviour {

	private static GameObject PheromonePrefab;
	private static GameObject cubeContainer;
	private static int pheromoneCount = 0;
	private static List<GameObject> cubes;

	public static Color GetRandomColor() {
		float r = Random.Range(0f, 1f);
		float g = Random.Range(0f, 1f);
		float b = Random.Range(0f, 1f);

		//make grey/sludge colors less likely
		for (int i = 0; i < Random.Range(1, 3); i++) {
			if (Random.Range(0, 10) > 1) {
				int a = Random.Range(0, 3);
				if (a == 0)
					r = 0;
				if (a == 1)
					g = 0;
				if (a == 2)
					b = 0;
			}
		}

		return new Color(r, g, b);
	}

	public static GameObject MakePheromone(float x, float y, float z) {
		return MakePheromone(x, y, z, Color.red, 1f);
	}

	public static GameObject MakePheromone(float x, float y, float z, Color color) {
		return MakePheromone(x, y, z, color, 1);
	}

	public static GameObject MakePheromone(float x, float y, float z, Color color, float size) {
		return MakePheromone(new Vector3(x, y, z), color, size);
	}

	private static GameObject GetPheromonePrefab() {
		if (PheromonePrefab == null)
			PheromonePrefab = Resources.Load("Pheromone") as GameObject;
		return PheromonePrefab;
	}

	public static GameObject MakePheromone(Vector3 position, Color color, float size) {
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

		//cube.GetComponent<Renderer>().material.color = color;
		pheromone.transform.localScale = new Vector3(size, size, size);

		return pheromone;
	}
}