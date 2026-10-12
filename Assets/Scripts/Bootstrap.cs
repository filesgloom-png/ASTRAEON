using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

namespace Astraeon
{
    public static class Bootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Launch()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            if (Object.FindObjectOfType<AstraeonTitleScreen>() == null)
            {
                var root = new GameObject("ASTRAEON_TitleScreen");
                root.AddComponent<AstraeonTitleScreen>();
            }
        }
    }

    public sealed class AstraeonTitleScreen : MonoBehaviour
    {
        private Font font;
        private Canvas canvas;

        private void Start()
        {
            font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            BuildUI();
        }

        private void BuildUI()
        {
            var canvasObject = new GameObject("ASTRAEON_Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.matchWidthOrHeight = 0.5f;

            if (FindObjectOfType<EventSystem>() == null)
                new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));

            var background = Panel("Background", canvas.transform, new Color(0.025f, 0.035f, 0.055f, 1f));
            Stretch(background.rectTransform);

            var glow = Panel("AmbientGlow", canvas.transform, new Color(0.04f, 0.16f, 0.17f, 0.45f));
            glow.rectTransform.anchorMin = new Vector2(0.48f, 0f);
            glow.rectTransform.anchorMax = new Vector2(1f, 1f);
            glow.rectTransform.offsetMin = Vector2.zero;
            glow.rectTransform.offsetMax = Vector2.zero;

            var emblem = Label("Sigil", canvas.transform, "✦", 74, new Color(0.72f, 0.79f, 0.56f), TextAnchor.MiddleCenter);
            Place(emblem.rectTransform, new Vector2(0.5f, 0.79f), new Vector2(0.5f, 0.79f), new Vector2(0, 0), new Vector2(150, 100));

            var title = Label("Title", canvas.transform, "ASTRAEON", 88, new Color(0.91f, 0.89f, 0.78f), TextAnchor.MiddleCenter);
            title.fontStyle = FontStyle.Bold;
            title.characterSpacing = 8;
            Place(title.rectTransform, new Vector2(0.5f, 0.64f), new Vector2(0.5f, 0.64f), Vector2.zero, new Vector2(1100, 130));

            var subtitle = Label("Subtitle", canvas.transform, "ETERNAL DOMINION", 24, new Color(0.49f, 0.68f, 0.64f), TextAnchor.MiddleCenter);
            subtitle.characterSpacing = 5;
            Place(subtitle.rectTransform, new Vector2(0.5f, 0.565f), new Vector2(0.5f, 0.565f), Vector2.zero, new Vector2(900, 48));

            var line = Panel("Divider", canvas.transform, new Color(0.57f, 0.63f, 0.43f, 0.85f));
            Place(line.rectTransform, new Vector2(0.5f, 0.525f), new Vector2(0.5f, 0.525f), Vector2.zero, new Vector2(360, 2));

            var description = Label("Description", canvas.transform, "THE AGE OF ASHES AWAITS", 23, new Color(0.69f, 0.72f, 0.72f), TextAnchor.MiddleCenter);
            description.characterSpacing = 2;
            Place(description.rectTransform, new Vector2(0.5f, 0.45f), new Vector2(0.5f, 0.45f), Vector2.zero, new Vector2(900, 55));

            var play = Button("BeginJourney", canvas.transform, "BEGIN JOURNEY", new Vector2(0.5f, 0.315f), new Vector2(420, 88), true);
            play.onClick.AddListener(() => ShowNotice("THE REALM IS BEING FORGED"));

            var settings = Button("Settings", canvas.transform, "SETTINGS", new Vector2(0.5f, 0.205f), new Vector2(260, 58), false);
            settings.onClick.AddListener(() => ShowNotice("SETTINGS WILL ARRIVE IN A FUTURE UPDATE"));

            var footer = Label("Footer", canvas.transform, "MOBILE CLIENT PROTOTYPE  •  PRE-ALPHA", 17, new Color(0.38f, 0.44f, 0.45f), TextAnchor.MiddleCenter);
            Place(footer.rectTransform, new Vector2(0.5f, 0.075f), new Vector2(0.5f, 0.075f), Vector2.zero, new Vector2(1000, 40));

            var version = Label("Version", canvas.transform, "ALPHA 0.1", 16, new Color(0.47f, 0.57f, 0.55f), TextAnchor.MiddleRight);
            Place(version.rectTransform, new Vector2(0.97f, 0.94f), new Vector2(0.97f, 0.94f), new Vector2(-180, -25), new Vector2(260, 40));
        }

        private Image Panel(string objectName, Transform parent, Color color)
        {
            var go = new GameObject(objectName, typeof(RectTransform), typeof(Image));
            go.transform.SetParent(parent, false);
            var image = go.GetComponent<Image>();
            image.color = color;
            image.raycastTarget = false;
            return image;
        }

        private Text Label(string objectName, Transform parent, string value, int size, Color color, TextAnchor alignment)
        {
            var go = new GameObject(objectName, typeof(RectTransform), typeof(Text));
            go.transform.SetParent(parent, false);
            var text = go.GetComponent<Text>();
            text.font = font;
            text.text = value;
            text.fontSize = size;
            text.color = color;
            text.alignment = alignment;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.raycastTarget = false;
            return text;
        }

        private Button Button(string objectName, Transform parent, string caption, Vector2 anchor, Vector2 size, bool primary)
        {
            var go = new GameObject(objectName, typeof(RectTransform), typeof(Image), typeof(Button));
            go.transform.SetParent(parent, false);
            var image = go.GetComponent<Image>();
            image.color = primary ? new Color(0.36f, 0.43f, 0.29f, 1f) : new Color(0.08f, 0.12f, 0.14f, 0.95f);
            var button = go.GetComponent<Button>();
            button.targetGraphic = image;
            var colors = button.colors;
            colors.normalColor = image.color;
            colors.highlightedColor = primary ? new Color(0.48f, 0.56f, 0.37f) : new Color(0.16f, 0.23f, 0.23f);
            colors.pressedColor = new Color(0.19f, 0.28f, 0.26f);
            button.colors = colors;
            Place(go.GetComponent<RectTransform>(), anchor, anchor, Vector2.zero, size);

            var label = Label("Label", go.transform, caption, primary ? 26 : 20, new Color(0.94f, 0.93f, 0.84f), TextAnchor.MiddleCenter);
            Stretch(label.rectTransform);
            label.fontStyle = FontStyle.Bold;
            label.characterSpacing = primary ? 3 : 2;
            return button;
        }

        private void ShowNotice(string message)
        {
            var overlay = Panel("NoticeOverlay", canvas.transform, new Color(0.015f, 0.025f, 0.03f, 0.92f));
            Stretch(overlay.rectTransform);
            var text = Label("NoticeText", overlay.transform, message + "\n\nTAP TO RETURN", 28, new Color(0.83f, 0.84f, 0.73f), TextAnchor.MiddleCenter);
            Stretch(text.rectTransform);
            var button = overlay.gameObject.AddComponent<Button>();
            button.targetGraphic = overlay;
            button.onClick.AddListener(() => Destroy(overlay.gameObject));
        }

        private static void Stretch(RectTransform rect)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        private static void Place(RectTransform rect, Vector2 min, Vector2 max, Vector2 position, Vector2 size)
        {
            rect.anchorMin = min;
            rect.anchorMax = max;
            rect.anchoredPosition = position;
            rect.sizeDelta = size;
        }
    }
}
