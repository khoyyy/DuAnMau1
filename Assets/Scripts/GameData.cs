using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultNamespace
{
    // lưu trữ thông tin game 
    [Serializable]
    public class GameData
    {
        public int score = 0;
        public string timePlayed;

    }

    [Serializable]
     public class GameDataPlayed
    {
        public List<GameData> plays;
    }
}
