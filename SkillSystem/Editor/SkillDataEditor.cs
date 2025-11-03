using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillData))]
public class SkillDataEditor : Editor{
    public override void OnInspectorGUI() {
        //1.绘制默认界面
        DrawDefaultInspector();
        //2.获取正在查看的技能
        SkillData skillData = (SkillData)target;
        //3.加入备注信息（关系运行时创建）
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("关系类型决定对谁生效：\n- Hostile：只对敌人生效\n- Friendly：只对友人生效\n" +
            "- Neutral：对敌人和友人都生效", MessageType.Info);

        //4.如果选择了新的关系，保存修改
        if (GUI.changed) {
            EditorUtility.SetDirty(skillData);
        }
    }
}