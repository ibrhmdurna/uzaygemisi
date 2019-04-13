using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] int Score;
    [SerializeField] Text ScoreText;
    [SerializeField] Text LifeText;
    [SerializeField] Slider LifeBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangedValue(float value)
    {
        LifeText.text = value.ToString();
        LifeBar.value = value;
    }

    public void AddScore(int point)
    {
        Score += point;
        ScoreText.text = Score.ToString();
    }

    public void ResetScore()
    {
        Score = 0;
        ScoreText.text = Score.ToString();
    }
}
