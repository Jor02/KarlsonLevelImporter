using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace KarlsonLevelImporter.Core.Components
{
    class CanPickup : ComponentBase
    {
        protected override void StartComp()
        {
            gameObject.tag = "Gun";
        }
    }
}
