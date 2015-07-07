using UnityEngine;
using System.Collections;

public class BackButtonController : MonoBehaviour
{
    public void Back()
    {
        Application.LoadLevel("Menu");
    }
}
