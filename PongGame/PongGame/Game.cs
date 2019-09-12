using System.Windows.Forms;
using System.Drawing;
using System;

namespace PongGame
{
    public class Game : Form
    {
        public static int WIDTH = 300;
        public static int HEIGHT = 300;
        public static int MAX_NUMBER_OF_FALLERS = 4;
        public static int MAX_NUMBER_OF_BRICKS = 64;

        private static Timer timer = new Timer();

        private Ball m_Ball;
        private Player m_Player;
        private Brick[] m_Bricks;
        private Faller[] m_Fallers;
        private Bullet[] m_Bullets;

        public Game()
        {
            DoubleBuffered = true;
            Width = WIDTH;
            Height = HEIGHT;
            Text = "Pong Game";

            m_Player = new Player();
            m_Ball = new Ball();
            m_Bricks = new Brick[MAX_NUMBER_OF_BRICKS];
            m_Fallers = new Faller[MAX_NUMBER_OF_FALLERS];
            m_Bullets = new Bullet[2];

            firstStage();

            timer.Tick += new EventHandler(TimerEventProcessor);
            KeyDown += new KeyEventHandler(KeyEventProssesor);
            timer.Interval = 20;
            timer.Start();
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            bool isStageUnfinished = true;
            m_Ball.Update(this);
            m_Player.Update(this);

            for (int i = 0; i < m_Bullets.Length; i++)
            {
                if (m_Bullets[i] != null)
                {
                    m_Bullets[i].Update(this);
                }
            }

            for (int i = 0; i < m_Fallers.Length; i++)
            {
                if (m_Fallers[i] != null)
                {
                    m_Fallers[i].Update(this);
                }
            }

            for (int i = 0; i < m_Bricks.Length; i++)
            {
                if (m_Bricks[i] != null)
                {
                    m_Bricks[i].Update(this);
                    isStageUnfinished = false;
                }
            }

            if (isStageUnfinished)
            {
                nextStage();
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            SolidBrush solidBrush = new SolidBrush(Color.Black);
            g.FillRectangle(solidBrush, new Rectangle(0, 0, WIDTH, HEIGHT));
            m_Ball.Display(g);
            m_Player.Display(g);
            for (int i = 0; i < m_Bricks.Length; i++)
            {
                if (m_Bricks[i] != null)
                {
                    m_Bricks[i].Display(g);
                }
            }

            for (int i = 0; i < m_Fallers.Length; i++)
            {
                if (m_Fallers[i] != null)
                {
                    m_Fallers[i].Display(g);
                }
            }

            for (int i = 0; i < m_Bullets.Length; i++)
            {
                if (m_Bullets[i] != null)
                {
                    m_Bullets[i].Display(g);
                }
            }
        }

        private void KeyEventProssesor(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !m_Ball.IsPlaying)
            {
                m_Ball.IsPlaying = !m_Ball.IsPlaying;
            }

            if (e.KeyCode == Keys.Space && m_Ball.IsPlaying && m_Bullets[0] == null && m_Bullets[1] == null)
            {
                m_Player.Shoot(m_Bullets);
            }

            if (e.KeyCode.Equals(Keys.Right) && m_Player.Position.X + 0.5 * m_Player.Width + 20 <= WIDTH)
            {
                m_Player.Position.X += m_Player.Speed;
            }

            if (e.KeyCode.Equals(Keys.Left) && m_Player.Position.X - 0.5 * m_Player.Width - 5 >= 0)
            {
                m_Player.Position.X -= m_Player.Speed;
            }
        }

        private void firstStage()
        {
            bool flipBrick = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int index = i + j * 5;
                    if (flipBrick)
                    {
                        m_Bricks[index] = new Brick(new Vector(i * Brick.WIDTH + 60, j * Brick.HEIGHT + 50), 1, index);
                    }
                    else
                    {
                        m_Bricks[index] = new Brick(new Vector(i * Brick.WIDTH + 60, j * Brick.HEIGHT + 50), 2, index);
                    }

                    flipBrick = !flipBrick;
                }
            }
        }

        private void nextStage()
        {
            Ball.IsPlaying = false;
            bool flipBrick = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int index = i + j * 5;
                    if (flipBrick)
                    {
                        m_Bricks[index] = new Brick(new Vector(i * Brick.WIDTH + 60, j * Brick.HEIGHT + 50), 1, index);
                    }
                    else
                    {
                        m_Bricks[index] = new Brick(new Vector(i * Brick.WIDTH + 60, j * Brick.HEIGHT + 50), 2, index);
                    }
                }
            }
        }

        public Player Player
        {
            get { return m_Player; }
            set { m_Player = value; }
        }

        public Ball Ball
        {
            get { return m_Ball; }
            set { m_Ball = value; }
        }

        public Faller[] Fallers
        {
            get { return m_Fallers; }
            set { m_Fallers = value; }
        }

        public Brick[] Bricks
        {
            get { return m_Bricks; }
            set { m_Bricks = value; }
        }

        public Bullet[] Bullets
        {
            get { return m_Bullets; }
            set { m_Bullets = value; }
        }
    }
}
