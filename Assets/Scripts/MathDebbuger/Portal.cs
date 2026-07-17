using CustomMath;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [field: SerializeField] public Room room1 { get; private set; }
    [field: SerializeField] public Room room2 { get; private set; }

    public Vec3[] Points { get; private set; } = new Vec3[4];

    private void Awake()
    {
        CalculatePoints();
    }

    private void OnValidate()
    {
        CalculatePoints();
    }

    private void CalculatePoints()
    {
        Vector3 halfSize = transform.lossyScale * 0.5f;

        Points[0] = transform.position + transform.up * halfSize.y + transform.right * halfSize.x;
        Points[1] = transform.position + -transform.up * halfSize.y + transform.right * halfSize.x;
        Points[2] = transform.position + -transform.up * halfSize.y + -transform.right * halfSize.x;
        Points[3] = transform.position + transform.up * halfSize.y + -transform.right * halfSize.x;
    }

    private void OnDrawGizmos()
    {
        CalculatePoints();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Points[0], Points[1]);
        Gizmos.DrawLine(Points[1], Points[2]);
        Gizmos.DrawLine(Points[2], Points[3]);
        Gizmos.DrawLine(Points[3], Points[0]);
    }
}
