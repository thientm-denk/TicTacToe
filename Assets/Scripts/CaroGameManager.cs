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
             get
             {
                 if (currentTurn == "x")
                 {
                     currentTurn = "o";
                     return "x";
                 }
                 
                 currentTurn = "x";
                 return "o";
             }
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
      
    }
}