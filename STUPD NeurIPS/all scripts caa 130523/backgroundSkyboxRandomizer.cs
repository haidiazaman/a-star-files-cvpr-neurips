using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;

[Serializable]
[AddRandomizerMenu("Perception/background Skybox Randomizer")]
public class backgroundSkyboxRandomizer : Randomizer
{
    public Material[] skyboxMaterials;

    protected override void OnIterationStart()
    {
        Material selectedMaterial = skyboxMaterials[Random.Range(0, skyboxMaterials.Length)];
        // Set the selected skybox material as the active skybox material
        RenderSettings.skybox = selectedMaterial;
    }

}