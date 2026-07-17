using System;
using UnityEngine;

namespace CustomMath
{
    public struct MyPlane : IEquatable<MyPlane>
    {
        #region Variables

        public Vec3 normal;
        public float distance;

        public MyPlane planeFlipped { get { return new MyPlane(-normal, -distance); } }

        #endregion

        #region Constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Constructors

        public MyPlane(Vec3 inNormal, Vec3 inPoint)
        {
            normal = inNormal.normalized;
            distance = Vec3.Dot(normal, inPoint);
        }

        public MyPlane(Vec3 inNormal, float d)
        {
            normal = inNormal.normalized;
            distance = d;
        }

        public MyPlane(Vec3 a, Vec3 b, Vec3 c)
        {
            normal = Vec3.Cross(b - a, c - a).normalized;
            distance = Vec3.Dot(normal, a);
        }

        #endregion

        #region Operators

        public static bool operator ==(MyPlane lhs, MyPlane rhs)
        {
            return lhs.normal == rhs.normal && MathF.Abs(lhs.distance - rhs.distance) < epsilon;
        }

        public static bool operator !=(MyPlane lhs, MyPlane rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator MyPlane(Plane p)
        {
            return new MyPlane(-p.normal, p.distance);
        }

        #endregion

        #region Functions

        public static MyPlane Translate(MyPlane plane, Vec3 translation)
        {
            return new MyPlane(plane.normal, Vec3.Dot(plane.normal, (plane.normal * plane.distance) + translation));
        }

        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            return point + normal * (distance - Vec3.Dot(normal, point));
        }

        public void Flip()
        {
            this = planeFlipped;
        }

        public float GetDistanceToPoint(Vec3 point)
        {
            return Vec3.Dot(normal, point) - distance;
        }

        public bool GetSide(Vec3 point)
        {
            return Vec3.Dot(normal, point) > distance;
        }

        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            return GetSide(inPt0) == GetSide(inPt1);
        }

        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            this = new MyPlane(a, b, c);
        }

        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            this = new MyPlane(inNormal, inPoint);
        }

        public void Translate(Vec3 translation)
        {
            this = Translate(this, translation);
        }
        public override string ToString()
        {
            return "Normal = (" + normal.x.ToString() + ", " + normal.y.ToString() + ", " + normal.z.ToString() + ") Distance = " + distance.ToString();
        }

        #endregion

        #region Internals

        public override bool Equals(object other)
        {
            if (!(other is MyPlane)) return false;
            return Equals((MyPlane)other);
        }

        public bool Equals(MyPlane other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(normal, distance);
        }

        #endregion
    }
}
