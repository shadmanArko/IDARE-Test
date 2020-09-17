using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class API : MonoBehaviour
{
    #region Fields

    private readonly string baseURL = "https://us-central1-marine-set-274003.cloudfunctions.net/GetTotalBoxes";

    private string key = "name";

    private string value = "IDARE";
    
    private float boxSpawnPositionXAxisOnly = 22.5f;
    
    public static bool moveToDrag;

    [Tooltip("This will show the box number which it is getting from the API")]
    public int checkingBoxNumber;

    [Tooltip("This model will be placed on the scene")]
    public GameObject box;
    
    [Tooltip("This will make the connection between 2 objects")]
    public GameObject connector;

    #endregion
    
    #region Enumerators
    
    IEnumerator GetBoxNumber()
    {
        string boxNumberURL = baseURL + "?" + key + "=" + value;

        UnityWebRequest boxNumberRequest = UnityWebRequest.Get(boxNumberURL);

        yield return boxNumberRequest.SendWebRequest();

        JSONNode boxNumberInfo = JSON.Parse(boxNumberRequest.downloadHandler.text);

        string boxNumber = boxNumberInfo["boxes"];

        int boxNumberInt = Int32.Parse(boxNumber);

        checkingBoxNumber = boxNumberInt;

        for (int i = 0; i < boxNumberInt; i++)
        {
            SpawnObjects();
            boxSpawnPositionXAxisOnly = boxSpawnPositionXAxisOnly - 5f;
        }
    }
    
    #endregion

    #region Methods

    /// <summary>
    /// spawn boxes by taking number from API
    /// </summary>
    public void OnButtonBoxNumber()
    {
        boxSpawnPositionXAxisOnly = 22.5f;
        
        StartCoroutine(GetBoxNumber());

        moveToDrag = true;
        
    }
    
    /// <summary>
    /// This method define the place where will the boxes will Instantiate 
    /// </summary>
    void SpawnObjects()
    {
        Vector3 position = new Vector3(boxSpawnPositionXAxisOnly, 0, -20);
        GameObject obj = Instantiate(box,position,Quaternion.identity);
    }

    /// <summary>
    /// This method will reset the whole game
    /// </summary>
    public void ResetGame()
    {
        SceneManager.LoadScene("Scenes/API");
        
    }

    /// <summary>
    /// This will create and handle the connection between two objects
    /// </summary>
    public void CreateConnector()
    {
        moveToDrag = false;
        Instantiate(connector);
    }

    #endregion
}
