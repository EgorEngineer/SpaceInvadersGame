using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class AlienShip : Asteroid
    {
        Size size;
        int speed, x, y;

        public new PictureBox Sprite { get; private set; }
        //public new int Health { get; set; }
        //public new bool Dead { get; set; }
        public AlienShip(Size size, int speed) : base(size, speed)
        {
            this.size = size;
            this.speed = speed;

            Sprite = new PictureBox
            {
                Size = size,
                BackColor = Color.Transparent,
                Image = Properties.Resources.AlienShip,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Sprite.BringToFront();
        }

        public new void SetLocation(int x, int y)
        {
            this.x = x - size.Width / 2;
            this.y = y - size.Width / 2;
            Sprite.Location = new Point(this.x, this.y);
        }

        public bool Location()
        {
            if (y > 0)
                return true;
            else
                return false;
        }
        public new void Move()
        {
            y += speed;
            Sprite.Top += speed;
        }

        public Enemymissle CreateMissle(Size size, int speed)
        {
            Enemymissle alienmissle = new Enemymissle(size, speed);
            alienmissle.SetLocation(x + this.size.Width / 2, y);
            return alienmissle;
        }

        public new bool IsDead()
        {
            return Dead;
        }

        public new bool IsOutOfScreen(int screenH)
        {
            return y > screenH;
        }

        public new bool IsCollision(Rectangle rect)
        {
            return Sprite.Bounds.IntersectsWith(rect);
        }
    }
}


