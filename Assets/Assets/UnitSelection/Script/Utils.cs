﻿using UnityEngine;

public static class Utils{
    static Texture2D _whiteTexture;
    public static Texture2D WhiteTexture{
        get{
            if(_whiteTexture == null){
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }

            return _whiteTexture;
        }
    }

//    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2){
//        // Move origin from bottom left to top left
//        screenPosition1.y = Screen.height - screenPosition1.y;
//        screenPosition2.y = Screen.height - screenPosition2.y;
//        // Calculate corners
//		Vector3 topLeft = Vector3.Min(screenPosition1, screenPosition2);
//		Vector3 bottomRight = Vector3.Max(screenPosition1, screenPosition2);
//        // Create Rect
//        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
//    }

	static Bounds bounds = new Bounds();

    public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2){
        var v1 = camera.ScreenToViewportPoint(screenPosition1);
        var v2 = camera.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;
        //min.z = 0.0f;
        //max.z = 1.0f;

        bounds.SetMinMax(min, max);

        return bounds;
    }

//    public static void DrawScreenRect(Rect rect, Color color){
//        GUI.color = color;
//        GUI.DrawTexture(rect, WhiteTexture);
//        GUI.color = Color.white;
//    }
//
//	static Rect r = new Rect(0, 0, 0, 0);//recycle a single rect instead of having four that are generated all the time
//
//    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color){
//        // Top
//		r.Set(rect.xMin, rect.yMin, rect.width, thickness);
//        Utils.DrawScreenRect(r, color);
//        // Left
//		r.Set(rect.xMin, rect.yMin, thickness, rect.height);
//        Utils.DrawScreenRect(r, color);
//        // Right
//		r.Set(rect.xMax - thickness, rect.yMin, thickness, rect.height);
//        Utils.DrawScreenRect(r, color);
//        // Bottom
//		r.Set(rect.xMin, rect.yMax - thickness, rect.width, thickness);
//        Utils.DrawScreenRect(r, color);
//    }
}