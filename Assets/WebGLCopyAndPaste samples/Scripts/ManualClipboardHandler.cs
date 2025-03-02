using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WebGLCopyAndPaste.Samples
{
    /// <summary>
    /// Handles using the clipboard with the methods of <see cref="WebGLCopyAndPasteAPI"/>
    /// instead of automatically when the user presses a key combination.
    /// </summary>

    public class ManualClipboardHandler : MonoBehaviour
    {
        #region Parameters


        [SerializeField] TMP_InputField inputField = default;
        [SerializeField] Button copyButton = default;


        #endregion




        /// <summary>
        /// Implementation of "MonoBehaviour.Start".
        /// </summary>

        void Start()
        {
            copyButton.onClick.RemoveListener(OnCopyButtonClicked);
            copyButton.onClick.AddListener(OnCopyButtonClicked);
        }




        /// <summary>
        /// Implementation of "MonoBehaviour.OnDestroy".
        /// </summary>

        void OnDestroy()
        {
            copyButton.onClick.RemoveListener(OnCopyButtonClicked);
        }




        /// <summary>
        /// Handler for the click event of the copy button.
        /// </summary>

        void OnCopyButtonClicked()
        {
            WebGLCopyAndPasteAPI.CopyToClipboard(inputField.text);
        }
    }
}
