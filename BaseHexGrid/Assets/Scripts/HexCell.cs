using UnityEngine;

public class HexCell : MonoBehaviour {

    // 轴向坐标系统：基于六角形网格的坐标系统，用于定义六个方向的移动，和检索
	public HexCoordinates coordinates;

    // 六边形颜色，通过重建mesh，重写shader体现
	public Color color;
}