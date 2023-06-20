using CanvasDrawing.Game;
using CanvasDrawing.UtalEngine2D_2023_1;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.GameModels
{
    public class Player : GameObject
    {
        public Moderator.ID id = Moderator.ID.player;
        public float speed;
        public Camera myCam;
        public Vector2 lastPos;
        public Image bulletImage = global::Shooter.Properties.Resources.bola;
        public float recoil = 0.3f;
        public float inmunity;

        public Player (float speed, Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0) : base(newSprite, newSize, xPos, yPos)
        {
            myCam = GameEngine.MainCamera;
            this.speed = speed;
        }

        public override void OnCollisionEnter(GameObject other)
        {
            Bullet bullet = other as Bullet;

            if (bullet != null && inmunity <= 0)
            {
                if (bullet.id == Moderator.ID.enemy)
                {
                    Moderator.playerHP--;
                    inmunity = 3f;
                }
            }

            Enemy enemy = other as Enemy;

            if (enemy != null && inmunity <= 0)
            {
                Moderator.playerHP--;
                inmunity = 3f;
            }
        }

        public override void Update()
        {
            bool moved = false;
            Vector2 auxLastPos = transform.position;

            recoil -= Time.deltaTime;
            inmunity -= Time.deltaTime;

            //Movimiento según WASD
            if (GameEngine.KeyPress(Keys.W))
            {
                transform.position.y -= speed * Time.deltaTime;
                moved = true;
            }

            if (GameEngine.KeyPress(Keys.S))
            {
                transform.position.y += speed * Time.deltaTime;
                moved = true;
            }

            if (GameEngine.KeyPress(Keys.A))
            {
                transform.position.x -= speed * Time.deltaTime;
                moved = true;
            }

            if (GameEngine.KeyPress(Keys.D))
            {
                transform.position.x += speed * Time.deltaTime;
                moved = true;
            }

            if (moved) //actualización de cámara y de última posición
            {
                lastPos = auxLastPos;
                myCam.Position = transform.position;
            }

            //Disparo de la bala
            if (GameEngine.KeyPress(Keys.Up) && recoil <= 0) 
            {
                new Bullet(id, new Vector2(0, -1), speed * 3, bulletImage, renderer.size, transform.position.x, transform.position.y);
                recoil = 0.3f;
            }

            if (GameEngine.KeyPress(Keys.Down) && recoil <= 0)
            {
                new Bullet(id, new Vector2(0, 1), speed * 3, bulletImage, renderer.size, transform.position.x, transform.position.y);
                recoil = 0.3f;
            }

            if (GameEngine.KeyPress(Keys.Left) && recoil <= 0)
            {
                new Bullet(id, new Vector2(-1, 0), speed * 3, bulletImage, renderer.size, transform.position.x, transform.position.y);
                recoil = 0.3f;
            }

            if (GameEngine.KeyPress(Keys.Right) && recoil <= 0)
            {
                new Bullet(id, new Vector2(1, 0), speed * 3, bulletImage, renderer.size, transform.position.x, transform.position.y);
                recoil = 0.3f;
            }
        }
    }
}
