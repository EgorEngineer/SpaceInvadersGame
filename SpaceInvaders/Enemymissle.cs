using System.Drawing;
using System.Windows.Forms;


namespace SpaceInvaders
{
    class Enemymissle : Missle
    {
        public new int x, y;
        public new PictureBox Sprite { get; private set; }

        public Enemymissle(Size size, int speed) : base(size,speed)
        {

            Sprite = new PictureBox
            {
                Tag = "enemymissile",
                Size = size,
                BackColor = Color.Transparent,
                Image = Properties.Resources.enemymissle,
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
            y += speed;
            Sprite.Top += speed;
        }

        public bool IsOutOfScreen(int screenH)
        {
            return y > screenH;
        }
    }
}
