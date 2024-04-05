using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private GameObject coinEffectPrefab;
    private float rotateSpeed;

    private void Awake()
    {
        rotateSpeed = Random.Range(0, 360);
    }

    private void Update()
    {
        //���� ������Ʈ ȸ��
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //���� ������Ʈ ȹ�� ȿ��(coinEffectPrefab) ����
        GameObject clone = Instantiate(coinEffectPrefab);
        clone.transform.position = transform.position;

        //���� ������Ʈ ����
        Destroy(gameObject);
    }
}
