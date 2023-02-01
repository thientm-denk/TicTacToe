using System;
using System.Collections;
using System.Collections.Generic;
using CaroGame;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
     public int Row { get; set; }
     public int Col { get; set; }
    

    [SerializeField] private Image  image;
    [SerializeField] private Button button;
    
    private CaroGameManager         caroGameManager;
    private bool                    isChecked = false;
    
    void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    public void OnInit(CaroGameManager caroGameManager, int row, int col)
    {
        this.caroGameManager = caroGameManager;
        Row = row;
        Col = col;
    }

    /// <summary>
    /// Change image to x or o when click
    /// </summary>
    public void ChangeImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    private void OnClick()
    {
        if (isChecked)
        {
            return;
        }
        caroGameManager.Check(this);
        isChecked = true;
    }
}
