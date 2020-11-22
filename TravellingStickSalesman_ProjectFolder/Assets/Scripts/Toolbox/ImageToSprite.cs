using UnityEngine;
using System.Collections;
using System.IO;

namespace Toolbox
{
    // Found this in the unity forums, and adapted it to be a bit simpler.
    // https://forum.unity.com/threads/generating-sprites-dynamically-from-png-or-jpeg-files-in-c.343735/
    public static class ImageToSprite
    {
        public static Sprite LoadNewSprite(string filePath, float pixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
        {
            // Load a PNG or JPG image from disk to a Texture2D...
            Texture2D spriteTexture = LoadTexture(filePath);
            // Now convert it to a new sprite and return its reference.
            return ConvertTextureToSprite(spriteTexture, pixelsPerUnit, spriteType);
        }

        private static Sprite ConvertTextureToSprite(Texture2D texture, float pixelsPerUnit, SpriteMeshType spriteType)
        {
            // Converts a Texture2D to a sprite, assign this texture to a new sprite and return its reference
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Vector2 pivot = new Vector2(0, 0);
            Sprite newSprite = Sprite.Create(texture, rect, pivot, pixelsPerUnit, 0, spriteType);
            return newSprite;
        }

        private static Texture2D LoadTexture(string filePath)
        {
            // Load a PNG or JPG file from disk to a Texture2D
            // Returns null if load fails
            if (!File.Exists(filePath)) return null; // Return null if load failed
            byte[] fileData = File.ReadAllBytes(filePath);
            var texture2D = new Texture2D(2, 2);
            return texture2D.LoadImage(fileData) ? texture2D : null;
        }
    }
}