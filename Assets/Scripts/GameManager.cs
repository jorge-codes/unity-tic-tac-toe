using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("References")]
    
    public Text txtPointsWin;
    public Text txtPointsTie;
    public Text txtPointsLose;
    public Button[] btnBoard;
    
    [Header("Member variables")]
    public int pointsWin;
    public int pointsTie;
    public int pointsLose;

    private int[] _board;

    
    public void Play(int playerId, int boardPositon)
    {
        _board[boardPositon] = playerId;
    }

    private void Start()
    {
        pointsWin = pointsTie = pointsLose = 0;
        UpdateScore();

        _board = new int[9];
        
    }

    private void UpdateScore()
    {
        txtPointsLose.text = pointsLose.ToString();
        txtPointsTie.text = pointsTie.ToString();
        txtPointsWin.text = pointsWin.ToString();
    }
    

    
}
