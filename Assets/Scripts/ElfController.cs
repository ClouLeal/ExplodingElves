using System;
using System.Collections;
using UnityEngine;

public class ElfController : MonoBehaviour
{
    private int _id;
    
    public int ID {
        get => _id;
    }
    
    [SerializeField] Collider _elfCollider;
    [SerializeField] ElfRender _elfRender;
    [SerializeField] MovimentController _elfMovimentController;

    public Action<ElfController> OnDeath;

    public Action<Vector3, string, string> SpawnElf;

    private AudioManager _audioManager;

    private const float _delayForAtiveCollider = 1f;
    private WaitForSeconds _waitForAtiveCollider = new WaitForSeconds(_delayForAtiveCollider);
    private bool _isEnable;

    public void SetID(int id)
    {
        name = id.ToString();
        _id = id;
    } 

  
     void OnTriggerExit(Collider other)
     {
        if(!_isEnable) return;

        if(other.gameObject.tag == gameObject.tag)
        {
             GenerateElf(other.name);           
        }
        else if(other.gameObject.tag != "Untagged" )
        {
            Explode();          
        }
    }

    private void GenerateElf(string otherId)
    {
        SpawnElf?.Invoke(transform.position, gameObject.name, otherId );
    }

    private void Explode()
    {
        DisableElf(false);
        PlayEffects(EffectType.Explosion);
        
    }

    private void OnEndEffect()
    {
        OnDeath?.Invoke(this);
    }



    public void SetUp(Vector3 initialPosition, AudioManager audioManager, Color _color, string _tag)
    {
        gameObject.tag = _tag;

        transform.position = initialPosition;
        _audioManager = audioManager;

        _elfRender.SetColor(_color);

        StartCoroutine(EnableElf());
    }

      IEnumerator EnableElf() 
    {
        _isEnable = true;

        _elfMovimentController.enabled = true;

        PlayEffects(EffectType.Spawn);

        yield return _waitForAtiveCollider;

        _elfCollider.enabled = true;
    }

    public void DisableElf(bool disableVisual){

        _isEnable = false;

        _elfMovimentController.enabled = false;
        _elfCollider.enabled = false;

        if(disableVisual) _elfRender.DisableElf();
    }

    
    private void PlayEffects(EffectType type)
    {
        if(type == EffectType.Spawn)
        {
            _elfRender.EnableElf(transform);
            _audioManager.Play(EffectType.Spawn);
        }
        else if(type == EffectType.Explosion)
        {
            _elfRender.ElfDie(transform, OnEndEffect);
            _audioManager.Play(EffectType.Explosion);
        }
        
    }
}
