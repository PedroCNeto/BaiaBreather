using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ProgressBar : MonoBehaviour
{

    public UnityEngine.UI.Image preenchimentoBarra;
    public TextMeshProUGUI texto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void atualizarBarra(float tempoAtual)
    {
        float progresso = tempoAtual <= 2.5f ? tempoAtual / 2.5f : (5.0f - tempoAtual) / 2.5f;
        texto.text = tempoAtual <= 2.5f ? "Expire" : "Inspire";
        preenchimentoBarra.fillAmount = Mathf.Lerp(preenchimentoBarra.fillAmount, progresso, Time.deltaTime * 10f);
    }


}
