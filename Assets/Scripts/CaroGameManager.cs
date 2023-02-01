using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CaroGame
{
    public class CaroGameManager : MonoBehaviour
    {
        [Header("Config Game")] [SerializeField]
        private CaroConfig config;

        [Space(1f)] [Header("Generate Board")] 
        [SerializeField] private GameObject cellPrefabs;

        [SerializeField] private Transform boardTransform;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        public int BoardSize { get; set; }

        [Space(1f)] [Header("Game Handler")] 
        [SerializeField] private string currentTurn = "x";
        
        [SerializeField] private string[,] board;
        [SerializeField] private List<Cell> cellList;

        public string[,] Board
        {
            get => board;
            set => board = value;
        }

        // Start is called before the first frame update
        void Start()
        {
            OnInit();
        }

        private void OnInit()
        {
            BoardSize = config.boardSize;
            board = new string[BoardSize, BoardSize];
            GenerateBoard();
        }

        private void GenerateBoard()
        {
            gridLayoutGroup.constraintCount = BoardSize;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    var obj = Instantiate(cellPrefabs, boardTransform);
                    var cell = obj.GetComponent<Cell>();
                    cell.OnInit(this, i, j);
                    cellList.Add(cell);
                    board[i, j] = "";
                }
            }
        }

        public void Check(Cell cell)
        {
            board[cell.Row, cell.Col] = currentTurn;
            cell.ChangeImage(currentTurn == "x" ? config.xSprite : config.oSprite);
            if (CheckWin(cell.Row, cell.Col))
            {
                Debug.Log("EndGame");
            }

            currentTurn = currentTurn == "x" ? "o" : "x";
        }
        public void Check(int row, int col)
        {
            Check(GetCellByPos(row, col));
        }
        private Cell GetCellByPos(int row, int col)
        {
            foreach (var cell in cellList)
            {
                if (cell.Row == row && cell.Col == col)
                {
                    return cell;
                }
            }

            return null;
        }
        
        #region  Win Check

        private bool CheckWin(int row, int col)
        {
            if (CheckRow(row, col))
            {
                return true;
            }

            if (CheckCol(row, col))
            {
                return true;
            }

            if (CheckDiagonalRight(row, col))
            {
                return true;
            }

            if (CheckDiagonalLeft(row, col))
            {
                return true;
            }

            return false;
        }

        private bool CheckRow(int row, int col)
        {
            int count = 1;
            for (int i = col - 1; i > -1; i--)
            {
                if (board[row, i] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            for (int i = col + 1; i < BoardSize; i++)
            {
                if (board[row, i] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count >= 5;
        }

        private bool CheckCol(int row, int col)
        {
            int count = 1;
            for (int i = row - 1; i > -1; i--)
            {
                if (board[i, col] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            for (int i = row + 1; i < BoardSize; i++)
            {
                if (board[i, col] == currentTurn)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count >= 5;
        }

        private bool CheckDiagonalRight(int row, int col)
        {
            int count = 1;
            for (int i = row - 1; i > -1; i--) // len 1 dong
            {
                int colPos = col - (row - i); // len may dong thi sang trai bay nhieu
                if (colPos < 0)
                {
                    break;
                }
                if (board[i, colPos] == currentTurn) // check
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            for (int i = row + 1; i < BoardSize; i++) // xuong 1 dong
            {
                int colPos = col + (i - row); // xuong may dong thi sang phai bay nhieu
                if (colPos >= BoardSize)
                {
                    break;
                }
                if (board[i, colPos] == currentTurn) // check
                {
                    count++;
                }
                else
                {
                    break;
                }
            }


            return count >= 5;
        }

        private bool CheckDiagonalLeft(int row, int col)
        {
            int count = 1;
            for (int i = row - 1; i > -1; i--) // len 1 dong
            {
                int colPos = col + (row - i); // len may dong thi sang phai bay nhieu
                if (colPos >= BoardSize)
                {
                    break;
                }
                if (board[i, colPos] == currentTurn) // check
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            for (int i = row + 1; i < BoardSize; i++) // xuong 1 dong
            {
                int colPos = col - (i - row); // xuong may dong thi sang trai bay nhieu
                if (colPos < 0)
                {
                    break;
                }
                if (board[i, colPos] == currentTurn) // check
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count >= 5;
        }

        #endregion
    }
}