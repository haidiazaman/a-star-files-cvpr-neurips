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
[AddRandomizerMenu("Perception/background Randomizer")]
public class backgroundRandomizer : Randomizer
{
    public string imageFolderPath;
    public Material skyboxMaterial;
    private List<Texture2D> textures = new List<Texture2D>();

    protected override void OnAwake()
    {
        // Load all images from the specified folder and store them in a list
        string[] imageFilePaths = System.IO.Directory.GetFiles(imageFolderPath, "*.jpg");
        foreach (string imagePath in imageFilePaths)
        {
            Texture2D texture = new Texture2D(2, 2);
            byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
            texture.LoadImage(imageData);
            textures.Add(texture);
        }
    }

    protected override void OnIterationStart()
    {
        // Select a random texture from the list
        Texture2D selectedTexture = textures[Random.Range(0, textures.Count)];
        // Apply the texture to the skybox material
        skyboxMaterial.SetTexture("_MainTex", selectedTexture);
    }

    protected override void OnIterationEnd()
    {
        // Release the texture to free up memory
        skyboxMaterial.SetTexture("_MainTex", null);
        Texture2D.Destroy(skyboxMaterial.GetTexture("_MainTex"));
    }
}



            // // Set RGBM compression mode for imported textures
            // string assetPath = imagePath.Replace(Application.dataPath, "Assets");
            // TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            // textureImporter.textureType = TextureImporterType.Default;
            // textureImporter.sRGBTexture = false;
            // textureImporter.alphaSource = TextureImporterAlphaSource.None;
            // textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            // textureImporter.mipmapEnabled = false;
            // textureImporter.wrapMode = TextureWrapMode.Clamp;
            // textureImporter.isReadable = true;
            // textureImporter.rgbmCompression = TextureImporterRGBMMode.RGBM;
            // AssetDatabase.ImportAsset(assetPath);
            // // // Load the HDR file as a Texture2D using Unity's built-in HDR loader
            // // Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBAFloat, false);
            // // byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
            // // texture.LoadImage(imageData);

            // textures.Add(texture);

    // protected override void OnIterationStart()
    // {
    //     // Select a random texture from the list
    //     Texture2D selectedTexture = textures[Random.Range(0, textures.Count)];
    //     // Apply the texture to the skybox material
    //     skyboxMaterial.SetTexture("_MainTex", selectedTexture);
    //     Debug.Log("Current skybox texture: " + skyboxMaterial.GetTexture("_MainTex").name);
    // }
    // protected override void OnAwake()
    // {
    //     // Load all images from the specified folder and store them in a list
    //     string[] imageFilePaths = System.IO.Directory.GetFiles(imageFolderPath, "*.hdr");
    //     foreach (string imagePath in imageFilePaths)
    //     {
    //         Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBM, true);
    //         byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
    //         texture.LoadRawTextureData(imageData);
    //         texture.Apply();
    //         textures.Add(texture);
    //     }
    // }

// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Perception.Randomization.Parameters;
// using UnityEngine.Perception.Randomization.Randomizers;
// using System;
// using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using Random=UnityEngine.Random;

// [Serializable]
// [AddRandomizerMenu("Perception/background Randomizer")]
// public class backgroundRandomizer : Randomizer
// {
//     public string imageFolderPath;
//     public Material skyboxMaterial;

//     protected override void OnIterationStart()
//     {
//         // Load all images from the specified folder
//         string[] imageFilePaths = System.IO.Directory.GetFiles(imageFolderPath, "*.jpg");
//         // Select a random image
//         string selectedImagePath = imageFilePaths[Random.Range(0, imageFilePaths.Length)];
//         Debug.Log(selectedImagePath);
//         // Load the selected image into a texture
//         Texture2D texture = new Texture2D(2, 2);
//         byte[] imageData = System.IO.File.ReadAllBytes(selectedImagePath);
//         texture.LoadImage(imageData);
//         // Apply the texture to the skybox material
//         skyboxMaterial.SetTexture("_MainTex", texture);
//     }
// }

// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Perception.Randomization.Parameters;
// using UnityEngine.Perception.Randomization.Randomizers;


// public class backgroundRandomizer : Randomizer
// {
//     public string imageFolderPath;
//     protected void Start()
//     {
//         // Get a reference to the RawImage component on your UI object
//         RawImage rawImage = GetComponent<RawImage>();
//         // Load all images from the specified folder
//         string[] imageFilePaths = System.IO.Directory.GetFiles(imageFolderPath);
//         // Select a random image
//         string selectedImagePath = imageFilePaths[Random.Range(0, imageFilePaths.Length)];
//         Debug.Log(selectedImagePath);
//         // Load the selected image into a texture
//         Texture2D texture = new Texture2D(2, 2);
//         byte[] imageData = System.IO.File.ReadAllBytes(selectedImagePath);
//         texture.LoadImage(imageData);
//         rawImage.texture = texture;
//     }

// }

// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Perception.Randomization.Parameters;
// using UnityEngine.Perception.Randomization.Randomizers;
// using System;
// using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using Random=UnityEngine.Random;


// [Serializable]
// [AddRandomizerMenu("Perception/background Randomizer")]
// public class backgroundRandomizer : Randomizer
// {
//     public string imageFolderPath;
//     RawImage rawImage;

//     protected override void OnScenarioStart()
//     {
//         // Get a reference to the RawImage component on your UI object
//         rawImage = GetComponent<RawImage>();
//         // Load all images from the specified folder
//         string[] imageFilePaths = System.IO.Directory.GetFiles(imageFolderPath);
//         // Select a random image
//         string selectedImagePath = imageFilePaths[Random.Range(0, imageFilePaths.Length)];
//         // Load the selected image into a texture
//         Texture2D texture = new Texture2D(2, 2);
//         byte[] imageData = System.IO.File.ReadAllBytes(selectedImagePath);
//         texture.LoadImage(imageData);
//         rawImage.texture = texture;
//     }

// }


// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;
// // using UnityEngine.UI;
// // using UnityEngine.Perception.Randomization.Parameters;

// // public class backgroundRandomizer : MonoBehaviour
// // {
// //     // public Texture2D[] backgrounds; // An array to hold your background textures
// //     // public GameObjectParameter images;
// //     public string imageFolderPath;
// //     void Start()
// //     {
// //         // Get a reference to the RawImage component on your UI object
// //         RawImage rawImage = GetComponent<RawImage>();
// //         // Load all images from the specified folder
// //         string[] imageFilePaths = System.IO.Directory.GetFiles(imageFolderPath);

// //         // Select a random image
// //         string selectedImagePath = imageFilePaths[Random.Range(0, imageFilePaths.Length)];
// //         Debug.Log(selectedImagePath);
// //         // Load the selected image into a texture
// //         Texture2D texture = new Texture2D(2, 2);
// //         byte[] imageData = System.IO.File.ReadAllBytes(selectedImagePath);
// //         texture.LoadImage(imageData);
// //         rawImage.texture = texture;
// //         // rawImage.texture = images.Sample();
// //         // Texture2D texture = new Texture2D(2, 2);
// //         // byte[] imageData = File.ReadAllBytes("Assets/Prefabs/istockphoto-1219502382-612x612 1.jpeg");
// //         // rawImage.texture.LoadImage(imageData);

// //         // // If there are any backgrounds in the array, choose a random one and set it as the texture for the RawImage component
// //         // if (backgrounds.Length > 0)
// //         // {
// //         //     int randomIndex = Random.Range(0, backgrounds.Length);
// //         //     rawImage.texture = backgrounds[randomIndex];
// //         // }
// //     }

// //     // Update is called once per frame
// //     void Update()
// //     {
        
// //     }
// // }