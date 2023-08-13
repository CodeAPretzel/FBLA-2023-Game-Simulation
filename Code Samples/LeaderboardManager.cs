//Below is the guidance for retrieving Lootlocker's API keys and login the player into their system.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI rankField;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI scoreField;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupRoutine());
    }
    
    IEnumerator SetupRoutine(){
        //Login to Lootlocker
        yield return LoginRoutine();

        //Set Highscores to Leaderboard UI
        yield return FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine(){
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) => {
            if (response.success){
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            } else {
                Debug.Log("Failed login: " + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator FetchTopHighscoresRoutine(){
        bool done = false;
        LootLockerSDKManager.GetScoreList("globalHighscore", 5, 0, (response) => {
            if (response.success){
                string tempPlayerNames = "";
                string tempPlayerScores = "";
                string tempPlayerRank = "";

                LootLockerLeaderboardMember[] members = response.items;

                //Setup for Leaderboard
                for (int i = 0; i < members.Length; i++){
                    tempPlayerRank += members[i].rank + ".\n\n";

                    if (members[i].player.name != ""){
                        tempPlayerNames += members[i].player.name;
                    } else {
                        tempPlayerNames += members[i].player.id;
                    }

                    tempPlayerScores += members[i].score + "\n\n";
                    tempPlayerNames += "\n\n";
                }
                done = true;

                rankField.text = tempPlayerRank;
                nameField.text = tempPlayerNames;
                scoreField.text = tempPlayerScores;
            } else {
                Debug.Log("Failed: " + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

}
