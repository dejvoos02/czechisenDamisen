using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CESKA_DAMA_EDIT;

/// <summary>
/// V této třídě si konstruuju šachovnici s defaultní pozicí figurek. Pozastavil jsem se nad rozestavěním figurek ve Vámi přiloženém odkaze,
/// kde bílá 0. řádek 0. sloupec není figurka, kdežto na všech šachovnicích/"dámovnicích", co jsem našel, začíná hlavní dark-squared
/// diagonála z pohledu bílého vlevo a jde doprava nahoru, tzn A0 do H8, proto jsem to implementoval takhle.
///
/// Následně zde implementuji tisk, kde to tisknu z pohledu bílého hráče(P1)
/// </summary>
public class Board
{
    int[,] board = new int[8, 8];
    public int[,] BoardState
    {
        get { return board; }
    }
    public Board()
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (i < 3)
                {
                    if ((i + j) % 2 == 0)
                    {
                        board[i, j] = 1;
                    }
                }
                else if (i > 4)
                {
                    if ((i + j) % 2 == 0)
                    {
                        board[i, j] = 2;
                    }
                }
            }
        }
    }

    public void Print()
    {
        Console.WriteLine("   A  B  C  D  E  F  G  H"); 

        for (int i = board.GetLength(0) - 1; i >= 0; i--) 
        {
            Console.Write((i + 1) + "  "); // Čísla řádků vlevo

            for (int j = 0; j < board.GetLength(1); j++) // Procházení sloupců zleva doprava
            {
                Console.Write(board[i, j] + "  ");
            }

            Console.WriteLine();
        }

        Console.WriteLine("   A  B  C  D  E  F  G  H"); 
    }


    public void Update(int player, int x, int y)
    {

    }
}