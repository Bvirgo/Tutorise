using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 1、拆分Grid为多个chunck
/// 2、每个chunck单独管理：mesh
/// </summary>
public class HexGridChunk : MonoBehaviour {

	HexCell[] cells;

	HexMesh hexMesh;
	Canvas gridCanvas;

	void Awake () {
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();

		cells = new HexCell[HexMetrics.chunkSizeX * HexMetrics.chunkSizeZ];
		ShowUI(false);
	}

	public void AddCell (int index, HexCell cell) {
		cells[index] = cell;
		cell.chunk = this;
		cell.transform.SetParent(transform, false);
		cell.uiRect.SetParent(gridCanvas.transform, false);
	}

    /// <summary>
    /// 如果同一帧中改变同属一个chunck的多个cell，就会调用多次mesh计算，没必要！
    /// 优化：
    /// 把chunck的刷新，延时到LateUpdate中去执行一次
    /// </summary>
	public void Refresh () {
		enabled = true;
	}

	public void ShowUI (bool visible) {
		gridCanvas.gameObject.SetActive(visible);
	}

    /// <summary>
    /// 只有enabled is True 才会被调用
    /// </summary>
	void LateUpdate () {
		hexMesh.Triangulate(cells);
		enabled = false;
	}
}