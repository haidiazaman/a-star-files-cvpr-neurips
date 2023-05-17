using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;


[Serializable]
[AddRandomizerMenu("Perception/between")]
public class between : Randomizer
{
    // fixed variables
    public GameObjectParameter prefabs1;
    public GameObjectParameter prefabs2;
    private GameObject currentInstance1;
    private GameObject currentInstance2;
    private GameObject currentInstance3;
    public MaterialParameter materials1;
    private Material current_material1;
    public MaterialParameter materials2;
    private Material current_material2;
    public MaterialParameter materials3;
    private Material current_material3;
    public Camera mainCamera;

    //public variables to change
    public FloatParameter objectXCoordinate1;
    public FloatParameter objectXCoordinate2;
    public FloatParameter objectXCoordinate3;
    public FloatParameter objectScale1;
    public FloatParameter objectScale2;
    public Vector3Parameter objectRotation1;
    public Vector3Parameter objectRotation2;   
    public FloatParameter cameraDepthZ;
    public FloatParameter cameraCircleRadius;
    public FloatParameter cameraFOV ;

    protected override void OnIterationStart()
    {
        Vector3 pos1 = new Vector3(objectXCoordinate1.Sample(),0,0); // object pos 1
        Vector3 pos2 = new Vector3(objectXCoordinate2.Sample(),0,0);  // object pos 2
        Vector3 pos3 = new Vector3(objectXCoordinate3.Sample(),0,0);  // object pos 3
        currentInstance1=GameObject.Instantiate(prefabs1.Sample(), pos1, Quaternion.identity);
        currentInstance2=GameObject.Instantiate(prefabs2.Sample(), pos2, Quaternion.identity);
        currentInstance3=GameObject.Instantiate(currentInstance2, pos3, Quaternion.identity);

        currentInstance1.transform.localScale = Vector3.one * objectScale1.Sample();
        currentInstance2.transform.localScale = Vector3.one * objectScale2.Sample();
        currentInstance3.transform.localScale = Vector3.one * objectScale2.Sample();
        currentInstance1.transform.rotation = Quaternion.Euler(objectRotation1.Sample());
        currentInstance2.transform.rotation = Quaternion.Euler(objectRotation2.Sample());
        currentInstance3.transform.rotation = Quaternion.Euler(objectRotation2.Sample());

        Vector2 cameraCirclePos = Random.insideUnitCircle * cameraCircleRadius.Sample();
        mainCamera.transform.position = new Vector3(cameraCirclePos.x,cameraCirclePos.y,cameraDepthZ.Sample());
        mainCamera.transform.rotation = Quaternion.Euler(0,180,0);
        mainCamera.fieldOfView = cameraFOV.Sample();

        // dont need to change -  for non container objects only 
        MeshRenderer[] meshRenderers1 = currentInstance1.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers1) 
        {
            current_material1 = materials1.Sample(); // assign a random material to each of the mesh renderers from the list of materials selected in the UI
            meshRenderer.material = current_material1;        
            MeshCollider meshCollider = meshRenderer.gameObject.AddComponent<MeshCollider>(); // only for NON CONTAINER objects !!! assign a meshcollider to eahc of the mesh renderers
            meshCollider.convex = true;
        }        
        for (int i = 0; i < materials1.GetCategoryCount(); i++) // assign a random color to each of the materials in the materials list, doing it like this in a separate loop is more efficient since you set a color only once for each material
        {
            materials1.GetCategory(i).color = Random.ColorHSV();
        }
        MeshRenderer[] meshRenderers2 = currentInstance2.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers2) 
        {
            current_material2 = materials2.Sample(); // assign a random material to each of the mesh renderers from the list of materials selected in the UI
            meshRenderer.material = current_material2;        
            MeshCollider meshCollider = meshRenderer.gameObject.AddComponent<MeshCollider>(); // only for NON CONTAINER objects !!! assign a meshcollider to eahc of the mesh renderers
            meshCollider.convex = true;
        }        
        for (int i = 0; i < materials2.GetCategoryCount(); i++) // assign a random color to each of the materials in the materials list, doing it like this in a separate loop is more efficient since you set a color only once for each material
        {
            materials2.GetCategory(i).color = Random.ColorHSV();
        }
        MeshRenderer[] meshRenderers3 = currentInstance3.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers3) 
        {
            current_material3 = materials3.Sample(); // assign a random material to each of the mesh renderers from the list of materials selected in the UI
            meshRenderer.material = current_material3;        
            MeshCollider meshCollider = meshRenderer.gameObject.AddComponent<MeshCollider>(); // only for NON CONTAINER objects !!! assign a meshcollider to eahc of the mesh renderers
            meshCollider.convex = true;
        }        
        for (int i = 0; i < materials3.GetCategoryCount(); i++) // assign a random color to each of the materials in the materials list, doing it like this in a separate loop is more efficient since you set a color only once for each material
        {
            materials3.GetCategory(i).color = Random.ColorHSV();
        }
    }

    protected override void OnIterationEnd()
    {
        GameObject.Destroy(currentInstance1);
        GameObject.Destroy(currentInstance2);
        GameObject.Destroy(currentInstance3);
    }
}