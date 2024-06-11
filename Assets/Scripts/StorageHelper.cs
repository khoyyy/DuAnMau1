using DefaultNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class StorageHelper
    {
        private readonly string filename = " game_data.txt";
        public GameDataPlayed played;
         public void LoadData()
        {
            played = new GameDataPlayed()
            {
                plays = new List<GameData>
                {

                }

            };
            // đọc chuổi từ file 
            string dataAsjson = StorageManager.LoadFromFile(filename);
            if (dataAsjson != null)
            {
                //  chuyển chuỗi json thành object
                played = JsonUtility.FromJson<GameDataPlayed>(dataAsjson);
            }
        }

        public void SaveData()
        {
            // chuyển object thành chuỗi json
            string dataAsJson = JsonUtility.ToJson(played);
            // lưu chuỗi json vào file 
            StorageManager.SaveToFile(filename, dataAsJson);
        }
    }
}
