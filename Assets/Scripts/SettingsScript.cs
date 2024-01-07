using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [Header("Sound Turn On/Off")]
    public Sprite toggleOn;
    public Sprite toggleOff;
    public Button audioButton;
    
    [Header("Music Select")]
    [SerializeField] private int selectedMusic = 0;
    private const int minValue = 0;
    private const int maxValue = 1;
    [SerializeField] private TextMeshProUGUI indexText;
    
    //[SerializeField] private GameObject audioManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        indexText.text = selectedMusic.ToString();
    }

    public void TurnOffMusic()
    {
        AudioManager.instance.StopMusic();
        Image buttonImg = audioButton.GetComponent<Image>();
        if (AudioManager.instance.isTurnedOn)
        {
            buttonImg.sprite = toggleOn;
            AudioManager.instance.PlaySoundtrack(selectedMusic);
        }
        else
        {
            buttonImg.sprite = toggleOff;
        }
    }

    public void NextSong()
    {
        selectedMusic++;
        if (selectedMusic > maxValue)
        {
            selectedMusic = minValue;
        }
        AudioManager.instance.StopSoundtrack();
        //AudioManager.instance.ost[selectedMusic - 1].Stop();
        AudioManager.instance.PlaySoundtrack(selectedMusic);
    }

    public void PreviousSong()
    {
        selectedMusic--;
        if (selectedMusic < minValue)
        {
            selectedMusic = maxValue;
        }
        AudioManager.instance.StopSoundtrack();
        AudioManager.instance.PlaySoundtrack(selectedMusic);
    }
}
    
