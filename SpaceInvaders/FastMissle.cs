using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    internal class FastMissle : Missle
    {
        new int  x, y;
        public new PictureBox Sprite { get; private set; }
        public FastMissle(Size size, int speed) : base(size, speed)
        {
            Sprite = new PictureBox
            {
                Tag = "fastmissile",
                Size = size,
                BackColor = Color.Transparent,
                Image = Properties.Resources.fastmissle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Sprite.BringToFront();
        }
        public new void SetLocation(int x, int y)
        {
            this.x = x - size.Width / 2;
            this.y = y - size.Height / 2;
            Sprite.Location = new Point(this.x, this.y);
        }

        public new void Move()
        {
            y -= speed;
            Sprite.Top -= speed;
        }

        public new int Damage()
        {
            return damage;
        }

        public new bool IsOutOfScreen()
        {
            return y < 0;
        }
    }
}

