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

    SerializedProperty BackgroundImage_Prop;

    SerializedProperty QuestTitle_Prop;
    SerializedProperty QuestImage_Prop;
    SerializedProperty QuestSeal_Prop;
    SerializedProperty IsThereCancelButton_Prop;
    SerializedProperty IsFreeCancel_Prop;

    SerializedProperty BodyText_Prop;
    SerializedProperty CancelText_Prop;

    SerializedProperty ActionTitle_Prop;
    SerializedProperty ActionSeal_Prop;
    SerializedProperty ActionRequestDiceSlots_Prop;
    SerializedProperty MultiValue_Prop;
    SerializedProperty ActionRewardText_Prop;
    SerializedProperty RewardContents_Prop;
    SerializedProperty IsThereProceedButton_Prop;

    SerializedProperty ConclusionTitle_Prop;
    SerializedProperty ConclusionSeal_Prop;
    SerializedProperty ConclusionText_Prop;

    private void OnEnable()
    {
        ContentType_Prop = serializedObject.FindProperty("contentType");

        ContentTemplate_Prop = serializedObject.FindProperty("contentTemplate");

        BackgroundImage_Prop = serializedObject.FindProperty("backgroundImage");

        QuestTitle_Prop = serializedObject.FindProperty("questTitle");
        QuestImage_Prop = serializedObject.FindProperty("questImage");
        QuestSeal_Prop = serializedObject.FindProperty("questSeal");
        IsThereCancelButton_Prop = serializedObject.FindProperty("isThereCancelButton");
        IsFreeCancel_Prop = serializedObject.FindProperty("isFreeCancel");

        BodyText_Prop = serializedObject.FindProperty("bodyText");
        CancelText_Prop = serializedObject.FindProperty("cancelText");

        ActionTitle_Prop = serializedObject.FindProperty("actionTitle");
        ActionSeal_Prop = serializedObject.FindProperty("actionSeal");
        ActionRequestDiceSlots_Prop = serializedObject.FindProperty("actionRequestDiceSlots");
        MultiValue_Prop = serializedObject.FindProperty("multiValue");
        ActionRewardText_Prop = serializedObject.FindProperty("actionRewardText");
        RewardContents_Prop = serializedObject.FindProperty("rewardContents");
        IsThereProceedButton_Prop = serializedObject.FindProperty("isThereProceedButton");

        ConclusionTitle_Prop = serializedObject.FindProperty("conclusionTitle");
        ConclusionSeal_Prop = serializedObject.FindProperty("conclusionSeal");
        ConclusionText_Prop = serializedObject.FindProperty("conclusionText");
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
            EditorGUILayout.PropertyField(IsFreeCancel_Prop);
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
            EditorGUILayout.PropertyField(RewardContents_Prop);
            EditorGUILayout.PropertyField(IsThereProceedButton_Prop);
        }
        else if ((ContentSO.ContentType)ContentType_Prop.enumValueIndex == ContentSO.ContentType.Conclusion)
        {
            EditorGUILayout.PropertyField(ContentTemplate_Prop);
            
            EditorGUILayout.PropertyField(BackgroundImage_Prop);
            
            EditorGUILayout.PropertyField(ConclusionTitle_Prop);
            EditorGUILayout.PropertyField(ConclusionSeal_Prop);
            EditorGUILayout.PropertyField(ConclusionText_Prop);
        }

        serializedObject.ApplyModifiedProperties();
    }
}