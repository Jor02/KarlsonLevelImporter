using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace KarlsonLevelImporter.Core
{
    /// <summary>
    /// Used for adding modded components to all GameObjects
    /// </summary>
    [Serializable]
    public class LevelScripts
    {
        public List<ScriptedObject> objects = new List<ScriptedObject>();

        [Serializable]
        public class ScriptedObject
        {
            public string path;
            public List<ObjectComponent> components = new List<ObjectComponent>();
        }

        [Serializable]
        public class ObjectComponent
        {
            public string typeName;
            public List<Member> members = new List<Member>();
        }

        [Serializable]
        public struct Member
        {
            public string name;
            public string value;
        }
    }
}
