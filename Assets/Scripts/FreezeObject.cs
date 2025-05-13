using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
public class FreezeObject : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    // ���� Grab ���� Interactor ����Ʈ
    private HashSet<IXRSelectInteractor> activeInteractors = new HashSet<IXRSelectInteractor>();

    // �⺻ Freeze ���� (��� ���� ���� �� ����)
    private readonly RigidbodyConstraints frozenConstraints =
        RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

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
        rb.constraints = RigidbodyConstraints.None;  // ���� ����
    }

    private void OnGrabEnd(SelectExitEventArgs args)
    {
        activeInteractors.Remove(args.interactorObject);

        if (activeInteractors.Count == 0)
        {
            rb.constraints = frozenConstraints;  // �ƹ��� �� ��� ������ �ٽ� Freeze
        }
    }
}