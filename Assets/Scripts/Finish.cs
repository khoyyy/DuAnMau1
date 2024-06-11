using Assets.Scripts;
using DefaultNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject informationCanvas;
    [SerializeField] GameObject finishCanvas;

    private StorageHelper storageHelper;
    private GameDataPlayed played;

    [SerializeField] GameObject row; 

    private void Start()
    {
        storageHelper = new StorageHelper();
        storageHelper.LoadData();
        played = storageHelper.played;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            informationCanvas.SetActive(false);
            var score = FindAnyObjectByType<GameController>().GetScore();
            // lưu thành tích của người chơi 
            var gameData = new GameData()
            {
                score = score,
                timePlayed = DateTime.Now.ToString("yyyy-MM-dd")
            };
            played.plays.Add(gameData);
            storageHelper.SaveData();
            // tải dữ liệu trong file hiễn thị lên bảng thành tích 
            storageHelper.LoadData();
            played = storageHelper.played;
            // sắp xếp giảm dần theo điểm 
            // lấy top 5 
            played.plays.Sort((x,y)=> y.score.CompareTo(x.score));
            var plays = played.plays.GetRange(0, Math.Min(5, played.plays.Count));
            Debug.Log("Count: " +played.plays.Count);
            // hiển trị lên giao diện 
            
            for (int i = 0; i < played.plays.Count; i++)
            {
                var rowInstance = Instantiate(row, row.transform.parent);
                rowInstance.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text =(i+1).ToString();
                rowInstance.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = played.plays[i].score.ToString();
                rowInstance.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = played.plays[i].timePlayed;
                rowInstance.SetActive(true);
            }
            // hiển thị giao diện kết thúc 
            finishCanvas.SetActive(true);
        }
    }
}
