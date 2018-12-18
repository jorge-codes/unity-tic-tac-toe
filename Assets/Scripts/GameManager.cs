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
    [Space(3)] public Sprite[] sprMark;
    
    [Header("Member variables")]
    public int pointsWin;
    public int pointsTie;
    public int pointsLose;

    private int[] _board;
   


    public void Play(int boardPosition)
    {
        Play(1, boardPosition);
    }
    
    public void Play(int playerId, int boardPositon)
    {
        print("PlayerID: " + playerId + " position:" + boardPositon);
        _board[boardPositon] = playerId;
    }

    private void Start()
    {
        pointsWin = pointsTie = pointsLose = 0;
        UpdateScore();
        _board = new int[9];
        
        // TODO
        // assignments with lambda expressions
        // or subscribing to events
    }

    private void UpdateScore()
    {
        txtPointsLose.text = pointsLose.ToString();
        txtPointsTie.text = pointsTie.ToString();
        txtPointsWin.text = pointsWin.ToString();
    }
    

    
}
