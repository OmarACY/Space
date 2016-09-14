using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	//Asteroides del juego
	public GameObject[] hazards;
	public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float starWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

	void Start(){
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        //Se inicializa el la puntuacion        
        score = 0;
        UpdateScore();
        //Ejecuta una corutina cada cierto tiempo
	    StartCoroutine (SpawnWaves ());
	}

    void Update(){
        if(restart){
            if(Input.GetKeyDown(KeyCode.R)){

              SceneManager.LoadScene("Main",LoadSceneMode.Single);
              //Application.LoadLevel(Application.loadedLevel);(obsoleta)
            }
        }
    }
	IEnumerator SpawnWaves(){
        //Tiempo de espera para ejecutar la accion
        yield return new WaitForSeconds(starWait);
        while(true){
            for (int i = 0; i < hazardCount; i++){
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                //Tiempo de espera para ejecutar la accion del ciclo
                yield return new  WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver){
                restartText.text = "Presiona 'R' para reiniciar";
                restart = true;
                break;
            }
        }
	}
    public void AddScore( int newScoreValue){
        score  += newScoreValue;
        UpdateScore();
    }
    void UpdateScore() {
        scoreText.text = "Puntuacion: " + score;
    }
    public void GameOver() { 
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
 
}
