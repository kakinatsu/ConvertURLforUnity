using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;
using System;
using System.Text.RegularExpressions;

namespace kakinatsu.niconicoURL
{
    public class niconicoURLConvertor : UdonSharpBehaviour
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

            if(beforeConvertedURL.StartsWith("https://www.nicovideo.jp/watch/sm"))
            {
                string target = "[0-9]+";
                Match IDs = Regex.Match(beforeConvertedURL, target);

                afterConvertedURL = "https://www.nicovideo.life/watch?v=sm" + IDs;
                debugText = "↑これが変換後のURLです。\n これは「茅野ななみのVRChat向け支援ツール」を利用しています";
            }
            else
            {
                afterConvertedURL = "";
                debugText = "不正なURLが入力されました。\nhttps://www.nicovideo.jp/watch/sm0000000の形式を確認";
            }
        }
    }
}