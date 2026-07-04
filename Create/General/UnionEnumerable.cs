using System.Collections;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable MemberCanBePrivate.Global

namespace Create.General;

public readonly struct UnionEnumerable<T0, T1> 
    where T0: allows ref struct
    where T1: allows ref struct
{
    public byte Index { get; }
    private IEnumerable Enumerable { get; }

    internal UnionEnumerable(byte index, IEnumerable enumerable)
    {
        Index = index;
        Enumerable = enumerable;
    }
    
    #region T0

        public bool TryGetAsT0([NotNullWhen(true)] out IEnumerable<T0>? value) {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T0> asT0)
        {
            Index = 1;
            Enumerable = asT0;
        }
        
        public IEnumerable<T0> AsT0 => Index == 1 ? (IEnumerable<T0>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T1

        public bool TryGetAsT1([NotNullWhen(true)] out IEnumerable<T1>? value) {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T1> asT1)
        {
            Index = 2;
            Enumerable = asT1;
        }
            
        public IEnumerable<T1> AsT1 => Index == 2 ? (IEnumerable<T1>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<IEnumerable<T0>, TOutput> t0Case, 
            Func<IEnumerable<T1>, TOutput> t1Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, Task<TOutput>> t1Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> t0Case,
            Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> t1Case,
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<IEnumerable<T0>> t0Case, 
            Action<IEnumerable<T1>> t1Case)
        {
            switch(Index)
            {
                case 1: t0Case(AsT0); break;
                case 2: t1Case(AsT1); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, Task> t0Case, 
            Func<IEnumerable<T1>, Task> t1Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, CancellationToken, Task> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task> t1Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
    
    #region Utility Properties
    
        public bool IsT0 => Index == 1;
        public bool IsT1 => Index == 2;
        
        public object Value => Enumerable;
    
    #endregion
    
}

public readonly struct UnionEnumerable<T0, T1, T2> 
    where T0 : allows ref struct
    where T1 : allows ref struct
    where T2 : allows ref struct
{
    public byte Index { get; }
    private IEnumerable Enumerable { get; }

    internal UnionEnumerable(byte index, IEnumerable enumerable)
    {
        Index = index;
        Enumerable = enumerable;
    }
    
    #region T0

        public bool TryGetAsT0([NotNullWhen(true)] out IEnumerable<T0>? value)
        {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T0> asT0)
        {
            Index = 1;
            Enumerable = asT0;
        }
        
        public IEnumerable<T0> AsT0 => Index == 1 ? (IEnumerable<T0>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T1

        public bool TryGetAsT1([NotNullWhen(true)] out IEnumerable<T1>? value)
        {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T1> asT1)
        {
            Index = 2;
            Enumerable = asT1;
        }
            
        public IEnumerable<T1> AsT1 => Index == 2 ? (IEnumerable<T1>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T2

        public bool TryGetAsT2([NotNullWhen(true)] out IEnumerable<T2>? value)
        {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T2> asT2)
        {
            Index = 3;
            Enumerable = asT2;
        }
        
        public IEnumerable<T2> AsT2 => Index == 3 ? (IEnumerable<T2>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<IEnumerable<T0>, TOutput> t0Case, 
            Func<IEnumerable<T1>, TOutput> t1Case,
            Func<IEnumerable<T2>, TOutput> t2Case)
        {
            switch (Index)
            {
                case 1: return t0Case(AsT0);
                case 2: return t1Case(AsT1);
                case 3: return t2Case(AsT2);
            }
            throw new ArgumentException("Union does not contain a value");
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, Task<TOutput>> t2Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> t2Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<IEnumerable<T0>> t0Case, 
            Action<IEnumerable<T1>> t1Case,
            Action<IEnumerable<T2>> t2Case)
        {
            switch (Index)
            {
                case 1: t0Case(AsT0); break;
                case 2: t1Case(AsT1); break;
                case 3: t2Case(AsT2); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, Task> t0Case, 
            Func<IEnumerable<T1>, Task> t1Case,
            Func<IEnumerable<T2>, Task> t2Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, CancellationToken, Task> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task> t2Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
    
    #region Utility Properties
    
        public bool IsT0 => Index == 1;
        public bool IsT1 => Index == 2;
        public bool IsT2 => Index == 3;
        
        public object Value => Enumerable;
    
    #endregion
}

public readonly struct UnionEnumerable<T0, T1, T2, T3> 
    where T0 : allows ref struct
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
{
    public byte Index { get; }
    private IEnumerable Enumerable { get; }

    internal UnionEnumerable(byte index, IEnumerable enumerable)
    {
        Index = index;
        Enumerable = enumerable;
    }
    
    #region T0

        public bool TryGetAsT0([NotNullWhen(true)] out IEnumerable<T0>? value)
        {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T0> asT0)
        {
            Index = 1;
            Enumerable = asT0;
        }
        
        public IEnumerable<T0> AsT0 => Index == 1 ? (IEnumerable<T0>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T1

        public bool TryGetAsT1([NotNullWhen(true)] out IEnumerable<T1>? value)
        {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T1> asT1)
        {
            Index = 2;
            Enumerable = asT1;
        }
            
        public IEnumerable<T1> AsT1 => Index == 2 ? (IEnumerable<T1>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T2

        public bool TryGetAsT2([NotNullWhen(true)] out IEnumerable<T2>? value)
        {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T2> asT2)
        {
            Index = 3;
            Enumerable = asT2;
        }
        
        public IEnumerable<T2> AsT2 => Index == 3 ? (IEnumerable<T2>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T3

        public bool TryGetAsT3([NotNullWhen(true)] out IEnumerable<T3>? value)
        {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T3> asT3)
        {
            Index = 4;
            Enumerable = asT3;
        }
        
        public IEnumerable<T3> AsT3 => Index == 4 ? (IEnumerable<T3>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<IEnumerable<T0>, TOutput> t0Case, 
            Func<IEnumerable<T1>, TOutput> t1Case,
            Func<IEnumerable<T2>, TOutput> t2Case,
            Func<IEnumerable<T3>, TOutput> t3Case)
        {
            switch (Index)
            {
                case 1: return t0Case(AsT0);
                case 2: return t1Case(AsT1);
                case 3: return t2Case(AsT2);
                case 4: return t3Case(AsT3);
            }
            throw new ArgumentException("Union does not contain a value");
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, Task<TOutput>> t3Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> t3Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<IEnumerable<T0>> t0Case, 
            Action<IEnumerable<T1>> t1Case,
            Action<IEnumerable<T2>> t2Case,
            Action<IEnumerable<T3>> t3Case)
        {
            switch (Index)
            {
                case 1: t0Case(AsT0); break;
                case 2: t1Case(AsT1); break;
                case 3: t2Case(AsT2); break;
                case 4: t3Case(AsT3); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, Task> t0Case, 
            Func<IEnumerable<T1>, Task> t1Case,
            Func<IEnumerable<T2>, Task> t2Case,
            Func<IEnumerable<T3>, Task> t3Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, CancellationToken, Task> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task> t3Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
    
    #region Utility Properties
    
        public bool IsT0 => Index == 1;
        public bool IsT1 => Index == 2;
        public bool IsT2 => Index == 3;
        public bool IsT3 => Index == 4;
        
        public object Value => Enumerable;
    
    #endregion
}

public readonly struct UnionEnumerable<T0, T1, T2, T3, T4> 
    where T0 : allows ref struct
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
{
    public byte Index { get; }
    private IEnumerable Enumerable { get; }

    internal UnionEnumerable(byte index, IEnumerable enumerable)
    {
        Index = index;
        Enumerable = enumerable;
    }
    
    #region T0

        public bool TryGetAsT0([NotNullWhen(true)] out IEnumerable<T0>? value)
        {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T0> asT0)
        {
            Index = 1;
            Enumerable = asT0;
        }
        
        public IEnumerable<T0> AsT0 => Index == 1 ? (IEnumerable<T0>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T1

        public bool TryGetAsT1([NotNullWhen(true)] out IEnumerable<T1>? value)
        {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T1> asT1)
        {
            Index = 2;
            Enumerable = asT1;
        }
            
        public IEnumerable<T1> AsT1 => Index == 2 ? (IEnumerable<T1>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T2

        public bool TryGetAsT2([NotNullWhen(true)] out IEnumerable<T2>? value)
        {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T2> asT2)
        {
            Index = 3;
            Enumerable = asT2;
        }
        
        public IEnumerable<T2> AsT2 => Index == 3 ? (IEnumerable<T2>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T3

        public bool TryGetAsT3([NotNullWhen(true)] out IEnumerable<T3>? value)
        {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T3> asT3)
        {
            Index = 4;
            Enumerable = asT3;
        }
        
        public IEnumerable<T3> AsT3 => Index == 4 ? (IEnumerable<T3>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T4

        public bool TryGetAsT4([NotNullWhen(true)] out IEnumerable<T4>? value)
        {
            if (Index == 5)
            {
                value = (IEnumerable<T4>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T4> asT4)
        {
            Index = 5;
            Enumerable = asT4;
        }
        
        public IEnumerable<T4> AsT4 => Index == 5 ? (IEnumerable<T4>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<IEnumerable<T0>, TOutput> t0Case, 
            Func<IEnumerable<T1>, TOutput> t1Case,
            Func<IEnumerable<T2>, TOutput> t2Case,
            Func<IEnumerable<T3>, TOutput> t3Case,
            Func<IEnumerable<T4>, TOutput> t4Case)
        {
            switch (Index)
            {
                case 1: return t0Case(AsT0);
                case 2: return t1Case(AsT1);
                case 3: return t2Case(AsT2);
                case 4: return t3Case(AsT3);
                case 5: return t4Case(AsT4);
            }
            throw new ArgumentException("Union does not contain a value");
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, Task<TOutput>> t4Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> t4Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<IEnumerable<T0>> t0Case, 
            Action<IEnumerable<T1>> t1Case,
            Action<IEnumerable<T2>> t2Case,
            Action<IEnumerable<T3>> t3Case,
            Action<IEnumerable<T4>> t4Case)
        {
            switch (Index)
            {
                case 1: t0Case(AsT0); break;
                case 2: t1Case(AsT1); break;
                case 3: t2Case(AsT2); break;
                case 4: t3Case(AsT3); break;
                case 5: t4Case(AsT4); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, Task> t0Case, 
            Func<IEnumerable<T1>, Task> t1Case,
            Func<IEnumerable<T2>, Task> t2Case,
            Func<IEnumerable<T3>, Task> t3Case,
            Func<IEnumerable<T4>, Task> t4Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, CancellationToken, Task> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task> t4Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
    
    #region Utility Properties
    
        public bool IsT0 => Index == 1;
        public bool IsT1 => Index == 2;
        public bool IsT2 => Index == 3;
        public bool IsT3 => Index == 4;
        public bool IsT4 => Index == 5;
        
        public object Value => Enumerable;
    
    #endregion
}

public readonly struct UnionEnumerable<T0, T1, T2, T3, T4, T5> 
    where T0 : allows ref struct
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
    where T5 : allows ref struct
{
    public byte Index { get; }
    private IEnumerable Enumerable { get; }

    internal UnionEnumerable(byte index, IEnumerable enumerable)
    {
        Index = index;
        Enumerable = enumerable;
    }
    
    #region T0

        public bool TryGetAsT0([NotNullWhen(true)] out IEnumerable<T0>? value)
        {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T0> asT0)
        {
            Index = 1;
            Enumerable = asT0;
        }
        
        public IEnumerable<T0> AsT0 => Index == 1 ? (IEnumerable<T0>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T1

        public bool TryGetAsT1([NotNullWhen(true)] out IEnumerable<T1>? value)
        {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T1> asT1)
        {
            Index = 2;
            Enumerable = asT1;
        }
            
        public IEnumerable<T1> AsT1 => Index == 2 ? (IEnumerable<T1>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T2

        public bool TryGetAsT2([NotNullWhen(true)] out IEnumerable<T2>? value)
        {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T2> asT2)
        {
            Index = 3;
            Enumerable = asT2;
        }
        
        public IEnumerable<T2> AsT2 => Index == 3 ? (IEnumerable<T2>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T3

        public bool TryGetAsT3([NotNullWhen(true)] out IEnumerable<T3>? value)
        {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T3> asT3)
        {
            Index = 4;
            Enumerable = asT3;
        }
        
        public IEnumerable<T3> AsT3 => Index == 4 ? (IEnumerable<T3>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T4

        public bool TryGetAsT4([NotNullWhen(true)] out IEnumerable<T4>? value)
        {
            if (Index == 5)
            {
                value = (IEnumerable<T4>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T4> asT4)
        {
            Index = 5;
            Enumerable = asT4;
        }
        
        public IEnumerable<T4> AsT4 => Index == 5 ? (IEnumerable<T4>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T5

        public bool TryGetAsT5([NotNullWhen(true)] out IEnumerable<T5>? value)
        {
            if (Index == 6)
            {
                value = (IEnumerable<T5>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T5> asT5)
        {
            Index = 6;
            Enumerable = asT5;
        }
        
        public IEnumerable<T5> AsT5 => Index == 6 ? (IEnumerable<T5>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<IEnumerable<T0>, TOutput> t0Case, 
            Func<IEnumerable<T1>, TOutput> t1Case,
            Func<IEnumerable<T2>, TOutput> t2Case,
            Func<IEnumerable<T3>, TOutput> t3Case,
            Func<IEnumerable<T4>, TOutput> t4Case,
            Func<IEnumerable<T5>, TOutput> t5Case)
        {
            switch (Index)
            {
                case 1: return t0Case(AsT0);
                case 2: return t1Case(AsT1);
                case 3: return t2Case(AsT2);
                case 4: return t3Case(AsT3);
                case 5: return t4Case(AsT4);
                case 6: return t5Case(AsT5);
            }
            throw new ArgumentException("Union does not contain a value");
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, Task<TOutput>> t4Case,
            Func<IEnumerable<T5>, Task<TOutput>> t5Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                6 => t5Case(AsT5),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> t4Case,
            Func<IEnumerable<T5>, CancellationToken, Task<TOutput>> t5Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                6 => t5Case(AsT5, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<IEnumerable<T0>> t0Case, 
            Action<IEnumerable<T1>> t1Case,
            Action<IEnumerable<T2>> t2Case,
            Action<IEnumerable<T3>> t3Case,
            Action<IEnumerable<T4>> t4Case,
            Action<IEnumerable<T5>> t5Case)
        {
            switch (Index)
            {
                case 1: t0Case(AsT0); break;
                case 2: t1Case(AsT1); break;
                case 3: t2Case(AsT2); break;
                case 4: t3Case(AsT3); break;
                case 5: t4Case(AsT4); break;
                case 6: t5Case(AsT5); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, Task> t0Case, 
            Func<IEnumerable<T1>, Task> t1Case,
            Func<IEnumerable<T2>, Task> t2Case,
            Func<IEnumerable<T3>, Task> t3Case,
            Func<IEnumerable<T4>, Task> t4Case,
            Func<IEnumerable<T5>, Task> t5Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                6 => t5Case(AsT5),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, CancellationToken, Task> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task> t4Case,
            Func<IEnumerable<T5>, CancellationToken, Task> t5Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                6 => t5Case(AsT5, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
    
    #region Utility Properties
    
        public bool IsT0 => Index == 1;
        public bool IsT1 => Index == 2;
        public bool IsT2 => Index == 3;
        public bool IsT3 => Index == 4;
        public bool IsT4 => Index == 5;
        public bool IsT5 => Index == 6;
        
        public object Value => Enumerable;
    
    #endregion
}

public readonly struct UnionEnumerable<T0, T1, T2, T3, T4, T5, T6> 
    where T0 : allows ref struct
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
    where T5 : allows ref struct
    where T6 : allows ref struct
{
    public byte Index { get; }
    private IEnumerable Enumerable { get; }

    internal UnionEnumerable(byte index, IEnumerable enumerable)
    {
        Index = index;
        Enumerable = enumerable;
    }
    
    #region T0

        public bool TryGetAsT0([NotNullWhen(true)] out IEnumerable<T0>? value)
        {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T0> asT0)
        {
            Index = 1;
            Enumerable = asT0;
        }
        
        public IEnumerable<T0> AsT0 => Index == 1 ? (IEnumerable<T0>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T1

        public bool TryGetAsT1([NotNullWhen(true)] out IEnumerable<T1>? value)
        {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T1> asT1)
        {
            Index = 2;
            Enumerable = asT1;
        }
            
        public IEnumerable<T1> AsT1 => Index == 2 ? (IEnumerable<T1>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T2

        public bool TryGetAsT2([NotNullWhen(true)] out IEnumerable<T2>? value)
        {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T2> asT2)
        {
            Index = 3;
            Enumerable = asT2;
        }
        
        public IEnumerable<T2> AsT2 => Index == 3 ? (IEnumerable<T2>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T3

        public bool TryGetAsT3([NotNullWhen(true)] out IEnumerable<T3>? value)
        {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T3> asT3)
        {
            Index = 4;
            Enumerable = asT3;
        }
        
        public IEnumerable<T3> AsT3 => Index == 4 ? (IEnumerable<T3>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T4

        public bool TryGetAsT4([NotNullWhen(true)] out IEnumerable<T4>? value)
        {
            if (Index == 5)
            {
                value = (IEnumerable<T4>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T4> asT4)
        {
            Index = 5;
            Enumerable = asT4;
        }
        
        public IEnumerable<T4> AsT4 => Index == 5 ? (IEnumerable<T4>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T5

        public bool TryGetAsT5([NotNullWhen(true)] out IEnumerable<T5>? value)
        {
            if (Index == 6)
            {
                value = (IEnumerable<T5>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T5> asT5)
        {
            Index = 6;
            Enumerable = asT5;
        }
        
        public IEnumerable<T5> AsT5 => Index == 6 ? (IEnumerable<T5>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T6

        public bool TryGetAsT6([NotNullWhen(true)] out IEnumerable<T6>? value)
        {
            if (Index == 7)
            {
                value = (IEnumerable<T6>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T6> asT6)
        {
            Index = 7;
            Enumerable = asT6;
        }
        
        public IEnumerable<T6> AsT6 => Index == 7 ? (IEnumerable<T6>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<IEnumerable<T0>, TOutput> t0Case, 
            Func<IEnumerable<T1>, TOutput> t1Case,
            Func<IEnumerable<T2>, TOutput> t2Case,
            Func<IEnumerable<T3>, TOutput> t3Case,
            Func<IEnumerable<T4>, TOutput> t4Case,
            Func<IEnumerable<T5>, TOutput> t5Case,
            Func<IEnumerable<T6>, TOutput> t6Case)
        {
            switch (Index)
            {
                case 1: return t0Case(AsT0);
                case 2: return t1Case(AsT1);
                case 3: return t2Case(AsT2);
                case 4: return t3Case(AsT3);
                case 5: return t4Case(AsT4);
                case 6: return t5Case(AsT5);
                case 7: return t6Case(AsT6);
            }
            throw new ArgumentException("Union does not contain a value");
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, Task<TOutput>> t4Case,
            Func<IEnumerable<T5>, Task<TOutput>> t5Case,
            Func<IEnumerable<T6>, Task<TOutput>> t6Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                6 => t5Case(AsT5),
                7 => t6Case(AsT6),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> t4Case,
            Func<IEnumerable<T5>, CancellationToken, Task<TOutput>> t5Case,
            Func<IEnumerable<T6>, CancellationToken, Task<TOutput>> t6Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                6 => t5Case(AsT5, ct),
                7 => t6Case(AsT6, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<IEnumerable<T0>> t0Case, 
            Action<IEnumerable<T1>> t1Case,
            Action<IEnumerable<T2>> t2Case,
            Action<IEnumerable<T3>> t3Case,
            Action<IEnumerable<T4>> t4Case,
            Action<IEnumerable<T5>> t5Case,
            Action<IEnumerable<T6>> t6Case)
        {
            switch (Index)
            {
                case 1: t0Case(AsT0); break;
                case 2: t1Case(AsT1); break;
                case 3: t2Case(AsT2); break;
                case 4: t3Case(AsT3); break;
                case 5: t4Case(AsT4); break;
                case 6: t5Case(AsT5); break;
                case 7: t6Case(AsT6); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, Task> t0Case, 
            Func<IEnumerable<T1>, Task> t1Case,
            Func<IEnumerable<T2>, Task> t2Case,
            Func<IEnumerable<T3>, Task> t3Case,
            Func<IEnumerable<T4>, Task> t4Case,
            Func<IEnumerable<T5>, Task> t5Case,
            Func<IEnumerable<T6>, Task> t6Case)
        {
            return Index switch            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                6 => t5Case(AsT5),
                7 => t6Case(AsT6),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, CancellationToken, Task> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task> t4Case,
            Func<IEnumerable<T5>, CancellationToken, Task> t5Case,
            Func<IEnumerable<T6>, CancellationToken, Task> t6Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                6 => t5Case(AsT5, ct),
                7 => t6Case(AsT6, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
    
    #region Utility Properties
    
        public bool IsT0 => Index == 1;
        public bool IsT1 => Index == 2;
        public bool IsT2 => Index == 3;
        public bool IsT3 => Index == 4;
        public bool IsT4 => Index == 5;
        public bool IsT5 => Index == 6;
        public bool IsT6 => Index == 7;
        
        public object Value => Enumerable;
    
    #endregion
}

public readonly struct UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7> 
    where T0 : allows ref struct
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
    where T5 : allows ref struct
    where T6 : allows ref struct
    where T7 : allows ref struct
{
    public byte Index { get; }
    private IEnumerable Enumerable { get; }

    internal UnionEnumerable(byte index, IEnumerable enumerable)
    {
        Index = index;
        Enumerable = enumerable;
    }
    
    #region T0

        public bool TryGetAsT0([NotNullWhen(true)] out IEnumerable<T0>? value)
        {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T0> asT0)
        {
            Index = 1;
            Enumerable = asT0;
        }
        
        public IEnumerable<T0> AsT0 => Index == 1 ? (IEnumerable<T0>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T1

        public bool TryGetAsT1([NotNullWhen(true)] out IEnumerable<T1>? value)
        {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T1> asT1)
        {
            Index = 2;
            Enumerable = asT1;
        }
            
        public IEnumerable<T1> AsT1 => Index == 2 ? (IEnumerable<T1>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T2

        public bool TryGetAsT2([NotNullWhen(true)] out IEnumerable<T2>? value)
        {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T2> asT2)
        {
            Index = 3;
            Enumerable = asT2;
        }
        
        public IEnumerable<T2> AsT2 => Index == 3 ? (IEnumerable<T2>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T3

        public bool TryGetAsT3([NotNullWhen(true)] out IEnumerable<T3>? value)
        {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T3> asT3)
        {
            Index = 4;
            Enumerable = asT3;
        }
        
        public IEnumerable<T3> AsT3 => Index == 4 ? (IEnumerable<T3>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T4

        public bool TryGetAsT4([NotNullWhen(true)] out IEnumerable<T4>? value)
        {
            if (Index == 5)
            {
                value = (IEnumerable<T4>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T4> asT4)
        {
            Index = 5;
            Enumerable = asT4;
        }
        
        public IEnumerable<T4> AsT4 => Index == 5 ? (IEnumerable<T4>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T5

        public bool TryGetAsT5([NotNullWhen(true)] out IEnumerable<T5>? value)
        {
            if (Index == 6)
            {
                value = (IEnumerable<T5>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T5> asT5)
        {
            Index = 6;
            Enumerable = asT5;
        }
        
        public IEnumerable<T5> AsT5 => Index == 6 ? (IEnumerable<T5>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T6

        public bool TryGetAsT6([NotNullWhen(true)] out IEnumerable<T6>? value)
        {
            if (Index == 7)
            {
                value = (IEnumerable<T6>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T6> asT6)
        {
            Index = 7;
            Enumerable = asT6;
        }
        
        public IEnumerable<T6> AsT6 => Index == 7 ? (IEnumerable<T6>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T7

        public bool TryGetAsT7([NotNullWhen(true)] out IEnumerable<T7>? value)
        {
            if (Index == 8)
            {
                value = (IEnumerable<T7>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T7> asT7)
        {
            Index = 8;
            Enumerable = asT7;
        }
        
        public IEnumerable<T7> AsT7 => Index == 8 ? (IEnumerable<T7>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<IEnumerable<T0>, TOutput> t0Case, 
            Func<IEnumerable<T1>, TOutput> t1Case,
            Func<IEnumerable<T2>, TOutput> t2Case,
            Func<IEnumerable<T3>, TOutput> t3Case,
            Func<IEnumerable<T4>, TOutput> t4Case,
            Func<IEnumerable<T5>, TOutput> t5Case,
            Func<IEnumerable<T6>, TOutput> t6Case,
            Func<IEnumerable<T7>, TOutput> t7Case)
        {
            switch (Index)
            {
                case 1: return t0Case(AsT0);
                case 2: return t1Case(AsT1);
                case 3: return t2Case(AsT2);
                case 4: return t3Case(AsT3);
                case 5: return t4Case(AsT4);
                case 6: return t5Case(AsT5);
                case 7: return t6Case(AsT6);
                case 8: return t7Case(AsT7);
            }
            throw new ArgumentException("Union does not contain a value");
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, Task<TOutput>> t4Case,
            Func<IEnumerable<T5>, Task<TOutput>> t5Case,
            Func<IEnumerable<T6>, Task<TOutput>> t6Case,
            Func<IEnumerable<T7>, Task<TOutput>> t7Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                6 => t5Case(AsT5),
                7 => t6Case(AsT6),
                8 => t7Case(AsT7),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> t4Case,
            Func<IEnumerable<T5>, CancellationToken, Task<TOutput>> t5Case,
            Func<IEnumerable<T6>, CancellationToken, Task<TOutput>> t6Case,
            Func<IEnumerable<T7>, CancellationToken, Task<TOutput>> t7Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                6 => t5Case(AsT5, ct),
                7 => t6Case(AsT6, ct),
                8 => t7Case(AsT7, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<IEnumerable<T0>> t0Case, 
            Action<IEnumerable<T1>> t1Case,
            Action<IEnumerable<T2>> t2Case,
            Action<IEnumerable<T3>> t3Case,
            Action<IEnumerable<T4>> t4Case,
            Action<IEnumerable<T5>> t5Case,
            Action<IEnumerable<T6>> t6Case,
            Action<IEnumerable<T7>> t7Case)
        {
            switch (Index)
            {
                case 1: t0Case(AsT0); break;
                case 2: t1Case(AsT1); break;
                case 3: t2Case(AsT2); break;
                case 4: t3Case(AsT3); break;
                case 5: t4Case(AsT4); break;
                case 6: t5Case(AsT5); break;
                case 7: t6Case(AsT6); break;
                case 8: t7Case(AsT7); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, Task> t0Case, 
            Func<IEnumerable<T1>, Task> t1Case,
            Func<IEnumerable<T2>, Task> t2Case,
            Func<IEnumerable<T3>, Task> t3Case,
            Func<IEnumerable<T4>, Task> t4Case,
            Func<IEnumerable<T5>, Task> t5Case,
            Func<IEnumerable<T6>, Task> t6Case,
            Func<IEnumerable<T7>, Task> t7Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                6 => t5Case(AsT5),
                7 => t6Case(AsT6),
                8 => t7Case(AsT7),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, CancellationToken, Task> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task> t4Case,
            Func<IEnumerable<T5>, CancellationToken, Task> t5Case,
            Func<IEnumerable<T6>, CancellationToken, Task> t6Case,
            Func<IEnumerable<T7>, CancellationToken, Task> t7Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                6 => t5Case(AsT5, ct),
                7 => t6Case(AsT6, ct),
                8 => t7Case(AsT7, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
    
    #region Utility Properties
    
        public bool IsT0 => Index == 1;
        public bool IsT1 => Index == 2;
        public bool IsT2 => Index == 3;
        public bool IsT3 => Index == 4;
        public bool IsT4 => Index == 5;
        public bool IsT5 => Index == 6;
        public bool IsT6 => Index == 7;
        public bool IsT7 => Index == 8;
        
        public object Value => Enumerable;
    
    #endregion
}

public readonly struct UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8> 
    where T0 : allows ref struct
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
    where T5 : allows ref struct
    where T6 : allows ref struct
    where T7 : allows ref struct
    where T8 : allows ref struct
{
    public byte Index { get; }
    private IEnumerable Enumerable { get; }

    internal UnionEnumerable(byte index, IEnumerable enumerable)
    {
        Index = index;
        Enumerable = enumerable;
    }
    
    #region T0

        public bool TryGetAsT0([NotNullWhen(true)] out IEnumerable<T0>? value)
        {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T0> asT0)
        {
            Index = 1;
            Enumerable = asT0;
        }
        
        public IEnumerable<T0> AsT0 => Index == 1 ? (IEnumerable<T0>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T1

        public bool TryGetAsT1([NotNullWhen(true)] out IEnumerable<T1>? value)
        {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T1> asT1)
        {
            Index = 2;
            Enumerable = asT1;
        }
            
        public IEnumerable<T1> AsT1 => Index == 2 ? (IEnumerable<T1>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T2

        public bool TryGetAsT2([NotNullWhen(true)] out IEnumerable<T2>? value)
        {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T2> asT2)
        {
            Index = 3;
            Enumerable = asT2;
        }
        
        public IEnumerable<T2> AsT2 => Index == 3 ? (IEnumerable<T2>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T3

        public bool TryGetAsT3([NotNullWhen(true)] out IEnumerable<T3>? value)
        {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T3> asT3)
        {
            Index = 4;
            Enumerable = asT3;
        }
        
        public IEnumerable<T3> AsT3 => Index == 4 ? (IEnumerable<T3>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T4

        public bool TryGetAsT4([NotNullWhen(true)] out IEnumerable<T4>? value)
        {
            if (Index == 5)
            {
                value = (IEnumerable<T4>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T4> asT4)
        {
            Index = 5;
            Enumerable = asT4;
        }
        
        public IEnumerable<T4> AsT4 => Index == 5 ? (IEnumerable<T4>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T5

        public bool TryGetAsT5([NotNullWhen(true)] out IEnumerable<T5>? value)
        {
            if (Index == 6)
            {
                value = (IEnumerable<T5>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T5> asT5)
        {
            Index = 6;
            Enumerable = asT5;
        }
        
        public IEnumerable<T5> AsT5 => Index == 6 ? (IEnumerable<T5>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T6

        public bool TryGetAsT6([NotNullWhen(true)] out IEnumerable<T6>? value)
        {
            if (Index == 7)
            {
                value = (IEnumerable<T6>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T6> asT6)
        {
            Index = 7;
            Enumerable = asT6;
        }
        
        public IEnumerable<T6> AsT6 => Index == 7 ? (IEnumerable<T6>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T7

        public bool TryGetAsT7([NotNullWhen(true)] out IEnumerable<T7>? value)
        {
            if (Index == 8)
            {
                value = (IEnumerable<T7>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T7> asT7)
        {
            Index = 8;
            Enumerable = asT7;
        }
        
        public IEnumerable<T7> AsT7 => Index == 8 ? (IEnumerable<T7>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T8

        public bool TryGetAsT8([NotNullWhen(true)] out IEnumerable<T8>? value)
        {
            if (Index == 9)
            {
                value = (IEnumerable<T8>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T8> asT8)
        {
            Index = 9;
            Enumerable = asT8;
        }
        
        public IEnumerable<T8> AsT8 => Index == 9 ? (IEnumerable<T8>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<IEnumerable<T0>, TOutput> t0Case, 
            Func<IEnumerable<T1>, TOutput> t1Case,
            Func<IEnumerable<T2>, TOutput> t2Case,
            Func<IEnumerable<T3>, TOutput> t3Case,
            Func<IEnumerable<T4>, TOutput> t4Case,
            Func<IEnumerable<T5>, TOutput> t5Case,
            Func<IEnumerable<T6>, TOutput> t6Case,
            Func<IEnumerable<T7>, TOutput> t7Case,
            Func<IEnumerable<T8>, TOutput> t8Case)
        {
            switch (Index)
            {
                case 1: return t0Case(AsT0);
                case 2: return t1Case(AsT1);
                case 3: return t2Case(AsT2);
                case 4: return t3Case(AsT3);
                case 5: return t4Case(AsT4);
                case 6: return t5Case(AsT5);
                case 7: return t6Case(AsT6);
                case 8: return t7Case(AsT7);
                case 9: return t8Case(AsT8);
            }
            throw new ArgumentException("Union does not contain a value");
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, Task<TOutput>> t4Case,
            Func<IEnumerable<T5>, Task<TOutput>> t5Case,
            Func<IEnumerable<T6>, Task<TOutput>> t6Case,
            Func<IEnumerable<T7>, Task<TOutput>> t7Case,
            Func<IEnumerable<T8>, Task<TOutput>> t8Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                6 => t5Case(AsT5),
                7 => t6Case(AsT6),
                8 => t7Case(AsT7),
                9 => t8Case(AsT8),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> t4Case,
            Func<IEnumerable<T5>, CancellationToken, Task<TOutput>> t5Case,
            Func<IEnumerable<T6>, CancellationToken, Task<TOutput>> t6Case,
            Func<IEnumerable<T7>, CancellationToken, Task<TOutput>> t7Case,
            Func<IEnumerable<T8>, CancellationToken, Task<TOutput>> t8Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                6 => t5Case(AsT5, ct),
                7 => t6Case(AsT6, ct),
                8 => t7Case(AsT7, ct),
                9 => t8Case(AsT8, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<IEnumerable<T0>> t0Case, 
            Action<IEnumerable<T1>> t1Case,
            Action<IEnumerable<T2>> t2Case,
            Action<IEnumerable<T3>> t3Case,
            Action<IEnumerable<T4>> t4Case,
            Action<IEnumerable<T5>> t5Case,
            Action<IEnumerable<T6>> t6Case,
            Action<IEnumerable<T7>> t7Case,
            Action<IEnumerable<T8>> t8Case)
        {
            switch (Index)
            {
                case 1: t0Case(AsT0); break;
                case 2: t1Case(AsT1); break;
                case 3: t2Case(AsT2); break;
                case 4: t3Case(AsT3); break;
                case 5: t4Case(AsT4); break;
                case 6: t5Case(AsT5); break;
                case 7: t6Case(AsT6); break;
                case 8: t7Case(AsT7); break;
                case 9: t8Case(AsT8); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, Task> t0Case, 
            Func<IEnumerable<T1>, Task> t1Case,
            Func<IEnumerable<T2>, Task> t2Case,
            Func<IEnumerable<T3>, Task> t3Case,
            Func<IEnumerable<T4>, Task> t4Case,
            Func<IEnumerable<T5>, Task> t5Case,
            Func<IEnumerable<T6>, Task> t6Case,
            Func<IEnumerable<T7>, Task> t7Case,
            Func<IEnumerable<T8>, Task> t8Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                6 => t5Case(AsT5),
                7 => t6Case(AsT6),
                8 => t7Case(AsT7),
                9 => t8Case(AsT8),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, CancellationToken, Task> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task> t4Case,
            Func<IEnumerable<T5>, CancellationToken, Task> t5Case,
            Func<IEnumerable<T6>, CancellationToken, Task> t6Case,
            Func<IEnumerable<T7>, CancellationToken, Task> t7Case,
            Func<IEnumerable<T8>, CancellationToken, Task> t8Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                6 => t5Case(AsT5, ct),
                7 => t6Case(AsT6, ct),
                8 => t7Case(AsT7, ct),
                9 => t8Case(AsT8, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
    
    #region Utility Properties
    
        public bool IsT0 => Index == 1;
        public bool IsT1 => Index == 2;
        public bool IsT2 => Index == 3;
        public bool IsT3 => Index == 4;
        public bool IsT4 => Index == 5;
        public bool IsT5 => Index == 6;
        public bool IsT6 => Index == 7;
        public bool IsT7 => Index == 8;
        public bool IsT8 => Index == 9;
        
        public object Value => Enumerable;
    
    #endregion
}

public readonly struct UnionEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> 
    where T0 : allows ref struct
    where T1 : allows ref struct
    where T2 : allows ref struct
    where T3 : allows ref struct
    where T4 : allows ref struct
    where T5 : allows ref struct
    where T6 : allows ref struct
    where T7 : allows ref struct
    where T8 : allows ref struct
    where T9 : allows ref struct
{
    public byte Index { get; }
    private IEnumerable Enumerable { get; }

    internal UnionEnumerable(byte index, IEnumerable enumerable)
    {
        Index = index;
        Enumerable = enumerable;
    }
    
    #region T0

        public bool TryGetAsT0([NotNullWhen(true)] out IEnumerable<T0>? value)
        {
            if (Index == 1)
            {
                value = (IEnumerable<T0>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T0> asT0)
        {
            Index = 1;
            Enumerable = asT0;
        }
        
        public IEnumerable<T0> AsT0 => Index == 1 ? (IEnumerable<T0>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T1

        public bool TryGetAsT1([NotNullWhen(true)] out IEnumerable<T1>? value)
        {
            if (Index == 2)
            {
                value = (IEnumerable<T1>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T1> asT1)
        {
            Index = 2;
            Enumerable = asT1;
        }
            
        public IEnumerable<T1> AsT1 => Index == 2 ? (IEnumerable<T1>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T2

        public bool TryGetAsT2([NotNullWhen(true)] out IEnumerable<T2>? value)
        {
            if (Index == 3)
            {
                value = (IEnumerable<T2>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T2> asT2)
        {
            Index = 3;
            Enumerable = asT2;
        }
        
        public IEnumerable<T2> AsT2 => Index == 3 ? (IEnumerable<T2>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T3

        public bool TryGetAsT3([NotNullWhen(true)] out IEnumerable<T3>? value)
        {
            if (Index == 4)
            {
                value = (IEnumerable<T3>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T3> asT3)
        {
            Index = 4;
            Enumerable = asT3;
        }
        
        public IEnumerable<T3> AsT3 => Index == 4 ? (IEnumerable<T3>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T4

        public bool TryGetAsT4([NotNullWhen(true)] out IEnumerable<T4>? value)
        {
            if (Index == 5)
            {
                value = (IEnumerable<T4>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T4> asT4)
        {
            Index = 5;
            Enumerable = asT4;
        }
        
        public IEnumerable<T4> AsT4 => Index == 5 ? (IEnumerable<T4>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T5

        public bool TryGetAsT5([NotNullWhen(true)] out IEnumerable<T5>? value)
        {
            if (Index == 6)
            {
                value = (IEnumerable<T5>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T5> asT5)
        {
            Index = 6;
            Enumerable = asT5;
        }
        
        public IEnumerable<T5> AsT5 => Index == 6 ? (IEnumerable<T5>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T6

        public bool TryGetAsT6([NotNullWhen(true)] out IEnumerable<T6>? value)
        {
            if (Index == 7)
            {
                value = (IEnumerable<T6>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T6> asT6)
        {
            Index = 7;
            Enumerable = asT6;
        }
        
        public IEnumerable<T6> AsT6 => Index == 7 ? (IEnumerable<T6>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T7

        public bool TryGetAsT7([NotNullWhen(true)] out IEnumerable<T7>? value)
        {
            if (Index == 8)
            {
                value = (IEnumerable<T7>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T7> asT7)
        {
            Index = 8;
            Enumerable = asT7;
        }
        
        public IEnumerable<T7> AsT7 => Index == 8 ? (IEnumerable<T7>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T8

        public bool TryGetAsT8([NotNullWhen(true)] out IEnumerable<T8>? value)
        {
            if (Index == 9)
            {
                value = (IEnumerable<T8>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T8> asT8)
        {
            Index = 9;
            Enumerable = asT8;
        }
        
        public IEnumerable<T8> AsT8 => Index == 9 ? (IEnumerable<T8>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region T9

        public bool TryGetAsT9([NotNullWhen(true)] out IEnumerable<T9>? value)
        {
            if (Index == 10)
            {
                value = (IEnumerable<T9>)Enumerable;
                return true;
            }
            value = null;
            return false;
        }

        public UnionEnumerable(IEnumerable<T9> asT9)
        {
            Index = 10;
            Enumerable = asT9;
        }
        
        public IEnumerable<T9> AsT9 => Index == 10 ? (IEnumerable<T9>)Enumerable : throw new InvalidCastException();

    #endregion
    
    #region Match and MatchAsync
    
        public TOutput Match<TOutput>(
            Func<IEnumerable<T0>, TOutput> t0Case, 
            Func<IEnumerable<T1>, TOutput> t1Case,
            Func<IEnumerable<T2>, TOutput> t2Case,
            Func<IEnumerable<T3>, TOutput> t3Case,
            Func<IEnumerable<T4>, TOutput> t4Case,
            Func<IEnumerable<T5>, TOutput> t5Case,
            Func<IEnumerable<T6>, TOutput> t6Case,
            Func<IEnumerable<T7>, TOutput> t7Case,
            Func<IEnumerable<T8>, TOutput> t8Case,
            Func<IEnumerable<T9>, TOutput> t9Case)
        {
            switch (Index)
            {
                case 1: return t0Case(AsT0);
                case 2: return t1Case(AsT1);
                case 3: return t2Case(AsT2);
                case 4: return t3Case(AsT3);
                case 5: return t4Case(AsT4);
                case 6: return t5Case(AsT5);
                case 7: return t6Case(AsT6);
                case 8: return t7Case(AsT7);
                case 9: return t8Case(AsT8);
                case 10: return t9Case(AsT9);
            }
            throw new ArgumentException("Union does not contain a value");
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, Task<TOutput>> t4Case,
            Func<IEnumerable<T5>, Task<TOutput>> t5Case,
            Func<IEnumerable<T6>, Task<TOutput>> t6Case,
            Func<IEnumerable<T7>, Task<TOutput>> t7Case,
            Func<IEnumerable<T8>, Task<TOutput>> t8Case,
            Func<IEnumerable<T9>, Task<TOutput>> t9Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                6 => t5Case(AsT5),
                7 => t6Case(AsT6),
                8 => t7Case(AsT7),
                9 => t8Case(AsT8),
                10 => t9Case(AsT9),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task<TOutput> MatchAsync<TOutput>(
            Func<IEnumerable<T0>, CancellationToken, Task<TOutput>> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task<TOutput>> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task<TOutput>> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task<TOutput>> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task<TOutput>> t4Case,
            Func<IEnumerable<T5>, CancellationToken, Task<TOutput>> t5Case,
            Func<IEnumerable<T6>, CancellationToken, Task<TOutput>> t6Case,
            Func<IEnumerable<T7>, CancellationToken, Task<TOutput>> t7Case,
            Func<IEnumerable<T8>, CancellationToken, Task<TOutput>> t8Case,
            Func<IEnumerable<T9>, CancellationToken, Task<TOutput>> t9Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                6 => t5Case(AsT5, ct),
                7 => t6Case(AsT6, ct),
                8 => t7Case(AsT7, ct),
                9 => t8Case(AsT8, ct),
                10 => t9Case(AsT9, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion

    #region Switch and SwitchAsync
    
        public void Switch(
            Action<IEnumerable<T0>> t0Case, 
            Action<IEnumerable<T1>> t1Case,
            Action<IEnumerable<T2>> t2Case,
            Action<IEnumerable<T3>> t3Case,
            Action<IEnumerable<T4>> t4Case,
            Action<IEnumerable<T5>> t5Case,
            Action<IEnumerable<T6>> t6Case,
            Action<IEnumerable<T7>> t7Case,
            Action<IEnumerable<T8>> t8Case,
            Action<IEnumerable<T9>> t9Case)
        {
            switch (Index)
            {
                case 1: t0Case(AsT0); break;
                case 2: t1Case(AsT1); break;
                case 3: t2Case(AsT2); break;
                case 4: t3Case(AsT3); break;
                case 5: t4Case(AsT4); break;
                case 6: t5Case(AsT5); break;
                case 7: t6Case(AsT6); break;
                case 8: t7Case(AsT7); break;
                case 9: t8Case(AsT8); break;
                case 10: t9Case(AsT9); break;
                default: throw new ArgumentException("Union does not contain a value");
            }
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, Task> t0Case, 
            Func<IEnumerable<T1>, Task> t1Case,
            Func<IEnumerable<T2>, Task> t2Case,
            Func<IEnumerable<T3>, Task> t3Case,
            Func<IEnumerable<T4>, Task> t4Case,
            Func<IEnumerable<T5>, Task> t5Case,
            Func<IEnumerable<T6>, Task> t6Case,
            Func<IEnumerable<T7>, Task> t7Case,
            Func<IEnumerable<T8>, Task> t8Case,
            Func<IEnumerable<T9>, Task> t9Case)
        {
            return Index switch
            {
                1 => t0Case(AsT0),
                2 => t1Case(AsT1),
                3 => t2Case(AsT2),
                4 => t3Case(AsT3),
                5 => t4Case(AsT4),
                6 => t5Case(AsT5),
                7 => t6Case(AsT6),
                8 => t7Case(AsT7),
                9 => t8Case(AsT8),
                10 => t9Case(AsT9),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }

        public Task SwitchAsync(
            Func<IEnumerable<T0>, CancellationToken, Task> t0Case, 
            Func<IEnumerable<T1>, CancellationToken, Task> t1Case,
            Func<IEnumerable<T2>, CancellationToken, Task> t2Case,
            Func<IEnumerable<T3>, CancellationToken, Task> t3Case,
            Func<IEnumerable<T4>, CancellationToken, Task> t4Case,
            Func<IEnumerable<T5>, CancellationToken, Task> t5Case,
            Func<IEnumerable<T6>, CancellationToken, Task> t6Case,
            Func<IEnumerable<T7>, CancellationToken, Task> t7Case,
            Func<IEnumerable<T8>, CancellationToken, Task> t8Case,
            Func<IEnumerable<T9>, CancellationToken, Task> t9Case, 
            CancellationToken ct)
        {
            return Index switch
            {
                1 => t0Case(AsT0, ct),
                2 => t1Case(AsT1, ct),
                3 => t2Case(AsT2, ct),
                4 => t3Case(AsT3, ct),
                5 => t4Case(AsT4, ct),
                6 => t5Case(AsT5, ct),
                7 => t6Case(AsT6, ct),
                8 => t7Case(AsT7, ct),
                9 => t8Case(AsT8, ct),
                10 => t9Case(AsT9, ct),
                _ => throw new ArgumentException("Union does not contain a value")
            };
        }
    
    #endregion
    
    #region Utility Properties
    
        public bool IsT0 => Index == 1;
        public bool IsT1 => Index == 2;
        public bool IsT2 => Index == 3;
        public bool IsT3 => Index == 4;
        public bool IsT4 => Index == 5;
        public bool IsT5 => Index == 6;
        public bool IsT6 => Index == 7;
        public bool IsT7 => Index == 8;
        public bool IsT8 => Index == 9;
        public bool IsT9 => Index == 10;
        
        public object Value => Enumerable;
    
    #endregion
}