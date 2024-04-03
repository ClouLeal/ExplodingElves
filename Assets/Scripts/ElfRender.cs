using System;
using System.Collections;
using UnityEngine;

public class ElfRender : MonoBehaviour
{
    Material _material;
    MeshRenderer _meshRenderer;
    [SerializeField] GameObject _model;
    [SerializeField] Animator _animator;

    private WaitForSeconds _waitToReturnToPool = new WaitForSeconds(1f);

    void Awake()
    {
        _meshRenderer = _model.GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
    }

    public void SetColor(Color color)
    {
        _material.color = color;
    }

    public void ElfDie(Transform transform, Action OnFinishEffect){
        _animator.SetTrigger("Die");
        StartCoroutine(PlayExplosionVFXCoroutine(0.5f,EffectType.Explosion,transform.position, OnFinishEffect));
    }

    //Todo: This can be rewrite as no need to copy and paste almost all code from PlayVFXCoroutine(). How ever to avoid colling 
    //two nested coroutine, for this project I will leave as ir is. 
     IEnumerator PlayExplosionVFXCoroutine(float delay,EffectType type , Vector3 position, Action OnFinishEffect = null){
         
        yield return new WaitForSeconds(delay);

        DisableElf();
        
        if(VFXPool.Instance.GetPooledObject(type, out var VFX))
        {
            VFX.transform.position = transform.position;
            VFX.Stop();
            VFX.Play();

            yield return _waitToReturnToPool;
            VFXPool.Instance.ReturntPooledObject(type,VFX);
        }

         OnFinishEffect?.Invoke();
    }

    public void EnableElf(Transform transform)
    {
        _model.SetActive(true);
        StartCoroutine(PlayVFXCoroutine(0,EffectType.Spawn, transform));
    }

  
    IEnumerator PlayVFXCoroutine(float delay,EffectType type , Transform transform, Action OnFinishEffect = null){
         
        if(delay > 0) yield return new WaitForSeconds(delay);

        if(VFXPool.Instance.GetPooledObject(type, out var VFX))
        {
            VFX.transform.position = transform.position;
            VFX.transform.SetParent(transform);
            VFX.Stop();
            VFX.Play();

            yield return _waitToReturnToPool;
            VFXPool.Instance.ReturntPooledObject(type,VFX);
        }

         OnFinishEffect?.Invoke();
    }


    public void DisableElf()
    {
        _model.SetActive(false);
    }
}
