using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace KarlsonLevelImporter.Core.Menu
{
    class LevelButtons : MonoBehaviour
    {
        private GameObject template;
        private Transform menu;
        
        public void Init(Transform Template, RectTransform alignment)
        {
            Template.name = "Template";

            template = Template.gameObject;
            template.SetActive(false);

            template.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().enableAutoSizing = true;

            menu = alignment;
        }

        public void AddButton(string name, string time, Texture2D thumb, string path, long HeaderSize)
        {
            GameObject tmpButton = Instantiate(template);

            tmpButton.GetComponent<Image>().sprite = Sprite.Create(thumb, new Rect(0.0f, 0.0f, thumb.width, thumb.height), new Vector2(0.5f, 0.5f));

            tmpButton.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = name;
            tmpButton.transform.Find("Text (TMP) (1)").GetComponent<TextMeshProUGUI>().text = time;
            
            Button btn = tmpButton.GetComponent<Button>();
            btn.onClick = new Button.ButtonClickedEvent();
            btn.onClick.AddListener(() => StartCoroutine(LevelLoader.Instance.BeginLoadLevel(path, (ulong)HeaderSize)));

            tmpButton.SetActive(true);
            tmpButton.transform.SetParent(menu, false);
        }
    }
}
