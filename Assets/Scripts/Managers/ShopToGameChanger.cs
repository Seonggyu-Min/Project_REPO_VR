using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopToGameChanger : MonoBehaviour
{
    public void GoToGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Debug.Log("Go To Game");
    }
}
