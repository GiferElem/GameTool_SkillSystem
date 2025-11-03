using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Skill {
public class SkillRequest {
    public Vector3 target_position;
    public Unit target_unit;

    public SkillRequest(Vector3 pos, Unit unit = null) {
        target_position = pos;
        target_unit = unit;
    }
    //从鼠标位置创建请求
    public static SkillRequest FromMousePos() {
        Vector2 mouse_position = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mouse_position.x, mouse_position.y, 0));
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            Unit target = hit.collider.GetComponent<Unit>();
            return new SkillRequest(hit.point, target);
        }
        return new SkillRequest(ray.GetPoint(10f));
    }
}}