using System;
using System.Collections;
using System.Collections.Generic;
using CaroGame;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private int    row;
    [SerializeField] private int    col;
    
    [SerializeField] private Sprite xSprite;
    [SerializeField] private Sprite oSprite;

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
        this.row = row;
        this.col = col;
    }

    /// <summary>
    /// Change image to x or o when click
    /// </summary>
    /// <param name="s">string s must be "x" or "o"</param>
    private void ChangeImage(string s)
    {
        image.sprite = s == "x" ? xSprite : oSprite;
    }

    private void OnClick()
    {
        if (isChecked)
        {
            return;
        }
        
        ChangeImage(caroGameManager.CurrentTurn);
        isChecked = true;
    }
}
