using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Animator Anim;
    private bool isRagdoll = false; // ���ٲ�����״̬

    public List<Rigidbody> RagdollRigidbodys = new List<Rigidbody>();
    public List<Collider> RagdollColliders = new List<Collider>();

    void Start()
    {
        Anim = GetComponent<Animator>();
        InitRagdoll();
    }

    void Update()
    {
        // ʹ�� "R" ���л�������״̬
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleRagdollState();
        }
    }

    /// <summary>
    /// ��ʼ��������ϵͳ��������ɫ�� Rigidbody �� Collider
    /// </summary>
    void InitRagdoll()
    {
        Rigidbody[] Rigidbodys = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < Rigidbodys.Length; i++)
        {
            if (Rigidbodys[i] == GetComponent<Rigidbody>())
            {
                // �ų����ڵ�� Rigidbody
                continue;
            }

            // ��� Rigidbody �� Collider ���б�
            RagdollRigidbodys.Add(Rigidbodys[i]);
            Rigidbodys[i].isKinematic = true; // ����Ϊ�˶�ѧģʽ
            Collider RagdollCollider = Rigidbodys[i].gameObject.GetComponent<Collider>();
            RagdollCollider.isTrigger = true; // ����Ϊ������
            RagdollColliders.Add(RagdollCollider);
        }
    }

    /// <summary>
    /// ���ò�����ϵͳ
    /// </summary>
    public void EnableRagdoll()
    {
        // ������޵����� Rigidbody �� Collider
        for (int i = 0; i < RagdollRigidbodys.Count; i++)
        {
            RagdollRigidbodys[i].isKinematic = false;
            RagdollColliders[i].isTrigger = false;
        }

        // ��������״̬�� Collider
        GetComponent<Collider>().enabled = false;

        // ��һ֡���ö���ϵͳ
        StartCoroutine(SetAnimatorEnable(false));

        isRagdoll = true; // ����״̬Ϊ������
    }

    /// <summary>
    /// ���ò�����ϵͳ
    /// </summary>
    public void DisableRagdoll()
    {
        // ���ò����޵����� Rigidbody �� Collider
        for (int i = 0; i < RagdollRigidbodys.Count; i++)
        {
            RagdollRigidbodys[i].isKinematic = true;
            RagdollColliders[i].isTrigger = true;
        }

        // ��������״̬�� Collider
        GetComponent<Collider>().enabled = true;

        // ��һ֡���ö���ϵͳ
        StartCoroutine(SetAnimatorEnable(true));

        isRagdoll = false; // ����״̬Ϊ����
    }

    /// <summary>
    /// �л�������״̬
    /// </summary>
    public void ToggleRagdollState()
    {
        if (isRagdoll)
        {
            DisableRagdoll(); // �����ǰ�ǲ�����״̬���л�����״̬
        }
        else
        {
            EnableRagdoll(); // �����ǰ������״̬���л���������״̬
        }
    }

    /// <summary>
    /// ���ö���ϵͳ����/����״̬
    /// </summary>
    IEnumerator SetAnimatorEnable(bool Enable)
    {
        yield return new WaitForEndOfFrame(); // �ȴ�һ֡
        Anim.enabled = Enable;
    }
}
