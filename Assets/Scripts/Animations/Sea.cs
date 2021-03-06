﻿
using System.Collections.Generic;
using UnityEngine;

struct Wave {
    public float x;
    public float y;
    public float z;
    public float ang;
    public float amp;
    public float speed;
    public Wave(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
        ang = Random.Range(0, Mathf.PI * 2); // a random angle
        amp = .01f + Random.Range(0, .03f); // a random distance 5f-20f
        speed = .016f + Random.Range(0, .032f); // a random speed between 0.016 and 0.048 radians / frame
    }
}

[RequireComponent(typeof(MeshFilter), typeof(DrawCylinder))]
public class Sea : MonoBehaviour {
    Mesh mesh;
    public Vector3[] vertices;
    List<int>[] points;
    List<Wave> waves = new List<Wave>();

    // Start is called before the first frame update
    void Start()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        DrawCylinder cylinder = gameObject.GetComponent<DrawCylinder>();

        if (cylinder.isDiscrete) {
            points = cylinder.groupSamePoint();
            for (int i = 0; i < points.Length; i++) {
                waves.Add(new Wave(vertices[points[i][0]]));
            }
        } else {

            for (int i = 0; i < vertices.Length; i++) {
                waves.Add(new Wave(vertices[i]));
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (points != null) {
            moveWave();
        } else {
            moveWaveCon();
        }

    }
    void moveWaveCon()
    {
        print("moveWaveCon");

        for (int i = 0; i < vertices.Length; i++) {
            Wave wave = waves[i];
            vertices[i] = new Vector3(wave.x + Mathf.Cos(wave.ang) * wave.amp, //wave range
                                        wave.y,
                                        wave.z + Mathf.Sin(wave.ang) * wave.amp //wave height
                                            );
            // for (int j = 0; j < points[i].Count; j++) {
            //     int index = points[i][j];
            // update the position of the vertex
            // vertices[i] = pos;

            // }
            // increment the angle for the next frame
            wave.ang += wave.speed;
            waves[i] = wave;
        }
        // mesh.Clear();
        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        gameObject.transform.Rotate(Vector3.down * Aviator.speed);// .005;* Time.deltaTime
        //   sea.mesh.rotation.z += game.speed * deltaTime;//*game.seaRotationSpeed;
    }
    void moveWave()
    {

        for (int i = 0; i < points.Length; i++) {
            Wave wave = waves[i];
            Vector3 pos = new Vector3(wave.x + Mathf.Cos(wave.ang) * wave.amp, //wave range
                                        wave.y,
                                        wave.z + Mathf.Sin(wave.ang) * wave.amp //wave height
                                            );
            for (int j = 0; j < points[i].Count; j++) {
                int index = points[i][j];
                // update the position of the vertex
                vertices[index] = pos;

            }
            // increment the angle for the next frame
            wave.ang += wave.speed;
            waves[i] = wave;
        }
        // mesh.Clear();
        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        gameObject.transform.Rotate(Vector3.down * Aviator.speed);// .005;* Time.deltaTime
        //   sea.mesh.rotation.z += game.speed * deltaTime;//*game.seaRotationSpeed;
    }
}
