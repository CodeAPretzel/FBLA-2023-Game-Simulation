using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;

    private List<Transform> highscoreEntryTransformList;
    
    private void Awake() {
        
        entryContainer = entryContainer.GetComponent<Transform>();
        entryTemplate = entryTemplate.GetComponent<Transform>();
        
        string jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        
        //Sort entry list by score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++){
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++){
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score){
                    //Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        if (highscores.highscoreEntryList.Count > 5){
            for (int h = highscores.highscoreEntryList.Count; h>5; h--){
                highscores.highscoreEntryList.RemoveAt(5);
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList){
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList){

        float templateHeight = 60f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryTransform.name = entryTransform.name.Replace("(Clone)","").Trim();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank){
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        //Rank
        entryTransform.Find("rankField").GetComponent<TextMeshProUGUI>().text = rankString;

        //Name
        string name = highscoreEntry.name;
        entryTransform.Find("nameField").GetComponent<TextMeshProUGUI>().text = name;
        
        //Score
        int score = highscoreEntry.score;
        entryTransform.Find("pointField").GetComponent<TextMeshProUGUI>().text = score.ToString();

        //Highlight first rank
        if (rank == 1){
            entryTransform.Find("nameField").GetComponent<TextMeshProUGUI>().color = Color.red;
            entryTransform.Find("rankField").GetComponent<TextMeshProUGUI>().color = Color.red;
            entryTransform.Find("pointField").GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        transformList.Add(entryTransform);
    }

    //Adding new entries for the json list
    public void AddHighscoreEntry(int score, string name){
        //Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name};

        //Load saved Highscores
        string jsonString = PlayerPrefs.GetString("HighscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        //Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    //Represents a single high score entry
    [System.Serializable]
    private class HighscoreEntry {
        public int score;
        public string name;
    }
}
