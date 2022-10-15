using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    private Transform coins;

    private Character character;

    private void Awake()
    {
        character = FindObjectOfType<Character>();

        coins = transform.GetChild(2);
    }

    public void Refresh()
    {
        coins.GetComponent<UnityEngine.UI.Text>().text = character.Score.ToString();
    }
}
