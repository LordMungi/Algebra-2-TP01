using UnityEngine;
using CustomMath;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    private MyPlane[] planes = new MyPlane[6];

    private void Awake()
    {
        CalculatePlanes();
    }

    void Update()
    {
        
    }

    private void OnValidate()
    {
        CalculatePlanes();
    }

    private void CalculatePlanes()
    {
        Vector3 center = transform.position;

        Vector3 halfSize = transform.lossyScale * 0.5f;

        planes[0].normal = new Vec3(1, 0, 0);
        planes[0].distance = center.x + halfSize.x;

        planes[1].normal = new Vec3(-1, 0, 0);
        planes[1].distance = -(center.x - halfSize.x);

        planes[2].normal = new Vec3(0, 0, 1);
        planes[2].distance = center.z + halfSize.z;

        planes[3].normal = new Vec3(0, 0, -1);
        planes[3].distance = -(center.z - halfSize.z);

        planes[4].normal = new Vec3(0, 1, 0);
        planes[4].distance = center.y + halfSize.y;

        planes[5].normal = new Vec3(0, -1, 0);
        planes[5].distance = -(center.y - halfSize.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }
}
