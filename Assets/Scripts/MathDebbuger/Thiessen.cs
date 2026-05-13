using CustomMath;
using System.Collections.Generic;
using UnityEngine;

public class Thiessen : MonoBehaviour
{
    [SerializeField] private GameObject PlanePrefab;
    [SerializeField] private GameObject PointPrefab;
    [SerializeField] private GameObject PointsOfInterestObjects;

    private List<Vec3> PointsOfInterest;
    private void Awake()
    {
        PointsOfInterest = new List<Vec3>();
        foreach (Transform point in PointsOfInterestObjects.transform)
        {
            PointsOfInterest.Add(point.position);
        }
        SetRegions();
    }

    private void SetRegions()
    {
        for (int i = 0; i < PointsOfInterest.Count; i++)
        {
            for (int j = i + 1; j < PointsOfInterest.Count; j++)
            {
                MyPlane plane = new MyPlane(PointsOfInterest[j] - PointsOfInterest[i], PointsOfInterest[i] + (PointsOfInterest[j] - PointsOfInterest[i]) / 2);

                Instantiate(PointPrefab, PointsOfInterest[i] + (PointsOfInterest[j] - PointsOfInterest[i]) / 2, transform.rotation);
                Instantiate(PlanePrefab, PointsOfInterest[i] + (PointsOfInterest[j] - PointsOfInterest[i]) / 2, Quaternion.FromToRotation(Vec3.Up, plane.normal));
            }
        }
    }
}
