using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    private static GameObject player1, player2, turnIndicator;
    private static GameObject[] player1Waypoints, player2Waypoints;
    public static int player1StartWaypoint = 0;
    public static int player2StartWaypoint = 0;
    public static int ThisPlayerShouldWalkNow = 0;
    public static int ItWasThisPlayersTurn = 1;


    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Ty");
        player2 = GameObject.Find("Granny");

        SetPlayersWaypoints();
        PutPlayersOnBoard();
        UpdateInformingText();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player1.GetComponent<FollowThePath>().moveAllowed = true;
            player1.GetComponent<FollowThePath>().anim.SetBool("isWalking", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player2.GetComponent<FollowThePath>().moveAllowed = true;
            player2.GetComponent<FollowThePath>().anim.SetBool("isWalking", true);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // save current positions
            PlayerPrefs.SetInt("player1WaypointIndex", player1.GetComponent<FollowThePath>().waypointIndex);
            PlayerPrefs.SetInt("player2WaypointIndex", player2.GetComponent<FollowThePath>().waypointIndex);

            SceneManager.LoadScene(1); // Scene(1) is TrunkMovementExplanation 
        }

        if (player1.GetComponent<FollowThePath>().waypointIndex >
            player1StartWaypoint)
        {
            player1.GetComponent<FollowThePath>().moveAllowed = false;
            player1.GetComponent<FollowThePath>().anim.SetBool("isWalking", false);
            player1StartWaypoint = player1.GetComponent<FollowThePath>().waypointIndex;
        }

        if (player2.GetComponent<FollowThePath>().waypointIndex >
            player2StartWaypoint)
        {
            player2.GetComponent<FollowThePath>().moveAllowed = false;
            player2.GetComponent<FollowThePath>().anim.SetBool("isWalking", false);
            player2StartWaypoint = player2.GetComponent<FollowThePath>().waypointIndex;
        }

        if (ThisPlayerShouldWalkNow != 0)
        {
            switch (ThisPlayerShouldWalkNow)
            {
                case 1:
                    Player1Walk();
                    ThisPlayerShouldWalkNow = 0;
                    ItWasThisPlayersTurn = 2;
                    UpdateInformingText();
                    break;
                case 2:
                    Player2Walk();
                    ThisPlayerShouldWalkNow = 0;
                    ItWasThisPlayersTurn = 1;
                    UpdateInformingText();
                    break;
                default:
                    break;
            }
        }

    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Player1Walk()
    {
        player1.GetComponent<FollowThePath>().moveAllowed = true;
        player1.GetComponent<FollowThePath>().anim.SetBool("isWalking", true);
    }

    private void Player2Walk()
    {
        player2.GetComponent<FollowThePath>().moveAllowed = true;
        player2.GetComponent<FollowThePath>().anim.SetBool("isWalking", true);
    }

    private void SetPlayersWaypoints()
    {
        player1Waypoints = GameObject.FindGameObjectsWithTag("Player1");
        player2Waypoints = GameObject.FindGameObjectsWithTag("Player2");

        for (int i = 0; i < player1Waypoints.Length; i++)
        {
            player1.GetComponent<FollowThePath>().waypoints[i] = player1Waypoints[i].transform;

        }

        for (int i = 0; i < player2Waypoints.Length; i++)
        {
            player2.GetComponent<FollowThePath>().waypoints[i] = player2Waypoints[i].transform;

        }
    }

    private void PutPlayersOnBoard()
    {
        player1.GetComponent<FollowThePath>().waypointIndex = PlayerPrefs.GetInt("player1WaypointIndex");
        player1.GetComponent<FollowThePath>().PutBackPlayer();
        player2.GetComponent<FollowThePath>().waypointIndex = PlayerPrefs.GetInt("player2WaypointIndex");
        player2.GetComponent<FollowThePath>().PutBackPlayer();
    }
    private void UpdateInformingText()
    {
        turnIndicator = GameObject.Find("TurnIndicator");
        switch (ItWasThisPlayersTurn)
        {
            case 1:
                turnIndicator.GetComponent<TextMesh>().text = "Player 1, perform shown movement to start the next game.";
                break;
            case 2:
                turnIndicator.GetComponent<TextMesh>().text = "Player 2, perform shown movement to start the next game.";
                break;
            default:
                break;
        }
    }
}
