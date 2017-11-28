using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int width = 6;
	public int height = 6;

	public Color defaultColor = Color.white;

	public HexCell cellPrefab;
	public Text cellLabelPrefab;

	HexCell[] cells;

	Canvas gridCanvas;
	HexMesh hexMesh;

	void Awake () {
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();

		cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++) {
			for (int x = 0; x < width; x++) {
				CreateCell(x, z, i++);
			}
		}
	}

	void Start () {
		hexMesh.Triangulate(cells);
	}

	public void ColorCell (Vector3 position, Color color) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		HexCell cell = cells[index];
		cell.color = color;
		hexMesh.Triangulate(cells);
	}

    /// <summary>
    /// 创建Hex单元格
    /// 1、摆正位置
    /// 2、建立轴向坐标
    /// 3、邻里关联
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="i"></param>
	void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
		cell.color = defaultColor;

        /*
         * 为什么不用特殊处理，第一行，第一列，或者最后一行最后一列呢？
         * 因为，关联是相互的，当在处理他们的邻里的时候，就已经反向实现了他们自身的邻里连接
         */
        // 邻居关联:第二列开始,都有西方邻居
		if (x > 0)
        {
			cell.SetNeighbor(HexDirection.W, cells[i - 1]);
		}
		if (z > 0) // 第二行开始
        {
			if ((z & 1) == 0) // 偶数行
            {
                // 肯定有东南邻居
				cell.SetNeighbor(HexDirection.SE, cells[i - width]);
				if (x > 0) // 除第一列外:都有西南邻居
                {
					cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
				}
			}
			else // 奇数行
            {
                // 肯定有西南邻居
				cell.SetNeighbor(HexDirection.SW, cells[i - width]);

                // 不是最外行，有东南邻居
				if (x < width - 1) 
                {
					cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
				}
			}
		}

		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();
	}
}