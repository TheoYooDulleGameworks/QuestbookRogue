using UnityEditor;

[CustomEditor(typeof(ContentSO))]
public class ContentSOEditor : Editor
{
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

    SerializedProperty ActionImage_Prop;
    SerializedProperty ActionBelt_Prop;
    SerializedProperty ActionTitle_Prop;
    SerializedProperty ActionRequestSlots1Row_Prop;
    SerializedProperty ActionRequestSlots2Row_Prop;
    SerializedProperty ActionRewardText_Prop;
    SerializedProperty IsThereProceedButton_Prop;
    SerializedProperty IsFreeAction_Prop;
    SerializedProperty CombatOptionSet_Prop;

    SerializedProperty RewardBelt_Prop;
    SerializedProperty RewardTitle_Prop;
    SerializedProperty RewardObjects_Prop;

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

        ActionImage_Prop = serializedObject.FindProperty("actionImage");
        ActionBelt_Prop = serializedObject.FindProperty("actionBelt");
        ActionTitle_Prop = serializedObject.FindProperty("actionTitle");
        ActionRequestSlots1Row_Prop = serializedObject.FindProperty("actionRequestSlots1Row");
        ActionRequestSlots2Row_Prop = serializedObject.FindProperty("actionRequestSlots2Row");
        ActionRewardText_Prop = serializedObject.FindProperty("actionRewardText");
        IsThereProceedButton_Prop = serializedObject.FindProperty("isThereProceedButton");
        IsFreeAction_Prop = serializedObject.FindProperty("isFreeAction");
        CombatOptionSet_Prop = serializedObject.FindProperty("combatOptionSet");

        RewardBelt_Prop = serializedObject.FindProperty("rewardBelt");
        RewardTitle_Prop = serializedObject.FindProperty("rewardTitle");
        RewardObjects_Prop = serializedObject.FindProperty("rewardObjects");
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
            EditorGUILayout.PropertyField(QuestTitle_Prop);
            EditorGUILayout.PropertyField(QuestImage_Prop);
            EditorGUILayout.PropertyField(QuestSeal_Prop);
            EditorGUILayout.PropertyField(IsThereCancelButton_Prop);
            EditorGUILayout.PropertyField(IsFreeCancel_Prop);
        }
        else if ((ContentSO.ContentType)ContentType_Prop.enumValueIndex == ContentSO.ContentType.Description)
        {
            EditorGUILayout.PropertyField(BackgroundImage_Prop);

            EditorGUILayout.PropertyField(BodyText_Prop);
            EditorGUILayout.PropertyField(CancelText_Prop);
        }
        else if ((ContentSO.ContentType)ContentType_Prop.enumValueIndex == ContentSO.ContentType.Action)
        {
            EditorGUILayout.PropertyField(BackgroundImage_Prop);

            EditorGUILayout.PropertyField(ActionImage_Prop);
            EditorGUILayout.PropertyField(ActionBelt_Prop);
            EditorGUILayout.PropertyField(ActionTitle_Prop);
            EditorGUILayout.PropertyField(ActionRequestSlots1Row_Prop);
            EditorGUILayout.PropertyField(ActionRequestSlots2Row_Prop);
            EditorGUILayout.PropertyField(ActionRewardText_Prop);
            EditorGUILayout.PropertyField(IsThereProceedButton_Prop);
            EditorGUILayout.PropertyField(IsFreeAction_Prop);
            EditorGUILayout.PropertyField(CombatOptionSet_Prop);

            EditorGUILayout.PropertyField(RewardBelt_Prop);
            EditorGUILayout.PropertyField(RewardTitle_Prop);
            EditorGUILayout.PropertyField(RewardObjects_Prop);
        }

        serializedObject.ApplyModifiedProperties();
    }
}