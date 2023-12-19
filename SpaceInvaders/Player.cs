using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Player
    {
        Size size;
        int interval;
        int speed, x, y;
        int screenW, screenH;
        int numberOfPositions;
        
        public PictureBox Sprite { get; private set; }
        public int Lives { get; set; }
        public int Score { get; set; }

        public Player(Size size, int numberOfPositions, int numberOfLives)
        {
            Lives = numberOfLives;
            this.size = size;
            this.numberOfPositions = numberOfPositions;

            // инициализация текстуры игрока
            Sprite = new PictureBox
            {
                Tag = "Player",
                Size = size,
                BackColor = Color.Transparent,
                Image = Properties.Resources.player,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Sprite.BringToFront();
        }
        
        public void Reposition(int screenW, int screenH)
        {
            this.screenW = screenW;
            this.screenH = screenH;

            this.interval = screenW / numberOfPositions;
            this.speed = interval;

            this.x = (numberOfPositions / 2) * interval;
            this.y = screenH - 150;

            Sprite.Location = new Point(x, y);
        }
        
        public void MoveLeft()
        {
            if (x <= speed)
                return;

            x -= speed;
            Sprite.Left -= speed;
        }

        public void MoveRight()
        {
            if (x >= interval * numberOfPositions - interval)
                return;

            x += speed;
            Sprite.Left += speed;
        }

        public Missle CreateMissle(Size size, int speed)
        {
            Missle missle = new Missle(size, speed);
            missle.SetLocation(x + this.size.Width / 2, y);
            return missle;
        }

        public FastMissle CreateFastMissle(Size size, int speed)
        {
            FastMissle fastmissle = new FastMissle(size, speed);
            fastmissle.SetLocation(x + this.size.Width / 2, y);
            return fastmissle;
        }  
    }
}
