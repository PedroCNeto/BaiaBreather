using Unity.Mathematics;
using UnityEngine;

public class BreathingEffect : MonoBehaviour
{

    public Animator animator;
    
    Vector3 scale;
    quaternion rotation;
    
    Vector3 position;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animSetter();
    }

    public void animationTrigger(string animName){
        animator.SetTrigger(animName);
    }

    public void setAnimValue(string animName, int value){
        animator.SetFloat(animName, value);
    }

    public void setAnimParams(Vector3 s, quaternion r, Vector3 p){
        scale = s;
        rotation = r;
        position = p;
    }

    void animSetter(){
        transform.localScale = scale;
        transform.rotation = rotation;
        transform.position = position;
    }
}
