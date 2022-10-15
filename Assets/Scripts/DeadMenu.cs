using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : Menu
{
    private bool isDead = false;

    private Character character;

    protected override void Awake()
    {
        character = FindObjectOfType<Character>();
    }
    protected override void Update()
    {
        if(character && (character.Lives <= 0 || character.transform.position.y <= -30) && isDead == false)
        {
            Pause();
            isDead = true;
        }
        if (Input.GetKeyDown(KeyCode.Return) && isDead)
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isDead)
        {
            LoadMenu();
        }
    }
}
