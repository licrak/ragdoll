using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Animator Anim;
    private bool isRagdoll = false; // 跟踪布娃娃状态

    public List<Rigidbody> RagdollRigidbodys = new List<Rigidbody>();
    public List<Collider> RagdollColliders = new List<Collider>();

    void Start()
    {
        Anim = GetComponent<Animator>();
        InitRagdoll();
    }

    void Update()
    {
        // 使用 "R" 键切换布娃娃状态
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleRagdollState();
        }
    }

    /// <summary>
    /// 初始化布娃娃系统，遍历角色的 Rigidbody 和 Collider
    /// </summary>
    void InitRagdoll()
    {
        Rigidbody[] Rigidbodys = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < Rigidbodys.Length; i++)
        {
            if (Rigidbodys[i] == GetComponent<Rigidbody>())
            {
                // 排除根节点的 Rigidbody
                continue;
            }

            // 添加 Rigidbody 和 Collider 到列表
            RagdollRigidbodys.Add(Rigidbodys[i]);
            Rigidbodys[i].isKinematic = true; // 设置为运动学模式
            Collider RagdollCollider = Rigidbodys[i].gameObject.GetComponent<Collider>();
            RagdollCollider.isTrigger = true; // 设置为触发器
            RagdollColliders.Add(RagdollCollider);
        }
    }

    /// <summary>
    /// 启用布娃娃系统
    /// </summary>
    public void EnableRagdoll()
    {
        // 激活布娃娃的所有 Rigidbody 和 Collider
        for (int i = 0; i < RagdollRigidbodys.Count; i++)
        {
            RagdollRigidbodys[i].isKinematic = false;
            RagdollColliders[i].isTrigger = false;
        }

        // 禁用正常状态的 Collider
        GetComponent<Collider>().enabled = false;

        // 下一帧禁用动画系统
        StartCoroutine(SetAnimatorEnable(false));

        isRagdoll = true; // 更新状态为布娃娃
    }

    /// <summary>
    /// 禁用布娃娃系统
    /// </summary>
    public void DisableRagdoll()
    {
        // 禁用布娃娃的所有 Rigidbody 和 Collider
        for (int i = 0; i < RagdollRigidbodys.Count; i++)
        {
            RagdollRigidbodys[i].isKinematic = true;
            RagdollColliders[i].isTrigger = true;
        }

        // 启用正常状态的 Collider
        GetComponent<Collider>().enabled = true;

        // 下一帧启用动画系统
        StartCoroutine(SetAnimatorEnable(true));

        isRagdoll = false; // 更新状态为正常
    }

    /// <summary>
    /// 切换布娃娃状态
    /// </summary>
    public void ToggleRagdollState()
    {
        if (isRagdoll)
        {
            DisableRagdoll(); // 如果当前是布娃娃状态，切回正常状态
        }
        else
        {
            EnableRagdoll(); // 如果当前是正常状态，切换到布娃娃状态
        }
    }

    /// <summary>
    /// 设置动画系统启用/禁用状态
    /// </summary>
    IEnumerator SetAnimatorEnable(bool Enable)
    {
        yield return new WaitForEndOfFrame(); // 等待一帧
        Anim.enabled = Enable;
    }
}
