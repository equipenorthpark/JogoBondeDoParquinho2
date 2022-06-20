using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;

    private Rigidbody2D rig;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    // Cria método de movimento
    void Move()
    {
        // O Input serce para detectar teclas e definir valores para elas
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        // transform.position só funciona com Vector3
        // Time.deltaTime serve para definir velpocidade e Speed controla na Unity
        transform.position += movement * Time.deltaTime * Speed;

        // Quando estiver andando (para esquerda ou direita) a boleando "walk" será true, então ele executará as animções feitas
        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            // eulerAngles nos permite rotacionar o objeto
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        
        if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            // Aqui o valor é 180 porque o objeto está virado para a esquerda
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        // Quando estiver parado o boleando "walk" será false
        if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        // Para pular, usaremos o RigidBody para movimentar o personagem
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                // Ativa a opção de pular duas vezes
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if(doubleJump)
                {
                    // Impede o personagem de pular mais de duas vezes
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
            
        }
    }

    // Métodos para verificar se o personagem toca em algo
    // Também corrige um problema de pular a cada vez que se pressiona a tecla espaço
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8) 
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

}
