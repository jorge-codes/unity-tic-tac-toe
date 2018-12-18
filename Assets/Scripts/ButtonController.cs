using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour
{
    public Image image;
    public Sprite[] mark;
    private bool _isClicked;

    public void Click(int playerId)
    {
        if (_isClicked)
        {
            return;
        }
        image.sprite = mark[playerId - 1];
        image.color = Color.white;
        _isClicked = true;
    }
    
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    
    
}
