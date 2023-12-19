using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public PlaneGenerator plane;
    public Vector3 genCount;
    public float noiseScale;
    public Color color;
    public Color snowColor;
    public Color plainsColor;
    public Color seaColor;
    public GameObject tree;
    [SerializeField] private float treeSpawnRate;
    // Start is called before the first frame update
    void Start()
    {
        plane.Create(50, 50);

        Vector3 offset;
        offset.x = Random.value * 100f;
        offset.z = Random.value * 100f;

        for (int x = 0; x < genCount.x; x++)
        {
            for (int z = 0; z < genCount.y; z++)
            {
                float coordX = (x * noiseScale) + offset.x;
                float coordZ = (z * noiseScale) + offset.z;

                //coordX += offset.x;
                //coordZ += offset.z;

                float noise = Mathf.PerlinNoise(coordX, coordZ);
                float height = noise * 5f;
                plane.SetHeight(x, z, height);

               
                if (noise < 0.33f)
                {
                    color = seaColor;
                }

                else if (noise < 0.66f)
                {
                    color = plainsColor;
                }

                else 
                {
                    color = snowColor;
                }

                plane.SetColor(x,z,color);
                
                    Vector3 position = new Vector3(x, height, z);
                    //Instantiate(tree, position, Quaternion.identity);

                    if (noise > 0.33f && Random.value < treeSpawnRate)
                    {
                        Instantiate(tree, position, Quaternion.identity);
                    }
                    
            }
        }

        plane.RefreshMesh();
    }
}
