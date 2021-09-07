using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	public bool isFollow = false;
	// ��������ӳ�ʱ��
	public float smoothTime = 0.2f;
	// ����ͼƬ
	public RectTransform bg;
	// ��ɫ
	public Transform player;

	// ���ڻ��������
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
		// �������һ����
		float hight = mainCamera.orthographicSize;
		// �������һ��߶ȣ�����ֱ��ʵĿ�߱ȳ�����
		// �ø߶ȳ��Էֱ��ʵĿ�߱ȵõ����
		float width = hight * Screen.width / Screen.height;

		// Ҫ�ƶ�����λ��
		Vector3 temp;
		// ����ɫ�ĺ����� + �������һ���ȴ��ڱ���ͼƬ��һ���ȣ���Ϊ���˱߽磬��������Ŀ����Ϊ�ٽ��
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

		// ʵ����������ӳ��ƶ�
		transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, smoothTime);
	}
}
