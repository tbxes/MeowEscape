using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 🔊 หยุดเพลง BGM และเล่นเสียงชนะ/หนีรอด
            if (Scene01Audio.Instance != null)
            {
                Scene01Audio.Instance.StopBGM();
                Scene01Audio.Instance.PlayVictory();
            }

            if (UIManager.Instance != null)
            {
                UIManager.Instance.TriggerStageClear();
            }
        }
    }
}