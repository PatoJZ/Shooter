﻿using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter.GameModels
{
    public class Moderator : EmptyUpdatable
    {
        public enum screen { Inicio, Juego, Fin }
        public enum ID { player, enemy }

        static public int score = 0;
        static public int playerHP = 3;
        static public List<Enemy> enemies = new List<Enemy>();
        public Player player;
        public bool win = false;

        public UtalText Score = new UtalText(Convert.ToString(score), 20, 20);
        public List<GameModels.screen> screens = new List<GameModels.screen>();
        public GameModels.screen current;
        Image TurretImage = Properties.Resources.Enemigo_Rayo_Idle;
        private button changeScreenButton;
        private bool isButtonDestroyed = false;

        public Moderator(Player player, Image spriteImage, Vector2 buttonSize, float xPos, float yPos)
        {
            this.player = player;
            //Funcion para crear las 3 pantallas
            screens.Add(new GameModels.screen(screen.Inicio));
            screens.Add(new GameModels.screen(screen.Juego));
            screens.Add(new GameModels.screen(screen.Fin));
            current = screens.Find(c => c.type == screen.Inicio); //screen.Inicio

            // Crear el botón para cambiar de pantalla
            changeScreenButton = new button(spriteImage, buttonSize, xPos, yPos);
           
        }

        public override void Update()
        {
            CheckChangeScreenButton();

            if (current.type == screen.Juego)
            {
                Score.drawString = Convert.ToString(score);

                if (playerHP == 0)
                {
                    GameEngine.Destroy(player);
                    current = screens.Find(c => c.type == screen.Fin);
                }

                if (enemies.Count == 0)
                {
                    current = screens.Find(c => c.type == screen.Fin);
                    win = true;
                }
            }

            if (current.type == screen.Fin)
            {
                if (win)
                {

                }
                else
                {

                }
            }
        }
        private void CheckChangeScreenButton()
        {
            if (changeScreenButton == null && current.type == screen.Inicio)
            {
                current = screens.Find(c => c.type == screen.Juego);
                Console.WriteLine("Cambio de pantalla");
                new Turret(TurretImage, new Vector2(50, 50), 100, 100);
            }
        }
    }
}
