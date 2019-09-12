using System.Drawing;
using System;
using System.Windows.Forms;

namespace PongGame
{
    public class Player : Entity
    {
        public static int WIDTH = 50;
        public static int HEIGHT = 5;
        public static int SPEED = 5;
        public static int HP = 3;

        private Vector m_Position;
        private int m_Speed;
        private int m_Width;
        private int m_Hp;

        public Player()
        {
            m_Position = new Vector(Game.WIDTH / 2, Game.HEIGHT * 0.8f);
            m_Speed = SPEED;
            m_Width = WIDTH;
            m_Hp = HP;
        }

        public void Update(Game i_game)
        {
            CollisionDetection(i_game);
        }

        public void Display(Graphics i_g)
        {
            SolidBrush solidBrush = new SolidBrush(Color.Gray);
            Pen pen = new Pen(Color.Black);
            i_g.FillRectangle(solidBrush, new Rectangle((int)(m_Position.X - 0.5 * Width), (int)(m_Position.Y - 0.5 * HEIGHT), Width, HEIGHT));
        }

        public void CollisionDetection(Game i_game)
        {
            float angle;
            if (i_game.Ball.Position.Y + Ball.RADIUS >= m_Position.Y - 0.5 * HEIGHT &&
                i_game.Ball.Position.Y + Ball.RADIUS <= m_Position.Y + 0.5 * HEIGHT &&
                i_game.Ball.Position.X < m_Position.X + 0.5 * Width &&
                i_game.Ball.Position.X > m_Position.X - 0.5 * Width)
            {
                angle = (i_game.Ball.Position.X - m_Position.X - 0.5f * Width) / Width * (float)Math.PI;
                i_game.Ball.Position = new Vector(i_game.Ball.Position.X, m_Position.Y - 0.5f * HEIGHT - Ball.RADIUS);
                i_game.Ball.Velocity = new Vector(angle) * i_game.Ball.Speed;
            }
        }

        public void Shoot(Bullet[] i_fire)
        {
            i_fire[0] = new Bullet(new Vector(Position.X - 0.5f * Width, Position.Y), 0);
            i_fire[1] = new Bullet(new Vector(Position.X + 0.5f * Width, Position.Y), 1);
        }

        public Vector Position
        {
            get { return m_Position; }
        }


        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }

        public int Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }
    }
}
