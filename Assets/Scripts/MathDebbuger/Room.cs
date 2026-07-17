using UnityEngine;
using CustomMath;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    private MyPlane[] planes = new MyPlane[6];
    private Color color;

    private void Awake()
    {
        CalculatePlanes();
    }

    private void OnValidate()
    {
        CalculatePlanes();
    }

    private void CalculatePlanes()
    {
        Vector3 center = transform.position;
        Vector3 halfSize = transform.lossyScale * 0.5f;

        planes[0].normal = transform.right;
        planes[0].distance = Vec3.Dot(transform.right, center) + halfSize.x;

        planes[1].normal = -transform.right;
        planes[1].distance = -(Vec3.Dot(transform.right, center) - halfSize.x);

        planes[2].normal = transform.forward;
        planes[2].distance = Vec3.Dot(transform.forward, center) + halfSize.z;

        planes[3].normal = -transform.forward;
        planes[3].distance = -(Vec3.Dot(transform.forward, center) - halfSize.z);

        planes[4].normal = transform.up;
        planes[4].distance = Vec3.Dot(transform.up, center) + halfSize.y;

        planes[5].normal = -transform.up;
        planes[5].distance = -(Vec3.Dot(transform.up, center) - halfSize.y);

        color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    public bool IsPointInsideRoom(Vec3 point)
    {
        foreach (MyPlane p in planes)
        {
            if (p.GetSide(point))
                return false;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vec3.Zero, Vec3.One);        
    }
}
