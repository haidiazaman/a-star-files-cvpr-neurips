using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/against_dynamic_camera")]
public class against_dynamic_camera : Randomizer
{
    // output colour filepaths
    string filePath1 = "/Users/haidiazaman/Desktop/FYP/y3s2/spur_2.0_exploration/colors_txt_files/colors_against1.txt";
    string filePath2 = "/Users/haidiazaman/Desktop/FYP/y3s2/spur_2.0_exploration/colors_txt_files/colors_against2.txt";
    

    // object variables initialisation
    public GameObjectParameter prefabs_mat1;
    public GameObjectParameter prefabs_mat2; 
    private GameObject currentInstance1;  // object in front
    private GameObject currentInstance2; // object behind
    private float timeCounter = 0f;
    private float speed=2.5f;

    // camera variables initialisation
    public Camera mainCamera;

    // material variables initialisation
    public Material material1;
    public Material material2;
    private Color[] colorList = { 
        Color.red, 
        Color.green, 
        Color.blue, 
        Color.magenta, //purple
        new Color(0.5f,0.5f,1,1), //light blue
        new Color(1.0f,0.64f,0.0f,1), //orange
        new Color(0.75f,0.75f,0.75f,1), //dark grey
        Color.yellow
        };
    private Dictionary<string, string> colour_identifier =  new Dictionary<string, string>()
        {
            {"RGBA(1.000, 0.000, 0.000, 1.000)","red"},
            {"RGBA(0.000, 1.000, 0.000, 1.000)","green"},
            {"RGBA(0.000, 0.000, 1.000, 1.000)","dark blue"},
            {"RGBA(1.000, 0.000, 1.000, 1.000)","purple"},
            {"RGBA(0.500, 0.500, 1.000, 1.000)","light blue"},
            {"RGBA(1.000, 0.640, 0.000, 1.000)","orange"},
            {"RGBA(0.750, 0.750, 0.750, 1.000)","dark grey"},
            {"RGBA(1.000, 0.922, 0.016, 1.000)","yellow"}
        }; 

    protected override void OnIterationStart()
    {   
        // object variables code
        float rotationX = Random.Range(-15f,15f);
        float rotationY = Random.Range(-15f,15f);
        float rotationZ = Random.Range(-15f,15f);
        float ObjectScale1 = Random.Range(1,1.5f); // currentInstance1, smaller scale, 1-1.5
        float ObjectScale2 = Random.Range(1,1.5f); // currentInstance2, larger scale, 3-4
        timeCounter = 0f;

        currentInstance1=GameObject.Instantiate(prefabs_mat1.Sample());
        currentInstance1.transform.position = new Vector3(-4,0,0);
        currentInstance1.transform.rotation = Quaternion.Euler(90,rotationY,rotationZ);
        currentInstance1.transform.localScale = Vector3.one * ObjectScale1;
        if (currentInstance1.name=="Pipe(Clone)" || currentInstance1.name=="Torus(Clone)" )
        {
            currentInstance1.transform.rotation = Quaternion.Euler(0,rotationY,rotationZ);
        }
        currentInstance2=GameObject.Instantiate(prefabs_mat2.Sample());
        currentInstance2.transform.position = new Vector3(4,0,0);
        currentInstance2.transform.rotation = Quaternion.Euler(90,rotationY,rotationZ);
        currentInstance2.transform.localScale = Vector3.one * ObjectScale2;
        if (currentInstance2.name=="Pipe(Clone)" || currentInstance2.name=="Torus(Clone)" )
        {
            currentInstance2.transform.rotation = Quaternion.Euler(0,rotationY,rotationZ);
        }

        // camera randomisation code
        mainCamera.transform.position = Random.onUnitSphere * 15;
        // mainCamera.transform.LookAt(currentInstance1.transform); // look at a gameobject position
        mainCamera.transform.LookAt(new Vector3(0,0,0)); // can set a manual point to look at

        // material randomisation code
        int randomNumber1 = Random.Range(0,8); //initialise at every iteration so new number generated
        material1.color = colorList[randomNumber1];
        int randomNumber2 = Random.Range(0,8); //initialise at every iteration so new number generated
        while (randomNumber2==randomNumber1)
        {
            randomNumber2 = Random.Range(0,8);
        }        
        material2.color = colorList[randomNumber2];

        // format object colour and object type
        string object_colour1 = colour_identifier[material1.color.ToString()];
        string object_type1 = currentInstance1.name;
        object_type1 = object_type1.Substring(0,object_type1.Length-7); // remove the "(Clone) " which is 7 characters
        string object_colour_type1 = object_colour1 + " " + object_type1;
        string object_colour2 = colour_identifier[material2.color.ToString()];
        string object_type2 = currentInstance2.name;
        object_type2 = object_type2.Substring(0,object_type2.Length-7); // remove the "(Clone) " which is 7 characters
        string object_colour_type2 = object_colour2 + " " + object_type2;

        // write colour and object type to txt file
        StreamWriter writer1 = new StreamWriter(filePath1, true);
        writer1.WriteLine(object_colour_type1);
        writer1.Close();
        StreamWriter writer2 = new StreamWriter(filePath2, true);
        writer2.WriteLine(object_colour_type2);
        writer2.Close();
    }

    protected override void OnUpdate()
    {
        timeCounter+=Time.deltaTime*speed;
        float x1 = currentInstance1.transform.position.x;
        float y1 = currentInstance1.transform.position.y;
        float z1 =  4 - timeCounter;
        currentInstance1.transform.position = new Vector3(x1,y1,z1);

        float x2 = currentInstance2.transform.position.x;
        float y2 = currentInstance2.transform.position.y;
        float z2 =  -4 + timeCounter;
        currentInstance2.transform.position = new Vector3(x2,y2,z2);
    }

    protected override void OnIterationEnd()
    {
        GameObject.Destroy(currentInstance1);
        GameObject.Destroy(currentInstance2);
    }

}