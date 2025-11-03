using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem {
    [CreateAssetMenu(fileName = "Skill", menuName = "New Skill/Skill Data", order = 1)]
public class SkillData : ScriptableObject {
    public int skill_ID;

    [Header("基本信息")]
    public Sprite skill_icon;
    public string skill_name;
    [TextArea(1, 8)]
    public string skill_description;

    [Header("技能参数")]
    public float skill_range = 5f;
    public float skill_cost = 10f;
    public float skill_cast_time = 1f;
    public float skill_damage = 10f;
    public float skill_cooldown = 1f;

    [Header("目标设置")]
    public RelationshipType relationship_type;

    [Header("音效")]
    public AudioClip sound;

    [Header("效果组件")]
    public List<SkillEffect> effect = new List<SkillEffect>();
}}