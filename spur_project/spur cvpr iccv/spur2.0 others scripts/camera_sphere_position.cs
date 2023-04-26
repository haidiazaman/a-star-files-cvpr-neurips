using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/camera_position")]
public class camera_sphere_position : Randomizer
{

    // objects variables initialisation
    public GameObjectParameter prefabs_mat1;
    public GameObjectParameter prefabs_mat2;
    private GameObject currentInstance1;
    private GameObject currentInstance2;

    // camera variables initialisation
    public Camera mainCamera;


    protected override void OnIterationStart()
    {   

        currentInstance1=GameObject.Instantiate(prefabs_mat1.Sample());
        currentInstance1.transform.position = new Vector3(-2,0,0);
        currentInstance2=GameObject.Instantiate(prefabs_mat2.Sample());
        currentInstance2.transform.position = new Vector3(2,0,0);

        // camera randomisation code
        mainCamera.transform.position = Random.onUnitSphere * 10;
        // mainCamera.transform.LookAt(currentInstance1.transform); // look at a gameobject position
        mainCamera.transform.LookAt(new Vector3(0,0,0)); // can set a manual point to look at
    }

    protected override void OnIterationEnd()
    {
        GameObject.Destroy(currentInstance1);
        GameObject.Destroy(currentInstance2);
    }
}