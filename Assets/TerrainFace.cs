using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace : MonoBehaviour {

    protected PlanetTerrain.PlanetFaceEnum face;
    protected int f
    {
        get
        {
            return (int)face;
        }
    }

    protected int[,] verticesToIndex;

    protected List<Vector3> vertices = new List<Vector3>();
    protected List<Vector3> normals = new List<Vector3>();
    protected List<int> triangles = new List<int>();

    protected MeshFilter meshFilter;
    protected MeshRenderer meshRenderer;
    protected MeshCollider meshCollider;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetStartingStates(PlanetTerrain.PlanetFaceEnum _face)
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        face = _face;

        verticesToIndex = new int[PlanetTerrain.FaceResolution, PlanetTerrain.FaceResolution];

        for (int x = 0; x < PlanetTerrain.FaceResolution; x++)
        {
            for (int y = 0; y < PlanetTerrain.FaceResolution; y++)
            {
                Vector3 basePos = PlanetTerrain.BaseVoxelConnectionPoss[f, x, y];
                Vector3 newVertex = basePos * PlanetTerrain.GenerateGroundHeight(basePos);
                //Vector3 newVertex = basePos;

                vertices.Add(newVertex);
                normals.Add(basePos);
                verticesToIndex[x, y] = vertices.Count-1;
            }
        }

        for (int x = 0; x < PlanetTerrain.FaceResolution-1; x++)
        {
            for (int y = 0; y < PlanetTerrain.FaceResolution-1; y++)
            {
                //Vector3 newVertex = PlanetTerrain.BaseVoxelConnectionPoss[f, x, y];
                //newVertex = newVertex * PlanetTerrain.GenerateGroundHeight(newVertex);

                triangles.Add(verticesToIndex[x, y]);
                triangles.Add(verticesToIndex[x+1, y+1]);
                triangles.Add(verticesToIndex[x + 1, y]);
                triangles.Add(verticesToIndex[x, y]);
                triangles.Add(verticesToIndex[x, y+1]);
                triangles.Add(verticesToIndex[x + 1, y + 1]);
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;

    }
}
