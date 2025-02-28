using UnityEngine;

public class BaiacuScript : MonoBehaviour
{

    public BreathingEffect breath;
    public SpriteRenderer mySprite;

    public float amplitude = 0.5f; // Amplitude do balanço do baiacu para cima e para baixo
    public float velocidadeDeBalanco = 1.4f; // Velocidade que o baiacu se movimenta
    public float velocidadeDeInflacao = 0.4f; // Velocidade que o baiacu vai inchar ou desinchar
    public float tamanhoMaximo = 25f; // Tamanho maximo que ele pode chegar

    private Vector3 posicaoInicial;
    private Vector3 escalaInicial;

    private Color corOriginal;
    public Color corAlvo = Color.magenta; // Cor que o baiacu vai ficar quando estiver inflado ao maximo
    private float velocidadeDaMudancaDeCor = 2f; // Velocidade da transição de cor

    private bool isInflating = false; // Flag para controlar a inflação

    // Parâmetros do balanço
    public float velocidadeBalanco = 2f; // Velocidade do balanço
    public float amplitudeBalanco = 10f; // Amplitude do balanço

    public Animator animator;

    void Start()
    {
        posicaoInicial = transform.position;
        escalaInicial = transform.localScale;
        corOriginal = mySprite.color;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimento de flutuação
        float newY = posicaoInicial.y + Mathf.Sin(Time.time * velocidadeDeBalanco) * amplitude;
        transform.position = new Vector3(posicaoInicial.x, newY, posicaoInicial.z);

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
        float newScale = Mathf.Lerp(transform.localScale.x, tamanhoMaximo, Time.deltaTime * velocidadeDeInflacao);
        transform.localScale = new Vector3(newScale, newScale, newScale);
        breath.animationTrigger("IsInflating");
        breath.setAnimValue("IsDeflating", 0);

        // Muda a cor quando atingir o tamanho máximo
        if (transform.localScale.x >= 20f)
        {
            mySprite.color = Color.Lerp(mySprite.color, corAlvo, Time.deltaTime * velocidadeDaMudancaDeCor);
            // Faz o Baiacu balançar
            float newRotation = Mathf.Sin(Time.time * velocidadeBalanco) * amplitudeBalanco;
            transform.rotation = Quaternion.Euler(0, 0, newRotation);
        }
    }

    void deflateBaiacu()
    {
        // Desinfla o Baiacu de forma suave
        float newScale = Mathf.Lerp(transform.localScale.x, escalaInicial.x, Time.deltaTime * velocidadeDeInflacao);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        // Volta para a cor original quando o tamanho diminui
        if (transform.localScale.x < 20f)
        {
            mySprite.color = Color.Lerp(mySprite.color, corOriginal, Time.deltaTime * velocidadeDaMudancaDeCor);
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
