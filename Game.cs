using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESKA_DAMA_EDIT
{
    internal class Game
    {
        Board board = new Board();
        bool isPlayerOne = true;

        public Game() { }

        public void Start()
        {
            while (true)
            {
                board.Print();
                Console.WriteLine(isPlayerOne ? "Player One's turn" : "Player Two's turn");

                Console.Write("Which stone do you want to move (A-F,1-8): ");
                string where = Console.ReadLine();
                if (IsSelectedValid(ConvertInputIntoCord(where)))
                {
                    Console.Write("Where do you want to move the stone (A-F,1-8): ");
                    string to = Console.ReadLine();

                    if (IsMoveValid(ConvertInputIntoCord(where), ConvertInputIntoCord(to)))
                    {
                        Console.WriteLine("Valid move!");
                        isPlayerOne = !isPlayerOne; // Střídání hráčů
                    }
                    else
                    {
                        Console.WriteLine("Invalid move, try again!");
                    }
                }
                else
                {
                    Console.WriteLine("You don't have a stone on this coordination! Try again.");
                    continue;
                }
                Console.WriteLine("Do you wish to continue? y/n");
                if (Console.ReadLine() == "n")
                    break;
            }
        }

        public bool IsSelectedValid(int[] cord)
        {
            int x = cord[0];
            int y = cord[1];

            if (!(x >= 0 && x <= 7 && y >= 0 && y <= 7))
                return false;
            int color = board.BoardState[x, y];

            return IsPlayerPiece(color);
        }

        public bool IsMoveValid(int[] from, int[] to)
        {
            int from_i = from[0];
            int from_j = from[1];

            int to_i = to[0];
            int to_j = to[1];

            if (!(to_i >= 0 && to_i <= 7 && to_j >= 0 && to_j <= 7))
            {
                return false;
            }

            int piece = board.BoardState[from_i, from_j];
            int pieceOnPosition = board.BoardState[to_i, to_j];

            if (piece == 1 || piece == 2)
            {
                return IsValidMoveDirection(from, to) && TryMove(from, to, pieceOnPosition) || TryJump(from, to, pieceOnPosition);
            }

            return false;
        }

        private bool IsPlayerPiece(int piece)
        {
            switch (piece)
            {
                case 1:
                case 3:
                    return isPlayerOne;
                case 2:
                case 4:
                    return !isPlayerOne;
                default:
                    return false;
            }
        }

        private bool IsValidMoveDirection(int[] from, int[] to)
        {
            int direction = isPlayerOne ? 1 : -1;
            int from_i = from[0];
            int from_j = from[1];
            int to_i = to[0];
            int to_j = to[1];

            return to_i == from_i + direction && (to_j == from_j + 1 || to_j == from_j - 1);
        }

        private bool TryMove(int[] from, int[] to, int pieceOnPosition)
        {
            if (pieceOnPosition != 0)
            {
                return false;
            }

            int piece = board.BoardState[from[0], from[1]];
            board.BoardState[from[0], from[1]] = 0;
            board.BoardState[to[0], to[1]] = piece;

            return true;
        }

        private bool TryJump(int[] from, int[] to, int pieceOnPosition)
        {
            if (pieceOnPosition != 0)
            {
                return false;
            }

            int from_i = from[0];
            int from_j = from[1];
            
            int to_i = to[0];
            int to_j = to[1];
            
            int middle_i = (from_i + to_i) / 2;
            int middle_j = (from_j + to_j) / 2;
            int middlePiece = board.BoardState[middle_i, middle_j];

            bool isOpponentPiece = isPlayerOne ? (middlePiece == 2 || middlePiece == 4) : (middlePiece == 1 || middlePiece == 3);

            if (isOpponentPiece && Math.Abs(to_i - from_i) == 2 && Math.Abs(to_j - from_j) == 2)
            {
                int piece = board.BoardState[from_i, from_j];
                board.BoardState[from_i, from_j] = 0;
                board.BoardState[middle_i, middle_j] = 0;
                board.BoardState[to_i, to_j] = piece;

                return true;
            }

            return false;
        }

        public int[] ConvertInputIntoCord(string input)
        {
            return new int[]
            {
                (int)input[1] - 49, 
                (int)input[0] - 65  
            };
        }

        public int Abs(int number)
        {
            return number < 0 ? -number : number;
        }
    }
}
