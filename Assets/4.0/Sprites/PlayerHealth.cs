using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private SpriteRenderer healthBar;
    public float health = 100f;
    public float repeatDamagePeriod = 2f;
    public float hurtForce = 10f;
    public float damageAmount = 10f;
    public AudioClip[] ouches;

    private float lastHitTime;
    private Vector3 healthScale;
    private PlayerCtrl playerControl;
    private Animator anim;
    void Awake()
    {
        playerControl = GetComponent<PlayerCtrl>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        healthScale = healthBar.transform.localScale;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            int i = Random.Range(0, ouches.Length);
            AudioSource.PlayClipAtPoint(ouches[i], transform.position);
            //可以再次减血
            if (Time.time > lastHitTime + repeatDamagePeriod)
            {
                if (health > 0f)
                {
                    TakeDamage(col.transform);
                    lastHitTime = Time.time;
                }

            }
        }
    }
    void death()
    {
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in spr)
        {
            s.sortingLayerName = "UI";
        }

        GetComponent<PlayerCtrl>().enabled = false;
        playerControl.enabled = false;
        GetComponentInChildren<Gun>().enabled = false;
        anim.SetTrigger("Die");

        //销毁血条
        GameObject go = GameObject.Find("UI_HealthBar");
        Destroy(go);
        GameObject.FindGameObjectWithTag("Kill").SetActive(false);
        StartCoroutine("ReloadGame");
    }

    IEnumerator ReloadGame()
    {
        // ... pause briefly
        yield return new WaitForSeconds(2);
        // ... and then reload the level.

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    void TakeDamage(Transform enemy)
    {
        playerControl.bJump = false;
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
        GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);
        health -= damageAmount;
        if (health <= 0)
        {
            anim.SetTrigger("Die");
            death();
            
            // return;
        }

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
    }

}
