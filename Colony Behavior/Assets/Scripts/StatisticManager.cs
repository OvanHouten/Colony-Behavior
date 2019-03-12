using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : MonoBehaviour {
	private static StatisticManager m_Instance;
	public static StatisticManager Instance { get { return m_Instance; } }

	private int visited_cells;
	private int iterations;

	void Awake() {
		m_Instance = this;
	}

	void Start() {
		visited_cells = 0;
		iterations = 0;
    }

    void Update() {
		iterations++;
		//print("time" + iterations);
		//print("cells" + visited_cells);
    }

	public void AddVisitedCell() {
		visited_cells++;
	}

	void OnDestroy() {
		m_Instance = null;
	}
}
