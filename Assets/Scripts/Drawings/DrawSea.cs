﻿using UnityEngine;
using UnityEngine.Rendering;

public class DrawSea : MonoBehaviour {
    // Start is called before the first frame update
    GameObject sea;

    void Start()
    {

        createSky();
        sea = createSea();

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region drawing
    GameObject createSky()
    {
        GameObject skyGo = PrimitiveHelper.CreatePrimitive(PrimitiveType.Quad, false, "Sky");
        // Mesh meshCube =  PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube);

        skyGo.transform.position = new Vector3(0, 100, 790);
        skyGo.transform.localScale = new Vector3(1700, 1200, 1);
        skyGo.transform.parent = gameObject.transform;

        //9) Give it a Material
        // Material material = new Material(PrimitiveHelper.GetMaterialStandard());
        Material skyMa = new Material(Shader.Find("Aviator/LinearGradientColor"));
        skyMa.SetColor("_color1", AviatorColors.Sky);
        skyMa.SetColor("_color2", AviatorColors.Fog);
        MeshRenderer skyMr = skyGo.GetComponent<MeshRenderer>();
        skyMr.material = skyMa;
        skyMr.shadowCastingMode = ShadowCastingMode.Off;
        skyMr.receiveShadows = false;

        skyGo.isStatic = true;

        return skyGo;
    }

    GameObject createSea()
    {// parent = new GameObject("Sea");
        // parent.transform.parent = gameObject.transform;

        //1) Create an empty GameObject with the required Components
        // GameObject seaGo = PrimitiveHelper.CreatePrimitive(PrimitiveType.Cylinder, false, "Sea");
        GameObject seaGo = new GameObject("Sea");
        DrawCylinder cylinder = seaGo.AddComponent<DrawCylinder>();
        cylinder.segmentsHeight = 10;
        cylinder.segmentsRadial = 40;
        // Mesh meshCube =  PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube);

        seaGo.transform.position = Vector3.down * Aviator.seaRadius;
        seaGo.transform.Rotate(Vector3.right * -90);
        // seaGo.transform.localScale = new Vector3(1200, 400, 1200); // for unity built-in PrimitiveType.Cylinder
        // remember to divide y by cylinder.segmentsHeight
        seaGo.transform.localScale = new Vector3(Aviator.seaRadius, Aviator.seaLength / cylinder.segmentsHeight, Aviator.seaRadius); // for self-built DrawCylinder
        seaGo.transform.parent = gameObject.transform;

        //9) Give it a Material
        MeshRenderer seaMr = seaGo.AddComponent<MeshRenderer>();
        Material seaMa = seaMr.material;
        seaMa.SetColor("_Color", AviatorColors.Blue);
        PrimitiveHelper.SetMaterialTransparent(seaMa);

        seaGo.AddComponent<Sea>();

        return seaGo;
    }
    #endregion

}
