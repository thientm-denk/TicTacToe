using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CaroGame
{
    public class CaroGameManager : MonoBehaviour
    {
        [Header("Config Game")] 
        [SerializeField] private CaroConfig config;

        [Space(1f)] [Header("Generate Board")] 
        [SerializeField] private GameObject      cellPrefabs;
        [SerializeField] private Transform       boardTransform;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
                         private int             boardSize;

         [Space(1f)] [Header("Game Handler")] 
         [SerializeField] private string      currentTurn = "x";
         
         public string CurrentTurn
         {
             get { return CurrentTurn;}
         }

         [SerializeField] private string[,] board;

         // Start is called before the first frame update
        void Start()
        {
            OnInit();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnInit()
        {
            boardSize = config.boardSize;
            board = new string[boardSize, boardSize];
            GenerateBoard();
        }
        
        private void GenerateBoard()
        {
            
            gridLayoutGroup.constraintCount = boardSize;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    var obj = Instantiate(cellPrefabs, boardTransform);
                    obj.GetComponent<Cell>().OnInit(this, i,j);
                    board[i, j] = "";
                }
            }
            
        }

        public Sprite Check(int row, int col)
        {
            if (CheckWin(row, col))
            {
                Debug.Log("EndGame");
            }
            board[row, col] = currentTurn;
            
            currentTurn = currentTurn == "x" ? "o" : "x";
            return currentTurn != "x" ? config.xSprite : config.oSprite;
        }
        private bool CheckWin(int row, int col)
        {
             
            if (CheckRow(row,col))
            {
                Debug.Log("Win Row");
                return true;
            }

            if (CheckCol(row,col))
            {
                Debug.Log("Win Col");
                return true;
            }
            if (CheckDiagonalRight(row,col))
            {
                Debug.Log("Win Diagonal right");
                return true;
            }
            
            if (CheckDiagonalLeft(row,col))
            {
                Debug.Log("Win Diagonal left");
                return true;
            }
           

            return false;
        }
        private bool CheckRow(int row, int col)
        {
            int  count = 1;
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
            for (int i = col + 1; i < boardSize; i++)
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
            for (int i = row + 1; i < boardSize; i++)
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
                if (board[i, colPos] == currentTurn) // check
                {
                    count++;
                }
                else
                {
                    break;
                }
            
            }
            
            for (int i = row + 1; i < boardSize; i++) // xuong 1 dong
            {
                int colPos = col + (i - row); // xuong may dong thi sang phai bay nhieu
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
                int colPos = col + (row - i);  // len may dong thi sang phai bay nhieu
                if (board[i, colPos] == currentTurn) // check
                {
                    count++;
                }
                else
                {
                    break;
                }
            
            }
            
            for (int i = row + 1; i < boardSize; i++) // xuong 1 dong
            {
                int colPos = col - (i - row); // xuong may dong thi sang trai bay nhieu
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
    }
}