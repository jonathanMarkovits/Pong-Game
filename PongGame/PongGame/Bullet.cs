using System.Drawing;

namespace PongGame
{
    public class Bullet : Entity
    {
        public static int WIDTH = 5;
        public static int HEIGHT = 5;
        public static int SPEED = 3;

        private Vector m_Position;
        private int m_Index;

        public Bullet(Vector i_position, int i_index)
        {
            m_Position = i_position;
            m_Index = i_index;
        }

        public void Update(Game i_game)
        {
            m_Position.Y -= SPEED;
            CollisionDetection(i_game);
        }

        public void Display(Graphics i_g)
        {
            SolidBrush solidBrush = new SolidBrush(Color.Orange);
            i_g.FillRectangle(solidBrush, new Rectangle((int)(m_Position.X - 0.5 * WIDTH), (int)(m_Position.Y - 0.5 * HEIGHT), WIDTH, HEIGHT));
        }

        public void CollisionDetection(Game i_game)
        {
            for (int i = 0; i < i_game.Bricks.Length; i++)
            {
                if (i_game.Bricks[i] != null)
                {
                    if (m_Position.Y - 0.5 * HEIGHT <= i_game.Bricks[i].Position.Y + 0.5 * Brick.HEIGHT &&
                        m_Position.Y - 0.5 * HEIGHT >= i_game.Bricks[i].Position.Y + 0.5 * Brick.HEIGHT - SPEED &&
                        m_Position.X <= i_game.Bricks[i].Position.X + 0.5 * Brick.WIDTH &&
                        m_Position.X >= i_game.Bricks[i].Position.X - 0.5 * Brick.WIDTH)
                    {
                        i_game.Bricks[i].OnCollision(i_game);
                        i_game.Bullets[m_Index] = null;
                    }
                }
            }

            if (m_Position.Y < 0)
            {
                i_game.Bullets[m_Index] = null;
            }
        }
    }
}
