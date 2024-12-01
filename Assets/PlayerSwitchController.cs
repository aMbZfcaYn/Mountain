using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchController : MonoBehaviour
{
    [SerializeField] private List<GameObject> players;
    private int currentPlayerIndex = 0;


    void Start()
    {
        if( players.Count > 0 )
            EnableControl( players[currentPlayerIndex] );
        else
            Debug.Log( "No players in the scene." );
    }

    
    void Update()
    {
        if( Input.GetKeyDown( KeyCode.Tab ) )
            SwitchPlayer();
    }

    void SwitchPlayer()
    {
        DisableControl( players[currentPlayerIndex] );

        currentPlayerIndex = ( currentPlayerIndex + 1 ) % players.Count;
    
        EnableControl( players[currentPlayerIndex] );
    }



    void EnableControl( GameObject player )
    {
        player.GetComponent<PlayerController>().enabled = true;
    }


    void DisableControl( GameObject player )
    {
        player.GetComponent <PlayerController>().enabled = false;
    }


}
