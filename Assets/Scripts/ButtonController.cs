using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour
{
    public Button button;

    public void Click(int playerId)
    {
        
    }
    
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    
    
}
