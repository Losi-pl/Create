using Create.Conteiner;
using Create.Linq;
using Create.OpenGL;
using Create.Render;
using Create.Space;
using OpenTK.Mathematics;

namespace Create.Elements.Bazic.Entitys;

public abstract class Mob : Entity
{
    protected internal override void OnSpawn(LivingEntity entity, object? args)
    {
        entity.Data.Set("camera_rot", new Vector2());
        entity.Data.Set("move_delta", new Vector3());
    }

    static Shader tmp_shader { get; } = Assets.GetShader("create:bazicmob").InvokeFor(s =>
        s.SetUniform("color", new Vector4(61 / 255f, 172 / 255f, 27 / 255f, 1)));

    /// <summary>
    /// Podaje wymiary moba
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual (float height, float width) GetMobSize(LivingEntity entity) => (1.86f, 0.68f);
    
    /// <summary>
    /// Na jakiej wysokości znajduje się kamera
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual float GetCameraHeight(LivingEntity entity) => 1.7f;

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

    /// <summary>
    /// Kontrola instancji przez gracza
    /// </summary>
    /// <param name="entity"></param>
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
        if (Input.Keyboard.Space.Status)
            if(IsOnGround(entity))
                move_delta.Y = 7;

        entity.Data.Set("move_delta", move_delta);

        int? move_vector()
        {
            bool up = Input.Keyboard.W.Status;
            bool down = Input.Keyboard.S.Status;
            bool left = Input.Keyboard.A.Status;
            bool right = Input.Keyboard.D.Status;

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
    
    /// <summary>
    /// Kontrola postaci przez AI
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="delta"></param>
    void mob_control(LivingEntity entity, float delta)
    {
        
    }

    public override EntityModel GetModel(LivingEntity entity)
    {
        var mob_size = ((Mob)entity.Entity).GetMobSize(entity);
        var model = Mesh.Create(tmp_shader)
            .SetVertex("poz", new Vector3[]
            {
                new(0, 0, 0),
                new(1, 0, 0),
                new(1, 0, 1),
                new(0, 0, 1),
                new(0, 1, 0),
                new(1, 1, 0),
                new(1, 1, 1),
                new(0, 1, 1)
            }.Convert(v => (v * new Vector3(mob_size.width, mob_size.height, mob_size.width)) - new Vector3(mob_size.width / 2, 0, mob_size.width / 2)))
            .SetTrangles(new[]
            {
                1,4,3, 1,3,2,
                5,8,7, 5,7,6,
                4,8,7, 4,7,3,
                //1,5,8, 1,8,4,
                //2,6,7, 2,3,7,
                //1,5,6, 1,6,2
            }.Convert(t => --t))
            .Finish();

        return new(model);
    }
    
    /// <summary>
    /// Mów instancje <paramref name="entity"/> o <paramref name="move"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="move"></param>
    /// <returns></returns>
    public static bool Move(LivingEntity entity, Vector3 move)
    {
        bool collid = false;
        if (MoveByY(entity, move.Y))
            collid = true;
        if (move.X < move.Z)
        {
            if (MoveByX(entity, move.X))
                collid = true;
            if (MoveByZ(entity, move.Z))
                collid = true;
        }
        else
        {
            if (MoveByZ(entity, move.Z))
                collid = true;
            if (MoveByX(entity, move.X))
                collid = true;
        }
        return collid;
    }
    
    /// <summary>
    /// Mów instancje <paramref name="entity"/> w osi Y o <paramref name="move"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="move"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool MoveByY(LivingEntity entity, float move)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        if (move == 0)
            return false;
        var world = entity.Dimention!.World;
        var entity_pozition = entity.PozitionByCenter;
        ((int min, int max) x, (int min, int max) z) entity_plane;
        (int min, int max) pozition_y;
        var entity_size = ((Mob)entity.Entity).GetMobSize(entity);
        {
            ((float min, float max) x, (float min, float max) z) entity_plane_f;
            entity_plane_f = (
                (entity_pozition.X - (entity_size.width / 2), entity_pozition.X + (entity_size.width / 2)),
                (entity_pozition.Z - (entity_size.width / 2), entity_pozition.Z + (entity_size.width / 2)));
            entity_plane = (
                (MathC.Section(entity_plane_f.x.min, 1), MathC.Section(entity_plane_f.x.max, 1)),
                (MathC.Section(entity_plane_f.z.min, 1), MathC.Section(entity_plane_f.z.max, 1)));
        }
        if (move < 0)
        {
            float? max_poz_y = null;
            {
                var pozition_y_f = (entity_pozition.Y + move, entity_pozition.Y);
                pozition_y = (MathC.Section(pozition_y_f.Item1, 1), MathC.Section(pozition_y_f.Item2, 1));
            }
            for (int y = pozition_y.min; y < pozition_y.max + 1; y++)
                for (int x = entity_plane.x.min; x < entity_plane.x.max + 1; x++)
                    for (int z = entity_plane.z.min; z < entity_plane.z.max + 1; z++)
                    {
                        IEnumerable<Block.BlockCollider> colliders = world.GetBlock(x, y, z).Cast(b =>
                            b.Block.GetPhisicCollision(new() { pozition = (x, y, z), block = b, world = world }));
                        foreach(var c in colliders)
                        {
                            var dist = (MathF.Abs(entity_pozition.X - (x + c.pozition.x)), MathF.Abs(entity_pozition.Z - (z + c.pozition.z)));
                            if (dist.Item1 < (c.size.x / 2) + (entity_size.width / 2) && dist.Item2 < (c.size.z / 2) + (entity_size.width / 2))
                            {
                                var wys = y + c.pozition.y + (c.size.y / 2);
                                if (entity_pozition.Y >= wys)
                                {
                                    if (!max_poz_y.HasValue)
                                        max_poz_y = wys;
                                    if (max_poz_y.Value < wys)
                                        max_poz_y = wys;
                                }
                            }
                        }
                    }
            if (!max_poz_y.HasValue)
            {
                entity.Pozition += new Vector3(0, move, 0);
                return false;
            }
            if (entity_pozition.Y + move < max_poz_y.Value)
            {
                var poz = entity.PozitionByCenter;
                poz.Y = max_poz_y.Value;
                entity.PozitionByCenter = poz;
                return true;
            }
            else
                entity.Pozition += new Vector3(0, move, 0);
            return false;
        }
        else
        {
            var entity_top = entity_pozition.Y + entity_size.height;
            float? min_poz_y = null;
            {
                var pozition_y_f = (entity_pozition.Y + entity_size.height, entity_pozition.Y + move + entity_size.height);
                pozition_y = (MathC.Section(pozition_y_f.Item1, 1), MathC.Section(pozition_y_f.Item2, 1));
            }
            for (int y = pozition_y.min; y < pozition_y.max + 1; y++)
                for (int x = entity_plane.x.min; x < entity_plane.x.max + 1; x++)
                    for (int z = entity_plane.z.min; z < entity_plane.z.max + 1; z++)
                    {
                        IEnumerable<Block.BlockCollider> colliders = world.GetBlock(x, y, z).Cast(b =>
                            b.Block.GetPhisicCollision(new() { pozition = (x, y, z), block = b, world = world }));
                        foreach (var c in colliders)
                        {
                            var dist = (MathF.Abs(entity_pozition.X - (x + c.pozition.x)), MathF.Abs(entity_pozition.Z - (z + c.pozition.z)));
                            if (dist.Item1 < (c.size.x / 2) + (entity_size.width / 2) && dist.Item2 < (c.size.z / 2) + (entity_size.width / 2))
                            {
                                var wys = y + c.pozition.y - (c.size.y / 2);
                                if (entity_top <= wys)
                                {
                                    if (!min_poz_y.HasValue)
                                        min_poz_y = wys;
                                    if (min_poz_y.Value > wys)
                                        min_poz_y = wys;
                                }
                            }
                        }
                    }
            if (!min_poz_y.HasValue)
            {
                entity.Pozition += new Vector3(0, move, 0);
                return false;
            }
            min_poz_y -= entity_size.height;
            if (min_poz_y < entity_pozition.Y + move)
            {
                var poz = entity.PozitionByCenter;
                poz.Y = min_poz_y.Value;
                entity.PozitionByCenter = poz;
                return true;
            }
            else
            {
                entity.Pozition += new Vector3(0, move, 0);
                return false;
            }
        }
    }

    /// <summary>
    /// Mów instancje <paramref name="entity"/> w osi X o <paramref name="move"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="move"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool MoveByX(LivingEntity entity, float move)
    {
        if (move == 0)
            return false;
        var world = entity.Dimention!.World;
        var entity_size = ((Mob)entity.Entity).GetMobSize(entity);
        var entity_pozition = entity.PozitionByCenter;
        var entity_center = entity_pozition + new Vector3(0, entity_size.height / 2, 0);
        ((int min, int max) z, (int min, int max) y) entity_plane;
        (int min, int max) slide;
        {
            var plane_f = (
                (entity_center.Z - (entity_size.width / 2), entity_center.Z + (entity_size.width / 2)),
                (entity_pozition.Y, entity_pozition.Y + entity_size.height));
            entity_plane = (
                (MathC.Section(plane_f.Item1.Item1, 1), MathC.Section(plane_f.Item1.Item2, 1)),
                (MathC.Section(plane_f.Item2.Item1, 1), MathC.Section(plane_f.Item2.Item2, 1)));

        }
        if (move < 0)
        {
            float? min = null;
            {
                var slide_f = (entity_pozition.X - (entity_size.width / 2) + move, entity_pozition.X - (entity_size.width / 2));
                slide = (MathC.Section(slide_f.Item1, 1), MathC.Section(slide_f.Item2, 1));
            }
            float entity_side = entity_center.X - (entity_size.width / 2);
            for (int x = slide.min; x < slide.max + 1; x++)
                for (int z = entity_plane.z.min; z < entity_plane.z.max + 1; z++)
                    for (int y = entity_plane.y.min; y < entity_plane.y.max + 1; y++)
                    {
                        IEnumerable<Block.BlockCollider> colliders = world.GetBlock(x, y, z).Cast(b =>
                            b.Block.GetPhisicCollision(new() { pozition = (x, y, z), block = b, world = world }));
                        foreach (var c in colliders)
                        {
                            var dist = (MathF.Abs(entity_center.Z - (z + c.pozition.z)), MathF.Abs(entity_center.Y - (y + c.pozition.y)));
                            if (dist.Item1 < (c.size.z / 2) + (entity_size.width / 2) && dist.Item2 < (c.size.y / 2) + (entity_size.height / 2))
                            {
                                var poz = x + c.pozition.x + (c.size.x / 2);
                                if (poz <= entity_side)
                                {
                                    if (!min.HasValue)
                                        min = poz;
                                    if (min.Value < poz)
                                        min = poz;
                                }
                            }
                        }
                    }
            if (!min.HasValue)
            {
                entity.Pozition += new Vector3(move, 0, 0);
                return false;
            }
            min += entity_size.width / 2;
            if (min > entity_pozition.X + move)
            {
                entity_pozition.X = min.Value;
                entity.PozitionByCenter = entity_pozition;
                return true;
            }
            else
            {
                entity.Pozition += new Vector3(move, 0, 0);
                return false;
            }
        }
        else
        {
            float? max = null;
            {
                var slide_f = (entity_pozition.X + (entity_size.width / 2), entity_pozition.X + (entity_size.width / 2) + move);
                slide = (MathC.Section(slide_f.Item1, 1), MathC.Section(slide_f.Item2, 1));
            }
            float entity_side = entity_center.X + (entity_size.width / 2);
            for (int x = slide.min; x < slide.max + 1; x++)
                for (int z = entity_plane.z.min; z < entity_plane.z.max + 1; z++)
                    for (int y = entity_plane.y.min; y < entity_plane.y.max + 1; y++)
                    {
                        IEnumerable<Block.BlockCollider> colliders = world.GetBlock(x, y, z).Cast(b =>
                            b.Block.GetPhisicCollision(new() { pozition = (x, y, z), block = b, world = world }));
                        foreach (var c in colliders)
                        {
                            var dist = (MathF.Abs(entity_center.Z - (z + c.pozition.z)), MathF.Abs(entity_center.Y - (y + c.pozition.y)));
                            if (dist.Item1 < (c.size.z / 2) + (entity_size.width / 2) && dist.Item2 < (c.size.y / 2) + (entity_size.height / 2))
                            {
                                var poz = x + c.pozition.x - (c.size.x / 2);
                                if (poz >= entity_side)
                                {
                                    if (!max.HasValue)
                                        max = poz;
                                    if (max.Value > poz)
                                        max = poz;
                                }
                            }
                        }
                    }
            if (!max.HasValue)
            {
                entity.Pozition += new Vector3(move, 0, 0);
                return false;
            }
            max -= entity_size.width / 2;
            if (max < entity_pozition.X + move)
            {
                entity_pozition.X = max.Value;
                entity.PozitionByCenter = entity_pozition;
                return true;
            }
            else
            {
                entity.Pozition += new Vector3(move, 0, 0);
                return false;
            }
        }
    }

    /// <summary>
    /// Mów instancje <paramref name="entity"/> w osi Z o <paramref name="move"/>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="move"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool MoveByZ(LivingEntity entity, float move)
    {
        if (move == 0)
            return false;
        var world = entity.Dimention!.World;
        var entity_size = ((Mob)entity.Entity).GetMobSize(entity);
        var entity_pozition = entity.PozitionByCenter;
        var entity_center = entity_pozition + new Vector3(0, entity_size.height / 2, 0);
        ((int min, int max) x, (int min, int max) y) entity_plane;
        (int min, int max) slide;
        {
            var plane_f = (
                (entity_center.X - (entity_size.width / 2), entity_center.X + (entity_size.width / 2)),
                (entity_pozition.Y, entity_pozition.Y + entity_size.height));
            entity_plane = (
                (MathC.Section(plane_f.Item1.Item1, 1), MathC.Section(plane_f.Item1.Item2, 1)),
                (MathC.Section(plane_f.Item2.Item1, 1), MathC.Section(plane_f.Item2.Item2, 1)));

        }
        if (move < 0)
        {
            float? min = null;
            {
                var slide_f = (entity_pozition.Z - (entity_size.width / 2) + move, entity_pozition.Z - (entity_size.width / 2));
                slide = (MathC.Section(slide_f.Item1, 1), MathC.Section(slide_f.Item2, 1));
            }
            float entity_side = entity_center.Z - (entity_size.width / 2);
            for (int z = slide.min; z < slide.max + 1; z++)
                for (int x = entity_plane.x.min; x < entity_plane.x.max + 1; x++)
                    for (int y = entity_plane.y.min; y < entity_plane.y.max + 1; y++)
                    {
                        IEnumerable<Block.BlockCollider> colliders = world.GetBlock(x, y, z).Cast(b =>
                            b.Block.GetPhisicCollision(new() { pozition = (x, y, z), block = b, world = world }));
                        foreach (var c in colliders)
                        {
                            var dist = (MathF.Abs(entity_center.X - (x + c.pozition.x)), MathF.Abs(entity_center.Y - (y + c.pozition.y)));
                            if (dist.Item1 < (c.size.x / 2) + (entity_size.width / 2) && dist.Item2 < (c.size.y / 2) + (entity_size.height / 2))
                            {
                                var poz = z + c.pozition.z + (c.size.z / 2);
                                if (poz <= entity_side)
                                {
                                    if (!min.HasValue)
                                        min = poz;
                                    if (min.Value < poz)
                                        min = poz;
                                }
                            }
                        }
                    }
            if (!min.HasValue)
            {
                entity.Pozition += new Vector3(0, 0, move);
                return false;
            }
            min += entity_size.width / 2;
            if (min > entity_pozition.Z + move)
            {
                entity_pozition.Z = min.Value;
                entity.PozitionByCenter = entity_pozition;
                return true;
            }
            else
            {
                entity.Pozition += new Vector3(0, 0, move);
                return false;
            }
        }
        else
        {
            float? max = null;
            {
                var slide_f = (entity_pozition.Z + (entity_size.width / 2), entity_pozition.Z + (entity_size.width / 2) + move);
                slide = (MathC.Section(slide_f.Item1, 1), MathC.Section(slide_f.Item2, 1));
            }
            float entity_side = entity_center.Z + (entity_size.width / 2);
            for (int z = slide.min; z < slide.max + 1; z++)
                for (int x = entity_plane.x.min; x < entity_plane.x.max + 1; x++)
                    for (int y = entity_plane.y.min; y < entity_plane.y.max + 1; y++)
                    {
                        IEnumerable<Block.BlockCollider> colliders = world.GetBlock(x, y, z).Cast(b =>
                            b.Block.GetPhisicCollision(new() { pozition = (x, y, z), block = b, world = world }));
                        foreach (var c in colliders)
                        {
                            var dist = (MathF.Abs(entity_center.X - (x + c.pozition.x)), MathF.Abs(entity_center.Y - (y + c.pozition.y)));
                            if (dist.Item1 < (c.size.x / 2) + (entity_size.width / 2) && dist.Item2 < (c.size.y / 2) + (entity_size.height / 2))
                            {
                                var poz = z + c.pozition.z - (c.size.z / 2);
                                if (poz >= entity_side)
                                {
                                    if (!max.HasValue)
                                        max = poz;
                                    if (max.Value > poz)
                                        max = poz;
                                }
                            }
                        }
                    }
            if (!max.HasValue)
            {
                entity.Pozition += new Vector3(0, 0, move);
                return false;
            }
            max -= entity_size.width / 2;
            if (max < entity_pozition.Z + move)
            {
                entity_pozition.Z = max.Value;
                entity.PozitionByCenter = entity_pozition;
                return true;
            }
            else
            {
                entity.Pozition += new Vector3(0, 0, move);
                return false;
            }
        }
    }
    
    /// <summary>
    /// Czy byt stoji na ziemi
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static bool IsOnGround(LivingEntity entity)
    {
        var world = entity.Dimention!.World;
        var entity_pozition = entity.PozitionByCenter;
        ((int min, int max) x, (int min, int max) z) entity_plane;
        var entity_size = ((Mob)entity.Entity).GetMobSize(entity);
        {
            ((float min, float max) x, (float min, float max) z) entity_plane_f;
            entity_plane_f = (
                (entity_pozition.X - (entity_size.width / 2), entity_pozition.X + (entity_size.width / 2)),
                (entity_pozition.Z - (entity_size.width / 2), entity_pozition.Z + (entity_size.width / 2)));
            entity_plane = (
                (MathC.Section(entity_plane_f.x.min, 1), MathC.Section(entity_plane_f.x.max, 1)),
                (MathC.Section(entity_plane_f.z.min, 1), MathC.Section(entity_plane_f.z.max, 1)));
        }
        var start_y = MathC.Section(entity_pozition.Y, 1);
        for (int y = start_y - 1; y < start_y + 1; y++)
            for (int x = entity_plane.x.min; x < entity_plane.x.max + 1; x++)
                for (int z = entity_plane.z.min; z < entity_plane.z.max + 1; z++)
                {
                    IEnumerable<Block.BlockCollider> colliders = world.GetBlock(x, y, z).Cast(b =>
                            b.Block.GetPhisicCollision(new() { pozition = (x, y, z), block = b, world = world }));
                    foreach (var c in colliders)
                    {
                        var dist = (MathF.Abs(entity_pozition.X - (x + c.pozition.x)), MathF.Abs(entity_pozition.Z - (z + c.pozition.z)));
                        if (dist.Item1 < (c.size.x / 2) + (entity_size.width / 2) && dist.Item2 < (c.size.z / 2) + (entity_size.width / 2))
                        {
                            var wys = y + c.pozition.y + (c.size.y / 2);
                            if (wys == entity_pozition.Y)
                                return true;
                        }
                    }
                }

        return false;
    }
    
    /// <summary>
    /// Gdzie instancja się patrzy
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static ImLookingAtRezult? ImLookingAt(LivingEntity entity, float distance)
    {
        if (entity.Dimention == null)
            throw new("Entity need to be in dimention");
        if (entity.Entity is not Mob)
            throw new("This method work only for mob or subclass");
        var cam_h = ((Mob)entity.Entity).GetCameraHeight(entity);
        var start_poz = entity.PozitionByCenter + new Vector3(0, cam_h, 0);
        Vector2 cam_rot = new();
        {
            var obj = entity.Data.Get("camera_rot");
            if (obj is Vector2)
                cam_rot = (Vector2)obj;
        }
        return ImLookingAt(entity.Dimention!.World, start_poz, cam_rot, distance);
    }

    /// <summary>
    /// Gdzie trafia promień interakcji
    /// </summary>
    /// <param name="world">Świat w którym będzie interakcja będzie sprawdzana</param>
    /// <param name="start">Pozycja wyjściowa dla interakcji</param>
    /// <param name="lootRotation">Orjentacja promienia dla interakcji</param>
    /// <param name="distance">Odległość na której interakcja będzie sprawdzana</param>
    /// <returns></returns>
    public static ImLookingAtRezult? ImLookingAt(World world, Vector3 start, Vector2 lootRotation, float distance)
    {
        var ray = MathC.CreateRay(start, lootRotation, distance);
        foreach(var bl_poz in MathC.CollidBlocks(start, lootRotation, distance))
        {
            var block = world.GetBlock(bl_poz);
            var colliders = block.Block.GetInteractionCollision(new() { block = block, pozition = bl_poz.ToTumple(), world = world });
            ((int index, Block.BlockSide side) collider, float distance)? collizion = null;
            int i = -1;
            foreach(var c in colliders)
            {
                var l_poz = bl_poz + c.pozition.ToVector();
                var coll = ray.CastBox(l_poz, c.size.ToVector());
                i++;
                if (coll.HasValue)
                {
                    var wyn = coll.Value.Enter ?? coll.Value.Exit ?? new();
                    var dis = wyn.Point.Distance(start);
                    if(!collizion.HasValue)
                    {
                        collizion = ((i, wyn.Side), dis);
                        continue;
                    }
                    if(collizion?.distance > dis)
                    {
                        collizion = ((i, wyn.Side), dis);
                        continue;
                    }
                }
            }
            if(collizion.HasValue)
                return new(bl_poz.ToTumple(), collizion.Value.collider.index, collizion.Value.collider.side);
        }

        return null;
    }

    public struct ImLookingAtRezult
    {
        public ImLookingAtRezult((int x, int y, int z) blockPozition, int hitBoxIndex, Block.BlockSide blockSide)
        {
            BlockPozition = blockPozition;
            HitBoxIndex = hitBoxIndex;
            BlockSide = blockSide;
        }

        public (int x, int y, int z) BlockPozition { get; set; }
        public int HitBoxIndex { get; set; }
        public Block.BlockSide BlockSide { get; set; }
    }
}
