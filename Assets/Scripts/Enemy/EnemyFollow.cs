using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
   //public Transform player;        // Référence au joueur
    public float speed = 0.05f;      // Vitesse de déplacement vers le joueur
    public const float activationDistance = 2.0f;  // Distance d'activation de l'ennemi
    private Animator animator;      // Référence à l'Animator

    Vector3 direction;
    [SerializeField] Transform player;
    UnityEngine.AI.NavMeshAgent agent;
    RaycastHit hitData;



    void Start()
    {
        // Si le joueur n'est pas assigné manuellement dans l'inspecteur, trouvez-le automatiquement
        
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            //player = GameObject.FindGameObjectWithTag("Player").transform;
        

        // Récupérer le composant Animator
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;  // Désactiver l'Animator au début
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifiez si l'objet qui est entré dans le trigger a le tag "Player"
        if ((other.CompareTag("Player")))
        {
            Debug.Log("Le est attacke.");
            GameObject.Find("Canvas").GetComponent<HandleSliders>().Attacked = true;
        }
    }
    void FireRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hitData))
        {
            Debug.Log($"Item: {hitData.collider.name} Distance: {hitData.distance}");
        }
    }
    void Update()
    {
        // Vérifier que le joueur est assigné et existe toujours
        if (player != null && animator != null)
        {
            FireRay();
            agent.SetDestination(player.position);
            // Calculer la distance entre l'ennemi et le joueur
            float distance = Vector3.Distance(transform.position, player.position);
            // Si le joueur est dans la distance d'activation, activer l'animation
            if (hitData.distance <= activationDistance)
            {
                //Debug.Log("Le joueur s'approche. Activation de l'ennemi  1.");
                if (!animator.enabled)
                {
                    animator.enabled = true;  // Activer l'animation
                    Debug.Log("Le joueur s'approche. Activation de l'ennemi  1.");
                }
                // // Calculer la direction vers le joueur
                direction = new Vector3(player.position.x - transform.position.x, 0, 0).normalized;
                //Debug.Log("Le joueur s'approche. Activation de l'ennemi."+ direction);

              
            }
            else
            {
                if (animator.enabled)
                {
                    animator.enabled = false;  // Désactiver l'animation si le joueur est trop loin
                    Debug.Log("Le joueur est trop loin. Désactivation de l'ennemi.");
                      // // Calculer la direction vers le joueur
                }
            }
        }
    }
}