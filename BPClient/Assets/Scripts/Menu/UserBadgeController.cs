using UnityEngine;
using System.Collections;

public class UserBadgeController : MonoBehaviour {
    public void SignIn()
    {
        // Log into Facebook
        UserManager.Instance.Login();
    }
}
