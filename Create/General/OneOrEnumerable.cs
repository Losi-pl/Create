using System.Collections;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable MemberCanBePrivate.Global, UnusedType.Global, UnusedMember.Global

namespace Create.General;

public readonly struct OneOrEnumerable<T>
{
    private byte Index { get; init; }
    private T AsOneV { get; init; }
    private IEnumerable<T>? AsMany { get; init; } = null;

    #region One

        public bool TryGetAsOne([NotNullWhen(true)] out T? value) {
            if (Index == 1)
            {
                value = AsOneV!;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T>(T asOne) => new OneOrEnumerable<T> { AsMany = null, Index = 1, AsOneV = asOne };
        
        public T AsOne => Index == 1 ? AsOneV : throw new InvalidCastException();

        public bool IsOne => Index == 1;
        
        public OneOrEnumerable(T asOne)
        {
            Index = 1;
            AsOneV = asOne;
            AsMany = null;
        }

    #endregion
    
    #region Enumerable

        public bool TryGetAsEnumerable([NotNullWhen(true)] out IEnumerable<T>? value) {
            if (Index == 2)
            {
                value = AsMany!;
                return true;
            }
            value = null!;
            return false;
        }
        
        public IEnumerable<T> AsEnumerable => Index == 2 ? AsMany! : throw new InvalidCastException($"Union does not contain an Enumerable of type {typeof(IEnumerable<T>).Name}");

        public bool IsEnumerable => Index == 2;

        public OneOrEnumerable(IEnumerable<T> asEnumerable) 
        {
            Index = 2;
            AsOneV = default!;
            AsMany = asEnumerable;
        }

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<T, TOutput> oneCase, Func<IEnumerable<T>, TOutput> enumerableCase)
        {
            switch (this) {
                case {Index: 1} : return oneCase(AsOne);
                case {Index: 2} : return enumerableCase(AsEnumerable);
            }
            throw new ArgumentException("Union does not contain a value");
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<T, Task<TOutput>> oneCase, Func<IEnumerable<T>, Task<TOutput>> enumerableCase) 
        {
            return Index switch
            {
                1 => oneCase(AsOne),
                2 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<T, CancellationToken, Task<TOutput>> oneCase, Func<IEnumerable<T>, CancellationToken, Task<TOutput>> enumerableCase, 
            CancellationToken ct) 
        {
            return Index switch
            {
                1 => oneCase(AsOne, ct),
                2 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<T> oneCase, 
            Action<IEnumerable<T>> enumerableCase) 
        {
            switch(Index)
            {
                case 1: oneCase(AsOne); break;
                case 2: enumerableCase(AsEnumerable); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<T, Task> oneCase, Func<IEnumerable<T>, Task> enumerableCase)
        {
            return Index switch
            {
                1 => oneCase(AsOne),
                2 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<T, CancellationToken, Task> oneCase, Func<IEnumerable<T>, CancellationToken, Task> enumerableCase, 
            CancellationToken ct){
            return Index switch
            {
                1 => oneCase(AsOne, ct),
                2 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
}

public readonly struct OneOrEnumerable<T0, T1>
{
    private sbyte Index { get; }
    // ReSharper disable InconsistentNaming
    private T0 t0 { get; } = default!;
    private T1 t1 { get; } = default!;
    // ReSharper restore InconsistentNaming
    private IEnumerable Many { get; } = null!;

    #region One T0

        public bool TryGetAsOneT0(out T0 value) {
            if (Index == -1)
            {
                value = t0;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1>(T0 asOne) => new(asOne);

        public T0 AsOneT0 => Index == -1 ? t0 : throw new InvalidCastException();

        public bool IsOneT0 => Index == -1;
            
        public OneOrEnumerable(T0 asOne)
        {
            Index = -1;
            t0 = asOne;
        }

    #endregion
    
    #region One T1

        public bool TryGetAsOneT1(out T1 value) {
            if (Index == -2)
            {
                value = t1;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1>(T1 asOne) => new(asOne);

        public T1 AsOneT1 => Index == -2 ? t1 : throw new InvalidCastException();

        public bool IsOneT1 => Index == -2;
                
        public OneOrEnumerable(T1 asOne)
        {
            Index = -2;
            t1 = asOne;
        }

    #endregion
    
    #region One

        public bool TryGetAsOne(out Union<T0, T1> value) {
            switch (Index)
            {
                case -1:
                    value = t0;
                    return true;
                case -2:
                    value = t1;
                    return true;
                default:
                    value = default!;
                    return false;
            }
        }

        public static implicit operator OneOrEnumerable<T0, T1>(Union<T0, T1> asOne) => new(asOne);
        
        public Union<T0, T1> AsOne => Index switch {
            -1 => t0,
            -2 => t1,
            _ => throw new InvalidCastException()
        };

        public bool IsOne => Index < 0;
        
        public OneOrEnumerable(Union<T0, T1> asOne)
        {
            switch (asOne)
            {
                case {IsT0: true, AsT0: var value}:
                    {
                        Index = -1;
                        t0 = value;
                    }
                    break;
                case {IsT1: true, AsT1: var value}:
                    {
                        Index = -2;
                        t1 = value;
                    }
                    break;
                default:
                    throw new InvalidCastException();
            }
        }

    #endregion
    
    #region Enumerable T0

        public bool TryGetAsEnumerableT0([NotNullWhen(true)] out IEnumerable<T0>? value) {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T0> AsEnumerableT0 => Index == 1 ? (IEnumerable<T0>)Many : throw new InvalidCastException();

        public bool IsEnumerableT0 => Index == 1;
                
        public OneOrEnumerable(IEnumerable<T0> asEnumerable)
        {
            Index = 1;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T1

        public bool TryGetAsEnumerableT1([NotNullWhen(true)] out IEnumerable<T1>? value) {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T1> AsEnumerableT1 => Index == 2 ? (IEnumerable<T1>)Many : throw new InvalidCastException();

        public bool IsEnumerableT1 => Index == 2;
                    
        public OneOrEnumerable(IEnumerable<T1> asEnumerable)
        {
            Index = 2;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable

        public bool TryGetAsEnumerable(out UnionEnumerable<T0, T1> value) {
            if (Index is 1 or 2)
            {
                value = new((byte)Index, Many);
                return true;
            }
            value = default;
            return false;
        }
        
        public static implicit operator OneOrEnumerable<T0, T1>(UnionEnumerable<T0, T1> asEnumerable) => new(asEnumerable);
        
        public UnionEnumerable<T0, T1> AsEnumerable => Index > 0 ? new((byte)Index, Many) : throw new InvalidCastException($"Union does not contain an Enumerable");

        public bool IsEnumerable => Index > 0;

        public OneOrEnumerable(UnionEnumerable<T0, T1> asEnumerable)
        {
            Index = (sbyte)asEnumerable.Index;
            Many = (IEnumerable)asEnumerable.Value;
        }

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<Union<T0, T1>, TOutput> oneCase, Func<UnionEnumerable<T0, T1>, TOutput> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public TOutput Match<TOutput>(
            Func<T0, TOutput> oneT0Case, Func<IEnumerable<T0>, TOutput> enumerableT0Case,
            Func<T1, TOutput> oneT1Case, Func<IEnumerable<T1>, TOutput> enumerableT1Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1>, Task<TOutput>> oneCase,
            Func<UnionEnumerable<T0, T1>, Task<TOutput>> enumerableCase) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, Task<TOutput>> enumerableT0Case,
            Func<T1, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, Task<TOutput>> enumerableT1Case) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1>, CancellationToken, Task<TOutput>> oneCase,
            Func<UnionEnumerable<T0, T1>, CancellationToken, Task<TOutput>> enumerableCase, 
            CancellationToken ct) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, CancellationToken, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> enumerableT0Case, 
            Func<T1, CancellationToken, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> enumerableT1Case,
            CancellationToken ct) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct), 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct), 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<Union<T0, T1>> oneCase, 
            Action<UnionEnumerable<T0, T1>> enumerableCase) 
        {
            switch(Index)
            {
                case < 0: oneCase(AsOne); break;
                case > 0: enumerableCase(AsEnumerable); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }
        
        public void Switch(
            Action<T0> oneT0Case, Action<IEnumerable<T0>> enumerableT0Case,
            Action<T1> oneT1Case, Action<IEnumerable<T1>> enumerableT1Case) 
        {
            switch(Index)
            {
                case -1: oneT0Case(t0); break;
                case  1: enumerableT0Case((IEnumerable<T0>)Many); break;
                case -2: oneT1Case(t1); break;
                case  2: enumerableT1Case((IEnumerable<T1>)Many); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<Union<T0, T1>, Task> oneCase, 
            Func<UnionEnumerable<T0, T1>, Task> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, Task> oneT0Case, Func<IEnumerable<T0>, Task> enumerableT0Case,
            Func<T1, Task> oneT1Case, Func<IEnumerable<T1>, Task> enumerableT1Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<Union<T0, T1>, CancellationToken, Task> oneCase, 
            Func<UnionEnumerable<T0, T1>, CancellationToken, Task> enumerableCase, 
            CancellationToken ct)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, CancellationToken, Task> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task> enumerableT0Case, 
            Func<T1, CancellationToken, Task> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task> enumerableT1Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct), 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct), 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
}

public readonly struct OneOrEnumerable<T0, T1, T2>
{
    private sbyte Index { get; }
    // ReSharper disable InconsistentNaming
    private T0 t0 { get; } = default!;
    private T1 t1 { get; } = default!;
    private T2 t2 { get; } = default!;
    // ReSharper restore InconsistentNaming
    private IEnumerable Many { get; } = null!;

    #region One T0

        public bool TryGetAsOneT0(out T0 value) {
            if (Index == -1)
            {
                value = t0;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2>(T0 asOne) => new(asOne);

        public T0 AsOneT0 => Index == -1 ? t0 : throw new InvalidCastException();

        public bool IsOneT0 => Index == -1;
            
        public OneOrEnumerable(T0 asOne)
        {
            Index = -1;
            t0 = asOne;
        }

    #endregion
    
    #region One T1

        public bool TryGetAsOneT1(out T1 value) {
            if (Index == -2)
            {
                value = t1;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2>(T1 asOne) => new(asOne);

        public T1 AsOneT1 => Index == -2 ? t1 : throw new InvalidCastException();

        public bool IsOneT1 => Index == -2;
                
        public OneOrEnumerable(T1 asOne)
        {
            Index = -2;
            t1 = asOne;
        }

    #endregion
    
    #region One T2

        public bool TryGetAsOneT2(out T2 value) {
            if (Index == -3)
            {
                value = t2;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2>(T2 asOne) => new(asOne);

        public T2 AsOneT2 => Index == -3 ? t2 : throw new InvalidCastException();

        public bool IsOneT2 => Index == -3;
                
        public OneOrEnumerable(T2 asOne)
        {
            Index = -3;
            t2 = asOne;
        }

    #endregion
    
    #region One

        public bool TryGetAsOne(out Union<T0, T1, T2> value) {
            switch (Index)
            {
                case -1:
                    value = t0;
                    return true;
                case -2:
                    value = t1;
                    return true;
                case -3:
                    value = t2;
                    return true;
                default:
                    value = default!;
                    return false;
            }
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2>(Union<T0, T1, T2> asOne) => new(asOne);
        
        public Union<T0, T1, T2> AsOne => Index switch {
            -1 => t0,
            -2 => t1,
            -3 => t2,
            _ => throw new InvalidCastException()
        };

        public bool IsOne => Index < 0;
        
        public OneOrEnumerable(Union<T0, T1, T2> asOne)
        {
            switch (asOne)
            {
                case {IsT0: true, AsT0: var value}:
                    {
                        Index = -1;
                        t0 = value;
                    }
                    break;
                case {IsT1: true, AsT1: var value}:
                    {
                        Index = -2;
                        t1 = value;
                    }
                    break;
                case {IsT2: true, AsT2: var value}:
                    {
                        Index = -3;
                        t2 = value;
                    }
                    break;
                default:
                    throw new InvalidCastException();
            }
        }

    #endregion
    
    #region Enumerable T0

        public bool TryGetAsEnumerableT0([NotNullWhen(true)] out IEnumerable<T0>? value) {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T0> AsEnumerableT0 => Index == 1 ? (IEnumerable<T0>)Many : throw new InvalidCastException();

        public bool IsEnumerableT0 => Index == 1;
                
        public OneOrEnumerable(IEnumerable<T0> asEnumerable)
        {
            Index = 1;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T1

        public bool TryGetAsEnumerableT1([NotNullWhen(true)] out IEnumerable<T1>? value) {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T1> AsEnumerableT1 => Index == 2 ? (IEnumerable<T1>)Many : throw new InvalidCastException();

        public bool IsEnumerableT1 => Index == 2;
                    
        public OneOrEnumerable(IEnumerable<T1> asEnumerable)
        {
            Index = 2;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T2

        public bool TryGetAsEnumerableT2([NotNullWhen(true)] out IEnumerable<T2>? value) {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T2> AsEnumerableT2 => Index == 3 ? (IEnumerable<T2>)Many : throw new InvalidCastException();

        public bool IsEnumerableT2 => Index == 3;
                    
        public OneOrEnumerable(IEnumerable<T2> asEnumerable)
        {
            Index = 3;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable

        public bool TryGetAsEnumerable(out UnionEnumerable<T0, T1, T2> value) {
            if (Index is 1 or 2 or 3)
            {
                value = new((byte)Index, Many);
                return true;
            }
            value = default;
            return false;
        }
        
        public static implicit operator OneOrEnumerable<T0, T1, T2>(UnionEnumerable<T0, T1, T2> asEnumerable) => new(asEnumerable);
        
        public UnionEnumerable<T0, T1, T2> AsEnumerable => Index > 0 ? new((byte)Index, Many) : throw new InvalidCastException($"Union does not contain an Enumerable");

        public bool IsEnumerable => Index > 0;

        public OneOrEnumerable(UnionEnumerable<T0, T1, T2> asEnumerable)
        {
            Index = (sbyte)asEnumerable.Index;
            Many = (IEnumerable)asEnumerable.Value;
        }

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<Union<T0, T1, T2>, TOutput> oneCase, 
            Func<UnionEnumerable<T0, T1, T2>, TOutput> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public TOutput Match<TOutput>(
            Func<T0, TOutput> oneT0Case, Func<IEnumerable<T0>, TOutput> enumerableT0Case,
            Func<T1, TOutput> oneT1Case, Func<IEnumerable<T1>, TOutput> enumerableT1Case,
            Func<T2, TOutput> oneT2Case, Func<IEnumerable<T2>, TOutput> enumerableT2Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0),
                 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),
                 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),
                 3 => enumerableT2Case((IEnumerable<T2>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2>, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2>, Task<TOutput>> enumerableCase) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, Task<TOutput>> enumerableT0Case,
            Func<T1, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, Task<TOutput>> enumerableT1Case,
            Func<T2, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, Task<TOutput>> enumerableT2Case) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2), 3 => enumerableT2Case((IEnumerable<T2>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2>, CancellationToken, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2>, CancellationToken, Task<TOutput>> enumerableCase, 
            CancellationToken ct) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, CancellationToken, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> enumerableT0Case, 
            Func<T1, CancellationToken, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> enumerableT1Case,
            Func<T2, CancellationToken, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> enumerableT2Case,
            CancellationToken ct) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct),
                 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct),
                 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct),
                 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<Union<T0, T1, T2>> oneCase, 
            Action<UnionEnumerable<T0, T1, T2>> enumerableCase) 
        {
            switch(Index)
            {
                case < 0: oneCase(AsOne); break;
                case > 0: enumerableCase(AsEnumerable); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }
        
        public void Switch(
            Action<T0> oneT0Case, 
            Action<IEnumerable<T0>> enumerableT0Case,
            Action<T1> oneT1Case, 
            Action<IEnumerable<T1>> enumerableT1Case,
            Action<T2> oneT2Case, 
            Action<IEnumerable<T2>> enumerableT2Case) 
        {
            switch(Index)
            {
                case -1: oneT0Case(t0); break;
                case  1: enumerableT0Case((IEnumerable<T0>)Many); break;
                case -2: oneT1Case(t1); break;
                case  2: enumerableT1Case((IEnumerable<T1>)Many); break;
                case -3: oneT2Case(t2); break;
                case  3: enumerableT2Case((IEnumerable<T2>)Many); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2>, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2>, Task> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, Task> oneT0Case, Func<IEnumerable<T0>, Task> enumerableT0Case,
            Func<T1, Task> oneT1Case, Func<IEnumerable<T1>, Task> enumerableT1Case,
            Func<T2, Task> oneT2Case, Func<IEnumerable<T2>, Task> enumerableT2Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0),
                 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),
                 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),
                 3 => enumerableT2Case((IEnumerable<T2>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2>, CancellationToken, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2>, CancellationToken, Task> enumerableCase, 
            CancellationToken ct)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, CancellationToken, Task> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task> enumerableT0Case, 
            Func<T1, CancellationToken, Task> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task> enumerableT1Case,
            Func<T2, CancellationToken, Task> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task> enumerableT2Case,
            CancellationToken ct)
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct),
                 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct),
                 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct),
                 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
}

public readonly struct OneOrEnumerable<T0, T1, T2, T3>
{
    private sbyte Index { get; }
    // ReSharper disable InconsistentNaming
    private T0 t0 { get; } = default!;
    private T1 t1 { get; } = default!;
    private T2 t2 { get; } = default!;
    private T3 t3 { get; } = default!;
    // ReSharper restore InconsistentNaming
    private IEnumerable Many { get; } = null!;

    #region One T0

        public bool TryGetAsOneT0(out T0 value) {
            if (Index == -1)
            {
                value = t0;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3>(T0 asOne) => new(asOne);

        public T0 AsOneT0 => Index == -1 ? t0 : throw new InvalidCastException();

        public bool IsOneT0 => Index == -1;
            
        public OneOrEnumerable(T0 asOne)
        {
            Index = -1;
            t0 = asOne;
        }

    #endregion
    
    #region One T1

        public bool TryGetAsOneT1(out T1 value) {
            if (Index == -2)
            {
                value = t1;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3>(T1 asOne) => new(asOne);

        public T1 AsOneT1 => Index == -2 ? t1 : throw new InvalidCastException();

        public bool IsOneT1 => Index == -2;
                
        public OneOrEnumerable(T1 asOne)
        {
            Index = -2;
            t1 = asOne;
        }

    #endregion
    
    #region One T2

        public bool TryGetAsOneT2(out T2 value) {
            if (Index == -3)
            {
                value = t2;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3>(T2 asOne) => new(asOne);

        public T2 AsOneT2 => Index == -3 ? t2 : throw new InvalidCastException();

        public bool IsOneT2 => Index == -3;
                
        public OneOrEnumerable(T2 asOne)
        {
            Index = -3;
            t2 = asOne;
        }

    #endregion
    
    #region One T3

        public bool TryGetAsOneT3(out T3 value) {
            if (Index == -4)
            {
                value = t3;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3>(T3 asOne) => new(asOne);

        public T3 AsOneT3 => Index == -4 ? t3 : throw new InvalidCastException();

        public bool IsOneT3 => Index == -4;
                
        public OneOrEnumerable(T3 asOne)
        {
            Index = -4;
            t3 = asOne;
        }

    #endregion
    
    #region One

        public bool TryGetAsOne(out Union<T0, T1, T2, T3> value) {
            switch (Index)
            {
                case -1:
                    value = t0;
                    return true;
                case -2:
                    value = t1;
                    return true;
                case -3:
                    value = t2;
                    return true;
                case -4:
                    value = t3;
                    return true;
                default:
                    value = default!;
                    return false;
            }
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3>(Union<T0, T1, T2, T3> asOne) => new(asOne);
        
        public Union<T0, T1, T2, T3> AsOne => Index switch {
            -1 => t0,
            -2 => t1,
            -3 => t2,
            -4 => t3,
            _ => throw new InvalidCastException()
        };

        public bool IsOne => Index < 0;
        
        public OneOrEnumerable(Union<T0, T1, T2, T3> asOne)
        {
            switch (asOne)
            {
                case {IsT0: true, AsT0: var value}:
                    {
                        Index = -1;
                        t0 = value;
                    }
                    break;
                case {IsT1: true, AsT1: var value}:
                    {
                        Index = -2;
                        t1 = value;
                    }
                    break;
                case {IsT2: true, AsT2: var value}:
                    {
                        Index = -3;
                        t2 = value;
                    }
                    break;
                case {IsT3: true, AsT3: var value}:
                    {
                        Index = -4;
                        t3 = value;
                    }
                    break;
                default:
                    throw new InvalidCastException();
            }
        }

    #endregion
    
    #region Enumerable T0

        public bool TryGetAsEnumerableT0([NotNullWhen(true)] out IEnumerable<T0>? value) {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T0> AsEnumerableT0 => Index == 1 ? (IEnumerable<T0>)Many : throw new InvalidCastException();

        public bool IsEnumerableT0 => Index == 1;
                
        public OneOrEnumerable(IEnumerable<T0> asEnumerable)
        {
            Index = 1;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T1

        public bool TryGetAsEnumerableT1([NotNullWhen(true)] out IEnumerable<T1>? value) {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T1> AsEnumerableT1 => Index == 2 ? (IEnumerable<T1>)Many : throw new InvalidCastException();

        public bool IsEnumerableT1 => Index == 2;
                    
        public OneOrEnumerable(IEnumerable<T1> asEnumerable)
        {
            Index = 2;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T2

        public bool TryGetAsEnumerableT2([NotNullWhen(true)] out IEnumerable<T2>? value) {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T2> AsEnumerableT2 => Index == 3 ? (IEnumerable<T2>)Many : throw new InvalidCastException();

        public bool IsEnumerableT2 => Index == 3;
                    
        public OneOrEnumerable(IEnumerable<T2> asEnumerable)
        {
            Index = 3;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T3

        public bool TryGetAsEnumerableT3([NotNullWhen(true)] out IEnumerable<T3>? value) {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T3> AsEnumerableT3 => Index == 4 ? (IEnumerable<T3>)Many : throw new InvalidCastException();

        public bool IsEnumerableT3 => Index == 4;
                    
        public OneOrEnumerable(IEnumerable<T3> asEnumerable)
        {
            Index = 4;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable

        public bool TryGetAsEnumerable(out UnionEnumerable<T0, T1, T2, T3> value) {
            if (Index is 1 or 2 or 3 or 4)
            {
                value = new((byte)Index, Many);
                return true;
            }
            value = default;
            return false;
        }
        
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3>(UnionEnumerable<T0, T1, T2, T3> asEnumerable) => new(asEnumerable);
        
        public UnionEnumerable<T0, T1, T2, T3> AsEnumerable => Index > 0 ? new((byte)Index, Many) : throw new InvalidCastException($"Union does not contain an Enumerable");

        public bool IsEnumerable => Index > 0;

        public OneOrEnumerable(UnionEnumerable<T0, T1, T2, T3> asEnumerable)
        {
            Index = (sbyte)asEnumerable.Index;
            Many = (IEnumerable)asEnumerable.Value;
        }

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<Union<T0, T1, T2, T3>, TOutput> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3>, TOutput> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public TOutput Match<TOutput>(
            Func<T0, TOutput> oneT0Case, Func<IEnumerable<T0>, TOutput> enumerableT0Case,
            Func<T1, TOutput> oneT1Case, Func<IEnumerable<T1>, TOutput> enumerableT1Case,
            Func<T2, TOutput> oneT2Case, Func<IEnumerable<T2>, TOutput> enumerableT2Case,
            Func<T3, TOutput> oneT3Case, Func<IEnumerable<T3>, TOutput> enumerableT3Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0),
                 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),
                 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),
                 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),
                 4 => enumerableT3Case((IEnumerable<T3>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3>, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3>, Task<TOutput>> enumerableCase) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, Task<TOutput>> enumerableT0Case,
            Func<T1, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, Task<TOutput>> enumerableT1Case,
            Func<T2, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, Task<TOutput>> enumerableT2Case,
            Func<T3, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, Task<TOutput>> enumerableT3Case) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0),
                 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),
                 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),
                 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),
                 4 => enumerableT3Case((IEnumerable<T3>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3>, CancellationToken, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3>, CancellationToken, Task<TOutput>> enumerableCase, 
            CancellationToken ct) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, CancellationToken, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> enumerableT0Case, 
            Func<T1, CancellationToken, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> enumerableT1Case,
            Func<T2, CancellationToken, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> enumerableT2Case,
            Func<T3, CancellationToken, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> enumerableT3Case,
            CancellationToken ct) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct),
                 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct),
                 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct),
                 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct),
                 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<Union<T0, T1, T2, T3>> oneCase, 
            Action<UnionEnumerable<T0, T1, T2, T3>> enumerableCase) 
        {
            switch(Index)
            {
                case < 0: oneCase(AsOne); break;
                case > 0: enumerableCase(AsEnumerable); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }
        
        public void Switch(
            Action<T0> oneT0Case, 
            Action<IEnumerable<T0>> enumerableT0Case,
            Action<T1> oneT1Case, 
            Action<IEnumerable<T1>> enumerableT1Case,
            Action<T2> oneT2Case, 
            Action<IEnumerable<T2>> enumerableT2Case,
            Action<T3> oneT3Case, 
            Action<IEnumerable<T3>> enumerableT3Case) 
        {
            switch(Index)
            {
                case -1: oneT0Case(t0); break;
                case  1: enumerableT0Case((IEnumerable<T0>)Many); break;
                case -2: oneT1Case(t1); break;
                case  2: enumerableT1Case((IEnumerable<T1>)Many); break;
                case -3: oneT2Case(t2); break;
                case  3: enumerableT2Case((IEnumerable<T2>)Many); break;
                case -4: oneT3Case(t3); break;
                case  4: enumerableT3Case((IEnumerable<T3>)Many); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3>, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3>, Task> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, Task> oneT0Case, Func<IEnumerable<T0>, Task> enumerableT0Case,
            Func<T1, Task> oneT1Case, Func<IEnumerable<T1>, Task> enumerableT1Case,
            Func<T2, Task> oneT2Case, Func<IEnumerable<T2>, Task> enumerableT2Case,
            Func<T3, Task> oneT3Case, Func<IEnumerable<T3>, Task> enumerableT3Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0),
                 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),
                 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),
                 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),
                 4 => enumerableT3Case((IEnumerable<T3>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3>, CancellationToken, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3>, CancellationToken, Task> enumerableCase, 
            CancellationToken ct)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, CancellationToken, Task> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task> enumerableT0Case, 
            Func<T1, CancellationToken, Task> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task> enumerableT1Case,
            Func<T2, CancellationToken, Task> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task> enumerableT2Case,
            Func<T3, CancellationToken, Task> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task> enumerableT3Case,
            CancellationToken ct)
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct),
                 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct),
                 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct),
                 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct),
                 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
}

public readonly struct OneOrEnumerable<T0, T1, T2, T3, T4>
{
    private sbyte Index { get; }
    // ReSharper disable InconsistentNaming
    private T0 t0 { get; } = default!;
    private T1 t1 { get; } = default!;
    private T2 t2 { get; } = default!;
    private T3 t3 { get; } = default!;
    private T4 t4 { get; } = default!;
    // ReSharper restore InconsistentNaming
    private IEnumerable Many { get; } = null!;

    #region One T0

        public bool TryGetAsOneT0(out T0 value) {
            if (Index == -1)
            {
                value = t0;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4>(T0 asOne) => new(asOne);

        public T0 AsOneT0 => Index == -1 ? t0 : throw new InvalidCastException();

        public bool IsOneT0 => Index == -1;
            
        public OneOrEnumerable(T0 asOne)
        {
            Index = -1;
            t0 = asOne;
        }

    #endregion
    
    #region One T1

        public bool TryGetAsOneT1(out T1 value) {
            if (Index == -2)
            {
                value = t1;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4>(T1 asOne) => new(asOne);

        public T1 AsOneT1 => Index == -2 ? t1 : throw new InvalidCastException();

        public bool IsOneT1 => Index == -2;
                
        public OneOrEnumerable(T1 asOne)
        {
            Index = -2;
            t1 = asOne;
        }

    #endregion
    
    #region One T2

        public bool TryGetAsOneT2(out T2 value) {
            if (Index == -3)
            {
                value = t2;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4>(T2 asOne) => new(asOne);

        public T2 AsOneT2 => Index == -3 ? t2 : throw new InvalidCastException();

        public bool IsOneT2 => Index == -3;
                
        public OneOrEnumerable(T2 asOne)
        {
            Index = -3;
            t2 = asOne;
        }

    #endregion
    
    #region One T3

        public bool TryGetAsOneT3(out T3 value) {
            if (Index == -4)
            {
                value = t3;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4>(T3 asOne) => new(asOne);

        public T3 AsOneT3 => Index == -4 ? t3 : throw new InvalidCastException();

        public bool IsOneT3 => Index == -4;
                
        public OneOrEnumerable(T3 asOne)
        {
            Index = -4;
            t3 = asOne;
        }

    #endregion
    
    #region One T4

        public bool TryGetAsOneT4(out T4 value) {
            if (Index == -5)
            {
                value = t4;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4>(T4 asOne) => new(asOne);

        public T4 AsOneT4 => Index == -5 ? t4 : throw new InvalidCastException();

        public bool IsOneT4 => Index == -5;
                
        public OneOrEnumerable(T4 asOne)
        {
            Index = -5;
            t4 = asOne;
        }

    #endregion
    
    #region One

        public bool TryGetAsOne(out Union<T0, T1, T2, T3, T4> value) {
            switch (Index)
            {
                case -1:
                    value = t0;
                    return true;
                case -2:
                    value = t1;
                    return true;
                case -3:
                    value = t2;
                    return true;
                case -4:
                    value = t3;
                    return true;
                case -5:
                    value = t4;
                    return true;
                default:
                    value = default!;
                    return false;
            }
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4>(Union<T0, T1, T2, T3, T4> asOne) => new(asOne);
        
        public Union<T0, T1, T2, T3, T4> AsOne => Index switch {
            -1 => t0,
            -2 => t1,
            -3 => t2,
            -4 => t3,
            -5 => t4,
            _ => throw new InvalidCastException()
        };

        public bool IsOne => Index < 0;
        
        public OneOrEnumerable(Union<T0, T1, T2, T3, T4> asOne)
        {
            switch (asOne)
            {
                case {IsT0: true, AsT0: var value}:
                    {
                        Index = -1;
                        t0 = value;
                    }
                    break;
                case {IsT1: true, AsT1: var value}:
                    {
                        Index = -2;
                        t1 = value;
                    }
                    break;
                case {IsT2: true, AsT2: var value}:
                    {
                        Index = -3;
                        t2 = value;
                    }
                    break;
                case {IsT3: true, AsT3: var value}:
                    {
                        Index = -4;
                        t3 = value;
                    }
                    break;
                case {IsT4: true, AsT4: var value}:
                    {
                        Index = -5;
                        t4 = value;
                    }
                    break;
                default:
                    throw new InvalidCastException();
            }
        }

    #endregion
    
    #region Enumerable T0

        public bool TryGetAsEnumerableT0([NotNullWhen(true)] out IEnumerable<T0>? value) {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T0> AsEnumerableT0 => Index == 1 ? (IEnumerable<T0>)Many : throw new InvalidCastException();

        public bool IsEnumerableT0 => Index == 1;
                
        public OneOrEnumerable(IEnumerable<T0> asEnumerable)
        {
            Index = 1;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T1

        public bool TryGetAsEnumerableT1([NotNullWhen(true)] out IEnumerable<T1>? value) {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T1> AsEnumerableT1 => Index == 2 ? (IEnumerable<T1>)Many : throw new InvalidCastException();

        public bool IsEnumerableT1 => Index == 2;
                    
        public OneOrEnumerable(IEnumerable<T1> asEnumerable)
        {
            Index = 2;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T2

        public bool TryGetAsEnumerableT2([NotNullWhen(true)] out IEnumerable<T2>? value) {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T2> AsEnumerableT2 => Index == 3 ? (IEnumerable<T2>)Many : throw new InvalidCastException();

        public bool IsEnumerableT2 => Index == 3;
                    
        public OneOrEnumerable(IEnumerable<T2> asEnumerable)
        {
            Index = 3;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T3

        public bool TryGetAsEnumerableT3([NotNullWhen(true)] out IEnumerable<T3>? value) {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T3> AsEnumerableT3 => Index == 4 ? (IEnumerable<T3>)Many : throw new InvalidCastException();

        public bool IsEnumerableT3 => Index == 4;
                    
        public OneOrEnumerable(IEnumerable<T3> asEnumerable)
        {
            Index = 4;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T4

        public bool TryGetAsEnumerableT4([NotNullWhen(true)] out IEnumerable<T4>? value) {
            if (Index == 5)
            {
                value = (IEnumerable<T4>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T4> AsEnumerableT4 => Index == 5 ? (IEnumerable<T4>)Many : throw new InvalidCastException();

        public bool IsEnumerableT4 => Index == 5;
                    
        public OneOrEnumerable(IEnumerable<T4> asEnumerable)
        {
            Index = 5;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable

        public bool TryGetAsEnumerable(out UnionEnumerable<T0, T1, T2, T3, T4> value) {
            if (Index is 1 or 2 or 3 or 4 or 5)
            {
                value = new((byte)Index, Many);
                return true;
            }
            value = default;
            return false;
        }
        
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4>(UnionEnumerable<T0, T1, T2, T3, T4> asEnumerable) => new(asEnumerable);
        
        public UnionEnumerable<T0, T1, T2, T3, T4> AsEnumerable => Index > 0 ? new((byte)Index, Many) : throw new InvalidCastException($"Union does not contain an Enumerable");

        public bool IsEnumerable => Index > 0;

        public OneOrEnumerable(UnionEnumerable<T0, T1, T2, T3, T4> asEnumerable)
        {
            Index = (sbyte)asEnumerable.Index;
            Many = (IEnumerable)asEnumerable.Value;
        }

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<Union<T0, T1, T2, T3, T4>, TOutput> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4>, TOutput> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public TOutput Match<TOutput>(
            Func<T0, TOutput> oneT0Case, Func<IEnumerable<T0>, TOutput> enumerableT0Case,
            Func<T1, TOutput> oneT1Case, Func<IEnumerable<T1>, TOutput> enumerableT1Case,
            Func<T2, TOutput> oneT2Case, Func<IEnumerable<T2>, TOutput> enumerableT2Case,
            Func<T3, TOutput> oneT3Case, Func<IEnumerable<T3>, TOutput> enumerableT3Case,
            Func<T4, TOutput> oneT4Case, Func<IEnumerable<T4>, TOutput> enumerableT4Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2), 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3), 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4), 5 => enumerableT4Case((IEnumerable<T4>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4>, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4>, Task<TOutput>> enumerableCase) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, Task<TOutput>> enumerableT0Case,
            Func<T1, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, Task<TOutput>> enumerableT1Case,
            Func<T2, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, Task<TOutput>> enumerableT2Case,
            Func<T3, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, Task<TOutput>> enumerableT3Case,
            Func<T4, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, Task<TOutput>> enumerableT4Case) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2), 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3), 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4), 5 => enumerableT4Case((IEnumerable<T4>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4>, CancellationToken, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4>, CancellationToken, Task<TOutput>> enumerableCase, 
            CancellationToken ct) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, CancellationToken, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> enumerableT0Case, 
            Func<T1, CancellationToken, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> enumerableT1Case,
            Func<T2, CancellationToken, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> enumerableT2Case,
            Func<T3, CancellationToken, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> enumerableT3Case,
            Func<T4, CancellationToken, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> enumerableT4Case,
            CancellationToken ct) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct), 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct), 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct), 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct), 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct), 5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<Union<T0, T1, T2, T3, T4>> oneCase, 
            Action<UnionEnumerable<T0, T1, T2, T3, T4>> enumerableCase) 
        {
            switch(Index)
            {
                case < 0: oneCase(AsOne); break;
                case > 0: enumerableCase(AsEnumerable); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }
        
        public void Switch(
            Action<T0> oneT0Case, Action<IEnumerable<T0>> enumerableT0Case,
            Action<T1> oneT1Case, Action<IEnumerable<T1>> enumerableT1Case,
            Action<T2> oneT2Case, Action<IEnumerable<T2>> enumerableT2Case,
            Action<T3> oneT3Case, Action<IEnumerable<T3>> enumerableT3Case,
            Action<T4> oneT4Case, Action<IEnumerable<T4>> enumerableT4Case) 
        {
            switch(Index)
            {
                case -1: oneT0Case(t0); break;
                case  1: enumerableT0Case((IEnumerable<T0>)Many); break;
                case -2: oneT1Case(t1); break;
                case  2: enumerableT1Case((IEnumerable<T1>)Many); break;
                case -3: oneT2Case(t2); break;
                case  3: enumerableT2Case((IEnumerable<T2>)Many); break;
                case -4: oneT3Case(t3); break;
                case  4: enumerableT3Case((IEnumerable<T3>)Many); break;
                case -5: oneT4Case(t4); break;
                case  5: enumerableT4Case((IEnumerable<T4>)Many); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4>, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4>, Task> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, Task> oneT0Case, Func<IEnumerable<T0>, Task> enumerableT0Case,
            Func<T1, Task> oneT1Case, Func<IEnumerable<T1>, Task> enumerableT1Case,
            Func<T2, Task> oneT2Case, Func<IEnumerable<T2>, Task> enumerableT2Case,
            Func<T3, Task> oneT3Case, Func<IEnumerable<T3>, Task> enumerableT3Case,
            Func<T4, Task> oneT4Case, Func<IEnumerable<T4>, Task> enumerableT4Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2), 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3), 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4), 5 => enumerableT4Case((IEnumerable<T4>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4>, CancellationToken, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4>, CancellationToken, Task> enumerableCase, 
            CancellationToken ct)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, CancellationToken, Task> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task> enumerableT0Case, 
            Func<T1, CancellationToken, Task> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task> enumerableT1Case,
            Func<T2, CancellationToken, Task> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task> enumerableT2Case,
            Func<T3, CancellationToken, Task> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task> enumerableT3Case,
            Func<T4, CancellationToken, Task> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task> enumerableT4Case,
            CancellationToken ct)
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct), 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct), 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct), 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct), 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct), 5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
}

public readonly struct OneOrEnumerable<T0, T1, T2, T3, T4, T5>
{
    private sbyte Index { get; }
    // ReSharper disable InconsistentNaming
    private T0 t0 { get; } = default!;
    private T1 t1 { get; } = default!;
    private T2 t2 { get; } = default!;
    private T3 t3 { get; } = default!;
    private T4 t4 { get; } = default!;
    private T5 t5 { get; } = default!;
    // ReSharper restore InconsistentNaming
    private IEnumerable Many { get; } = null!;

    #region One T0

        public bool TryGetAsOneT0(out T0 value) {
            if (Index == -1)
            {
                value = t0;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5>(T0 asOne) => new(asOne);

        public T0 AsOneT0 => Index == -1 ? t0 : throw new InvalidCastException();

        public bool IsOneT0 => Index == -1;
            
        public OneOrEnumerable(T0 asOne)
        {
            Index = -1;
            t0 = asOne;
        }

    #endregion
    
    #region One T1

        public bool TryGetAsOneT1(out T1 value) {
            if (Index == -2)
            {
                value = t1;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5>(T1 asOne) => new(asOne);

        public T1 AsOneT1 => Index == -2 ? t1 : throw new InvalidCastException();

        public bool IsOneT1 => Index == -2;
                
        public OneOrEnumerable(T1 asOne)
        {
            Index = -2;
            t1 = asOne;
        }

    #endregion
    
    #region One T2

        public bool TryGetAsOneT2(out T2 value) {
            if (Index == -3)
            {
                value = t2;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5>(T2 asOne) => new(asOne);

        public T2 AsOneT2 => Index == -3 ? t2 : throw new InvalidCastException();

        public bool IsOneT2 => Index == -3;
                
        public OneOrEnumerable(T2 asOne)
        {
            Index = -3;
            t2 = asOne;
        }

    #endregion
    
    #region One T3

        public bool TryGetAsOneT3(out T3 value) {
            if (Index == -4)
            {
                value = t3;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5>(T3 asOne) => new(asOne);

        public T3 AsOneT3 => Index == -4 ? t3 : throw new InvalidCastException();

        public bool IsOneT3 => Index == -4;
                
        public OneOrEnumerable(T3 asOne)
        {
            Index = -4;
            t3 = asOne;
        }

    #endregion
    
    #region One T4

        public bool TryGetAsOneT4(out T4 value) {
            if (Index == -5)
            {
                value = t4;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5>(T4 asOne) => new(asOne);

        public T4 AsOneT4 => Index == -5 ? t4 : throw new InvalidCastException();

        public bool IsOneT4 => Index == -5;
                
        public OneOrEnumerable(T4 asOne)
        {
            Index = -5;
            t4 = asOne;
        }

    #endregion
    
    #region One T5

        public bool TryGetAsOneT5(out T5 value) {
            if (Index == -6)
            {
                value = t5;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5>(T5 asOne) => new(asOne);

        public T5 AsOneT5 => Index == -6 ? t5 : throw new InvalidCastException();

        public bool IsOneT5 => Index == -6;
                
        public OneOrEnumerable(T5 asOne)
        {
            Index = -6;
            t5 = asOne;
        }

    #endregion
    
    #region One

        public bool TryGetAsOne(out Union<T0, T1, T2, T3, T4, T5> value) {
            switch (Index)
            {
                case -1:
                    value = t0;
                    return true;
                case -2:
                    value = t1;
                    return true;
                case -3:
                    value = t2;
                    return true;
                case -4:
                    value = t3;
                    return true;
                case -5:
                    value = t4;
                    return true;
                case -6:
                    value = t5;
                    return true;
                default:
                    value = default!;
                    return false;
            }
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5>(Union<T0, T1, T2, T3, T4, T5> asOne) => new(asOne);
        
        public Union<T0, T1, T2, T3, T4, T5> AsOne => Index switch {
            -1 => t0,
            -2 => t1,
            -3 => t2,
            -4 => t3,
            -5 => t4,
            -6 => t5,
            _ => throw new InvalidCastException()
        };

        public bool IsOne => Index < 0;
        
        public OneOrEnumerable(Union<T0, T1, T2, T3, T4, T5> asOne)
        {
            switch (asOne)
            {
                case {IsT0: true, AsT0: var value}:
                    {
                        Index = -1;
                        t0 = value;
                    }
                    break;
                case {IsT1: true, AsT1: var value}:
                    {
                        Index = -2;
                        t1 = value;
                    }
                    break;
                case {IsT2: true, AsT2: var value}:
                    {
                        Index = -3;
                        t2 = value;
                    }
                    break;
                case {IsT3: true, AsT3: var value}:
                    {
                        Index = -4;
                        t3 = value;
                    }
                    break;
                case {IsT4: true, AsT4: var value}:
                    {
                        Index = -5;
                        t4 = value;
                    }
                    break;
                case {IsT5: true, AsT5: var value}:
                    {
                        Index = -6;
                        t5 = value;
                    }
                    break;
                default:
                    throw new InvalidCastException();
            }
        }

    #endregion
    
    #region Enumerable T0

        public bool TryGetAsEnumerableT0([NotNullWhen(true)] out IEnumerable<T0>? value) {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T0> AsEnumerableT0 => Index == 1 ? (IEnumerable<T0>)Many : throw new InvalidCastException();

        public bool IsEnumerableT0 => Index == 1;
                
        public OneOrEnumerable(IEnumerable<T0> asEnumerable)
        {
            Index = 1;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T1

        public bool TryGetAsEnumerableT1([NotNullWhen(true)] out IEnumerable<T1>? value) {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T1> AsEnumerableT1 => Index == 2 ? (IEnumerable<T1>)Many : throw new InvalidCastException();

        public bool IsEnumerableT1 => Index == 2;
                    
        public OneOrEnumerable(IEnumerable<T1> asEnumerable)
        {
            Index = 2;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T2

        public bool TryGetAsEnumerableT2([NotNullWhen(true)] out IEnumerable<T2>? value) {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T2> AsEnumerableT2 => Index == 3 ? (IEnumerable<T2>)Many : throw new InvalidCastException();

        public bool IsEnumerableT2 => Index == 3;
                    
        public OneOrEnumerable(IEnumerable<T2> asEnumerable)
        {
            Index = 3;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T3

        public bool TryGetAsEnumerableT3([NotNullWhen(true)] out IEnumerable<T3>? value) {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T3> AsEnumerableT3 => Index == 4 ? (IEnumerable<T3>)Many : throw new InvalidCastException();

        public bool IsEnumerableT3 => Index == 4;
                    
        public OneOrEnumerable(IEnumerable<T3> asEnumerable)
        {
            Index = 4;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T4

        public bool TryGetAsEnumerableT4([NotNullWhen(true)] out IEnumerable<T4>? value) {
            if (Index == 5)
            {
                value = (IEnumerable<T4>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T4> AsEnumerableT4 => Index == 5 ? (IEnumerable<T4>)Many : throw new InvalidCastException();

        public bool IsEnumerableT4 => Index == 5;
                    
        public OneOrEnumerable(IEnumerable<T4> asEnumerable)
        {
            Index = 5;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T5

        public bool TryGetAsEnumerableT5([NotNullWhen(true)] out IEnumerable<T5>? value) {
            if (Index == 6)
            {
                value = (IEnumerable<T5>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T5> AsEnumerableT5 => Index == 6 ? (IEnumerable<T5>)Many : throw new InvalidCastException();

        public bool IsEnumerableT5 => Index == 6;
                    
        public OneOrEnumerable(IEnumerable<T5> asEnumerable)
        {
            Index = 6;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable

        public bool TryGetAsEnumerable(out UnionEnumerable<T0, T1, T2, T3, T4, T5> value) {
            if (Index is 1 or 2 or 3 or 4 or 5 or 6)
            {
                value = new((byte)Index, Many);
                return true;
            }
            value = default;
            return false;
        }
        
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5>(UnionEnumerable<T0, T1, T2, T3, T4, T5> asEnumerable) => new(asEnumerable);
        
        public UnionEnumerable<T0, T1, T2, T3, T4, T5> AsEnumerable => Index > 0 ? new((byte)Index, Many) : throw new InvalidCastException($"Union does not contain an Enumerable");

        public bool IsEnumerable => Index > 0;

        public OneOrEnumerable(UnionEnumerable<T0, T1, T2, T3, T4, T5> asEnumerable)
        {
            Index = (sbyte)asEnumerable.Index;
            Many = (IEnumerable)asEnumerable.Value;
        }

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5>, TOutput> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5>, TOutput> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public TOutput Match<TOutput>(
            Func<T0, TOutput> oneT0Case, Func<IEnumerable<T0>, TOutput> enumerableT0Case,
            Func<T1, TOutput> oneT1Case, Func<IEnumerable<T1>, TOutput> enumerableT1Case,
            Func<T2, TOutput> oneT2Case, Func<IEnumerable<T2>, TOutput> enumerableT2Case,
            Func<T3, TOutput> oneT3Case, Func<IEnumerable<T3>, TOutput> enumerableT3Case,
            Func<T4, TOutput> oneT4Case, Func<IEnumerable<T4>, TOutput> enumerableT4Case,
            Func<T5, TOutput> oneT5Case, Func<IEnumerable<T5>, TOutput> enumerableT5Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0),
                 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),
                 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),
                 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),
                 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4),
                 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5),
                 6 => enumerableT5Case((IEnumerable<T5>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5>, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5>, Task<TOutput>> enumerableCase) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, Task<TOutput>> enumerableT0Case,
            Func<T1, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, Task<TOutput>> enumerableT1Case,
            Func<T2, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, Task<TOutput>> enumerableT2Case,
            Func<T3, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, Task<TOutput>> enumerableT3Case,
            Func<T4, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, Task<TOutput>> enumerableT4Case,
            Func<T5, Task<TOutput>> oneT5Case, Func<IEnumerable<T5>, Task<TOutput>> enumerableT5Case) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0),
                 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),
                 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),
                 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),
                 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4),
                 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5),
                 6 => enumerableT5Case((IEnumerable<T5>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5>, CancellationToken, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5>, CancellationToken, Task<TOutput>> enumerableCase, 
            CancellationToken ct) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, CancellationToken, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> enumerableT0Case, 
            Func<T1, CancellationToken, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> enumerableT1Case,
            Func<T2, CancellationToken, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> enumerableT2Case,
            Func<T3, CancellationToken, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> enumerableT3Case,
            Func<T4, CancellationToken, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> enumerableT4Case,
            Func<T5, CancellationToken, Task<TOutput>> oneT5Case, Func<IEnumerable<T5>, CancellationToken, Task<TOutput>> enumerableT5Case,
            CancellationToken ct) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct),
                 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct),
                 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct),
                 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct),
                 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct),
                 5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                -6 => oneT5Case(t5, ct),
                 6 => enumerableT5Case((IEnumerable<T5>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<Union<T0, T1, T2, T3, T4, T5>> oneCase, 
            Action<UnionEnumerable<T0, T1, T2, T3, T4, T5>> enumerableCase) 
        {
            switch(Index)
            {
                case < 0: oneCase(AsOne); break;
                case > 0: enumerableCase(AsEnumerable); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }
        
        public void Switch(
            Action<T0> oneT0Case, 
            Action<IEnumerable<T0>> enumerableT0Case,
            Action<T1> oneT1Case, 
            Action<IEnumerable<T1>> enumerableT1Case,
            Action<T2> oneT2Case, 
            Action<IEnumerable<T2>> enumerableT2Case,
            Action<T3> oneT3Case, 
            Action<IEnumerable<T3>> enumerableT3Case,
            Action<T4> oneT4Case, 
            Action<IEnumerable<T4>> enumerableT4Case,
            Action<T5> oneT5Case, 
            Action<IEnumerable<T5>> enumerableT5Case) 
        {
            switch(Index)
            {
                case -1: oneT0Case(t0); break;
                case  1: enumerableT0Case((IEnumerable<T0>)Many); break;
                case -2: oneT1Case(t1); break;
                case  2: enumerableT1Case((IEnumerable<T1>)Many); break;
                case -3: oneT2Case(t2); break;
                case  3: enumerableT2Case((IEnumerable<T2>)Many); break;
                case -4: oneT3Case(t3); break;
                case  4: enumerableT3Case((IEnumerable<T3>)Many); break;
                case -5: oneT4Case(t4); break;
                case  5: enumerableT4Case((IEnumerable<T4>)Many); break;
                case -6: oneT5Case(t5); break;
                case  6: enumerableT5Case((IEnumerable<T5>)Many); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4, T5>, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5>, Task> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, Task> oneT0Case, Func<IEnumerable<T0>, Task> enumerableT0Case,
            Func<T1, Task> oneT1Case, Func<IEnumerable<T1>, Task> enumerableT1Case,
            Func<T2, Task> oneT2Case, Func<IEnumerable<T2>, Task> enumerableT2Case,
            Func<T3, Task> oneT3Case, Func<IEnumerable<T3>, Task> enumerableT3Case,
            Func<T4, Task> oneT4Case, Func<IEnumerable<T4>, Task> enumerableT4Case,
            Func<T5, Task> oneT5Case, Func<IEnumerable<T5>, Task> enumerableT5Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0),
                 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),
                 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),
                 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),
                 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4),
                 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5),
                 6 => enumerableT5Case((IEnumerable<T5>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4, T5>, CancellationToken, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5>, CancellationToken, Task> enumerableCase, 
            CancellationToken ct)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, CancellationToken, Task> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task> enumerableT0Case, 
            Func<T1, CancellationToken, Task> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task> enumerableT1Case,
            Func<T2, CancellationToken, Task> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task> enumerableT2Case,
            Func<T3, CancellationToken, Task> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task> enumerableT3Case,
            Func<T4, CancellationToken, Task> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task> enumerableT4Case,
            Func<T5, CancellationToken, Task> oneT5Case, Func<IEnumerable<T5>, CancellationToken, Task> enumerableT5Case,
            CancellationToken ct)
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct),
                 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct),
                 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct),
                 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct),
                 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct),
                 5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                -6 => oneT5Case(t5, ct),
                 6 => enumerableT5Case((IEnumerable<T5>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
}

public readonly struct OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6>
{
    private sbyte Index { get; }
    // ReSharper disable InconsistentNaming
    private T0 t0 { get; } = default!;
    private T1 t1 { get; } = default!;
    private T2 t2 { get; } = default!;
    private T3 t3 { get; } = default!;
    private T4 t4 { get; } = default!;
    private T5 t5 { get; } = default!;
    private T6 t6 { get; } = default!;
    // ReSharper restore InconsistentNaming
    private IEnumerable Many { get; } = null!;

    #region One T0

        public bool TryGetAsOneT0(out T0 value) {
            if (Index == -1)
            {
                value = t0;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6>(T0 asOne) => new(asOne);

        public T0 AsOneT0 => Index == -1 ? t0 : throw new InvalidCastException();

        public bool IsOneT0 => Index == -1;
            
        public OneOrEnumerable(T0 asOne)
        {
            Index = -1;
            t0 = asOne;
        }

    #endregion
    
    #region One T1

        public bool TryGetAsOneT1(out T1 value) {
            if (Index == -2)
            {
                value = t1;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6>(T1 asOne) => new(asOne);

        public T1 AsOneT1 => Index == -2 ? t1 : throw new InvalidCastException();

        public bool IsOneT1 => Index == -2;
                
        public OneOrEnumerable(T1 asOne)
        {
            Index = -2;
            t1 = asOne;
        }

    #endregion
    
    #region One T2

        public bool TryGetAsOneT2(out T2 value) {
            if (Index == -3)
            {
                value = t2;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6>(T2 asOne) => new(asOne);

        public T2 AsOneT2 => Index == -3 ? t2 : throw new InvalidCastException();

        public bool IsOneT2 => Index == -3;
                
        public OneOrEnumerable(T2 asOne)
        {
            Index = -3;
            t2 = asOne;
        }

    #endregion
    
    #region One T3

        public bool TryGetAsOneT3(out T3 value) {
            if (Index == -4)
            {
                value = t3;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6>(T3 asOne) => new(asOne);

        public T3 AsOneT3 => Index == -4 ? t3 : throw new InvalidCastException();

        public bool IsOneT3 => Index == -4;
                
        public OneOrEnumerable(T3 asOne)
        {
            Index = -4;
            t3 = asOne;
        }

    #endregion
    
    #region One T4

        public bool TryGetAsOneT4(out T4 value) {
            if (Index == -5)
            {
                value = t4;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6>(T4 asOne) => new(asOne);

        public T4 AsOneT4 => Index == -5 ? t4 : throw new InvalidCastException();

        public bool IsOneT4 => Index == -5;
                
        public OneOrEnumerable(T4 asOne)
        {
            Index = -5;
            t4 = asOne;
        }

    #endregion
    
    #region One T5

        public bool TryGetAsOneT5(out T5 value) {
            if (Index == -6)
            {
                value = t5;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6>(T5 asOne) => new(asOne);

        public T5 AsOneT5 => Index == -6 ? t5 : throw new InvalidCastException();

        public bool IsOneT5 => Index == -6;
                
        public OneOrEnumerable(T5 asOne)
        {
            Index = -6;
            t5 = asOne;
        }

    #endregion
    
    #region One T6

        public bool TryGetAsOneT6(out T6 value) {
            if (Index == -7)
            {
                value = t6;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6>(T6 asOne) => new(asOne);

        public T6 AsOneT6 => Index == -7 ? t6 : throw new InvalidCastException();

        public bool IsOneT6 => Index == -7;
                
        public OneOrEnumerable(T6 asOne)
        {
            Index = -7;
            t6 = asOne;
        }

    #endregion
    
    #region One

        public bool TryGetAsOne(out Union<T0, T1, T2, T3, T4, T5, T6> value) {
            switch (Index)
            {
                case -1:
                    value = t0;
                    return true;
                case -2:
                    value = t1;
                    return true;
                case -3:
                    value = t2;
                    return true;
                case -4:
                    value = t3;
                    return true;
                case -5:
                    value = t4;
                    return true;
                case -6:
                    value = t5;
                    return true;
                case -7:
                    value = t6;
                    return true;
                default:
                    value = default!;
                    return false;
            }
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6>(Union<T0, T1, T2, T3, T4, T5, T6> asOne) => new(asOne);
        
        public Union<T0, T1, T2, T3, T4, T5, T6> AsOne => Index switch {
            -1 => t0,
            -2 => t1,
            -3 => t2,
            -4 => t3,
            -5 => t4,
            -6 => t5,
            -7 => t6,
            _ => throw new InvalidCastException()
        };

        public bool IsOne => Index < 0;
        
        public OneOrEnumerable(Union<T0, T1, T2, T3, T4, T5, T6> asOne)
        {
            switch (asOne)
            {
                case {IsT0: true, AsT0: var value}:
                    {
                        Index = -1;
                        t0 = value;
                    }
                    break;
                case {IsT1: true, AsT1: var value}:
                    {
                        Index = -2;
                        t1 = value;
                    }
                    break;
                case {IsT2: true, AsT2: var value}:
                    {
                        Index = -3;
                        t2 = value;
                    }
                    break;
                case {IsT3: true, AsT3: var value}:
                    {
                        Index = -4;
                        t3 = value;
                    }
                    break;
                case {IsT4: true, AsT4: var value}:
                    {
                        Index = -5;
                        t4 = value;
                    }
                    break;
                case {IsT5: true, AsT5: var value}:
                    {
                        Index = -6;
                        t5 = value;
                    }
                    break;
                case {IsT6: true, AsT6: var value}:
                    {
                        Index = -7;
                        t6 = value;
                    }
                    break;
                default:
                    throw new InvalidCastException();
            }
        }

    #endregion
    
    #region Enumerable T0

        public bool TryGetAsEnumerableT0([NotNullWhen(true)] out IEnumerable<T0>? value) {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T0> AsEnumerableT0 => Index == 1 ? (IEnumerable<T0>)Many : throw new InvalidCastException();

        public bool IsEnumerableT0 => Index == 1;
                
        public OneOrEnumerable(IEnumerable<T0> asEnumerable)
        {
            Index = 1;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T1

        public bool TryGetAsEnumerableT1([NotNullWhen(true)] out IEnumerable<T1>? value) {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T1> AsEnumerableT1 => Index == 2 ? (IEnumerable<T1>)Many : throw new InvalidCastException();

        public bool IsEnumerableT1 => Index == 2;
                    
        public OneOrEnumerable(IEnumerable<T1> asEnumerable)
        {
            Index = 2;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T2

        public bool TryGetAsEnumerableT2([NotNullWhen(true)] out IEnumerable<T2>? value) {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T2> AsEnumerableT2 => Index == 3 ? (IEnumerable<T2>)Many : throw new InvalidCastException();

        public bool IsEnumerableT2 => Index == 3;
                    
        public OneOrEnumerable(IEnumerable<T2> asEnumerable)
        {
            Index = 3;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T3

        public bool TryGetAsEnumerableT3([NotNullWhen(true)] out IEnumerable<T3>? value) {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T3> AsEnumerableT3 => Index == 4 ? (IEnumerable<T3>)Many : throw new InvalidCastException();

        public bool IsEnumerableT3 => Index == 4;
                    
        public OneOrEnumerable(IEnumerable<T3> asEnumerable)
        {
            Index = 4;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T4

        public bool TryGetAsEnumerableT4([NotNullWhen(true)] out IEnumerable<T4>? value) {
            if (Index == 5)
            {
                value = (IEnumerable<T4>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T4> AsEnumerableT4 => Index == 5 ? (IEnumerable<T4>)Many : throw new InvalidCastException();

        public bool IsEnumerableT4 => Index == 5;
                    
        public OneOrEnumerable(IEnumerable<T4> asEnumerable)
        {
            Index = 5;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T5

        public bool TryGetAsEnumerableT5([NotNullWhen(true)] out IEnumerable<T5>? value) {
            if (Index == 6)
            {
                value = (IEnumerable<T5>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T5> AsEnumerableT5 => Index == 6 ? (IEnumerable<T5>)Many : throw new InvalidCastException();

        public bool IsEnumerableT5 => Index == 6;
                    
        public OneOrEnumerable(IEnumerable<T5> asEnumerable)
        {
            Index = 6;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable T6

        public bool TryGetAsEnumerableT6([NotNullWhen(true)] out IEnumerable<T6>? value) {
            if (Index == 7)
            {
                value = (IEnumerable<T6>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T6> AsEnumerableT6 => Index == 7 ? (IEnumerable<T6>)Many : throw new InvalidCastException();

        public bool IsEnumerableT6 => Index == 7;
                    
        public OneOrEnumerable(IEnumerable<T6> asEnumerable)
        {
            Index = 7;
            Many = asEnumerable;
        }

    #endregion
    
    #region Enumerable

        public bool TryGetAsEnumerable(out UnionEnumerable<T0, T1, T2, T3, T4, T5, T6> value) {
            if (Index is 1 or 2 or 3 or 4 or 5 or 6 or 7)
            {
                value = new((byte)Index, Many);
                return true;
            }
            value = default;
            return false;
        }
        
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6>(UnionEnumerable<T0, T1, T2, T3, T4, T5, T6> asEnumerable) => new(asEnumerable);
        
        public UnionEnumerable<T0, T1, T2, T3, T4, T5, T6> AsEnumerable => Index > 0 ? new((byte)Index, Many) : throw new InvalidCastException($"Union does not contain an Enumerable");

        public bool IsEnumerable => Index > 0;

        public OneOrEnumerable(UnionEnumerable<T0, T1, T2, T3, T4, T5, T6> asEnumerable)
        {
            Index = (sbyte)asEnumerable.Index;
            Many = (IEnumerable)asEnumerable.Value;
        }

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6>, TOutput> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6>, TOutput> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public TOutput Match<TOutput>(
            Func<T0, TOutput> oneT0Case, Func<IEnumerable<T0>, TOutput> enumerableT0Case,
            Func<T1, TOutput> oneT1Case, Func<IEnumerable<T1>, TOutput> enumerableT1Case,
            Func<T2, TOutput> oneT2Case, Func<IEnumerable<T2>, TOutput> enumerableT2Case,
            Func<T3, TOutput> oneT3Case, Func<IEnumerable<T3>, TOutput> enumerableT3Case,
            Func<T4, TOutput> oneT4Case, Func<IEnumerable<T4>, TOutput> enumerableT4Case,
            Func<T5, TOutput> oneT5Case, Func<IEnumerable<T5>, TOutput> enumerableT5Case,
            Func<T6, TOutput> oneT6Case, Func<IEnumerable<T6>, TOutput> enumerableT6Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0),
                 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),
                 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),
                 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),
                 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4),
                 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5),
                 6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6),
                 7 => enumerableT6Case((IEnumerable<T6>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6>, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6>, Task<TOutput>> enumerableCase) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, Task<TOutput>> enumerableT0Case,
            Func<T1, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, Task<TOutput>> enumerableT1Case,
            Func<T2, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, Task<TOutput>> enumerableT2Case,
            Func<T3, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, Task<TOutput>> enumerableT3Case,
            Func<T4, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, Task<TOutput>> enumerableT4Case,
            Func<T5, Task<TOutput>> oneT5Case, Func<IEnumerable<T5>, Task<TOutput>> enumerableT5Case,
            Func<T6, Task<TOutput>> oneT6Case, Func<IEnumerable<T6>, Task<TOutput>> enumerableT6Case) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0),
                 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),
                 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),
                 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),
                 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4),
                 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5),
                 6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6),
                 7 => enumerableT6Case((IEnumerable<T6>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6>, CancellationToken, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6>, CancellationToken, Task<TOutput>> enumerableCase, 
            CancellationToken ct) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, CancellationToken, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> enumerableT0Case, 
            Func<T1, CancellationToken, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> enumerableT1Case,
            Func<T2, CancellationToken, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> enumerableT2Case,
            Func<T3, CancellationToken, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> enumerableT3Case,
            Func<T4, CancellationToken, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> enumerableT4Case,
            Func<T5, CancellationToken, Task<TOutput>> oneT5Case, Func<IEnumerable<T5>, CancellationToken, Task<TOutput>> enumerableT5Case,
            Func<T6, CancellationToken, Task<TOutput>> oneT6Case, Func<IEnumerable<T6>, CancellationToken, Task<TOutput>> enumerableT6Case,
            CancellationToken ct) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct),
                 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct),
                 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct),
                 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct),
                 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct),
                 5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                -6 => oneT5Case(t5, ct),
                 6 => enumerableT5Case((IEnumerable<T5>)Many, ct),
                -7 => oneT6Case(t6, ct),
                 7 => enumerableT6Case((IEnumerable<T6>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<Union<T0, T1, T2, T3, T4, T5, T6>> oneCase, 
            Action<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6>> enumerableCase) 
        {
            switch(Index)
            {
                case < 0: oneCase(AsOne); break;
                case > 0: enumerableCase(AsEnumerable); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }
        
        public void Switch(
            Action<T0> oneT0Case, 
            Action<IEnumerable<T0>> enumerableT0Case,
            Action<T1> oneT1Case, 
            Action<IEnumerable<T1>> enumerableT1Case,
            Action<T2> oneT2Case, 
            Action<IEnumerable<T2>> enumerableT2Case,
            Action<T3> oneT3Case, 
            Action<IEnumerable<T3>> enumerableT3Case,
            Action<T4> oneT4Case, 
            Action<IEnumerable<T4>> enumerableT4Case,
            Action<T5> oneT5Case, 
            Action<IEnumerable<T5>> enumerableT5Case,
            Action<T6> oneT6Case, 
            Action<IEnumerable<T6>> enumerableT6Case) 
        {
            switch(Index)
            {
                case -1: oneT0Case(t0); break;
                case  1: enumerableT0Case((IEnumerable<T0>)Many); break;
                case -2: oneT1Case(t1); break;
                case  2: enumerableT1Case((IEnumerable<T1>)Many); break;
                case -3: oneT2Case(t2); break;
                case  3: enumerableT2Case((IEnumerable<T2>)Many); break;
                case -4: oneT3Case(t3); break;
                case  4: enumerableT3Case((IEnumerable<T3>)Many); break;
                case -5: oneT4Case(t4); break;
                case  5: enumerableT4Case((IEnumerable<T4>)Many); break;
                case -6: oneT5Case(t5); break;
                case  6: enumerableT5Case((IEnumerable<T5>)Many); break;
                case -7: oneT6Case(t6); break;
                case  7: enumerableT6Case((IEnumerable<T6>)Many); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4, T5, T6>, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6>, Task> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, Task> oneT0Case, Func<IEnumerable<T0>, Task> enumerableT0Case,
            Func<T1, Task> oneT1Case, Func<IEnumerable<T1>, Task> enumerableT1Case,
            Func<T2, Task> oneT2Case, Func<IEnumerable<T2>, Task> enumerableT2Case,
            Func<T3, Task> oneT3Case, Func<IEnumerable<T3>, Task> enumerableT3Case,
            Func<T4, Task> oneT4Case, Func<IEnumerable<T4>, Task> enumerableT4Case,
            Func<T5, Task> oneT5Case, Func<IEnumerable<T5>, Task> enumerableT5Case,
            Func<T6, Task> oneT6Case, Func<IEnumerable<T6>, Task> enumerableT6Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0),
                 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),
                 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),
                 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),
                 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4),
                 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5),
                 6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6),
                 7 => enumerableT6Case((IEnumerable<T6>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4, T5, T6>, CancellationToken, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6>, CancellationToken, Task> enumerableCase, 
            CancellationToken ct)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, CancellationToken, Task> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task> enumerableT0Case, 
            Func<T1, CancellationToken, Task> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task> enumerableT1Case,
            Func<T2, CancellationToken, Task> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task> enumerableT2Case,
            Func<T3, CancellationToken, Task> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task> enumerableT3Case,
            Func<T4, CancellationToken, Task> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task> enumerableT4Case,
            Func<T5, CancellationToken, Task> oneT5Case, Func<IEnumerable<T5>, CancellationToken, Task> enumerableT5Case,
            Func<T6, CancellationToken, Task> oneT6Case, Func<IEnumerable<T6>, CancellationToken, Task> enumerableT6Case,
            CancellationToken ct)
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct),
                 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct),
                 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct),
                 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct),
                 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct),
                 5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                -6 => oneT5Case(t5, ct),
                 6 => enumerableT5Case((IEnumerable<T5>)Many, ct),
                -7 => oneT6Case(t6, ct),
                 7 => enumerableT6Case((IEnumerable<T6>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
}

public readonly struct OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>
{
    private sbyte Index { get; }
    // ReSharper disable InconsistentNaming
    private T0 t0 { get; } = default!;
    private T1 t1 { get; } = default!;
    private T2 t2 { get; } = default!;
    private T3 t3 { get; } = default!;
    private T4 t4 { get; } = default!;
    private T5 t5 { get; } = default!;
    private T6 t6 { get; } = default!;
    private T7 t7 { get; } = default!;
    // ReSharper restore InconsistentNaming
    private IEnumerable Many { get; } = null!;

    #region One T0
        public bool TryGetAsOneT0(out T0 value) {
            if (Index == -1)
            {
                value = t0;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>(T0 asOne) => new(asOne);

        public T0 AsOneT0 => Index == -1 ? t0 : throw new InvalidCastException();

        public bool IsOneT0 => Index == -1;
            
        public OneOrEnumerable(T0 asOne)
        {
            Index = -1;
            t0 = asOne;
        }
    #endregion
    
    #region One T1
        public bool TryGetAsOneT1(out T1 value) {
            if (Index == -2)
            {
                value = t1;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>(T1 asOne) => new(asOne);

        public T1 AsOneT1 => Index == -2 ? t1 : throw new InvalidCastException();

        public bool IsOneT1 => Index == -2;
                
        public OneOrEnumerable(T1 asOne)
        {
            Index = -2;
            t1 = asOne;
        }
    #endregion
    
    #region One T2
        public bool TryGetAsOneT2(out T2 value) {
            if (Index == -3)
            {
                value = t2;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>(T2 asOne) => new(asOne);

        public T2 AsOneT2 => Index == -3 ? t2 : throw new InvalidCastException();

        public bool IsOneT2 => Index == -3;
                
        public OneOrEnumerable(T2 asOne)
        {
            Index = -3;
            t2 = asOne;
        }
    #endregion
    
    #region One T3
        public bool TryGetAsOneT3(out T3 value) {
            if (Index == -4)
            {
                value = t3;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>(T3 asOne) => new(asOne);

        public T3 AsOneT3 => Index == -4 ? t3 : throw new InvalidCastException();

        public bool IsOneT3 => Index == -4;
                
        public OneOrEnumerable(T3 asOne)
        {
            Index = -4;
            t3 = asOne;
        }
    #endregion
    
    #region One T4
        public bool TryGetAsOneT4(out T4 value) {
            if (Index == -5)
            {
                value = t4;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>(T4 asOne) => new(asOne);

        public T4 AsOneT4 => Index == -5 ? t4 : throw new InvalidCastException();

        public bool IsOneT4 => Index == -5;
                
        public OneOrEnumerable(T4 asOne)
        {
            Index = -5;
            t4 = asOne;
        }
    #endregion
    
    #region One T5
        public bool TryGetAsOneT5(out T5 value) {
            if (Index == -6)
            {
                value = t5;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>(T5 asOne) => new(asOne);

        public T5 AsOneT5 => Index == -6 ? t5 : throw new InvalidCastException();

        public bool IsOneT5 => Index == -6;
                
        public OneOrEnumerable(T5 asOne)
        {
            Index = -6;
            t5 = asOne;
        }
    #endregion
    
    #region One T6
        public bool TryGetAsOneT6(out T6 value) {
            if (Index == -7)
            {
                value = t6;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>(T6 asOne) => new(asOne);

        public T6 AsOneT6 => Index == -7 ? t6 : throw new InvalidCastException();

        public bool IsOneT6 => Index == -7;
                
        public OneOrEnumerable(T6 asOne)
        {
            Index = -7;
            t6 = asOne;
        }
    #endregion
    
    #region One T7
        public bool TryGetAsOneT7(out T7 value) {
            if (Index == -8)
            {
                value = t7;
                return true;
            }
            value = default!;
            return false;
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>(T7 asOne) => new(asOne);

        public T7 AsOneT7 => Index == -8 ? t7 : throw new InvalidCastException();

        public bool IsOneT7 => Index == -8;
                
        public OneOrEnumerable(T7 asOne)
        {
            Index = -8;
            t7 = asOne;
        }
    #endregion
    
    #region One
        public bool TryGetAsOne(out Union<T0, T1, T2, T3, T4, T5, T6, T7> value) {
            switch (Index)
            {
                case -1: value = t0; return true;
                case -2: value = t1; return true;
                case -3: value = t2; return true;
                case -4: value = t3; return true;
                case -5: value = t4; return true;
                case -6: value = t5; return true;
                case -7: value = t6; return true;
                case -8: value = t7; return true;
                default: value = default!; return false;
            }
        }

        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>(Union<T0, T1, T2, T3, T4, T5, T6, T7> asOne) => new(asOne);
        
        public Union<T0, T1, T2, T3, T4, T5, T6, T7> AsOne => Index switch {
            -1 => t0, -2 => t1, -3 => t2, -4 => t3,
            -5 => t4, -6 => t5, -7 => t6, -8 => t7,
            _ => throw new InvalidCastException()
        };

        public bool IsOne => Index < 0;
        
        public OneOrEnumerable(Union<T0, T1, T2, T3, T4, T5, T6, T7> asOne)
        {
            switch (asOne)
            {
                case {IsT0: true, AsT0: var value}: Index = -1; t0 = value; break;
                case {IsT1: true, AsT1: var value}: Index = -2; t1 = value; break;
                case {IsT2: true, AsT2: var value}: Index = -3; t2 = value; break;
                case {IsT3: true, AsT3: var value}: Index = -4; t3 = value; break;
                case {IsT4: true, AsT4: var value}: Index = -5; t4 = value; break;
                case {IsT5: true, AsT5: var value}: Index = -6; t5 = value; break;
                case {IsT6: true, AsT6: var value}: Index = -7; t6 = value; break;
                case {IsT7: true, AsT7: var value}: Index = -8; t7 = value; break;
                default: throw new InvalidCastException();
            }
        }
    #endregion
    
    #region Enumerable T0
        public bool TryGetAsEnumerableT0([NotNullWhen(true)] out IEnumerable<T0>? value) {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T0> AsEnumerableT0 => Index == 1 ? (IEnumerable<T0>)Many : throw new InvalidCastException();

        public bool IsEnumerableT0 => Index == 1;
                
        public OneOrEnumerable(IEnumerable<T0> asEnumerable)
        {
            Index = 1;
            Many = asEnumerable;
        }
    #endregion
    
    #region Enumerable T1
        public bool TryGetAsEnumerableT1([NotNullWhen(true)] out IEnumerable<T1>? value) {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T1> AsEnumerableT1 => Index == 2 ? (IEnumerable<T1>)Many : throw new InvalidCastException();

        public bool IsEnumerableT1 => Index == 2;
                    
        public OneOrEnumerable(IEnumerable<T1> asEnumerable)
        {
            Index = 2;
            Many = asEnumerable;
        }
    #endregion
    
    #region Enumerable T2
        public bool TryGetAsEnumerableT2([NotNullWhen(true)] out IEnumerable<T2>? value) {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T2> AsEnumerableT2 => Index == 3 ? (IEnumerable<T2>)Many : throw new InvalidCastException();

        public bool IsEnumerableT2 => Index == 3;
                    
        public OneOrEnumerable(IEnumerable<T2> asEnumerable)
        {
            Index = 3;
            Many = asEnumerable;
        }
    #endregion
    
    #region Enumerable T3
        public bool TryGetAsEnumerableT3([NotNullWhen(true)] out IEnumerable<T3>? value) {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T3> AsEnumerableT3 => Index == 4 ? (IEnumerable<T3>)Many : throw new InvalidCastException();

        public bool IsEnumerableT3 => Index == 4;
                    
        public OneOrEnumerable(IEnumerable<T3> asEnumerable)
        {
            Index = 4;
            Many = asEnumerable;
        }
    #endregion
    
    #region Enumerable T4
        public bool TryGetAsEnumerableT4([NotNullWhen(true)] out IEnumerable<T4>? value) {
            if (Index == 5)
            {
                value = (IEnumerable<T4>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T4> AsEnumerableT4 => Index == 5 ? (IEnumerable<T4>)Many : throw new InvalidCastException();

        public bool IsEnumerableT4 => Index == 5;
                    
        public OneOrEnumerable(IEnumerable<T4> asEnumerable)
        {
            Index = 5;
            Many = asEnumerable;
        }
    #endregion
    
    #region Enumerable T5
        public bool TryGetAsEnumerableT5([NotNullWhen(true)] out IEnumerable<T5>? value) {
            if (Index == 6)
            {
                value = (IEnumerable<T5>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T5> AsEnumerableT5 => Index == 6 ? (IEnumerable<T5>)Many : throw new InvalidCastException();

        public bool IsEnumerableT5 => Index == 6;
                    
        public OneOrEnumerable(IEnumerable<T5> asEnumerable)
        {
            Index = 6;
            Many = asEnumerable;
        }
    #endregion
    
    #region Enumerable T6
        public bool TryGetAsEnumerableT6([NotNullWhen(true)] out IEnumerable<T6>? value) {
            if (Index == 7)
            {
                value = (IEnumerable<T6>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T6> AsEnumerableT6 => Index == 7 ? (IEnumerable<T6>)Many : throw new InvalidCastException();

        public bool IsEnumerableT6 => Index == 7;
                    
        public OneOrEnumerable(IEnumerable<T6> asEnumerable)
        {
            Index = 7;
            Many = asEnumerable;
        }
    #endregion
    
    #region Enumerable T7
        public bool TryGetAsEnumerableT7([NotNullWhen(true)] out IEnumerable<T7>? value) {
            if (Index == 8)
            {
                value = (IEnumerable<T7>)Many;
                return true;
            }
            value = null!;
            return false;
        }

        public IEnumerable<T7> AsEnumerableT7 => Index == 8 ? (IEnumerable<T7>)Many : throw new InvalidCastException();

        public bool IsEnumerableT7 => Index == 8;
                    
        public OneOrEnumerable(IEnumerable<T7> asEnumerable)
        {
            Index = 8;
            Many = asEnumerable;
        }
    #endregion
    
    #region Enumerable
        public bool TryGetAsEnumerable(out UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7> value) {
            if (Index is 1 or 2 or 3 or 4 or 5 or 6 or 7 or 8)
            {
                value = new((byte)Index, Many);
                return true;
            }
            value = default;
            return false;
        }
        
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>(UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7> asEnumerable) => new(asEnumerable);
        
        public UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7> AsEnumerable => Index > 0 ? new((byte)Index, Many) : throw new InvalidCastException($"Union does not contain an Enumerable");

        public bool IsEnumerable => Index > 0;

        public OneOrEnumerable(UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7> asEnumerable)
        {
            Index = (sbyte)asEnumerable.Index;
            Many = (IEnumerable)asEnumerable.Value;
        }
    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7>, TOutput> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>, TOutput> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public TOutput Match<TOutput>(
            Func<T0, TOutput> oneT0Case, Func<IEnumerable<T0>, TOutput> enumerableT0Case,
            Func<T1, TOutput> oneT1Case, Func<IEnumerable<T1>, TOutput> enumerableT1Case,
            Func<T2, TOutput> oneT2Case, Func<IEnumerable<T2>, TOutput> enumerableT2Case,
            Func<T3, TOutput> oneT3Case, Func<IEnumerable<T3>, TOutput> enumerableT3Case,
            Func<T4, TOutput> oneT4Case, Func<IEnumerable<T4>, TOutput> enumerableT4Case,
            Func<T5, TOutput> oneT5Case, Func<IEnumerable<T5>, TOutput> enumerableT5Case,
            Func<T6, TOutput> oneT6Case, Func<IEnumerable<T6>, TOutput> enumerableT6Case,
            Func<T7, TOutput> oneT7Case, Func<IEnumerable<T7>, TOutput> enumerableT7Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2), 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3), 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4), 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5), 6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6), 7 => enumerableT6Case((IEnumerable<T6>)Many),
                -8 => oneT7Case(t7), 8 => enumerableT7Case((IEnumerable<T7>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7>, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>, Task<TOutput>> enumerableCase) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, Task<TOutput>> enumerableT0Case,
            Func<T1, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, Task<TOutput>> enumerableT1Case,
            Func<T2, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, Task<TOutput>> enumerableT2Case,
            Func<T3, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, Task<TOutput>> enumerableT3Case,
            Func<T4, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, Task<TOutput>> enumerableT4Case,
            Func<T5, Task<TOutput>> oneT5Case, Func<IEnumerable<T5>, Task<TOutput>> enumerableT5Case,
            Func<T6, Task<TOutput>> oneT6Case, Func<IEnumerable<T6>, Task<TOutput>> enumerableT6Case,
            Func<T7, Task<TOutput>> oneT7Case, Func<IEnumerable<T7>, Task<TOutput>> enumerableT7Case) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2), 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3), 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4), 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5), 6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6), 7 => enumerableT6Case((IEnumerable<T6>)Many),
                -8 => oneT7Case(t7), 8 => enumerableT7Case((IEnumerable<T7>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7>, CancellationToken, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>, CancellationToken, Task<TOutput>> enumerableCase, 
            CancellationToken ct) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, CancellationToken, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> enumerableT0Case, 
            Func<T1, CancellationToken, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> enumerableT1Case,
            Func<T2, CancellationToken, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> enumerableT2Case,
            Func<T3, CancellationToken, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> enumerableT3Case,
            Func<T4, CancellationToken, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> enumerableT4Case,
            Func<T5, CancellationToken, Task<TOutput>> oneT5Case, Func<IEnumerable<T5>, CancellationToken, Task<TOutput>> enumerableT5Case,
            Func<T6, CancellationToken, Task<TOutput>> oneT6Case, Func<IEnumerable<T6>, CancellationToken, Task<TOutput>> enumerableT6Case,
            Func<T7, CancellationToken, Task<TOutput>> oneT7Case, Func<IEnumerable<T7>, CancellationToken, Task<TOutput>> enumerableT7Case,
            CancellationToken ct) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct), 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct), 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct), 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct), 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct), 5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                -6 => oneT5Case(t5, ct), 6 => enumerableT5Case((IEnumerable<T5>)Many, ct),
                -7 => oneT6Case(t6, ct), 7 => enumerableT6Case((IEnumerable<T6>)Many, ct),
                -8 => oneT7Case(t7, ct), 8 => enumerableT7Case((IEnumerable<T7>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<Union<T0, T1, T2, T3, T4, T5, T6, T7>> oneCase, 
            Action<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>> enumerableCase) 
        {
            switch(Index)
            {
                case < 0: oneCase(AsOne); break;
                case > 0: enumerableCase(AsEnumerable); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }
        
        public void Switch(
            Action<T0> oneT0Case, 
            Action<IEnumerable<T0>> enumerableT0Case,
            Action<T1> oneT1Case, 
            Action<IEnumerable<T1>> enumerableT1Case,
            Action<T2> oneT2Case, 
            Action<IEnumerable<T2>> enumerableT2Case,
            Action<T3> oneT3Case, 
            Action<IEnumerable<T3>> enumerableT3Case,
            Action<T4> oneT4Case, 
            Action<IEnumerable<T4>> enumerableT4Case,
            Action<T5> oneT5Case, 
            Action<IEnumerable<T5>> enumerableT5Case,
            Action<T6> oneT6Case, 
            Action<IEnumerable<T6>> enumerableT6Case,
            Action<T7> oneT7Case, 
            Action<IEnumerable<T7>> enumerableT7Case) 
        {
            switch(Index)
            {
                case -1: oneT0Case(t0); break; case  1: enumerableT0Case((IEnumerable<T0>)Many); break;
                case -2: oneT1Case(t1); break; case  2: enumerableT1Case((IEnumerable<T1>)Many); break;
                case -3: oneT2Case(t2); break; case  3: enumerableT2Case((IEnumerable<T2>)Many); break;
                case -4: oneT3Case(t3); break; case  4: enumerableT3Case((IEnumerable<T3>)Many); break;
                case -5: oneT4Case(t4); break; case  5: enumerableT4Case((IEnumerable<T4>)Many); break;
                case -6: oneT5Case(t5); break; case  6: enumerableT5Case((IEnumerable<T5>)Many); break;
                case -7: oneT6Case(t6); break; case  7: enumerableT6Case((IEnumerable<T6>)Many); break;
                case -8: oneT7Case(t7); break; case  8: enumerableT7Case((IEnumerable<T7>)Many); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7>, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>, Task> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, Task> oneT0Case, Func<IEnumerable<T0>, Task> enumerableT0Case,
            Func<T1, Task> oneT1Case, Func<IEnumerable<T1>, Task> enumerableT1Case,
            Func<T2, Task> oneT2Case, Func<IEnumerable<T2>, Task> enumerableT2Case,
            Func<T3, Task> oneT3Case, Func<IEnumerable<T3>, Task> enumerableT3Case,
            Func<T4, Task> oneT4Case, Func<IEnumerable<T4>, Task> enumerableT4Case,
            Func<T5, Task> oneT5Case, Func<IEnumerable<T5>, Task> enumerableT5Case,
            Func<T6, Task> oneT6Case, Func<IEnumerable<T6>, Task> enumerableT6Case,
            Func<T7, Task> oneT7Case, Func<IEnumerable<T7>, Task> enumerableT7Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2), 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3), 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4), 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5), 6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6), 7 => enumerableT6Case((IEnumerable<T6>)Many),
                -8 => oneT7Case(t7), 8 => enumerableT7Case((IEnumerable<T7>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7>, CancellationToken, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>, CancellationToken, Task> enumerableCase, 
            CancellationToken ct)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, CancellationToken, Task> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task> enumerableT0Case, 
            Func<T1, CancellationToken, Task> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task> enumerableT1Case,
            Func<T2, CancellationToken, Task> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task> enumerableT2Case,
            Func<T3, CancellationToken, Task> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task> enumerableT3Case,
            Func<T4, CancellationToken, Task> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task> enumerableT4Case,
            Func<T5, CancellationToken, Task> oneT5Case, Func<IEnumerable<T5>, CancellationToken, Task> enumerableT5Case,
            Func<T6, CancellationToken, Task> oneT6Case, Func<IEnumerable<T6>, CancellationToken, Task> enumerableT6Case,
            Func<T7, CancellationToken, Task> oneT7Case, Func<IEnumerable<T7>, CancellationToken, Task> enumerableT7Case,
            CancellationToken ct)
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct), 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct), 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct), 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct), 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct), 5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                -6 => oneT5Case(t5, ct), 6 => enumerableT5Case((IEnumerable<T5>)Many, ct),
                -7 => oneT6Case(t6, ct), 7 => enumerableT6Case((IEnumerable<T6>)Many, ct),
                -8 => oneT7Case(t7, ct), 8 => enumerableT7Case((IEnumerable<T7>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
}

public readonly struct OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>
{
    private sbyte Index { get; }
    // ReSharper disable InconsistentNaming
    private T0 t0 { get; } = default!;
    private T1 t1 { get; } = default!;
    private T2 t2 { get; } = default!;
    private T3 t3 { get; } = default!;
    private T4 t4 { get; } = default!;
    private T5 t5 { get; } = default!;
    private T6 t6 { get; } = default!;
    private T7 t7 { get; } = default!;
    private T8 t8 { get; } = default!;
    // ReSharper restore InconsistentNaming
    private IEnumerable Many { get; } = null!;

    #region One T0
        public bool TryGetAsOneT0(out T0 value) {
            if (Index == -1) { value = t0; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(T0 asOne) => new(asOne);
        public T0 AsOneT0 => Index == -1 ? t0 : throw new InvalidCastException();
        public bool IsOneT0 => Index == -1;
        public OneOrEnumerable(T0 asOne) { Index = -1; t0 = asOne; }
    #endregion
    
    #region One T1
        public bool TryGetAsOneT1(out T1 value) {
            if (Index == -2) { value = t1; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(T1 asOne) => new(asOne);
        public T1 AsOneT1 => Index == -2 ? t1 : throw new InvalidCastException();
        public bool IsOneT1 => Index == -2;
        public OneOrEnumerable(T1 asOne) { Index = -2; t1 = asOne; }
    #endregion
    
    #region One T2
        public bool TryGetAsOneT2(out T2 value) {
            if (Index == -3) { value = t2; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(T2 asOne) => new(asOne);
        public T2 AsOneT2 => Index == -3 ? t2 : throw new InvalidCastException();
        public bool IsOneT2 => Index == -3;
        public OneOrEnumerable(T2 asOne) { Index = -3; t2 = asOne; }
    #endregion
    
    #region One T3
        public bool TryGetAsOneT3(out T3 value) {
            if (Index == -4) { value = t3; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(T3 asOne) => new(asOne);
        public T3 AsOneT3 => Index == -4 ? t3 : throw new InvalidCastException();
        public bool IsOneT3 => Index == -4;
        public OneOrEnumerable(T3 asOne) { Index = -4; t3 = asOne; }
    #endregion
    
    #region One T4
        public bool TryGetAsOneT4(out T4 value) {
            if (Index == -5) { value = t4; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(T4 asOne) => new(asOne);
        public T4 AsOneT4 => Index == -5 ? t4 : throw new InvalidCastException();
        public bool IsOneT4 => Index == -5;
        public OneOrEnumerable(T4 asOne) { Index = -5; t4 = asOne; }
    #endregion
    
    #region One T5
        public bool TryGetAsOneT5(out T5 value) {
            if (Index == -6) { value = t5; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(T5 asOne) => new(asOne);
        public T5 AsOneT5 => Index == -6 ? t5 : throw new InvalidCastException();
        public bool IsOneT5 => Index == -6;
        public OneOrEnumerable(T5 asOne) { Index = -6; t5 = asOne; }
    #endregion
    
    #region One T6
        public bool TryGetAsOneT6(out T6 value) {
            if (Index == -7) { value = t6; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(T6 asOne) => new(asOne);
        public T6 AsOneT6 => Index == -7 ? t6 : throw new InvalidCastException();
        public bool IsOneT6 => Index == -7;
        public OneOrEnumerable(T6 asOne) { Index = -7; t6 = asOne; }
    #endregion
    
    #region One T7
        public bool TryGetAsOneT7(out T7 value) {
            if (Index == -8) { value = t7; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(T7 asOne) => new(asOne);
        public T7 AsOneT7 => Index == -8 ? t7 : throw new InvalidCastException();
        public bool IsOneT7 => Index == -8;
        public OneOrEnumerable(T7 asOne) { Index = -8; t7 = asOne; }
    #endregion
    
    #region One T8
        public bool TryGetAsOneT8(out T8 value) {
            if (Index == -9) { value = t8; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(T8 asOne) => new(asOne);
        public T8 AsOneT8 => Index == -9 ? t8 : throw new InvalidCastException();
        public bool IsOneT8 => Index == -9;
        public OneOrEnumerable(T8 asOne) { Index = -9; t8 = asOne; }
    #endregion
    
    #region One
        public bool TryGetAsOne(out Union<T0, T1, T2, T3, T4, T5, T6, T7, T8> value) {
            switch (Index)
            {
                case -1: value = t0; return true;
                case -2: value = t1; return true;
                case -3: value = t2; return true;
                case -4: value = t3; return true;
                case -5: value = t4; return true;
                case -6: value = t5; return true;
                case -7: value = t6; return true;
                case -8: value = t7; return true;
                case -9: value = t8; return true;
                default: value = default!; return false;
            }
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Union<T0, T1, T2, T3, T4, T5, T6, T7, T8> asOne) => new(asOne);
        public Union<T0, T1, T2, T3, T4, T5, T6, T7, T8> AsOne => Index switch {
            -1 => t0, -2 => t1, -3 => t2, -4 => t3,
            -5 => t4, -6 => t5, -7 => t6, -8 => t7, -9 => t8,
            _ => throw new InvalidCastException()
        };
        public bool IsOne => Index < 0;
        public OneOrEnumerable(Union<T0, T1, T2, T3, T4, T5, T6, T7, T8> asOne)
        {
            switch (asOne)
            {
                case {IsT0: true, AsT0: var value}: Index = -1; t0 = value; break;
                case {IsT1: true, AsT1: var value}: Index = -2; t1 = value; break;
                case {IsT2: true, AsT2: var value}: Index = -3; t2 = value; break;
                case {IsT3: true, AsT3: var value}: Index = -4; t3 = value; break;
                case {IsT4: true, AsT4: var value}: Index = -5; t4 = value; break;
                case {IsT5: true, AsT5: var value}: Index = -6; t5 = value; break;
                case {IsT6: true, AsT6: var value}: Index = -7; t6 = value; break;
                case {IsT7: true, AsT7: var value}: Index = -8; t7 = value; break;
                case {IsT8: true, AsT8: var value}: Index = -9; t8 = value; break;
                default: throw new InvalidCastException();
            }
        }
    #endregion
    
    #region Enumerable T0
        public bool TryGetAsEnumerableT0([NotNullWhen(true)] out IEnumerable<T0>? value) {
            if (Index == 1) { value = (IEnumerable<T0>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T0> AsEnumerableT0 => Index == 1 ? (IEnumerable<T0>)Many : throw new InvalidCastException();
        public bool IsEnumerableT0 => Index == 1;
        public OneOrEnumerable(IEnumerable<T0> asEnumerable) { Index = 1; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T1
        public bool TryGetAsEnumerableT1([NotNullWhen(true)] out IEnumerable<T1>? value) {
            if (Index == 2) { value = (IEnumerable<T1>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T1> AsEnumerableT1 => Index == 2 ? (IEnumerable<T1>)Many : throw new InvalidCastException();
        public bool IsEnumerableT1 => Index == 2;
        public OneOrEnumerable(IEnumerable<T1> asEnumerable) { Index = 2; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T2
        public bool TryGetAsEnumerableT2([NotNullWhen(true)] out IEnumerable<T2>? value) {
            if (Index == 3) { value = (IEnumerable<T2>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T2> AsEnumerableT2 => Index == 3 ? (IEnumerable<T2>)Many : throw new InvalidCastException();
        public bool IsEnumerableT2 => Index == 3;
        public OneOrEnumerable(IEnumerable<T2> asEnumerable) { Index = 3; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T3
        public bool TryGetAsEnumerableT3([NotNullWhen(true)] out IEnumerable<T3>? value) {
            if (Index == 4) { value = (IEnumerable<T3>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T3> AsEnumerableT3 => Index == 4 ? (IEnumerable<T3>)Many : throw new InvalidCastException();
        public bool IsEnumerableT3 => Index == 4;
        public OneOrEnumerable(IEnumerable<T3> asEnumerable) { Index = 4; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T4
        public bool TryGetAsEnumerableT4([NotNullWhen(true)] out IEnumerable<T4>? value) {
            if (Index == 5) { value = (IEnumerable<T4>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T4> AsEnumerableT4 => Index == 5 ? (IEnumerable<T4>)Many : throw new InvalidCastException();
        public bool IsEnumerableT4 => Index == 5;
        public OneOrEnumerable(IEnumerable<T4> asEnumerable) { Index = 5; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T5
        public bool TryGetAsEnumerableT5([NotNullWhen(true)] out IEnumerable<T5>? value) {
            if (Index == 6) { value = (IEnumerable<T5>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T5> AsEnumerableT5 => Index == 6 ? (IEnumerable<T5>)Many : throw new InvalidCastException();
        public bool IsEnumerableT5 => Index == 6;
        public OneOrEnumerable(IEnumerable<T5> asEnumerable) { Index = 6; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T6
        public bool TryGetAsEnumerableT6([NotNullWhen(true)] out IEnumerable<T6>? value) {
            if (Index == 7) { value = (IEnumerable<T6>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T6> AsEnumerableT6 => Index == 7 ? (IEnumerable<T6>)Many : throw new InvalidCastException();
        public bool IsEnumerableT6 => Index == 7;
        public OneOrEnumerable(IEnumerable<T6> asEnumerable) { Index = 7; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T7
        public bool TryGetAsEnumerableT7([NotNullWhen(true)] out IEnumerable<T7>? value) {
            if (Index == 8) { value = (IEnumerable<T7>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T7> AsEnumerableT7 => Index == 8 ? (IEnumerable<T7>)Many : throw new InvalidCastException();
        public bool IsEnumerableT7 => Index == 8;
        public OneOrEnumerable(IEnumerable<T7> asEnumerable) { Index = 8; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T8
        public bool TryGetAsEnumerableT8([NotNullWhen(true)] out IEnumerable<T8>? value) {
            if (Index == 9) { value = (IEnumerable<T8>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T8> AsEnumerableT8 => Index == 9 ? (IEnumerable<T8>)Many : throw new InvalidCastException();
        public bool IsEnumerableT8 => Index == 9;
        public OneOrEnumerable(IEnumerable<T8> asEnumerable) { Index = 9; Many = asEnumerable; }
    #endregion
    
    #region Enumerable
        public bool TryGetAsEnumerable(out UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8> value) {
            if (Index is 1 or 2 or 3 or 4 or 5 or 6 or 7 or 8 or 9)
            {
                value = new((byte)Index, Many);
                return true;
            }
            value = default;
            return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8> asEnumerable) => new(asEnumerable);
        public UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8> AsEnumerable => Index > 0 ? new((byte)Index, Many) : throw new InvalidCastException($"Union does not contain an Enumerable");
        public bool IsEnumerable => Index > 0;
        public OneOrEnumerable(UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8> asEnumerable)
        {
            Index = (sbyte)asEnumerable.Index;
            Many = (IEnumerable)asEnumerable.Value;
        }
    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8>, TOutput> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>, TOutput> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public TOutput Match<TOutput>(
            Func<T0, TOutput> oneT0Case, Func<IEnumerable<T0>, TOutput> enumerableT0Case,
            Func<T1, TOutput> oneT1Case, Func<IEnumerable<T1>, TOutput> enumerableT1Case,
            Func<T2, TOutput> oneT2Case, Func<IEnumerable<T2>, TOutput> enumerableT2Case,
            Func<T3, TOutput> oneT3Case, Func<IEnumerable<T3>, TOutput> enumerableT3Case,
            Func<T4, TOutput> oneT4Case, Func<IEnumerable<T4>, TOutput> enumerableT4Case,
            Func<T5, TOutput> oneT5Case, Func<IEnumerable<T5>, TOutput> enumerableT5Case,
            Func<T6, TOutput> oneT6Case, Func<IEnumerable<T6>, TOutput> enumerableT6Case,
            Func<T7, TOutput> oneT7Case, Func<IEnumerable<T7>, TOutput> enumerableT7Case,
            Func<T8, TOutput> oneT8Case, Func<IEnumerable<T8>, TOutput> enumerableT8Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2), 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3), 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4), 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5), 6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6), 7 => enumerableT6Case((IEnumerable<T6>)Many),
                -8 => oneT7Case(t7), 8 => enumerableT7Case((IEnumerable<T7>)Many),
                -9 => oneT8Case(t8), 9 => enumerableT8Case((IEnumerable<T8>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8>, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>, Task<TOutput>> enumerableCase) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, Task<TOutput>> enumerableT0Case,
            Func<T1, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, Task<TOutput>> enumerableT1Case,
            Func<T2, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, Task<TOutput>> enumerableT2Case,
            Func<T3, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, Task<TOutput>> enumerableT3Case,
            Func<T4, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, Task<TOutput>> enumerableT4Case,
            Func<T5, Task<TOutput>> oneT5Case, Func<IEnumerable<T5>, Task<TOutput>> enumerableT5Case,
            Func<T6, Task<TOutput>> oneT6Case, Func<IEnumerable<T6>, Task<TOutput>> enumerableT6Case,
            Func<T7, Task<TOutput>> oneT7Case, Func<IEnumerable<T7>, Task<TOutput>> enumerableT7Case,
            Func<T8, Task<TOutput>> oneT8Case, Func<IEnumerable<T8>, Task<TOutput>> enumerableT8Case) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2), 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3), 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4), 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5), 6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6), 7 => enumerableT6Case((IEnumerable<T6>)Many),
                -8 => oneT7Case(t7), 8 => enumerableT7Case((IEnumerable<T7>)Many),
                -9 => oneT8Case(t8), 9 => enumerableT8Case((IEnumerable<T8>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8>, CancellationToken, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>, CancellationToken, Task<TOutput>> enumerableCase, 
            CancellationToken ct) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, CancellationToken, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> enumerableT0Case, 
            Func<T1, CancellationToken, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> enumerableT1Case,
            Func<T2, CancellationToken, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> enumerableT2Case,
            Func<T3, CancellationToken, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> enumerableT3Case,
            Func<T4, CancellationToken, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> enumerableT4Case,
            Func<T5, CancellationToken, Task<TOutput>> oneT5Case, Func<IEnumerable<T5>, CancellationToken, Task<TOutput>> enumerableT5Case,
            Func<T6, CancellationToken, Task<TOutput>> oneT6Case, Func<IEnumerable<T6>, CancellationToken, Task<TOutput>> enumerableT6Case,
            Func<T7, CancellationToken, Task<TOutput>> oneT7Case, Func<IEnumerable<T7>, CancellationToken, Task<TOutput>> enumerableT7Case,
            Func<T8, CancellationToken, Task<TOutput>> oneT8Case, Func<IEnumerable<T8>, CancellationToken, Task<TOutput>> enumerableT8Case,
            CancellationToken ct) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct), 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct), 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct), 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct), 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct), 5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                -6 => oneT5Case(t5, ct), 6 => enumerableT5Case((IEnumerable<T5>)Many, ct),
                -7 => oneT6Case(t6, ct), 7 => enumerableT6Case((IEnumerable<T6>)Many, ct),
                -8 => oneT7Case(t7, ct), 8 => enumerableT7Case((IEnumerable<T7>)Many, ct),
                -9 => oneT8Case(t8, ct), 9 => enumerableT8Case((IEnumerable<T8>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8>> oneCase, 
            Action<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>> enumerableCase) 
        {
            switch(Index)
            {
                case < 0: oneCase(AsOne); break;
                case > 0: enumerableCase(AsEnumerable); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }
        
        public void Switch(
            Action<T0> oneT0Case, 
            Action<IEnumerable<T0>> enumerableT0Case,
            Action<T1> oneT1Case, 
            Action<IEnumerable<T1>> enumerableT1Case,
            Action<T2> oneT2Case, 
            Action<IEnumerable<T2>> enumerableT2Case,
            Action<T3> oneT3Case, 
            Action<IEnumerable<T3>> enumerableT3Case,
            Action<T4> oneT4Case, 
            Action<IEnumerable<T4>> enumerableT4Case,
            Action<T5> oneT5Case, 
            Action<IEnumerable<T5>> enumerableT5Case,
            Action<T6> oneT6Case, 
            Action<IEnumerable<T6>> enumerableT6Case,
            Action<T7> oneT7Case, 
            Action<IEnumerable<T7>> enumerableT7Case,
            Action<T8> oneT8Case, 
            Action<IEnumerable<T8>> enumerableT8Case) 
        {
            switch(Index)
            {
                case -1: oneT0Case(t0); break; case  1: enumerableT0Case((IEnumerable<T0>)Many); break;
                case -2: oneT1Case(t1); break; case  2: enumerableT1Case((IEnumerable<T1>)Many); break;
                case -3: oneT2Case(t2); break; case  3: enumerableT2Case((IEnumerable<T2>)Many); break;
                case -4: oneT3Case(t3); break; case  4: enumerableT3Case((IEnumerable<T3>)Many); break;
                case -5: oneT4Case(t4); break; case  5: enumerableT4Case((IEnumerable<T4>)Many); break;
                case -6: oneT5Case(t5); break; case  6: enumerableT5Case((IEnumerable<T5>)Many); break;
                case -7: oneT6Case(t6); break; case  7: enumerableT6Case((IEnumerable<T6>)Many); break;
                case -8: oneT7Case(t7); break; case  8: enumerableT7Case((IEnumerable<T7>)Many); break;
                case -9: oneT8Case(t8); break; case  9: enumerableT8Case((IEnumerable<T8>)Many); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8>, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>, Task> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, Task> oneT0Case, Func<IEnumerable<T0>, Task> enumerableT0Case,
            Func<T1, Task> oneT1Case, Func<IEnumerable<T1>, Task> enumerableT1Case,
            Func<T2, Task> oneT2Case, Func<IEnumerable<T2>, Task> enumerableT2Case,
            Func<T3, Task> oneT3Case, Func<IEnumerable<T3>, Task> enumerableT3Case,
            Func<T4, Task> oneT4Case, Func<IEnumerable<T4>, Task> enumerableT4Case,
            Func<T5, Task> oneT5Case, Func<IEnumerable<T5>, Task> enumerableT5Case,
            Func<T6, Task> oneT6Case, Func<IEnumerable<T6>, Task> enumerableT6Case,
            Func<T7, Task> oneT7Case, Func<IEnumerable<T7>, Task> enumerableT7Case,
            Func<T8, Task> oneT8Case, Func<IEnumerable<T8>, Task> enumerableT8Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0), 1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1), 2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2), 3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3), 4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4), 5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5), 6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6), 7 => enumerableT6Case((IEnumerable<T6>)Many),
                -8 => oneT7Case(t7), 8 => enumerableT7Case((IEnumerable<T7>)Many),
                -9 => oneT8Case(t8), 9 => enumerableT8Case((IEnumerable<T8>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8>, CancellationToken, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>, CancellationToken, Task> enumerableCase, 
            CancellationToken ct)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, CancellationToken, Task> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task> enumerableT0Case, 
            Func<T1, CancellationToken, Task> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task> enumerableT1Case,
            Func<T2, CancellationToken, Task> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task> enumerableT2Case,
            Func<T3, CancellationToken, Task> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task> enumerableT3Case,
            Func<T4, CancellationToken, Task> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task> enumerableT4Case,
            Func<T5, CancellationToken, Task> oneT5Case, Func<IEnumerable<T5>, CancellationToken, Task> enumerableT5Case,
            Func<T6, CancellationToken, Task> oneT6Case, Func<IEnumerable<T6>, CancellationToken, Task> enumerableT6Case,
            Func<T7, CancellationToken, Task> oneT7Case, Func<IEnumerable<T7>, CancellationToken, Task> enumerableT7Case,
            Func<T8, CancellationToken, Task> oneT8Case, Func<IEnumerable<T8>, CancellationToken, Task> enumerableT8Case,
            CancellationToken ct)
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct), 1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct), 2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct), 3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct), 4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct), 5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                -6 => oneT5Case(t5, ct), 6 => enumerableT5Case((IEnumerable<T5>)Many, ct),
                -7 => oneT6Case(t6, ct), 7 => enumerableT6Case((IEnumerable<T6>)Many, ct),
                -8 => oneT7Case(t7, ct), 8 => enumerableT7Case((IEnumerable<T7>)Many, ct),
                -9 => oneT8Case(t8, ct), 9 => enumerableT8Case((IEnumerable<T8>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
}

public readonly struct OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
    private sbyte Index { get; }
    // ReSharper disable InconsistentNaming
    private T0 t0 { get; } = default!;
    private T1 t1 { get; } = default!;
    private T2 t2 { get; } = default!;
    private T3 t3 { get; } = default!;
    private T4 t4 { get; } = default!;
    private T5 t5 { get; } = default!;
    private T6 t6 { get; } = default!;
    private T7 t7 { get; } = default!;
    private T8 t8 { get; } = default!;
    private T9 t9 { get; } = default!;
    // ReSharper restore InconsistentNaming
    private IEnumerable Many { get; } = null!;

    #region One T0
        public bool TryGetAsOneT0(out T0 value) {
            if (Index == -1) { value = t0; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T0 asOne) => new(asOne);
        public T0 AsOneT0 => Index == -1 ? t0 : throw new InvalidCastException();
        public bool IsOneT0 => Index == -1;
        public OneOrEnumerable(T0 asOne) { Index = -1; t0 = asOne; }
    #endregion
    
    #region One T1
        public bool TryGetAsOneT1(out T1 value) {
            if (Index == -2) { value = t1; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 asOne) => new(asOne);
        public T1 AsOneT1 => Index == -2 ? t1 : throw new InvalidCastException();
        public bool IsOneT1 => Index == -2;
        public OneOrEnumerable(T1 asOne) { Index = -2; t1 = asOne; }
    #endregion
    
    #region One T2
        public bool TryGetAsOneT2(out T2 value) {
            if (Index == -3) { value = t2; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T2 asOne) => new(asOne);
        public T2 AsOneT2 => Index == -3 ? t2 : throw new InvalidCastException();
        public bool IsOneT2 => Index == -3;
        public OneOrEnumerable(T2 asOne) { Index = -3; t2 = asOne; }
    #endregion
    
    #region One T3
        public bool TryGetAsOneT3(out T3 value) {
            if (Index == -4) { value = t3; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T3 asOne) => new(asOne);
        public T3 AsOneT3 => Index == -4 ? t3 : throw new InvalidCastException();
        public bool IsOneT3 => Index == -4;
        public OneOrEnumerable(T3 asOne) { Index = -4; t3 = asOne; }
    #endregion
    
    #region One T4
        public bool TryGetAsOneT4(out T4 value) {
            if (Index == -5) { value = t4; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T4 asOne) => new(asOne);
        public T4 AsOneT4 => Index == -5 ? t4 : throw new InvalidCastException();
        public bool IsOneT4 => Index == -5;
        public OneOrEnumerable(T4 asOne) { Index = -5; t4 = asOne; }
    #endregion
    
    #region One T5
        public bool TryGetAsOneT5(out T5 value) {
            if (Index == -6) { value = t5; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T5 asOne) => new(asOne);
        public T5 AsOneT5 => Index == -6 ? t5 : throw new InvalidCastException();
        public bool IsOneT5 => Index == -6;
        public OneOrEnumerable(T5 asOne) { Index = -6; t5 = asOne; }
    #endregion
    
    #region One T6
        public bool TryGetAsOneT6(out T6 value) {
            if (Index == -7) { value = t6; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T6 asOne) => new(asOne);
        public T6 AsOneT6 => Index == -7 ? t6 : throw new InvalidCastException();
        public bool IsOneT6 => Index == -7;
        public OneOrEnumerable(T6 asOne) { Index = -7; t6 = asOne; }
    #endregion
    
    #region One T7
        public bool TryGetAsOneT7(out T7 value) {
            if (Index == -8) { value = t7; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T7 asOne) => new(asOne);
        public T7 AsOneT7 => Index == -8 ? t7 : throw new InvalidCastException();
        public bool IsOneT7 => Index == -8;
        public OneOrEnumerable(T7 asOne) { Index = -8; t7 = asOne; }
    #endregion
    
    #region One T8
        public bool TryGetAsOneT8(out T8 value) {
            if (Index == -9) { value = t8; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T8 asOne) => new(asOne);
        public T8 AsOneT8 => Index == -9 ? t8 : throw new InvalidCastException();
        public bool IsOneT8 => Index == -9;
        public OneOrEnumerable(T8 asOne) { Index = -9; t8 = asOne; }
    #endregion
    
    #region One T9
        public bool TryGetAsOneT9(out T9 value) {
            if (Index == -10) { value = t9; return true; }
            value = default!; return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(T9 asOne) => new(asOne);
        public T9 AsOneT9 => Index == -10 ? t9 : throw new InvalidCastException();
        public bool IsOneT9 => Index == -10;
        public OneOrEnumerable(T9 asOne) { Index = -10; t9 = asOne; }
    #endregion
    
    #region One
        public bool TryGetAsOne(out Union<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> value) {
            switch (Index)
            {
                case -1: value = t0; return true;
                case -2: value = t1; return true;
                case -3: value = t2; return true;
                case -4: value = t3; return true;
                case -5: value = t4; return true;
                case -6: value = t5; return true;
                case -7: value = t6; return true;
                case -8: value = t7; return true;
                case -9: value = t8; return true;
                case -10: value = t9; return true;
                default: value = default!; return false;
            }
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Union<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> asOne) => new(asOne);
        public Union<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> AsOne => Index switch {
            -1 => t0, -2 => t1, -3 => t2, -4 => t3, -5 => t4,
            -6 => t5, -7 => t6, -8 => t7, -9 => t8, -10 => t9,
            _ => throw new InvalidCastException()
        };
        public bool IsOne => Index < 0;
        public OneOrEnumerable(Union<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> asOne)
        {
            switch (asOne)
            {
                case {IsT0: true, AsT0: var value}: Index = -1; t0 = value; break;
                case {IsT1: true, AsT1: var value}: Index = -2; t1 = value; break;
                case {IsT2: true, AsT2: var value}: Index = -3; t2 = value; break;
                case {IsT3: true, AsT3: var value}: Index = -4; t3 = value; break;
                case {IsT4: true, AsT4: var value}: Index = -5; t4 = value; break;
                case {IsT5: true, AsT5: var value}: Index = -6; t5 = value; break;
                case {IsT6: true, AsT6: var value}: Index = -7; t6 = value; break;
                case {IsT7: true, AsT7: var value}: Index = -8; t7 = value; break;
                case {IsT8: true, AsT8: var value}: Index = -9; t8 = value; break;
                case {IsT9: true, AsT9: var value}: Index = -10; t9 = value; break;
                default: throw new InvalidCastException();
            }
        }
    #endregion
    
    #region Enumerable T0
        public bool TryGetAsEnumerableT0([NotNullWhen(true)] out IEnumerable<T0>? value) {
            if (Index == 1) { value = (IEnumerable<T0>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T0> AsEnumerableT0 => Index == 1 ? (IEnumerable<T0>)Many : throw new InvalidCastException();
        public bool IsEnumerableT0 => Index == 1;
        public OneOrEnumerable(IEnumerable<T0> asEnumerable) { Index = 1; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T1
        public bool TryGetAsEnumerableT1([NotNullWhen(true)] out IEnumerable<T1>? value) {
            if (Index == 2) { value = (IEnumerable<T1>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T1> AsEnumerableT1 => Index == 2 ? (IEnumerable<T1>)Many : throw new InvalidCastException();
        public bool IsEnumerableT1 => Index == 2;
        public OneOrEnumerable(IEnumerable<T1> asEnumerable) { Index = 2; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T2
        public bool TryGetAsEnumerableT2([NotNullWhen(true)] out IEnumerable<T2>? value) {
            if (Index == 3) { value = (IEnumerable<T2>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T2> AsEnumerableT2 => Index == 3 ? (IEnumerable<T2>)Many : throw new InvalidCastException();
        public bool IsEnumerableT2 => Index == 3;
        public OneOrEnumerable(IEnumerable<T2> asEnumerable) { Index = 3; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T3
        public bool TryGetAsEnumerableT3([NotNullWhen(true)] out IEnumerable<T3>? value) {
            if (Index == 4) { value = (IEnumerable<T3>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T3> AsEnumerableT3 => Index == 4 ? (IEnumerable<T3>)Many : throw new InvalidCastException();
        public bool IsEnumerableT3 => Index == 4;
        public OneOrEnumerable(IEnumerable<T3> asEnumerable) { Index = 4; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T4
        public bool TryGetAsEnumerableT4([NotNullWhen(true)] out IEnumerable<T4>? value) {
            if (Index == 5) { value = (IEnumerable<T4>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T4> AsEnumerableT4 => Index == 5 ? (IEnumerable<T4>)Many : throw new InvalidCastException();
        public bool IsEnumerableT4 => Index == 5;
        public OneOrEnumerable(IEnumerable<T4> asEnumerable) { Index = 5; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T5
        public bool TryGetAsEnumerableT5([NotNullWhen(true)] out IEnumerable<T5>? value) {
            if (Index == 6) { value = (IEnumerable<T5>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T5> AsEnumerableT5 => Index == 6 ? (IEnumerable<T5>)Many : throw new InvalidCastException();
        public bool IsEnumerableT5 => Index == 6;
        public OneOrEnumerable(IEnumerable<T5> asEnumerable) { Index = 6; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T6
        public bool TryGetAsEnumerableT6([NotNullWhen(true)] out IEnumerable<T6>? value) {
            if (Index == 7) { value = (IEnumerable<T6>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T6> AsEnumerableT6 => Index == 7 ? (IEnumerable<T6>)Many : throw new InvalidCastException();
        public bool IsEnumerableT6 => Index == 7;
        public OneOrEnumerable(IEnumerable<T6> asEnumerable) { Index = 7; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T7
        public bool TryGetAsEnumerableT7([NotNullWhen(true)] out IEnumerable<T7>? value) {
            if (Index == 8) { value = (IEnumerable<T7>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T7> AsEnumerableT7 => Index == 8 ? (IEnumerable<T7>)Many : throw new InvalidCastException();
        public bool IsEnumerableT7 => Index == 8;
        public OneOrEnumerable(IEnumerable<T7> asEnumerable) { Index = 8; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T8
        public bool TryGetAsEnumerableT8([NotNullWhen(true)] out IEnumerable<T8>? value) {
            if (Index == 9) { value = (IEnumerable<T8>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T8> AsEnumerableT8 => Index == 9 ? (IEnumerable<T8>)Many : throw new InvalidCastException();
        public bool IsEnumerableT8 => Index == 9;
        public OneOrEnumerable(IEnumerable<T8> asEnumerable) { Index = 9; Many = asEnumerable; }
    #endregion
    
    #region Enumerable T9
        public bool TryGetAsEnumerableT9([NotNullWhen(true)] out IEnumerable<T9>? value) {
            if (Index == 10) { value = (IEnumerable<T9>)Many; return true; }
            value = null!; return false;
        }
        public IEnumerable<T9> AsEnumerableT9 => Index == 10 ? (IEnumerable<T9>)Many : throw new InvalidCastException();
        public bool IsEnumerableT9 => Index == 10;
        public OneOrEnumerable(IEnumerable<T9> asEnumerable) { Index = 10; Many = asEnumerable; }
    #endregion
    
    #region Enumerable
        public bool TryGetAsEnumerable(out UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> value) {
            if (Index is 1 or 2 or 3 or 4 or 5 or 6 or 7 or 8 or 9 or 10)
            {
                value = new((byte)Index, Many);
                return true;
            }
            value = default;
            return false;
        }
        public static implicit operator OneOrEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> asEnumerable) => new(asEnumerable);
        public UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> AsEnumerable => Index > 0 ? new((byte)Index, Many) : throw new InvalidCastException($"Union does not contain an Enumerable");
        public bool IsEnumerable => Index > 0;
        public OneOrEnumerable(UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> asEnumerable): this()
        {
            Index = (sbyte)asEnumerable.Index;
            Many = (IEnumerable)asEnumerable.Value;
        }
    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>, TOutput> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>, TOutput> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public TOutput Match<TOutput>(
            Func<T0, TOutput> oneT0Case, Func<IEnumerable<T0>, TOutput> enumerableT0Case,
            Func<T1, TOutput> oneT1Case, Func<IEnumerable<T1>, TOutput> enumerableT1Case,
            Func<T2, TOutput> oneT2Case, Func<IEnumerable<T2>, TOutput> enumerableT2Case,
            Func<T3, TOutput> oneT3Case, Func<IEnumerable<T3>, TOutput> enumerableT3Case,
            Func<T4, TOutput> oneT4Case, Func<IEnumerable<T4>, TOutput> enumerableT4Case,
            Func<T5, TOutput> oneT5Case, Func<IEnumerable<T5>, TOutput> enumerableT5Case,
            Func<T6, TOutput> oneT6Case, Func<IEnumerable<T6>, TOutput> enumerableT6Case,
            Func<T7, TOutput> oneT7Case, Func<IEnumerable<T7>, TOutput> enumerableT7Case,
            Func<T8, TOutput> oneT8Case, Func<IEnumerable<T8>, TOutput> enumerableT8Case,
            Func<T9, TOutput> oneT9Case, Func<IEnumerable<T9>, TOutput> enumerableT9Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0),   1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),   2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),   3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),   4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4),   5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5),   6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6),   7 => enumerableT6Case((IEnumerable<T6>)Many),
                -8 => oneT7Case(t7),   8 => enumerableT7Case((IEnumerable<T7>)Many),
                -9 => oneT8Case(t8),   9 => enumerableT8Case((IEnumerable<T8>)Many),
                -10 => oneT9Case(t9), 10 => enumerableT9Case((IEnumerable<T9>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>, Task<TOutput>> enumerableCase) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, Task<TOutput>> enumerableT0Case,
            Func<T1, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, Task<TOutput>> enumerableT1Case,
            Func<T2, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, Task<TOutput>> enumerableT2Case,
            Func<T3, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, Task<TOutput>> enumerableT3Case,
            Func<T4, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, Task<TOutput>> enumerableT4Case,
            Func<T5, Task<TOutput>> oneT5Case, Func<IEnumerable<T5>, Task<TOutput>> enumerableT5Case,
            Func<T6, Task<TOutput>> oneT6Case, Func<IEnumerable<T6>, Task<TOutput>> enumerableT6Case,
            Func<T7, Task<TOutput>> oneT7Case, Func<IEnumerable<T7>, Task<TOutput>> enumerableT7Case,
            Func<T8, Task<TOutput>> oneT8Case, Func<IEnumerable<T8>, Task<TOutput>> enumerableT8Case,
            Func<T9, Task<TOutput>> oneT9Case, Func<IEnumerable<T9>, Task<TOutput>> enumerableT9Case) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0),   1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),   2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),   3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),   4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4),   5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5),   6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6),   7 => enumerableT6Case((IEnumerable<T6>)Many),
                -8 => oneT7Case(t7),   8 => enumerableT7Case((IEnumerable<T7>)Many),
                -9 => oneT8Case(t8),   9 => enumerableT8Case((IEnumerable<T8>)Many),
                -10 => oneT9Case(t9), 10 => enumerableT9Case((IEnumerable<T9>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>, CancellationToken, Task<TOutput>> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>, CancellationToken, Task<TOutput>> enumerableCase, 
            CancellationToken ct) 
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task<TOutput> MatchAsync<TOutput>(
            Func<T0, CancellationToken, Task<TOutput>> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> enumerableT0Case, 
            Func<T1, CancellationToken, Task<TOutput>> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> enumerableT1Case,
            Func<T2, CancellationToken, Task<TOutput>> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> enumerableT2Case,
            Func<T3, CancellationToken, Task<TOutput>> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> enumerableT3Case,
            Func<T4, CancellationToken, Task<TOutput>> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> enumerableT4Case,
            Func<T5, CancellationToken, Task<TOutput>> oneT5Case, Func<IEnumerable<T5>, CancellationToken, Task<TOutput>> enumerableT5Case,
            Func<T6, CancellationToken, Task<TOutput>> oneT6Case, Func<IEnumerable<T6>, CancellationToken, Task<TOutput>> enumerableT6Case,
            Func<T7, CancellationToken, Task<TOutput>> oneT7Case, Func<IEnumerable<T7>, CancellationToken, Task<TOutput>> enumerableT7Case,
            Func<T8, CancellationToken, Task<TOutput>> oneT8Case, Func<IEnumerable<T8>, CancellationToken, Task<TOutput>> enumerableT8Case,
            Func<T9, CancellationToken, Task<TOutput>> oneT9Case, Func<IEnumerable<T9>, CancellationToken, Task<TOutput>> enumerableT9Case,
            CancellationToken ct) 
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct),   1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct),   2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct),   3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct),   4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct),   5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                -6 => oneT5Case(t5, ct),   6 => enumerableT5Case((IEnumerable<T5>)Many, ct),
                -7 => oneT6Case(t6, ct),   7 => enumerableT6Case((IEnumerable<T6>)Many, ct),
                -8 => oneT7Case(t7, ct),   8 => enumerableT7Case((IEnumerable<T7>)Many, ct),
                -9 => oneT8Case(t8, ct),   9 => enumerableT8Case((IEnumerable<T8>)Many, ct),
                -10 => oneT9Case(t9, ct), 10 => enumerableT9Case((IEnumerable<T9>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>> oneCase, 
            Action<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>> enumerableCase) 
        {
            switch(Index)
            {
                case < 0: oneCase(AsOne); break;
                case > 0: enumerableCase(AsEnumerable); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }
        
        public void Switch(
            Action<T0> oneT0Case, 
            Action<IEnumerable<T0>> enumerableT0Case,
            Action<T1> oneT1Case, 
            Action<IEnumerable<T1>> enumerableT1Case,
            Action<T2> oneT2Case, 
            Action<IEnumerable<T2>> enumerableT2Case,
            Action<T3> oneT3Case, 
            Action<IEnumerable<T3>> enumerableT3Case,
            Action<T4> oneT4Case, 
            Action<IEnumerable<T4>> enumerableT4Case,
            Action<T5> oneT5Case, 
            Action<IEnumerable<T5>> enumerableT5Case,
            Action<T6> oneT6Case, 
            Action<IEnumerable<T6>> enumerableT6Case,
            Action<T7> oneT7Case, 
            Action<IEnumerable<T7>> enumerableT7Case,
            Action<T8> oneT8Case, 
            Action<IEnumerable<T8>> enumerableT8Case,
            Action<T9> oneT9Case, 
            Action<IEnumerable<T9>> enumerableT9Case) 
        {
            switch(Index)
            {
                case -1: oneT0Case(t0); break;  case  1: enumerableT0Case((IEnumerable<T0>)Many); break;
                case -2: oneT1Case(t1); break;  case  2: enumerableT1Case((IEnumerable<T1>)Many); break;
                case -3: oneT2Case(t2); break;  case  3: enumerableT2Case((IEnumerable<T2>)Many); break;
                case -4: oneT3Case(t3); break;  case  4: enumerableT3Case((IEnumerable<T3>)Many); break;
                case -5: oneT4Case(t4); break;  case  5: enumerableT4Case((IEnumerable<T4>)Many); break;
                case -6: oneT5Case(t5); break;  case  6: enumerableT5Case((IEnumerable<T5>)Many); break;
                case -7: oneT6Case(t6); break;  case  7: enumerableT6Case((IEnumerable<T6>)Many); break;
                case -8: oneT7Case(t7); break;  case  8: enumerableT7Case((IEnumerable<T7>)Many); break;
                case -9: oneT8Case(t8); break;  case  9: enumerableT8Case((IEnumerable<T8>)Many); break;
                case -10: oneT9Case(t9); break; case 10: enumerableT9Case((IEnumerable<T9>)Many); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>, Task> enumerableCase)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne),
                > 0 => enumerableCase(AsEnumerable),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, Task> oneT0Case, Func<IEnumerable<T0>, Task> enumerableT0Case,
            Func<T1, Task> oneT1Case, Func<IEnumerable<T1>, Task> enumerableT1Case,
            Func<T2, Task> oneT2Case, Func<IEnumerable<T2>, Task> enumerableT2Case,
            Func<T3, Task> oneT3Case, Func<IEnumerable<T3>, Task> enumerableT3Case,
            Func<T4, Task> oneT4Case, Func<IEnumerable<T4>, Task> enumerableT4Case,
            Func<T5, Task> oneT5Case, Func<IEnumerable<T5>, Task> enumerableT5Case,
            Func<T6, Task> oneT6Case, Func<IEnumerable<T6>, Task> enumerableT6Case,
            Func<T7, Task> oneT7Case, Func<IEnumerable<T7>, Task> enumerableT7Case,
            Func<T8, Task> oneT8Case, Func<IEnumerable<T8>, Task> enumerableT8Case,
            Func<T9, Task> oneT9Case, Func<IEnumerable<T9>, Task> enumerableT9Case)
        {
            return Index switch
            {
                -1 => oneT0Case(t0),   1 => enumerableT0Case((IEnumerable<T0>)Many),
                -2 => oneT1Case(t1),   2 => enumerableT1Case((IEnumerable<T1>)Many),
                -3 => oneT2Case(t2),   3 => enumerableT2Case((IEnumerable<T2>)Many),
                -4 => oneT3Case(t3),   4 => enumerableT3Case((IEnumerable<T3>)Many),
                -5 => oneT4Case(t4),   5 => enumerableT4Case((IEnumerable<T4>)Many),
                -6 => oneT5Case(t5),   6 => enumerableT5Case((IEnumerable<T5>)Many),
                -7 => oneT6Case(t6),   7 => enumerableT6Case((IEnumerable<T6>)Many),
                -8 => oneT7Case(t7),   8 => enumerableT7Case((IEnumerable<T7>)Many),
                -9 => oneT8Case(t8),   9 => enumerableT8Case((IEnumerable<T8>)Many),
                -10 => oneT9Case(t9), 10 => enumerableT9Case((IEnumerable<T9>)Many),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<Union<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>, CancellationToken, Task> oneCase, 
            Func<UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>, CancellationToken, Task> enumerableCase, 
            CancellationToken ct)
        {
            return Index switch
            {
                < 0 => oneCase(AsOne, ct),
                > 0 => enumerableCase(AsEnumerable, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
        
        public Task SwitchAsync(
            Func<T0, CancellationToken, Task> oneT0Case, Func<IEnumerable<T0>, CancellationToken, Task> enumerableT0Case, 
            Func<T1, CancellationToken, Task> oneT1Case, Func<IEnumerable<T1>, CancellationToken, Task> enumerableT1Case,
            Func<T2, CancellationToken, Task> oneT2Case, Func<IEnumerable<T2>, CancellationToken, Task> enumerableT2Case,
            Func<T3, CancellationToken, Task> oneT3Case, Func<IEnumerable<T3>, CancellationToken, Task> enumerableT3Case,
            Func<T4, CancellationToken, Task> oneT4Case, Func<IEnumerable<T4>, CancellationToken, Task> enumerableT4Case,
            Func<T5, CancellationToken, Task> oneT5Case, Func<IEnumerable<T5>, CancellationToken, Task> enumerableT5Case,
            Func<T6, CancellationToken, Task> oneT6Case, Func<IEnumerable<T6>, CancellationToken, Task> enumerableT6Case,
            Func<T7, CancellationToken, Task> oneT7Case, Func<IEnumerable<T7>, CancellationToken, Task> enumerableT7Case,
            Func<T8, CancellationToken, Task> oneT8Case, Func<IEnumerable<T8>, CancellationToken, Task> enumerableT8Case,
            Func<T9, CancellationToken, Task> oneT9Case, Func<IEnumerable<T9>, CancellationToken, Task> enumerableT9Case,
            CancellationToken ct)
        {
            return Index switch
            {
                -1 => oneT0Case(t0, ct),   1 => enumerableT0Case((IEnumerable<T0>)Many, ct),
                -2 => oneT1Case(t1, ct),   2 => enumerableT1Case((IEnumerable<T1>)Many, ct),
                -3 => oneT2Case(t2, ct),   3 => enumerableT2Case((IEnumerable<T2>)Many, ct),
                -4 => oneT3Case(t3, ct),   4 => enumerableT3Case((IEnumerable<T3>)Many, ct),
                -5 => oneT4Case(t4, ct),   5 => enumerableT4Case((IEnumerable<T4>)Many, ct),
                -6 => oneT5Case(t5, ct),   6 => enumerableT5Case((IEnumerable<T5>)Many, ct),
                -7 => oneT6Case(t6, ct),   7 => enumerableT6Case((IEnumerable<T6>)Many, ct),
                -8 => oneT7Case(t7, ct),   8 => enumerableT7Case((IEnumerable<T7>)Many, ct),
                -9 => oneT8Case(t8, ct),   9 => enumerableT8Case((IEnumerable<T8>)Many, ct),
                -10 => oneT9Case(t9, ct), 10 => enumerableT9Case((IEnumerable<T9>)Many, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
}