using CanvasDrawing.UtalEngine2D_2023_1;
using Shooter.GameModels;

namespace Shooter
{
    public partial class Form1 : Form
    {
        Image playerImage = global::Shooter.Properties.Resources.CorrerPistolas_2;
      
    

        private Moderator mod;

        public Form1()
        {
            GameEngine.MainCamera = new Camera(800, 800, 1);
            DoubleBuffered = true;
            InitializeComponent();
            screen inicioScreen = new screen(Moderator.screen.Inicio);
            System.Windows.Forms.Button start = new System.Windows.Forms.Button();


            mod = new Moderator(new Player(200, playerImage, new Vector2(50, 50), 0, 0), new Vector2(60, 50), 50, 50);
            mod.current = inicioScreen;

            mod.screens.Add(inicioScreen);
            GameEngine.InitEngine(this);
            

            

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}