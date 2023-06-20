using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ameye.EditorUtilities.Editor.CircularMenu
{
    public sealed class CircularMenuButton : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<CircularMenuButton, UxmlTraits>
        {
        }

        public new class UxmlTraits : BindableElement.UxmlTraits
        {
        }

        public CircularMenuButton()
        {

        }

        private readonly Button button;
        public readonly int Section;

        public CircularMenuButton(string text, string icon, int section, Action clickEvent)
        {
            Section = section;

            style.position = Position.Absolute;
            style.alignItems = Align.Center;

            button = new Button(clickEvent)
            {
                style =
                {
                    paddingLeft = 8,
                    paddingRight = 8,
                    paddingTop = 4,
                    paddingBottom = 4,
                    flexDirection = FlexDirection.Row,
                    borderTopLeftRadius = 4.0f,
                    borderBottomLeftRadius = 4.0f,
                    borderBottomRightRadius = 4.0f,
                    borderTopRightRadius = 4.0f,
                    flexGrow = 1,
                    backgroundColor = new Color(0.02f, 0.02f, 0.02f, 0.8f)
                },
                text = ""
            };

            var label = new Label
            {
                style =
                {
                    paddingBottom = 0.0f,
                    paddingLeft = 0.0f,
                    paddingRight = 0.0f,
                    paddingTop = 0.0f,
                    marginLeft = 5.0f,
                    marginRight = 5.0f,
                    flexGrow = 1,
                },
                text = text
            };

            if (icon != "")
            {
                var image = new Image
                {
                    image = EditorGUIUtility.IconContent(icon).image,
                    style =
                    {
                        width = 16.0f,
                        height = 16.0f,
                        flexShrink = 0
                    }
                };
                button.Add(image);
            }

            button.Add(label);

            if (section != -1)
            {
                var index = new Label
                {
                    text = section.ToString(),
                    style =
                    {
                        color = new Color(0.7f, 0.7f, 0.7f, 1.0f),
                        unityFontStyleAndWeight = FontStyle.Italic
                    }
                };
                button.Add(index);
            }
            Add(button);
        }

        public void Hover(bool active)
        {
            button.style.backgroundColor = active ? new Color(0.2745098f, 0.3764706f, 0.4862745f, 1.0f) : new Color(0.02f, 0.02f, 0.02f, 0.8f);
        }
    }
}