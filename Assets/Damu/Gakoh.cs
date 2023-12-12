using UnityEngine;

public class Gakoh : MonoBehaviour
{
    public Faonshian person1; // 定义第一个人
    public Faonshian person2; // 定义第二个人

    public float angle; // 定义两个人朝向的夹角

    // Update方法在每一帧更新时调用
    void Update()
    {
        // 计算两个人朝向的夹角
        float angle = Vector3.Angle(person1.direction, person2.direction);

        // 输出夹角
        Debug.Log("两人朝向的夹角为: " + angle);
    }
}