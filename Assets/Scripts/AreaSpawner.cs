using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] areaPrefabs; //���� ������ �迭
    [SerializeField]
    private int spawnAreaCountAtStart = 3; //���� ���� �� ���� �����Ǵ� ���� ����
    [SerializeField]
    private float zDistance = 20; //���� ������ �Ÿ�(z)
    private int areaIndex = 0; //���� �ε���(��ġ�Ǵ� ������ z ��ġ ���꿡 ���)

    [SerializeField]
    private Transform playerTransform; //�÷��̾� Transform

    private void Awake()
    {
        //spawnAreaCountAtStart�� ����� ������ŭ ���� ���� ����
        for(int i =0; i<spawnAreaCountAtStart; ++i)
        {
            //ù ���� ������ �׻� 0�� ���� ���������� ����
            if(i == 0)
            {
                SpawnArea(false);
            }
            else
            {
                SpawnArea();
            }
        }
    }

    public void SpawnArea(bool isRandom = true)
    {
        GameObject clone = null;

        if(isRandom == false)
        {
            clone = Instantiate(areaPrefabs[0]);
        }
        else
        {
            int index = Random.Range(0, areaPrefabs.Length);
            clone = Instantiate(areaPrefabs[index]);
        }

        //������ ��ġ�Ǵ� ��ġ ���� (z���� ���� ���� �ε���*zDistance);
        clone.transform.position = new Vector3(0, 0, areaIndex * zDistance);
        //������ ������ �� ���ο� ������ ������ �� �ֵ��� this�� �÷��̾��� Transform ���� ����
        clone.GetComponent<Area>().Setup(this, playerTransform);
        areaIndex++;
    }
}
