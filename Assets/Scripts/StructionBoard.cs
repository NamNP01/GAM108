using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructionBoard : MonoBehaviour
{
    public GameObject BoxChat;

    // Start is called before the first frame update
    private void Start()
    {
        if (BoxChat != null)
        {
            BoxChat.SetActive(false); // Đảm bảo BoxChat bắt đầu ở trạng thái tắt
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && BoxChat != null)
        {
            BoxChat.SetActive(true); // Hiển thị BoxChat khi người chơi chạm vào
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && BoxChat != null)
        {
            BoxChat.SetActive(false); // Tắt BoxChat khi người chơi rời đi
        }
    }
}
