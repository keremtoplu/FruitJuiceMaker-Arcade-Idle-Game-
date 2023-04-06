using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SfxController : Singleton<SfxController>
{
    [SerializeField]
    private AudioSource openGateSound;

    [SerializeField]
    private AudioSource closeGateSound;

    [SerializeField]
    private AudioSource cycleWalkSound;

    [SerializeField]
    private AudioSource cashOutSound;

    [SerializeField]
    private AudioSource collectSound;

    [SerializeField]
    private Button soundsButton;

    private bool allSoundsClose = false;

    public bool AllSoundsClose => allSoundsClose;

    public void GetOpenGateSound() => openGateSound.Play();
    public void GetCloseGateSound() => closeGateSound.Play();
    public void GetCycleWalkSound() => cycleWalkSound.Play();
    public void GetCashOutSound() => cashOutSound.Play();
    public void GetCollectSound() => collectSound.Play();

    public void AllSoundsPause()
    {
        if (!allSoundsClose)
        {
            allSoundsClose = true;
            soundsButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Off";
        }
        else
        {
            allSoundsClose = false;
            soundsButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "On";
        }

    }
}
