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
    private Vector3 _lerpedSpeed;
    private Vector3 _perFrameOffset;
    private Vector3 _prevFramePosition;

    [SerializeField] private float lerpIn;
    [SerializeField] private float lerpOut;

    public override void OnInit()
    {
        game.Player.Animator.SetMoveSpeedAnimator(0);
        _prevFramePosition = game.Player.transform.position;
    }

    public override void OnUpdate()
    {
        if (!game.Player.Agent.enabled) return;

        MovePlayerByJoystick();
    }

    public override void OnLateUpdate()
    {
        UpdateAnimatorByDirection();
    }

    public void MovePlayerByJoystick()
    {
        direction = new Vector3(game.Joystick.Direction.x, 0, game.Joystick.Direction.y);
        direction = Quaternion.Euler(0, cameraController.GameCamera.transform.eulerAngles.y, 0) * direction;

        if (direction.sqrMagnitude > 0)
        {
            game.isPlayerMoving = true;
            if(!game.isAttack)
                game.Player.transform.forward = Vector3.Slerp(game.Player.transform.forward, direction, lerpIn * Time.deltaTime);
        }
        else
        {
            game.isPlayerMoving = false;
            game.Player.transform.forward = Vector3.Slerp(game.Player.transform.forward, game.Player.transform.forward, lerpOut * Time.deltaTime);
        }

        game.Player.Agent.Move(direction * config.PlayerMoveSpeed * Time.deltaTime);

        moveLerpedValue = Mathf.Lerp(moveLerpedValue, Velocity().magnitude / config.PlayerMoveSpeed, 5 * Time.deltaTime);
        previousPosition = game.Player.transform.position;

        UpdateCircleRotation();
    }

    private void UpdateAnimatorByDirection()
    {

        if (game.isAttack)
        {
            _perFrameOffset = (game.Player.transform.position - _prevFramePosition) / Time.deltaTime / 7.5f;
            _perFrameOffset =
                Quaternion.Euler(0, Vector3.SignedAngle(game.Player.transform.forward, Vector3.forward, Vector3.up), 0) *
                _perFrameOffset;

            _lerpedSpeed = Vector3.Lerp(_lerpedSpeed, _perFrameOffset, 10 * Time.deltaTime);

            _prevFramePosition = game.Player.transform.position;

            Debug.Log(_lerpedSpeed.x);
            game.Player.Animator.SetSideOffsetAnimator(_lerpedSpeed.x);
            game.Player.Animator.SetMoveSpeedAnimator(_lerpedSpeed.z);
        }
        else
        {
            game.Player.Animator.SetSideOffsetAnimator(0);
            game.Player.Animator.SetMoveSpeedAnimator(moveLerpedValue);
        }
    }

    private Vector3 Velocity()
    {
        return ((game.Player.transform.position - previousPosition) / Time.deltaTime);
    }

    private float CheckSide()
    {
        var angle = Vector3.SignedAngle(game.Player.transform.forward, direction, Vector3.up);
        return angle / 25;
    }

    private void UpdateCircleRotation()
    {
        Vector3 eulerRot = new Vector3(90, -game.Player.transform.localRotation.eulerAngles.y, -game.Player.transform.localRotation.eulerAngles.z);
        game.Player.Circle.transform.localRotation = Quaternion.Euler(eulerRot);
    }
}
