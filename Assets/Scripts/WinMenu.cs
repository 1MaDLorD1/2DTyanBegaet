using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : Menu
{
    private bool isWin = false;

    private Character character;

    protected override void Awake()
    {
        character = FindObjectOfType<Character>();
    }

    protected override void Update()
    {
        if(character && character.Score >= 1 && isWin == false)
        {
            Pause();
            isWin = true;
        }
        if (Input.GetKeyDown(KeyCode.Return) && isWin)
        {
            NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isWin)
        {
            LoadMenu();
        }
    }
}
