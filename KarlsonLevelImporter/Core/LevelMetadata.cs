using System;
using UnityEngine;

namespace KarlsonLevelImporter.Core
{
    [Serializable]
    public struct LevelMetadata
    {
        public string LevelName { get; }
        private byte[] Thumbnail;
        private byte type;

        public LevelMetadata(string levelName, byte[] thumbnail, byte thumbnailFormat)
        {
            LevelName = levelName;
            Thumbnail = thumbnail;
            type = thumbnailFormat;
        }

        public Texture2D GetThumbnail()
        {
            Texture2D thumbnail = new Texture2D(960, 540, (TextureFormat)type, false);
            ImageConversion.LoadImage(thumbnail, Thumbnail);
            return thumbnail;
        }
    }
}
