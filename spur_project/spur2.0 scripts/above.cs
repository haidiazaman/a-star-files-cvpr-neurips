using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/above")]
public class above : Randomizer
{
    // prefabs selection
    public GameObjectParameter prefabs_mat1;
    public GameObjectParameter prefabs_mat2;
    private GameObject currentInstance1;
    private GameObject currentInstance2;
    // private float random_x;
    // private float random_y;
    // private float random_z;
    // camera selection 
    public Camera mainCamera;
    public float sphere_limit=5f;
    // materials selection
    public Material material1;
    public Material material2;

    protected override void OnAwake()
    {
        // Set the random seed value for the randomizer
        Random.InitState(1000);
    }

    protected override void OnIterationStart()
    {   
        // object variables code
        float random_x = Random.Range(0,1.5f);
        float random_y = Random.Range(1,2);
        float random_z = Random.Range(0,1.5f);
        float random_rotation_y1 = Random.Range(0,360);
        float random_rotation_y2 = Random.Range(0,360);
        float random_scale1 = Random.Range(1,1.25f);
        float random_scale2 = Random.Range(1,1.25f);

        currentInstance1=GameObject.Instantiate(prefabs_mat1.Sample());
        Vector3 random_position1 =  new Vector3(random_x,random_y,random_z);
        currentInstance1.transform.position = random_position1;
        Quaternion originalRotation1 = currentInstance1.transform.rotation;
        Quaternion newRotation1 = Quaternion.Euler(originalRotation1.eulerAngles.x, random_rotation_y1, originalRotation1.eulerAngles.z);
        currentInstance1.transform.rotation = newRotation1;
        currentInstance1.transform.localScale = Vector3.one * random_scale1;

        currentInstance2=GameObject.Instantiate(prefabs_mat2.Sample());
        Vector3 random_position2 =  new Vector3(random_x,-random_y,random_z);
        currentInstance2.transform.position = random_position2;
        Quaternion originalRotation2 = currentInstance2.transform.rotation;
        Quaternion newRotation2 = Quaternion.Euler(originalRotation2.eulerAngles.x, random_rotation_y2, originalRotation2.eulerAngles.z);
        currentInstance2.transform.rotation = newRotation2;
        currentInstance2.transform.localScale = Vector3.one * random_scale2;
        

        // camera randomisation code
        mainCamera.transform.position = Random.onUnitSphere * 10;
        // formula for sphere x2+y2+z2=r2, r=10: x2+y2+z2=100
        // set a limit, e.g. limit=7.5
        while (Mathf.Abs(mainCamera.transform.position.y)>sphere_limit)
        {
            mainCamera.transform.position = Random.onUnitSphere * 10;
        }
        Debug.Log(mainCamera.transform.position);
        // mainCamera.transform.LookAt(currentInstance1.transform); //look at gameobject position
        mainCamera.transform.LookAt(new Vector3(0,0,0)); // can set a manual point to look at
        mainCamera.fieldOfView = 35f;


        // material randomisation code
        material1.color = Random.ColorHSV();
        material2.color = Random.ColorHSV();
    }

    protected override void OnIterationEnd()
    {
        GameObject.Destroy(currentInstance1);
        GameObject.Destroy(currentInstance2);
    }

}

        // while (Mathf.Abs(mainCamera.transform.position.x)>sphere_limit | Mathf.Abs(mainCamera.transform.position.y)>sphere_limit | Mathf.Abs(mainCamera.transform.position.z)>sphere_limit)
        // {
        //     mainCamera.transform.position = Random.onUnitSphere * 10;
        // }