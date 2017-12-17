using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorReuse : PoolObject
{
    public GameEvent onLevelStart;

    public override void OnObjectReuse()
    {
        StartCoroutine(TriggerTheCollider());
    }

    private IEnumerator TriggerTheCollider()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PoolObject>().Destroy();
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy();
            onLevelStart.Raise();
            //SceneManager.LoadScene("test");
        }
    }
}
