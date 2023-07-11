using Create.OpenGL.Textures;
using Create.Virtuals;
using OpenTK.Mathematics;
using Create.OpenGL.Mathematic;
using System.Diagnostics;

namespace Create.OpenGL;

/// <summary>
/// Mechanizm renderowania modelu
/// </summary>
[DebuggerTypeProxy(typeof(Proxy))]
[DebuggerDisplay("Shader: {(new Proxy(this)).get_status(),nq}")]
public sealed partial class Shader : IDisposable
{
    #region variables
    int handle;
    AttributInfo[] attributInfos;
    UniformInfo[] uniformInfos;
    Texture[] textures;
    VertexAttributesBind bind;
    bool disposed;

    (string name, int handle)? pozition_variable = null;
    (string name, int handle)? rotation_variable = null;
    (string name, int handle)? use_default_matrix_mehanic = null;
    (BlendingFactor, BlendingFactor)? blend = null;

    VirtualList<AttributInfo> virtual_attribut;
    VirtualList<UniformInfo> virtual_uniform;
    VirtualList<Texture> virtual_textures;
    CullFaceMode cull_face;
    bool alphatest, depthtest;
    #endregion

    #region disable constructor
#pragma warning disable CS8618
    private Shader() { }
#pragma warning restore CS8618
    #endregion

    #region get only
    /// <summary>
    /// Parametry przehowywane przez poszczegulne Wertexy
    /// </summary>
    public VirtualList<AttributInfo> Attributes => virtual_attribut;
    
    /// <summary>
    /// Statyczne parametry w <see cref="Shader"/>ze
    /// </summary>
    public VirtualList<UniformInfo> Uniforms => virtual_uniform;

    /// <summary>
    /// Wrzystkie tekstury używane w <see cref="Shader"/>ze
    /// </summary>
    public VirtualList<Texture> Textures => virtual_textures;

    /// <summary>
    /// Struktura danych przehowywanych w modelach tego <see cref="Shader"/>u
    /// </summary>
    public VertexAttributesBind ShaderBind => bind;

    /// <summary>
    /// Z jakich perspektyw trójkąty modelu nie są widoczne
    /// </summary>
    public CullFaceMode CullFace => cull_face;

    /// <summary>
    /// Odnośnik do Shaderu w pamięci karty graficznej
    /// </summary>
    public int Handle => handle;
    public bool IsDisposed => disposed;

    /// <summary>
    /// Jak w <see cref="Shader"/>ze nazwana jest zmienna przechowująca obecną pozycje modelu w przestrzeni
    /// </summary>
    internal (string name, int handle)? PozitionVariable => pozition_variable;

    /// <summary>
    /// Jak w <see cref="Shader"/>ze nazwana jest zmienna przechowująca obecną orjentacje modelu w przestrzeni
    /// </summary>
    internal (string name, int handle)? RotationVariable => rotation_variable;

    /// <summary>
    /// Jak w <see cref="Shader"/>ze nazwana jest zmienna przechowująca dodatkowy Matrix do modyfikacji modelu
    /// </summary>
    internal (string name, int handle)? DefaultMatrixSystem => use_default_matrix_mehanic;

    /// <summary>
    /// Ustawienia przezroczystości modelu
    /// </summary>
    internal (BlendingFactor s, BlendingFactor d)? blendfunc => blend;

    /// <summary>
    /// Testy przezroczystości i głębokości tego modelu
    /// </summary>
    internal (bool alphatest, bool depthtest) simple_mekanizms => (alphatest, depthtest);
    #endregion

    /// <summary>
    /// Ustawia nową teksture dla tego shaderu
    /// </summary>
    /// <param name="index"></param>
    /// <param name="texture"></param>
    internal void set_texture(int index, Texture texture) => textures[index] = texture;

    #region SetUniform
    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, int value) => set_parametr(name, ActiveUniformType.Int, u => GL.Uniform1(u.Handle, value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Vector2i value) => set_parametr(name, ActiveUniformType.IntVec2, u => GL.Uniform2(u.Handle, value), ActiveUniformType.UnsignedIntVec2);

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Vector3i value) => set_parametr(name, ActiveUniformType.IntVec3, u => GL.Uniform3(u.Handle, value), ActiveUniformType.UnsignedIntVec3);

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Vector4i value) => set_parametr(name, ActiveUniformType.IntVec4, u => GL.Uniform4(u.Handle, value), ActiveUniformType.UnsignedIntVec4);

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, bool value) => set_parametr(name, ActiveUniformType.Bool, u => GL.Uniform1(u.Handle, value ? 1 : 0));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Vector2b value) => set_parametr(name, ActiveUniformType.BoolVec2, u => GL.Uniform2(u.Handle, value.X ? 1 : 0, value.Y ? 1 : 0));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Vector3b value) => set_parametr(name, ActiveUniformType.BoolVec2, u => GL.Uniform3(u.Handle, value.X ? 1 : 0, value.Y ? 1 : 0, value.Z ? 1 : 0));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Vector4b value) => set_parametr(name, ActiveUniformType.BoolVec2, u => GL.Uniform4(u.Handle, value.X ? 1 : 0, value.Y ? 1 : 0, value.Z ? 1 : 0, value.W ? 1 : 0));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, float value) => set_parametr(name, ActiveUniformType.Float, u => GL.Uniform1(u.Handle, value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Vector2 value) => set_parametr(name, ActiveUniformType.FloatVec2, u => GL.Uniform2(u.Handle, value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Vector3 value) => set_parametr(name, ActiveUniformType.FloatVec3, u => GL.Uniform3(u.Handle, value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Vector4 value) => set_parametr(name, ActiveUniformType.FloatVec4, u => GL.Uniform4(u.Handle, value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]

    public Shader SetUniform(string name, Matrix2 value) => set_parametr(name, ActiveUniformType.FloatMat2, u => GL.UniformMatrix2(u.Handle, false, ref value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Matrix2x3 value) => set_parametr(name, ActiveUniformType.FloatMat2x3, u => GL.UniformMatrix2x3(u.Handle, false, ref value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Matrix2x4 value) => set_parametr(name, ActiveUniformType.FloatMat2x4, u => GL.UniformMatrix2x4(u.Handle, false, ref value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Matrix3x2 value) => set_parametr(name, ActiveUniformType.FloatMat3x2, u => GL.UniformMatrix3x2(u.Handle, false, ref value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Matrix3 value) => set_parametr(name, ActiveUniformType.FloatMat3, u => GL.UniformMatrix3(u.Handle, false, ref value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Matrix3x4 value) => set_parametr(name, ActiveUniformType.FloatMat3x4, u => GL.UniformMatrix3x4(u.Handle, false, ref value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Matrix4x2 value) => set_parametr(name, ActiveUniformType.FloatMat4x2, u => GL.UniformMatrix4x2(u.Handle, false, ref value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Matrix4x3 value) => set_parametr(name, ActiveUniformType.FloatMat4x3, u => GL.UniformMatrix4x3(u.Handle, false, ref value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Matrix4 value) => set_parametr(name, ActiveUniformType.FloatMat4, u => GL.UniformMatrix4(u.Handle, false, ref value));

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Texture2D texture) => set_parametr_test(name, ActiveUniformType.Sampler2D, t => textures[t.TextureNumer!.Value] = texture, ActiveUniformType.UnsignedIntSampler2D);

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, Texture2DArray texture) => set_parametr_test(name, ActiveUniformType.Sampler2DArray, t => textures[t.TextureNumer!.Value] = texture, ActiveUniformType.UnsignedIntSampler2DArray);

    /// <summary>
    /// Ustawienie statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    public Shader SetUniform(string name, RenderTexture texture) => set_parametr_test(name, ActiveUniformType.Sampler2D, t => textures[t.TextureNumer!.Value] = texture, ActiveUniformType.UnsignedIntSampler2D);

    /// <summary>
    /// Podstawa do zmieniania statycznych wartości w <see cref="Shader"/>ze
    /// </summary>
    [DebuggerHidden]
    Shader set_parametr(string name, ActiveUniformType type, Action<UniformInfo> func, ActiveUniformType? secondary_type = null)
    {
        var var_ = uniformInfos.FindAndWhere(u => u.Name == name);
        if (!var_.HasValue)
            throw new ArgumentException($"Parament with name \"{name}\" don't exist");
        if (var_.Value.element.Type != type && var_.Value.element.Type != secondary_type)
        {
            string? secound_type = (secondary_type != null ?(type.GetCSharpType() != secondary_type.Value.GetCSharpType() ? $" or {secondary_type.Value.GetCSharpType()}" : null) : null);
            throw new ArgumentException($"Types are not match\nexpected {type.GetCSharpType()}{secound_type}");
        }
        //MainTask.Run(() => 
        {
            GL.UseProgram(handle);
            func(var_.Value.element);
            GL.UseProgram(0);
        }//);
        return this;
    }

    /// <summary>
    /// Podstawa do zmieniania statycznych wartości w <see cref="Shader"/> kąkretnie Tekstur
    /// </summary>
    [DebuggerHidden]
    Shader set_parametr_test(string name, ActiveUniformType type, Action<UniformInfo> func, ActiveUniformType? secondary_type = null)
    {
        var var_ = uniformInfos.FindAndWhere(u => u.Name == name);
        if (!var_.HasValue)
            throw new ArgumentException($"Parament with name \"{name}\" don't exist");
        if (var_.Value.element.Type != type && var_.Value.element.Type != secondary_type)
        {
            string? secound_type = (secondary_type != null ?(type.GetCSharpType() != secondary_type.Value.GetCSharpType() ? $" or {secondary_type.Value.GetCSharpType()}" : null) : null);
            throw new ArgumentException($"Types are not match\nexpected {type.GetCSharpType()}{secound_type}");
        }
        func(var_.Value.element);
        return this;
    }
    #endregion

    #region destructor
    ~Shader() => Dispose();
    public void Dispose()
    {
        if (disposed)
            return;
        disposed = true;
        GC.SuppressFinalize(this);
        OpenGL.disposing.add(new disposing() { handle = handle });
        attributInfos = null!;
        uniformInfos = null!;
        textures = null!;
        virtual_attribut = VirtualList.Create<AttributInfo>().Finish();
        virtual_uniform = VirtualList.Create<UniformInfo>().Finish();
        virtual_textures = VirtualList.Create<Texture>().Finish();
    }

    /// <summary>
    /// Odpowiedzialny za niszczenie <see cref="Shader"/>a
    /// </summary>
    struct disposing : OpenGL.disposing.gl_element
    {
        public int handle;

        public void Dispose()
        {
            GL.UseProgram(0);
            GL.DeleteProgram(handle);
        }
    }
    #endregion

    #region shader variable info
    /// <summary>
    /// Informacje o pojedyńczej wartości przechowywanej w Verteksie modelu
    /// </summary>
    public struct AttributInfo
    {
        int handle;
        ActiveAttribType type;
        string name;
        public AttributInfo(int handle, ActiveAttribType type, string name)
        {
            this.handle = handle;
            this.type = type;
            this.name = name;
        }

        /// <summary>
        /// Identyfikator w <see cref="Shader"/>ze
        /// </summary>
        public int Handle => handle;

        /// <summary>
        /// Nazwa w <see cref="Shader"/>ze
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Typ wartości w formacie OpenGL
        /// </summary>
        public ActiveAttribType GLType => type;

        /// <summary>
        /// Odpowiednik <see cref="GLType"/> w formacie C#
        /// </summary>
        public Type? Type => type.GetCSharpType();
    }
    
    /// <summary>
    /// Informacje o pojedyńczej statycznej wartości w <see cref="Shader"/>ze
    /// </summary>
    public struct UniformInfo
    {
        int handle;
        ActiveUniformType type;
        string name;
        int? tex_id;

        public UniformInfo(int handle, ActiveUniformType type, string name)
        {
            this.handle = handle;
            this.type = type;
            this.name = name;
            tex_id = null;
        }
        public UniformInfo(int handle, ActiveUniformType type, string name, int tex_num)
        {
            this.handle = handle;
            this.type = type;
            this.name = name;
            tex_id = tex_num;
        }

        /// <summary>
        /// Identyfikator w <see cref="Shader"/>ze
        /// </summary>
        public int Handle => handle;

        /// <summary>
        /// Nazwa w <see cref="Shader"/>ze
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Typ wartości w formacie OpenGL
        /// </summary>
        public ActiveUniformType Type => type;

        /// <summary>
        /// Jeśli zawiera teksture to numer która to jest tekstura w kolejności w tym <see cref="Shader"/>ze
        /// </summary>
        public int? TextureNumer => tex_id;
    }
    
    /// <summary>
    /// Rozmieszczenie danych poszczegulnych Werteksów w pamięci karty graficznej
    /// </summary>
    public struct VertexAttributesBind
    {
        int byte_lenght;
        (int index, int offset)[] attributes;
        internal VertexAttributesBind(AttributInfo[] attributs)
        {
            byte_lenght = 0;
            attributes = new (int index, int offset)[attributs.Length];
            for (int i = 0; i < attributs.Length; i++)
            {
                attributes[i] = new(i, byte_lenght);
                byte_lenght += attributs[i].GLType.ElementByteSize();
            }
        }

        /// <summary>
        /// Wielkość w bitach pojedyńczego Werteksa
        /// </summary>
        public int ByteSize => byte_lenght;

        /// <summary>
        /// Pozycje i przesunięcie poszczegulnych informacji w modelu
        /// </summary>
        public ReadOnlySpan<(int index, int offset)> Binds => new(attributes);
    }
    #endregion
}
