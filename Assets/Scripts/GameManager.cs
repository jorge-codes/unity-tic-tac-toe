using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

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
    private List<int> _availablePosition;


    public void Play(int playerId, int boardPositon)
    {
        _board[boardPositon] = playerId;
        _availablePosition.Remove(boardPositon);
        btnBoard[boardPositon].onClick.RemoveAllListeners();

        if (_availablePosition.Count == 0)
        {
            // TODO
            // end the game
        }
    }
    
    
    public void PlayPlayer(int boardPosition)
    {
        Play(1, boardPosition);
        if (_availablePosition.Count != 0)
        {
            Invoke("PlayComputer", 0.5f);            
        }
        
    }

    public void PlayComputer()
    {
        int randomIndex = Random.Range(0, _availablePosition.Count - 1);
        int randomPosition = _availablePosition[randomIndex];
        btnBoard[randomPosition].GetComponent<ButtonController>().Click(2);
        Play(2, randomPosition);
    }

    private void Start()
    {
        pointsWin = pointsTie = pointsLose = 0;
        UpdateScore();
        _board = new int[9];
        _availablePosition = new List<int>();
       
        int i;
        for (i = 0; i < btnBoard.Length; i++)
        {
            _availablePosition.Add(i);
            Button button = btnBoard[i];
            int position = i;
            ButtonController bc = button.GetComponent<ButtonController>();
            button.onClick.AddListener(() => PlayPlayer(position));
            button.onClick.AddListener(() => bc.Click(1));
        }

    }

    private void UpdateScore()
    {
        txtPointsLose.text = pointsLose.ToString();
        txtPointsTie.text = pointsTie.ToString();
        txtPointsWin.text = pointsWin.ToString();
    }

    
}
