using System.Drawing;
using System;

namespace aaa
{
    public class Brick : Entity
    {
        public static Color[] COLORS = { Color.Yellow, Color.Purple };
        public static int WIDTH = 40;
        public static int HEIGHT = 20;
        public static float PROBABILITY_OF_FALLER = 0.25f;

        private Vector m_Position;
        private Color m_Color;
        private int m_Hp;
        private int m_Index;

        public Brick(Vector i_position, int i_hp, int i_index)
        {
            m_Position = i_position;
            m_Color = COLORS[i_hp - 1];
            m_Hp = i_hp;
            m_Index = i_index;
        }

        public void Update(Game i_game)
        {
            CollisionDetection(i_game);
        }

        public void Display(Graphics i_g)
        {
            SolidBrush solidBrush = new SolidBrush(m_Color);
            i_g.FillRectangle(solidBrush, new Rectangle((int)(m_Position.X - 0.5 * WIDTH), (int)(m_Position.Y - 0.5 * HEIGHT), WIDTH, HEIGHT));
            Pen pen = new Pen(Color.Black);
            i_g.DrawRectangle(pen, new Rectangle((int)(m_Position.X - 0.5 * WIDTH), (int)(m_Position.Y - 0.5 * HEIGHT), WIDTH, HEIGHT));
        }

        public void CollisionDetection(Game i_game)
        {
            if (i_game.Ball.Position.X <= m_Position.X + 0.5 * WIDTH &&
                i_game.Ball.Position.X >= m_Position.X - 0.5 * WIDTH &&
                i_game.Ball.Position.Y + Ball.RADIUS <= m_Position.Y - 0.5 * HEIGHT + i_game.Ball.Speed &&
                i_game.Ball.Position.Y + Ball.RADIUS >= m_Position.Y - 0.5 * HEIGHT)
            {
                i_game.Ball.Position = (new Vector(i_game.Ball.Position.X, m_Position.Y - 0.5f * HEIGHT - Ball.RADIUS - 1));
                i_game.Ball.Velocity = (new Vector(i_game.Ball.Velocity.X, -1 * i_game.Ball.Velocity.Y));
                OnCollision(i_game);
            }

            if (i_game.Ball.Position.X <= m_Position.X + 0.5 * WIDTH &&
                i_game.Ball.Position.X >= m_Position.X - 0.5 * WIDTH &&
                i_game.Ball.Position.Y - Ball.RADIUS <= m_Position.Y + 0.5 * HEIGHT &&
                i_game.Ball.Position.Y - Ball.RADIUS >= m_Position.Y + 0.5 * HEIGHT +-i_game.Ball.Speed)
            {
                i_game.Ball.Position = new Vector(i_game.Ball.Position.X, m_Position.Y + 0.5f * HEIGHT + Ball.RADIUS + 1);
                i_game.Ball.Velocity = new Vector(i_game.Ball.Velocity.X, -1 * i_game.Ball.Velocity.Y);
                OnCollision(i_game);
            }

            if (i_game.Ball.Position.X + Ball.RADIUS <= m_Position.X - 0.5 * WIDTH + i_game.Ball.Speed &&
                i_game.Ball.Position.X + Ball.RADIUS >= m_Position.X - 0.5 * WIDTH &&
                i_game.Ball.Position.Y <= m_Position.Y + 0.5 * HEIGHT &&
                i_game.Ball.Position.Y >= m_Position.Y - 0.5 * HEIGHT)
            {
                i_game.Ball.Position = new Vector(m_Position.X - 0.5f * WIDTH - Ball.RADIUS - 1, i_game.Ball.Position.Y);
                i_game.Ball.Velocity = new Vector(-1 * i_game.Ball.Velocity.X, i_game.Ball.Velocity.Y);
                OnCollision(i_game);
            }

            if (i_game.Ball.Position.X - Ball.RADIUS <= m_Position.X + 0.5 * WIDTH &&
                i_game.Ball.Position.X - Ball.RADIUS >= m_Position.X + 0.5 * WIDTH - i_game.Ball.Speed &&
                i_game.Ball.Position.Y <= m_Position.Y + 0.5 * HEIGHT &&
                i_game.Ball.Position.Y >= m_Position.Y - 0.5 * HEIGHT)
            {
                i_game.Ball.Position = new Vector(m_Position.X + 0.5f * WIDTH + Ball.RADIUS + 1, i_game.Ball.Position.Y);
                i_game.Ball.Velocity = new Vector(-1 * i_game.Ball.Velocity.X, i_game.Ball.Velocity.Y);
                OnCollision(i_game);
            }
        }

        public void OnCollision(Game i_game)
        {
            i_game.Ball.Speed = i_game.Ball.Speed + Ball.ACCELERATION;
            if (--m_Hp == 0)
            {
                i_game.Bricks[m_Index] = null;
                Random random = new Random();
                float rand = (float)random.NextDouble();
                if (rand < PROBABILITY_OF_FALLER)
                {
                    for (int i = 0; i < i_game.Fallers.Length; i++)
                    {
                        if (i_game.Fallers[i] == null)
                        {
                            if (rand < 0.125)
                            {
                                i_game.Fallers[i] = new SmallerSizeFaller(m_Position, i);
                            }
                            else
                            {
                                i_game.Fallers[i] = new BiggerSizeFaller(m_Position, i);
                            }

                            break;
                        }
                    }
                }

                return;
            }

            m_Color = COLORS[m_Hp - 1];
        }

        public Vector Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }
    }
}
