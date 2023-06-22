using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.GameModels
{
    public class screen : EmptyUpdatable
    {
        public Moderator.screen type; //Esta es para definir si es la pantalla de inicio, juego o Final
        public List<UtalText> texts = new List<UtalText>();
        public List<System.Windows.Forms.Button> buttons = new List<System.Windows.Forms.Button>();

        // No se qué más puedas llegar a necesitar para una pantalla, pero feel free
        // de cambiarlo como necesites

        public screen(Moderator.screen type)
        {
            this.type = type;
            texts = new List<UtalText>();
            buttons = new List<System.Windows.Forms.Button>();
        }

        public screen(Moderator.screen type, List<UtalText> texts, List<System.Windows.Forms.Button> buttons) 
        {
            this.type = type;
            this.texts = texts;
            this.buttons = buttons;
        }

        public override void Update()
        {
            
        }
    }
}
