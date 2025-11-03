using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill {
public abstract class SkillEffect : ScriptableObject {
    [Header("基础设置")]
    public EffectType effect_type = EffectType.Direct;
    public float delay = 0f;
    public bool is_async = false;

    [Header("目标设置")]
    public RelationshipType target_allow = RelationshipType.Hostile;

    //应用效果
    public abstract void Apply(Unit user, Unit target, Vector3 target_position, SkillData skill_data);
    //异步应用效果
    public virtual IEnumerator ApplyAsync(Unit user, Unit target, Vector3 target_position, SkillData skill_data) {
        Apply(user, target, target_position, skill_data);
        yield break;//默认立即完成
    }
    //影响目标
    public bool CanAffectTarget(Unit user, Unit target) {
        if (target == null) return false;

        switch (target_allow) {
            case RelationshipType.Friendly:
                return user.team_ID == target.team_ID;
            case RelationshipType.Hostile:
                return user.team_ID != target.team_ID;
            case RelationshipType.Neutral:
                return true;
            default:
                return false;
        }
    }
}}