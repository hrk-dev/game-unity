using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	public bool isFollow = false;
	// 摄像机的延迟时间
	public float smoothTime = 0.2f;
	// 背景图片
	public RectTransform bg;
	// 角色
	public Transform player;

	// 用于缓冲摄像机
	private Vector3 velocity = Vector3.one;
	private Camera mainCamera;
	private float bgWidth;
	private float bgHeight;

	void Start()
	{
		if (!isFollow) return;
		bg = GameObject.Find("Map").GetComponent<RectTransform>();
		player = GameObject.FindObjectOfType<Player>().transform;

		mainCamera = GetComponent<Camera>();
		bgWidth = bg.sizeDelta.x * bg.transform.localScale.x;
		bgHeight = bg.sizeDelta.y * bg.transform.localScale.y;
	}

	void LateUpdate()
	{
		if (!isFollow) return;
		// 摄像机的一半宽度
		float hight = mainCamera.orthographicSize;
		// 摄像机的一半高度，会随分辨率的宽高比成正比
		// 用高度乘以分辨率的宽高比得到宽度
		float width = hight * Screen.width / Screen.height;

		// 要移动到的位置
		Vector3 temp;
		// 当角色的横坐标 + 摄像机的一半宽度大于背景图片的一半宽度，则为到了边界，把摄像机的宽度设为临界点
		float x = player.position.x;
		float y = player.position.y;
		if (width + Mathf.Abs(player.position.x) > bgWidth / 2)
		{
			x = Mathf.Sign(player.position.x) * (bgWidth / 2 - width);
		}
		if (hight + Mathf.Abs(player.position.y) > bgHeight / 2)
		{
			y = Mathf.Sign(player.position.y) * (bgHeight / 2 - hight);
		}
		temp = new Vector3(x, y, transform.position.z);

		// 实现摄像机的延迟移动
		transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, smoothTime);
	}
}
