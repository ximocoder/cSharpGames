﻿using System;
using System.Threading;


class Program
{
    static int left = 0;
    static int right = 1;
    static int up = 2;
    static int down = 3;


    static int firstPlayerScore = 0;
    static int firstPlayerDirection = right;
    static int firstPlayerColumn = 0; // column
    static int firstPlayerRow = 0; // row


    static int secondPlayerScore = 0;
    static int secondPlayerDirection = left;
    static int secondPlayerColumn = 40; // column
    static int secondPlayerRow = 5; // row


    static bool[,] isUsed;


    static void Main(string[] args)
    {
        SetGameField();
        StartupScreen();

        isUsed = new bool[Console.WindowWidth, Console.WindowHeight];


        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                ChangePlayerDirection(key);
            }


            MovePlayers();


            bool firstPlayerLoses = DoesPlayerLose(firstPlayerRow, firstPlayerColumn);
            bool secondPlayerLoses = DoesPlayerLose(secondPlayerRow, secondPlayerColumn);


            if (firstPlayerLoses && secondPlayerLoses)
            {
                firstPlayerScore++;
                secondPlayerScore++;
                Console.WriteLine();
                Console.WriteLine("Game over");
                Console.WriteLine("Draw game!!!");
                Console.WriteLine("Current score: {0} - {1}", firstPlayerScore, secondPlayerScore);
                ResetGame();
            }
            if (firstPlayerLoses)
            {
                secondPlayerScore++;
                Console.WriteLine();
                Console.WriteLine("Game over");
                Console.WriteLine("Second player wins!!!");
                Console.WriteLine("Current score: {0} - {1}", firstPlayerScore, secondPlayerScore);
                ResetGame();
            }
            if (secondPlayerLoses)
            {
                firstPlayerScore++;
                Console.WriteLine();
                Console.WriteLine("Game over");
                Console.WriteLine("First player wins!!!");
                Console.WriteLine("Current score: {0} - {1}", firstPlayerScore, secondPlayerScore);
                ResetGame();
            }


            isUsed[firstPlayerColumn, firstPlayerRow] = true;
            isUsed[secondPlayerColumn, secondPlayerRow] = true;


            WriteOnPosition(firstPlayerColumn, firstPlayerRow, '*', ConsoleColor.Yellow);
            WriteOnPosition(secondPlayerColumn, secondPlayerRow, '*', ConsoleColor.Cyan);


            Thread.Sleep(100);
        }
    }


    static void StartupScreen()
    {
        string heading = "A simple tron-like game";
        Console.CursorLeft = Console.BufferWidth / 2 - heading.Length / 2;
        Console.WriteLine(heading);


        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Player 1's controls:\n");
        Console.WriteLine("W - Up");
        Console.WriteLine("A - Left");
        Console.WriteLine("S - Down");
        Console.WriteLine("D - Right");

        string longestString = "Player 2's controls:";
        int cursorLeft = Console.BufferWidth - longestString.Length;

        Console.CursorTop = 1;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.CursorLeft = cursorLeft;
        Console.WriteLine("Player 2's controls:");
        Console.CursorLeft = cursorLeft;
        Console.WriteLine("Up Arrow - Up");
        Console.CursorLeft = cursorLeft;
        Console.WriteLine("Left Arrow - Left");
        Console.CursorLeft = cursorLeft;
        Console.WriteLine("Down Arrow - Down");
        Console.CursorLeft = cursorLeft;
        Console.WriteLine("Right Arrow - Right");

        Console.ReadKey();
        Console.Clear();
    }
    static void ResetGame()
    {
        isUsed = new bool[Console.WindowWidth, Console.WindowHeight];
        SetGameField();
        firstPlayerDirection = right;
        secondPlayerDirection = left;
        Console.WriteLine("Press any key to start again...");
        Console.ReadKey();
        Console.Clear();
        MovePlayers();
    }


    static bool DoesPlayerLose(int row, int col)
    {
        if (row < 0)
        {
            return true;
        }
        if (col < 0)
        {
            return true;
        }
        if (row >= Console.WindowHeight)
        {
            return true;
        }
        if (col >= Console.WindowWidth)
        {
            return true;
        }


        if (isUsed[col, row])
        {
            return true;
        }


        return false;
    }


    static void SetGameField()
    {
        Console.WindowHeight = 30;
        Console.BufferHeight = 30;


        Console.WindowWidth = 100;
        Console.BufferWidth = 100;


        /*
         * 
         * ->>>>            <<<<-
         * 
         */
        firstPlayerColumn = 0;
        firstPlayerRow = Console.WindowHeight / 2;


        secondPlayerColumn = Console.WindowWidth - 1;
        secondPlayerRow = Console.WindowHeight / 2;
    }


    static void MovePlayers()
    {
        if (firstPlayerDirection == right)
        {
            firstPlayerColumn++;
        }
        if (firstPlayerDirection == left)
        {
            firstPlayerColumn--;
        }
        if (firstPlayerDirection == up)
        {
            firstPlayerRow--;
        }
        if (firstPlayerDirection == down)
        {
            firstPlayerRow++;
        }


        if (secondPlayerDirection == right)
        {
            secondPlayerColumn++;
        }
        if (secondPlayerDirection == left)
        {
            secondPlayerColumn--;
        }
        if (secondPlayerDirection == up)
        {
            secondPlayerRow--;
        }
        if (secondPlayerDirection == down)
        {
            secondPlayerRow++;
        }
    }


    static void WriteOnPosition(int x, int y, char ch, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.SetCursorPosition(x, y);
        Console.Write(ch);
    }


    static void ChangePlayerDirection(ConsoleKeyInfo key)
    {
        if (key.Key == ConsoleKey.W && firstPlayerDirection != down)
        {
            firstPlayerDirection = up;
        }
        if (key.Key == ConsoleKey.A && firstPlayerDirection != right)
        {
            firstPlayerDirection = left;
        }
        if (key.Key == ConsoleKey.D && firstPlayerDirection != left)
        {
            firstPlayerDirection = right;
        }
        if (key.Key == ConsoleKey.S && firstPlayerDirection != up)
        {
            firstPlayerDirection = down;
        }


        if (key.Key == ConsoleKey.UpArrow && secondPlayerDirection != down)
        {
            secondPlayerDirection = up;
        }
        if (key.Key == ConsoleKey.LeftArrow && secondPlayerDirection != right)
        {
            secondPlayerDirection = left;
        }
        if (key.Key == ConsoleKey.RightArrow && secondPlayerDirection != left)
        {
            secondPlayerDirection = right;
        }
        if (key.Key == ConsoleKey.DownArrow && secondPlayerDirection != up)
        {
            secondPlayerDirection = down;
        }
    }
}

/*
******
     ***************
  ####            **
 #####
 * 
 * 
 *->>>              <<-
 * 
 * 
 * 
*/

/*
 W         ^
ASD       <v>
*/