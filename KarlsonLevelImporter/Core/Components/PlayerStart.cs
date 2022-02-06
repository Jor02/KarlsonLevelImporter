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

            Transform player = GameObject.Find("/Player").transform;
            player.position = transform.position + (Vector3.up * 1.4953f);
            player.GetComponent<PlayerMovement>().playerCam.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            Destroy(gameObject);
        }
    }
}

