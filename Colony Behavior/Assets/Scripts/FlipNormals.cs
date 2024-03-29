﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script flips the normal vectors inside out of the bounding box cube, so you can look inside it from all angles
public class FlipNormals : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
		Mesh mesh = this.GetComponent<MeshFilter>().mesh;

		Vector3[] normals = mesh.normals;
		for (int i = 0; i < normals.Length; i++)
			normals[i] = -1 * normals[i];

		for (int i = 0; i < mesh.subMeshCount; i++) {

			int[] tris = mesh.GetTriangles(i);
			for (int j = 0; j < tris.Length; j += 3) {
				//swap order of tri vertices
				int temp = tris[j];
				tris[j] = tris[j + 1];
				tris[j + 1] = temp;
			}

			mesh.SetTriangles(tris, i);
		}
	}
}
