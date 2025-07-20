using System;
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

        private void Form1_Load(object sender, EventArgs e)
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

            for (int i = 1; i < 3; i++)
            {
                graphics.DrawImage(imgList.Images[2], snakeXY[i].x * 35, snakeXY[i].y * 35);    // Draw snake body
            }
        }

    }
}
