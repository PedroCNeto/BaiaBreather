using UnityEngine;

public class BaiacuScript : MonoBehaviour
{

    public BreathingEffect breath;
    public SpriteRenderer mySprite;

    public float amplitude = 0.5f; 
    public float speed = 1.4f;
    public float inflateSpeed = 0.4f; 
    public float maxScale = 25f; 


    private float originalRotation;
    private Vector3 startPosition;
    private Vector3 originalScale;

    private Color originalColor;
    public Color targetColor = Color.magenta; // Cor de destino para a transição
    private float colorChangeSpeed = 2f; // Velocidade da transição de cor

    private bool isInflating = false; // Flag para controlar a inflação

    // Parâmetros do balanço
    public float balanceSpeed = 2f; // Velocidade do balanço
    public float balanceAmount = 10f; // Amplitude do balanço

    public Animator animator;

    void Start()
    {
        startPosition = transform.position;
        originalScale = transform.localScale;
        originalColor = mySprite.color;
        originalRotation = transform.rotation.z;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimento de flutuação
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Verifica se o jogador está pressionando a tecla espaço
        if (Input.GetKey(KeyCode.Space)) 
        {
            isInflating = true;  // Inicia o processo de inflação
            animator.SetBool("isInflating", true);
        }
        else 
        {
            isInflating = false;  // Inicia o processo de desinflação
            animator.SetBool("isInflating", false);
        }

        if (isInflating)
        {
            inflateBaiacu();
        }
        else
        {
            deflateBaiacu();
        }
        breath.setAnimParams(transform.localScale, transform.rotation, transform.position);
    }

    void inflateBaiacu()
    {
        // Infla o Baiacu de forma suave
        float newScale = Mathf.Lerp(transform.localScale.x, maxScale, Time.deltaTime * inflateSpeed);
        transform.localScale = new Vector3(newScale, newScale, newScale);
        breath.animationTrigger("IsInflating");
        breath.setAnimValue("IsDeflating", 0);

        // Muda a cor quando atingir o tamanho máximo
        if (transform.localScale.x >= 20f)
        {
            mySprite.color = Color.Lerp(mySprite.color, targetColor, Time.deltaTime * colorChangeSpeed);
            // Faz o Baiacu balançar
            float newRotation = Mathf.Sin(Time.time * balanceSpeed) * balanceAmount;
            transform.rotation = Quaternion.Euler(0, 0, newRotation);
        }
    }

    void deflateBaiacu()
    {
        // Desinfla o Baiacu de forma suave
        float newScale = Mathf.Lerp(transform.localScale.x, originalScale.x, Time.deltaTime * inflateSpeed);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        // Volta para a cor original quando o tamanho diminui
        if (transform.localScale.x < 20f)
        {
            mySprite.color = Color.Lerp(mySprite.color, originalColor, Time.deltaTime * colorChangeSpeed);
        }

        if (transform.localScale.x > 16f){
            animator.SetFloat("IsDeflating", 0);
        }
        else{
            animator.SetFloat("IsDeflating", 1);
        }

        if(transform.localScale.x > 12f){
            breath.setAnimValue("IsDeflating", 1);
        }
        else{
            breath.animationTrigger("IsNormalAgain");
            breath.setAnimValue("IsDeflating", 0);
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
