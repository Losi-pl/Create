using Create.Conteiner;
using Create.OpenGL;
using OpenTK.Mathematics;

namespace Create.Elements.Bazic.Entitys;

public abstract class Mob : Entity
{
    protected internal override void OnSpawn(LivingEntity entity, object? args)
    {
        entity.Data.Set("camera_rot", new Vector2());
        entity.Data.Set("move_delta", new Vector3());
    }

    public override void OnPhisicUpdate(LivingEntity entity, float delta)
    {
        if (entity.Player != null)
            player_control(entity);
        else
            mob_control(entity, delta);

        Vector3 move_delta;
        {
            var wyn = entity.Data.Get("move_delta");
            if (wyn != null)
                move_delta = (Vector3)wyn;
            else
                move_delta = new();
        }
        {
            if (MoveByY(entity, move_delta.Y * delta))
                move_delta.Y = 0;

            if (move_delta.X < move_delta.Z)
            {
                MoveByX(entity, move_delta.X * delta);
                MoveByZ(entity, move_delta.Z * delta);
            }
            else
            {
                MoveByZ(entity, move_delta.Z * delta);
                MoveByX(entity, move_delta.X * delta);
            }
        }
        move_delta -= new Vector3(0, 20 * delta, 0);

        entity.Data.Set("move_delta", move_delta);
    }

    void player_control(LivingEntity entity)
    {
        float cam_rot;
        {
            var player_camera_rot_v = (Vector2?)entity.Data.Get("camera_rot");
            cam_rot = player_camera_rot_v!.Value.X;
        }
        Vector3 move_delta;
        {
            var wyn = entity.Data.Get("move_delta");
            if (wyn != null)
                move_delta = (Vector3)wyn;
            else
                move_delta = new();
        }
        var cam = move_vector();
        if (cam.HasValue)
        {
            var rot = (cam.Value * .25f * MathF.PI) + (cam_rot / 180f * MathF.PI);
            move_delta = new Vector3(MathF.Sin(rot) * 6, move_delta.Y, MathF.Cos(rot) * 6);
        }
        else
            move_delta = new Vector3(0, move_delta.Y, 0);
        if (Input.Keyboard.Space)
            if(IsOnGround(entity))
                move_delta.Y = 7;

        entity.Data.Set("move_delta", move_delta);

        int? move_vector()
        {
            bool up = Input.Keyboard.W;
            bool down = Input.Keyboard.S;
            bool left = Input.Keyboard.A;
            bool right = Input.Keyboard.D;
            if (!(up || down || left || right))
                return null;
            if (up && down)
            {
                if (left)
                    return 6;
                if (right)
                    return 2;
                return null;
            }
            if (up)
            {
                if (left && right)
                    return 0;
                if (left)
                    return 7;
                if (right)
                    return 1;
                return 0;
            }
            if (down)
            {
                if (left && right)
                    return 4;
                if (left)
                    return 5;
                if (right)
                    return 3;
                return 4;
            }
            if (left)
                return 6;
            else
                return 2;
        }
    }
    void mob_control(LivingEntity entity, float delta)
    {

    }
}
