using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;
using System;
using System.Text.RegularExpressions;

namespace kakinatsu.largeImageURL
{
    public class largeImageURLConvertor : UdonSharpBehaviour
    {
        [SerializeField] private InputField _inputField;
        [SerializeField] private Text _inputFieldPlaceholder;
        [SerializeField] private Text _debugTextField;
        [SerializeField] private InputField _outputField;

        string beforeConvertedURL;
        string afterConvertedURL;
        string debugText;

        void Start()
        {
            _inputField = this.gameObject.GetComponent<InputField>();
            _inputFieldPlaceholder = this.gameObject.GetComponent<Text>();
        }

        void Update()
        {
            _debugTextField.text = debugText;
            _outputField.GetComponent<InputField>().text = afterConvertedURL;
        }

        public void SetContent()    //InputFieldオブジェクトのOnSubmitイベントで呼び出される.
        {
            beforeConvertedURL = _inputField.text;

            //入力フィールドに入力したURLが表示されたままになるようにする.
            _inputField.text = "";
            _inputFieldPlaceholder.text = beforeConvertedURL;

            //https://images.weserv.nl/ を利用するためのURLの変換をする.
            beforeConvertedURL = beforeConvertedURL.Replace("https://", "");
            beforeConvertedURL = beforeConvertedURL.Replace("?", "%3F");
            beforeConvertedURL = beforeConvertedURL.Replace("&", "%26");
            
            afterConvertedURL = "https://wsrv.nl/?url=" + beforeConvertedURL + "&w=2048&h=2048";
            debugText = "↑これが変換後のURLです。\n これは images.weserv.nl/ を利用しています";
        }
    }
}