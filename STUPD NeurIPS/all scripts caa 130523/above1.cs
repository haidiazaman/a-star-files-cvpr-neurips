using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/above1")]
public class above1 : Randomizer
{
    
    // output colour filepaths
    string filePath1 = "/Users/haidiazaman/Desktop/FYP/y3s2/mini_SPUR_VG_project/mini_SPUR/colors_txt_files/colors_above1.txt";
    string filePath2 = "/Users/haidiazaman/Desktop/FYP/y3s2/mini_SPUR_VG_project/mini_SPUR/colors_txt_files/colors_above2.txt";

    // camera selection 
    public Camera mainCamera;
    // materials selection
    public Material material1;
    public Material material2;
    // prefabs selection
    public GameObjectParameter prefabs_mat1;
    public GameObjectParameter prefabs_mat2;
    // camera variables initialisation
    public Vector3Parameter cameraPosition;
    public Vector3Parameter cameraRot;
    // material variables initialisation
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




    // object variables initialisation
    private GameObject currentInstance1;
    private GameObject second_object;
    private GameObject currentInstance2;
    private GameObject currentInstance3;
    public Vector3Parameter objectPosition1;
    public Vector3Parameter objectPosition2;
    public Vector3Parameter objectRot1;
    public Vector3Parameter objectRot2;
    public FloatParameter objectScale1;
    public FloatParameter objectScale2;
    // public Vector3Parameter objectPosition3;
    // public FloatParameter objectScale3;
    // public Vector3Parameter objectRot3;
    // public float speed = 2.5f;
    // private float timeCounter = 0f;
    protected override void OnIterationStart()
    {   
        // timeCounter = 0f;
        // object variables code
        currentInstance1=GameObject.Instantiate(prefabs_mat1.Sample());
        currentInstance1.transform.position = objectPosition1.Sample();
        currentInstance1.transform.rotation = Quaternion.Euler(objectRot1.Sample());
        currentInstance1.transform.localScale = Vector3.one * objectScale1.Sample();
        
        currentInstance2=GameObject.Instantiate(prefabs_mat2.Sample());
        while (currentInstance1.name==currentInstance2.name)
        {
            GameObject.Destroy(currentInstance2);
            currentInstance2=GameObject.Instantiate(prefabs_mat2.Sample());
        }
        currentInstance2.transform.position = objectPosition2.Sample();
        currentInstance2.transform.rotation = Quaternion.Euler(objectRot2.Sample());
        currentInstance2.transform.localScale = Vector3.one * objectScale2.Sample();

        // camera randomisation code
        mainCamera.transform.position = cameraPosition.Sample();
        mainCamera.transform.rotation = Quaternion.Euler(cameraRot.Sample());
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
        // timeCounter+=Time.deltaTime*speed;
        // float x1 = currentInstance1.transform.position.x;
        // float y1 = currentInstance1.transform.position.y;
        // float z1 =  4 - timeCounter;
        // currentInstance1.transform.position = new Vector3(x1,y1,z1);

        // float x2 = currentInstance2.transform.position.x;
        // float y2 = currentInstance2.transform.position.y;
        // float z2 =  -4 + timeCounter;
        // currentInstance2.transform.position = new Vector3(x2,y2,z2);
    }

    protected override void OnIterationEnd()
    {
        GameObject.Destroy(currentInstance1);
        GameObject.Destroy(currentInstance2);
    }

}