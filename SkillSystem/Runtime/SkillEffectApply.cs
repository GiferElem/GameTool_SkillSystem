using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem {
public static class SkillEffectApply {
    //应用效果
    public static void ApplyEffect(SkillEffect effect, Unit user, Unit target, Vector3 target_position, SkillData skill_data) {
        switch (effect.effect_type) {
            case EffectType.Direct:
                ApplyEffect_Direct(effect, user, target, target_position, skill_data);
                break;
            case EffectType.Projectile:
                ApplyEffect_Projectile(effect, user, target, target_position, skill_data);
                break;
            case EffectType.Area:
                ApplyEffect_Area(effect, user, target_position, skill_data);
                break;
        }
    }
    //异步应用效果
    public static IEnumerator ApplyEffectAsync(SkillEffect effect, Unit user, Unit target, Vector3 target_position, SkillData skill_data) {
        if (effect.is_async) {
            yield return effect.ApplyAsync(user, target, target_position, skill_data);
        } else {
            ApplyEffect(effect, user, target, target_position, skill_data);
            yield return null;
        }
    }
    //应用效果:具体效果类型
    private static void ApplyEffect_Direct(SkillEffect effect, Unit user, Unit target, Vector3 target_position, SkillData skill_data) {
        //效果类型: Direct
        if (target != null && effect.CanAffectTarget(user, target)) {
            effect.Apply(user, target, target_position, skill_data);
        }
    }
    private static void ApplyEffect_Projectile(SkillEffect effect, Unit user, Unit target, Vector3 target_position, SkillData skill_data) {
        //效果类型: Projectile
        //ProjectileEffect
    }
    private static void ApplyEffect_Area(SkillEffect effect, Unit user, Vector3 target_position, SkillData skill_data) {
        //效果类型: 区域效果
        //1.寻找范围内的碰撞体
        Collider[] colliders = Physics.OverlapSphere(target_position, skill_data.skill_range);
        foreach (Collider col in colliders) {
            //2.找到碰撞体所在物体的Unit组件
            Unit target = col.GetComponent<Unit>();
            //3.应用效果
            if (target != null && effect.CanAffectTarget(user, target)) {
                effect.Apply(user, target, target_position, skill_data);
            }
        }
    }
}}
