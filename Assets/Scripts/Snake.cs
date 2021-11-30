using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    // variaveis
    private List<Transform> _segments = new List<Transform>();
    
    public Transform segmentPrefab;
    
    public Vector2 direction = Vector2.right;
    
    public int initialSize = 4;
        
    // inicio do jogo
    private void Start()
    {
        ResetState();
    }
    
    // movimentaçao do player
    private void Update()
    {
        // condição de ir para cima e para baixo
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                this.direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                this.direction = Vector2.down;
            }
        }
        // condição de ir para direita e para esquerda
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                this.direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        // definindo o gameobj corpo
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        // sempre atualiza o X e o Y do corpo
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;

        this.transform.position = new Vector2(x, y);
    }

    // definindo a coletavel
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    // reseta o jogo ao morrer
    public void ResetState()
    {
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    // Definindo o collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // condição para crescer
        if (other.tag == "Food") {
            Grow();
        }
        // condição para morrer e reineciar o jogo
        else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}
