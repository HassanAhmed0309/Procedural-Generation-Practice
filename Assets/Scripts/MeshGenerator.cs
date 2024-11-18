using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    [SerializeField] MeshFilter filter;
    Vector3[] vertices;
    int[] triangles;

    public int xSize, zSize;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        filter.mesh = mesh;
        CreateShape();
        UpdateMesh();
        MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        collider.sharedMesh = mesh;
        collider.convex = true;
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        float y = 0;
        for(int index = 0, z =0; z <= zSize; z++)
        {
            for(int x = 0; x<=xSize;x++)
            {
                y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[index] = new Vector3(x, y, z);
                index++;
            }
        }

        int vert = 0;
        int tris = 0;
        triangles = new int[xSize *zSize* 6];
        for(int z = 0;z <zSize;z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

    }

    private void OnDrawGizmos()
    {
        if(vertices ==null)
        {
            return;
        }

        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
