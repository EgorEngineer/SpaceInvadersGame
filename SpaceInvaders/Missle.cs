using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    internal class Missle
    {
        public Size size;
        public int speed, x, y, damage;

        public PictureBox Sprite { get; private set; }

        public Missle(Size size, int speed)
        {
            this.size = size;
            this.speed = speed;

            Sprite = new PictureBox
            {
                Tag = "missile",
                Size = size,
                BackColor = Color.Transparent,
                Image = Properties.Resources.missle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Sprite.BringToFront();
        }

        public void SetLocation(int x, int y)
        {
            this.x = x - size.Width / 2;
            this.y = y - size.Height / 2;
            Sprite.Location = new Point(this.x, this.y);
        }

        public void Move()
        {
            y -= speed;
            Sprite.Top -= speed;
        }

        public int Damage()
        {
            return damage;
        }
        public bool IsOutOfScreen()
        {
            return y < 0;
        } 
    }
}
