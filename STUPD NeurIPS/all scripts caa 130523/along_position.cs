using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;


[Serializable]
[AddRandomizerMenu("Perception/along position")]
public class along_position : Randomizer
{
    // fixed variables
    public GameObjectParameter prefabs1;
    public GameObjectParameter prefabs2;
    private GameObject currentInstance1;
    private GameObject currentInstance2;
    public MaterialParameter materials1;
    private Material current_material1;
    public MaterialParameter materials2;
    private Material current_material2;
    public Camera mainCamera;

    //public variables to change
    public Vector3Parameter object1Pos;
    public Vector3Parameter object2Pos;
    public FloatParameter objectScale1;
    public FloatParameter objectScale2;
    public Vector3Parameter objectRotation1;
    public Vector3Parameter objectRotation2;   
    public int minNumObjects;
    public int maxNumObjects;
    private int objectCount;
    private float randomX;
    public FloatParameter cameraDepthZ;
    public FloatParameter cameraCircleRadius;
    public FloatParameter cameraFOV ;

    protected override void OnIterationStart()
    {
        // static Vector3 RandomPointOnLine(float x, float y, float z)
        // {
        //     Vector3 point = new Vector3(x,y,z);
        //     return point;
        // }

        randomX = 0;
        objectCount = Random.Range(minNumObjects,maxNumObjects);   
        Vector3 pos1 = object1Pos.Sample(); // object pos 1
        Vector3 pos2 = object2Pos.Sample(); // object pos 1
        currentInstance1=GameObject.Instantiate(prefabs1.Sample(), pos1, Quaternion.identity);
        currentInstance2=GameObject.Instantiate(prefabs2.Sample(), pos2, Quaternion.identity);

        currentInstance1.transform.localScale = Vector3.one * objectScale1.Sample();
        currentInstance2.transform.localScale = Vector3.one * objectScale2.Sample();
        currentInstance1.transform.rotation = Quaternion.Euler(objectRotation1.Sample());
        currentInstance2.transform.rotation = Quaternion.Euler(objectRotation2.Sample());

        for (int loop=0;loop<objectCount;loop++)
        {
            float random_step = Random.Range(1.5f,2);
            randomX += random_step;
            Vector3 currentPos1 = currentInstance1.transform.position;
            Vector3 spawnPoint = new Vector3(currentPos1.x+randomX,currentPos1.y,currentPos1.z);
            GameObject.Instantiate(currentInstance1,spawnPoint,Random.rotation);
        }


        Vector2 cameraCirclePos = Random.insideUnitCircle * cameraCircleRadius.Sample();
        mainCamera.transform.position = new Vector3(cameraCirclePos.x,cameraCirclePos.y,cameraDepthZ.Sample());
        mainCamera.transform.rotation = Quaternion.Euler(0,180,0);
        mainCamera.fieldOfView = cameraFOV.Sample();

        // dont need to change -  for non container objects only 
        GameObject[] GameObjects = (GameObject.FindObjectsOfType<GameObject>() as GameObject[]);
        for (int i = 0; i < GameObjects.Length; i++)
        {
            if (GameObjects[i].name==currentInstance1.name || GameObjects[i].name==currentInstance1.name+"(Clone)") 
            {
                MeshRenderer[] meshRenderers1 = GameObjects[i].GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer meshRenderer in meshRenderers1) 
                {
                    current_material1 = materials1.Sample(); // assign a random material to each of the mesh renderers from the list of materials selected in the UI
                    meshRenderer.material = current_material1;        
                    MeshCollider meshCollider = meshRenderer.gameObject.AddComponent<MeshCollider>(); // only for NON CONTAINER objects !!! assign a meshcollider to eahc of the mesh renderers
                    meshCollider.convex = true;
                }        
                for (int j = 0; j < materials1.GetCategoryCount(); j++) // assign a random color to each of the materials in the materials list, doing it like this in a separate loop is more efficient since you set a color only once for each material
                {
                    materials1.GetCategory(j).color = Random.ColorHSV();
                }  
            }

            else if (GameObjects[i].name==currentInstance2.name || GameObjects[i].name==currentInstance2.name+"(Clone)") 
            {
                MeshRenderer[] meshRenderers2 = GameObjects[i].GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer meshRenderer in meshRenderers2) 
                {
                    current_material2 = materials2.Sample(); // assign a random material to each of the mesh renderers from the list of materials selected in the UI
                    meshRenderer.material = current_material2;        
                    MeshCollider meshCollider = meshRenderer.gameObject.AddComponent<MeshCollider>(); // only for NON CONTAINER objects !!! assign a meshcollider to eahc of the mesh renderers
                    meshCollider.convex = true;
                }        
                for (int j = 0; j < materials2.GetCategoryCount(); j++) // assign a random color to each of the materials in the materials list, doing it like this in a separate loop is more efficient since you set a color only once for each material
                {
                    materials2.GetCategory(j).color = Random.ColorHSV();
                }  
            }
        }
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
// [AddRandomizerMenu("Perception/along_position")]



// public class along_position : Randomizer
// {
//     // object variables initialisation
//     public GameObjectParameter prefabs_mat1; // objects folder
//     public GameObjectParameter prefabs_mat2; // track folder
//     private GameObject currentInstance1; // objects
//     private GameObject currentInstance2; // track
//     public MaterialParameter materials1;
//     private Material current_material1;
//     public MaterialParameter materials2;
//     private Material current_material2;
//     public FloatParameter objectScale;
//     public Vector3Parameter trackScale;
//     public float spawnCollisionCheckRadius=0.5f;   
//     public FloatParameter objectsXPos;
//     public FloatParameter objectsYPos;
//     public Camera mainCamera;
//     public float camera_radius=10f;
//     public float sphere_limit=7.5f;
//     public float cameraFOV = 30f;

//     protected override void OnIterationStart()
//     {   
//         static Vector3 RandomPointOnLine(float x, float y, float z)
//         {
//             Vector3 point = new Vector3(x,y,z);
//             return point;
//         }

//         // object variables code
//         // tracks 
//         currentInstance1 = GameObject.Instantiate(prefabs_mat1.Sample()); // track
//         currentInstance1.transform.localScale = Vector3.one * objectScale.Sample();

//         currentInstance2 = GameObject.Instantiate(prefabs_mat2.Sample()); // track
//         currentInstance2.transform.position = new Vector3(0,0,0);
//         currentInstance2.transform.localScale = trackScale.Sample(); 

//         float randomZ = Random.Range(-2,-5);
//         Vector3 randomPoint = RandomPointOnLine(objectsXPos.Sample(),objectsYPos.Sample(),randomZ);
//         currentInstance1.transform.position = randomPoint;

//         int objectCount = Random.Range(2,6);
//         for (int loop=0;loop<objectCount;loop++)
//         {
//             float random_step = Random.Range(1.5f,2);
//             randomZ += random_step;
//             Vector3 spawnPoint = RandomPointOnLine(objectsYPos.Sample(),objectsYPos.Sample(),randomZ);
//             GameObject.Instantiate(currentInstance1,spawnPoint,Random.rotation);
//         }

//         // camera randomisation code
//         mainCamera.transform.position = Random.onUnitSphere * camera_radius;
//         while (Mathf.Abs(mainCamera.transform.position.y)<sphere_limit | mainCamera.transform.position.y<0)
//         {
//             mainCamera.transform.position = Random.onUnitSphere * camera_radius;
//         }
//         mainCamera.transform.LookAt(new Vector3(0,0,0)); // can set a manual point to look at
//         mainCamera.fieldOfView = cameraFOV;

//         // dont need to change -  for non container objects only 
//         MeshRenderer[] meshRenderers1 = currentInstance1.GetComponentsInChildren<MeshRenderer>();
//         foreach (MeshRenderer meshRenderer in meshRenderers1) 
//         {
//             current_material1 = materials1.Sample(); // assign a random material to each of the mesh renderers from the list of materials selected in the UI
//             meshRenderer.material = current_material1;        
//             MeshCollider meshCollider = meshRenderer.gameObject.AddComponent<MeshCollider>(); // only for NON CONTAINER objects !!! assign a meshcollider to eahc of the mesh renderers
//             meshCollider.convex = true;
//         }        
//         for (int i = 0; i < materials1.GetCategoryCount(); i++) // assign a random color to each of the materials in the materials list, doing it like this in a separate loop is more efficient since you set a color only once for each material
//         {
//             materials1.GetCategory(i).color = Random.ColorHSV();
//         }
//         MeshRenderer[] meshRenderers2 = currentInstance2.GetComponentsInChildren<MeshRenderer>();
//         foreach (MeshRenderer meshRenderer in meshRenderers2) 
//         {
//             current_material2 = materials2.Sample(); // assign a random material to each of the mesh renderers from the list of materials selected in the UI
//             meshRenderer.material = current_material2;        
//             MeshCollider meshCollider = meshRenderer.gameObject.AddComponent<MeshCollider>(); // only for NON CONTAINER objects !!! assign a meshcollider to eahc of the mesh renderers
//             meshCollider.convex = true;
//         }        
//         for (int i = 0; i < materials2.GetCategoryCount(); i++) // assign a random color to each of the materials in the materials list, doing it like this in a separate loop is more efficient since you set a color only once for each material
//         {
//             materials2.GetCategory(i).color = Random.ColorHSV();
//         }
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