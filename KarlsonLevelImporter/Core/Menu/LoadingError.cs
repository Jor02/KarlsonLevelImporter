using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace KarlsonLevelImporter.Core.Menu
{
    class LoadingError : MonoBehaviour
    {
        public static LoadingError Instance { get; private set; }
        private TextMeshProUGUI errorText;
        private Transform textTransform;
        private Transform okButton;
        private GameObject backButton;

        private GameObject buttonAlignment;

        private List<Error> errors = new List<Error>();

        public LoadingError()
        {
            Instance = this;
        }

        public void Init(Transform text, Transform menu)
        {
            backButton = text.parent.gameObject;
            buttonAlignment = menu.Find("Align").gameObject;

            //Error Text
            textTransform = Instantiate(text, menu);

            textTransform.localPosition = new Vector3(-150f, 31f);
            textTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 250);

            errorText = textTransform.GetComponent<TextMeshProUGUI>();
            errorText.color = new Color32(145, 19, 19, 255);

            //Error button
            okButton = Instantiate(text.parent, menu);
            okButton.localPosition = new Vector3(-150f, -95.7f);
            okButton.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ok";

            textTransform.gameObject.SetActive(false);
            okButton.gameObject.SetActive(false);

            Button OK = okButton.GetComponent<Button>();
            OK.onClick = new Button.ButtonClickedEvent();

            OK.onClick.AddListener(OKClick);
        }

        public void OKClick()
        {
            errors.RemoveAt(0);

            buttonAlignment.SetActive(true);
            backButton.SetActive(true);
            okButton.gameObject.SetActive(false);
            textTransform.gameObject.SetActive(false);

            ShowNext();
        }

        public void ShowNext()
        {
            if (errors.Count > 0) {
                ShowError(errors[0].message, errors[0].showOk);
            }
        }

        public void AddError(string message, bool showOk)
        {
            errors.Add(new Error(message, showOk));
            if (errors.Count == 1) ShowNext();
        }

        public void ShowError(string message, bool showOk)
        {
            buttonAlignment.SetActive(false);

            errorText.text = message;
            textTransform.gameObject.SetActive(true);
            okButton.gameObject.SetActive(showOk);
            backButton.SetActive(!showOk);
        }

        struct Error
        {
            public string message;
            public bool showOk;

            public Error(string Message, bool ShowOk)
            {
                message = Message;
                showOk = ShowOk;
            }
        }
    }
}
