using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour
{
 
    //переменная для проверки контакта с землей
    private bool isGrounded = false;
    //фонарик
    private Light torch;

    //ссылка на компонент Transform объекта
    //для определения соприкосновения с землей
    public Transform groundCheck;
    //радиус определения соприкосновения с землей
    private float groundRadius = 0.3f;
    //ссылка на слой, представляющий землю
    public LayerMask whatIsGround;
    //переменная для установки макс. скорости персонажа
    public float maxSpeed = 6f;
    public float jumpPower = 20f;
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;
    //ссылка на компонент анимаций
    private Animator anim;
    
    /// <summary>
    /// Начальная инициализация
    /// </summary>
	private void Start()
    {
        torch = (Light)GameObject.Find("FlashLight").GetComponent("Light");
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
    /// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
    /// </summary>
    private void FixedUpdate()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //устанавливаем соответствующую переменную в аниматоре
        anim.SetBool("Grounded", isGrounded);
        //устанавливаем в аниматоре значение скорости взлета/падения
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        //если персонаж в прыжке - выход из метода, чтобы не выполнялись действия, связанные с бегом
    

        //используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
        //при стандартных настройках проекта 
        //-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
        //1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
        float move = Input.GetAxis("Horizontal");
        

        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
        //приэтом нам нужен модуль значения
        anim.SetFloat("Speed", Mathf.Abs(move));

        //обращаемся к компоненту персонажа RigidBody2D. задаем ему скорость по оси Х, 
        //равную значению оси Х умноженное на значение макс. скорости

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        //если нажали клавишу для перемещения вправо, а персонаж направлен влево
        if (move > 0 && !isFacingRight)
            //отражаем персонажа вправо
            Flip();
        //обратная ситуация. отражаем персонажа влево
        else if (move < 0 && isFacingRight)
            Flip();
    }
    private void Update()
    {
        //если персонаж на земле и нажат пробел...
        if (isGrounded != false && (Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump")))
        {
            //устанавливаем в аниматоре переменную в false
            anim.SetBool("Grounded", false);
            isGrounded = false;
            //прикладываем силу вверх, чтобы персонаж подпрыгнул
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 600));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {

            if (torch.enabled == true)
            {
                anim.SetBool("torch", false);
                torch.enabled = false;
            }
            else
            {
                anim.SetBool("torch", true);
                torch.enabled = true;
            }
        }
    }


    /// <summary>
    /// Метод для смены направления движения персонажа и его зеркального отражения
    /// </summary>
    private void Flip()
    {
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }
}