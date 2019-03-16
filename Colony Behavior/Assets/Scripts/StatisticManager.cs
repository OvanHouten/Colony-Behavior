using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StatisticManager : MonoBehaviour {
	private static StatisticManager m_Instance;
	public static StatisticManager Instance { get { return m_Instance; } }
	string path;
	string filename;

	private int visited_cells;
	private int iterations;
	private int total_pheromones;
	private float covarage;

	// We want a global statistic manager, and this is how we make sure there can be only one.
	void Awake() {
		m_Instance = this;
	}

	// Start is called before the first frame update
	void Start() {
		visited_cells = 0;
		iterations = 0;

		path = Application.dataPath;
		filename = "/output.txt";
		File.WriteAllText((path + filename), "Time \t % visited cells \t\t Visited cells\n\n");

		// Calculate total amount of pheromones in the world
		Vector3 world_bounds = GameObject.Find("BoundingBox").transform.lossyScale;
		total_pheromones = (int)world_bounds.x * (int)world_bounds.y * (int)world_bounds.z; // NOTE not foolproof.. if world bounds have a decimal this might get broken.
	}

	// Dump data in file every iteration
    void FixedUpdate() {
		iterations++;
		covarage = (visited_cells * 100f) / total_pheromones;
		File.AppendAllText((path + filename), iterations + "\t" + covarage + "\t\t" + visited_cells + "\n");

		if (covarage > 98f) {
			Debug.Break();
		}
	}

	public void AddVisitedCell() {
		visited_cells++;
	}

	void OnDestroy() {
		m_Instance = null;
	}
}
