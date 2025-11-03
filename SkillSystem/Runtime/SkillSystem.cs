using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SkillSystem {
public class SkillSystem : MonoBehaviour {
    [Header("技能配置")]
    public SkillData skill_data;
    protected Unit user;
    protected bool is_cool;
    protected float timer_cooldown;

    private void Awake() {
        user = GetComponent<Unit>();
    }
    private void Update() {
        if (is_cool) {
            timer_cooldown -= Time.deltaTime;
            if (timer_cooldown <= 0) {
                is_cool = false;
            }
        }
    }

    //检测是否在技能范围内
    public bool IsSkillRange(Vector3 target_position) {
        float distance = Vector3.Distance(user.transform.position, target_position);
        return distance <= skill_data.skill_range;
    }
    //尝试释放技能
    public void TrySkill(SkillRequest request) {
        if (is_cool) return;
        if (user.power < skill_data.skill_cost) return;

        StartCoroutine(CastSkill(request));
    }
    private IEnumerator CastSkill(SkillRequest request) {
        Debug.Log("开始释放");
        //前摇
        if (skill_data.skill_cast_time > 0) {
            yield return new WaitForSeconds(skill_data.skill_cast_time);
        }

        //应用效果
        foreach (var e in skill_data.effect) {
            if (e.delay > 0) {
                yield return new WaitForSeconds(e.delay);
            }
            if (e.is_async) {
                yield return StartCoroutine(e.ApplyAsync(user, request.target_unit, request.target_position, skill_data));
            } else {
                SkillEffectApply.ApplyEffect(e, user, request.target_unit, request.target_position, skill_data);
            }
        }

        //消耗和冷却
        user.ModifyAttribute("power", -skill_data.skill_cost);
        StartCoolDown();
    }
    //进入冷却
    private void StartCoolDown() {
        is_cool = true;
        timer_cooldown = skill_data.skill_cooldown;
    }
}}