using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject prefabButton;
    [Space(2)]
    public GameObject panelRestart;
    public Transform parentObject;
    public Text txtPointsWin;
    public Text txtPointsTie;
    public Text txtPointsLose;
    [HideInInspector]
    public Button[] btnBoard;
    public AudioSource audioSourceSfxEnd;
    public AudioClip[] sfxEnd;
    public Image imgBackground;
    public Sprite[] backgroundList;
    [Space(3)]
    public Sprite[] sprMark;
    
    [Header("Member variables")]
    public int pointsWin;
    public int pointsTie;
    public int pointsLose;
    private int[] _board;
    private List<int> _availablePositions;
    private int[][] _rowChecks;
    private int[] _rowWinner;
    

    public void Play(int playerId, int boardPositon)
    {
        _board[boardPositon] = playerId;
        _availablePositions.Remove(boardPositon);
        btnBoard[boardPositon].onClick.RemoveAllListeners();
        btnBoard[boardPositon].interactable = false;
        int winner = CheckWinner();

        if (winner != 0)
        {
            _availablePositions.Clear();
            SetScore(winner);
        }
        else if (_availablePositions.Count == 0)
        {
            // it's a tie!
            SetScore(0);
        }
    }  
    
    public void PlayPlayer(int boardPosition)
    {
        Play(1, boardPosition);
        if (_availablePositions.Count != 0)
        {
            Invoke("PlayComputer", 0.5f);
        }
        
    }

    public void PlayComputer()
    {
        int randomIndex = Random.Range(0, _availablePositions.Count - 1);
        int randomPosition = _availablePositions[randomIndex];
        btnBoard[randomPosition].GetComponent<ButtonController>().Click(2);
        Play(2, randomPosition);
    }

    public void InitGame()
    {
        CleanBoard();
        int randomIndex = Random.Range(0, backgroundList.Length - 1);
        imgBackground.sprite = backgroundList[randomIndex];
        _rowWinner = null;
        panelRestart.GetComponent<Image>().raycastTarget = false;
        for (int i = 0; i < btnBoard.Length; i++)
        {
            _board[i] = 0;
            GameObject obj = Instantiate(prefabButton, parentObject);
            Button button = obj.GetComponent<Button>();
            btnBoard[i] = button;
            _availablePositions.Add(i);
            int position = i;
            ButtonController bc = button.GetComponent<ButtonController>();
            button.onClick.AddListener(() => PlayPlayer(position));
            button.onClick.AddListener(() => bc.Click(1));
        }
    }

    public void FinishGame()
    {
        foreach (Button button in btnBoard)
        {
            button.onClick.RemoveAllListeners();
        }

        panelRestart.GetComponent<Image>().raycastTarget = true;
    }

    public void CleanBoard()
    {
        while (parentObject.childCount != 0)
        {
            Transform child = parentObject.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int CheckWinner()
    {
        int i;
        for (i = 0; i < _rowChecks.Length; i++)
        {
            int[] row = _rowChecks[i];
            
            if (_board[row[0]] != 0 && _board[row[0]] == _board[row[1]] && _board[row[1]] == _board[row[2]])
            {
                // winner, winner! chicken dinner!!
                _rowWinner = row;
                return _board[row[0]];
            }
        }

        return 0;
    }

    

    private void Start()
    {
        btnBoard = new Button[9];
        
        _rowChecks = new int[8][];
        _rowChecks[0] = new int[] {0, 1, 2}; // top
        _rowChecks[1] = new int[] {3, 4, 5}; // middle
        _rowChecks[2] = new int[] {6, 7, 8}; // bottom
        _rowChecks[3] = new int[] {0, 3, 6}; // left
        _rowChecks[4] = new int[] {1, 4, 7}; // center
        _rowChecks[5] = new int[] {2, 5, 8}; // right
        _rowChecks[6] = new int[] {0, 4, 8}; // top-down (left to right)
        _rowChecks[7] = new int[] {6, 4, 2}; // bottom-up (left to right)
        
        pointsWin = pointsTie = pointsLose = 0;
        UpdateScore();
        _board = new int[9];
        _availablePositions = new List<int>();
      
        
        InitGame();
    }

    private void SetScore(int winnerID)
    {
        Animator scoreAnimator;
        GameObject scoreUpdated;
        switch (winnerID)
        {
            
            case 0:
                pointsTie++;
                scoreUpdated = txtPointsTie.transform.parent.gameObject;
                break;
            case 1:
                pointsWin++;
                scoreUpdated = txtPointsWin.transform.parent.gameObject;
                break;
            case 2:
                pointsLose++;
                scoreUpdated = txtPointsLose.transform.parent.gameObject;
                break;
            default:
                scoreUpdated = txtPointsTie.transform.parent.gameObject;
                break;
        }

        if (_rowWinner != null)
        {
            for (int i = 0; i < _rowWinner.Length; i++)
            {
                int index = _rowWinner[i];
                btnBoard[index].GetComponent<ButtonController>().Blink();
            }
        }

        scoreAnimator = scoreUpdated.GetComponent<Animator>();
        scoreAnimator.SetTrigger("Score");
        audioSourceSfxEnd.clip = sfxEnd[winnerID];
        audioSourceSfxEnd.Play();
        UpdateScore();
        FinishGame();
    }

    private void UpdateScore()
    {
        txtPointsLose.text = pointsLose.ToString();
        txtPointsTie.text = pointsTie.ToString();
        txtPointsWin.text = pointsWin.ToString();
    }

    
}
