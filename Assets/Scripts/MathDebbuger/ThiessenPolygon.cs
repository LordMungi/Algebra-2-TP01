using CustomMath;
using System.Collections.Generic;
using System;

public class ThiessenPolygon
{
    public Vec3 center;
    public List<MyPlane> Planes;

    public ThiessenPolygon(Vec3 point)
    {
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
            //if (Planes.Count <= 1) break;
            if (isPlaneRedundant(i))
                Planes.RemoveAt(i);
        }
    }

    private bool isPlaneRedundant(int planeIndex)
    {
        Vec3 closestPointToCenter = Planes[planeIndex].ClosestPointOnPlane(center);

        for (int i = 0; i < Planes.Count; i++)
        {
            if (i == planeIndex) continue;

            if (!Planes[i].GetSide(closestPointToCenter))
                return true;
        }
        return false;
    }
}
