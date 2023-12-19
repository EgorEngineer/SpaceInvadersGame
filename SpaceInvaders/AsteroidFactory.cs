using System;
using System.Drawing;

namespace SpaceInvaders
{
    class AsteroidFactory
    {
        Size size;
        int speed;
        int health;
        int numberOfPositions;

        public int ScreenW { private get; set; }
        public AsteroidFactory(Size size, int speed, int health, int numberOfPositions)
        {
            this.size = size;
            this.speed = speed;
            this.health = health;
            this.numberOfPositions = numberOfPositions;
        }

        public Asteroid CreateAsteroid()
        {
            Asteroid asteroid = new Asteroid(size, speed);
            asteroid.SetLocation(RandomizeX(), -100);
            return asteroid;
        }

        // возвращает рандомную координату x
        // исходя из ширины окна и ширины врага
        int RandomizeX()
        {
            int interval = ScreenW / numberOfPositions;
            return new Random().Next(1, numberOfPositions) * interval + size.Width / 2;
        }

    }
}
