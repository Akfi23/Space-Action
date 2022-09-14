using Kuhpik;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerMovementSystem : GameSystem
{
    private Vector3 direction;
    private Vector3 previousPosition;
    private float moveLerpedValue;
    private float sideLerpedValue;

    [SerializeField] private float lerpIn;
    [SerializeField] private float lerpOut;

    public override void OnInit()
    {
        game.Player.Animator.SetMoveSpeedAnimator(0);

    }

    public override void OnUpdate()
    {
        MovePlayerByJoystick();
    }

    public void MovePlayerByJoystick()
    {
        if (!game.Player.Agent.enabled) return;

        direction = new Vector3(game.Joystick.Direction.x, 0, game.Joystick.Direction.y);
        direction = Quaternion.Euler(0, cameraController.GameCamera.transform.eulerAngles.y, 0) * direction;

        if (direction.sqrMagnitude > 0)
        {
            game.isPlayerMoving = true;
            game.Player.transform.forward = Vector3.Slerp(game.Player.transform.forward, direction, lerpIn * Time.deltaTime);
        }
        else
        {
            game.isPlayerMoving = false;
            game.Player.transform.forward = Vector3.Slerp(game.Player.transform.forward, game.Player.transform.forward, lerpOut * Time.deltaTime);
        }

        game.Player.Agent.Move(direction * config.PlayerMoveSpeed * Time.deltaTime);

        moveLerpedValue = Mathf.Lerp(moveLerpedValue, Velocity().magnitude / config.PlayerMoveSpeed, 5 * Time.deltaTime);
        game.Player.Animator.SetMoveSpeedAnimator(moveLerpedValue);

        previousPosition = game.Player.transform.position;

        UpdateCircleRotation();
    }

    private Vector3 Velocity()
    {
        return ((game.Player.transform.position - previousPosition) / Time.deltaTime);
    }

    private float CheckSide()
    {
        var angle = Vector3.SignedAngle(game.Player.transform.forward, direction, Vector3.up);
        return angle / 90;
    }

    private void UpdateCircleRotation()
    {
        Vector3 eulerRot = new Vector3(90, -game.Player.transform.localRotation.eulerAngles.y, -game.Player.transform.localRotation.eulerAngles.z);
        game.Player.Circle.transform.localRotation = Quaternion.Euler(eulerRot);
    }
}
