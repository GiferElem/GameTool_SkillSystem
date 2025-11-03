using System;
using UnityEngine;

namespace SkillSystem {
public class ProjectileController : MonoBehaviour {
    private Action on_complete;
    private Unit user;
    private SkillData skill_data;
    private ProjectileEffect effect;
    private Vector3 target_position;
    private Vector3 start_position;
    private float speed;
    private float fly_time;
    private float current_time;
    private bool is_hit = false;

    //常量
    private const float TIME_AUTO_DESTORY = 10f;
    private const float DISTANCE_ARRIVE = 0.5f;
    private const float TIME_FUTURE = 0.05f;
    public void Initialize(Unit user, SkillData skill_data, ProjectileEffect effect, Vector3 target_position, float speed, Action on_complete = null) {
        this.user = user;
        this.skill_data = skill_data;
        this.effect = effect;
        this.target_position = target_position;
        this.start_position = transform.position;
        this.speed = speed;
        this.on_complete = on_complete;

        TurnSet();
        //销毁投掷物
        Destroy(gameObject, TIME_AUTO_DESTORY);
    }

    private void FixedUpdate() {
        if (is_hit) return;

        current_time += Time.fixedDeltaTime;
        Move();
        if (CheckArrival()) {
            Arrive();
        }
    }
    /// <summary>
    /// TurnSet
    /// </summary>
    //根据发射方式设置朝向
    private void TurnSet() {
        switch (effect.launch_type) {
            case LaunchType.Straight:
                TurnSet_Straight();
                break;
            case LaunchType.Parabola:
                TurnSet_Parabola();
                break;
        }
    }
    private void TurnSet_Straight() {
        //朝前
        Vector3 dir = (target_position - transform.position).normalized;
        transform.forward = dir;
    }
    private void TurnSet_Parabola() {
        //飞行时间
        Vector3 dir = target_position - start_position;
        fly_time = dir.magnitude / speed;
        //面向水平方向
        dir.y = 0;
        transform.forward = dir.normalized;
    }
    /// <summary>
    /// Move
    /// </summary>
    private void Move() {
        switch (effect.launch_type) {
            case LaunchType.Straight:
                Move_Straight();
                break;
            case LaunchType.Parabola:
                Move_Parabola();
                break;
        }
    }
    private void Move_Straight() {
        //向前移动
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void Move_Parabola() {
        //当前飞行位置
        float t = current_time / fly_time;
        Vector3 current_pos = Vector3.Lerp(start_position, target_position, t);
        //正弦函数模拟抛物线垂直高度变化
        float h = effect.height * Mathf.Sin(t * Mathf.PI);
        current_pos.y += h;

        transform.position = current_pos;
        //面向运动方向
        if (t < 0.95f) {
            Vector3 next_pos = Vector3.Lerp(start_position, target_position, t + TIME_FUTURE);
            next_pos.y += effect.height * Mathf.Sin((t + 0.05f) * Mathf.PI);
            transform.forward = (next_pos - transform.position).normalized;
        }
    }
    //检测是否到达目标处
    private bool CheckArrival() {
        switch (effect.launch_type) {
            case LaunchType.Straight:
                //Straight:检查距离
                return Vector3.Distance(transform.position, target_position) < DISTANCE_ARRIVE;
            case LaunchType.Parabola:
                //Parabola：检查时间
                return current_time >= fly_time;
            default:
                return false;
        }
    }
    //到达目标处
    private void Arrive() {
        if (is_hit) return;
        is_hit = true;

        //区域效果应用
        if (effect.effect_type == EffectType.Area) {
            effect.Apply(user, null, target_position, skill_data);
        }
        //完成回调
        CompleteAndDestroy();
    }
    //触碰目标
    private void OnTriggerEnter(Collider other) {
        if (is_hit) return;

        Unit target = other.GetComponent<Unit>();
        if (target != null && target != user) {
            //应用效果到命中的目标
            if (effect.CanAffectTarget(user, target)) {
                effect.Apply(user, target, target_position, skill_data);
                is_hit = true;

                //完成回调
                CompleteAndDestroy();
            }
        }
        //有触碰不是目标提前销毁（墙体或地面）
        else if (target == null) {
            //完成回调
            CompleteAndDestroy();
        }
    }
    //回调和销毁
    private void CompleteAndDestroy() {
        //完成回调
        on_complete?.Invoke();
        //销毁投掷物
        Destroy(gameObject);
    }
}}