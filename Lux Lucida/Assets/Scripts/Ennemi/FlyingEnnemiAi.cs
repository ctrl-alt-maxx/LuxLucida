using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnnemiAi : MonoBehaviour
{
    [SerializeField]
    private float _ForceMouvement = 10.0f;

    [SerializeField]
    private Transform _Transform3dModel;

    [SerializeField]
    private EnnemyHead _EnnemyHead;

    private Animator _AnimatorE;

    private Rigidbody2D _Rigidbody2D;

    [SerializeField]
    private float _LongueurMouvement = 2.0f;

    [SerializeField]
    private GameObject _DeathVFX;

    Vector2 _DirectionMouvement;

    IEnumerator _Errer;

    // Start is called before the first frame update
    void Start()
    {
        //_Animator = GetComponent<Animator>();
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _AnimatorE = gameObject.GetComponent<Animator>();
        _Errer = Errer();
        StartCoroutine(_Errer);
    }
    // Update is called once per frame
    void Update()
    {


        if (_EnnemyHead.IsHit)
        {
            _AnimatorE.SetBool("Dead", true);


        }

        // Détermine sa vitesse et l'envoie à l'animateur pour déterminer si 
        // il fait l'animation de courir ou sur place et change aussi la direction de l'animation
        // dépendant de vers où il cours.

        float vitesse = _Rigidbody2D.velocity.magnitude;
    }


    private void FixedUpdate()
    {
        // Met a jour son mouvement avec une force prédifini

        _Rigidbody2D.AddForce(_DirectionMouvement * _ForceMouvement);
    }

    private IEnumerator Errer()
    {
        while (true)
        {
           
            _DirectionMouvement = Vector2.zero;
            yield return new WaitForSeconds(1.0f);
            _DirectionMouvement = new Vector2(_LongueurMouvement, 0.0f);
            _Transform3dModel.rotation = Quaternion.LookRotation(Vector3.right);
            yield return new WaitForSeconds(_LongueurMouvement);
            _DirectionMouvement = Vector2.zero;
            yield return new WaitForSeconds(1.0f);
            _DirectionMouvement = new Vector2(-(_LongueurMouvement), 0.0f);
            _Transform3dModel.rotation = Quaternion.LookRotation(Vector3.left);
            yield return new WaitForSeconds(_LongueurMouvement);
        }
    }

    public void Dies()
    {
        Instantiate(_DeathVFX, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        StopCoroutine(_Errer);
    }
}
