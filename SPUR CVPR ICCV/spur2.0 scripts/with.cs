using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/with")]
public class with : Randomizer
{
    public GameObjectParameter prefabs_mat1;
    public GameObjectParameter prefabs_mat2;
    private GameObject currentInstance1;
    private GameObject currentInstance2;
    private float timeCounter;
    private float speed;
    private float randomX;
    private float randomY;
    private float randomZ;
    private Vector3 cameraFocus;
    public Camera mainCamera;
    public Material material1;
    public Material material2;

    protected override void OnIterationStart()
    {
        timeCounter = 0;
        speed = Random.Range(2,3);
        currentInstance1 = GameObject.Instantiate(prefabs_mat1.Sample());
        currentInstance2 = GameObject.Instantiate(prefabs_mat2.Sample());
        currentInstance1.transform.localScale = Vector3.one;
        currentInstance2.transform.localScale = Vector3.one;
        currentInstance1.transform.position = new Vector3(4,1,0);
        currentInstance2.transform.position = new Vector3(4,-1,0);
        mainCamera.transform.position = Random.onUnitSphere * 10;
        randomX = Random.Range(-2,2);
        randomY = Random.Range(-2,2);
        randomZ = Random.Range(-2,2);
        cameraFocus = new Vector3(randomX,randomY,randomZ);
        mainCamera.transform.LookAt(cameraFocus);
        // mainCamera.transform.LookAt(new Vector3(0,0,0));
        material1.color = Random.ColorHSV();
        material2.color = Random.ColorHSV();
    }

    protected override void OnUpdate()
    {
        timeCounter+=Time.deltaTime*speed;
        float x1 = 4 - timeCounter;
        float y1 = currentInstance1.transform.position.y;
        float z1 = currentInstance1.transform.position.z;
        currentInstance1.transform.position = new Vector3(x1,y1,z1);

        float x2 = 4 - timeCounter;
        float y2 = currentInstance2.transform.position.y;
        float z2 = currentInstance2.transform.position.z;
        currentInstance2.transform.position = new Vector3(x2,y2,z2);
    }

    protected override void OnIterationEnd()
    {
        GameObject.Destroy(currentInstance1);
        GameObject.Destroy(currentInstance2);
    }
}