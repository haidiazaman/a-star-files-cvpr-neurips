using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/into_crash")]
public class into_crash : Randomizer
{
    public GameObjectParameter prefabs_mat1;
    public GameObjectParameter prefabs_mat2;
    private GameObject currentInstance1;
    private GameObject currentInstance2;
    private Rigidbody rb;
    // camera variables initialisation
    public Camera mainCamera;
    public float sphere_limit=3f;
    // material variables initialisation
    public Material material1;
    public Material material2;
    
    protected override void OnIterationStart()
    {   
        // object variables code
        float rotationY = Random.Range(0,360);
        float ObjectScale1 = Random.Range(1,1.5f); // currentInstance1, smaller scale, 1-1.5
        currentInstance1=GameObject.Instantiate(prefabs_mat1.Sample());
        currentInstance1.transform.position = new Vector3(0,5,0);
        currentInstance1.transform.rotation = Quaternion.Euler(0,rotationY,0);
        currentInstance1.transform.localScale = Vector3.one * ObjectScale1;
        rb = currentInstance1.AddComponent<Rigidbody>();

        currentInstance2=GameObject.Instantiate(prefabs_mat2.Sample());
        currentInstance2.transform.position = new Vector3(0,0,0); 
        float scaleX = Random.Range(3,6);
        float scaleZ = Random.Range(3,6);
        currentInstance2.transform.localScale = new Vector3(scaleX,0.5f,scaleZ);
        // camera randomisation code
        mainCamera.transform.position = Random.onUnitSphere * 10;
        while (mainCamera.transform.position.y>sphere_limit | mainCamera.transform.position.y<-sphere_limit)
        {
            mainCamera.transform.position = Random.onUnitSphere * 10;
        }
        float randomX = Random.Range(-2,2);
        float randomY = Random.Range(-2,2);
        float randomZ = Random.Range(-2,2);
        Vector3 cameraFocus = new Vector3(randomX,randomY,randomZ);
        mainCamera.transform.LookAt(cameraFocus);
        Quaternion originalRotation1 = mainCamera.transform.rotation;
        Quaternion newRotation1 = Quaternion.Euler(originalRotation1.eulerAngles.x, originalRotation1.eulerAngles.y,90);
        mainCamera.transform.rotation = newRotation1;
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