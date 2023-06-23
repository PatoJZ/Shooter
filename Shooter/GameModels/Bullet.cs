using CanvasDrawing.UtalEngine2D_2023_1;
using Shooter.GameModels;
using Shooter.Properties;
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
        public Image[] animationImages;
        public int indexAnim;
        public float timeBTFrames = 0.2f;
        public float animTimer = 0.2f;

        public Bullet(Moderator.ID id, Vector2 dir, float speed, Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0) : base(newSprite, newSize, false, xPos, yPos)
        {
            this.id = id;
            this.direction = dir;
            this.speed = speed;
            rigidbody.colliders[0].isSolid = false;
            rigidbody.isStatic = true;
            animationImages = new Image[12];
            animationImages[0] = newSprite;
            animationImages[1] = Resources.frame_02;
            animationImages[2] = Resources.frame_03;
            animationImages[3] = Resources.frame_04;
            animationImages[4] = Resources.frame_05;
            animationImages[5] = Resources.frame_06;
            animationImages[6] = Resources.frame_07;
            animationImages[7] = Resources.frame_08;
            animationImages[8] = Resources.frame_09;
            animationImages[9] = Resources.frame_10;
            animationImages[10] = Resources.frame_11;
            animationImages[11] = Resources.frame_12;
        }

        public override void Update()
        {
            base.Update();
            animTimer -= Time.deltaTime;
            if (animTimer <= 0)
            {
                indexAnim++;
                indexAnim %= animationImages.Length;
                renderer.rotatedSprite = animationImages[indexAnim];
                animTimer = timeBTFrames;
            }

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
