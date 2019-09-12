using System.Drawing;

namespace aaa
{
    public class Ball : Entity
    {
        public static int RADIUS = 5;
        public static int INITIALE_SPEED = 3;
        public static float ACCELERATION = 0.1f;

        private Vector m_Position;
        private Vector m_Velocity;
        private bool m_IsPlaying;
        private float m_Speed;

        public Ball()
        {
            m_Position = new Vector(0, 0);
            m_Velocity = new Vector(0, 0);
            m_IsPlaying = false;
            m_Speed = INITIALE_SPEED;
        }

        public void Update(Game i_game)
        {
            if (m_IsPlaying)
            {
                m_Position += m_Velocity;
                CollisionDetection(i_game);
            }
            else
            {
                m_Speed = INITIALE_SPEED;
                m_Position = new Vector(i_game.Player.Position.X, i_game.Player.Position.Y - 0.5f * Player.HEIGHT - RADIUS);
            }
        }

        public void Display(Graphics i_g)
        {
            SolidBrush solidBrush = new SolidBrush(Color.Blue);
            i_g.FillEllipse(solidBrush, m_Position.X - RADIUS, m_Position.Y - RADIUS, 2 * RADIUS, 2 * RADIUS);
        }

        public void CollisionDetection(Game i_game)
        {
            if (m_Position.X + RADIUS + 15 > Game.WIDTH)
            {
                m_Velocity.X = -1 * m_Velocity.X;

            }

            if (m_Position.X - RADIUS < 0)
            {
                m_Velocity.X = -1 * m_Velocity.X;
            }

            if (m_Position.Y - RADIUS < 0)
            {
                m_Velocity.Y = -1 * m_Velocity.Y;
            }

            if (m_Position.Y > Game.WIDTH)
            {
                m_IsPlaying = false;
            }
        }

        public bool IsPlaying
        {
            get { return m_IsPlaying; }
            set { m_IsPlaying = value; }
        }

        public Vector Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public Vector Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }

        public float Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }
    }
}
