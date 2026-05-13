using System;

namespace CustomMath
{
    public struct MyPlane : IEquatable<MyPlane>
    {
        #region Variables

        public Vec3 normal;
        public float distance;

        public MyPlane planeFlipped { get { throw new NotImplementedException(); } }

        #endregion

        #region Constructors

        MyPlane(Vec3 inNormal, Vec3 inPoint)
        {
            throw new NotImplementedException();
        }

        MyPlane(Vec3 inNormal, float d)
        {
            throw new NotImplementedException();
        }

        MyPlane(Vec3 a, Vec3 b, Vec3 c)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Operators

        public static bool operator ==(MyPlane lhs, MyPlane rhs)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(MyPlane lhs, MyPlane rhs)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Functions

        public MyPlane Translate(MyPlane plane, Vec3 translation)
        {
            throw new NotImplementedException();
        }

        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            throw new NotImplementedException();
        }

        public void Flip()
        {
            throw new NotImplementedException();
        }

        public float GetDistanceToPoint(Vec3 point)
        {
            throw new NotImplementedException();
        }

        public bool GetSide(Vec3 point)
        {
            throw new NotImplementedException();
        }

        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            throw new NotImplementedException();
        }

        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            throw new NotImplementedException();
        }

        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            throw new NotImplementedException();
        }

        public void Translate(Vec3 translation)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Internals

        public override bool Equals(object other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(MyPlane other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
