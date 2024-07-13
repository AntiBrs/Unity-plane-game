using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TerrainMesher : MonoBehaviour
{
    public Terrain terrain; // A terület, amelyből a hálót létre szeretnéd hozni

    void Start()
    {
        Mesh mesh = new Mesh();
        TerrainData terrainData = terrain.terrainData;
        List<int> indices = new List<int>();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();

        Vector3 heightmapScale = terrainData.heightmapScale;
        float dx = 1f / (terrainData.heightmapResolution - 1);
        float dz = 1f / (terrainData.heightmapResolution - 1);

        for (int ix = 0; ix < terrainData.heightmapResolution; ix++)
        {
            float x = ix * heightmapScale.x;
            float ddx = ix * dx;

            for (int iz = 0; iz < terrainData.heightmapResolution; iz++)
            {
                float z = iz * heightmapScale.z;
                float ddz = iz * dz;

                Vector3 point = new Vector3(x, terrainData.GetInterpolatedHeight(ddx, ddz), z);
                Vector3 normal = terrainData.GetInterpolatedNormal(ddx, ddz);

                vertices.Add(point);
                normals.Add(normal);
            }
        }

        int w = terrainData.heightmapResolution;
        int h = terrainData.heightmapResolution;

        for (int xx = 0; xx < w - 1; xx++)
        {
            for (int zz = 0; zz < h - 1; zz++)
            {
                int a = zz + xx * w;
                int b = a + w + 1;
                int c = a + w;
                int d = a + 1;

                indices.Add(a);
                indices.Add(b);
                indices.Add(c);

                indices.Add(a);
                indices.Add(d);
                indices.Add(b);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = indices.ToArray();

        mesh.RecalculateBounds();

        // Adj hozzá egy MeshCollider komponenst a meshhez
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        // Mentés .asset fájlba
        string meshAssetPath = "Assets/SajatM/GMesh" + terrainData.name + ".asset";

        AssetDatabase.CreateAsset(mesh, meshAssetPath);
        AssetDatabase.SaveAssets();
    }
}
