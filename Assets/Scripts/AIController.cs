using System.Collections;
using System.Collections.Generic;
using CaroGame;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private CaroGameManager caroGameManager;
    
    
    public void AiTurn()
    {
        int row = 0;
        int col = 0;
//        GetBestMove(out row, out col);
        caroGameManager.Check(row,col);
    }

//    private void GetBestMove(out int row, out int col)
//    {
//        for (int i = 0; i < caroGameManager.BoardSize; i++)
//        {
//            for (int j = 0; j < caroGameManager.BoardSize; j++)
//            {
//                if (caroGameManager.Board[i,j] == "")
//                {
//                    
//                }
//            }
//        }
//    }
}