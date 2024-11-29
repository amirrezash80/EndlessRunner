using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private float score; // امتیاز فعلی بازیکن
    public Text scoreUI; // مرجع به UI برای نمایش امتیاز
    private int highscore; // بالاترین امتیاز ذخیره‌شده
    public Text highscoreUI; // مرجع به UI برای نمایش بالاترین امتیاز

    public AudioSource audioSource; // مرجع به منبع صدا برای پخش یا توقف موسیقی
    public Transform player; // مرجع به شیء بازیکن
    public PlayerMovement movement; // مرجع به اسکریپت حرکت بازیکن

    public GameObject obstaclePrefab; // پیش‌ساخته موانع
    public Transform obstacles; // محفظه‌ای برای ذخیره موانع
    public int obstacleStartX = 100; // موقعیت شروع برای تولید موانع

    public GameObject deathOverlayUI; // UI نمایش صفحه Game Over

    public Fade fade; // اسکریپت مدیریت محو و ظاهر شدن صفحه

    // متد اجرای انتقال صفحه Game Over
    IEnumerator DeathOverlayTransition()
    {
        yield return new WaitForSeconds(1); // تأخیر اولیه برای نمایش صفحه
        yield return new WaitForSeconds(fade.BeginFade(1)); // شروع محو شدن صفحه

        deathOverlayUI.SetActive(true); // فعال‌سازی UI صفحه Game Over
        audioSource.Stop(); // توقف موسیقی پس‌زمینه

        yield return new WaitForSeconds(fade.BeginFade(-1)); // پایان محو شدن و نمایش مجدد صفحه
    }

    // متد برای انتقال به صفحه جدید (صحنه جدید)
    IEnumerator SceneTransition(int scene)
    {
        yield return new WaitForSeconds(fade.BeginFade(1)); // محو شدن قبل از تغییر صحنه

        SceneManager.LoadScene(scene, LoadSceneMode.Single); // بارگذاری صحنه جدید
    }

    public void SwitchScene(int scene)
    {
        StartCoroutine(SceneTransition(scene)); // شروع انتقال به صحنه جدید به صورت همزمان
    }

    // متد برای شروع فرآیند (Game Over)
    public void InitiateDeath()
    {
        CancelInvoke("Spawn"); // لغو فراخوانی تابع تولید موانع

        FindObjectOfType<PlayerMovement>().enabled = false; // غیرفعال کردن حرکت بازیکن

        // غیرفعال کردن تمامی موانع در صحنه
        foreach (Transform obstacle in obstacles)
        {
            obstacle.gameObject.GetComponent<ObstacleMovement>().enabled = false;
        } 

        UpdateHighscore(); // به‌روزرسانی بالاترین امتیاز
        highscoreUI.text = "Highscore: " + highscore; // نمایش بالاترین امتیاز در UI

        StartCoroutine(DeathOverlayTransition()); // شروع انیمیشن انتقال صفحه Game Over
    }

    // متد برای به‌روزرسانی بالاترین امتیاز
    private void UpdateHighscore()
    {
        highscore = PlayerPrefs.GetInt("Highscore"); // دریافت بالاترین امتیاز ذخیره‌شده

        if (score > highscore) // اگر امتیاز فعلی بیشتر از بالاترین امتیاز باشد
        {
            highscore = (int)score; // تنظیم امتیاز فعلی به عنوان بالاترین امتیاز
            PlayerPrefs.SetInt("Highscore", highscore); // ذخیره بالاترین امتیاز
        }
    }

    // متد برای تولید موانع در بازی
    private void Spawn()
    {
        int i;

        // تولید دو مانع جدید در موقعیت‌های مختلف
        for (i = -7; i < 7; i += 7)
        {
            Instantiate(obstaclePrefab,
                        new Vector3(Mathf.Floor(Random.Range(i, i + 7)), 1, obstacleStartX),
                        Quaternion.identity, obstacles);
        }
    }

    // متد به‌روزرسانی هر فریم
    private void Update()
    {
        if (FindObjectOfType<PlayerMovement>().enabled) // اگر بازیکن در حال حرکت است
        {
            score += Time.deltaTime * 10; // افزایش امتیاز با توجه به زمان
            scoreUI.text = "Score: " + (int)score; // به‌روزرسانی UI امتیاز
            highscoreUI.text = "High Score: " + highscore; // به‌روزرسانی UI بالاترین امتیاز
        }

        if (Input.GetKey("r"))
        {
            SwitchScene(1); // تغییر به صحنه بازی جدید
            return;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ResetAllData(); // ریست کردن تمام داده‌ها
            ResetHighScore(); // ریست کردن بالاترین امتیاز
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (deathOverlayUI.activeSelf) // اگر صفحه Game Over فعال باشد
            {
                SwitchScene(0); // تغییر به صفحه اصلی
            }
            else
            {
                InitiateDeath(); // شروع فرآیند Game Over
            }
            return;
        }
    }

    // متد برای ریست کردن تمام داده‌های ذخیره‌شده (بالاترین امتیاز و سایر داده‌ها)
    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll(); // پاک کردن تمام داده‌های ذخیره‌شده
        highscore = 0; // مقداردهی مجدد بالاترین امتیاز
        highscoreUI.text = "High Score: " + highscore; // به‌روزرسانی UI
    }

    // متد برای ریست کردن فقط بالاترین امتیاز
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("Highscore"); // حذف بالاترین امتیاز از PlayerPrefs
        highscore = 0; // مقداردهی مجدد بالاترین امتیاز
        highscoreUI.text = "High Score: " + highscore; // به‌روزرسانی UI
    }

    // متد شروع بازی (اولین بار که صحنه بارگذاری می‌شود)
    private void Start()
    {
        fade.BeginFade(-1); // شروع انیمیشن محو شدن قبل از شروع بازی

        // دریافت بالاترین امتیاز ذخیره‌شده در بازی
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreUI.text = "High Score: " + highscore; // نمایش بالاترین امتیاز

        // فراخوانی تولید موانع با فواصل زمانی
        InvokeRepeating("Spawn", 1f, 0.5f);
    }
}
