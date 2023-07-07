using System;
using System.Collections.Generic;
namespace Create.Resource;

/// <summary>
/// Parametry jaki plik jest pobierany z <see cref="Resources"/>
/// </summary>
public ref struct GetStreamStruct
{
    object? sender;
    ResourceFile file;

    public GetStreamStruct() => throw new NotSupportedException();

    internal GetStreamStruct(object? sender, ResourceFile file)
    {
        this.sender = sender;
        this.file = file;
    }

    /// <summary>
    /// Opcjonalny obiekt z parametrami do stworzenia <see cref="Stream"/>u
    /// </summary>
    public object? Sender => sender;

    /// <summary>
    /// Plik w repozytorium z któreko pobiera <see cref="Stream"/>
    /// </summary>
    public ResourceFile File => file;
}
