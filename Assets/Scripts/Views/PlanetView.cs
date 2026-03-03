using UnityEngine;

public class PlanetView : MonoBehaviour
{
    public PlanetData.Planet planet;
    public LineRenderer lineRenderer;

    public void SetPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public void SetTrajectory(Vector3[] points)
    {
        if (lineRenderer == null) return;
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
}
