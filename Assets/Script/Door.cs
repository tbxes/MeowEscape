using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (UIManager.Instance != null)
            {
                UIManager.Instance.TriggerStageClear();
            }
        }
    }
}