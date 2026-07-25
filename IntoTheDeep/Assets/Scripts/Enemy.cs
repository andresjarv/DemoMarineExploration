using UnityEngine;

public class SharkEnemy : MonoBehaviour
{
    [Header("Configuraci�n")]
    [SerializeField] private float moveSpeed = 3f;          // Velocidad de movimiento
    [SerializeField] private float detectionRange = 8f;     // Rango para detectar al jugador
    [SerializeField] private float attackRange = 1.5f;      // Rango para atacar
    [SerializeField] private int attackDamage = 20;         // Da�o del mordisco
    [SerializeField] private float attackCooldown = 2f;     // Tiempo entre ataques

    private Transform player;                               // Referencia al jugador
    private Rigidbody2D rb;
    private bool isAttacking = false;
    private float attackTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Aseg�rate de etiquetar al jugador como "Player"
    }

    void Update()
    {
        if (player == null) return;

        // Calcular distancia al jugador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Si el jugador est� en rango de detecci�n, perseguirlo
        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            ChasePlayer();
            FlipSprite(); // Rotar sprite seg�n direcci�n
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Detenerse si est� fuera de rango
        }

        // Atacar si est� en rango y no est� en cooldown
        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            Attack();
        }

        // Temporizador del cooldown del ataque
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                isAttacking = false;
                attackTimer = 0f;
            }
        }
    }

    // Perseguir al jugador
    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    // Ataque del tibur�n
    private void Attack()
    {
        isAttacking = true;
        Debug.Log("�Tibur�n ataca!");

        //// Aplicar da�o al jugador (aseg�rate de que el jugador tenga un componente "Health")
        //if (player.TryGetComponent(out Health playerHealth))
        //{
        //    playerHealth.TakeDamage(attackDamage);
        //}
    }

    // Rotar el sprite seg�n la direcci�n del movimiento
    private void FlipSprite()
    {
        if (rb.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Mirando a la derecha
        }
        else if (rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Mirando a la izquierda
        }
    }

    // Dibujar rangos en el editor (opcional)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}