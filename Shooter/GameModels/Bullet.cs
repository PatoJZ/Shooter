using CanvasDrawing.UtalEngine2D_2023_1;
using Shooter.GameModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.Game
{
    public class Bullet : GameObject
    {
        public Moderator.ID id;
        public Vector2 direction;
        public float speed;
        public float timeToDie = 1f;

        public Bullet(Moderator.ID id, Vector2 dir, float speed, Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0) : base(newSprite, newSize, false, xPos, yPos)
        {
            this.id = id;
            this.direction = dir;
            this.speed = speed;
        }

        public override void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
            
            if (timeToDie > 0)
            {
                timeToDie -= Time.deltaTime;
            }
            else
            {
                GameEngine.Destroy(this);
            }
        }
    }
}
