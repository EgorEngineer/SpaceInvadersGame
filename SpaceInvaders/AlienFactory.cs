using System;
using System.Drawing;

namespace SpaceInvaders
{
    class AlienFactory:AsteroidFactory
    { 
        Size size;
        int speed;
        int health;
        int numbOfPositions;

        public int ScreenW { private get; set; }
        public AlienFactory(Size size, int speed, int health, int numberOfPositions) : base(size, speed, health,numberOfPositions)
        {
            this.size = size;
            this.speed = speed;
            this.health = health;
            this.numbOfPositions = numberOfPositions;
        }

        public AlienShip CreateAlien()
        {
            AlienShip alien = new AlienShip(size, speed);
            alien.SetLocation(RandomizeX(), -40);
            return alien;
        }

        // возвращает рандомную координату x
        // исходя из ширины окна и ширины врага
        int RandomizeX()
        {
            int interval = ScreenW / numbOfPositions;
            return new Random().Next(1, numbOfPositions) * interval + size.Width / 2;
        }
    }
}
