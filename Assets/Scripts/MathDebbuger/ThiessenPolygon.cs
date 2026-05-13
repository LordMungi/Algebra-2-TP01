using CustomMath;
using System.Collections.Generic;

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
            if (isPlaneRedundant(Planes[i]))
                Planes.RemoveAt(i);
        }

    }

    private bool isPlaneRedundant(MyPlane plane)
    {
        Vec3 closestPointToCenter = plane.ClosestPointOnPlane(center);
        foreach (MyPlane other in Planes)
        {
            if (!other.GetSide(closestPointToCenter))
            {
                return false;
            }
        }
        return true;
    }
}
