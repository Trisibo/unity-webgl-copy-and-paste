using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WebGLCopyAndPaste.Samples
{
    /// <summary>
    /// Add to a password input field to reveal its text in another label.
    /// </summary>

    public class PasswordFieldTextRevealer : MonoBehaviour
    {
        #region Parameters


        [SerializeField] TMP_Text labelToCopyPasswordTo = default;


        #endregion




        #region Private fields


        TMP_InputField textMeshProInputField;
        InputField     legacyInputField;


        #endregion




        /// <summary>
        /// Implementation of "MonoBehaviour.Start".
        /// </summary>

        void Start()
        {
            labelToCopyPasswordTo.richText = true;
            labelToCopyPasswordTo.text = null;

            if (TryGetComponent<TMP_InputField>(out textMeshProInputField))
            {
                textMeshProInputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
                textMeshProInputField.onValueChanged.AddListener(OnInputFieldValueChanged);

                OnInputFieldValueChanged(textMeshProInputField.text);
            }
            else if (TryGetComponent<InputField>(out legacyInputField))
            {
                legacyInputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
                legacyInputField.onValueChanged.AddListener(OnInputFieldValueChanged);

                OnInputFieldValueChanged(legacyInputField.text);
            }
        }




        /// <summary>
        /// Implementation of "MonoBehaviour.OnDestroy".
        /// </summary>

        void OnDestroy()
        {
            if (textMeshProInputField != null)
            {
                textMeshProInputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
            }
            else if (legacyInputField != null)
            {
                legacyInputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
            }
        }




        /// <summary>
        /// Handler for the input field value changed event.
        /// </summary>
        /// <param name="newText">The new text.</param>

        void OnInputFieldValueChanged(string newText)
        {
            string message = "Password: ";

            if (string.IsNullOrEmpty(newText))
            {
                message += "<alpha=#7F>(empty)";
            }
            else
            {
                message += $"\"{newText}\"";
            }

            labelToCopyPasswordTo.text = message;
        }
    }
}
