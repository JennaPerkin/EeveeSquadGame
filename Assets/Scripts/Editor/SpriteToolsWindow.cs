using System;

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class SpriteToolsWindow : EditorWindow
{

    //private Palette oldPalette;
    //public Palette newPalette;
    public float ratio = 1f;
    public int realTimePreviewUnder = 256;
    public string saveName = "";

    //public Color selection, variation;

    SpriteToolsWindow window;
    Texture2D toRecolor;
    Texture2D recolored;
    Texture2D preview;
    static Texture2D picker;

    float y_button = 80, height_button = 30;
    float y_palette;

    static int saved = 0;

    Rect toRecolor_rect, preview_rect;

    [MenuItem("Tools/Sprite Editor")]
    public static void Open()
    {
        picker = EditorGUIUtility.FindTexture("EyeDropper.Large");//Resources.Load("EyeDrop") as Texture2D;

        //Debug.Log(picker.width + " " + picker.height);
        SpriteToolsWindow window = GetWindow<SpriteToolsWindow>("Sprite tools");
        window.position = new Rect(0, 0, 600, 300);
        window.Show();
    }

    static Dictionary<Color, List<Vector2Int>> colorMap = new Dictionary<Color, List<Vector2Int>>();
    public static Palette GetPaletteFrom(Texture2D texture)
    {
        colorMap = new Dictionary<Color, List<Vector2Int>>();
        Palette p = new Palette();
        Vector2Int pos = Vector2Int.zero;
        for (pos.x = 0; pos.x < texture.width; pos.x++)
        {
            for (pos.y = 0; pos.y < texture.height; pos.y++)
            {
                Color c = texture.GetPixel(pos.x, pos.y);
                if (p.Contains(c))
                {
                    colorMap[c].Add(pos);
                }
                else if (c.a != 0)
                {
                    p.palette.Add(c);
                    List < Vector2Int > list = new List<Vector2Int>();
                    list.Add(pos);
                    colorMap.Add(c, list);
                }
            }
        }
        return p;
    }



    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        Texture prevToRecolor = toRecolor;
        toRecolor = TextureField("To Recolor", toRecolor);

        if (toRecolor)
        {
            if (toRecolor.isReadable)
            {
                if (!toRecolor.Equals(prevToRecolor))
                {
                    //Debug.Log("Sprite change");
                    InitializeView();
                    ApplyPalette();
                }

                DrawButtons();
                Color lastVariation = windowPalette.variation;
                DrawFields();

                if (toRecolor.width < realTimePreviewUnder&& !lastVariation.Equals(windowPalette.variation))//

                {
                    //Debug.Log("Realtime update");
                    ApplyColor(windowPalette.selection, windowPalette.variation);                    
                    //ApplyPalette();
                }
                DrawImages();

                if (this.hasFocus)
                {
                    windowPalette.Repaint();
                }

            }
            else
            {
                preview = null;
                GUILayout.BeginVertical();
                EditorGUILayout.HelpBox("Please enable read/write flag to edit and click restore palette", MessageType.Warning);
                EditorGUILayout.HelpBox("Recommended settings are:\n read/write flag on\n filter mode point no filter\n compression none\nImages with too many colors or too large can be difficult to manage ", MessageType.Info);
                GUILayout.EndVertical();
            }

        }
        else
        {
            EditorGUILayout.HelpBox("Select a Texture", MessageType.Info);
        }

        GUILayout.EndHorizontal();
    }

    void InitializeView()
    {

        // Add an image to the editor/resources directory & set its type to 'Cursor' first...
        Cursor.SetCursor(picker, new Vector2(0, 19), CursorMode.Auto);
        toRecolor.filterMode = FilterMode.Point;
        toRecolor.Apply();
        saveName = toRecolor.name + " " + saved;
        preview = DuplicateTexture(toRecolor);

        if (!windowPalette)
        {
            windowPalette = PaletteWindow.Open(GetPaletteFrom(toRecolor), GetPaletteFrom(toRecolor), this);
        }
        else
        {
            windowPalette.oldPalette = GetPaletteFrom(toRecolor);
            windowPalette.newPalette = GetPaletteFrom(toRecolor);
        }

        if (windowPalette.oldPalette.palette.Count > 0)
        {
            //Debug.Log("Selection changed by InitializeView");
            windowPalette.selection = windowPalette.oldPalette.palette[0];
            windowPalette.variation = windowPalette.newPalette.palette[0];
        }
    }
    Vector2 scrollPosition;
    private void DrawImages()
    {
        scrollPosition = GUI.BeginScrollView(new Rect(55, y_button + height_button * 2, position.width, position.height - (y_button + height_button * 2)), scrollPosition, new Rect(0, 0, toRecolor.width * 2 * ratio, toRecolor.height * ratio < 512 ? toRecolor.height * ratio : 512));

       // toRecolor_rect = new Rect(55, y_button + height_button * 2, toRecolor.width * ratio, toRecolor.height * ratio);
       // preview_rect = new Rect(80 + preview.width * ratio, y_button + height_button * 2, preview.width * ratio, preview.height * ratio);
        toRecolor_rect = new Rect(0, height_button, toRecolor.width * ratio, toRecolor.height * ratio);
        preview_rect = new Rect(25 + preview.width * ratio, height_button, preview.width * ratio, preview.height * ratio);

        EditorGUI.LabelField(new Rect(0, 0, 100, 30), "Original: ");
        EditorGUI.DrawTextureTransparent(toRecolor_rect, toRecolor, ScaleMode.ScaleToFit, 0);

        EditorGUIUtility.AddCursorRect(toRecolor_rect, MouseCursor.CustomCursor);

        if (toRecolor.width > realTimePreviewUnder)
        {
            if (GUI.Button(new Rect(25 + toRecolor.width * ratio, 0, 100, 30), "Update Preview"))
            {
                //Debug.Log("Request from button");
                ApplyPalette();
                return;
            }
        }
        else
        {
            EditorGUI.LabelField(new Rect(25 + toRecolor.width * ratio, 0, 100, 30), "Preview: ");
        }
        EditorGUI.DrawTextureTransparent(preview_rect, preview, ScaleMode.ScaleToFit, 0);




        if (Event.current.type == EventType.MouseDown)
        {
            windowPalette.selection = PickColor();
            if (windowPalette.oldPalette.Contains(windowPalette.selection))
                windowPalette.variation = windowPalette.VariationOf(windowPalette.selection);// windowPalette.newPalette.palette[windowPalette.oldPalette.IndexOf(windowPalette.selection)];

            windowPalette.UpdateScrollView();
            //Update the window to be consistent with selected color
            Repaint();
        }


        GUI.EndScrollView();
    }

    void DrawFields()
    {

        saveName = EditorGUI.TextField(new Rect(170, 0, 300, 20), "Save Name", saveName);
        realTimePreviewUnder = EditorGUI.IntField(new Rect(170, 20, 200, 20), "Realtime Preview Under", realTimePreviewUnder>1? realTimePreviewUnder:1);
        EditorGUI.LabelField(new Rect(370, 20, 120, 20), " px");

        ratio = EditorGUI.Slider(new Rect(170, 40, 300, 20), "Zoom: ", ratio, 0.1f, 10f);
        EditorGUI.DrawRect(new Rect(520, 60, 50, 50), windowPalette.selection);
        GUIContent eyedropper = EditorGUIUtility.IconContent("EyeDropper.Large");
        EditorGUI.LabelField(new Rect(530, 85, 20, 20), eyedropper);


        if (windowPalette.oldPalette.Contains(windowPalette.selection))
        {
            //windowPalette.newPalette.palette[windowPalette.oldPalette.IndexOf(windowPalette.selection)] = windowPalette.variation;
            windowPalette.SetColorSelection();
            //windowPalette.SetColor(windowPalette.GetIndexOfSelection(), windowPalette.variation);
            windowPalette.variation = EditorGUI.ColorField(new Rect(580, 60, 50, 50), windowPalette.variation);
        }
        else
        {
            EditorGUI.HelpBox(new Rect(580, 60, 100, 50), "Selection must be in palette", MessageType.Warning);
        }

    }

    PaletteWindow windowPalette;
    void DrawButtons()
    {
        if (GUI.Button(new Rect(170, y_button, 100, height_button), "Restore Palette"))
        {
            InitializeView();
        }
        if (GUI.Button(new Rect(280, y_button, 100, height_button), "Save"))
        {
            string path = AssetDatabase.GetAssetPath(toRecolor).Replace(toRecolor.name + ".png", "");

            SaveTexture(preview, saveName, path);
            saved++;

        }
        if (GUI.Button(new Rect(390, y_button, 100, height_button), "Save As..."))
        {
            string path = path = EditorUtility.SaveFilePanel(
            "Save texture as PNG",
            "",
            saveName + ".png",
            "png");

            SaveTexture(preview, saveName, path);
            saved++;
        }
    }

    public void ApplyPalette()
    {
        for (int x = 0; x < preview.width; x++)
        {
            for (int y = 0; y < preview.height; y++)
            {
                Color toReplace = toRecolor.GetPixel(x, y);
                if (windowPalette.oldPalette.Contains(toReplace))
                {
                    Color color = windowPalette.newPalette.palette[windowPalette.oldPalette.IndexOf(toReplace)];
                    preview.SetPixel(x, y, color);
                }
            }
        }
        preview.Apply();
    }

    public void ApplyColor(Color key, Color variation)
    {
        
            if (colorMap.ContainsKey(key))
            {
                List<Vector2Int> pos = colorMap[key];
                for (int i = 0; i < pos.Count; i++)
                {
                    preview.SetPixel(pos[i].x, pos[i].y, variation);
                }
            }
            preview.Apply();
    }


    public Color PickColor()
    {
        //Rect pickArea = new Rect(55, y_button + height_button * 2, toRecolor.width * ratio, toRecolor.height * ratio); 
        Color c = windowPalette.selection;
        if (IsMouseOverRect(toRecolor_rect))
        {
            Vector2Int pos = TextureCoordinate(toRecolor_rect, toRecolor, ratio);
            c = toRecolor.GetPixel(pos.x, pos.y);
            //Debug.Log("Picking color in "+pos);
        }
        return c;
    }

    #region SUPPORT_FUNCTIONS
    private bool SaveTexture(Texture2D t, string fileName, string path)
    {
        if (path != "")
        {
            var bytes = t.EncodeToPNG();
            string fullPath = path + fileName + ".png";
            var file = File.Open(fullPath, FileMode.Create);
            var binary = new BinaryWriter(file);
            binary.Write(bytes);
            file.Close();


            AssetDatabase.ImportAsset(fullPath, ImportAssetOptions.ImportRecursive);
            this.ShowNotification(new GUIContent("Saved " + fileName + " in " + path), 5);
            Debug.Log("Saved " + fileName + " in " + path);
            return true;
        }
        else
        {
            this.ShowNotification(new GUIContent("File not saved"), 5);
            return false;
        }

    }

    public static void InvertColors(Texture2D texture)
    {
        for (int m = 0; m < texture.mipmapCount; m++)
        {
            Color[] c = texture.GetPixels(m);
            for (int i = 0; i < c.Length; i++)
            {
                c[i].r = 1 - c[i].r;
                c[i].g = 1 - c[i].g;
                c[i].b = 1 - c[i].b;
            }
            texture.SetPixels(c, m);
        }
        texture.Apply();
    }

    private static Texture2D TextureField(string name, Texture2D texture)
    {
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperCenter;
        style.fixedWidth = 70;
        GUILayout.Label(name, style);
        var result = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(70), GUILayout.Height(70));

        return result;
    }

    public static Vector2 RelativeMousePosIn(Rect rect)
    {
        Vector2 relativePos = new Vector2(-1, -1);
        Vector2 mouse = Event.current.mousePosition;
        if (IsMouseOverRect(rect))
        {
            //Inside the rectangle
            relativePos.x = (int)(mouse.x - rect.x);
            relativePos.y = (int)(mouse.y - rect.y);
        }
        return relativePos;
    }

    public static bool IsMouseOverRect(Rect rect)
    {
        Vector2 mouse = Event.current.mousePosition;
        return (mouse.x > rect.x && mouse.x < rect.x + rect.width && mouse.y > rect.y && mouse.y < rect.y + rect.height);
    }

    public static Vector2Int TextureCoordinate(Rect rect, Texture texture, float ratio)
    {
        Vector2Int coordinate = new Vector2Int(0, 0);
        if (IsMouseOverRect(rect))
        {
            Vector2 v2 = RelativeMousePosIn(rect);
            coordinate = new Vector2Int((int)(v2.x / ratio), (int)(v2.y / ratio));
            //Inside the rectangle
            coordinate.y = ((texture.height - 1) - coordinate.y);
        }
        return coordinate;
    }

    public static Texture2D DuplicateTexture(Texture2D texture)
    {
        Texture2D duplicated = new Texture2D(texture.width, texture.height);

        duplicated.SetPixels(texture.GetPixels());
        duplicated.filterMode = FilterMode.Point;

        duplicated.Apply();

        return duplicated;
    }
    
    #endregion

}

[Serializable]
public class Palette
{
    public List<Color> palette = new List<Color>();

    public Palette()
    {
    }

    public bool Contains(Color color)
    {
        return palette.Contains(color);
    }

    public int IndexOf(Color color)
    {
        return palette.IndexOf(color);
    }
}

public class PaletteWindow : EditorWindow
{
    public Palette oldPalette, newPalette;
    public Color selection, variation;
    public SpriteToolsWindow linked;

    public static PaletteWindow Open(Palette oldPalette, Palette newPalette, SpriteToolsWindow linked)
    {
        PaletteWindow window = GetWindow<PaletteWindow>("Palette", new Type[1] { linked.GetType() }); //, new Type[1] { docked.GetType() }

        window.linked = linked;

        window.position = new Rect(linked.position.x + linked.position.width, linked.position.y, linked.position.width / 2f, linked.position.height);
        window.oldPalette = oldPalette;
        window.newPalette = newPalette;

        window.Show();

        return window;
    }

    public void OnGUI()
    {

        DrawPalette(20, 0, 40, 30, 8, "Palette variation");
        /*if (this.hasFocus)
        {
            linked.Repaint();
        }*/


    }
    public Vector2 scrollPosition = Vector2.zero;
    public void DrawPalette(float x_origin = 0, float y_origin = 0, float cell_width = 40, float cell_height = 40, float spacing = 2, string paletteName = "Palette: ")
    {

        int col, row = 0;
        float x = x_origin, y = y_origin;
        int cells = Mathf.CeilToInt(position.width / (cell_width * 1.5f + spacing)) - 1;
        if (autoScroll)
        {
            float selectionRow = newPalette.IndexOf(selection) / (float)cells;
            scrollPosition = new Vector2(0, (selectionRow * cell_height + spacing));
            autoScroll = false;
            //Debug.Log("autoScroll"+scrollPosition);
        }

        scrollPosition = GUI.BeginScrollView(new Rect(x, y, position.width, position.height), scrollPosition, new Rect(x, y, position.width, (cell_height + spacing) * (newPalette.palette.Count / cells)));

        EditorGUI.LabelField(new Rect(x_origin, y_origin, 100, cell_height), paletteName);


        for (int i = 0; i < newPalette.palette.Count; i++)
        {

            cells = cells < 1 ? 1 : cells;


            col = i % cells;
            x = x_origin + col * (cell_width * 1.5f + spacing);

            if (col == 0)
            {
                row++;
                y = y_origin + row * (cell_height + spacing);
            }


            Rect rect = new Rect(x, y + 1, cell_width / 2f, cell_height - 1);
            if (SpriteToolsWindow.IsMouseOverRect(new Rect(x - 1, y - 1, cell_width * 1.5f + 3, cell_height + 3)))
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    //Debug.Log("Mouse down on palette");
                    selection = oldPalette.palette[i];
                    variation = newPalette.palette[i];
                    Repaint();
                }
            }
            if (variation.Equals(newPalette.palette[i]))
                EditorGUI.DrawRect(new Rect(x - 2, y - 2, cell_width * 1.5f + 5, cell_height + 3), new Color(.6f, 1f, 1f, 1f));
            else
                EditorGUI.DrawRect(new Rect(x - 1, y - 1, cell_width * 1.5f + 3, cell_height + 3), new Color(.3f, .3f, .3f, 1f));


            Color temp = newPalette.palette[i];
            EditorGUI.DrawRect(rect, oldPalette.palette[i]);
            newPalette.palette[i] = EditorGUI.ColorField(new Rect(x + cell_width / 2f, y, cell_width, cell_height), newPalette.palette[i]);
            
            //Se viene modificato questo field aggiorna la selection e la variation
            if (!temp.Equals(newPalette.palette[i]))
            {
                selection = oldPalette.palette[i];
                variation = newPalette.palette[i];
                linked.ApplyColor(selection,variation);
                linked.Repaint();
            }
        }

        GUI.EndScrollView();
    }
    bool autoScroll = false;
    public void UpdateScrollView()
    {
        autoScroll = true;
    }


    public Color VariationOf(Color color)
    {
        return newPalette.palette[oldPalette.IndexOf(color)];
    }

    public void SetColor(int i, Color color)
    {
        newPalette.palette[i] = color;
    }
    public void SetColorSelection()
    {
        SetColor(GetIndexOfSelection(), variation);
    }
    public int GetIndexOfSelection()
    {
        return oldPalette.IndexOf(selection);
    }
}