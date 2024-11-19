using UnityEditor;

[CustomEditor(typeof(ContentSO))]
public class ContentSOEditor : Editor
{
    SerializedProperty ContentType_Prop;

    SerializedProperty ContentTemplate_Prop;

    SerializedProperty IsFreeCancel_Prop;
    SerializedProperty CombatEpilogue_Prop;
    SerializedProperty CombatRewards_Prop;

    SerializedProperty BodyText_Prop;

    SerializedProperty ActionTitle_Prop;
    SerializedProperty ActionRequestSlots1Row_Prop;
    SerializedProperty ActionRequestSlots2Row_Prop;
    SerializedProperty CombatOptionSets_Prop;

    SerializedProperty EventEpilogueImage_Prop;
    SerializedProperty EventEpilogue_Prop;
    SerializedProperty EventRewards_Prop;

    private void OnEnable()
    {
        ContentType_Prop = serializedObject.FindProperty("contentType");

        ContentTemplate_Prop = serializedObject.FindProperty("contentTemplate");

        IsFreeCancel_Prop = serializedObject.FindProperty("isFreeCancel");
        CombatEpilogue_Prop = serializedObject.FindProperty("combatEpilogue");
        CombatRewards_Prop = serializedObject.FindProperty("combatRewards");

        BodyText_Prop = serializedObject.FindProperty("bodyText");

        ActionTitle_Prop = serializedObject.FindProperty("actionTitle");
        ActionRequestSlots1Row_Prop = serializedObject.FindProperty("actionRequestSlots1Row");
        ActionRequestSlots2Row_Prop = serializedObject.FindProperty("actionRequestSlots2Row");
        CombatOptionSets_Prop = serializedObject.FindProperty("combatOptionSets");

        EventEpilogueImage_Prop = serializedObject.FindProperty("eventEpilogueImage");
        EventEpilogue_Prop = serializedObject.FindProperty("eventEpilogue");
        EventRewards_Prop = serializedObject.FindProperty("eventRewards");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(ContentType_Prop);
        EditorGUILayout.PropertyField(ContentTemplate_Prop);

        if ((ContentSO.ContentType)ContentType_Prop.enumValueIndex == ContentSO.ContentType.Blank)
        {
        }
        else if ((ContentSO.ContentType)ContentType_Prop.enumValueIndex == ContentSO.ContentType.Image)
        {
            EditorGUILayout.PropertyField(IsFreeCancel_Prop);

            EditorGUILayout.PropertyField(CombatEpilogue_Prop);
            EditorGUILayout.PropertyField(CombatRewards_Prop);
        }
        else if ((ContentSO.ContentType)ContentType_Prop.enumValueIndex == ContentSO.ContentType.Description)
        {
            EditorGUILayout.PropertyField(BodyText_Prop);
        }
        else if ((ContentSO.ContentType)ContentType_Prop.enumValueIndex == ContentSO.ContentType.Action)
        {
            EditorGUILayout.PropertyField(ActionTitle_Prop);
            EditorGUILayout.PropertyField(ActionRequestSlots1Row_Prop);
            EditorGUILayout.PropertyField(ActionRequestSlots2Row_Prop);
            EditorGUILayout.PropertyField(CombatOptionSets_Prop);

            EditorGUILayout.PropertyField(EventEpilogueImage_Prop);
            EditorGUILayout.PropertyField(EventEpilogue_Prop);
            EditorGUILayout.PropertyField(EventRewards_Prop);
        }

        serializedObject.ApplyModifiedProperties();
    }
}