using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/around_static")]
public class around_static : Randomizer
{
    // objects variables initialisation
    public GameObjectParameter prefabs_mat1;
    public GameObjectParameter prefabs_mat2;
    private GameObject currentInstance1;
    private GameObject currentInstance2;
    // camera selection 
    public Camera mainCamera;
    // material variables initialisation
    public Material material1;
    public Material material2;


    protected override void OnIterationStart()
    {   
        // these are the surrounding objects 
        currentInstance1=prefabs_mat1.Sample();
        // currentInstance1=GameObject.Instantiate(prefabs_mat1.Sample());
        // Vector2 randomPos1 = Random.insideUnitCircle.normalized * 2;
        // Vector3 spawnPoint1 = new Vector3(randomPos1.x,0,randomPos1.y); //first surround object spawned anywhere along the circumference of circle of radius 3
        // currentInstance1.transform.position = spawnPoint1;
        // currentInstance1.transform.rotation = Random.rotation;

        // float radius = Random.Range(3,8);
        // Vector3 center = Vector3.zero;
        // int objectCount = Random.Range(3,7); // "central" objects
        // for (int i = 0; i < objectCount; i++)
        // {
        //     float angle = i * Mathf.PI * 2f / objectCount;
        //     Vector3 pos = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius + center;
        //     GameObject.Instantiate(currentInstance1,pos,Random.rotation);
        // }


        // for (int loop=0;loop<objectCount;loop++)
        // {
        //     float randomPoint = Random.Range(3,8); // possible range of circle radius for new surround objects to spawn along
        //     Vector2 randomPos3 = Random.insideUnitCircle.normalized * randomPoint;
        //     Vector3 spawnPoint3 = new Vector3(randomPos3.x,0,randomPos3.y);
        //     if (!Physics.CheckSphere(spawnPoint3,0.5f))
        //     {
        //         GameObject.Instantiate(currentInstance1,spawnPoint3,Random.rotation);
        //     }
        // }

        // this is the central object
        currentInstance2=GameObject.Instantiate(prefabs_mat2.Sample());
        Vector2 randomPos2 = Random.insideUnitCircle;
        Vector3 spawnPoint2 = new Vector3(randomPos2.x,0,randomPos2.y); //first surround object spawned anywhere along the circumference of circle of radius 3
        currentInstance2.transform.position = spawnPoint2; //central object spawned anywhere within a circle of radius 2
        currentInstance2.transform.rotation = Random.rotation;

        // camera randomisation code
        mainCamera.transform.position = Random.onUnitSphere * 10;
        while (mainCamera.transform.position.y<10)
        {
            mainCamera.transform.position = Random.onUnitSphere * 10;
        }
        mainCamera.transform.LookAt(new Vector3(0,0,0)); // can set a manual point to look at
        // material randomisation code
        material1.color = Random.ColorHSV();
        material2.color = Random.ColorHSV();
    }


    protected override void OnIterationEnd()
    {
        GameObject[] GameObjects = (GameObject.FindObjectsOfType<GameObject>() as GameObject[]);
        for (int i = 0; i < GameObjects.Length; i++)
        {
            if (GameObjects[i].name==currentInstance1.name || GameObjects[i].name==currentInstance1.name+"(Clone)" || GameObjects[i].name==currentInstance2.name || GameObjects[i].name==currentInstance2.name+"(Clone)")
            {
                GameObject.Destroy(GameObjects[i]);
            }
        }
    }
}