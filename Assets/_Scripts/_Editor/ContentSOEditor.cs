using UnityEditor;

[CustomEditor(typeof(ContentSO))]
public class ContentSOEditor : Editor
{

    // CUSTOM EDITOR 항목 추가하기 //

    // Step.1 SerializedProperty에 _Prop 추가
    // Step.2 OnEnable에 Property 참조, 연결
    // Step.3 OnInspectorGUI에 GUI Layout 연동

    SerializedProperty ContentType_Prop;

    SerializedProperty ContentTemplate_Prop;

    SerializedProperty QuestTitle_Prop;
    SerializedProperty QuestImage_Prop;
    SerializedProperty QuestSeal_Prop;
    SerializedProperty IsThereCancelButton_Prop;

    SerializedProperty BackgroundImage_Prop;
    SerializedProperty BodyText_Prop;
    SerializedProperty CancelText_Prop;

    SerializedProperty ActionTitle_Prop;
    SerializedProperty ActionSeal_Prop;
    SerializedProperty ActionRequestDiceSlots_Prop;
    SerializedProperty MultiValue_Prop;
    SerializedProperty ActionRewardText_Prop;
    SerializedProperty IsThereProceedButton_Prop;

    private void OnEnable()
    {
        ContentType_Prop = serializedObject.FindProperty("contentType");

        ContentTemplate_Prop = serializedObject.FindProperty("contentTemplate");

        QuestTitle_Prop = serializedObject.FindProperty("questTitle");
        QuestImage_Prop = serializedObject.FindProperty("questImage");
        QuestSeal_Prop = serializedObject.FindProperty("questSeal");
        IsThereCancelButton_Prop = serializedObject.FindProperty("isThereCancelButton");

        BackgroundImage_Prop = serializedObject.FindProperty("backgroundImage");
        BodyText_Prop = serializedObject.FindProperty("bodyText");
        CancelText_Prop = serializedObject.FindProperty("cancelText");

        ActionTitle_Prop = serializedObject.FindProperty("actionTitle");
        ActionSeal_Prop = serializedObject.FindProperty("actionSeal");
        ActionRequestDiceSlots_Prop = serializedObject.FindProperty("actionRequestDiceSlots");
        MultiValue_Prop = serializedObject.FindProperty("multiValue");
        ActionRewardText_Prop = serializedObject.FindProperty("actionRewardText");
        IsThereProceedButton_Prop = serializedObject.FindProperty("isThereProceedButton");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(ContentType_Prop);

        if ((ContentSO.ContentType)ContentType_Prop.enumValueIndex == ContentSO.ContentType.Blank)
        {
            EditorGUILayout.PropertyField(ContentTemplate_Prop);
        }
        else if ((ContentSO.ContentType)ContentType_Prop.enumValueIndex == ContentSO.ContentType.Image)
        {
            EditorGUILayout.PropertyField(ContentTemplate_Prop);

            EditorGUILayout.PropertyField(QuestTitle_Prop);
            EditorGUILayout.PropertyField(QuestImage_Prop);
            EditorGUILayout.PropertyField(QuestSeal_Prop);
            EditorGUILayout.PropertyField(IsThereCancelButton_Prop);
        }
        else if ((ContentSO.ContentType)ContentType_Prop.enumValueIndex == ContentSO.ContentType.Description)
        {
            EditorGUILayout.PropertyField(ContentTemplate_Prop);

            EditorGUILayout.PropertyField(BackgroundImage_Prop);
            EditorGUILayout.PropertyField(BodyText_Prop);
            EditorGUILayout.PropertyField(CancelText_Prop);
        }
        else if ((ContentSO.ContentType)ContentType_Prop.enumValueIndex == ContentSO.ContentType.Action)
        {
            EditorGUILayout.PropertyField(ContentTemplate_Prop);

            EditorGUILayout.PropertyField(ActionTitle_Prop);
            EditorGUILayout.PropertyField(ActionSeal_Prop);
            EditorGUILayout.PropertyField(ActionRequestDiceSlots_Prop);
            EditorGUILayout.PropertyField(MultiValue_Prop);
            EditorGUILayout.PropertyField(ActionRewardText_Prop);
            EditorGUILayout.PropertyField(IsThereProceedButton_Prop);
        }

        serializedObject.ApplyModifiedProperties();
    }
}