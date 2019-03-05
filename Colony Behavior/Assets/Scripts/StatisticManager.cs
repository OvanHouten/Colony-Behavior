using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticManager : MonoBehaviour {
	private StatisticManager instance;
	public StatisticManager Instance { get { return instance; } }

	private int visited_cells;

	void Awake() {
		instance = this;
	}

	void OnDestroy() {
		instance = null;
	}

	// Start is called before the first frame update
	void Start() {
		visited_cells = 0;
    }

    // Update is called once per frame
    void Update() {
        
    }

	private void AddVisitedCell() {
		visited_cells++;
	}
}
