using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using RosMessageTypes.Geometry;
using Unity.Robotics.ROSTCPConnector;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Utilities;

public class cube_btn : MonoBehaviour, IMixedRealitySourcePoseHandler
{

    public GameObject Cube;

    // Variables required for ROS communication
    [SerializeField]
    string m_TopicName = "/cmd_vel";

    // ROS Connector
    ROSConnection m_Ros;

    void Start()
    {
        // Get ROS connection static instance
        m_Ros = ROSConnection.GetOrCreateInstance();
        m_Ros.RegisterPublisher<TwistMsg>(m_TopicName);
    }

    public void Publish(float a_z)
    {
        var twist = new TwistMsg();
        twist.angular = new Vector3Msg(0.0f, 0.0f, a_z);
        twist.linear = new Vector3Msg(0.0f, 0.0f, 0.0f);
        // Finally send the message to server_endpoint.py running in ROS
        m_Ros.Publish(m_TopicName, twist);
    }

    // Update is called once per frame
    void Update()
    {
/*        Publish();*/
    }

    public void OnSourcePoseChanged(SourcePoseEventData<TrackingState> eventData)
    {
        Publish(0.03f);
    }

    public void OnSourcePoseChanged(SourcePoseEventData<Vector2> eventData)
    {
        Publish(0.04f);
    }

    public void OnSourcePoseChanged(SourcePoseEventData<Vector3> eventData)
    {
        Publish(0.05f);
    }

    public void OnSourcePoseChanged(SourcePoseEventData<Quaternion> eventData)
    {
        Publish(0.06f);
    }

    public void OnSourcePoseChanged(SourcePoseEventData<MixedRealityPose> eventData)
    {
        Publish(0.07f);
    }

    public void OnSourceDetected(SourceStateEventData eventData)
    {
        Publish(0.08f);
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        Publish(0.09f);
    }
}
