using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.up;

    [SerializeField] private List<Transform> _segments;
    public Transform segmentPrefab;

    private void Start(){
        _segments.Add(this.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)){
            _direction = Vector2.up;
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S)){
            _direction = Vector2.down;
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (Input.GetKeyDown(KeyCode.A)){
            _direction = Vector2.left;
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.D)){
            _direction = Vector2.right;
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    private void FixedUpdate(){
        for (int i = _segments.Count - 1; i > 0; i--){
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
            );
    }

    private void ResetGame()
    {
        SceneManager.LoadScene("Snake");
    }

    private void Grow(){
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.GetComponent<Food>()){
            Grow();
        }else if (collision.gameObject.GetComponent<GameOver>()){
            ResetGame();
        }
    }
}
