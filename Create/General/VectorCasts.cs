using System.Numerics;
using System.Runtime.CompilerServices;
using Silk.NET.Maths;
using Do = System.Runtime.CompilerServices.MethodImplAttribute;
// ReSharper disable InconsistentNaming, ArrangeAccessorOwnerBody, UseWithExpressionToCopyStruct, UnusedMember.Global, UnusedType.Global, IdentifierTypo

namespace Create.General;

/// <summary> Provides swizzle properties for vector types. </summary>
/// <remarks>When using setters on structs retrieved from auto-properties, the setter will mutate a copy, not the original property.</remarks>
public static class VectorCasts
{
    private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
    
    extension(ref Vector2 v)
    {
        public Vector2 XX { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2 XY { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2 YX { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2 YY { [Do(Inline)]get => new(v.Y, v.Y); }

        public Vector3 XXX { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3 XXY { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3 XYX { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3 XYY { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3 YXX { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3 YXY { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3 YYX { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3 YYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }

        public Vector4 XXXX { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4 XXXY { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4 XXYX { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4 XXYY { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4 XYXX { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4 XYXY { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4 XYYX { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4 XYYY { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4 YXXX { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4 YXXY { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4 YXYX { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4 YXYY { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4 YYXX { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4 YYXY { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4 YYYX { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4 YYYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
    }
    
    extension<T>(ref Vector2D<T> v) where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
    {
        public Vector2D<T> XX { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2D<T> XY { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2D<T> YX { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2D<T> YY { [Do(Inline)]get => new(v.Y, v.Y); }

        public Vector3D<T> XXX { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3D<T> XXY { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3D<T> XYX { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3D<T> XYY { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3D<T> YXX { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3D<T> YXY { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3D<T> YYX { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3D<T> YYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }

        public Vector4D<T> XXXX { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4D<T> XXXY { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4D<T> XXYX { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4D<T> XXYY { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4D<T> XYXX { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4D<T> XYXY { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4D<T> XYYX { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4D<T> XYYY { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4D<T> YXXX { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4D<T> YXXY { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4D<T> YXYX { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4D<T> YXYY { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4D<T> YYXX { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4D<T> YYXY { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4D<T> YYYX { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4D<T> YYYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
    }
    
    extension(ref Vector3 v)
    {
        public Vector2 XX { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2 XY { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2 XZ { [Do(Inline)]get => new(v.X, v.Z); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; } }
        public Vector2 YX { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2 YY { [Do(Inline)]get => new(v.Y, v.Y); }
        public Vector2 YZ { [Do(Inline)]get => new(v.Y, v.Z); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; } }
        public Vector2 ZX { [Do(Inline)]get => new(v.Z, v.X); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; } }
        public Vector2 ZY { [Do(Inline)]get => new(v.Z, v.Y); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; } }
        public Vector2 ZZ { [Do(Inline)]get => new(v.Z, v.Z); }

        public Vector3 XXX { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3 XXY { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3 XXZ { [Do(Inline)]get => new(v.X, v.X, v.Z); }
        public Vector3 XYX { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3 XYY { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3 XYZ { [Do(Inline)]get => new(v.X, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3 XZX { [Do(Inline)]get => new(v.X, v.Z, v.X); }
        public Vector3 XZY { [Do(Inline)]get => new(v.X, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3 XZZ { [Do(Inline)]get => new(v.X, v.Z, v.Z); }
        public Vector3 YXX { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3 YXY { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3 YXZ { [Do(Inline)]get => new(v.Y, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3 YYX { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3 YYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }
        public Vector3 YYZ { [Do(Inline)]get => new(v.Y, v.Y, v.Z); }
        public Vector3 YZX { [Do(Inline)]get => new(v.Y, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3 YZY { [Do(Inline)]get => new(v.Y, v.Z, v.Y); }
        public Vector3 YZZ { [Do(Inline)]get => new(v.Y, v.Z, v.Z); }
        public Vector3 ZXX { [Do(Inline)]get => new(v.Z, v.X, v.X); }
        public Vector3 ZXY { [Do(Inline)]get => new(v.Z, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3 ZXZ { [Do(Inline)]get => new(v.Z, v.X, v.Z); }
        public Vector3 ZYX { [Do(Inline)]get => new(v.Z, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3 ZYY { [Do(Inline)]get => new(v.Z, v.Y, v.Y); }
        public Vector3 ZYZ { [Do(Inline)]get => new(v.Z, v.Y, v.Z); }
        public Vector3 ZZX { [Do(Inline)]get => new(v.Z, v.Z, v.X); }
        public Vector3 ZZY { [Do(Inline)]get => new(v.Z, v.Z, v.Y); }
        public Vector3 ZZZ { [Do(Inline)]get => new(v.Z, v.Z, v.Z); }

        public Vector4 XXXX { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4 XXXY { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4 XXXZ { [Do(Inline)]get => new(v.X, v.X, v.X, v.Z); }
        public Vector4 XXYX { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4 XXYY { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4 XXYZ { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Z); }
        public Vector4 XXZX { [Do(Inline)]get => new(v.X, v.X, v.Z, v.X); }
        public Vector4 XXZY { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Y); }
        public Vector4 XXZZ { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Z); }
        public Vector4 XYXX { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4 XYXY { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4 XYXZ { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Z); }
        public Vector4 XYYX { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4 XYYY { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4 XYYZ { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Z); }
        public Vector4 XYZY { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Y); }
        public Vector4 XYZZ { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Z); }
        public Vector4 XZXX { [Do(Inline)]get => new(v.X, v.Z, v.X, v.X); }
        public Vector4 XZXY { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Y); }
        public Vector4 XZXZ { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Z); }
        public Vector4 XZYX { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.X); }
        public Vector4 XZYY { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Y); }
        public Vector4 XZYZ { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Z); }
        public Vector4 XZZX { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.X); }
        public Vector4 XZZY { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Y); }
        public Vector4 XZZZ { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Z); }
        public Vector4 YXXX { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4 YXXY { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4 YXXZ { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Z); }
        public Vector4 YXYX { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4 YXYY { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4 YXYZ { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Z); }
        public Vector4 YXZX { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.X); }
        public Vector4 YXZY { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Y); }
        public Vector4 YXZZ { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Z); }
        public Vector4 YYXX { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4 YYXY { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4 YYXZ { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Z); }
        public Vector4 YYYX { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4 YYYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
        public Vector4 YYYZ { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Z); }
        public Vector4 YYZX { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.X); }
        public Vector4 YYZY { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Y); }
        public Vector4 YYZZ { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Z); }
        public Vector4 YZXX { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.X); }
        public Vector4 YZXY { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Y); }
        public Vector4 YZXZ { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Z); }
        public Vector4 YZYX { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.X); }
        public Vector4 YZYY { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Y); }
        public Vector4 YZYZ { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Z); }
        public Vector4 YZZX { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.X); }
        public Vector4 YZZY { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Y); }
        public Vector4 YZZZ { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Z); }
        public Vector4 ZXXX { [Do(Inline)]get => new(v.Z, v.X, v.X, v.X); }
        public Vector4 ZXXY { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Y); }
        public Vector4 ZXXZ { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Z); }
        public Vector4 ZXYX { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.X); }
        public Vector4 ZXYY { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Y); }
        public Vector4 ZXYZ { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Z); }
        public Vector4 ZXZX { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.X); }
        public Vector4 ZXZY { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Y); }
        public Vector4 ZXZZ { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Z); }
        public Vector4 ZYXX { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.X); }
        public Vector4 ZYXY { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Y); }
        public Vector4 ZYXZ { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Z); }
        public Vector4 ZYYX { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.X); }
        public Vector4 ZYYY { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Y); }
        public Vector4 ZYYZ { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Z); }
        public Vector4 ZYZX { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.X); }
        public Vector4 ZYZY { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Y); }
        public Vector4 ZYZZ { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Z); }
        public Vector4 ZZXX { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.X); }
        public Vector4 ZZXY { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Y); }
        public Vector4 ZZXZ { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Z); }
        public Vector4 ZZYX { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.X); }
        public Vector4 ZZYY { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Y); }
        public Vector4 ZZYZ { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Z); }
        public Vector4 ZZZX { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.X); }
        public Vector4 ZZZY { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Y); }
        public Vector4 ZZZZ { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Z); }
    }
    
    extension<T>(ref Vector3D<T> v) where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
    {
        public Vector2D<T> XX { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2D<T> XY { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2D<T> XZ { [Do(Inline)]get => new(v.X, v.Z); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; } }
        public Vector2D<T> YX { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2D<T> YY { [Do(Inline)]get => new(v.Y, v.Y); }
        public Vector2D<T> YZ { [Do(Inline)]get => new(v.Y, v.Z); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; } }
        public Vector2D<T> ZX { [Do(Inline)]get => new(v.Z, v.X); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; } }
        public Vector2D<T> ZY { [Do(Inline)]get => new(v.Z, v.Y); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; } }
        public Vector2D<T> ZZ { [Do(Inline)]get => new(v.Z, v.Z); }

        public Vector3D<T> XXX { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3D<T> XXY { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3D<T> XXZ { [Do(Inline)]get => new(v.X, v.X, v.Z); }
        public Vector3D<T> XYX { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3D<T> XYY { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3D<T> XYZ { [Do(Inline)]get => new(v.X, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3D<T> XZX { [Do(Inline)]get => new(v.X, v.Z, v.X); }
        public Vector3D<T> XZY { [Do(Inline)]get => new(v.X, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3D<T> XZZ { [Do(Inline)]get => new(v.X, v.Z, v.Z); }
        public Vector3D<T> YXX { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3D<T> YXY { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3D<T> YXZ { [Do(Inline)]get => new(v.Y, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3D<T> YYX { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3D<T> YYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }
        public Vector3D<T> YYZ { [Do(Inline)]get => new(v.Y, v.Y, v.Z); }
        public Vector3D<T> YZX { [Do(Inline)]get => new(v.Y, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3D<T> YZY { [Do(Inline)]get => new(v.Y, v.Z, v.Y); }
        public Vector3D<T> YZZ { [Do(Inline)]get => new(v.Y, v.Z, v.Z); }
        public Vector3D<T> ZXX { [Do(Inline)]get => new(v.Z, v.X, v.X); }
        public Vector3D<T> ZXY { [Do(Inline)]get => new(v.Z, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3D<T> ZXZ { [Do(Inline)]get => new(v.Z, v.X, v.Z); }
        public Vector3D<T> ZYX { [Do(Inline)]get => new(v.Z, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3D<T> ZYY { [Do(Inline)]get => new(v.Z, v.Y, v.Y); }
        public Vector3D<T> ZYZ { [Do(Inline)]get => new(v.Z, v.Y, v.Z); }
        public Vector3D<T> ZZX { [Do(Inline)]get => new(v.Z, v.Z, v.X); }
        public Vector3D<T> ZZY { [Do(Inline)]get => new(v.Z, v.Z, v.Y); }
        public Vector3D<T> ZZZ { [Do(Inline)]get => new(v.Z, v.Z, v.Z); }

        public Vector4D<T> XXXX { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4D<T> XXXY { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4D<T> XXXZ { [Do(Inline)]get => new(v.X, v.X, v.X, v.Z); }
        public Vector4D<T> XXYX { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4D<T> XXYY { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4D<T> XXYZ { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Z); }
        public Vector4D<T> XXZX { [Do(Inline)]get => new(v.X, v.X, v.Z, v.X); }
        public Vector4D<T> XXZY { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Y); }
        public Vector4D<T> XXZZ { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Z); }
        public Vector4D<T> XYXX { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4D<T> XYXY { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4D<T> XYXZ { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Z); }
        public Vector4D<T> XYYX { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4D<T> XYYY { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4D<T> XYYZ { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Z); }
        public Vector4D<T> XYZY { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Y); }
        public Vector4D<T> XYZZ { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Z); }
        public Vector4D<T> XZXX { [Do(Inline)]get => new(v.X, v.Z, v.X, v.X); }
        public Vector4D<T> XZXY { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Y); }
        public Vector4D<T> XZXZ { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Z); }
        public Vector4D<T> XZYX { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.X); }
        public Vector4D<T> XZYY { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Y); }
        public Vector4D<T> XZYZ { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Z); }
        public Vector4D<T> XZZX { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.X); }
        public Vector4D<T> XZZY { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Y); }
        public Vector4D<T> XZZZ { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Z); }
        public Vector4D<T> YXXX { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4D<T> YXXY { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4D<T> YXXZ { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Z); }
        public Vector4D<T> YXYX { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4D<T> YXYY { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4D<T> YXYZ { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Z); }
        public Vector4D<T> YXZX { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.X); }
        public Vector4D<T> YXZY { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Y); }
        public Vector4D<T> YXZZ { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Z); }
        public Vector4D<T> YYXX { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4D<T> YYXY { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4D<T> YYXZ { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Z); }
        public Vector4D<T> YYYX { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4D<T> YYYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
        public Vector4D<T> YYYZ { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Z); }
        public Vector4D<T> YYZX { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.X); }
        public Vector4D<T> YYZY { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Y); }
        public Vector4D<T> YYZZ { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Z); }
        public Vector4D<T> YZXX { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.X); }
        public Vector4D<T> YZXY { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Y); }
        public Vector4D<T> YZXZ { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Z); }
        public Vector4D<T> YZYX { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.X); }
        public Vector4D<T> YZYY { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Y); }
        public Vector4D<T> YZYZ { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Z); }
        public Vector4D<T> YZZX { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.X); }
        public Vector4D<T> YZZY { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Y); }
        public Vector4D<T> YZZZ { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Z); }
        public Vector4D<T> ZXXX { [Do(Inline)]get => new(v.Z, v.X, v.X, v.X); }
        public Vector4D<T> ZXXY { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Y); }
        public Vector4D<T> ZXXZ { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Z); }
        public Vector4D<T> ZXYX { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.X); }
        public Vector4D<T> ZXYY { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Y); }
        public Vector4D<T> ZXYZ { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Z); }
        public Vector4D<T> ZXZX { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.X); }
        public Vector4D<T> ZXZY { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Y); }
        public Vector4D<T> ZXZZ { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Z); }
        public Vector4D<T> ZYXX { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.X); }
        public Vector4D<T> ZYXY { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Y); }
        public Vector4D<T> ZYXZ { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Z); }
        public Vector4D<T> ZYYX { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.X); }
        public Vector4D<T> ZYYY { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Y); }
        public Vector4D<T> ZYYZ { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Z); }
        public Vector4D<T> ZYZX { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.X); }
        public Vector4D<T> ZYZY { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Y); }
        public Vector4D<T> ZYZZ { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Z); }
        public Vector4D<T> ZZXX { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.X); }
        public Vector4D<T> ZZXY { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Y); }
        public Vector4D<T> ZZXZ { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Z); }
        public Vector4D<T> ZZYX { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.X); }
        public Vector4D<T> ZZYY { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Y); }
        public Vector4D<T> ZZYZ { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Z); }
        public Vector4D<T> ZZZX { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.X); }
        public Vector4D<T> ZZZY { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Y); }
        public Vector4D<T> ZZZZ { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Z); }
    }

    extension(ref Vector4 v)
    {
        public Vector2 XX { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2 XY { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2 XZ { [Do(Inline)]get => new(v.X, v.Z); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; } }
        public Vector2 XW { [Do(Inline)]get => new(v.X, v.W); [Do(Inline)]set { v.X = value.X; v.W = value.Y; } }
        public Vector2 YX { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2 YY { [Do(Inline)]get => new(v.Y, v.Y); }
        public Vector2 YZ { [Do(Inline)]get => new(v.Y, v.Z); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; } }
        public Vector2 YW { [Do(Inline)]get => new(v.Y, v.W); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; } }
        public Vector2 ZX { [Do(Inline)]get => new(v.Z, v.X); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; } }
        public Vector2 ZY { [Do(Inline)]get => new(v.Z, v.Y); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; } }
        public Vector2 ZZ { [Do(Inline)]get => new(v.Z, v.Z); }
        public Vector2 ZW { [Do(Inline)]get => new(v.Z, v.W); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; } }
        public Vector2 WX { [Do(Inline)]get => new(v.W, v.X); [Do(Inline)]set { v.W = value.X; v.X = value.Y; } }
        public Vector2 WY { [Do(Inline)]get => new(v.W, v.Y); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; } }
        public Vector2 WZ { [Do(Inline)]get => new(v.W, v.Z); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; } }
        public Vector2 WW { [Do(Inline)]get => new(v.W, v.W); }
        
        public Vector3 XXX { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3 XXY { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3 XXZ { [Do(Inline)]get => new(v.X, v.X, v.Z); }
        public Vector3 XXW { [Do(Inline)]get => new(v.X, v.X, v.W); }
        public Vector3 XYX { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3 XYY { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3 XYZ { [Do(Inline)]get => new(v.X, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3 XYW { [Do(Inline)]get => new(v.X, v.Y, v.W); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.W = value.Z; } }
        public Vector3 XZX { [Do(Inline)]get => new(v.X, v.Z, v.X); }
        public Vector3 XZY { [Do(Inline)]get => new(v.X, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3 XZZ { [Do(Inline)]get => new(v.X, v.Z, v.Z); }
        public Vector3 XZW { [Do(Inline)]get => new(v.X, v.Z, v.W); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.W = value.Z; } }
        public Vector3 XWX { [Do(Inline)]get => new(v.X, v.W, v.X); }
        public Vector3 XWY { [Do(Inline)]get => new(v.X, v.W, v.Y); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Y = value.Z; } }
        public Vector3 XWZ { [Do(Inline)]get => new(v.X, v.W, v.Z); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Z = value.Z; } }
        public Vector3 XWW { [Do(Inline)]get => new(v.X, v.W, v.W); }
        public Vector3 YXX { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3 YXY { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3 YXZ { [Do(Inline)]get => new(v.Y, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3 YXW { [Do(Inline)]get => new(v.Y, v.X, v.W); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.W = value.Z; } }
        public Vector3 YYX { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3 YYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }
        public Vector3 YYZ { [Do(Inline)]get => new(v.Y, v.Y, v.Z); }
        public Vector3 YYW { [Do(Inline)]get => new(v.Y, v.Y, v.W); }
        public Vector3 YZX { [Do(Inline)]get => new(v.Y, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3 YZY { [Do(Inline)]get => new(v.Y, v.Z, v.Y); }
        public Vector3 YZZ { [Do(Inline)]get => new(v.Y, v.Z, v.Z); }
        public Vector3 YZW { [Do(Inline)]get => new(v.Y, v.Z, v.W); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.W = value.Z; } }
        public Vector3 YWX { [Do(Inline)]get => new(v.Y, v.W, v.X); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.X = value.Z; } }
        public Vector3 YWY { [Do(Inline)]get => new(v.Y, v.W, v.Y); }
        public Vector3 YWZ { [Do(Inline)]get => new(v.Y, v.W, v.Z); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.Z = value.Z; } }
        public Vector3 YWW { [Do(Inline)]get => new(v.Y, v.W, v.W); }
        public Vector3 ZXX { [Do(Inline)]get => new(v.Z, v.X, v.X); }
        public Vector3 ZXY { [Do(Inline)]get => new(v.Z, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3 ZXZ { [Do(Inline)]get => new(v.Z, v.X, v.Z); }
        public Vector3 ZXW { [Do(Inline)]get => new(v.Z, v.X, v.W); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.W = value.Z; } }
        public Vector3 ZYX { [Do(Inline)]get => new(v.Z, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3 ZYY { [Do(Inline)]get => new(v.Z, v.Y, v.Y); }
        public Vector3 ZYZ { [Do(Inline)]get => new(v.Z, v.Y, v.Z); }
        public Vector3 ZYW { [Do(Inline)]get => new(v.Z, v.Y, v.W); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.W = value.Z; } }
        public Vector3 ZZX { [Do(Inline)]get => new(v.Z, v.Z, v.X); }
        public Vector3 ZZY { [Do(Inline)]get => new(v.Z, v.Z, v.Y); }
        public Vector3 ZZZ { [Do(Inline)]get => new(v.Z, v.Z, v.Z); }
        public Vector3 ZZW { [Do(Inline)]get => new(v.Z, v.Z, v.W); }
        public Vector3 ZWX { [Do(Inline)]get => new(v.Z, v.W, v.X); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.X = value.Z; } }
        public Vector3 ZWY { [Do(Inline)]get => new(v.Z, v.W, v.Y); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.Y = value.Z; } }
        public Vector3 ZWZ { [Do(Inline)]get => new(v.Z, v.W, v.Z); }
        public Vector3 ZWW { [Do(Inline)]get => new(v.Z, v.W, v.W); }
        public Vector3 WXX { [Do(Inline)]get => new(v.W, v.X, v.X); }
        public Vector3 WXY { [Do(Inline)]get => new(v.W, v.X, v.Y); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3 WXZ { [Do(Inline)]get => new(v.W, v.X, v.Z); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3 WXW { [Do(Inline)]get => new(v.W, v.X, v.W); }
        public Vector3 WYX { [Do(Inline)]get => new(v.W, v.Y, v.X); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3 WYY { [Do(Inline)]get => new(v.W, v.Y, v.Y); }
        public Vector3 WYZ { [Do(Inline)]get => new(v.W, v.Y, v.Z); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3 WYW { [Do(Inline)]get => new(v.W, v.Y, v.W); }
        public Vector3 WZX { [Do(Inline)]get => new(v.W, v.Z, v.X); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3 WZY { [Do(Inline)]get => new(v.W, v.Z, v.Y); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3 WZZ { [Do(Inline)]get => new(v.W, v.Z, v.Z); }
        public Vector3 WZW { [Do(Inline)]get => new(v.W, v.Z, v.W); }
        public Vector3 WWX { [Do(Inline)]get => new(v.W, v.W, v.X); }
        public Vector3 WWY { [Do(Inline)]get => new(v.W, v.W, v.Y); }
        public Vector3 WWZ { [Do(Inline)]get => new(v.W, v.W, v.Z); }
        public Vector3 WWW { [Do(Inline)]get => new(v.W, v.W, v.W); }
        
        public Vector4 XXXX { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4 XXXY { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4 XXXZ { [Do(Inline)]get => new(v.X, v.X, v.X, v.Z); }
        public Vector4 XXXW { [Do(Inline)]get => new(v.X, v.X, v.X, v.W); }
        public Vector4 XXYX { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4 XXYY { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4 XXYZ { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Z); }
        public Vector4 XXYW { [Do(Inline)]get => new(v.X, v.X, v.Y, v.W); }
        public Vector4 XXZX { [Do(Inline)]get => new(v.X, v.X, v.Z, v.X); }
        public Vector4 XXZY { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Y); }
        public Vector4 XXZZ { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Z); }
        public Vector4 XXZW { [Do(Inline)]get => new(v.X, v.X, v.Z, v.W); }
        public Vector4 XXWX { [Do(Inline)]get => new(v.X, v.X, v.W, v.X); }
        public Vector4 XXWY { [Do(Inline)]get => new(v.X, v.X, v.W, v.Y); }
        public Vector4 XXWZ { [Do(Inline)]get => new(v.X, v.X, v.W, v.Z); }
        public Vector4 XXWW { [Do(Inline)]get => new(v.X, v.X, v.W, v.W); }
        public Vector4 XYXX { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4 XYXY { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4 XYXZ { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Z); }
        public Vector4 XYXW { [Do(Inline)]get => new(v.X, v.Y, v.X, v.W); }
        public Vector4 XYYX { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4 XYYY { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4 XYYZ { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Z); }
        public Vector4 XYYW { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.W); }
        public Vector4 XYZY { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Y); }
        public Vector4 XYZZ { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Z); }
        public Vector4 XYZW { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.W); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; v.W = value.W; } }
        public Vector4 XYWX { [Do(Inline)]get => new(v.X, v.Y, v.W, v.X); }
        public Vector4 XYWY { [Do(Inline)]get => new(v.X, v.Y, v.W, v.Y); }
        public Vector4 XYWZ { [Do(Inline)]get => new(v.X, v.Y, v.W, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.W = value.Z; v.Z = value.W; } }
        public Vector4 XYWW { [Do(Inline)]get => new(v.X, v.Y, v.W, v.W); }
        public Vector4 XZXX { [Do(Inline)]get => new(v.X, v.Z, v.X, v.X); }
        public Vector4 XZXY { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Y); }
        public Vector4 XZXZ { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Z); }
        public Vector4 XZXW { [Do(Inline)]get => new(v.X, v.Z, v.X, v.W); }
        public Vector4 XZYX { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.X); }
        public Vector4 XZYY { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Y); }
        public Vector4 XZYZ { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Z); }
        public Vector4 XZYW { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.W); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; v.W = value.W; } }
        public Vector4 XZZX { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.X); }
        public Vector4 XZZY { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Y); }
        public Vector4 XZZZ { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Z); }
        public Vector4 XZZW { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.W); }
        public Vector4 XZWX { [Do(Inline)]get => new(v.X, v.Z, v.W, v.X); }
        public Vector4 XZWY { [Do(Inline)]get => new(v.X, v.Z, v.W, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.W = value.Z; v.Y = value.W; } }
        public Vector4 XZWZ { [Do(Inline)]get => new(v.X, v.Z, v.W, v.Z); }
        public Vector4 XZWW { [Do(Inline)]get => new(v.X, v.Z, v.W, v.W); }
        public Vector4 XWXX { [Do(Inline)]get => new(v.X, v.W, v.X, v.X); }
        public Vector4 XWXY { [Do(Inline)]get => new(v.X, v.W, v.X, v.Y); }
        public Vector4 XWXZ { [Do(Inline)]get => new(v.X, v.W, v.X, v.Z); }
        public Vector4 XWXW { [Do(Inline)]get => new(v.X, v.W, v.X, v.W); }
        public Vector4 XWYX { [Do(Inline)]get => new(v.X, v.W, v.Y, v.X); }
        public Vector4 XWYY { [Do(Inline)]get => new(v.X, v.W, v.Y, v.Y); }
        public Vector4 XWYZ { [Do(Inline)]get => new(v.X, v.W, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Y = value.Z; v.Z = value.W; } }
        public Vector4 XWYW { [Do(Inline)]get => new(v.X, v.W, v.Y, v.W); }
        public Vector4 XWZX { [Do(Inline)]get => new(v.X, v.W, v.Z, v.X); }
        public Vector4 XWZY { [Do(Inline)]get => new(v.X, v.W, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Z = value.Z; v.Y = value.W; } }
        public Vector4 XWZZ { [Do(Inline)]get => new(v.X, v.W, v.Z, v.Z); }
        public Vector4 XWZW { [Do(Inline)]get => new(v.X, v.W, v.Z, v.W); }
        public Vector4 XWWX { [Do(Inline)]get => new(v.X, v.W, v.W, v.X); }
        public Vector4 XWWY { [Do(Inline)]get => new(v.X, v.W, v.W, v.Y); }
        public Vector4 XWWZ { [Do(Inline)]get => new(v.X, v.W, v.W, v.Z); }
        public Vector4 XWWW { [Do(Inline)]get => new(v.X, v.W, v.W, v.W); }
        public Vector4 YXXX { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4 YXXY { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4 YXXZ { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Z); }
        public Vector4 YXXW { [Do(Inline)]get => new(v.Y, v.X, v.X, v.W); }
        public Vector4 YXYX { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4 YXYY { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4 YXYZ { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Z); }
        public Vector4 YXYW { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.W); }
        public Vector4 YXZX { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.X); }
        public Vector4 YXZY { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Y); }
        public Vector4 YXZZ { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Z); }
        public Vector4 YXZW { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.W); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; v.W = value.W; } }
        public Vector4 YXWX { [Do(Inline)]get => new(v.Y, v.X, v.W, v.X); }
        public Vector4 YXWY { [Do(Inline)]get => new(v.Y, v.X, v.W, v.Y); }
        public Vector4 YXWZ { [Do(Inline)]get => new(v.Y, v.X, v.W, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.W = value.Z; v.Z = value.W; } }
        public Vector4 YXWW { [Do(Inline)]get => new(v.Y, v.X, v.W, v.W); }
        public Vector4 YYXX { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4 YYXY { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4 YYXZ { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Z); }
        public Vector4 YYXW { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.W); }
        public Vector4 YYYX { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4 YYYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
        public Vector4 YYYZ { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Z); }
        public Vector4 YYYW { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.W); }
        public Vector4 YYZX { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.X); }
        public Vector4 YYZY { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Y); }
        public Vector4 YYZZ { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Z); }
        public Vector4 YYZW { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.W); }
        public Vector4 YYWX { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.X); }
        public Vector4 YYWY { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.Y); }
        public Vector4 YYWZ { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.Z); }
        public Vector4 YYWW { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.W); }
        public Vector4 YZXX { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.X); }
        public Vector4 YZXY { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Y); }
        public Vector4 YZXZ { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Z); }
        public Vector4 YZXW { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.W); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; v.W = value.W; } }
        public Vector4 YZYX { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.X); }
        public Vector4 YZYY { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Y); }
        public Vector4 YZYZ { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Z); }
        public Vector4 YZYW { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.W); }
        public Vector4 YZZX { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.X); }
        public Vector4 YZZY { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Y); }
        public Vector4 YZZZ { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Z); }
        public Vector4 YZZW { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.W); }
        public Vector4 YZWX { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.W = value.Z; v.X = value.W; } }
        public Vector4 YZWY { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.Y); }
        public Vector4 YZWZ { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.Z); }
        public Vector4 YZWW { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.W); }
        public Vector4 YWXX { [Do(Inline)]get => new(v.Y, v.W, v.X, v.X); }
        public Vector4 YWXY { [Do(Inline)]get => new(v.Y, v.W, v.X, v.Y); }
        public Vector4 YWXZ { [Do(Inline)]get => new(v.Y, v.W, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.X = value.Z; v.Z = value.W; } }
        public Vector4 YWXW { [Do(Inline)]get => new(v.Y, v.W, v.X, v.W); }
        public Vector4 YWYX { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.X); }
        public Vector4 YWYY { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.Y); }
        public Vector4 YWYZ { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.Z); }
        public Vector4 YWYW { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.W); }
        public Vector4 YWZX { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.Z = value.Z; v.X = value.W; } }
        public Vector4 YWZY { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.Y); }
        public Vector4 YWZZ { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.Z); }
        public Vector4 YWZW { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.W); }
        public Vector4 YWWX { [Do(Inline)]get => new(v.Y, v.W, v.W, v.X); }
        public Vector4 YWWY { [Do(Inline)]get => new(v.Y, v.W, v.W, v.Y); }
        public Vector4 YWWZ { [Do(Inline)]get => new(v.Y, v.W, v.W, v.Z); }
        public Vector4 YWWW { [Do(Inline)]get => new(v.Y, v.W, v.W, v.W); }
        public Vector4 ZXXX { [Do(Inline)]get => new(v.Z, v.X, v.X, v.X); }
        public Vector4 ZXXY { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Y); }
        public Vector4 ZXXZ { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Z); }
        public Vector4 ZXXW { [Do(Inline)]get => new(v.Z, v.X, v.X, v.W); }
        public Vector4 ZXYX { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.X); }
        public Vector4 ZXYY { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Y); }
        public Vector4 ZXYZ { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Z); }
        public Vector4 ZXYW { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.W); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; v.W = value.W; } }
        public Vector4 ZXZX { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.X); }
        public Vector4 ZXZY { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Y); }
        public Vector4 ZXZZ { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Z); }
        public Vector4 ZXZW { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.W); }
        public Vector4 ZXWX { [Do(Inline)]get => new(v.Z, v.X, v.W, v.X); }
        public Vector4 ZXWY { [Do(Inline)]get => new(v.Z, v.X, v.W, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.W = value.Z; v.Y = value.W; } }
        public Vector4 ZXWZ { [Do(Inline)]get => new(v.Z, v.X, v.W, v.Z); }
        public Vector4 ZXWW { [Do(Inline)]get => new(v.Z, v.X, v.W, v.W); }
        public Vector4 ZYXX { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.X); }
        public Vector4 ZYXY { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Y); }
        public Vector4 ZYXZ { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Z); }
        public Vector4 ZYXW { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.W); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; v.W = value.W; } }
        public Vector4 ZYYX { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.X); }
        public Vector4 ZYYY { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Y); }
        public Vector4 ZYYZ { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Z); }
        public Vector4 ZYYW { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.W); }
        public Vector4 ZYZX { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.X); }
        public Vector4 ZYZY { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Y); }
        public Vector4 ZYZZ { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Z); }
        public Vector4 ZYZW { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.W); }
        public Vector4 ZYWX { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.W = value.Z; v.X = value.W; } }
        public Vector4 ZYWY { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.Y); }
        public Vector4 ZYWZ { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.Z); }
        public Vector4 ZYWW { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.W); }
        public Vector4 ZZXX { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.X); }
        public Vector4 ZZXY { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Y); }
        public Vector4 ZZXZ { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Z); }
        public Vector4 ZZXW { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.W); }
        public Vector4 ZZYX { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.X); }
        public Vector4 ZZYY { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Y); }
        public Vector4 ZZYZ { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Z); }
        public Vector4 ZZYW { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.W); }
        public Vector4 ZZZX { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.X); }
        public Vector4 ZZZY { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Y); }
        public Vector4 ZZZZ { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Z); }
        public Vector4 ZZZW { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.W); }
        public Vector4 ZZWX { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.X); }
        public Vector4 ZZWY { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.Y); }
        public Vector4 ZZWZ { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.Z); }
        public Vector4 ZZWW { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.W); }
        public Vector4 ZWXX { [Do(Inline)]get => new(v.Z, v.W, v.X, v.X); }
        public Vector4 ZWXY { [Do(Inline)]get => new(v.Z, v.W, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.X = value.Z; v.Y = value.W; } }
        public Vector4 ZWXZ { [Do(Inline)]get => new(v.Z, v.W, v.X, v.Z); }
        public Vector4 ZWXW { [Do(Inline)]get => new(v.Z, v.W, v.X, v.W); }
        public Vector4 ZWYX { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.Y = value.Z; v.X = value.W; } }
        public Vector4 ZWYY { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.Y); }
        public Vector4 ZWYZ { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.Z); }
        public Vector4 ZWYW { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.W); }
        public Vector4 ZWZX { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.X); }
        public Vector4 ZWZY { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.Y); }
        public Vector4 ZWZZ { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.Z); }
        public Vector4 ZWZW { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.W); }
        public Vector4 ZWWX { [Do(Inline)]get => new(v.Z, v.W, v.W, v.X); }
        public Vector4 ZWWY { [Do(Inline)]get => new(v.Z, v.W, v.W, v.Y); }
        public Vector4 ZWWZ { [Do(Inline)]get => new(v.Z, v.W, v.W, v.Z); }
        public Vector4 ZWWW { [Do(Inline)]get => new(v.Z, v.W, v.W, v.W); }
        public Vector4 WXXX { [Do(Inline)]get => new(v.W, v.X, v.X, v.X); }
        public Vector4 WXXY { [Do(Inline)]get => new(v.W, v.X, v.X, v.Y); }
        public Vector4 WXXZ { [Do(Inline)]get => new(v.W, v.X, v.X, v.Z); }
        public Vector4 WXXW { [Do(Inline)]get => new(v.W, v.X, v.X, v.W); }
        public Vector4 WXYX { [Do(Inline)]get => new(v.W, v.X, v.Y, v.X); }
        public Vector4 WXYY { [Do(Inline)]get => new(v.W, v.X, v.Y, v.Y); }
        public Vector4 WXYZ { [Do(Inline)]get => new(v.W, v.X, v.Y, v.Z); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Y = value.Z; v.Z = value.W; } }
        public Vector4 WXYW { [Do(Inline)]get => new(v.W, v.X, v.Y, v.W); }
        public Vector4 WXZX { [Do(Inline)]get => new(v.W, v.X, v.Z, v.X); }
        public Vector4 WXZY { [Do(Inline)]get => new(v.W, v.X, v.Z, v.Y); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Z = value.Z; v.Y = value.W; } }
        public Vector4 WXZZ { [Do(Inline)]get => new(v.W, v.X, v.Z, v.Z); }
        public Vector4 WXZW { [Do(Inline)]get => new(v.W, v.X, v.Z, v.W); }
        public Vector4 WXWX { [Do(Inline)]get => new(v.W, v.X, v.W, v.X); }
        public Vector4 WXWY { [Do(Inline)]get => new(v.W, v.X, v.W, v.Y); }
        public Vector4 WXWZ { [Do(Inline)]get => new(v.W, v.X, v.W, v.Z); }
        public Vector4 WXWW { [Do(Inline)]get => new(v.W, v.X, v.W, v.W); }
        public Vector4 WYXX { [Do(Inline)]get => new(v.W, v.Y, v.X, v.X); }
        public Vector4 WYXY { [Do(Inline)]get => new(v.W, v.Y, v.X, v.Y); }
        public Vector4 WYXZ { [Do(Inline)]get => new(v.W, v.Y, v.X, v.Z); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.X = value.Z; v.Z = value.W; } }
        public Vector4 WYXW { [Do(Inline)]get => new(v.W, v.Y, v.X, v.W); }
        public Vector4 WYYX { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.X); }
        public Vector4 WYYY { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.Y); }
        public Vector4 WYYZ { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.Z); }
        public Vector4 WYYW { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.W); }
        public Vector4 WYZX { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.X); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.Z = value.Z; v.X = value.W; } }
        public Vector4 WYZY { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.Y); }
        public Vector4 WYZZ { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.Z); }
        public Vector4 WYZW { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.W); }
        public Vector4 WYWX { [Do(Inline)]get => new(v.W, v.Y, v.W, v.X); }
        public Vector4 WYWY { [Do(Inline)]get => new(v.W, v.Y, v.W, v.Y); }
        public Vector4 WYWZ { [Do(Inline)]get => new(v.W, v.Y, v.W, v.Z); }
        public Vector4 WYWW { [Do(Inline)]get => new(v.W, v.Y, v.W, v.W); }
        public Vector4 WZXX { [Do(Inline)]get => new(v.W, v.Z, v.X, v.X); }
        public Vector4 WZXY { [Do(Inline)]get => new(v.W, v.Z, v.X, v.Y); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.X = value.Z; v.Y = value.W; } }
        public Vector4 WZXZ { [Do(Inline)]get => new(v.W, v.Z, v.X, v.Z); }
        public Vector4 WZXW { [Do(Inline)]get => new(v.W, v.Z, v.X, v.W); }
        public Vector4 WZYX { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.X); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.Y = value.Z; v.X = value.W; } }
        public Vector4 WZYY { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.Y); }
        public Vector4 WZYZ { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.Z); }
        public Vector4 WZYW { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.W); }
        public Vector4 WZZX { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.X); }
        public Vector4 WZZY { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.Y); }
        public Vector4 WZZZ { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.Z); }
        public Vector4 WZZW { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.W); }
        public Vector4 WZWX { [Do(Inline)]get => new(v.W, v.Z, v.W, v.X); }
        public Vector4 WZWY { [Do(Inline)]get => new(v.W, v.Z, v.W, v.Y); }
        public Vector4 WZWZ { [Do(Inline)]get => new(v.W, v.Z, v.W, v.Z); }
        public Vector4 WZWW { [Do(Inline)]get => new(v.W, v.Z, v.W, v.W); }
        public Vector4 WWXX { [Do(Inline)]get => new(v.W, v.W, v.X, v.X); }
        public Vector4 WWXY { [Do(Inline)]get => new(v.W, v.W, v.X, v.Y); }
        public Vector4 WWXZ { [Do(Inline)]get => new(v.W, v.W, v.X, v.Z); }
        public Vector4 WWXW { [Do(Inline)]get => new(v.W, v.W, v.X, v.W); }
        public Vector4 WWYX { [Do(Inline)]get => new(v.W, v.W, v.Y, v.X); }
        public Vector4 WWYY { [Do(Inline)]get => new(v.W, v.W, v.Y, v.Y); }
        public Vector4 WWYZ { [Do(Inline)]get => new(v.W, v.W, v.Y, v.Z); }
        public Vector4 WWYW { [Do(Inline)]get => new(v.W, v.W, v.Y, v.W); }
        public Vector4 WWZX { [Do(Inline)]get => new(v.W, v.W, v.Z, v.X); }
        public Vector4 WWZY { [Do(Inline)]get => new(v.W, v.W, v.Z, v.Y); }
        public Vector4 WWZZ { [Do(Inline)]get => new(v.W, v.W, v.Z, v.Z); }
        public Vector4 WWZW { [Do(Inline)]get => new(v.W, v.W, v.Z, v.W); }
        public Vector4 WWWX { [Do(Inline)]get => new(v.W, v.W, v.W, v.X); }
        public Vector4 WWWY { [Do(Inline)]get => new(v.W, v.W, v.W, v.Y); }
        public Vector4 WWWZ { [Do(Inline)]get => new(v.W, v.W, v.W, v.Z); }
        public Vector4 WWWW { [Do(Inline)]get => new(v.W, v.W, v.W, v.W); }
    }
    
    extension<T>(ref Vector4D<T> v) where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
    {
        public Vector2D<T> XX { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2D<T> XY { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2D<T> XZ { [Do(Inline)]get => new(v.X, v.Z); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; } }
        public Vector2D<T> XW { [Do(Inline)]get => new(v.X, v.W); [Do(Inline)]set { v.X = value.X; v.W = value.Y; } }
        public Vector2D<T> YX { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2D<T> YY { [Do(Inline)]get => new(v.Y, v.Y); }
        public Vector2D<T> YZ { [Do(Inline)]get => new(v.Y, v.Z); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; } }
        public Vector2D<T> YW { [Do(Inline)]get => new(v.Y, v.W); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; } }
        public Vector2D<T> ZX { [Do(Inline)]get => new(v.Z, v.X); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; } }
        public Vector2D<T> ZY { [Do(Inline)]get => new(v.Z, v.Y); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; } }
        public Vector2D<T> ZZ { [Do(Inline)]get => new(v.Z, v.Z); }
        public Vector2D<T> ZW { [Do(Inline)]get => new(v.Z, v.W); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; } }
        public Vector2D<T> WX { [Do(Inline)]get => new(v.W, v.X); [Do(Inline)]set { v.W = value.X; v.X = value.Y; } }
        public Vector2D<T> WY { [Do(Inline)]get => new(v.W, v.Y); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; } }
        public Vector2D<T> WZ { [Do(Inline)]get => new(v.W, v.Z); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; } }
        public Vector2D<T> WW { [Do(Inline)]get => new(v.W, v.W); }
        
        public Vector3D<T> XXX { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3D<T> XXY { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3D<T> XXZ { [Do(Inline)]get => new(v.X, v.X, v.Z); }
        public Vector3D<T> XXW { [Do(Inline)]get => new(v.X, v.X, v.W); }
        public Vector3D<T> XYX { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3D<T> XYY { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3D<T> XYZ { [Do(Inline)]get => new(v.X, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3D<T> XYW { [Do(Inline)]get => new(v.X, v.Y, v.W); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.W = value.Z; } }
        public Vector3D<T> XZX { [Do(Inline)]get => new(v.X, v.Z, v.X); }
        public Vector3D<T> XZY { [Do(Inline)]get => new(v.X, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3D<T> XZZ { [Do(Inline)]get => new(v.X, v.Z, v.Z); }
        public Vector3D<T> XZW { [Do(Inline)]get => new(v.X, v.Z, v.W); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.W = value.Z; } }
        public Vector3D<T> XWX { [Do(Inline)]get => new(v.X, v.W, v.X); }
        public Vector3D<T> XWY { [Do(Inline)]get => new(v.X, v.W, v.Y); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Y = value.Z; } }
        public Vector3D<T> XWZ { [Do(Inline)]get => new(v.X, v.W, v.Z); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Z = value.Z; } }
        public Vector3D<T> XWW { [Do(Inline)]get => new(v.X, v.W, v.W); }
        public Vector3D<T> YXX { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3D<T> YXY { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3D<T> YXZ { [Do(Inline)]get => new(v.Y, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3D<T> YXW { [Do(Inline)]get => new(v.Y, v.X, v.W); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.W = value.Z; } }
        public Vector3D<T> YYX { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3D<T> YYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }
        public Vector3D<T> YYZ { [Do(Inline)]get => new(v.Y, v.Y, v.Z); }
        public Vector3D<T> YYW { [Do(Inline)]get => new(v.Y, v.Y, v.W); }
        public Vector3D<T> YZX { [Do(Inline)]get => new(v.Y, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3D<T> YZY { [Do(Inline)]get => new(v.Y, v.Z, v.Y); }
        public Vector3D<T> YZZ { [Do(Inline)]get => new(v.Y, v.Z, v.Z); }
        public Vector3D<T> YZW { [Do(Inline)]get => new(v.Y, v.Z, v.W); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.W = value.Z; } }
        public Vector3D<T> YWX { [Do(Inline)]get => new(v.Y, v.W, v.X); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.X = value.Z; } }
        public Vector3D<T> YWY { [Do(Inline)]get => new(v.Y, v.W, v.Y); }
        public Vector3D<T> YWZ { [Do(Inline)]get => new(v.Y, v.W, v.Z); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.Z = value.Z; } }
        public Vector3D<T> YWW { [Do(Inline)]get => new(v.Y, v.W, v.W); }
        public Vector3D<T> ZXX { [Do(Inline)]get => new(v.Z, v.X, v.X); }
        public Vector3D<T> ZXY { [Do(Inline)]get => new(v.Z, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3D<T> ZXZ { [Do(Inline)]get => new(v.Z, v.X, v.Z); }
        public Vector3D<T> ZXW { [Do(Inline)]get => new(v.Z, v.X, v.W); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.W = value.Z; } }
        public Vector3D<T> ZYX { [Do(Inline)]get => new(v.Z, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3D<T> ZYY { [Do(Inline)]get => new(v.Z, v.Y, v.Y); }
        public Vector3D<T> ZYZ { [Do(Inline)]get => new(v.Z, v.Y, v.Z); }
        public Vector3D<T> ZYW { [Do(Inline)]get => new(v.Z, v.Y, v.W); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.W = value.Z; } }
        public Vector3D<T> ZZX { [Do(Inline)]get => new(v.Z, v.Z, v.X); }
        public Vector3D<T> ZZY { [Do(Inline)]get => new(v.Z, v.Z, v.Y); }
        public Vector3D<T> ZZZ { [Do(Inline)]get => new(v.Z, v.Z, v.Z); }
        public Vector3D<T> ZZW { [Do(Inline)]get => new(v.Z, v.Z, v.W); }
        public Vector3D<T> ZWX { [Do(Inline)]get => new(v.Z, v.W, v.X); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.X = value.Z; } }
        public Vector3D<T> ZWY { [Do(Inline)]get => new(v.Z, v.W, v.Y); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.Y = value.Z; } }
        public Vector3D<T> ZWZ { [Do(Inline)]get => new(v.Z, v.W, v.Z); }
        public Vector3D<T> ZWW { [Do(Inline)]get => new(v.Z, v.W, v.W); }
        public Vector3D<T> WXX { [Do(Inline)]get => new(v.W, v.X, v.X); }
        public Vector3D<T> WXY { [Do(Inline)]get => new(v.W, v.X, v.Y); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3D<T> WXZ { [Do(Inline)]get => new(v.W, v.X, v.Z); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3D<T> WXW { [Do(Inline)]get => new(v.W, v.X, v.W); }
        public Vector3D<T> WYX { [Do(Inline)]get => new(v.W, v.Y, v.X); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3D<T> WYY { [Do(Inline)]get => new(v.W, v.Y, v.Y); }
        public Vector3D<T> WYZ { [Do(Inline)]get => new(v.W, v.Y, v.Z); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3D<T> WYW { [Do(Inline)]get => new(v.W, v.Y, v.W); }
        public Vector3D<T> WZX { [Do(Inline)]get => new(v.W, v.Z, v.X); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3D<T> WZY { [Do(Inline)]get => new(v.W, v.Z, v.Y); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3D<T> WZZ { [Do(Inline)]get => new(v.W, v.Z, v.Z); }
        public Vector3D<T> WZW { [Do(Inline)]get => new(v.W, v.Z, v.W); }
        public Vector3D<T> WWX { [Do(Inline)]get => new(v.W, v.W, v.X); }
        public Vector3D<T> WWY { [Do(Inline)]get => new(v.W, v.W, v.Y); }
        public Vector3D<T> WWZ { [Do(Inline)]get => new(v.W, v.W, v.Z); }
        public Vector3D<T> WWW { [Do(Inline)]get => new(v.W, v.W, v.W); }
        
        public Vector4D<T> XXXX { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4D<T> XXXY { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4D<T> XXXZ { [Do(Inline)]get => new(v.X, v.X, v.X, v.Z); }
        public Vector4D<T> XXXW { [Do(Inline)]get => new(v.X, v.X, v.X, v.W); }
        public Vector4D<T> XXYX { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4D<T> XXYY { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4D<T> XXYZ { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Z); }
        public Vector4D<T> XXYW { [Do(Inline)]get => new(v.X, v.X, v.Y, v.W); }
        public Vector4D<T> XXZX { [Do(Inline)]get => new(v.X, v.X, v.Z, v.X); }
        public Vector4D<T> XXZY { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Y); }
        public Vector4D<T> XXZZ { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Z); }
        public Vector4D<T> XXZW { [Do(Inline)]get => new(v.X, v.X, v.Z, v.W); }
        public Vector4D<T> XXWX { [Do(Inline)]get => new(v.X, v.X, v.W, v.X); }
        public Vector4D<T> XXWY { [Do(Inline)]get => new(v.X, v.X, v.W, v.Y); }
        public Vector4D<T> XXWZ { [Do(Inline)]get => new(v.X, v.X, v.W, v.Z); }
        public Vector4D<T> XXWW { [Do(Inline)]get => new(v.X, v.X, v.W, v.W); }
        public Vector4D<T> XYXX { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4D<T> XYXY { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4D<T> XYXZ { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Z); }
        public Vector4D<T> XYXW { [Do(Inline)]get => new(v.X, v.Y, v.X, v.W); }
        public Vector4D<T> XYYX { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4D<T> XYYY { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4D<T> XYYZ { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Z); }
        public Vector4D<T> XYYW { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.W); }
        public Vector4D<T> XYZY { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Y); }
        public Vector4D<T> XYZZ { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Z); }
        public Vector4D<T> XYZW { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.W); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; v.W = value.W; } }
        public Vector4D<T> XYWX { [Do(Inline)]get => new(v.X, v.Y, v.W, v.X); }
        public Vector4D<T> XYWY { [Do(Inline)]get => new(v.X, v.Y, v.W, v.Y); }
        public Vector4D<T> XYWZ { [Do(Inline)]get => new(v.X, v.Y, v.W, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.W = value.Z; v.Z = value.W; } }
        public Vector4D<T> XYWW { [Do(Inline)]get => new(v.X, v.Y, v.W, v.W); }
        public Vector4D<T> XZXX { [Do(Inline)]get => new(v.X, v.Z, v.X, v.X); }
        public Vector4D<T> XZXY { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Y); }
        public Vector4D<T> XZXZ { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Z); }
        public Vector4D<T> XZXW { [Do(Inline)]get => new(v.X, v.Z, v.X, v.W); }
        public Vector4D<T> XZYX { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.X); }
        public Vector4D<T> XZYY { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Y); }
        public Vector4D<T> XZYZ { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Z); }
        public Vector4D<T> XZYW { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.W); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; v.W = value.W; } }
        public Vector4D<T> XZZX { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.X); }
        public Vector4D<T> XZZY { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Y); }
        public Vector4D<T> XZZZ { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Z); }
        public Vector4D<T> XZZW { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.W); }
        public Vector4D<T> XZWX { [Do(Inline)]get => new(v.X, v.Z, v.W, v.X); }
        public Vector4D<T> XZWY { [Do(Inline)]get => new(v.X, v.Z, v.W, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.W = value.Z; v.Y = value.W; } }
        public Vector4D<T> XZWZ { [Do(Inline)]get => new(v.X, v.Z, v.W, v.Z); }
        public Vector4D<T> XZWW { [Do(Inline)]get => new(v.X, v.Z, v.W, v.W); }
        public Vector4D<T> XWXX { [Do(Inline)]get => new(v.X, v.W, v.X, v.X); }
        public Vector4D<T> XWXY { [Do(Inline)]get => new(v.X, v.W, v.X, v.Y); }
        public Vector4D<T> XWXZ { [Do(Inline)]get => new(v.X, v.W, v.X, v.Z); }
        public Vector4D<T> XWXW { [Do(Inline)]get => new(v.X, v.W, v.X, v.W); }
        public Vector4D<T> XWYX { [Do(Inline)]get => new(v.X, v.W, v.Y, v.X); }
        public Vector4D<T> XWYY { [Do(Inline)]get => new(v.X, v.W, v.Y, v.Y); }
        public Vector4D<T> XWYZ { [Do(Inline)]get => new(v.X, v.W, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Y = value.Z; v.Z = value.W; } }
        public Vector4D<T> XWYW { [Do(Inline)]get => new(v.X, v.W, v.Y, v.W); }
        public Vector4D<T> XWZX { [Do(Inline)]get => new(v.X, v.W, v.Z, v.X); }
        public Vector4D<T> XWZY { [Do(Inline)]get => new(v.X, v.W, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Z = value.Z; v.Y = value.W; } }
        public Vector4D<T> XWZZ { [Do(Inline)]get => new(v.X, v.W, v.Z, v.Z); }
        public Vector4D<T> XWZW { [Do(Inline)]get => new(v.X, v.W, v.Z, v.W); }
        public Vector4D<T> XWWX { [Do(Inline)]get => new(v.X, v.W, v.W, v.X); }
        public Vector4D<T> XWWY { [Do(Inline)]get => new(v.X, v.W, v.W, v.Y); }
        public Vector4D<T> XWWZ { [Do(Inline)]get => new(v.X, v.W, v.W, v.Z); }
        public Vector4D<T> XWWW { [Do(Inline)]get => new(v.X, v.W, v.W, v.W); }
        public Vector4D<T> YXXX { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4D<T> YXXY { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4D<T> YXXZ { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Z); }
        public Vector4D<T> YXXW { [Do(Inline)]get => new(v.Y, v.X, v.X, v.W); }
        public Vector4D<T> YXYX { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4D<T> YXYY { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4D<T> YXYZ { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Z); }
        public Vector4D<T> YXYW { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.W); }
        public Vector4D<T> YXZX { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.X); }
        public Vector4D<T> YXZY { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Y); }
        public Vector4D<T> YXZZ { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Z); }
        public Vector4D<T> YXZW { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.W); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; v.W = value.W; } }
        public Vector4D<T> YXWX { [Do(Inline)]get => new(v.Y, v.X, v.W, v.X); }
        public Vector4D<T> YXWY { [Do(Inline)]get => new(v.Y, v.X, v.W, v.Y); }
        public Vector4D<T> YXWZ { [Do(Inline)]get => new(v.Y, v.X, v.W, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.W = value.Z; v.Z = value.W; } }
        public Vector4D<T> YXWW { [Do(Inline)]get => new(v.Y, v.X, v.W, v.W); }
        public Vector4D<T> YYXX { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4D<T> YYXY { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4D<T> YYXZ { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Z); }
        public Vector4D<T> YYXW { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.W); }
        public Vector4D<T> YYYX { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4D<T> YYYY { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
        public Vector4D<T> YYYZ { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Z); }
        public Vector4D<T> YYYW { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.W); }
        public Vector4D<T> YYZX { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.X); }
        public Vector4D<T> YYZY { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Y); }
        public Vector4D<T> YYZZ { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Z); }
        public Vector4D<T> YYZW { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.W); }
        public Vector4D<T> YYWX { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.X); }
        public Vector4D<T> YYWY { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.Y); }
        public Vector4D<T> YYWZ { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.Z); }
        public Vector4D<T> YYWW { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.W); }
        public Vector4D<T> YZXX { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.X); }
        public Vector4D<T> YZXY { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Y); }
        public Vector4D<T> YZXZ { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Z); }
        public Vector4D<T> YZXW { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.W); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; v.W = value.W; } }
        public Vector4D<T> YZYX { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.X); }
        public Vector4D<T> YZYY { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Y); }
        public Vector4D<T> YZYZ { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Z); }
        public Vector4D<T> YZYW { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.W); }
        public Vector4D<T> YZZX { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.X); }
        public Vector4D<T> YZZY { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Y); }
        public Vector4D<T> YZZZ { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Z); }
        public Vector4D<T> YZZW { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.W); }
        public Vector4D<T> YZWX { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.W = value.Z; v.X = value.W; } }
        public Vector4D<T> YZWY { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.Y); }
        public Vector4D<T> YZWZ { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.Z); }
        public Vector4D<T> YZWW { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.W); }
        public Vector4D<T> YWXX { [Do(Inline)]get => new(v.Y, v.W, v.X, v.X); }
        public Vector4D<T> YWXY { [Do(Inline)]get => new(v.Y, v.W, v.X, v.Y); }
        public Vector4D<T> YWXZ { [Do(Inline)]get => new(v.Y, v.W, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.X = value.Z; v.Z = value.W; } }
        public Vector4D<T> YWXW { [Do(Inline)]get => new(v.Y, v.W, v.X, v.W); }
        public Vector4D<T> YWYX { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.X); }
        public Vector4D<T> YWYY { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.Y); }
        public Vector4D<T> YWYZ { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.Z); }
        public Vector4D<T> YWYW { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.W); }
        public Vector4D<T> YWZX { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.Z = value.Z; v.X = value.W; } }
        public Vector4D<T> YWZY { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.Y); }
        public Vector4D<T> YWZZ { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.Z); }
        public Vector4D<T> YWZW { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.W); }
        public Vector4D<T> YWWX { [Do(Inline)]get => new(v.Y, v.W, v.W, v.X); }
        public Vector4D<T> YWWY { [Do(Inline)]get => new(v.Y, v.W, v.W, v.Y); }
        public Vector4D<T> YWWZ { [Do(Inline)]get => new(v.Y, v.W, v.W, v.Z); }
        public Vector4D<T> YWWW { [Do(Inline)]get => new(v.Y, v.W, v.W, v.W); }
        public Vector4D<T> ZXXX { [Do(Inline)]get => new(v.Z, v.X, v.X, v.X); }
        public Vector4D<T> ZXXY { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Y); }
        public Vector4D<T> ZXXZ { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Z); }
        public Vector4D<T> ZXXW { [Do(Inline)]get => new(v.Z, v.X, v.X, v.W); }
        public Vector4D<T> ZXYX { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.X); }
        public Vector4D<T> ZXYY { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Y); }
        public Vector4D<T> ZXYZ { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Z); }
        public Vector4D<T> ZXYW { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.W); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; v.W = value.W; } }
        public Vector4D<T> ZXZX { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.X); }
        public Vector4D<T> ZXZY { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Y); }
        public Vector4D<T> ZXZZ { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Z); }
        public Vector4D<T> ZXZW { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.W); }
        public Vector4D<T> ZXWX { [Do(Inline)]get => new(v.Z, v.X, v.W, v.X); }
        public Vector4D<T> ZXWY { [Do(Inline)]get => new(v.Z, v.X, v.W, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.W = value.Z; v.Y = value.W; } }
        public Vector4D<T> ZXWZ { [Do(Inline)]get => new(v.Z, v.X, v.W, v.Z); }
        public Vector4D<T> ZXWW { [Do(Inline)]get => new(v.Z, v.X, v.W, v.W); }
        public Vector4D<T> ZYXX { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.X); }
        public Vector4D<T> ZYXY { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Y); }
        public Vector4D<T> ZYXZ { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Z); }
        public Vector4D<T> ZYXW { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.W); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; v.W = value.W; } }
        public Vector4D<T> ZYYX { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.X); }
        public Vector4D<T> ZYYY { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Y); }
        public Vector4D<T> ZYYZ { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Z); }
        public Vector4D<T> ZYYW { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.W); }
        public Vector4D<T> ZYZX { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.X); }
        public Vector4D<T> ZYZY { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Y); }
        public Vector4D<T> ZYZZ { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Z); }
        public Vector4D<T> ZYZW { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.W); }
        public Vector4D<T> ZYWX { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.W = value.Z; v.X = value.W; } }
        public Vector4D<T> ZYWY { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.Y); }
        public Vector4D<T> ZYWZ { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.Z); }
        public Vector4D<T> ZYWW { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.W); }
        public Vector4D<T> ZZXX { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.X); }
        public Vector4D<T> ZZXY { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Y); }
        public Vector4D<T> ZZXZ { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Z); }
        public Vector4D<T> ZZXW { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.W); }
        public Vector4D<T> ZZYX { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.X); }
        public Vector4D<T> ZZYY { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Y); }
        public Vector4D<T> ZZYZ { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Z); }
        public Vector4D<T> ZZYW { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.W); }
        public Vector4D<T> ZZZX { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.X); }
        public Vector4D<T> ZZZY { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Y); }
        public Vector4D<T> ZZZZ { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Z); }
        public Vector4D<T> ZZZW { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.W); }
        public Vector4D<T> ZZWX { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.X); }
        public Vector4D<T> ZZWY { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.Y); }
        public Vector4D<T> ZZWZ { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.Z); }
        public Vector4D<T> ZZWW { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.W); }
        public Vector4D<T> ZWXX { [Do(Inline)]get => new(v.Z, v.W, v.X, v.X); }
        public Vector4D<T> ZWXY { [Do(Inline)]get => new(v.Z, v.W, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.X = value.Z; v.Y = value.W; } }
        public Vector4D<T> ZWXZ { [Do(Inline)]get => new(v.Z, v.W, v.X, v.Z); }
        public Vector4D<T> ZWXW { [Do(Inline)]get => new(v.Z, v.W, v.X, v.W); }
        public Vector4D<T> ZWYX { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.Y = value.Z; v.X = value.W; } }
        public Vector4D<T> ZWYY { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.Y); }
        public Vector4D<T> ZWYZ { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.Z); }
        public Vector4D<T> ZWYW { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.W); }
        public Vector4D<T> ZWZX { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.X); }
        public Vector4D<T> ZWZY { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.Y); }
        public Vector4D<T> ZWZZ { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.Z); }
        public Vector4D<T> ZWZW { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.W); }
        public Vector4D<T> ZWWX { [Do(Inline)]get => new(v.Z, v.W, v.W, v.X); }
        public Vector4D<T> ZWWY { [Do(Inline)]get => new(v.Z, v.W, v.W, v.Y); }
        public Vector4D<T> ZWWZ { [Do(Inline)]get => new(v.Z, v.W, v.W, v.Z); }
        public Vector4D<T> ZWWW { [Do(Inline)]get => new(v.Z, v.W, v.W, v.W); }
        public Vector4D<T> WXXX { [Do(Inline)]get => new(v.W, v.X, v.X, v.X); }
        public Vector4D<T> WXXY { [Do(Inline)]get => new(v.W, v.X, v.X, v.Y); }
        public Vector4D<T> WXXZ { [Do(Inline)]get => new(v.W, v.X, v.X, v.Z); }
        public Vector4D<T> WXXW { [Do(Inline)]get => new(v.W, v.X, v.X, v.W); }
        public Vector4D<T> WXYX { [Do(Inline)]get => new(v.W, v.X, v.Y, v.X); }
        public Vector4D<T> WXYY { [Do(Inline)]get => new(v.W, v.X, v.Y, v.Y); }
        public Vector4D<T> WXYZ { [Do(Inline)]get => new(v.W, v.X, v.Y, v.Z); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Y = value.Z; v.Z = value.W; } }
        public Vector4D<T> WXYW { [Do(Inline)]get => new(v.W, v.X, v.Y, v.W); }
        public Vector4D<T> WXZX { [Do(Inline)]get => new(v.W, v.X, v.Z, v.X); }
        public Vector4D<T> WXZY { [Do(Inline)]get => new(v.W, v.X, v.Z, v.Y); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Z = value.Z; v.Y = value.W; } }
        public Vector4D<T> WXZZ { [Do(Inline)]get => new(v.W, v.X, v.Z, v.Z); }
        public Vector4D<T> WXZW { [Do(Inline)]get => new(v.W, v.X, v.Z, v.W); }
        public Vector4D<T> WXWX { [Do(Inline)]get => new(v.W, v.X, v.W, v.X); }
        public Vector4D<T> WXWY { [Do(Inline)]get => new(v.W, v.X, v.W, v.Y); }
        public Vector4D<T> WXWZ { [Do(Inline)]get => new(v.W, v.X, v.W, v.Z); }
        public Vector4D<T> WXWW { [Do(Inline)]get => new(v.W, v.X, v.W, v.W); }
        public Vector4D<T> WYXX { [Do(Inline)]get => new(v.W, v.Y, v.X, v.X); }
        public Vector4D<T> WYXY { [Do(Inline)]get => new(v.W, v.Y, v.X, v.Y); }
        public Vector4D<T> WYXZ { [Do(Inline)]get => new(v.W, v.Y, v.X, v.Z); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.X = value.Z; v.Z = value.W; } }
        public Vector4D<T> WYXW { [Do(Inline)]get => new(v.W, v.Y, v.X, v.W); }
        public Vector4D<T> WYYX { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.X); }
        public Vector4D<T> WYYY { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.Y); }
        public Vector4D<T> WYYZ { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.Z); }
        public Vector4D<T> WYYW { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.W); }
        public Vector4D<T> WYZX { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.X); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.Z = value.Z; v.X = value.W; } }
        public Vector4D<T> WYZY { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.Y); }
        public Vector4D<T> WYZZ { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.Z); }
        public Vector4D<T> WYZW { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.W); }
        public Vector4D<T> WYWX { [Do(Inline)]get => new(v.W, v.Y, v.W, v.X); }
        public Vector4D<T> WYWY { [Do(Inline)]get => new(v.W, v.Y, v.W, v.Y); }
        public Vector4D<T> WYWZ { [Do(Inline)]get => new(v.W, v.Y, v.W, v.Z); }
        public Vector4D<T> WYWW { [Do(Inline)]get => new(v.W, v.Y, v.W, v.W); }
        public Vector4D<T> WZXX { [Do(Inline)]get => new(v.W, v.Z, v.X, v.X); }
        public Vector4D<T> WZXY { [Do(Inline)]get => new(v.W, v.Z, v.X, v.Y); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.X = value.Z; v.Y = value.W; } }
        public Vector4D<T> WZXZ { [Do(Inline)]get => new(v.W, v.Z, v.X, v.Z); }
        public Vector4D<T> WZXW { [Do(Inline)]get => new(v.W, v.Z, v.X, v.W); }
        public Vector4D<T> WZYX { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.X); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.Y = value.Z; v.X = value.W; } }
        public Vector4D<T> WZYY { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.Y); }
        public Vector4D<T> WZYZ { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.Z); }
        public Vector4D<T> WZYW { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.W); }
        public Vector4D<T> WZZX { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.X); }
        public Vector4D<T> WZZY { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.Y); }
        public Vector4D<T> WZZZ { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.Z); }
        public Vector4D<T> WZZW { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.W); }
        public Vector4D<T> WZWX { [Do(Inline)]get => new(v.W, v.Z, v.W, v.X); }
        public Vector4D<T> WZWY { [Do(Inline)]get => new(v.W, v.Z, v.W, v.Y); }
        public Vector4D<T> WZWZ { [Do(Inline)]get => new(v.W, v.Z, v.W, v.Z); }
        public Vector4D<T> WZWW { [Do(Inline)]get => new(v.W, v.Z, v.W, v.W); }
        public Vector4D<T> WWXX { [Do(Inline)]get => new(v.W, v.W, v.X, v.X); }
        public Vector4D<T> WWXY { [Do(Inline)]get => new(v.W, v.W, v.X, v.Y); }
        public Vector4D<T> WWXZ { [Do(Inline)]get => new(v.W, v.W, v.X, v.Z); }
        public Vector4D<T> WWXW { [Do(Inline)]get => new(v.W, v.W, v.X, v.W); }
        public Vector4D<T> WWYX { [Do(Inline)]get => new(v.W, v.W, v.Y, v.X); }
        public Vector4D<T> WWYY { [Do(Inline)]get => new(v.W, v.W, v.Y, v.Y); }
        public Vector4D<T> WWYZ { [Do(Inline)]get => new(v.W, v.W, v.Y, v.Z); }
        public Vector4D<T> WWYW { [Do(Inline)]get => new(v.W, v.W, v.Y, v.W); }
        public Vector4D<T> WWZX { [Do(Inline)]get => new(v.W, v.W, v.Z, v.X); }
        public Vector4D<T> WWZY { [Do(Inline)]get => new(v.W, v.W, v.Z, v.Y); }
        public Vector4D<T> WWZZ { [Do(Inline)]get => new(v.W, v.W, v.Z, v.Z); }
        public Vector4D<T> WWZW { [Do(Inline)]get => new(v.W, v.W, v.Z, v.W); }
        public Vector4D<T> WWWX { [Do(Inline)]get => new(v.W, v.W, v.W, v.X); }
        public Vector4D<T> WWWY { [Do(Inline)]get => new(v.W, v.W, v.W, v.Y); }
        public Vector4D<T> WWWZ { [Do(Inline)]get => new(v.W, v.W, v.W, v.Z); }
        public Vector4D<T> WWWW { [Do(Inline)]get => new(v.W, v.W, v.W, v.W); }
    }
    
    extension(ref Vector2 v)
    {
        public float R { [Do(Inline)]get => v.X; [Do(Inline)]set => v.X = value; }
        public float G { [Do(Inline)]get => v.Y; [Do(Inline)]set => v.Y = value; }

        public Vector2 RR { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2 RG { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2 GR { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2 GG { [Do(Inline)]get => new(v.Y, v.Y); }

        public Vector3 RRR { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3 RRG { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3 RGR { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3 RGG { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3 GRR { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3 GRG { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3 GGR { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3 GGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }

        public Vector4 RRRR { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4 RRRG { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4 RRGR { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4 RRGG { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4 RGRR { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4 RGRG { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4 RGGR { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4 RGGG { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4 GRRR { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4 GRRG { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4 GRGR { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4 GRGG { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4 GGRR { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4 GGRG { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4 GGGR { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4 GGGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
    }
    
    extension<T>(ref Vector2D<T> v) where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
    {
        public T R { [Do(Inline)]get => v.X; [Do(Inline)]set => v.X = value; }
        public T G { [Do(Inline)]get => v.Y; [Do(Inline)]set => v.Y = value; }
        
        public Vector2D<T> RR { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2D<T> RG { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2D<T> GR { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2D<T> GG { [Do(Inline)]get => new(v.Y, v.Y); }

        public Vector3D<T> RRR { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3D<T> RRG { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3D<T> RGR { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3D<T> RGG { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3D<T> GRR { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3D<T> GRG { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3D<T> GGR { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3D<T> GGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }

        public Vector4D<T> RRRR { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4D<T> RRRG { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4D<T> RRGR { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4D<T> RRGG { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4D<T> RGRR { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4D<T> RGRG { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4D<T> RGGR { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4D<T> RGGG { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4D<T> GRRR { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4D<T> GRRG { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4D<T> GRGR { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4D<T> GRGG { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4D<T> GGRR { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4D<T> GGRG { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4D<T> GGGR { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4D<T> GGGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
    }
    
    extension(ref Vector3 v)
    {
        public float R { [Do(Inline)]get => v.X; [Do(Inline)]set => v.X = value; }
        public float G { [Do(Inline)]get => v.Y; [Do(Inline)]set => v.Y = value; }
        public float B { [Do(Inline)]get => v.Z; [Do(Inline)]set => v.Z = value; }
        
        public Vector2 RR { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2 RG { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2 RB { [Do(Inline)]get => new(v.X, v.Z); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; } }
        public Vector2 GR { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2 GG { [Do(Inline)]get => new(v.Y, v.Y); }
        public Vector2 GB { [Do(Inline)]get => new(v.Y, v.Z); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; } }
        public Vector2 BR { [Do(Inline)]get => new(v.Z, v.X); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; } }
        public Vector2 BG { [Do(Inline)]get => new(v.Z, v.Y); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; } }
        public Vector2 BB { [Do(Inline)]get => new(v.Z, v.Z); }

        public Vector3 RRR { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3 RRG { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3 RRB { [Do(Inline)]get => new(v.X, v.X, v.Z); }
        public Vector3 RGR { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3 RGG { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3 RGB { [Do(Inline)]get => new(v.X, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3 RBR { [Do(Inline)]get => new(v.X, v.Z, v.X); }
        public Vector3 RBG { [Do(Inline)]get => new(v.X, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3 RBB { [Do(Inline)]get => new(v.X, v.Z, v.Z); }
        public Vector3 GRR { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3 GRG { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3 GRB { [Do(Inline)]get => new(v.Y, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3 GGR { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3 GGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }
        public Vector3 GGB { [Do(Inline)]get => new(v.Y, v.Y, v.Z); }
        public Vector3 GBR { [Do(Inline)]get => new(v.Y, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3 GBG { [Do(Inline)]get => new(v.Y, v.Z, v.Y); }
        public Vector3 GBB { [Do(Inline)]get => new(v.Y, v.Z, v.Z); }
        public Vector3 BRR { [Do(Inline)]get => new(v.Z, v.X, v.X); }
        public Vector3 BRG { [Do(Inline)]get => new(v.Z, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3 BRB { [Do(Inline)]get => new(v.Z, v.X, v.Z); }
        public Vector3 BGR { [Do(Inline)]get => new(v.Z, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3 BGG { [Do(Inline)]get => new(v.Z, v.Y, v.Y); }
        public Vector3 BGB { [Do(Inline)]get => new(v.Z, v.Y, v.Z); }
        public Vector3 BBR { [Do(Inline)]get => new(v.Z, v.Z, v.X); }
        public Vector3 BBG { [Do(Inline)]get => new(v.Z, v.Z, v.Y); }
        public Vector3 BBB { [Do(Inline)]get => new(v.Z, v.Z, v.Z); }

        public Vector4 RRRR { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4 RRRG { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4 RRRB { [Do(Inline)]get => new(v.X, v.X, v.X, v.Z); }
        public Vector4 RRGR { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4 RRGG { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4 RRGB { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Z); }
        public Vector4 RRBR { [Do(Inline)]get => new(v.X, v.X, v.Z, v.X); }
        public Vector4 RRBG { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Y); }
        public Vector4 RRBB { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Z); }
        public Vector4 RGRR { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4 RGRG { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4 RGRB { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Z); }
        public Vector4 RGGR { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4 RGGG { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4 RGGB { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Z); }
        public Vector4 RGBG { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Y); }
        public Vector4 RGBB { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Z); }
        public Vector4 RBRR { [Do(Inline)]get => new(v.X, v.Z, v.X, v.X); }
        public Vector4 RBRG { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Y); }
        public Vector4 RBRB { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Z); }
        public Vector4 RBGR { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.X); }
        public Vector4 RBGG { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Y); }
        public Vector4 RBGB { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Z); }
        public Vector4 RBBR { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.X); }
        public Vector4 RBBG { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Y); }
        public Vector4 RBBB { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Z); }
        public Vector4 GRRR { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4 GRRG { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4 GRRB { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Z); }
        public Vector4 GRGR { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4 GRGG { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4 GRGB { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Z); }
        public Vector4 GRBR { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.X); }
        public Vector4 GRBG { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Y); }
        public Vector4 GRBB { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Z); }
        public Vector4 GGRR { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4 GGRG { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4 GGRB { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Z); }
        public Vector4 GGGR { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4 GGGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
        public Vector4 GGGB { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Z); }
        public Vector4 GGBR { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.X); }
        public Vector4 GGBG { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Y); }
        public Vector4 GGBB { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Z); }
        public Vector4 GBRR { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.X); }
        public Vector4 GBRG { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Y); }
        public Vector4 GBRB { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Z); }
        public Vector4 GBGR { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.X); }
        public Vector4 GBGG { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Y); }
        public Vector4 GBGB { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Z); }
        public Vector4 GBBR { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.X); }
        public Vector4 GBBG { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Y); }
        public Vector4 GBBB { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Z); }
        public Vector4 BRRR { [Do(Inline)]get => new(v.Z, v.X, v.X, v.X); }
        public Vector4 BRRG { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Y); }
        public Vector4 BRRB { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Z); }
        public Vector4 BRGR { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.X); }
        public Vector4 BRGG { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Y); }
        public Vector4 BRGB { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Z); }
        public Vector4 BRBR { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.X); }
        public Vector4 BRBG { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Y); }
        public Vector4 BRBB { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Z); }
        public Vector4 BGRR { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.X); }
        public Vector4 BGRG { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Y); }
        public Vector4 BGRB { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Z); }
        public Vector4 BGGR { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.X); }
        public Vector4 BGGG { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Y); }
        public Vector4 BGGB { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Z); }
        public Vector4 BGBR { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.X); }
        public Vector4 BGBG { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Y); }
        public Vector4 BGBB { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Z); }
        public Vector4 BBRR { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.X); }
        public Vector4 BBRG { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Y); }
        public Vector4 BBRB { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Z); }
        public Vector4 BBGR { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.X); }
        public Vector4 BBGG { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Y); }
        public Vector4 BBGB { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Z); }
        public Vector4 BBBR { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.X); }
        public Vector4 BBBG { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Y); }
        public Vector4 BBBB { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Z); }
    }
    
    extension<T>(ref Vector3D<T> v) where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
    {
        public T R { [Do(Inline)]get => v.X; [Do(Inline)]set => v.X = value; }
        public T G { [Do(Inline)]get => v.Y; [Do(Inline)]set => v.Y = value; }
        public T B { [Do(Inline)]get => v.Z; [Do(Inline)]set => v.Z = value; }

        public Vector2D<T> RR { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2D<T> RG { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2D<T> RB { [Do(Inline)]get => new(v.X, v.Z); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; } }
        public Vector2D<T> GR { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2D<T> GG { [Do(Inline)]get => new(v.Y, v.Y); }
        public Vector2D<T> GB { [Do(Inline)]get => new(v.Y, v.Z); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; } }
        public Vector2D<T> BR { [Do(Inline)]get => new(v.Z, v.X); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; } }
        public Vector2D<T> BG { [Do(Inline)]get => new(v.Z, v.Y); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; } }
        public Vector2D<T> BB { [Do(Inline)]get => new(v.Z, v.Z); }

        public Vector3D<T> RRR { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3D<T> RRG { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3D<T> RRB { [Do(Inline)]get => new(v.X, v.X, v.Z); }
        public Vector3D<T> RGR { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3D<T> RGG { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3D<T> RGB { [Do(Inline)]get => new(v.X, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3D<T> RBR { [Do(Inline)]get => new(v.X, v.Z, v.X); }
        public Vector3D<T> RBG { [Do(Inline)]get => new(v.X, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3D<T> RBB { [Do(Inline)]get => new(v.X, v.Z, v.Z); }
        public Vector3D<T> GRR { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3D<T> GRG { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3D<T> GRB { [Do(Inline)]get => new(v.Y, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3D<T> GGR { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3D<T> GGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }
        public Vector3D<T> GGB { [Do(Inline)]get => new(v.Y, v.Y, v.Z); }
        public Vector3D<T> GBR { [Do(Inline)]get => new(v.Y, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3D<T> GBG { [Do(Inline)]get => new(v.Y, v.Z, v.Y); }
        public Vector3D<T> GBB { [Do(Inline)]get => new(v.Y, v.Z, v.Z); }
        public Vector3D<T> BRR { [Do(Inline)]get => new(v.Z, v.X, v.X); }
        public Vector3D<T> BRG { [Do(Inline)]get => new(v.Z, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3D<T> BRB { [Do(Inline)]get => new(v.Z, v.X, v.Z); }
        public Vector3D<T> BGR { [Do(Inline)]get => new(v.Z, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3D<T> BGG { [Do(Inline)]get => new(v.Z, v.Y, v.Y); }
        public Vector3D<T> BGB { [Do(Inline)]get => new(v.Z, v.Y, v.Z); }
        public Vector3D<T> BBR { [Do(Inline)]get => new(v.Z, v.Z, v.X); }
        public Vector3D<T> BBG { [Do(Inline)]get => new(v.Z, v.Z, v.Y); }
        public Vector3D<T> BBB { [Do(Inline)]get => new(v.Z, v.Z, v.Z); }

        public Vector4D<T> RRRR { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4D<T> RRRG { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4D<T> RRRB { [Do(Inline)]get => new(v.X, v.X, v.X, v.Z); }
        public Vector4D<T> RRGR { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4D<T> RRGG { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4D<T> RRGB { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Z); }
        public Vector4D<T> RRBR { [Do(Inline)]get => new(v.X, v.X, v.Z, v.X); }
        public Vector4D<T> RRBG { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Y); }
        public Vector4D<T> RRBB { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Z); }
        public Vector4D<T> RGRR { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4D<T> RGRG { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4D<T> RGRB { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Z); }
        public Vector4D<T> RGGR { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4D<T> RGGG { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4D<T> RGGB { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Z); }
        public Vector4D<T> RGBG { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Y); }
        public Vector4D<T> RGBB { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Z); }
        public Vector4D<T> RBRR { [Do(Inline)]get => new(v.X, v.Z, v.X, v.X); }
        public Vector4D<T> RBRG { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Y); }
        public Vector4D<T> RBRB { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Z); }
        public Vector4D<T> RBGR { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.X); }
        public Vector4D<T> RBGG { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Y); }
        public Vector4D<T> RBGB { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Z); }
        public Vector4D<T> RBBR { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.X); }
        public Vector4D<T> RBBG { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Y); }
        public Vector4D<T> RBBB { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Z); }
        public Vector4D<T> GRRR { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4D<T> GRRG { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4D<T> GRRB { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Z); }
        public Vector4D<T> GRGR { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4D<T> GRGG { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4D<T> GRGB { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Z); }
        public Vector4D<T> GRBR { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.X); }
        public Vector4D<T> GRBG { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Y); }
        public Vector4D<T> GRBB { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Z); }
        public Vector4D<T> GGRR { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4D<T> GGRG { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4D<T> GGRB { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Z); }
        public Vector4D<T> GGGR { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4D<T> GGGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
        public Vector4D<T> GGGB { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Z); }
        public Vector4D<T> GGBR { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.X); }
        public Vector4D<T> GGBG { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Y); }
        public Vector4D<T> GGBB { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Z); }
        public Vector4D<T> GBRR { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.X); }
        public Vector4D<T> GBRG { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Y); }
        public Vector4D<T> GBRB { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Z); }
        public Vector4D<T> GBGR { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.X); }
        public Vector4D<T> GBGG { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Y); }
        public Vector4D<T> GBGB { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Z); }
        public Vector4D<T> GBBR { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.X); }
        public Vector4D<T> GBBG { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Y); }
        public Vector4D<T> GBBB { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Z); }
        public Vector4D<T> BRRR { [Do(Inline)]get => new(v.Z, v.X, v.X, v.X); }
        public Vector4D<T> BRRG { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Y); }
        public Vector4D<T> BRRB { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Z); }
        public Vector4D<T> BRGR { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.X); }
        public Vector4D<T> BRGG { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Y); }
        public Vector4D<T> BRGB { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Z); }
        public Vector4D<T> BRBR { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.X); }
        public Vector4D<T> BRBG { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Y); }
        public Vector4D<T> BRBB { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Z); }
        public Vector4D<T> BGRR { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.X); }
        public Vector4D<T> BGRG { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Y); }
        public Vector4D<T> BGRB { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Z); }
        public Vector4D<T> BGGR { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.X); }
        public Vector4D<T> BGGG { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Y); }
        public Vector4D<T> BGGB { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Z); }
        public Vector4D<T> BGBR { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.X); }
        public Vector4D<T> BGBG { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Y); }
        public Vector4D<T> BGBB { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Z); }
        public Vector4D<T> BBRR { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.X); }
        public Vector4D<T> BBRG { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Y); }
        public Vector4D<T> BBRB { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Z); }
        public Vector4D<T> BBGR { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.X); }
        public Vector4D<T> BBGG { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Y); }
        public Vector4D<T> BBGB { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Z); }
        public Vector4D<T> BBBR { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.X); }
        public Vector4D<T> BBBG { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Y); }
        public Vector4D<T> BBBB { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Z); }
    }

    extension(ref Vector4 v)
    {
        public float R { [Do(Inline)]get => v.X; [Do(Inline)]set => v.X = value; }
        public float G { [Do(Inline)]get => v.Y; [Do(Inline)]set => v.Y = value; }
        public float B { [Do(Inline)]get => v.Z; [Do(Inline)]set => v.Z = value; }
        public float A { [Do(Inline)]get => v.W; [Do(Inline)]set => v.W = value; }

        public Vector2 RR { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2 RG { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2 RB { [Do(Inline)]get => new(v.X, v.Z); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; } }
        public Vector2 RA { [Do(Inline)]get => new(v.X, v.W); [Do(Inline)]set { v.X = value.X; v.W = value.Y; } }
        public Vector2 GR { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2 GG { [Do(Inline)]get => new(v.Y, v.Y); }
        public Vector2 GB { [Do(Inline)]get => new(v.Y, v.Z); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; } }
        public Vector2 GA { [Do(Inline)]get => new(v.Y, v.W); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; } }
        public Vector2 BR { [Do(Inline)]get => new(v.Z, v.X); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; } }
        public Vector2 BG { [Do(Inline)]get => new(v.Z, v.Y); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; } }
        public Vector2 BB { [Do(Inline)]get => new(v.Z, v.Z); }
        public Vector2 BA { [Do(Inline)]get => new(v.Z, v.W); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; } }
        public Vector2 AR { [Do(Inline)]get => new(v.W, v.X); [Do(Inline)]set { v.W = value.X; v.X = value.Y; } }
        public Vector2 AG { [Do(Inline)]get => new(v.W, v.Y); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; } }
        public Vector2 AB { [Do(Inline)]get => new(v.W, v.Z); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; } }
        public Vector2 AA { [Do(Inline)]get => new(v.W, v.W); }
        
        public Vector3 RRR { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3 RRG { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3 RRB { [Do(Inline)]get => new(v.X, v.X, v.Z); }
        public Vector3 RRA { [Do(Inline)]get => new(v.X, v.X, v.W); }
        public Vector3 RGR { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3 RGG { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3 RGB { [Do(Inline)]get => new(v.X, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3 RGA { [Do(Inline)]get => new(v.X, v.Y, v.W); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.W = value.Z; } }
        public Vector3 RBR { [Do(Inline)]get => new(v.X, v.Z, v.X); }
        public Vector3 RBG { [Do(Inline)]get => new(v.X, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3 RBB { [Do(Inline)]get => new(v.X, v.Z, v.Z); }
        public Vector3 RBA { [Do(Inline)]get => new(v.X, v.Z, v.W); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.W = value.Z; } }
        public Vector3 RAR { [Do(Inline)]get => new(v.X, v.W, v.X); }
        public Vector3 RAG { [Do(Inline)]get => new(v.X, v.W, v.Y); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Y = value.Z; } }
        public Vector3 RAB { [Do(Inline)]get => new(v.X, v.W, v.Z); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Z = value.Z; } }
        public Vector3 RAA { [Do(Inline)]get => new(v.X, v.W, v.W); }
        public Vector3 GRR { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3 GRG { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3 GRB { [Do(Inline)]get => new(v.Y, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3 GRA { [Do(Inline)]get => new(v.Y, v.X, v.W); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.W = value.Z; } }
        public Vector3 GGR { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3 GGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }
        public Vector3 GGB { [Do(Inline)]get => new(v.Y, v.Y, v.Z); }
        public Vector3 GGA { [Do(Inline)]get => new(v.Y, v.Y, v.W); }
        public Vector3 GBR { [Do(Inline)]get => new(v.Y, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3 GBG { [Do(Inline)]get => new(v.Y, v.Z, v.Y); }
        public Vector3 GBB { [Do(Inline)]get => new(v.Y, v.Z, v.Z); }
        public Vector3 GBA { [Do(Inline)]get => new(v.Y, v.Z, v.W); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.W = value.Z; } }
        public Vector3 GAR { [Do(Inline)]get => new(v.Y, v.W, v.X); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.X = value.Z; } }
        public Vector3 GAG { [Do(Inline)]get => new(v.Y, v.W, v.Y); }
        public Vector3 GAB { [Do(Inline)]get => new(v.Y, v.W, v.Z); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.Z = value.Z; } }
        public Vector3 GAA { [Do(Inline)]get => new(v.Y, v.W, v.W); }
        public Vector3 BRR { [Do(Inline)]get => new(v.Z, v.X, v.X); }
        public Vector3 BRG { [Do(Inline)]get => new(v.Z, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3 BRB { [Do(Inline)]get => new(v.Z, v.X, v.Z); }
        public Vector3 BRA { [Do(Inline)]get => new(v.Z, v.X, v.W); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.W = value.Z; } }
        public Vector3 BGR { [Do(Inline)]get => new(v.Z, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3 BGG { [Do(Inline)]get => new(v.Z, v.Y, v.Y); }
        public Vector3 BGB { [Do(Inline)]get => new(v.Z, v.Y, v.Z); }
        public Vector3 BGA { [Do(Inline)]get => new(v.Z, v.Y, v.W); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.W = value.Z; } }
        public Vector3 BBR { [Do(Inline)]get => new(v.Z, v.Z, v.X); }
        public Vector3 BBG { [Do(Inline)]get => new(v.Z, v.Z, v.Y); }
        public Vector3 BBB { [Do(Inline)]get => new(v.Z, v.Z, v.Z); }
        public Vector3 BBA { [Do(Inline)]get => new(v.Z, v.Z, v.W); }
        public Vector3 BAR { [Do(Inline)]get => new(v.Z, v.W, v.X); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.X = value.Z; } }
        public Vector3 BAG { [Do(Inline)]get => new(v.Z, v.W, v.Y); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.Y = value.Z; } }
        public Vector3 BAB { [Do(Inline)]get => new(v.Z, v.W, v.Z); }
        public Vector3 BAA { [Do(Inline)]get => new(v.Z, v.W, v.W); }
        public Vector3 ARR { [Do(Inline)]get => new(v.W, v.X, v.X); }
        public Vector3 ARG { [Do(Inline)]get => new(v.W, v.X, v.Y); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3 ARB { [Do(Inline)]get => new(v.W, v.X, v.Z); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3 ARA { [Do(Inline)]get => new(v.W, v.X, v.W); }
        public Vector3 AGR { [Do(Inline)]get => new(v.W, v.Y, v.X); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3 AGG { [Do(Inline)]get => new(v.W, v.Y, v.Y); }
        public Vector3 AGB { [Do(Inline)]get => new(v.W, v.Y, v.Z); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3 AGA { [Do(Inline)]get => new(v.W, v.Y, v.W); }
        public Vector3 ABR { [Do(Inline)]get => new(v.W, v.Z, v.X); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3 ABG { [Do(Inline)]get => new(v.W, v.Z, v.Y); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3 ABB { [Do(Inline)]get => new(v.W, v.Z, v.Z); }
        public Vector3 ABA { [Do(Inline)]get => new(v.W, v.Z, v.W); }
        public Vector3 AAR { [Do(Inline)]get => new(v.W, v.W, v.X); }
        public Vector3 AAG { [Do(Inline)]get => new(v.W, v.W, v.Y); }
        public Vector3 AAB { [Do(Inline)]get => new(v.W, v.W, v.Z); }
        public Vector3 AAA { [Do(Inline)]get => new(v.W, v.W, v.W); }
        
        public Vector4 RRRR { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4 RRRG { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4 RRRB { [Do(Inline)]get => new(v.X, v.X, v.X, v.Z); }
        public Vector4 RRRA { [Do(Inline)]get => new(v.X, v.X, v.X, v.W); }
        public Vector4 RRGR { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4 RRGG { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4 RRGB { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Z); }
        public Vector4 RRGA { [Do(Inline)]get => new(v.X, v.X, v.Y, v.W); }
        public Vector4 RRBR { [Do(Inline)]get => new(v.X, v.X, v.Z, v.X); }
        public Vector4 RRBG { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Y); }
        public Vector4 RRBB { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Z); }
        public Vector4 RRBA { [Do(Inline)]get => new(v.X, v.X, v.Z, v.W); }
        public Vector4 RRAR { [Do(Inline)]get => new(v.X, v.X, v.W, v.X); }
        public Vector4 RRAG { [Do(Inline)]get => new(v.X, v.X, v.W, v.Y); }
        public Vector4 RRAB { [Do(Inline)]get => new(v.X, v.X, v.W, v.Z); }
        public Vector4 RRAA { [Do(Inline)]get => new(v.X, v.X, v.W, v.W); }
        public Vector4 RGRR { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4 RGRG { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4 RGRB { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Z); }
        public Vector4 RGRA { [Do(Inline)]get => new(v.X, v.Y, v.X, v.W); }
        public Vector4 RGGR { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4 RGGG { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4 RGGB { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Z); }
        public Vector4 RGGA { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.W); }
        public Vector4 RGBR { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.X); }
        public Vector4 RGBG { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Y); }
        public Vector4 RGBB { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Z); }
        public Vector4 RGBA { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.W); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; v.W = value.W; } }
        public Vector4 RGAR { [Do(Inline)]get => new(v.X, v.Y, v.W, v.X); }
        public Vector4 RGAG { [Do(Inline)]get => new(v.X, v.Y, v.W, v.Y); }
        public Vector4 RGAB { [Do(Inline)]get => new(v.X, v.Y, v.W, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.W = value.Z; v.Z = value.W; } }
        public Vector4 RGAA { [Do(Inline)]get => new(v.X, v.Y, v.W, v.W); }
        public Vector4 RBRR { [Do(Inline)]get => new(v.X, v.Z, v.X, v.X); }
        public Vector4 RBRG { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Y); }
        public Vector4 RBRB { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Z); }
        public Vector4 RBRA { [Do(Inline)]get => new(v.X, v.Z, v.X, v.W); }
        public Vector4 RBGR { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.X); }
        public Vector4 RBGG { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Y); }
        public Vector4 RBGB { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Z); }
        public Vector4 RBGA { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.W); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; v.W = value.W; } }
        public Vector4 RBBR { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.X); }
        public Vector4 RBBG { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Y); }
        public Vector4 RBBB { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Z); }
        public Vector4 RBBA { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.W); }
        public Vector4 RBAR { [Do(Inline)]get => new(v.X, v.Z, v.W, v.X); }
        public Vector4 RBAG { [Do(Inline)]get => new(v.X, v.Z, v.W, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.W = value.Z; v.Y = value.W; } }
        public Vector4 RBAB { [Do(Inline)]get => new(v.X, v.Z, v.W, v.Z); }
        public Vector4 RBAA { [Do(Inline)]get => new(v.X, v.Z, v.W, v.W); }
        public Vector4 RARR { [Do(Inline)]get => new(v.X, v.W, v.X, v.X); }
        public Vector4 RARG { [Do(Inline)]get => new(v.X, v.W, v.X, v.Y); }
        public Vector4 RARB { [Do(Inline)]get => new(v.X, v.W, v.X, v.Z); }
        public Vector4 RARA { [Do(Inline)]get => new(v.X, v.W, v.X, v.W); }
        public Vector4 RAGR { [Do(Inline)]get => new(v.X, v.W, v.Y, v.X); }
        public Vector4 RAGG { [Do(Inline)]get => new(v.X, v.W, v.Y, v.Y); }
        public Vector4 RAGB { [Do(Inline)]get => new(v.X, v.W, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Y = value.Z; v.Z = value.W; } }
        public Vector4 RAGA { [Do(Inline)]get => new(v.X, v.W, v.Y, v.W); }
        public Vector4 RABR { [Do(Inline)]get => new(v.X, v.W, v.Z, v.X); }
        public Vector4 RABG { [Do(Inline)]get => new(v.X, v.W, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Z = value.Z; v.Y = value.W; } }
        public Vector4 RABB { [Do(Inline)]get => new(v.X, v.W, v.Z, v.Z); }
        public Vector4 RABA { [Do(Inline)]get => new(v.X, v.W, v.Z, v.W); }
        public Vector4 RAAR { [Do(Inline)]get => new(v.X, v.W, v.W, v.X); }
        public Vector4 RAAG { [Do(Inline)]get => new(v.X, v.W, v.W, v.Y); }
        public Vector4 RAAB { [Do(Inline)]get => new(v.X, v.W, v.W, v.Z); }
        public Vector4 RAAA { [Do(Inline)]get => new(v.X, v.W, v.W, v.W); }
        public Vector4 GRRR { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4 GRRG { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4 GRRB { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Z); }
        public Vector4 GRRA { [Do(Inline)]get => new(v.Y, v.X, v.X, v.W); }
        public Vector4 GRGR { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4 GRGG { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4 GRGB { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Z); }
        public Vector4 GRGA { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.W); }
        public Vector4 GRBR { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.X); }
        public Vector4 GRBG { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Y); }
        public Vector4 GRBB { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Z); }
        public Vector4 GRBA { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.W); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; v.W = value.W; } }
        public Vector4 GRAR { [Do(Inline)]get => new(v.Y, v.X, v.W, v.X); }
        public Vector4 GRAG { [Do(Inline)]get => new(v.Y, v.X, v.W, v.Y); }
        public Vector4 GRAB { [Do(Inline)]get => new(v.Y, v.X, v.W, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.W = value.Z; v.Z = value.W; } }
        public Vector4 GRAA { [Do(Inline)]get => new(v.Y, v.X, v.W, v.W); }
        public Vector4 GGRR { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4 GGRG { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4 GGRB { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Z); }
        public Vector4 GGRA { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.W); }
        public Vector4 GGGR { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4 GGGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
        public Vector4 GGGB { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Z); }
        public Vector4 GGGA { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.W); }
        public Vector4 GGBR { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.X); }
        public Vector4 GGBG { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Y); }
        public Vector4 GGBB { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Z); }
        public Vector4 GGBA { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.W); }
        public Vector4 GGAR { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.X); }
        public Vector4 GGAG { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.Y); }
        public Vector4 GGAB { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.Z); }
        public Vector4 GGAA { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.W); }
        public Vector4 GBRR { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.X); }
        public Vector4 GBRG { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Y); }
        public Vector4 GBRB { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Z); }
        public Vector4 GBRA { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.W); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; v.W = value.W; } }
        public Vector4 GBGR { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.X); }
        public Vector4 GBGG { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Y); }
        public Vector4 GBGB { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Z); }
        public Vector4 GBGA { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.W); }
        public Vector4 GBBR { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.X); }
        public Vector4 GBBG { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Y); }
        public Vector4 GBBB { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Z); }
        public Vector4 GBBA { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.W); }
        public Vector4 GBAR { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.W = value.Z; v.X = value.W; } }
        public Vector4 GBAG { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.Y); }
        public Vector4 GBAB { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.Z); }
        public Vector4 GBAA { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.W); }
        public Vector4 GARR { [Do(Inline)]get => new(v.Y, v.W, v.X, v.X); }
        public Vector4 GARG { [Do(Inline)]get => new(v.Y, v.W, v.X, v.Y); }
        public Vector4 GARB { [Do(Inline)]get => new(v.Y, v.W, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.X = value.Z; v.Z = value.W; } }
        public Vector4 GARA { [Do(Inline)]get => new(v.Y, v.W, v.X, v.W); }
        public Vector4 GAGR { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.X); }
        public Vector4 GAGG { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.Y); }
        public Vector4 GAGB { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.Z); }
        public Vector4 GAGA { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.W); }
        public Vector4 GABR { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.Z = value.Z; v.X = value.W; } }
        public Vector4 GABG { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.Y); }
        public Vector4 GABB { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.Z); }
        public Vector4 GABA { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.W); }
        public Vector4 GAAR { [Do(Inline)]get => new(v.Y, v.W, v.W, v.X); }
        public Vector4 GAAG { [Do(Inline)]get => new(v.Y, v.W, v.W, v.Y); }
        public Vector4 GAAB { [Do(Inline)]get => new(v.Y, v.W, v.W, v.Z); }
        public Vector4 GAAA { [Do(Inline)]get => new(v.Y, v.W, v.W, v.W); }
        public Vector4 BRRR { [Do(Inline)]get => new(v.Z, v.X, v.X, v.X); }
        public Vector4 BRRG { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Y); }
        public Vector4 BRRB { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Z); }
        public Vector4 BRRA { [Do(Inline)]get => new(v.Z, v.X, v.X, v.W); }
        public Vector4 BRGR { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.X); }
        public Vector4 BRGG { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Y); }
        public Vector4 BRGB { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Z); }
        public Vector4 BRGA { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.W); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; v.W = value.W; } }
        public Vector4 BRBR { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.X); }
        public Vector4 BRBG { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Y); }
        public Vector4 BRBB { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Z); }
        public Vector4 BRBA { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.W); }
        public Vector4 BRAR { [Do(Inline)]get => new(v.Z, v.X, v.W, v.X); }
        public Vector4 BRAG { [Do(Inline)]get => new(v.Z, v.X, v.W, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.W = value.Z; v.Y = value.W; } }
        public Vector4 BRAB { [Do(Inline)]get => new(v.Z, v.X, v.W, v.Z); }
        public Vector4 BRAA { [Do(Inline)]get => new(v.Z, v.X, v.W, v.W); }
        public Vector4 BGRR { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.X); }
        public Vector4 BGRG { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Y); }
        public Vector4 BGRB { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Z); }
        public Vector4 BGRA { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.W); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; v.W = value.W; } }
        public Vector4 BGGR { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.X); }
        public Vector4 BGGG { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Y); }
        public Vector4 BGGB { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Z); }
        public Vector4 BGGA { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.W); }
        public Vector4 BGBR { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.X); }
        public Vector4 BGBG { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Y); }
        public Vector4 BGBB { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Z); }
        public Vector4 BGBA { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.W); }
        public Vector4 BGAR { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.W = value.Z; v.X = value.W; } }
        public Vector4 BGAG { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.Y); }
        public Vector4 BGAB { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.Z); }
        public Vector4 BGAA { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.W); }
        public Vector4 BBRR { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.X); }
        public Vector4 BBRG { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Y); }
        public Vector4 BBRB { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Z); }
        public Vector4 BBRA { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.W); }
        public Vector4 BBGR { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.X); }
        public Vector4 BBGG { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Y); }
        public Vector4 BBGB { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Z); }
        public Vector4 BBGA { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.W); }
        public Vector4 BBBR { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.X); }
        public Vector4 BBBG { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Y); }
        public Vector4 BBBB { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Z); }
        public Vector4 BBBA { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.W); }
        public Vector4 BBAR { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.X); }
        public Vector4 BBAG { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.Y); }
        public Vector4 BBAB { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.Z); }
        public Vector4 BBAA { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.W); }
        public Vector4 BARR { [Do(Inline)]get => new(v.Z, v.W, v.X, v.X); }
        public Vector4 BARG { [Do(Inline)]get => new(v.Z, v.W, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.X = value.Z; v.Y = value.W; } }
        public Vector4 BARB { [Do(Inline)]get => new(v.Z, v.W, v.X, v.Z); }
        public Vector4 BARA { [Do(Inline)]get => new(v.Z, v.W, v.X, v.W); }
        public Vector4 BAGR { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.Y = value.Z; v.X = value.W; } }
        public Vector4 BAGG { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.Y); }
        public Vector4 BAGB { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.Z); }
        public Vector4 BAGA { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.W); }
        public Vector4 BABR { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.X); }
        public Vector4 BABG { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.Y); }
        public Vector4 BABB { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.Z); }
        public Vector4 BABA { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.W); }
        public Vector4 BAAR { [Do(Inline)]get => new(v.Z, v.W, v.W, v.X); }
        public Vector4 BAAG { [Do(Inline)]get => new(v.Z, v.W, v.W, v.Y); }
        public Vector4 BAAB { [Do(Inline)]get => new(v.Z, v.W, v.W, v.Z); }
        public Vector4 BAAA { [Do(Inline)]get => new(v.Z, v.W, v.W, v.W); }
        public Vector4 ARRR { [Do(Inline)]get => new(v.W, v.X, v.X, v.X); }
        public Vector4 ARRG { [Do(Inline)]get => new(v.W, v.X, v.X, v.Y); }
        public Vector4 ARRB { [Do(Inline)]get => new(v.W, v.X, v.X, v.Z); }
        public Vector4 ARRA { [Do(Inline)]get => new(v.W, v.X, v.X, v.W); }
        public Vector4 ARGR { [Do(Inline)]get => new(v.W, v.X, v.Y, v.X); }
        public Vector4 ARGG { [Do(Inline)]get => new(v.W, v.X, v.Y, v.Y); }
        public Vector4 ARGB { [Do(Inline)]get => new(v.W, v.X, v.Y, v.Z); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Y = value.Z; v.Z = value.W; } }
        public Vector4 ARGA { [Do(Inline)]get => new(v.W, v.X, v.Y, v.W); }
        public Vector4 ARBR { [Do(Inline)]get => new(v.W, v.X, v.Z, v.X); }
        public Vector4 ARBG { [Do(Inline)]get => new(v.W, v.X, v.Z, v.Y); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Z = value.Z; v.Y = value.W; } }
        public Vector4 ARBB { [Do(Inline)]get => new(v.W, v.X, v.Z, v.Z); }
        public Vector4 ARBA { [Do(Inline)]get => new(v.W, v.X, v.Z, v.W); }
        public Vector4 ARAR { [Do(Inline)]get => new(v.W, v.X, v.W, v.X); }
        public Vector4 ARAG { [Do(Inline)]get => new(v.W, v.X, v.W, v.Y); }
        public Vector4 ARAB { [Do(Inline)]get => new(v.W, v.X, v.W, v.Z); }
        public Vector4 ARAA { [Do(Inline)]get => new(v.W, v.X, v.W, v.W); }
        public Vector4 AGRR { [Do(Inline)]get => new(v.W, v.Y, v.X, v.X); }
        public Vector4 AGRG { [Do(Inline)]get => new(v.W, v.Y, v.X, v.Y); }
        public Vector4 AGRB { [Do(Inline)]get => new(v.W, v.Y, v.X, v.Z); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.X = value.Z; v.Z = value.W; } }
        public Vector4 AGRA { [Do(Inline)]get => new(v.W, v.Y, v.X, v.W); }
        public Vector4 AGGR { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.X); }
        public Vector4 AGGG { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.Y); }
        public Vector4 AGGB { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.Z); }
        public Vector4 AGGA { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.W); }
        public Vector4 AGBR { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.X); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.Z = value.Z; v.X = value.W; } }
        public Vector4 AGBG { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.Y); }
        public Vector4 AGBB { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.Z); }
        public Vector4 AGBA { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.W); }
        public Vector4 AGAR { [Do(Inline)]get => new(v.W, v.Y, v.W, v.X); }
        public Vector4 AGAG { [Do(Inline)]get => new(v.W, v.Y, v.W, v.Y); }
        public Vector4 AGAB { [Do(Inline)]get => new(v.W, v.Y, v.W, v.Z); }
        public Vector4 AGAA { [Do(Inline)]get => new(v.W, v.Y, v.W, v.W); }
        public Vector4 ABRR { [Do(Inline)]get => new(v.W, v.Z, v.X, v.X); }
        public Vector4 ABRG { [Do(Inline)]get => new(v.W, v.Z, v.X, v.Y); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.X = value.Z; v.Y = value.W; } }
        public Vector4 ABRB { [Do(Inline)]get => new(v.W, v.Z, v.X, v.Z); }
        public Vector4 ABRA { [Do(Inline)]get => new(v.W, v.Z, v.X, v.W); }
        public Vector4 ABGR { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.X); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.Y = value.Z; v.X = value.W; } }
        public Vector4 ABGG { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.Y); }
        public Vector4 ABGB { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.Z); }
        public Vector4 ABGA { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.W); }
        public Vector4 ABBR { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.X); }
        public Vector4 ABBG { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.Y); }
        public Vector4 ABBB { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.Z); }
        public Vector4 ABBA { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.W); }
        public Vector4 ABAR { [Do(Inline)]get => new(v.W, v.Z, v.W, v.X); }
        public Vector4 ABAG { [Do(Inline)]get => new(v.W, v.Z, v.W, v.Y); }
        public Vector4 ABAB { [Do(Inline)]get => new(v.W, v.Z, v.W, v.Z); }
        public Vector4 ABAA { [Do(Inline)]get => new(v.W, v.Z, v.W, v.W); }
        public Vector4 AARR { [Do(Inline)]get => new(v.W, v.W, v.X, v.X); }
        public Vector4 AARG { [Do(Inline)]get => new(v.W, v.W, v.X, v.Y); }
        public Vector4 AARB { [Do(Inline)]get => new(v.W, v.W, v.X, v.Z); }
        public Vector4 AARA { [Do(Inline)]get => new(v.W, v.W, v.X, v.W); }
        public Vector4 AAGR { [Do(Inline)]get => new(v.W, v.W, v.Y, v.X); }
        public Vector4 AAGG { [Do(Inline)]get => new(v.W, v.W, v.Y, v.Y); }
        public Vector4 AAGB { [Do(Inline)]get => new(v.W, v.W, v.Y, v.Z); }
        public Vector4 AAGA { [Do(Inline)]get => new(v.W, v.W, v.Y, v.W); }
        public Vector4 AABR { [Do(Inline)]get => new(v.W, v.W, v.Z, v.X); }
        public Vector4 AABG { [Do(Inline)]get => new(v.W, v.W, v.Z, v.Y); }
        public Vector4 AABB { [Do(Inline)]get => new(v.W, v.W, v.Z, v.Z); }
        public Vector4 AABA { [Do(Inline)]get => new(v.W, v.W, v.Z, v.W); }
        public Vector4 AAAR { [Do(Inline)]get => new(v.W, v.W, v.W, v.X); }
        public Vector4 AAAG { [Do(Inline)]get => new(v.W, v.W, v.W, v.Y); }
        public Vector4 AAAB { [Do(Inline)]get => new(v.W, v.W, v.W, v.Z); }
        public Vector4 AAAA { [Do(Inline)]get => new(v.W, v.W, v.W, v.W); }
    }
    
    extension<T>(ref Vector4D<T> v) where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
    {
        public T R { [Do(Inline)]get => v.X; [Do(Inline)]set => v.X = value; }
        public T G { [Do(Inline)]get => v.Y; [Do(Inline)]set => v.Y = value; }
        public T B { [Do(Inline)]get => v.Z; [Do(Inline)]set => v.Z = value; }
        public T A { [Do(Inline)]get => v.W; [Do(Inline)]set => v.W = value; }

        public Vector2D<T> RR { [Do(Inline)]get => new(v.X, v.X); }
        public Vector2D<T> RG { [Do(Inline)]get => new(v.X, v.Y); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; } }
        public Vector2D<T> RB { [Do(Inline)]get => new(v.X, v.Z); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; } }
        public Vector2D<T> RA { [Do(Inline)]get => new(v.X, v.W); [Do(Inline)]set { v.X = value.X; v.W = value.Y; } }
        public Vector2D<T> GR { [Do(Inline)]get => new(v.Y, v.X); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; } }
        public Vector2D<T> GG { [Do(Inline)]get => new(v.Y, v.Y); }
        public Vector2D<T> GB { [Do(Inline)]get => new(v.Y, v.Z); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; } }
        public Vector2D<T> GA { [Do(Inline)]get => new(v.Y, v.W); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; } }
        public Vector2D<T> BR { [Do(Inline)]get => new(v.Z, v.X); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; } }
        public Vector2D<T> BG { [Do(Inline)]get => new(v.Z, v.Y); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; } }
        public Vector2D<T> BB { [Do(Inline)]get => new(v.Z, v.Z); }
        public Vector2D<T> BA { [Do(Inline)]get => new(v.Z, v.W); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; } }
        public Vector2D<T> AR { [Do(Inline)]get => new(v.W, v.X); [Do(Inline)]set { v.W = value.X; v.X = value.Y; } }
        public Vector2D<T> AG { [Do(Inline)]get => new(v.W, v.Y); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; } }
        public Vector2D<T> AB { [Do(Inline)]get => new(v.W, v.Z); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; } }
        public Vector2D<T> AA { [Do(Inline)]get => new(v.W, v.W); }
        
        public Vector3D<T> RRR { [Do(Inline)]get => new(v.X, v.X, v.X); }
        public Vector3D<T> RRG { [Do(Inline)]get => new(v.X, v.X, v.Y); }
        public Vector3D<T> RRB { [Do(Inline)]get => new(v.X, v.X, v.Z); }
        public Vector3D<T> RRA { [Do(Inline)]get => new(v.X, v.X, v.W); }
        public Vector3D<T> RGR { [Do(Inline)]get => new(v.X, v.Y, v.X); }
        public Vector3D<T> RGG { [Do(Inline)]get => new(v.X, v.Y, v.Y); }
        public Vector3D<T> RGB { [Do(Inline)]get => new(v.X, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3D<T> RGA { [Do(Inline)]get => new(v.X, v.Y, v.W); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.W = value.Z; } }
        public Vector3D<T> RBR { [Do(Inline)]get => new(v.X, v.Z, v.X); }
        public Vector3D<T> RBG { [Do(Inline)]get => new(v.X, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3D<T> RBB { [Do(Inline)]get => new(v.X, v.Z, v.Z); }
        public Vector3D<T> RBA { [Do(Inline)]get => new(v.X, v.Z, v.W); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.W = value.Z; } }
        public Vector3D<T> RAR { [Do(Inline)]get => new(v.X, v.W, v.X); }
        public Vector3D<T> RAG { [Do(Inline)]get => new(v.X, v.W, v.Y); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Y = value.Z; } }
        public Vector3D<T> RAB { [Do(Inline)]get => new(v.X, v.W, v.Z); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Z = value.Z; } }
        public Vector3D<T> RAA { [Do(Inline)]get => new(v.X, v.W, v.W); }
        public Vector3D<T> GRR { [Do(Inline)]get => new(v.Y, v.X, v.X); }
        public Vector3D<T> GRG { [Do(Inline)]get => new(v.Y, v.X, v.Y); }
        public Vector3D<T> GRB { [Do(Inline)]get => new(v.Y, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3D<T> GRA { [Do(Inline)]get => new(v.Y, v.X, v.W); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.W = value.Z; } }
        public Vector3D<T> GGR { [Do(Inline)]get => new(v.Y, v.Y, v.X); }
        public Vector3D<T> GGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y); }
        public Vector3D<T> GGB { [Do(Inline)]get => new(v.Y, v.Y, v.Z); }
        public Vector3D<T> GGA { [Do(Inline)]get => new(v.Y, v.Y, v.W); }
        public Vector3D<T> GBR { [Do(Inline)]get => new(v.Y, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3D<T> GBG { [Do(Inline)]get => new(v.Y, v.Z, v.Y); }
        public Vector3D<T> GBB { [Do(Inline)]get => new(v.Y, v.Z, v.Z); }
        public Vector3D<T> GBA { [Do(Inline)]get => new(v.Y, v.Z, v.W); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.W = value.Z; } }
        public Vector3D<T> GAR { [Do(Inline)]get => new(v.Y, v.W, v.X); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.X = value.Z; } }
        public Vector3D<T> GAG { [Do(Inline)]get => new(v.Y, v.W, v.Y); }
        public Vector3D<T> GAB { [Do(Inline)]get => new(v.Y, v.W, v.Z); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.Z = value.Z; } }
        public Vector3D<T> GAA { [Do(Inline)]get => new(v.Y, v.W, v.W); }
        public Vector3D<T> BRR { [Do(Inline)]get => new(v.Z, v.X, v.X); }
        public Vector3D<T> BRG { [Do(Inline)]get => new(v.Z, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3D<T> BRB { [Do(Inline)]get => new(v.Z, v.X, v.Z); }
        public Vector3D<T> BRA { [Do(Inline)]get => new(v.Z, v.X, v.W); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.W = value.Z; } }
        public Vector3D<T> BGR { [Do(Inline)]get => new(v.Z, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3D<T> BGG { [Do(Inline)]get => new(v.Z, v.Y, v.Y); }
        public Vector3D<T> BGB { [Do(Inline)]get => new(v.Z, v.Y, v.Z); }
        public Vector3D<T> BGA { [Do(Inline)]get => new(v.Z, v.Y, v.W); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.W = value.Z; } }
        public Vector3D<T> BBR { [Do(Inline)]get => new(v.Z, v.Z, v.X); }
        public Vector3D<T> BBG { [Do(Inline)]get => new(v.Z, v.Z, v.Y); }
        public Vector3D<T> BBB { [Do(Inline)]get => new(v.Z, v.Z, v.Z); }
        public Vector3D<T> BBA { [Do(Inline)]get => new(v.Z, v.Z, v.W); }
        public Vector3D<T> BAR { [Do(Inline)]get => new(v.Z, v.W, v.X); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.X = value.Z; } }
        public Vector3D<T> BAG { [Do(Inline)]get => new(v.Z, v.W, v.Y); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.Y = value.Z; } }
        public Vector3D<T> BAB { [Do(Inline)]get => new(v.Z, v.W, v.Z); }
        public Vector3D<T> BAA { [Do(Inline)]get => new(v.Z, v.W, v.W); }
        public Vector3D<T> ARR { [Do(Inline)]get => new(v.W, v.X, v.X); }
        public Vector3D<T> ARG { [Do(Inline)]get => new(v.W, v.X, v.Y); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Y = value.Z; } }
        public Vector3D<T> ARB { [Do(Inline)]get => new(v.W, v.X, v.Z); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Z = value.Z; } }
        public Vector3D<T> ARA { [Do(Inline)]get => new(v.W, v.X, v.W); }
        public Vector3D<T> AGR { [Do(Inline)]get => new(v.W, v.Y, v.X); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.X = value.Z; } }
        public Vector3D<T> AGG { [Do(Inline)]get => new(v.W, v.Y, v.Y); }
        public Vector3D<T> AGB { [Do(Inline)]get => new(v.W, v.Y, v.Z); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.Z = value.Z; } }
        public Vector3D<T> AGA { [Do(Inline)]get => new(v.W, v.Y, v.W); }
        public Vector3D<T> ABR { [Do(Inline)]get => new(v.W, v.Z, v.X); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.X = value.Z; } }
        public Vector3D<T> ABG { [Do(Inline)]get => new(v.W, v.Z, v.Y); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.Y = value.Z; } }
        public Vector3D<T> ABB { [Do(Inline)]get => new(v.W, v.Z, v.Z); }
        public Vector3D<T> ABA { [Do(Inline)]get => new(v.W, v.Z, v.W); }
        public Vector3D<T> AAR { [Do(Inline)]get => new(v.W, v.W, v.X); }
        public Vector3D<T> AAG { [Do(Inline)]get => new(v.W, v.W, v.Y); }
        public Vector3D<T> AAB { [Do(Inline)]get => new(v.W, v.W, v.Z); }
        public Vector3D<T> AAA { [Do(Inline)]get => new(v.W, v.W, v.W); }
        
        public Vector4D<T> RRRR { [Do(Inline)]get => new(v.X, v.X, v.X, v.X); }
        public Vector4D<T> RRRG { [Do(Inline)]get => new(v.X, v.X, v.X, v.Y); }
        public Vector4D<T> RRRB { [Do(Inline)]get => new(v.X, v.X, v.X, v.Z); }
        public Vector4D<T> RRRA { [Do(Inline)]get => new(v.X, v.X, v.X, v.W); }
        public Vector4D<T> RRGR { [Do(Inline)]get => new(v.X, v.X, v.Y, v.X); }
        public Vector4D<T> RRGG { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Y); }
        public Vector4D<T> RRGB { [Do(Inline)]get => new(v.X, v.X, v.Y, v.Z); }
        public Vector4D<T> RRGA { [Do(Inline)]get => new(v.X, v.X, v.Y, v.W); }
        public Vector4D<T> RRBR { [Do(Inline)]get => new(v.X, v.X, v.Z, v.X); }
        public Vector4D<T> RRBG { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Y); }
        public Vector4D<T> RRBB { [Do(Inline)]get => new(v.X, v.X, v.Z, v.Z); }
        public Vector4D<T> RRBA { [Do(Inline)]get => new(v.X, v.X, v.Z, v.W); }
        public Vector4D<T> RRAR { [Do(Inline)]get => new(v.X, v.X, v.W, v.X); }
        public Vector4D<T> RRAG { [Do(Inline)]get => new(v.X, v.X, v.W, v.Y); }
        public Vector4D<T> RRAB { [Do(Inline)]get => new(v.X, v.X, v.W, v.Z); }
        public Vector4D<T> RRAA { [Do(Inline)]get => new(v.X, v.X, v.W, v.W); }
        public Vector4D<T> RGRR { [Do(Inline)]get => new(v.X, v.Y, v.X, v.X); }
        public Vector4D<T> RGRG { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Y); }
        public Vector4D<T> RGRB { [Do(Inline)]get => new(v.X, v.Y, v.X, v.Z); }
        public Vector4D<T> RGRA { [Do(Inline)]get => new(v.X, v.Y, v.X, v.W); }
        public Vector4D<T> RGGR { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.X); }
        public Vector4D<T> RGGG { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Y); }
        public Vector4D<T> RGGB { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.Z); }
        public Vector4D<T> RGGA { [Do(Inline)]get => new(v.X, v.Y, v.Y, v.W); }
        public Vector4D<T> RGBR { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.X); }
        public Vector4D<T> RGBG { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Y); }
        public Vector4D<T> RGBB { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.Z); }
        public Vector4D<T> RGBA { [Do(Inline)]get => new(v.X, v.Y, v.Z, v.W); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.Z = value.Z; v.W = value.W; } }
        public Vector4D<T> RGAR { [Do(Inline)]get => new(v.X, v.Y, v.W, v.X); }
        public Vector4D<T> RGAG { [Do(Inline)]get => new(v.X, v.Y, v.W, v.Y); }
        public Vector4D<T> RGAB { [Do(Inline)]get => new(v.X, v.Y, v.W, v.Z); [Do(Inline)]set { v.X = value.X; v.Y = value.Y; v.W = value.Z; v.Z = value.W; } }
        public Vector4D<T> RGAA { [Do(Inline)]get => new(v.X, v.Y, v.W, v.W); }
        public Vector4D<T> RBRR { [Do(Inline)]get => new(v.X, v.Z, v.X, v.X); }
        public Vector4D<T> RBRG { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Y); }
        public Vector4D<T> RBRB { [Do(Inline)]get => new(v.X, v.Z, v.X, v.Z); }
        public Vector4D<T> RBRA { [Do(Inline)]get => new(v.X, v.Z, v.X, v.W); }
        public Vector4D<T> RBGR { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.X); }
        public Vector4D<T> RBGG { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Y); }
        public Vector4D<T> RBGB { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.Z); }
        public Vector4D<T> RBGA { [Do(Inline)]get => new(v.X, v.Z, v.Y, v.W); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.Y = value.Z; v.W = value.W; } }
        public Vector4D<T> RBBR { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.X); }
        public Vector4D<T> RBBG { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Y); }
        public Vector4D<T> RBBB { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.Z); }
        public Vector4D<T> RBBA { [Do(Inline)]get => new(v.X, v.Z, v.Z, v.W); }
        public Vector4D<T> RBAR { [Do(Inline)]get => new(v.X, v.Z, v.W, v.X); }
        public Vector4D<T> RBAG { [Do(Inline)]get => new(v.X, v.Z, v.W, v.Y); [Do(Inline)]set { v.X = value.X; v.Z = value.Y; v.W = value.Z; v.Y = value.W; } }
        public Vector4D<T> RBAB { [Do(Inline)]get => new(v.X, v.Z, v.W, v.Z); }
        public Vector4D<T> RBAA { [Do(Inline)]get => new(v.X, v.Z, v.W, v.W); }
        public Vector4D<T> RARR { [Do(Inline)]get => new(v.X, v.W, v.X, v.X); }
        public Vector4D<T> RARG { [Do(Inline)]get => new(v.X, v.W, v.X, v.Y); }
        public Vector4D<T> RARB { [Do(Inline)]get => new(v.X, v.W, v.X, v.Z); }
        public Vector4D<T> RARA { [Do(Inline)]get => new(v.X, v.W, v.X, v.W); }
        public Vector4D<T> RAGR { [Do(Inline)]get => new(v.X, v.W, v.Y, v.X); }
        public Vector4D<T> RAGG { [Do(Inline)]get => new(v.X, v.W, v.Y, v.Y); }
        public Vector4D<T> RAGB { [Do(Inline)]get => new(v.X, v.W, v.Y, v.Z); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Y = value.Z; v.Z = value.W; } }
        public Vector4D<T> RAGA { [Do(Inline)]get => new(v.X, v.W, v.Y, v.W); }
        public Vector4D<T> RABR { [Do(Inline)]get => new(v.X, v.W, v.Z, v.X); }
        public Vector4D<T> RABG { [Do(Inline)]get => new(v.X, v.W, v.Z, v.Y); [Do(Inline)]set { v.X = value.X; v.W = value.Y; v.Z = value.Z; v.Y = value.W; } }
        public Vector4D<T> RABB { [Do(Inline)]get => new(v.X, v.W, v.Z, v.Z); }
        public Vector4D<T> RABA { [Do(Inline)]get => new(v.X, v.W, v.Z, v.W); }
        public Vector4D<T> RAAR { [Do(Inline)]get => new(v.X, v.W, v.W, v.X); }
        public Vector4D<T> RAAG { [Do(Inline)]get => new(v.X, v.W, v.W, v.Y); }
        public Vector4D<T> RAAB { [Do(Inline)]get => new(v.X, v.W, v.W, v.Z); }
        public Vector4D<T> RAAA { [Do(Inline)]get => new(v.X, v.W, v.W, v.W); }
        public Vector4D<T> GRRR { [Do(Inline)]get => new(v.Y, v.X, v.X, v.X); }
        public Vector4D<T> GRRG { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Y); }
        public Vector4D<T> GRRB { [Do(Inline)]get => new(v.Y, v.X, v.X, v.Z); }
        public Vector4D<T> GRRA { [Do(Inline)]get => new(v.Y, v.X, v.X, v.W); }
        public Vector4D<T> GRGR { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.X); }
        public Vector4D<T> GRGG { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Y); }
        public Vector4D<T> GRGB { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.Z); }
        public Vector4D<T> GRGA { [Do(Inline)]get => new(v.Y, v.X, v.Y, v.W); }
        public Vector4D<T> GRBR { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.X); }
        public Vector4D<T> GRBG { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Y); }
        public Vector4D<T> GRBB { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.Z); }
        public Vector4D<T> GRBA { [Do(Inline)]get => new(v.Y, v.X, v.Z, v.W); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.Z = value.Z; v.W = value.W; } }
        public Vector4D<T> GRAR { [Do(Inline)]get => new(v.Y, v.X, v.W, v.X); }
        public Vector4D<T> GRAG { [Do(Inline)]get => new(v.Y, v.X, v.W, v.Y); }
        public Vector4D<T> GRAB { [Do(Inline)]get => new(v.Y, v.X, v.W, v.Z); [Do(Inline)]set { v.Y = value.X; v.X = value.Y; v.W = value.Z; v.Z = value.W; } }
        public Vector4D<T> GRAA { [Do(Inline)]get => new(v.Y, v.X, v.W, v.W); }
        public Vector4D<T> GGRR { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.X); }
        public Vector4D<T> GGRG { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Y); }
        public Vector4D<T> GGRB { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.Z); }
        public Vector4D<T> GGRA { [Do(Inline)]get => new(v.Y, v.Y, v.X, v.W); }
        public Vector4D<T> GGGR { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.X); }
        public Vector4D<T> GGGG { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Y); }
        public Vector4D<T> GGGB { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.Z); }
        public Vector4D<T> GGGA { [Do(Inline)]get => new(v.Y, v.Y, v.Y, v.W); }
        public Vector4D<T> GGBR { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.X); }
        public Vector4D<T> GGBG { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Y); }
        public Vector4D<T> GGBB { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.Z); }
        public Vector4D<T> GGBA { [Do(Inline)]get => new(v.Y, v.Y, v.Z, v.W); }
        public Vector4D<T> GGAR { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.X); }
        public Vector4D<T> GGAG { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.Y); }
        public Vector4D<T> GGAB { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.Z); }
        public Vector4D<T> GGAA { [Do(Inline)]get => new(v.Y, v.Y, v.W, v.W); }
        public Vector4D<T> GBRR { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.X); }
        public Vector4D<T> GBRG { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Y); }
        public Vector4D<T> GBRB { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.Z); }
        public Vector4D<T> GBRA { [Do(Inline)]get => new(v.Y, v.Z, v.X, v.W); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.X = value.Z; v.W = value.W; } }
        public Vector4D<T> GBGR { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.X); }
        public Vector4D<T> GBGG { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Y); }
        public Vector4D<T> GBGB { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.Z); }
        public Vector4D<T> GBGA { [Do(Inline)]get => new(v.Y, v.Z, v.Y, v.W); }
        public Vector4D<T> GBBR { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.X); }
        public Vector4D<T> GBBG { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Y); }
        public Vector4D<T> GBBB { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.Z); }
        public Vector4D<T> GBBA { [Do(Inline)]get => new(v.Y, v.Z, v.Z, v.W); }
        public Vector4D<T> GBAR { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.X); [Do(Inline)]set { v.Y = value.X; v.Z = value.Y; v.W = value.Z; v.X = value.W; } }
        public Vector4D<T> GBAG { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.Y); }
        public Vector4D<T> GBAB { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.Z); }
        public Vector4D<T> GBAA { [Do(Inline)]get => new(v.Y, v.Z, v.W, v.W); }
        public Vector4D<T> GARR { [Do(Inline)]get => new(v.Y, v.W, v.X, v.X); }
        public Vector4D<T> GARG { [Do(Inline)]get => new(v.Y, v.W, v.X, v.Y); }
        public Vector4D<T> GARB { [Do(Inline)]get => new(v.Y, v.W, v.X, v.Z); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.X = value.Z; v.Z = value.W; } }
        public Vector4D<T> GARA { [Do(Inline)]get => new(v.Y, v.W, v.X, v.W); }
        public Vector4D<T> GAGR { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.X); }
        public Vector4D<T> GAGG { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.Y); }
        public Vector4D<T> GAGB { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.Z); }
        public Vector4D<T> GAGA { [Do(Inline)]get => new(v.Y, v.W, v.Y, v.W); }
        public Vector4D<T> GABR { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.X); [Do(Inline)]set { v.Y = value.X; v.W = value.Y; v.Z = value.Z; v.X = value.W; } }
        public Vector4D<T> GABG { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.Y); }
        public Vector4D<T> GABB { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.Z); }
        public Vector4D<T> GABA { [Do(Inline)]get => new(v.Y, v.W, v.Z, v.W); }
        public Vector4D<T> GAAR { [Do(Inline)]get => new(v.Y, v.W, v.W, v.X); }
        public Vector4D<T> GAAG { [Do(Inline)]get => new(v.Y, v.W, v.W, v.Y); }
        public Vector4D<T> GAAB { [Do(Inline)]get => new(v.Y, v.W, v.W, v.Z); }
        public Vector4D<T> GAAA { [Do(Inline)]get => new(v.Y, v.W, v.W, v.W); }
        public Vector4D<T> BRRR { [Do(Inline)]get => new(v.Z, v.X, v.X, v.X); }
        public Vector4D<T> BRRG { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Y); }
        public Vector4D<T> BRRB { [Do(Inline)]get => new(v.Z, v.X, v.X, v.Z); }
        public Vector4D<T> BRRA { [Do(Inline)]get => new(v.Z, v.X, v.X, v.W); }
        public Vector4D<T> BRGR { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.X); }
        public Vector4D<T> BRGG { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Y); }
        public Vector4D<T> BRGB { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.Z); }
        public Vector4D<T> BRGA { [Do(Inline)]get => new(v.Z, v.X, v.Y, v.W); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.Y = value.Z; v.W = value.W; } }
        public Vector4D<T> BRBR { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.X); }
        public Vector4D<T> BRBG { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Y); }
        public Vector4D<T> BRBB { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.Z); }
        public Vector4D<T> BRBA { [Do(Inline)]get => new(v.Z, v.X, v.Z, v.W); }
        public Vector4D<T> BRAR { [Do(Inline)]get => new(v.Z, v.X, v.W, v.X); }
        public Vector4D<T> BRAG { [Do(Inline)]get => new(v.Z, v.X, v.W, v.Y); [Do(Inline)]set { v.Z = value.X; v.X = value.Y; v.W = value.Z; v.Y = value.W; } }
        public Vector4D<T> BRAB { [Do(Inline)]get => new(v.Z, v.X, v.W, v.Z); }
        public Vector4D<T> BRAA { [Do(Inline)]get => new(v.Z, v.X, v.W, v.W); }
        public Vector4D<T> BGRR { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.X); }
        public Vector4D<T> BGRG { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Y); }
        public Vector4D<T> BGRB { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.Z); }
        public Vector4D<T> BGRA { [Do(Inline)]get => new(v.Z, v.Y, v.X, v.W); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.X = value.Z; v.W = value.W; } }
        public Vector4D<T> BGGR { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.X); }
        public Vector4D<T> BGGG { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Y); }
        public Vector4D<T> BGGB { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.Z); }
        public Vector4D<T> BGGA { [Do(Inline)]get => new(v.Z, v.Y, v.Y, v.W); }
        public Vector4D<T> BGBR { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.X); }
        public Vector4D<T> BGBG { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Y); }
        public Vector4D<T> BGBB { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.Z); }
        public Vector4D<T> BGBA { [Do(Inline)]get => new(v.Z, v.Y, v.Z, v.W); }
        public Vector4D<T> BGAR { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.X); [Do(Inline)]set { v.Z = value.X; v.Y = value.Y; v.W = value.Z; v.X = value.W; } }
        public Vector4D<T> BGAG { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.Y); }
        public Vector4D<T> BGAB { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.Z); }
        public Vector4D<T> BGAA { [Do(Inline)]get => new(v.Z, v.Y, v.W, v.W); }
        public Vector4D<T> BBRR { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.X); }
        public Vector4D<T> BBRG { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Y); }
        public Vector4D<T> BBRB { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.Z); }
        public Vector4D<T> BBRA { [Do(Inline)]get => new(v.Z, v.Z, v.X, v.W); }
        public Vector4D<T> BBGR { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.X); }
        public Vector4D<T> BBGG { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Y); }
        public Vector4D<T> BBGB { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.Z); }
        public Vector4D<T> BBGA { [Do(Inline)]get => new(v.Z, v.Z, v.Y, v.W); }
        public Vector4D<T> BBBR { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.X); }
        public Vector4D<T> BBBG { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Y); }
        public Vector4D<T> BBBB { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.Z); }
        public Vector4D<T> BBBA { [Do(Inline)]get => new(v.Z, v.Z, v.Z, v.W); }
        public Vector4D<T> BBAR { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.X); }
        public Vector4D<T> BBAG { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.Y); }
        public Vector4D<T> BBAB { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.Z); }
        public Vector4D<T> BBAA { [Do(Inline)]get => new(v.Z, v.Z, v.W, v.W); }
        public Vector4D<T> BARR { [Do(Inline)]get => new(v.Z, v.W, v.X, v.X); }
        public Vector4D<T> BARG { [Do(Inline)]get => new(v.Z, v.W, v.X, v.Y); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.X = value.Z; v.Y = value.W; } }
        public Vector4D<T> BARB { [Do(Inline)]get => new(v.Z, v.W, v.X, v.Z); }
        public Vector4D<T> BARA { [Do(Inline)]get => new(v.Z, v.W, v.X, v.W); }
        public Vector4D<T> BAGR { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.X); [Do(Inline)]set { v.Z = value.X; v.W = value.Y; v.Y = value.Z; v.X = value.W; } }
        public Vector4D<T> BAGG { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.Y); }
        public Vector4D<T> BAGB { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.Z); }
        public Vector4D<T> BAGA { [Do(Inline)]get => new(v.Z, v.W, v.Y, v.W); }
        public Vector4D<T> BABR { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.X); }
        public Vector4D<T> BABG { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.Y); }
        public Vector4D<T> BABB { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.Z); }
        public Vector4D<T> BABA { [Do(Inline)]get => new(v.Z, v.W, v.Z, v.W); }
        public Vector4D<T> BAAR { [Do(Inline)]get => new(v.Z, v.W, v.W, v.X); }
        public Vector4D<T> BAAG { [Do(Inline)]get => new(v.Z, v.W, v.W, v.Y); }
        public Vector4D<T> BAAB { [Do(Inline)]get => new(v.Z, v.W, v.W, v.Z); }
        public Vector4D<T> BAAA { [Do(Inline)]get => new(v.Z, v.W, v.W, v.W); }
        public Vector4D<T> ARRR { [Do(Inline)]get => new(v.W, v.X, v.X, v.X); }
        public Vector4D<T> ARRG { [Do(Inline)]get => new(v.W, v.X, v.X, v.Y); }
        public Vector4D<T> ARRB { [Do(Inline)]get => new(v.W, v.X, v.X, v.Z); }
        public Vector4D<T> ARRA { [Do(Inline)]get => new(v.W, v.X, v.X, v.W); }
        public Vector4D<T> ARGR { [Do(Inline)]get => new(v.W, v.X, v.Y, v.X); }
        public Vector4D<T> ARGG { [Do(Inline)]get => new(v.W, v.X, v.Y, v.Y); }
        public Vector4D<T> ARGB { [Do(Inline)]get => new(v.W, v.X, v.Y, v.Z); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Y = value.Z; v.Z = value.W; } }
        public Vector4D<T> ARGA { [Do(Inline)]get => new(v.W, v.X, v.Y, v.W); }
        public Vector4D<T> ARBR { [Do(Inline)]get => new(v.W, v.X, v.Z, v.X); }
        public Vector4D<T> ARBG { [Do(Inline)]get => new(v.W, v.X, v.Z, v.Y); [Do(Inline)]set { v.W = value.X; v.X = value.Y; v.Z = value.Z; v.Y = value.W; } }
        public Vector4D<T> ARBB { [Do(Inline)]get => new(v.W, v.X, v.Z, v.Z); }
        public Vector4D<T> ARBA { [Do(Inline)]get => new(v.W, v.X, v.Z, v.W); }
        public Vector4D<T> ARAR { [Do(Inline)]get => new(v.W, v.X, v.W, v.X); }
        public Vector4D<T> ARAG { [Do(Inline)]get => new(v.W, v.X, v.W, v.Y); }
        public Vector4D<T> ARAB { [Do(Inline)]get => new(v.W, v.X, v.W, v.Z); }
        public Vector4D<T> ARAA { [Do(Inline)]get => new(v.W, v.X, v.W, v.W); }
        public Vector4D<T> AGRR { [Do(Inline)]get => new(v.W, v.Y, v.X, v.X); }
        public Vector4D<T> AGRG { [Do(Inline)]get => new(v.W, v.Y, v.X, v.Y); }
        public Vector4D<T> AGRB { [Do(Inline)]get => new(v.W, v.Y, v.X, v.Z); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.X = value.Z; v.Z = value.W; } }
        public Vector4D<T> AGRA { [Do(Inline)]get => new(v.W, v.Y, v.X, v.W); }
        public Vector4D<T> AGGR { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.X); }
        public Vector4D<T> AGGG { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.Y); }
        public Vector4D<T> AGGB { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.Z); }
        public Vector4D<T> AGGA { [Do(Inline)]get => new(v.W, v.Y, v.Y, v.W); }
        public Vector4D<T> AGBR { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.X); [Do(Inline)]set { v.W = value.X; v.Y = value.Y; v.Z = value.Z; v.X = value.W; } }
        public Vector4D<T> AGBG { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.Y); }
        public Vector4D<T> AGBB { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.Z); }
        public Vector4D<T> AGBA { [Do(Inline)]get => new(v.W, v.Y, v.Z, v.W); }
        public Vector4D<T> AGAR { [Do(Inline)]get => new(v.W, v.Y, v.W, v.X); }
        public Vector4D<T> AGAG { [Do(Inline)]get => new(v.W, v.Y, v.W, v.Y); }
        public Vector4D<T> AGAB { [Do(Inline)]get => new(v.W, v.Y, v.W, v.Z); }
        public Vector4D<T> AGAA { [Do(Inline)]get => new(v.W, v.Y, v.W, v.W); }
        public Vector4D<T> ABRR { [Do(Inline)]get => new(v.W, v.Z, v.X, v.X); }
        public Vector4D<T> ABRG { [Do(Inline)]get => new(v.W, v.Z, v.X, v.Y); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.X = value.Z; v.Y = value.W; } }
        public Vector4D<T> ABRB { [Do(Inline)]get => new(v.W, v.Z, v.X, v.Z); }
        public Vector4D<T> ABRA { [Do(Inline)]get => new(v.W, v.Z, v.X, v.W); }
        public Vector4D<T> ABGR { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.X); [Do(Inline)]set { v.W = value.X; v.Z = value.Y; v.Y = value.Z; v.X = value.W; } }
        public Vector4D<T> ABGG { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.Y); }
        public Vector4D<T> ABGB { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.Z); }
        public Vector4D<T> ABGA { [Do(Inline)]get => new(v.W, v.Z, v.Y, v.W); }
        public Vector4D<T> ABBR { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.X); }
        public Vector4D<T> ABBG { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.Y); }
        public Vector4D<T> ABBB { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.Z); }
        public Vector4D<T> ABBA { [Do(Inline)]get => new(v.W, v.Z, v.Z, v.W); }
        public Vector4D<T> ABAR { [Do(Inline)]get => new(v.W, v.Z, v.W, v.X); }
        public Vector4D<T> ABAG { [Do(Inline)]get => new(v.W, v.Z, v.W, v.Y); }
        public Vector4D<T> ABAB { [Do(Inline)]get => new(v.W, v.Z, v.W, v.Z); }
        public Vector4D<T> ABAA { [Do(Inline)]get => new(v.W, v.Z, v.W, v.W); }
        public Vector4D<T> AARR { [Do(Inline)]get => new(v.W, v.W, v.X, v.X); }
        public Vector4D<T> AARG { [Do(Inline)]get => new(v.W, v.W, v.X, v.Y); }
        public Vector4D<T> AARB { [Do(Inline)]get => new(v.W, v.W, v.X, v.Z); }
        public Vector4D<T> AARA { [Do(Inline)]get => new(v.W, v.W, v.X, v.W); }
        public Vector4D<T> AAGR { [Do(Inline)]get => new(v.W, v.W, v.Y, v.X); }
        public Vector4D<T> AAGG { [Do(Inline)]get => new(v.W, v.W, v.Y, v.Y); }
        public Vector4D<T> AAGB { [Do(Inline)]get => new(v.W, v.W, v.Y, v.Z); }
        public Vector4D<T> AAGA { [Do(Inline)]get => new(v.W, v.W, v.Y, v.W); }
        public Vector4D<T> AABR { [Do(Inline)]get => new(v.W, v.W, v.Z, v.X); }
        public Vector4D<T> AABG { [Do(Inline)]get => new(v.W, v.W, v.Z, v.Y); }
        public Vector4D<T> AABB { [Do(Inline)]get => new(v.W, v.W, v.Z, v.Z); }
        public Vector4D<T> AABA { [Do(Inline)]get => new(v.W, v.W, v.Z, v.W); }
        public Vector4D<T> AAAR { [Do(Inline)]get => new(v.W, v.W, v.W, v.X); }
        public Vector4D<T> AAAG { [Do(Inline)]get => new(v.W, v.W, v.W, v.Y); }
        public Vector4D<T> AAAB { [Do(Inline)]get => new(v.W, v.W, v.W, v.Z); }
        public Vector4D<T> AAAA { [Do(Inline)]get => new(v.W, v.W, v.W, v.W); }
    }
}