using UnityEngine;

[RequireComponent(typeof(MeshRenderer))] 
[RequireComponent(typeof(MeshFilter))] 

public class WaterManager : MonoBehaviour
{
    MeshFilter meshFilter;

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    void FixedUpdate()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + vertices[i].x, transform.position.z + vertices[i].z);
        }

        meshFilter.mesh.vertices = vertices;
        // meshFilter.mesh.RecalculateNormals();
    }
}
