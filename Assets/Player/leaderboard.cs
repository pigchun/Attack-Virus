using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using playerNameSpace;
using TMPro;

public class leaderBoard : MonoBehaviour
{
    public TextMeshProUGUI[] topScores;
    private List<int> scores = new List<int>();

    void Start()
    {
        //PlayerPrefs.DeleteAll(); // use to delete scores that are stored in playerprefs
        loadScores(); // load scores int score list

        addSortScore(Player.scoreVal); // add to score list

        updateScores(); // update Scores in leaderboard
    }
    private void loadScores() // load playerprefs scores into scores list
    {
        scores.Clear();
       
        for (int i = 0; i < 3; i++) // load top 3 scores into scores list
        {
            int score = PlayerPrefs.GetInt($"HighScore{i}", 0);
            scores.Add(score);
        }
    }

    private void saveScores()
    {
        for (int i = 0; i < scores.Count; i++) // save scores into playerprefs
        {
            PlayerPrefs.SetInt($"HighScore{i}", scores[i]);
        }
        PlayerPrefs.Save();
    }

    private void addSortScore(int newScore) // add and sort new scores
    {
        scores.Add(newScore);

        scores.Sort((a, b) => b.CompareTo(a));

        if (scores.Count > 3) // remove scores past 3
        {
            scores.RemoveAt(scores.Count - 1);
        }
        saveScores();
    }
    private void updateScores() // update the TMP scores ingame
    {
        for(int i =0; i< topScores.Length;i++)
        {
            if (i < scores.Count) // set scores ingame to the scores list
            {
                topScores[i].text = $"{scores[i]}";
            }
            else
            {
                topScores[i].text = $"0";
            }
        }
    }
}
