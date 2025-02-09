using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;


[Serializable]
[AddRandomizerMenu("Perception/on")]
public class on : Randomizer
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
    public FloatParameter cameraDepthZ;
    public FloatParameter cameraCircleRadius;
    public FloatParameter cameraFOV ;
    private Rigidbody object1rb;

    protected override void OnIterationStart()
    {
        Vector3 pos1 = object1Pos.Sample(); // object pos 1
        Vector3 pos2 = object2Pos.Sample(); // object pos 2
        currentInstance1=GameObject.Instantiate(prefabs1.Sample(), pos1, Quaternion.identity);
        currentInstance2=GameObject.Instantiate(prefabs2.Sample(), pos2, Quaternion.identity);
        currentInstance1.transform.localScale = Vector3.one * objectScale1.Sample();
        currentInstance1.transform.rotation = Quaternion.Euler(objectRotation1.Sample());
        currentInstance2.transform.localScale = Vector3.one * objectScale2.Sample();
        currentInstance2.transform.rotation = Quaternion.Euler(objectRotation2.Sample());
        object1rb = currentInstance1.AddComponent<Rigidbody>();

        Vector2 cameraCirclePos = Random.insideUnitCircle * cameraCircleRadius.Sample();
        mainCamera.transform.position = new Vector3(cameraCirclePos.x,cameraCirclePos.y,cameraDepthZ.Sample());
        mainCamera.transform.rotation = Quaternion.Euler(0,180,Random.Range(0,360));
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
    }

    // function for checking when agent collides with room or reference man
    void OnCollisionEnter(Collision collided_object)
    {
        object1rb.velocity = Vector3.zero;
    }
    
    protected override void OnIterationEnd()
    {
        GameObject.Destroy(currentInstance1);
        GameObject.Destroy(currentInstance2);
    }
}
