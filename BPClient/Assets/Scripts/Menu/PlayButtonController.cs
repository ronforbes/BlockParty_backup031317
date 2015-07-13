using UnityEngine;
using System.Collections;

public class PlayButtonController : MonoBehaviour
{
    public void Play()
    {
        string level = GameClock.Instance.State.ToString();
        Application.LoadLevel(level);
    }	
}
