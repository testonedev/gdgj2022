using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI heavenScoreText;
    [SerializeField] private TextMeshProUGUI heavenCurrentPointsText;
    [SerializeField] private TextMeshProUGUI hellScoreText;

    private void Start()
    {
        GameManager gameManager = GameManager.Instance;

        heavenScoreText.text = gameManager.HeavenManager.TotalCollectedPoints.ToString();
        heavenCurrentPointsText.text = gameManager.HeavenManager.SoulPoints.ToString();
        hellScoreText.text = gameManager.HellManager.TotalCollectedPoints.ToString();
        gameManager.HeavenManager.OnTotalPointsChanged += x => heavenScoreText.text = x.ToString();
        gameManager.HeavenManager.OnSoulPointsChanged += x => heavenCurrentPointsText.text = x.ToString();
        gameManager.HellManager.OnTotalPointsChanged += x => hellScoreText.text = x.ToString();
    }
}
