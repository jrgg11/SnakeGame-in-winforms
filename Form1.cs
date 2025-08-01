﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        Random rand;

        enum gameBoardFields
        {
            empty,
            snake,
            food
        };

        enum direction
        {
            up,
            down,
            left,
            right
        };

        struct snakeCoordinates
        {
            public int x;
            public int y;
        };

        gameBoardFields[,] gameBoardArray;
        snakeCoordinates[] snakeXY;
        int snakeLength;
        direction snakeDirection;
        Graphics graphics;

        public Form1()
        {
            InitializeComponent();
            gameBoardArray = new gameBoardFields[11, 11];
            snakeXY = new snakeCoordinates[100];
            rand = new Random();
            
        }

        private void Food()
        {
            int x, y;

            do
            {
                x = rand.Next(1, 10);
                y = rand.Next(1, 10);
            } while (gameBoardArray[x, y] != gameBoardFields.empty); // Ensure food does not spawn on snake or border

            gameBoardArray[x, y] = gameBoardFields.food; // Mark food position in the game board array
            graphics.DrawImage(imgList.Images[0], x * 35, y * 35); // Draw food on the game board
        }

        private void snakeLoad(object sender, EventArgs e)
        {
            gameBoard.Image = new Bitmap(420, 420);
            graphics = Graphics.FromImage(gameBoard.Image);
            graphics.Clear(Color.White);

            for(int i = 0; i < 11; i++)                                              //
                for (int j = 0; j < 11; j++)                                        // Border
                    if (i == 0 || i == 10 || j == 0 || j == 10)                    // 
                        graphics.DrawImage(imgList.Images[1], i * 35, j * 35);    //   

            // Initial snake head position
            snakeXY[0].x = 5; 
            snakeXY[0].y = 5;

            //Initial snake body position
            snakeXY[1].x = 5;
            snakeXY[1].y = 6;
            snakeXY[2].x = 5;
            snakeXY[2].y = 7;

            graphics.DrawImage(imgList.Images[3], snakeXY[0].x * 35, snakeXY[0].y * 35);    // Draw snake head
            gameBoardArray[5,5] = gameBoardFields.snake; // Mark snake head position in the game board array

            for (int i = 1; i < 3; i++)
            {
                graphics.DrawImage(imgList.Images[2], snakeXY[i].x * 35, snakeXY[i].y * 35);    // Draw snake body
                gameBoardArray[snakeXY[i].x, snakeXY[i].y] = gameBoardFields.snake; // Mark snake body position in the game board array
            }

            snakeDirection = direction.up; // Set initial direction of the snake
            snakeLength = 3; // Set initial length of the snake

            Food(); // Spawn initial food
        }

        private void snakeKey_Down(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    snakeDirection = direction.up;
                    break;
                case Keys.Down:
                    snakeDirection = direction.down;
                    break;
                case Keys.Left:
                    snakeDirection = direction.left;
                    break;
                case Keys.Right:
                    snakeDirection = direction.right;
                    break;
            }
        }

        private void GameOver()
        {
            timer.Stop();
            MessageBox.Show("Game Over! Your score: " + (snakeLength - 3));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            graphics.FillRectangle(Brushes.White, snakeXY[snakeLength-1].x * 35, snakeXY[snakeLength - 1].y * 35, 35, 35); // Clear the tail
            gameBoardArray[snakeXY[snakeLength - 1].x, snakeXY[snakeLength - 1].y] = gameBoardFields.empty; // Mark tail position as empty

            for (int i = snakeLength; i > 0; i--) // Move the snake body
            {
                snakeXY[i] = snakeXY[i - 1];
            }

            graphics.DrawImage(imgList.Images[2], snakeXY[0].x * 35, snakeXY[0].y * 35); // Draw the new head position

            switch (snakeDirection)
            {
                case direction.up:
                    snakeXY[0].y--;
                    break;
                case direction.down:
                    snakeXY[0].y++;
                    break;
                case direction.left:
                    snakeXY[0].x--;
                    break;
                case direction.right:
                    snakeXY[0].x++;
                    break;
            }

            // Check for collisions with walls and snake body
            if (snakeXY[0].x < 1 || snakeXY[0].x > 10 || snakeXY[0].y < 1 || snakeXY[0].y > 10 ||
                gameBoardArray[snakeXY[0].x, snakeXY[0].y] == gameBoardFields.snake)
            {
                GameOver();
                return;
            }
            else if (gameBoardArray[snakeXY[0].x, snakeXY[0].y] == gameBoardFields.food) // Check if snake eats food
            {
                graphics.DrawImage(imgList.Images[2], snakeXY[snakeLength].x * 35, snakeXY[snakeLength].y * 35); // Draw the new body part
                gameBoardArray[snakeXY[snakeLength].x, snakeXY[snakeLength].y] = gameBoardFields.snake; // Mark new body position as snake
                snakeLength++;

                if(snakeLength < 100) // Check if snake length is within bounds
                {
                    Food(); // Spawn new food
                }

                this.Text = "Snake Game - Score: " + (snakeLength - 3); // Update score
            }

            graphics.DrawImage(imgList.Images[3], snakeXY[0].x * 35, snakeXY[0].y * 35); // Draw the new head position
            gameBoardArray[snakeXY[0].x, snakeXY[0].y] = gameBoardFields.snake; // Mark new head position as snake

            gameBoard.Refresh();
        }
    }
}
