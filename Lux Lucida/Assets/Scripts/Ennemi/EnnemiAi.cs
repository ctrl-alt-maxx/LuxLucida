using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.Net.WebRequestMethods;

//[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnnemiAi : MonoBehaviour
{

    [SerializeField]
    private Transform _Cible;

    [SerializeField]
    private float _ForceMouvement = 10.0f;

    [SerializeField]
    private bool _EstEnChasse = false;

    [SerializeField]
    private float _DistanceVision = 5.0f;

    [SerializeField]
    private Transform _Transform3dModel;

    [SerializeField]
    private EnnemyHead _EnnemyHead;

    [SerializeField]
    private float _LongueurMouvement = 2.0f;

    [SerializeField]
    private GameObject _DeathVFX;

    [SerializeField]
    private GameObject _LightVFX;


    private Animator _AnimatorE;
 
    private Rigidbody2D _Rigidbody2D;

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
        // NOTE:
        // Pour que ce code marche il faut que la cible ai le layer Joueur attribué sinon l'AI ne le suivera pas
        // Cela dit, si on veut que l'AI ce fasse cacher la vision par un obstacle il suffit d'ajouter l'objet au layer Obstacle.
        bool etaitEnChasse = _EstEnChasse;
        Vector2 delta = _Cible.position - gameObject.transform.position;
        Vector2 _DirectionVision = delta.normalized;
        int layerMask = LayerMask.GetMask(new[] { "Obstacle", "Player" });
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, _DirectionVision, _DistanceVision, layerMask);
        _EstEnChasse = hit.collider && hit.collider.gameObject.layer == _Cible.gameObject.layer;
        Debug.DrawRay(this.gameObject.transform.position, _DirectionVision * _DistanceVision, _EstEnChasse ? Color.green : Color.gray);

        ////////////////////////////////////////////////////////////////////////////////////////////

        if (_EstEnChasse)
        {
            bool estADroite = _DirectionVision.x > 0;
            if (estADroite)
            {
                _Transform3dModel.rotation = Quaternion.LookRotation(Vector3.right);
            }
            else
            {
                _Transform3dModel.rotation = Quaternion.LookRotation(Vector3.left);
            }
            //Vient de tomber en chasse
            if (!etaitEnChasse)
            {
                StopCoroutine(_Errer);
                _Errer = Errer();
            }
            

            _DirectionMouvement = _DirectionVision;
        }
        else
        {
            if (_DirectionMouvement == Vector2.zero)
            {
                _Transform3dModel.rotation = Quaternion.LookRotation(Vector3.back);
            }
            else
            {
                _Transform3dModel.rotation = Quaternion.LookRotation(_DirectionMouvement);
            }
            
            //Il ne voit plus sa cible donc il est immobile
            if (etaitEnChasse)
            {
                StartCoroutine(_Errer);
            }

            //_DirectionMouvement = Vector2.zero; 
        }
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
            yield return new WaitForSeconds(_LongueurMouvement);

            _DirectionMouvement = Vector2.zero;
            yield return new WaitForSeconds(1.0f);
            _DirectionMouvement = new Vector2(-(_LongueurMouvement), 0.0f);
            yield return new WaitForSeconds(_LongueurMouvement);



        }
    }

    public void Dies()
    {
        Instantiate(_DeathVFX, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _AnimatorE.SetBool("Attacking", true);
            _LightVFX.SetActive(true);  
            

        }

    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _AnimatorE.SetBool("Attacking", false);
            _LightVFX.SetActive(false);
        }
    }
}
