using System;
using System.Collections;
using UnityEngine;

public class Perks : MonoBehaviour
{
    private void OnEnable() => GameStateManager.OnStateChange += OnGameStateChanged;
    private void OnDisable() => GameStateManager.OnStateChange -= OnGameStateChanged;

    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Game)
        {
            StartCoroutine(Invincible());
        }
    }

    public IEnumerator Invincible()
    {
        yield return null; 
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<Collider>().enabled = false;
        }
        yield return new WaitForSeconds(10f);
        foreach (GameObject player in players)
        {
            player.GetComponent<Collider>().enabled = false;
        }
    }
}
