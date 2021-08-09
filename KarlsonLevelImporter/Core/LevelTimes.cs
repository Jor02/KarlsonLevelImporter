using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace KarlsonLevelImporter.Core
{
    class LevelTimes
    {
        private Dictionary<string,float> levelTimes;

        public string this[string path]
        {
            get
            {
                string time = "NAN";
                if (levelTimes.ContainsKey(path))
                    time = Timer.Instance.GetFormattedTime(levelTimes[path]);
                return time;
            }
        }

        public void Set(string path, float value)
        {
            if (levelTimes.ContainsKey(path))
                levelTimes[path] = value;
            else levelTimes.Add(path, value);
        }

        public float Get(string path)
        {
            if (levelTimes.ContainsKey(path))
                return levelTimes[path];
            else return 0f;
        }

        public void Save()
        {
            string JSON = JsonConvert.SerializeObject(levelTimes);
            byte[] encodedJSON = Encoding.UTF8.GetBytes(JSON);
            byte[] compressedJSON = SevenZip.Compression.LZMA.SevenZipHelper.Compress(encodedJSON);
            File.WriteAllBytes(LevelLoader.targetPath + "times.dat", compressedJSON);
        }

        public LevelTimes()
        {
            if (File.Exists(LevelLoader.targetPath + "times.dat"))
            {
                byte[] timeList = File.ReadAllBytes(LevelLoader.targetPath + "times.dat");
                byte[] decompressedList = SevenZip.Compression.LZMA.SevenZipHelper.Decompress(timeList);
                string JSONList = Encoding.UTF8.GetString(decompressedList);
                levelTimes = JsonConvert.DeserializeObject<Dictionary<string, float>>(JSONList);
            }
            else levelTimes = new Dictionary<string, float>();
        }
    }
}
