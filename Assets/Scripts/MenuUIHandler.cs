#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI firstPlace;
    public TextMeshProUGUI secondPlace;
    public TextMeshProUGUI thirdPlace;
    public TextMeshProUGUI playerName;

    private void Start()
    {        
        DisplayHighScores();
    }

    //NEED A METHOD TO PULL AND DISPLAY THE HIGH SCORES FROM SCOREMANAGER ON SCENE LOAD
    private void DisplayHighScores()        
    {
        ScoreManager.Instance.LoadHighScores();
        firstPlace.text = "1. " + ScoreManager.Instance.firstPlaceName + " (" + ScoreManager.Instance.firstPlaceScore + ")";
        secondPlace.text = "2. " + ScoreManager.Instance.secondPlaceName + " (" + ScoreManager.Instance.secondPlaceScore + ")";
        thirdPlace.text = "3. " + ScoreManager.Instance.thirdPlaceName + " (" + ScoreManager.Instance.thirdPlaceScore + ")";
    }

    //Method to start a new game with current display name
    public void StartNew()
    {
        ScoreManager.Instance.currentName = playerName.text;
        SceneManager.LoadScene(1);
    }

    //Method to quit the game with special function to check if in editor or not
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


}
