using UnityEngine;

namespace UnwantedCompany.Render
{
    public class Rendering : MonoBehaviour
    {
        private static readonly Texture2D LineTexture = new Texture2D(1, 1);

        public static GUIStyle StringStyle { get; } = new GUIStyle(GUI.skin.label);

        public static Color Color
        {
            get => GUI.color;
            set => GUI.color = value;
        }

        public static void DrawString(Vector2 position, string label, bool centered = true)
        {
            var content = new GUIContent(label);
            var size = StringStyle.CalcSize(content);
            var pos = centered ? position - size / 2f : position;
            GUI.Label(new Rect(pos, size), content);
        }

        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
        {
            var matrix = GUI.matrix;
            Color = color;

            var angle = Vector3.Angle(pointB - pointA, Vector2.right);
            if (pointA.y > pointB.y) angle = -angle;

            var difference = pointB - pointA;
            GUIUtility.ScaleAroundPivot(new Vector2(difference.magnitude, width), pointA + new Vector2(0, 0.5f));
            GUIUtility.RotateAroundPivot(angle, pointA);
            GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), LineTexture);

            GUI.matrix = matrix;
        }
    }
}