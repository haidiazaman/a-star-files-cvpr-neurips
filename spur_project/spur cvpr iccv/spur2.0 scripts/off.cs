using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/off")]
public class off : Randomizer
{
    public GameObjectParameter prefabs_mat1;
    public GameObjectParameter prefabs_mat2;
    private GameObject currentInstance1;
    private GameObject currentInstance2;
    private float timeCounter = 0f;
    private float speed;
    // camera variables initialisation
    public Camera mainCamera;
    public float sphere_limit=3f;
    // material variables initialisation
    public Material material1;
    public Material material2;
    
    protected override void OnIterationStart()
    {   
        timeCounter = 0f;
        speed = Random.Range(2,3);
        // object variables code
        // float randomHeight = Random.Range(2,4);
        // currentInstance1 = GameObject.Instantiate(prefabs_mat1.Sample(),new Vector3(randomHeight,-3.5f,0),Random.rotation);
        // currentInstance2 = GameObject.Instantiate(prefabs_mat2.Sample(),Vector3.zero,Random.rotation);

        // object variables code
        float rotationY = Random.Range(0,360);
        float ObjectScale1 = Random.Range(1,1.5f); // currentInstance1, smaller scale, 1-1.5
        currentInstance1=GameObject.Instantiate(prefabs_mat1.Sample());
        currentInstance1.transform.position = new Vector3(0,1,0);
        currentInstance1.transform.rotation = Quaternion.Euler(0,rotationY,0);
        currentInstance1.transform.localScale = Vector3.one * ObjectScale1;

        float scaleX = Random.Range(6,8);
        float scaleZ = Random.Range(6,8);
        currentInstance2=GameObject.Instantiate(prefabs_mat2.Sample());
        currentInstance2.transform.position = new Vector3(0,0,0); 
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
        // material randomisation code
        material1.color = Random.ColorHSV();
        material2.color = Random.ColorHSV();
    }

    protected override void OnUpdate()
    {
        timeCounter+=Time.deltaTime*speed;
        float x1 = currentInstance1.transform.position.x;
        float y1 = 1 + timeCounter;
        float z1 = currentInstance1.transform.position.z;
        currentInstance1.transform.position = new Vector3(x1,y1,z1);   
    }

    protected override void OnIterationEnd()
    {
        GameObject.Destroy(currentInstance1);
        GameObject.Destroy(currentInstance2);
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
// [AddRandomizerMenu("Perception/off")]
// public class off : Randomizer
// {
//     // output colour filepaths
//     string filePath1 = "C:/Users/haidi/OneDrive/Desktop/dataset_v3/colors_txt_files/colors_off1.txt";
//     string filePath2 = "C:/Users/haidi/OneDrive/Desktop/dataset_v3/colors_txt_files/colors_off2.txt";

//     // objects variables initialisation
//     public GameObjectParameter prefabs_mat1;
//     public GameObjectParameter prefabs_mat2;
//     private GameObject currentInstance1;
//     private GameObject currentInstance2;
//     public Vector3Parameter objectScale;
//     private float timeCounter = 0f;
//     public float speed=2.5f;
//     public FloatParameter cameraX;
//     public FloatParameter cameraY;
//     public FloatParameter cameraZ;
//     public FloatParameter cameraRotX;
//     public FloatParameter cameraRotY;
//     public FloatParameter cameraRotZ;

//     // camera variables initialisation
//     public Camera mainCamera;

//     // material variables initialisation
//     public Material material1;
//     public Material material2;
//     private Color[] colorList = { 
//         Color.red, 
//         Color.green, 
//         Color.blue, 
//         Color.magenta, //purple
//         new Color(0.5f,0.5f,1,1), //light blue
//         new Color(1.0f,0.64f,0.0f,1), //orange
//         new Color(0.75f,0.75f,0.75f,1), //dark grey
//         Color.yellow
//         };
//     private Dictionary<string, string> colour_identifier =  new Dictionary<string, string>()
//         {
//             {"RGBA(1.000, 0.000, 0.000, 1.000)","red"},
//             {"RGBA(0.000, 1.000, 0.000, 1.000)","green"},
//             {"RGBA(0.000, 0.000, 1.000, 1.000)","dark blue"},
//             {"RGBA(1.000, 0.000, 1.000, 1.000)","purple"},
//             {"RGBA(0.500, 0.500, 1.000, 1.000)","light blue"},
//             {"RGBA(1.000, 0.640, 0.000, 1.000)","orange"},
//             {"RGBA(0.750, 0.750, 0.750, 1.000)","dark grey"},
//             {"RGBA(1.000, 0.922, 0.016, 1.000)","yellow"}
//         }; 

//     protected override void OnIterationStart()
//     {   
//         timeCounter = 0f;
//         // object variables code
//         float rotationY = Random.Range(0,360);
//         float ObjectScale1 = Random.Range(1,1.5f); // currentInstance1, smaller scale, 1-1.5
//         currentInstance1=GameObject.Instantiate(prefabs_mat1.Sample());
//         currentInstance1.transform.position = new Vector3(0,1,0);
//         currentInstance1.transform.rotation = Quaternion.Euler(0,rotationY,0);
//         currentInstance1.transform.localScale = Vector3.one * ObjectScale1;

//         // float scaleX = Random.Range(6,8);
//         // float scaleZ = Random.Range(6,8);
//         currentInstance2=GameObject.Instantiate(prefabs_mat2.Sample());
//         currentInstance2.transform.position = new Vector3(0,0,0); 
//         currentInstance2.transform.localScale = objectScale.Sample();
//         // currentInstance2.transform.localScale = new Vector3(scaleX,0.5f,scaleZ);

//         // camera randomisation codex
//         mainCamera.transform.position = new Vector3(cameraX.Sample(),cameraY.Sample(),cameraZ.Sample());
//         mainCamera.transform.rotation = Quaternion.Euler(cameraRotX.Sample(),cameraRotY.Sample(),cameraRotZ.Sample());

//         // material randomisation code
//         int randomNumber1 = Random.Range(0,8); //initialise at every iteration so new number generated
//         material1.color = colorList[randomNumber1];
//         int randomNumber2 = Random.Range(0,8); //initialise at every iteration so new number generated
//         while (randomNumber2==randomNumber1)
//         {
//             randomNumber2 = Random.Range(0,8);
//         }
//         material2.color = colorList[randomNumber2];

//         // format object colour and object type
//         string object_colour1 = colour_identifier[material1.color.ToString()];
//         string object_type1 = currentInstance1.name;
//         object_type1 = object_type1.Substring(0,object_type1.Length-7); // remove the "(Clone) " which is 7 characters
//         string object_colour_type1 = object_colour1 + " " + object_type1;
//         string object_colour2 = colour_identifier[material2.color.ToString()];
//         string object_type2 = currentInstance2.name;
//         object_type2 = object_type2.Substring(0,object_type2.Length-7); // remove the "(Clone) " which is 7 characters
//         string object_colour_type2 = object_colour2 + " " + object_type2;

//         // write colour and object type to txt file
//         StreamWriter writer1 = new StreamWriter(filePath1, true);
//         writer1.WriteLine(object_colour_type1);
//         writer1.Close();
//         StreamWriter writer2 = new StreamWriter(filePath2, true);
//         writer2.WriteLine(object_colour_type2);
//         writer2.Close();
//     }

//     protected override void OnUpdate()
//     {
//         timeCounter+=Time.deltaTime*speed;
//         float x1 = currentInstance1.transform.position.x;
//         float y1 = 1 + timeCounter;
//         float z1 = currentInstance1.transform.position.z;
//         currentInstance1.transform.position = new Vector3(x1,y1,z1);    
//     }

//     protected override void OnIterationEnd()
//     {
//         GameObject.Destroy(currentInstance1);
//         GameObject.Destroy(currentInstance2);
//     }
// }

