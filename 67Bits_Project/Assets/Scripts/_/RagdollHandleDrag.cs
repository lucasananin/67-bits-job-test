using UnityEngine;

public class RagdollHandleDrag : MonoBehaviour
{
    public Rigidbody rootRigidbody;
    private SpringJoint joint;
    public Transform handleTarget;
    [SerializeField] ConfigurableJoint _cJoint = null;

    void Start()
    {
        //joint = rootRigidbody.gameObject.AddComponent<SpringJoint>();
        //joint.autoConfigureConnectedAnchor = false;
        //joint.connectedAnchor = handleTarget.position;
        //joint.spring = 500;
        //joint.damper = 50;

        ImpaleWithFlex(_cJoint);
    }

    //void Update()
    //{
    //    joint.connectedAnchor = handleTarget.position;
    //}

    public void ImpaleWithFlex(ConfigurableJoint joint)
    {
        // Lock all rotations (acts like a nail, no spin)
        joint.angularXMotion = ConfigurableJointMotion.Locked;
        joint.angularYMotion = ConfigurableJointMotion.Locked;
        joint.angularZMotion = ConfigurableJointMotion.Locked;

        // Allow position movement but limit it
        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Limited;
        joint.zMotion = ConfigurableJointMotion.Limited;

        // Soft pull-back behavior
        SoftJointLimitSpring spring = new SoftJointLimitSpring();
        spring.spring = 500f;   // How strong the pull is
        spring.damper = 50f;    // How smooth it returns

        joint.linearLimitSpring = spring;

        // Limit how far it can stretch
        SoftJointLimit limit = new SoftJointLimit();
        limit.limit = 0.1f; // Max stretch distance before pull

        joint.linearLimit = limit;

        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector3.zero;
        joint.connectedAnchor = joint.transform.InverseTransformPoint(joint.transform.position);
    }
}
