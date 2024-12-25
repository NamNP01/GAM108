using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject[] playerCharacters; // Chỉ định tất cả các nhân vật có thể sử dụng trong Scene
    private int selectedCharacterIndex = -1; // Chỉ số của nhân vật được chọn

    void Start()
    {
        selectedCharacterIndex = CharacterSelectionData.SelectedCharacterIndex;

        // Đảm bảo chỉ số nhân vật được chọn là hợp lệ
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < playerCharacters.Length)
        {
            // Lặp qua tất cả các nhân vật trong Scene
            for (int i = 0; i < playerCharacters.Length; i++)
            {
                if (i == selectedCharacterIndex)
                {
                    // Hiển thị nhân vật được chọn
                    playerCharacters[i].SetActive(true);
                }
                else
                {
                    // Hủy nhân vật không được chọn
                    Destroy(playerCharacters[i]);
                }
            }
        }
        else
        {
            Debug.LogWarning("SelectedCharacterIndex out of range or not initialized.");
        }
    }
}
