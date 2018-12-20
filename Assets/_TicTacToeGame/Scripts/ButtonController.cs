using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour
{
    public Image image;
    public AudioSource audioSource;
    public Sprite uiSprite;
    public Sprite[] mark;
    public AudioClip[] clip;
    private bool _isClicked;


    public void Init()
    {
//        image.sprite
    }
    
    public void Click(int playerId)
    {
        if (_isClicked)
        {
            return;
        }
        image.sprite = mark[playerId - 1];
        image.color = Color.white;
        audioSource.clip = clip[playerId - 1];
        audioSource.Play();
        _isClicked = true;
    }

    public void Blink()
    {
        StartCoroutine(StartBlinkin());
    }

    private IEnumerator StartBlinkin(float interval = 0.2f)
    {
        WaitForSeconds wait = new WaitForSeconds(interval);
        for (;;)
        {
            yield return wait;
            image.enabled = false;
            yield return wait;
            image.enabled = true;
        }    
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
    }
    
    
}
