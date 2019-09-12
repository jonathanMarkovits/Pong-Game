using System.Drawing;

namespace aaa
{
    public abstract class Faller : Entity
    {
        public static int WIDTH = 15;
        public static int HEIGHT = 15;
        public static int SPEED = 2;

        private Vector m_Position;
        private Vector m_Velocity;
        private Color m_Color;
        private int m_Index;

        public Faller(Vector i_position, int i_index)
        {
            m_Position = i_position;
            m_Velocity = new Vector(0, SPEED);
            m_Color = Color.Red;
            m_Index = i_index;
        }

        public void Update(Game i_game)
        {
            m_Position += m_Velocity;
            CollisionDetection(i_game);
        }

        public void Display(Graphics i_g)
        {
            SolidBrush solidBrush = new SolidBrush(m_Color);
            i_g.FillRectangle(solidBrush, new Rectangle((int)(m_Position.X - 0.5 * WIDTH), (int)(m_Position.Y - 0.5 * HEIGHT), WIDTH, HEIGHT));
        }

        public void CollisionDetection(Game i_game)
        {
            if (m_Position.Y + 0.5 * HEIGHT >= i_game.Player.Position.Y - 0.5 * Player.HEIGHT &&
                m_Position.Y + 0.5 * HEIGHT <= i_game.Player.Position.Y + 0.5 * Player.HEIGHT &&
                m_Position.X < i_game.Player.Position.X + 0.5 * i_game.Player.Width &&
                m_Position.X > i_game.Player.Position.X - 0.5 * i_game.Player.Width)
            {
                i_game.Fallers[m_Index] = null;
                MakeEffect(i_game);
            }

            if (m_Position.Y > Game.HEIGHT)
            {
                i_game.Fallers[m_Index] = null;
            }
        }

        public abstract void MakeEffect(Game i_game);

        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }
    }
}
