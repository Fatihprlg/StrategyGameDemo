namespace Helpers
{
    using UnityEngine;

    public static class Vectors
    {
        public static bool IsPointInAreaRange(Vector2 point, Vector2 center, float range)
        {
            Vector3 start = center - Vector2.one * range;
            Vector3 end = center + Vector2.one * range;
            bool isInRange = (point.x >= start.x && point.x <= end.x) && (point.y >= start.y && point.y <= end.y);
            return isInRange;
        }

        public static Vector3 ScreenToWorldPoint(Vector2 point, Camera camera, float distanceFromCamera = 10)
        {
            Vector3 pos = point;
            pos.z = distanceFromCamera;
            Vector3 worldPos = camera.ScreenToWorldPoint(pos);
            return worldPos;
        }
        
        public static bool IsPointBetweenTwoVector(Vector3 aStartPoint, Vector3 aEndPoint, Vector3 point)
        {
            return Vector3.Dot((aEndPoint - aStartPoint).normalized, (point - aEndPoint).normalized) <= 0f && Vector3.Dot((aStartPoint - aEndPoint).normalized, (point - aStartPoint).normalized) <= 0f;
        }
    }
    
    public static class Colors
    {
        public static Color InvertColor(Color color)
        {
            return new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
        }

        public static Color ToColor(string str)
        {
            if (!str.Contains("#"))
            {
                str = "#" + str;
            }

            ColorUtility.TryParseHtmlString(str, out Color color);
            return color;
        }

        public static string ToString(Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }

    }
    
    public static class Other
    {
        public static void SaveTexture(Texture2D texture, string path = Constants.Strings.RENDERED_TEXTURES_PATH, string name = "TextureOutput")
        {
            byte[] bytes = texture.EncodeToPNG();
            
            var dirPath = Application.dataPath + "/Resources/" +path;
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }

            if (System.IO.File.Exists(dirPath + "/" + name))
                name += Random.Range(0, 100000).ToString();
            System.IO.File.WriteAllBytes(dirPath + "/" + name + ".png", bytes);
            Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
        public static Texture2D DeCompress(this Texture2D source)
        {
            var pixels = source.GetPixels();
            Texture2D readableText = new (source.width, source.height);
            readableText.SetPixels(pixels);
            readableText.Apply();
            return readableText;
        }
        public static Texture2D LoadTexture(string path)
        {
            Texture2D texSource = Resources.Load<Texture2D>(path);
            return texSource.DeCompress();
        }

        public static Texture2D[] LoadAllTextures(string path)
        {
            return Resources.LoadAll<Texture2D>(path);
        }
    }
    
   
}
