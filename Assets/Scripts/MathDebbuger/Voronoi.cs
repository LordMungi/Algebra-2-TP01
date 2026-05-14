using CustomMath;
using System.Collections.Generic;
using UnityEngine;

public class Voronoi : MonoBehaviour
{
    [SerializeField] private GameObject PlanePrefab;
    [SerializeField] private GameObject PointsOfInterestObjects;

    private List<Vec3> PointsOfInterest;
    private List<ThiessenPolygon> Regions;
    private void Awake()
    {
        PointsOfInterest = new List<Vec3>();
        Regions = new List<ThiessenPolygon>();

        foreach (Transform point in PointsOfInterestObjects.transform)
        {
            PointsOfInterest.Add(point.position);
        }
        SetRegions();
        BuildRegions();
    }

    private void SetRegions()
    {
        foreach (Vec3 point in PointsOfInterest)
        {
            Regions.Add(new ThiessenPolygon(point));
        }

        for (int i = 0; i < Regions.Count; i++)
        {
            for (int j = i + 1; j < Regions.Count; j++)
            {
                Vec3 vec = Regions[i].center - Regions[j].center;
                Vec3 midpoint = Regions[j].center + vec / 2;

                Regions[i].AddPlane(new MyPlane(vec, midpoint));
                Regions[j].AddPlane(new MyPlane(-vec, midpoint));
            }
        }

        foreach (ThiessenPolygon region in Regions)
        {
            //region.RemoveRedundantPlanes();
        }
    }

    private void BuildRegions()
    {
        foreach (ThiessenPolygon region in Regions)
        {
            Color regionColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 0.3f);
            foreach (MyPlane plane in region.Planes)
            {
                Instantiate(PlanePrefab, plane.ClosestPointOnPlane(region.center), Quaternion.FromToRotation(Vec3.Up, plane.normal))
                .GetComponent<Renderer>().material.color = regionColor;
            }
        }
    }
}
