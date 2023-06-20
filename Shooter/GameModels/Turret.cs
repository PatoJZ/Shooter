using CanvasDrawing.Game;
using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.GameModels
{
    internal class Turret : Enemy
    {
        Random r = new Random();
        List<Vector2> dirsUsed = new List<Vector2>();

        public Turret(Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0) : base(newSprite, newSize, xPos, yPos)
        {
            
        }

        public override void Update()
        {
            recoil -= Time.deltaTime;

            if (recoil <= 0)
            {
                recoil = 1f;

                bool shot;
                dirsUsed.Clear();

                for (int i = 0; i < 3; i++) 
                {
                    shot = false;

                    while (!shot)
                    {
                        Vector2 dir = new Vector2(r.Next(-2, 2), r.Next(-2, 2));
                        //dir = Vector2.Normalize(dir);

                        if (dirsUsed.Count == 0)
                        {
                            new Bullet(id, dir, bulletSpeed, bulletImage, renderer.size, transform.position.x, transform.position.y);
                            dirsUsed.Add(dir);
                            shot = true;
                        }
                        else if (!dirsUsed.Contains(dir))
                        {
                            new Bullet(id, dir, bulletSpeed, bulletImage, renderer.size, transform.position.x, transform.position.y);
                            dirsUsed.Add(dir);
                            shot = true;
                        }
                    }
                }
            }
        }
    }
}
