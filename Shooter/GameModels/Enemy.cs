using CanvasDrawing.Game;
using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.GameModels
{
    public class Enemy : GameObject
    {
        public Moderator.ID id = Moderator.ID.enemy;
        public int HP = 3;
        public float recoil;
        public int bulletSpeed = 500;
        public Image bulletImage = Properties.Resources.frame_01;

        public Enemy(Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0) : base(newSprite, newSize, xPos, yPos)
        {
            Moderator.enemies.Add(this);
        }

        public override void OnCollisionEnter(GameObject other)
        {
            Bullet bullet = other as Bullet;

            if (bullet != null)
            {
                if (bullet.id == Moderator.ID.player)
                {
                    GameEngine.Destroy(bullet);
                    HP--;

                    if (HP <= 0) 
                    {
                        GameEngine.Destroy(this);
                    }
                }
            }
        }
    }
}
