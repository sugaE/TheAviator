﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DrawCylinder : MonoBehaviour {
    // how many segments do we want in between;
    public int segmentsHeight = 1; //
    public int segmentsRadial = 3;
    // public bool addStandardMaterial = true;
    int stepH = 1;
    int radiusRound = 1;

    List<Vector3> vertices;
    List<int> triangles;

    // Start is called before the first frame update
    void Start()
    {
        makeMeshData();
        createMesh();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void makeMeshData()
    {

        //prepare
        List<Vector3> xzs = new List<Vector3>(segmentsRadial + 1);
        float tranferToCenterY = -segmentsHeight / 2;

        // from 0 to pi,
        float stepR = Mathf.PI * 2 / segmentsRadial;
        for (int i = 0; i < segmentsRadial; i++) {
            float angle = stepR * i;
            xzs.Add(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radiusRound);
        }
        //connect the first with last
        //add back origin point so in for loop it dose not overflow
        xzs.Add(new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)) * radiusRound);

        //
        //start generation
        vertices = new List<Vector3>();
        //faces: basicly from 0 to segmentsHeight*segmentsRound-1
        triangles = Enumerable.Range(0, (segmentsHeight * segmentsRadial) * 6).ToList();

        // only need to generate side
        for (int j = 0; j < segmentsHeight; j++) {
            for (int i = 0; i < segmentsRadial; i++) {
                //order matters
                //points
                vertices.Add(xzs[i] + Vector3.up * (j + tranferToCenterY));
                vertices.Add(xzs[i + 1] + Vector3.up * (j + 1 + tranferToCenterY));
                vertices.Add(xzs[i + 1] + Vector3.up * (j + tranferToCenterY));

                vertices.Add(xzs[i] + Vector3.up * (j + tranferToCenterY));
                vertices.Add(xzs[i] + Vector3.up * (j + 1 + tranferToCenterY));
                vertices.Add(xzs[i + 1] + Vector3.up * (j + 1 + tranferToCenterY));

                // int start = segmentsRound * j + i;
                // triangles.AddRange(Enumerable.Range(start * 3, 3));
            }

        }
    }

    void createMesh()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        // if (addStandardMaterial) {
        //     MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        //     meshRenderer.material = new Material(Shader.Find("Standard"));
        // }

        meshFilter.mesh.Clear();
        meshFilter.mesh.vertices = vertices.ToArray();
        meshFilter.mesh.triangles = triangles.ToArray();

        meshFilter.mesh.RecalculateNormals();
        // gameObject.transform.localScale = Vector3.one * 1000;
    }

}