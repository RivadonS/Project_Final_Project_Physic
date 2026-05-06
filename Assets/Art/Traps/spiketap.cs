using UnityEngine;

public class spiketap : MonoBehaviour
{
    public float timekill = 13; // เวลาที่ผู้เล่นจะถูกฆ่าหลังจากสัมผัสกับสไปค์
    public float timeUse = 0; // จำนวนความเสียหายที่ผู้เล่นจะได้รับจากสไปค์
    public Collider2D spikeCollider; // คอลลิเดอร์ของสไปค์
    public PlayerController playerController; // อ้างอิงถึงสคริปต์ PlayerController ของผู้เล่น
    void Start()
    {
            Debug.LogError("🚨 สคริปต์ spiketap หา Collider2D ไม่เจอครับ!");
        
    }
    void Update()
    {
        timeUse += Time.deltaTime; // เพิ่มเวลาที่ผู้เล่นสัมผัสกับสไปค์
        if (timeUse >= timekill)
        {   
            spikeCollider.enabled = true; // ปิดคอลลิเดอร์ของสไปค์เพื่อไม่ให้ผู้เล่นได้รับความเสียหายอีก

            timeUse = 0; // รีเซ็ตเวลาที่ผู้เล่นสัมผัสกับสไปค์
        }
        else if (timeUse < timekill)
        {
            spikeCollider.enabled = false; // เปิดคอลลิเดอร์ของสไปค์เพื่อให้ผู้เล่นได้รับความเสียหาย
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.Die(); 
        }
    }
}
