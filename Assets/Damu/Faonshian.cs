using UnityEngine;

public class Faonshian : MonoBehaviour
{
    public Transform leftShoulder; // 定义左肩的游戏对象
    public Transform rightShoulder; // 定义右肩的游戏对象
    public Vector3 direction; // 定义朝向

    

    // Update方法在每一帧更新时调用
    void Update()
    {
        Vector3 midpoint = (rightShoulder.position + leftShoulder.position) / 2; // 计算左肩和右肩的中点
        direction = -transform.forward; // 获取中点前方的单位向量
        direction.y = 0; // 忽略y轴，即忽略高度因素
        direction = direction.normalized; // 将方向向量转化为单位向量
        Debug.DrawRay(midpoint, direction, Color.red); // 在中点处绘制方向向量
    }
}