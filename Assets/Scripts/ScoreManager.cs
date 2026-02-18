using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;

    public int firstPlaceScore;
    public int secondPlaceScore;
    public int thirdPlaceScore;
    public string firstPlaceName;
    public string secondPlaceName;
    public string thirdPlaceName;
        
    public string currentName;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScores();
    }

    
    //NEED A CLASS TO SAVE THE TOP SCORES AND NAMES
    [System.Serializable]
    class SaveData
    {
        public int firstPlaceScore;
        public int secondPlaceScore;
        public int thirdPlaceScore;
        public string firstPlaceName;
        public string secondPlaceName;
        public string thirdPlaceName;
    }

    //METHOD TO LOAD PREVIOUS HIGH SCORES
    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            firstPlaceScore = data.firstPlaceScore;
            secondPlaceScore = data.secondPlaceScore;
            thirdPlaceScore = data.thirdPlaceScore;
            firstPlaceName = data.firstPlaceName;
            secondPlaceName = data.secondPlaceName;
            thirdPlaceName = data.thirdPlaceName;
        }
    }

    //METHOD TO COMPARE CURRENT SCORE TO HIGH SCORES AND UPDATE SAVE FILE ACCORDINGLY
    public void UpdateHighScores(int currentScore)
    {
        //Load existing JSON save information
        string path = Application.persistentDataPath + "/savefile.json";
        SaveData data;

        //Read existing high scores
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            data = new SaveData();
        }        
        
        //Change first and move old scores down
        if (currentScore > data.firstPlaceScore)
        {
            data.thirdPlaceScore = data.secondPlaceScore;
            data.thirdPlaceName = data.secondPlaceName;
            data.secondPlaceScore = data.firstPlaceScore;
            data.secondPlaceName = data.firstPlaceName;            

            data.firstPlaceScore = currentScore;
            data.firstPlaceName = currentName;
        }
        //Change second and move old score down
        else if (currentScore < data.firstPlaceScore && currentScore > data.secondPlaceScore)
        {
            data.thirdPlaceScore = data.secondPlaceScore;

            data.secondPlaceScore = currentScore;
            data.secondPlaceName = currentName;
        }
        //Change third place
        else if (currentScore < data.secondPlaceScore && currentScore > data.thirdPlaceScore)
        {
            data.thirdPlaceScore = currentScore;
            data.thirdPlaceName = currentName;
        }

        string updatedJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, updatedJson);
    }

    

}
