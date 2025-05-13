using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class FreezeKeyboard : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    // ���� Grab ���� Interactor ����Ʈ
    private HashSet<IXRSelectInteractor> activeInteractors = new HashSet<IXRSelectInteractor>();

    // �ƹ��� �� ��� ���� ��: ��ġ & ȸ�� ��� ����
    private readonly RigidbodyConstraints frozenConstraints =
        RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

    // ��� ���� ��: ��ġ�� �����Ӱ�, ȸ���� ����
    private readonly RigidbodyConstraints moveOnlyConstraints =
        RigidbodyConstraints.FreezeRotation;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        grabInteractable.selectEntered.AddListener(OnGrabStart);
        grabInteractable.selectExited.AddListener(OnGrabEnd);

        // ���� �� Freeze ���·�
        rb.constraints = frozenConstraints;
    }

    private void OnGrabStart(SelectEnterEventArgs args)
    {
        activeInteractors.Add(args.interactorObject);
        rb.constraints = moveOnlyConstraints;  // �̵��� ���, ȸ���� ����
    }

    private void OnGrabEnd(SelectExitEventArgs args)
    {
        activeInteractors.Remove(args.interactorObject);

        if (activeInteractors.Count == 0)
        {
            rb.constraints = frozenConstraints;  // �ƹ��� �� ��� ������ ��ü ����
        }
    }
}