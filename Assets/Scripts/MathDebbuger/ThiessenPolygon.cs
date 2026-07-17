using CustomMath;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ThiessenPolygon
{
    public int id;

    public Vec3 center;
    public List<MyPlane> Planes;

    public ThiessenPolygon(Vec3 point, int number)
    {
        id = number;
        center = point;
        Planes = new List<MyPlane>();
    }

    public void AddPlane(MyPlane plane)
    {
        Planes.Add(plane);
    }

    public void RemoveRedundantPlanes()
    {
        for (int i = Planes.Count - 1; i >= 0; i--)
        {
            bool planeHasPointInside = false;

            for (int j = Planes.Count - 1; j >= 0; j--)
            {
                if (i == j)
                    continue;

                for (int k = j - 1; k >= 0; k--)
                {
                    if (i == k)
                        continue;

                    float d1 = Planes[i].distance;
                    Vec3 n1 = Planes[i].normal;

                    float d2 = Planes[j].distance;
                    Vec3 n2 = Planes[j].normal;

                    float d3 = Planes[k].distance;
                    Vec3 n3 = Planes[k].normal;

                    float det = Vec3.Dot(n1, Vec3.Cross(n2, n3));
                    if (Math.Abs(det) < 0.00001f)
                        continue;

                    Vec3 intersection = (d1 * Vec3.Cross(n2, n3) + d2 * Vec3.Cross(n3, n1) + d3 * Vec3.Cross(n1, n2)) / det;

                    bool intersectionIsInside = true;

                    for (int l = 0; l < Planes.Count; l++)
                    {
                        if (l == i || l == j || l == k)
                            continue;

                        if (Planes[l].GetDistanceToPoint(intersection) < -MyPlane.epsilon)
                        {
                            intersectionIsInside = false;
                            break;
                        }
                    }
                    if (intersectionIsInside)
                    {
                        planeHasPointInside = true;
                        break;
                    }
                }
                if (planeHasPointInside)
                    break;
            }
            if (!planeHasPointInside)
                Planes.RemoveAt(i);
        }
    }
}
