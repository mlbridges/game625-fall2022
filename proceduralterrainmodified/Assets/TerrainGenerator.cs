using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TerrainGenerator : MonoBehaviour
{
    [RangeAttribute(1f,10f)]
    public float flatness=1f;
    [RangeAttribute(1f, 20f)]
    public float frequency = 1f;
    [RangeAttribute(1, 10)]
    public int octaves = 8;
    Texture2D image;
    Terrain terrain;
    float z_offset = 0;
    float x_offset = 0;
    float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        image = new Texture2D(terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);
        image.LoadImage(File.ReadAllBytes("Assets/mt-taranaki.png"));

    }


    // Update is called once per frame
    
    //void HeightMappingFromImage(i,j)
    //{

    //float height = 0.4f;


    ///*Reading a pixel in the picture at the position corresponding to the current position in the terrain and
    // * uses its blue color component as height */

    //    height = image.GetPixel(i, j).b;

    //}

    void Update()
    {

        float forward = Time.deltaTime * speed;
        z_offset += forward;
        x_offset += forward;
        // moving over the map
        if (Input.GetKey(KeyCode.Space))
        {
            speed = 0;
        }
        else
        {
            speed = 10;
        }

        // get heights map to modify it
        float[,] heightmap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);


        // iterating over two dimentional square array
        for (int i = 0; i < terrain.terrainData.heightmapResolution; ++i)
        {
            for (int j = 0; j < terrain.terrainData.heightmapResolution; ++j)
            {

                //Method 1. Image-based height-mapping -- uncomment below and comment entire perlin noise
                //float height = image.GetPixel(i, j).b;


                ////Method 2.Perlin Noise
                float x = j / (float)terrain.terrainData.heightmapResolution;
                float y = i / (float)terrain.terrainData.heightmapResolution;
                float current_frequency = frequency;
                float amplitude = 1;
                float height = 0;

                for (int z = 0; z < octaves; ++z) // octaves control how many times the perlin noise heigth generation is performed
                {
                    // frequency controlls how many times perlin noise is repeated onto the terrain.
                    // frequency = 1 means entire terrain uses all values of perlin noise
                    // frequence = 2 means that the same perlin noise is repeated 4 times (2x2) on the terrain

                    // amplitude determines much much of the perlin noise is added to the final heigth of the vertex.
                    // aplitude =1 means that the whole value of perlin noise is added
                    // amplitute = 1/2 means that half of the perlin noise is applied for each vertex during the current octave

                    // see http://www.yaldex.com/open-gl/ch15lev1sec1.html for more details

                    height = height + Mathf.PerlinNoise(x * current_frequency, y * current_frequency) * amplitude; // during every occave add a bit more height

                    amplitude /= 2; // add less
                    current_frequency *= 3; // and more detailed
                }
                //Perlin noise method end


                //updating map height and moving 
                heightmap[Mathf.Abs(((int)(i + x_offset)) % terrain.terrainData.heightmapResolution),
                         Mathf.Abs(((int)(j + z_offset)) % terrain.terrainData.heightmapResolution)] = height / flatness;
            }
        }
        terrain.terrainData.SetHeights(0, 0, heightmap);
    }
}
