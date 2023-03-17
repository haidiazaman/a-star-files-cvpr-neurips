using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/around_surround")]
public class around_surround : Randomizer
{
    // objects variables initialisation
    public GameObjectParameter prefabs_mat1;
    public GameObjectParameter prefabs_mat2;
    private GameObject currentInstance1;
    private GameObject currentInstance2;
    // public float spawnCollisionCheckRadius=0.5f;   
    // camera selection 
    public Camera mainCamera;
    public float sphere_limit=7.5f;
    // material variables initialisation
    public Material material1;
    public Material material2;

    protected override void OnIterationStart()
    {   
        // these are the surrounding objects 
        currentInstance1=GameObject.Instantiate(prefabs_mat1.Sample());
        // Vector2 randomPos1 = Random.insideUnitCircle.normalized * 3;
        // Vector3 spawnPoint1 = new Vector3(randomPos1.x,0,randomPos1.y); //first surround object spawned anywhere along the circumference of circle of radius 3
        // currentInstance1.transform.position = spawnPoint1;
        currentInstance1.transform.rotation = Random.rotation;
        
        // int objectCount = Random.Range(2,7); // "central" objects
        // for (int loop=0;loop<objectCount;loop++)
        // {
        //     float randomPoint = Random.Range(3,8); // possible range of circle radius for new surround objects to spawn along
        //     Vector2 randomPos3 = Random.insideUnitCircle.normalized * randomPoint;
        //     Vector3 spawnPoint3 = new Vector3(randomPos3.x,0,randomPos3.y);
        //     if (!Physics.CheckSphere(spawnPoint3,spawnCollisionCheckRadius))
        //     {
        //         GameObject.Instantiate(currentInstance1,spawnPoint3,Random.rotation);
        //     }
        // }
        
        // this is the central object
        currentInstance2=GameObject.Instantiate(prefabs_mat2.Sample());
        // Vector2 randomPos2 = Random.insideUnitCircle * 2;
        // Vector3 spawnPoint2 = new Vector3(randomPos2.x,0,randomPos2.y); //first surround object spawned anywhere along the circumference of circle of radius 3
        // currentInstance2.transform.position = spawnPoint2; //central object spawned anywhere within a circle of radius 2

        // camera randomisation code
        mainCamera.transform.position = Random.onUnitSphere * 10;
        // formula for sphere x2+y2+z2=r2, r=10: x2+y2+z2=100
        // set a limit, e.g. limit=7.5
        while (Mathf.Abs(mainCamera.transform.position.x)<sphere_limit | Mathf.Abs(mainCamera.transform.position.y)<sphere_limit | Mathf.Abs(mainCamera.transform.position.z)<sphere_limit)
        {
            mainCamera.transform.position = Random.onUnitSphere * 10;
        }
        // mainCamera.transform.LookAt(currentInstance1.transform); //look at gameobject position
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

// using System;
// using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using Random=UnityEngine.Random;
// using UnityEngine;
// using UnityEngine.Perception.Randomization.Parameters;
// using UnityEngine.Perception.Randomization.Randomizers;

// [Serializable]
// [AddRandomizerMenu("Perception/around_surround")]
// public class around_surround : Randomizer
// {
//     // objects variables initialisation
//     public GameObjectParameter prefabs_mat1;
//     public GameObjectParameter prefabs_mat2;
//     private GameObject currentInstance1;
//     private GameObject currentInstance2;
//     public float spawnCollisionCheckRadius=0.5f;   
//     // camera selection 
//     public Camera mainCamera;
//     public float sphere_limit=7.5f;
//     // material variables initialisation
//     public Material material1;
//     public Material material2;

//     protected override void OnIterationStart()
//     {   
//         // these are the surrounding objects 
//         currentInstance1=GameObject.Instantiate(prefabs_mat1.Sample());
//         Vector2 randomPos1 = Random.insideUnitCircle.normalized * 3;
//         Vector3 spawnPoint1 = new Vector3(randomPos1.x,0,randomPos1.y); //first surround object spawned anywhere along the circumference of circle of radius 3
//         currentInstance1.transform.position = spawnPoint1;
//         currentInstance1.transform.rotation = Random.rotation;
        
//         int objectCount = Random.Range(2,7); // "central" objects
//         for (int loop=0;loop<objectCount;loop++)
//         {
//             float randomPoint = Random.Range(3,8); // possible range of circle radius for new surround objects to spawn along
//             Vector2 randomPos3 = Random.insideUnitCircle.normalized * randomPoint;
//             Vector3 spawnPoint3 = new Vector3(randomPos3.x,0,randomPos3.y);
//             if (!Physics.CheckSphere(spawnPoint3,spawnCollisionCheckRadius))
//             {
//                 GameObject.Instantiate(currentInstance1,spawnPoint3,Random.rotation);
//             }
//         }
        
//         // this is the central object
//         currentInstance2=GameObject.Instantiate(prefabs_mat2.Sample());
//         Vector2 randomPos2 = Random.insideUnitCircle * 2;
//         Vector3 spawnPoint2 = new Vector3(randomPos2.x,0,randomPos2.y); //first surround object spawned anywhere along the circumference of circle of radius 3
//         currentInstance2.transform.position = spawnPoint2; //central object spawned anywhere within a circle of radius 2

//         // camera randomisation code
//         mainCamera.transform.position = Random.onUnitSphere * 10;
//         // formula for sphere x2+y2+z2=r2, r=10: x2+y2+z2=100
//         // set a limit, e.g. limit=7.5
//         while (Mathf.Abs(mainCamera.transform.position.x)<sphere_limit | Mathf.Abs(mainCamera.transform.position.y)<sphere_limit | Mathf.Abs(mainCamera.transform.position.z)<sphere_limit)
//         {
//             mainCamera.transform.position = Random.onUnitSphere * 10;
//         }
//         // mainCamera.transform.LookAt(currentInstance1.transform); //look at gameobject position
//         mainCamera.transform.LookAt(new Vector3(0,0,0)); // can set a manual point to look at
//         // material randomisation code
//         material1.color = Random.ColorHSV();
//         material2.color = Random.ColorHSV();
//     }

//     protected override void OnIterationEnd()
//     {
//         GameObject[] GameObjects = (GameObject.FindObjectsOfType<GameObject>() as GameObject[]);
//         for (int i = 0; i < GameObjects.Length; i++)
//         {
//             if (GameObjects[i].name==currentInstance1.name || GameObjects[i].name==currentInstance1.name+"(Clone)" || GameObjects[i].name==currentInstance2.name || GameObjects[i].name==currentInstance2.name+"(Clone)")
//             {
//                 GameObject.Destroy(GameObjects[i]);
//             }
//         }
//     }
// }

// using System;
// using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using Random=UnityEngine.Random;
// using UnityEngine;
// using UnityEngine.Perception.Randomization.Parameters;
// using UnityEngine.Perception.Randomization.Randomizers;

// [Serializable]
// [AddRandomizerMenu("Perception/around_surround")]
// public class around_surround : Randomizer
// {
//     // objects variables initialisation
//     public FloatParameter centreObjectScale; // larger
//     public FloatParameter surroundObjectScale; // smaller
//     public Vector3Parameter objectRotation;
//     public GameObjectParameter prefabs_mat1;
//     public GameObjectParameter prefabs_mat2;
//     private GameObject currentInstance1;
//     private GameObject second_object;
//     private GameObject currentInstance2;
//     private GameObject currentInstance3;
//     private GameObject currentInstance4;
//     private GameObject currentInstance5;
//     private GameObject currentInstance6;
//     private GameObject currentInstance7;

//     // camera variables initialisation
//     public Camera mainCamera;
//     public FloatParameter cameraDistance;
//     public Vector3Parameter cameraRot;

//     // material variables initialisation
//     public Material material1;
//     public Material material2;

//     protected override void OnIterationStart()
//     {   
//         currentInstance1 = GameObject.Instantiate(prefabs_mat1.Sample());
//         currentInstance1.transform.rotation = Quaternion.Euler(90,0,0);
//         currentInstance1.transform.localScale = Vector3.one * centreObjectScale.Sample();   
//         currentInstance1.transform.position = new Vector3(0,0,0);

//         second_object = prefabs_mat2.Sample();
//         currentInstance2 = GameObject.Instantiate(second_object);
//         currentInstance3 = GameObject.Instantiate(second_object);
//         currentInstance4 = GameObject.Instantiate(second_object);
//         currentInstance5 = GameObject.Instantiate(second_object);
//         currentInstance6 = GameObject.Instantiate(second_object);
//         currentInstance7 = GameObject.Instantiate(second_object);

//         currentInstance2.transform.rotation = Quaternion.Euler(objectRotation.Sample());
//         currentInstance3.transform.rotation = Quaternion.Euler(objectRotation.Sample());
//         currentInstance4.transform.rotation = Quaternion.Euler(objectRotation.Sample());
//         currentInstance5.transform.rotation = Quaternion.Euler(objectRotation.Sample());
//         currentInstance6.transform.rotation = Quaternion.Euler(objectRotation.Sample());
//         currentInstance7.transform.rotation = Quaternion.Euler(objectRotation.Sample());

//         currentInstance2.transform.localScale = Vector3.one * surroundObjectScale.Sample();   
//         currentInstance3.transform.localScale = Vector3.one * surroundObjectScale.Sample();   
//         currentInstance4.transform.localScale = Vector3.one * surroundObjectScale.Sample();   
//         currentInstance5.transform.localScale = Vector3.one * surroundObjectScale.Sample();   
//         currentInstance6.transform.localScale = Vector3.one * surroundObjectScale.Sample();   
//         currentInstance7.transform.localScale = Vector3.one * surroundObjectScale.Sample();   

//         currentInstance2.transform.position = new Vector3(0,0,-2.5f);
//         currentInstance3.transform.position = new Vector3(2,0,-1.5f);
//         currentInstance4.transform.position = new Vector3(-2,0,-1.5f);
//         currentInstance5.transform.position = new Vector3(0,0,2.5f);
//         currentInstance6.transform.position = new Vector3(2,0,1.5f);
//         currentInstance7.transform.position = new Vector3(-2,0,1.5f);

//         // camera randomisation code
//         var distance1 = cameraDistance.Sample();
//         var distance2 = cameraDistance.Sample();
//         mainCamera.transform.position = new Vector3(distance1,10,distance2);
//         mainCamera.transform.rotation = Quaternion.Euler(cameraRot.Sample());

//         // material randomisation code
//         material1.color = Random.ColorHSV();
//         material2.color = Random.ColorHSV();
//     }

//     protected override void OnIterationEnd()
//     {
//         GameObject.Destroy(currentInstance1);
//         GameObject.Destroy(currentInstance2);
//         GameObject.Destroy(currentInstance3);
//         GameObject.Destroy(currentInstance4);
//         GameObject.Destroy(currentInstance5);
//         GameObject.Destroy(currentInstance6);
//         GameObject.Destroy(currentInstance7);
//     }
// }