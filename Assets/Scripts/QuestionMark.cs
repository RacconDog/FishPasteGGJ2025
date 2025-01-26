using UnityEngine;

public class QuestionMark : MonoBehaviour
{
    public GameObject follow;

    void Start()
    {
        Destroy(gameObject, 1);
    }

    void Update()
    {
        transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y + 2f, 0);
    }
}
