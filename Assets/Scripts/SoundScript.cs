using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
 
    [SerializeField] private AudioSource _fightSound;
    [SerializeField] private AudioSource _eatSound;
    [SerializeField] private AudioSource _createPeasantSound;
    [SerializeField] private AudioSource _createWarriorSound;
    [SerializeField] private AudioSource _harwestSound;
    [SerializeField] private AudioSource _background;

    [SerializeField] private AudioClip _backgroundSound;
    [SerializeField] private AudioClip _finalWinSound;
    [SerializeField] private AudioClip _finalLoseSound;


    public void FightSound()
    {
        if (_fightSound.isPlaying)
            _fightSound.Pause();
        else
            _fightSound.Play();
    }
    public void EatSound()
    {
        if (_eatSound.isPlaying)
            _eatSound.Pause();
        else
            _eatSound.Play();
    }
    public void CreatePearsantSound()
    {
        if (_createPeasantSound.isPlaying)
            _createPeasantSound.Pause();
        else
            _createPeasantSound.Play();
    }
    public void CreateWarriorSound()
    {
        if (_createWarriorSound.isPlaying)
            _createWarriorSound.Pause();
        else
            _createWarriorSound.Play();
    }
    public void HarwestSound()
    {
        if (_harwestSound.isPlaying)
            _harwestSound.Pause();
        else
            _harwestSound.Play();
    }

    public void WinFinalSound()
    {
        _background.GetComponent<AudioSource>().clip = _finalWinSound;
        if (_background.isPlaying)
            _background.Pause();
        else
            _background.Play();
    }
    public void LoseFinalSound()
    {
        _background.GetComponent<AudioSource>().clip = _finalLoseSound;
        if (_background.isPlaying)
            _background.Pause();
        else
            _background.Play();
    }

    public void Background()
    {
        _background.GetComponent<AudioSource>().clip = _backgroundSound;
        if (_background.isPlaying)
            _background.Pause();
        else
            _background.Play();
    }
}
