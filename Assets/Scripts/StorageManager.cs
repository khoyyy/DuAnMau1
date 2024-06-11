using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class StorageManager
    {
        public static bool SaveToFile(string filename, string json)
        {
            try
            {
                var fileStream = new FileStream(filename, FileMode.Create);
                using(var writer = new StreamWriter(fileStream))
                {
                    writer.Write(json);
                }
                return true;
            }
            catch (System.Exception e)
            {
                Debug.Log("Error saving file: " + e.Message);
                return false;
            }
        }

        public static string LoadFromFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    var fileStream = new FileStream(filename, FileMode.Open);
                    using (var reader = new StreamReader(fileStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                else
                {
                    Debug.Log("File not found: "+ filename);
                    return null;
                }
            }
            catch(System.Exception e)
            {
                Debug.Log("Error loading file:" + e.Message);
                return null;
            }
        }
    }
}
