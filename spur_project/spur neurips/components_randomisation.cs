using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;

[Serializable]
[AddRandomizerMenu("Perception/components_randomisation")]
public class components_randomisation : Randomizer
{
    public GameObjectParameter prefabs1;
    private GameObject currentInstance1;
    private Rigidbody rb1;
    public MaterialParameter materials1;
    private Material current_material1;

    protected override void OnIterationStart()
    {
        currentInstance1=GameObject.Instantiate(prefabs1.Sample());
        currentInstance1.transform.position = new Vector3(4,0,0);

        // add rigidbody to the object if need to
        rb1=currentInstance1.AddComponent<Rigidbody>();
        rb1.mass=1.0f;

        // get all the mesh renderers in the prefab
        MeshRenderer[] meshRenderers1 = currentInstance1.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers1) 
        {
            // assign a random material to each of the mesh renderers from the list of materials selected in the UI
            current_material1 = materials1.Sample();
            meshRenderer.material = current_material1;
        
            // only for NON CONTAINER objects !!! assign a meshcollider to eahc of the mesh renderers
            MeshCollider meshCollider = meshRenderer.gameObject.AddComponent<MeshCollider>();
            meshCollider.convex = true;
        }

        // assign a random color to each of the materials in the materials list, doing it like this in a separate loop is more efficient since you set a color only once for each material
        for (int i = 0; i < materials1.GetCategoryCount(); i++) 
        {
            materials1.GetCategory(i).color = Random.ColorHSV();
        }
    }

    protected override void OnIterationEnd()
    {
        GameObject.Destroy(currentInstance1);
    }

}
