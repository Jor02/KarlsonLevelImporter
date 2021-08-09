using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace KarlsonLevelImporter.Core.Components
{
    class PlayerStart : ComponentBase
    {
        protected override void StartComp()
        {
            UnityEngine.Debug.Log("Test");

            GameObject player = GameObject.Find("/Player");
            player.transform.position = transform.position + (Vector3.up * 1.4953f);
            player.transform.Find("Orientation").rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            Destroy(gameObject);
        }
    }
}
