using System;

namespace aaa
{
    public class Vector
    {
        private float m_X;
        private float m_Y;

        public Vector(float i_X, float i_Y, bool i_pollarCordinates = false)
        {
            m_X = i_X;
            m_Y = i_Y;
   
        }

        public Vector(float theta)
        {
            m_X = (float)Math.Cos(theta);
            m_Y = (float)Math.Sin(theta);
        }

        public static Vector operator +(Vector i_u, Vector i_v)
        {
            return new Vector(i_u.m_X + i_v.m_X, i_u.m_Y + i_v.m_Y);
        }

        public static Vector operator *(Vector i_vector, float i_scalar)
        {
            return new Vector(i_vector.X * i_scalar, i_vector.Y * i_scalar);
        }

        public float GetMagnitude()
        {
            return (float)Math.Sqrt(m_X * m_X + m_Y * m_Y);
        }

        public Vector Copy()
        {
            return new Vector(X, Y);
        }

        public float X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        public float Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }
    }
}
