using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start()
    {
        // cria o elemento
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        // o elemento aparece aleatoriamente na tela
        Bounds bounds = this.gridArea.bounds;

        // define o X e Y
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // quando o player colidir com esse elemento ele é coletado.
        RandomizePosition();
    }

}
