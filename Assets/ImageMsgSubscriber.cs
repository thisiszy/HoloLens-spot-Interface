using RosMessageTypes.Sensor;
using Unity.Robotics.ROSTCPConnector;
using UnityEngine;
using UnityEngine.UI;

public class ImageMsgSubscriber : MonoBehaviour
{

    // Variables required for ROS communication
    [SerializeField]
    string m_TopicName = "/rgb/image_raw";

    // ROS Connector
    ROSConnection m_Ros;
    public GameObject m_screen;
    public RawImage m_image;
    private Texture2D tex = null;
    private byte[] img_data;
    private bool is_msg_valid = false;

    void Start()
    {
        // Get ROS connection static instance
        m_Ros = ROSConnection.GetOrCreateInstance();
        m_Ros.Subscribe<CompressedImageMsg>(m_TopicName, ImageMsgArrive);
        m_image = m_screen.GetComponent<RawImage>();
        is_msg_valid = false;
        tex = new Texture2D(1, 1);
    }

    private void ImageMsgArrive(CompressedImageMsg img_msg)
    {
        /*        tex = new Texture2D((int)img_msg.width, (int)img_msg.height, TextureFormat.RGB24, false);*/
        if (!is_msg_valid)
        {
            img_data = img_msg.data;
            is_msg_valid = true;
        }
    }

    void Update()
    {
        if (is_msg_valid)
        {
            tex.LoadImage(img_data);
            tex.Apply();
            m_image.texture = tex;
            is_msg_valid = false;
        }
    }
}
