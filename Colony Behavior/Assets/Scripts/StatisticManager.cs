using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StatisticManager : MonoBehaviour {
	private static StatisticManager m_Instance;
	private static string filename;
	private static string path;
	public static StatisticManager Instance { get { return m_Instance; } }

	private int visited_cells;
	private int iterations;

	void Awake() {
		m_Instance = this;
	}

	void Start() {
		visited_cells = 0;
		iterations = 0;
		filename = "/output.txt";
		path = Application.dataPath + filename;
		CreateOutputFile();
	}

    void FixedUpdate() {
		iterations++;

		string content = iterations + "\t\t\t" + visited_cells + "\n";
		File.AppendAllText(path, content);
		//print("time" + iterations);
		//print("cells" + visited_cells);
    }

	// if statement is commented out since we want to create a new text file every simulation
	private void CreateOutputFile() {
		//if (!File.Exists(path)) {
			File.WriteAllText(path, "Iterations \t Visited Cells\n\n");
		//}
	}

	public void AddVisitedCell() {
		visited_cells++;
	}

	void OnDestroy() {
		m_Instance = null;
	}
}
