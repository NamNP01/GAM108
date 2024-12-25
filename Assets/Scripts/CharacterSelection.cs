using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    private int index;
    [SerializeField] GameObject[] characters;
    [SerializeField] TextMeshProUGUI CharacterName;
    [SerializeField] GameObject[] CharacterPrefabs;

    void Start()
    {
        index = 0;
        SelectCharacter();
    }

    public void Play()
    {
        // Lưu chỉ số nhân vật được chọn trước khi chuyển cảnh
        CharacterSelectionData.SelectedCharacterIndex = index;
        SceneManager.LoadScene("Level 1");
    }

    public void OnPrevBtnClick()
    {
        if (index > 0)
        {
            index--;
        }
        SelectCharacter();
    }

    public void OnNextBtnClick()
    {
        if (index < characters.Length - 1)
        {
            index++;
        }
        SelectCharacter();
    }

    private void SelectCharacter()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == index)
            {
                characters[i].GetComponent<SpriteRenderer>().color = Color.white;
                characters[i].GetComponent<Animator>().enabled = true;
                CharacterName.text = CharacterPrefabs[i].name;
            }
            else
            {
                characters[i].GetComponent<SpriteRenderer>().color = Color.black;
                characters[i].GetComponent<Animator>().enabled = false;
            }
        }
    }
}
