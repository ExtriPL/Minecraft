using Minecraft.Core.Blocks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Block))]
public class BlockEditor : Editor
{
    private const float ColumnWidht = 110f, EmptyFieldHeight = 91.25f;
    private bool areAllSidesDifferent = false;

    public override void OnInspectorGUI()
    {
        Block block = (Block)target;

        areAllSidesDifferent = GUILayout.Toggle(block.Texture.areAllSidesDifferent, "Are all sides different");
        GUILayout.Space(20);

        if (areAllSidesDifferent)
            DrawAllTextureFields(block);
        else
            DrawSingleTextureField(block);

        GUILayout.Space(20);

        serializedObject.Update();

        base.OnInspectorGUI();

        if(GUI.changed)
            EditorUtility.SetDirty(target);
    }

    private static Texture2D TextureField(string name, Texture2D texture)
    {
        GUILayout.BeginVertical(GUILayout.Width(70));
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperCenter;
        style.fixedWidth = 70;
        style.fixedHeight = 15;
        GUILayout.Label(name, style);
        var result = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(70), GUILayout.Height(70));
        GUILayout.EndVertical();
        return result;
    }

    private void DrawAllTextureFields(Block block)
    {
        BlockTexture texture = block.Texture;

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical(GUILayout.Width(ColumnWidht));

        GUILayout.Space(EmptyFieldHeight);
        texture.left = TextureField("Left", texture.left);

        GUILayout.EndVertical();

        GUILayout.BeginVertical(GUILayout.Width(ColumnWidht));

        texture.up = TextureField("Up", texture.up);
        texture.front = TextureField("Front", texture.front);
        texture.down = TextureField("Down", texture.down);

        GUILayout.EndVertical();

        GUILayout.BeginVertical(GUILayout.Width(ColumnWidht));

        GUILayout.Space(EmptyFieldHeight);
        texture.right = TextureField("Right", texture.right);

        GUILayout.EndVertical();

        GUILayout.BeginVertical(GUILayout.Width(ColumnWidht));

        GUILayout.Space(EmptyFieldHeight);
        texture.back = TextureField("Back", texture.back);

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();

        texture.areAllSidesDifferent = true;
        block.SetTexture(texture);
    }

    private void DrawSingleTextureField(Block block)
    {
        BlockTexture texture = block.Texture;

        texture.up = TextureField("All Sides", texture.up);
        texture.down = texture.up;
        texture.left = texture.up;
        texture.right = texture.up;
        texture.front = texture.up;
        texture.back = texture.up;

        texture.areAllSidesDifferent = false;
        block.SetTexture(texture);
    }
}
