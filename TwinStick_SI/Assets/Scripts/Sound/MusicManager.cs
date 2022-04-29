using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music1;
    public AudioSource music2;
    public AudioSource music3;
    public AudioSource music4;

    public AudioSource noSound;

    public string lastMusic;

    void Update()
    {
        if (TestMusic() == false)
        {
            playMusic();
        }
    }

    public void playMusic()
    {
        int rand = 0;

        if (lastMusic == null || lastMusic != "Void")
        {
            noSound.Play();
            lastMusic = noSound.clip.name;
        }
        else if (lastMusic == "Void")
        {
            rand = Random.Range(1,9);
            switch (rand)
            {
                case 1:
                    music1.Play();
                    lastMusic = music1.clip.name;
                    break;
                case 2:
                    music2.Play();
                    lastMusic = music2.clip.name;
                    break;
                case 3:
                    music3.Play();
                    lastMusic = music3.clip.name;
                    break;
                case 4:
                    music4.Play();
                    lastMusic = music4.clip.name;
                    break;
                case 5:
                    noSound.Play();
                    lastMusic = noSound.clip.name;
                    break;
                case 6:
                    noSound.Play();
                    lastMusic = noSound.clip.name;
                    break;
                case 7:
                    noSound.Play();
                    lastMusic = noSound.clip.name;
                    break;
                case 8:
                    noSound.Play();
                    lastMusic = noSound.clip.name;
                    break;
                default:
                    break;
            }
        }



    }

    public bool TestMusic()
    {
        if (noSound.isPlaying || music1.isPlaying || music2.isPlaying || music3.isPlaying || music4.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
