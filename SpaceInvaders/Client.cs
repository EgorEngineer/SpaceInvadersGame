using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;


namespace SpaceInvaders
{
    public partial class Client : Form
    {
        #region Параметры
        SoundPlayer ambient = new SoundPlayer(Properties.Resources.ambient);
        SoundPlayer fire = new SoundPlayer(Properties.Resources.misslefire);
        SoundPlayer fly = new SoundPlayer(Properties.Resources.flying);
        SoundPlayer boom = new SoundPlayer(Properties.Resources.asterBOOM);
        SoundPlayer enemyfire = new SoundPlayer(Properties.Resources.enemyfire);

        int tickCount;
        
        Player player;
        List<Missle> missles;
        List<FastMissle> fastmissles;
        List<Asteroid> asteroids;
        List<AlienShip> aliens;
        List<Enemymissle> enemymissles;
        AsteroidFactory asteroidFactory;
        AlienFactory alienFactory;

        // параметры
        int tickInterval = 25;
        int AlientickInterval = 40;
        int AlienAtackInterval = 50;
        int numberOfPositions = 10;
        int AlienNumberOfPositions = 10;
        int numberOfLives = 3;
        int asteroidSpeed = 10;
        int asteroidHealth = 1;
        int alienHealth = 2;
        int alienSpeed = 15;
        int missleSpeed = 40;
        int enemymissleSpeed = 50;
        int fastmissleSpeed = 90;
        Pair<bool, bool> FireTag = new Pair<bool, bool>(false, false);
        Pair<bool, bool> check = new Pair<bool, bool>(false, true);
        bool ambcheck = false;
        bool helpch = false;
        bool pausech = false;
        bool spcheck = false;

        Pair<int,int> moment = new Pair<int, int>(1,1);
        Size playerSize = new Size(75, 75);
        Size asteroidSize = new Size(75, 75);
        Size aliensize = new Size(75, 75);
        Size missleSize = new Size(15, 45);
        Size fastmissleSize = new Size(10, 35);
        Size enemymissleSize = new Size(50, 40);

        #endregion

        #region Игровая область
        public Client()
        {
            InitializeComponent();
            fly.Play();

            missles = new List<Missle>();
            asteroids = new List<Asteroid>();
            fastmissles = new List<FastMissle>();
            aliens = new List<AlienShip>();
            enemymissles = new List<Enemymissle>();

            player = new Player(playerSize, numberOfPositions, numberOfLives);
            asteroidFactory = new AsteroidFactory(asteroidSize, asteroidSpeed, asteroidHealth, numberOfPositions);
            alienFactory = new AlienFactory(aliensize, alienSpeed, alienHealth, AlienNumberOfPositions);

            player.Score = 0;
            numberOfLivesLabel.Text = String.Format("Жизни = {0}", player.Lives);
            scoreLabel.Text = String.Format("Очки = {0:D2}", player.Score);

            Controls.Add(player.Sprite);
            fly.Dispose();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (player != null) player.Reposition(this.Width, this.Height);
            if (asteroidFactory != null) asteroidFactory.ScreenW = this.Width;
            if (alienFactory != null) alienFactory.ScreenW = this.Width;
        }

        #endregion

        #region Нажатие клавиш
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // движение вправо
            if (timer.Enabled && e.KeyCode == Keys.D)
            {
                player.MoveRight();
            }

            // движение влево
            if (timer.Enabled && e.KeyCode == Keys.A)
            {
                player.MoveLeft();
            }

            // стрельба ракетой
            if (timer.Enabled && e.KeyCode == Keys.Space && FireTag.First==false)
            {
                fire.Play();
                Missle missle = player.CreateMissle(missleSize, missleSpeed);
                missles.Add(missle);
                Controls.Add(missle.Sprite);
                FireTag.First = true;
                moment.First = tickCount;
                fire.Dispose();
            }

            //Кулдаун ракеты
            if (tickCount%moment.First > 5)
                FireTag.First = false;

            // стрельба быстрой ракетой
            if (timer.Enabled && e.KeyCode == Keys.E && FireTag.Second==false)
            {
                FastMissle fmissle = player.CreateFastMissle(fastmissleSize, fastmissleSpeed);
                fastmissles.Add(fmissle);
                Controls.Add(fmissle.Sprite);
                FireTag.Second = true;
                moment.Second = tickCount;
                fire.Play();
            }

            //Кулдаун быстрой ракеты
            if (tickCount%moment.Second > 5)
                FireTag.Second = false;

            // пауза
            if (e.KeyCode == Keys.Escape && check.First == false && helpch == false)
            {
                ambient.Play();
                timer.Enabled = !timer.Enabled;
                PauseLabel.Text = String.Format("ПАУЗА! НАЖМИТЕ ESC ДЛЯ ПРОДОЛЖЕНИЯ\n      НАЖМИТЕ ENTER ДЛЯ ПЕРЕЗАПУСКА,\n   ИЛИ НАЖМИТЕ L ДЛЯ ВЫХОДА ИЗ ИГРЫ!\n\n"+
                    "                              Очки: "+ player.Score +"\n\n\n            ОТКРЫТЬ МЕНЮ ПОМОЩИ - H");
                Controls.Add(PauseLabel);
                PauseLabel.BringToFront();
                check.Second =false;
                ambcheck = true;
                pausech = true;

            }

            if(e.KeyCode == Keys.PageDown)
            {
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.WindowState = FormWindowState.Normal;
            }

            if(e.KeyCode == Keys.PageUp)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }

            if (e.KeyCode == Keys.H && pausech ==false)
            {
                helpch = true;
                timer.Enabled = !timer.Enabled;
                Controls.Add(HelpLabel);
                HelpLabel.BringToFront();
            }

            // перезапуск
            if (!timer.Enabled && e.KeyCode == Keys.Enter && helpch == false)
            {
                Controls.Clear();
                asteroids.Clear();
                missles.Clear();
                player.Lives = numberOfLives;
                player.Reposition(this.Width, this.Height);
                FireTag.First = false;
                FireTag.Second = false;

                numberOfLivesLabel.Text = String.Format("Жизни = {0}", player.Lives);
                scoreLabel.Text = String.Format("Очки = {0:D2}", player.Score = 0);
                Controls.Add(numberOfLivesLabel);
                Controls.Add(scoreLabel);
                player.Sprite.Image = Properties.Resources.player;
                Controls.Add(player.Sprite);
                check.First = false;
                check.Second = true;
                timer.Start();
            }

            //закрытие игры
            if (!timer.Enabled && e.KeyCode == Keys.L)
            {
                Close();
            }
        }

        #endregion

        #region Динамика
        private void Timer_Tick(object sender, EventArgs e)
        {
            helpch = false;
            pausech = false;
            Controls.Remove(HelpLabel);
            Controls.Remove(PauseLabel);
            if (ambcheck)
            {
                ambient.Stop();
                ambient.Dispose();
                ambcheck = false;
            }
            // создание и отрисовка врагов через временной интервал
            if (tickCount % tickInterval == 0)
            {
                switch (player.Score)
                {
                    case 10:
                        asteroidSpeed = 12;
                        tickInterval = 20;
                        alienSpeed = 17;
                        break;
                    case 20:
                        asteroidSpeed = 15;
                        tickInterval = 15;
                        alienSpeed = 20;
                        break;
                    case 30:
                        asteroidSpeed = 18;
                        tickInterval = 15;
                        alienSpeed = 20;
                        break;

                }

                Asteroid asteroid = asteroidFactory.CreateAsteroid();
                AlienShip alien = alienFactory.CreateAlien();
                asteroids.Add(asteroid);
                Controls.Add(asteroid.Sprite);
                aliens.Add(alien);
                Controls.Add(alien.Sprite);
            }

            for (int i = aliens.Count - 1; i >= 0; i--)
            {
                if (tickCount > 3 && tickCount % AlienAtackInterval == 0 && aliens[i].Dead == false && aliens[i].Location() == true)
                {
                    enemyfire.Play();
                    Enemymissle enemymissle = aliens[i].CreateMissle(enemymissleSize, enemymissleSpeed);
                    enemymissles.Add(enemymissle);
                    Controls.Add(enemymissle.Sprite);
                    enemyfire.Dispose();
                }
            }

            #region Столкновения с игроком
            // враги
            for (int i = asteroids.Count - 1; i >= 0; i--)
            {
                asteroids[i].Move();

                // столкновение с игроком
                if (asteroids[i].Sprite.Bounds.IntersectsWith(player.Sprite.Bounds))
                {
                    if (player.Lives > 1 && asteroids[i].Dead == false)
                    {
                        Controls.Remove(asteroids[i].Sprite);
                        asteroids.RemoveAt(i);
                        player.Lives--;

                        numberOfLivesLabel.Text = String.Format("Жизни = {0}", player.Lives);
                        break;
                    }


                    if (asteroids[i].Dead == false && player.Lives == 1)
                    {
                        Controls.Remove(scoreLabel);
                        Controls.Remove(numberOfLivesLabel);
                        Controls.Remove(asteroids[i].Sprite);
                        player.Sprite.Image = Properties.Resources.BOOM;
                        aliens.Clear();
                        asteroids.Clear();
                        missles.Clear();
                        fastmissles.Clear();
                        enemymissles.Clear();
                        ambient.Play();
                        ambcheck = true;
                        RestartLabel.Text = "                    ИГРА ЗАВЕРШЕНА!\n    НАЖМИТЕ ENTER ДЛЯ ПЕРЕЗАПУСКА,\n ИЛИ НАЖМИТЕ L ДЛЯ ВЫХОДА ИЗ ИГРЫ!\n\n                        Результат: " + player.Score;
                        Controls.Add(RestartLabel);
                        RestartLabel.BringToFront();
                        timer.Enabled = false;
                        check.First = true;
                        break;
                    }

                    else if (asteroids[i].Dead == true)
                    {
                        player.Score++;
                        scoreLabel.Text = String.Format("Очки = {0:D2}", player.Score);
                        Controls.Remove(asteroids[i].Sprite);
                        asteroids.RemoveAt(i);
                    }

                }

                // удаление врагов за пределами
                if (asteroids[i].IsOutOfScreen(this.Height))
                {
                    Controls.Remove(asteroids[i].Sprite);
                    asteroids.RemoveAt(i);
                }

            }

            for (int i = aliens.Count - 1; i >= 0; i--)
            {
                aliens[i].Move();

                // столкновение с игроком
                if (aliens[i].Sprite.Bounds.IntersectsWith(player.Sprite.Bounds))
                {
                    if (player.Lives > 1 && aliens[i].Dead == false)
                    {
                        Controls.Remove(aliens[i].Sprite);
                        aliens.RemoveAt(i);
                        player.Lives--;

                        numberOfLivesLabel.Text = String.Format("Жизни = {0}", player.Lives);
                        break;
                    }

                    //Смерть игрока
                    if (aliens[i].Dead == false && player.Lives == 1)
                    {
                        Controls.Remove(scoreLabel);
                        Controls.Remove(numberOfLivesLabel);
                        Controls.Remove(aliens[i].Sprite);
                        player.Sprite.Image = Properties.Resources.BOOM;
                        asteroids.Clear();
                        aliens.Clear();
                        missles.Clear();
                        fastmissles.Clear();
                        enemymissles.Clear();
                        ambient.Play();
                        ambcheck = true;
                        RestartLabel.Text = "                    ИГРА ЗАВЕРШЕНА!\n    НАЖМИТЕ ENTER ДЛЯ ПЕРЕЗАПУСКА,\n ИЛИ НАЖМИТЕ L ДЛЯ ВЫХОДА ИЗ ИГРЫ!\n\n                        Результат: " + player.Score;
                        Controls.Add(RestartLabel);
                        RestartLabel.BringToFront();
                        timer.Enabled = false;
                        check.First = true;
                        break;
                    }

                    if (aliens[i].Dead == true && aliens[i].Sprite.Bounds.IntersectsWith(player.Sprite.Bounds))
                    {
                        Controls.Remove(aliens[i].Sprite);
                        aliens.RemoveAt(i);
                    }
                }

                //создание врага
                if (tickCount % AlientickInterval == 0 && aliens.Count <= 2)
                {
                    AlienShip alien = alienFactory.CreateAlien();
                    aliens.Add(alien);
                    Controls.Add(alien.Sprite);
                }

                // удаление пришельца за пределами
                else if (aliens[i].IsOutOfScreen(this.Height))
                {
                    Controls.Remove(aliens[i].Sprite);
                    aliens.RemoveAt(i);
                }
            }


            for (int i = enemymissles.Count - 1; i >= 0; i--)
            {
                enemymissles[i].Move();

                // столкновение с игроком
                if (enemymissles[i].Sprite.Bounds.IntersectsWith(player.Sprite.Bounds))
                {
                    if (player.Lives > 1)
                    {
                        Controls.Remove(enemymissles[i].Sprite);
                        enemymissles.RemoveAt(i);
                        player.Lives--;

                        numberOfLivesLabel.Text = String.Format("Жизни = {0}", player.Lives);
                        break;
                    }


                    else if (player.Lives == 1)
                    {
                        Controls.Remove(scoreLabel);
                        Controls.Remove(numberOfLivesLabel);
                        Controls.Remove(enemymissles[i].Sprite);
                        player.Sprite.Image = Properties.Resources.BOOM;
                        aliens.Clear();
                        asteroids.Clear();
                        missles.Clear();
                        fastmissles.Clear();
                        enemymissles.Clear();
                        ambient.Play();
                        ambcheck = true;
                        RestartLabel.Text = "                    ИГРА ЗАВЕРШЕНА!\n    НАЖМИТЕ ENTER ДЛЯ ПЕРЕЗАПУСКА,\n ИЛИ НАЖМИТЕ L ДЛЯ ВЫХОДА ИЗ ИГРЫ!\n\n                        Результат: " + player.Score;
                        Controls.Add(RestartLabel);
                        RestartLabel.BringToFront();
                        timer.Enabled = false;
                        check.First = true;
                        break;
                    }
                }

                // удаление вражеского выстрела за пределами
                if (enemymissles[i].IsOutOfScreen(this.Height))
                {
                    Controls.Remove(enemymissles[i].Sprite);
                    enemymissles.RemoveAt(i);
                }

            }

            #endregion

            #region Снаряды
            // снаряд
            for (int j = missles.Count - 1; j >= 0; j--)
            {
                missles[j].Move();

                // удаление снаряда за пределами
                if (missles[j].IsOutOfScreen())
                {
                    Controls.Remove(missles[j].Sprite);
                    missles.RemoveAt(j);
                }
            }

            // быстрая ракета
            for (int j = fastmissles.Count - 1; j >= 0; j--)
            {
                fastmissles[j].Move();

                // удаление снаряда за пределами
                if (fastmissles[j].IsOutOfScreen())
                {
                    Controls.Remove(fastmissles[j].Sprite);
                    fastmissles.RemoveAt(j);
                }
            }

            //вражеские выстрелы
            for (int j = enemymissles.Count - 1; j >= 0; j--)
            {
                enemymissles[j].Move();

                // удаление снаряда за пределами
                if (enemymissles[j].IsOutOfScreen(this.Height))
                {
                    Controls.Remove(enemymissles[j].Sprite);
                    enemymissles.RemoveAt(j);
                }
            }

            #endregion

            #region Столкновение врагов и снарядов
            // попадание снаряда в астероид
            for (int i = asteroids.Count - 1; i >= 0; i--)
            {
                for (int j = missles.Count - 1; j >= 0; j--)
                {
                    if (i > asteroids.Count - 1 || j > missles.Count - 1)
                        break;

                    if (asteroids[i].Sprite.Bounds.IntersectsWith(missles[j].Sprite.Bounds) && asteroids[i].Dead == false)
                    {
                        asteroids[i].Sprite.Size = new Size(75, 50);
                        asteroids[i].Sprite.Image = Properties.Resources.ruby;
                        asteroids[i].Sprite.BackColor = Color.Transparent;
                        asteroids[i].Dead = true;
                        boom.Play();
                        boom.Dispose();
                        Controls.Remove(missles[j].Sprite);
                        missles.RemoveAt(j);
                    }
                }
            }

            // попадание быстрого снаряда в астероид
            for (int i = asteroids.Count - 1; i >= 0; i--)
            {
                for (int j = fastmissles.Count - 1; j >= 0; j--)
                {
                    if (i > asteroids.Count - 1 || j > fastmissles.Count - 1)
                        break;

                    if (asteroids[i].Sprite.Bounds.IntersectsWith(fastmissles[j].Sprite.Bounds) && asteroids[i].Dead == false)
                    {
                        asteroids[i].Sprite.Size = new Size(75, 50);
                        asteroids[i].Sprite.Image = Properties.Resources.ruby;
                        asteroids[i].Sprite.BackColor = Color.Transparent;
                        asteroids[i].Dead = true;
                        boom.Play();
                        boom.Dispose();
                        Controls.Remove(fastmissles[j].Sprite);
                        fastmissles.RemoveAt(j);
                    }
                }
            }

            //попадание снаряда во вражеский корабль
            for (int i = aliens.Count - 1; i >= 0; i--)
            {
                for (int j = missles.Count - 1; j >= 0; j--)
                {
                    if (i > aliens.Count - 1 || j > missles.Count - 1)
                        break;

                    if (aliens[i].Sprite.Bounds.IntersectsWith(missles[j].Sprite.Bounds) && aliens[i].Dead == false)
                    {
                        aliens[i].Sprite.Image = Properties.Resources.BOOM;
                        aliens[i].Sprite.BackColor = Color.Transparent;
                        aliens[i].Dead = true;
                        boom.Play();
                        boom.Dispose();
                        Controls.Remove(missles[j].Sprite);
                        missles.RemoveAt(j);
                    }
                }
            }

            //попадание быстрого снаряда во вражеский корабль
            for (int i = aliens.Count - 1; i >= 0; i--)
            {
                for (int j = fastmissles.Count - 1; j >= 0; j--)
                {
                    if (i > aliens.Count - 1 || j > fastmissles.Count - 1)
                        break;

                    if (aliens[i].Sprite.Bounds.IntersectsWith(fastmissles[j].Sprite.Bounds) && aliens[i].Dead == false )
                    {
                        aliens[i].Sprite.Image = Properties.Resources.BOOM;
                        aliens[i].Sprite.BackColor = Color.Transparent;
                        aliens[i].Dead = true;
                        boom.Play();
                        boom.Dispose();
                        Controls.Remove(fastmissles[j].Sprite);
                        fastmissles.RemoveAt(j);
                    }
                }
            }

            //столкновение корабля пришельцев с астероидом
            for (int i = aliens.Count - 1; i >= 0; i--)
            {
                for (int j = asteroids.Count - 1; j >= 0; j--)
                {
                    if (i > aliens.Count - 1 || j > asteroids.Count - 1)
                        break;

                    if (aliens[i].Sprite.Bounds.IntersectsWith(asteroids[j].Sprite.Bounds) && aliens[i].Dead == false && tickCount % 2 == 0)
                    {
                        Controls.Remove(asteroids[j].Sprite);
                        asteroids.RemoveAt(j);
                    }

                    else if (aliens[i].Sprite.Bounds.IntersectsWith(asteroids[j].Sprite.Bounds) && asteroids[j].Dead == false && tickCount % 2 == 1)
                    {
                        Controls.Remove(aliens[i].Sprite);
                        aliens.RemoveAt(i);
                    }

                    else if ((aliens[i].Sprite.Bounds.IntersectsWith(asteroids[j].Sprite.Bounds) && asteroids[j].Dead == true && aliens[i].Dead == true))
                    {
                        Controls.Remove(aliens[i].Sprite);
                        aliens.RemoveAt(i);
                    }
                }
            }

            //попадание вражеского снаряда в астероид
            for (int i = asteroids.Count - 1; i >= 0; i--)
            {
                for (int j = enemymissles.Count - 1; j >= 0; j--)
                {
                    if (i > asteroids.Count - 1 || j > enemymissles.Count - 1)
                        break;

                    if (asteroids[i].Sprite.Bounds.IntersectsWith(enemymissles[j].Sprite.Bounds) && asteroids[i].Dead == false)
                    {
                        Controls.Remove(enemymissles[j].Sprite);
                        enemymissles.RemoveAt(j);
                        asteroids[i].Sprite.Image = Properties.Resources.BOOM;
                        asteroids[i].Sprite.Size = new Size(75, 75);
                        asteroids[i].Dead = true;
                        boom.Play();
                        boom.Dispose();
                    }
                }
            }

            //столкновение вражеский кораблей
            for (int i = aliens.Count - 1; i >= 0; i--)
            {
                for (int j = aliens.Count - 2; j >= 0; j--)
                {
                    if (i > aliens.Count - 1 || j > aliens.Count - 2 || aliens[i] == aliens[j])
                        break;

                    if (aliens[i].Sprite.Bounds.IntersectsWith(aliens[j].Sprite.Bounds))
                    {
                        Controls.Remove(aliens[j].Sprite);
                        aliens.RemoveAt(j);
                    }
                }
            }
            Cursor.Hide();
            tickCount++;

            #endregion
        }

        #endregion
    }
}
