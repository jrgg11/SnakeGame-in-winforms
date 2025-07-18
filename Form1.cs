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

        gameBoardFields[,] gameBoard;
        snakeCoordinates[] snakeXY;
        int snakeLength;
        direction snakeDirection;
        Graphics graphics;

        public Form1()
        {
            InitializeComponent();
            gameBoard = new gameBoardFields[11, 11];
            snakeXY = new snakeCoordinates[100];
            rand = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
