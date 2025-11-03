using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem {
[CreateAssetMenu(fileName = "Skill", menuName = "New Skill/Skill Effect/MoveEffect")]
public class MoveEffect : SkillEffect {
    [Header("ª˜ÕÀ…Ë÷√")]
    public float force = 10f;
    public ForceMode forceMode = ForceMode.Impulse;

    public override void Apply(Unit user, Unit target, Vector3 target_position, SkillData skill_data) {
        if (!CanAffectTarget(user, target)) return;

        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb != null) {
            Vector3 dir = (target.transform.position - user.transform.position).normalized;
            rb.AddForce(dir * force, forceMode);
        }
    }
}}
