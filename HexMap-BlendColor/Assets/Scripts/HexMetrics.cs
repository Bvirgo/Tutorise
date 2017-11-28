using UnityEngine;

public static class HexMetrics {

	public const float outerRadius = 10f;

	public const float innerRadius = outerRadius * 0.866025404f;

    // 纯色区域(内核)比重
	public const float solidFactor = 0.75f;

    // 颜色混合区域（混合区域）比重
	public const float blendFactor = 1f - solidFactor;

    /// <summary>
    /// 六个顶点，相对于Hex单元中心点的偏移量
    /// </summary>
	static Vector3[] corners = {
		new Vector3(0f, 0f, outerRadius),
		new Vector3(innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(0f, 0f, -outerRadius),
		new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(0f, 0f, outerRadius)
	};

    /// <summary>
    /// 这个方向上的第一个顶点
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
	public static Vector3 GetFirstCorner (HexDirection direction) {
		return corners[(int)direction];
	}
    /// <summary>
    /// 这个方向上的第二个顶点
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
	public static Vector3 GetSecondCorner (HexDirection direction) {
		return corners[(int)direction + 1];
	}

    /// <summary>
    /// 指定方向上，第一个内核顶点
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
	public static Vector3 GetFirstSolidCorner (HexDirection direction) {
		return corners[(int)direction] * solidFactor;
	}

    /// <summary>
    /// 指定方向上，第二个内核顶点
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
	public static Vector3 GetSecondSolidCorner (HexDirection direction) {
		return corners[(int)direction + 1] * solidFactor;
	}

    /// <summary>
    /// 获取指定方向，两个邻里的桥：
    /// 桥的方向是：两个邻里之间的中心点连线方向
    /// 桥的长度：内核边 到 六角形边的距离
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
	public static Vector3 GetBridge (HexDirection direction) {
		return (corners[(int)direction] + corners[(int)direction + 1]) *
			blendFactor;
	}
}