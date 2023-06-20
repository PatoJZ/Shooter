    using CanvasDrawing.UtalEngine2D_2023_1;
    using Shooter.GameModels;

    namespace Shooter
    {
        public partial class Form1 : Form
        {
            Image playerImage = global::Shooter.Properties.Resources.CorrerPistolas_2;
            Image TurretImage = Properties.Resources.Enemigo_Rayo_Idle;

        private Moderator mod;

        public Form1()
            {
                GameEngine.MainCamera = new Camera(800, 800, 1);
                DoubleBuffered = true; 
                InitializeComponent();
                screen inicioScreen = new screen(Moderator.screen.Inicio);
                Button start = new Button();

                start.Text = "START";
                start.Size = new Size(200, 50);
                start.Location = new Point((Width / 2) - 20 - (start.Width / 2), (Height / 2) + 100 - (start.Height / 2));
                start.Click += StartButton_Click;
                this.Controls.Add(start);

                mod = new Moderator(new Player(200, playerImage, new Vector2(50, 50), 0, 0));
                mod.current = inicioScreen;
                


                mod.screens.Add(inicioScreen);
                GameEngine.InitEngine(this);
                StartPosition = FormStartPosition.CenterScreen;

                new Turret(TurretImage, new Vector2(50, 50), 100, 100);
                
            }
            private void StartButton_Click(object sender, EventArgs e)
            {
                mod.current = mod.screens.Find(c => c.type == Moderator.screen.Juego);
                Hide();
            }
        }
    }