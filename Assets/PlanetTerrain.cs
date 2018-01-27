using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplexNoise;

public class PlanetTerrain : Zac.ZacGOSingleton<PlanetTerrain>
{

    public enum PlanetFaceEnum
    {
        South = 0,
        North = 1,
        West = 2,
        East = 3,
        Bottom = 4,
        Top = 5
    }

    #region MeshGen

    [SerializeField]
    protected TerrainFace pfb_terrainFace;
    [SerializeField]
    protected int faceResolution = 20;
    public static int FaceResolution
    {
        get
        {
            return instance.faceResolution;
        }
    }

    protected TerrainFace[] terrainFaces;

    /// <summary>
    /// Face first, then face's x and y
    /// </summary>
    protected Vector3[,,] baseVoxelConnectionPoss;
    public static Vector3[,,] BaseVoxelConnectionPoss
    {
        get
        {
            return instance.baseVoxelConnectionPoss;
        }
    }

    ///// <summary>
    ///// Face first, then tile's base x and y idx, then face again (no top and bottom)
    ///// </summary>
    //protected Vector3[,,,] baseVoxelSideFaceNormals;

    #endregion
    #region TerrainNoise

    [SerializeField]
    protected float surfaceDepth = 20;
    [SerializeField]
    protected int octave = 3;
    [SerializeField]
    protected float noisePosMultiplier = 2;
    [SerializeField]
    protected Vector3 generationSeed = new Vector3(500, 500, 500);

    [SerializeField]
    protected float noiseFrequency = 0.005f;
    [SerializeField]
    protected float noiseHeight = 10;
    [SerializeField]
    protected float genNormalizedExp = 5;

    #endregion

    // Use this for initialization
    protected override void set()
    {
        establishZacSingleton(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static float GetHeightNoise(float x, float y, float z, float scale, float max)
    {
        return (Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f);
    }

    public static float GetHeightNoise(Vector3 _pos, float scale, float max)
    {
        return GetHeightNoise(_pos.x, _pos.y, _pos.z, scale, max);
    }

    protected float reuse_persistent = 1 / Mathf.Sqrt(2);

    protected float generateGroundHeight(Vector3 _position)
    {
        float genHeight = surfaceDepth;

        float stoneNormalizedHeight = 0;

        int loop = octave;

        float totalAmplitude = 0;

        for (int l = 0; l < loop; l++)
        {
            float frequency = Mathf.Pow(2, l);
            float amplitude = Mathf.Pow(reuse_persistent, l);
            totalAmplitude += amplitude;
            stoneNormalizedHeight += GetHeightNoise(_position * noisePosMultiplier + generationSeed, noiseFrequency * frequency, amplitude);

        }

        stoneNormalizedHeight /= totalAmplitude;
        stoneNormalizedHeight = Mathf.Pow(stoneNormalizedHeight, genNormalizedExp);
        genHeight += stoneNormalizedHeight * noiseHeight;

        return genHeight;
    }

    public static float GenerateGroundHeight(Vector3 _position)
    {
        return instance.generateGroundHeight(_position);
    }

    protected Vector3 convertCubeToSpherePos(Vector3 _cubeMinMaxPos)
    {

        Vector3 spherePos = new Vector3(0, 0, 0);

        float xSq = _cubeMinMaxPos.x * _cubeMinMaxPos.x;
        float ySq = _cubeMinMaxPos.y * _cubeMinMaxPos.y;
        float zSq = _cubeMinMaxPos.z * _cubeMinMaxPos.z;

        spherePos.x = _cubeMinMaxPos.x * Mathf.Sqrt(1 - ySq / 2 - zSq / 2 + ySq * zSq / 3);
        spherePos.y = _cubeMinMaxPos.y * Mathf.Sqrt(1 - zSq / 2 - xSq / 2 + zSq * xSq / 3);
        spherePos.z = _cubeMinMaxPos.z * Mathf.Sqrt(1 - xSq / 2 - ySq / 2 + xSq * ySq / 3);

        return spherePos;
    }

    protected Vector3 minMaxBasePos(Vector3 _3DIndex)
    {
        return _3DIndex / (faceResolution-1) * 2 - Vector3.one;
    }

    protected void computeBaseVoxelConnecionPoss()
    {
        PlanetFaceEnum[] faceEnumValues = (PlanetFaceEnum[])System.Enum.GetValues(typeof(PlanetFaceEnum));

        // === Computing the Base Tile Connection Positions === //

        // South face

        for (int x = 0; x < faceResolution; x++)
        {
            for (int y = 0; y < faceResolution; y++)
            {

                int z = 0;

                Vector3 cubeConnectionPos = new Vector3(x, y, z);

                baseVoxelConnectionPoss[(int)PlanetFaceEnum.South, x, y] = convertCubeToSpherePos(minMaxBasePos(cubeConnectionPos)).normalized;

            }
        }

        // North face

        for (int x = 0; x < faceResolution; x++)
        {
            for (int y = 0; y < faceResolution; y++)
            {

                int z = faceResolution-1;

                Vector3 cubeConnectionPos = new Vector3(x, y, z);

                baseVoxelConnectionPoss[(int)PlanetFaceEnum.North, faceResolution - 1 - x, y] = convertCubeToSpherePos(minMaxBasePos(cubeConnectionPos)).normalized;

            }
        }

        // left face

        for (int z = 0; z < faceResolution; z++)
        {
            for (int y = 0; y < faceResolution; y++)
            {

                int x = 0;

                Vector3 cubeConnectionPos = new Vector3(x, y, z);

                baseVoxelConnectionPoss[(int)PlanetFaceEnum.West, faceResolution - 1 - z, y] = convertCubeToSpherePos(minMaxBasePos(cubeConnectionPos)).normalized;

                //  baseFaceTileNum-z is to flip order, remember the second channel is the order of x on the face, not the world's 
            }
        }

        // right face

        for (int z = 0; z < faceResolution; z++)
        {
            for (int y = 0; y < faceResolution; y++)
            {
                int x = faceResolution-1;

                Vector3 cubeConnectionPos = new Vector3(x, y, z);

                baseVoxelConnectionPoss[(int)PlanetFaceEnum.East, z, y] = convertCubeToSpherePos(minMaxBasePos(cubeConnectionPos)).normalized;

            }
        }

        // bottom face

        for (int x = 0; x < faceResolution; x++)
        {
            for (int z = 0; z < faceResolution; z++)
            {

                int y = 0;

                Vector3 cubeConnectionPos = new Vector3(x, y, z);

                baseVoxelConnectionPoss[(int)PlanetFaceEnum.Bottom, x, faceResolution - 1 - z] = convertCubeToSpherePos(minMaxBasePos(cubeConnectionPos)).normalized;

            }
        }

        // top face

        for (int x = 0; x < faceResolution; x++)
        {
            for (int z = 0; z < faceResolution; z++)
            {

                int y = faceResolution-1;

                Vector3 cubeConnectionPos = new Vector3(x, y, z);

                baseVoxelConnectionPoss[(int)PlanetFaceEnum.Top, x, z] = convertCubeToSpherePos(minMaxBasePos(cubeConnectionPos)).normalized;

            }
        }
    }

    //protected void computeTileSideNormals()
    //{
    //    PlanetFaceEnum[] faceEnumValues = (PlanetFaceEnum[])System.Enum.GetValues(typeof(PlanetFaceEnum));

    //    baseVoxelSideFaceNormals = new Vector3[faceEnumValues.Length, faceResolution, faceResolution, faceEnumValues.Length];

    //    for (int f = 0; f < faceEnumValues.Length; f++)
    //    {
    //        for (int x = 0; x < faceResolution; x++)
    //        {
    //            for (int y = 0; y < faceResolution; y++)
    //            {

    //                // = South Face = //
    //                Vector3 vectorToCross = baseVoxelConnectionPoss[f, x + 1, y] - baseVoxelConnectionPoss[f, x, y];

    //                baseVoxelSideFaceNormals[f, x, y, (int)PlanetFaceEnum.South] = Vector3.Cross(baseVoxelConnectionPoss[f, x, y], vectorToCross).normalized;

    //                // = North Face = //
    //                vectorToCross = baseVoxelConnectionPoss[f, x, y + 1] - baseVoxelConnectionPoss[f, x + 1, y + 1];

    //                baseVoxelSideFaceNormals[f, x, y, (int)PlanetFaceEnum.North] = Vector3.Cross(baseVoxelConnectionPoss[f, x, y + 1], vectorToCross).normalized;

    //                // = Left Face = //
    //                vectorToCross = baseVoxelConnectionPoss[f, x, y] - baseVoxelConnectionPoss[f, x, y + 1];

    //                baseVoxelSideFaceNormals[f, x, y, (int)PlanetFaceEnum.West] = Vector3.Cross(baseVoxelConnectionPoss[f, x, y], vectorToCross).normalized;

    //                // = Right = //
    //                vectorToCross = baseVoxelConnectionPoss[f, x + 1, y + 1] - baseVoxelConnectionPoss[f, x + 1, y];

    //                baseVoxelSideFaceNormals[f, x, y, (int)PlanetFaceEnum.East] = Vector3.Cross(baseVoxelConnectionPoss[f, x, y], vectorToCross).normalized;

    //            }

    //        }

    //    }
    //}

    protected void generateFaces()
    {
        PlanetFaceEnum[] faceEnumValues = (PlanetFaceEnum[])System.Enum.GetValues(typeof(PlanetFaceEnum));
        terrainFaces = new TerrainFace[faceEnumValues.Length];
        for (int f = 0; f < faceEnumValues.Length; f++)
        {
            terrainFaces[f] = (TerrainFace)Instantiate(pfb_terrainFace);
            terrainFaces[f].transform.SetParent(this.transform, false);
            terrainFaces[f].gameObject.name = "Terrain Face=" + (PlanetFaceEnum)f;
            terrainFaces[f].SetStartingStates((PlanetFaceEnum)f);
        }
     }

    public void SetStartingStates()
    {
        PlanetFaceEnum[] faceEnumValues = (PlanetFaceEnum[])System.Enum.GetValues(typeof(PlanetFaceEnum));
        baseVoxelConnectionPoss = new Vector3[faceEnumValues.Length, faceResolution, faceResolution];
        computeBaseVoxelConnecionPoss();
        generateFaces();
    }
}
