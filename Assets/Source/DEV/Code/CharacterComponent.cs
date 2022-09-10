using EPOOutline;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterComponent : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currentHealth;

    protected NavMeshAgent agent;
    protected AnimatorComponent animator;
    protected FXHolderComponent fx;
    protected Outlinable outlinableComponent;
    protected Collider col;

    public NavMeshAgent Agent => agent;
    public AnimatorComponent Animator => animator;
    public FXHolderComponent FX => fx;
    public Outlinable Outline => outlinableComponent;
    public int CurrentHealth => currentHealth;
    public Collider Collider => col;

    public virtual void Init()
    {
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<AnimatorComponent>();
        animator.InitAnimator();
        fx = GetComponent<FXHolderComponent>();
        outlinableComponent = GetComponent<Outlinable>();
        col = GetComponent<Collider>();
    }

    public virtual void TakeDamage()
    {
        if (currentHealth <= 0) return;
        currentHealth--;

        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        animator.SetDie();
        outlinableComponent.enabled = false;
    }

    public virtual void RenewStats()
    {
        currentHealth = maxHealth;
        col.enabled = true;
    }
}
