using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

// - pseudocode
//     - start from centre (0,0,0) and venture outwards,
//         - create a float scale
//         - use the scale to multiply the distances for the positions of objects
//         - use the scale to adjust the camera sphere
//             - set a range for the camera sphere radius
//                 - by doing this, you dont need to further vary the size of objects and position since these variables seem to already be randomised by the camera POV
//         - randomise the rotation of the object slightly for all axes, to make it “shake” abit but make sure they dont flip

// fix camera
// spawn objects inside
// variables to change in public: commonObjectScale, cameraSphereRadius (fix around 5)
[Serializable]
[AddRandomizerMenu("Perception/static template")]
public class static_template : Randomizer
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
    public float commonObjectScale=1.0f;
    public float minCameraRadius = 5f;
    public float maxCameraRadius = 10f;
    public float cameraLimit = 7.5f;
    public float cameraFOV = 50f;
    public float objectRotationMinMax = 45f;    

    protected override void OnIterationStart()
    {
        // need to change the positions according to prepositions
        Vector3 pos1 = new Vector3(0,2,0); // object pos 1
        Vector3 pos2 = new Vector3(0,-2,0);  // object pos 2

    	// dont need to change this block
        currentInstance1=GameObject.Instantiate(prefabs1.Sample(), pos1, Quaternion.identity);
        currentInstance2=GameObject.Instantiate(prefabs2.Sample(), pos2, Quaternion.identity);
        currentInstance1.transform.rotation = Quaternion.Euler(Random.Range(-objectRotationMinMax,objectRotationMinMax),Random.Range(-objectRotationMinMax,objectRotationMinMax),Random.Range(-objectRotationMinMax,objectRotationMinMax));
        currentInstance2.transform.rotation = Quaternion.Euler(Random.Range(-objectRotationMinMax,objectRotationMinMax),Random.Range(-objectRotationMinMax,objectRotationMinMax),Random.Range(-objectRotationMinMax,objectRotationMinMax));
        currentInstance1.transform.localScale = Vector3.one * commonObjectScale;
        currentInstance2.transform.localScale = Vector3.one * commonObjectScale;

        // need to change the while block to the different prepositions
        float cameraSphereRadius = Random.Range(minCameraRadius,maxCameraRadius); // at scale 1, the camera distance varies from 10 to 15
        mainCamera.transform.position = Random.onUnitSphere * cameraSphereRadius;
        while (Mathf.Abs(mainCamera.transform.position.y)>cameraLimit)
        {
            mainCamera.transform.position = Random.onUnitSphere * cameraSphereRadius;
        }
        mainCamera.transform.LookAt(new Vector3(0,0,0)); // look at origin
        mainCamera.fieldOfView = cameraFOV;

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

    protected override void OnIterationEnd()
    {
        GameObject.Destroy(currentInstance1);
        GameObject.Destroy(currentInstance2);
    }
}