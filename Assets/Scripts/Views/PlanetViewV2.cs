using UnityEngine;

/// <summary>
/// Version améliorée de PlanetView utilisant un maillage 3D (Tube) 
/// au lieu d'un LineRenderer pour une meilleure stabilité en VR.
/// </summary>
public class PlanetViewV2 : MonoBehaviour
{
    public PlanetData.Planet planet;
    
    [Header("Paramètres du Tube (Orbite)")]
    public MeshFilter orbitMeshFilter;
    public float tubeRadius = 0.005f;
    [Range(3, 12)] public int radialSegments = 6;

    private Mesh _orbitMesh;

    void Awake()
    {
        // Création du mesh procédural
        _orbitMesh = new Mesh();
        _orbitMesh.name = "GeneratedOrbitMesh_" + planet.ToString();
        if (orbitMeshFilter != null)
        {
            orbitMeshFilter.mesh = _orbitMesh;
        }
    }

    public void SetPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    /// <summary>
    /// Génère un maillage en forme de tube autour des points de trajectoire.
    /// </summary>
    public void SetTrajectory(Vector3[] points)
    {
        // On vérifie si on a assez de points
        if (points == null || points.Length < 2 || orbitMeshFilter == null) return;

        int numPoints = points.Length;
        int vertexCount = numPoints * radialSegments;
        int triangleCount = numPoints * radialSegments * 6;

        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[triangleCount];

        // Calcul des vertices
        for (int i = 0; i < numPoints; i++)
        {
            Vector3 current = points[i];
            Vector3 next = points[(i + 1) % numPoints];
            Vector3 forward = (next - current).normalized;
            
            // On crée un repère local pour dessiner le cercle du tube
            Vector3 up = Vector3.up;
            if (Mathf.Abs(Vector3.Dot(forward, up)) > 0.9f) up = Vector3.right;
            Vector3 right = Vector3.Cross(up, forward).normalized;
            up = Vector3.Cross(forward, right).normalized;

            for (int j = 0; j < radialSegments; j++)
            {
                float angle = (j / (float)radialSegments) * Mathf.PI * 2f;
                Vector3 offset = (right * Mathf.Cos(angle) + up * Mathf.Sin(angle)) * tubeRadius;
                vertices[i * radialSegments + j] = current + offset;
            }
        }

        // Calcul des triangles (connexion des segments)
        int ti = 0;
        for (int i = 0; i < numPoints; i++)
        {
            int nextI = (i + 1) % numPoints;
            for (int j = 0; j < radialSegments; j++)
            {
                int nextJ = (j + 1) % radialSegments;

                int v1 = i * radialSegments + j;
                int v2 = i * radialSegments + nextJ;
                int v3 = nextI * radialSegments + j;
                int v4 = nextI * radialSegments + nextJ;

                triangles[ti++] = v1;
                triangles[ti++] = v3;
                triangles[ti++] = v2;

                triangles[ti++] = v2;
                triangles[ti++] = v3;
                triangles[ti++] = v4;
            }
        }

        // Mise à jour du maillage
        _orbitMesh.Clear();
        _orbitMesh.vertices = vertices;
        _orbitMesh.triangles = triangles;
        _orbitMesh.RecalculateNormals();
        _orbitMesh.RecalculateBounds();
    }
}