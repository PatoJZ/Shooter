using Shooter.GameModels;
using System;
using System.Drawing;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public class button : GameObject
    {
        private bool isPressed;

        public button(Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0)
            : base(newSprite, newSize, true, xPos, yPos)
        {
        }

        public override void OnCollisionEnter(GameObject other)
        {
            Player player = other as Player;

            if (player != null)
            {
                GameEngine.Destroy(this); // destruir el boton


            }

        } 
    }
}
