using System.Collections;
using System.Collections.Generic;
using Shadout.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject RootGameObject;

    [SerializeField]
    private ScriptableObject RootGameContainer;



    private void SetupAreas()
    {
        
    }
}
