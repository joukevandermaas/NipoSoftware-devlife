using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);

        if (directionalInput.y > 0)
        {
            player.OnJumpInputDown();
        }

        if(Input.GetButtonDown("Jump"))
        {
            var shoot = player.GetComponent<Shoot>();

            shoot.Pew(player.transform.position, player.facingDirection);
        }
    }
}
