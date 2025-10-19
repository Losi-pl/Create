using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Text;

namespace Create.SourceGenerators
{
    [Generator(LanguageNames.CSharp)]
    public class StructArray : IIncrementalGenerator
    {
        static string generated = generate();
        
        internal static string generate()
        {
            StringBuilder arrays = new StringBuilder();
            for (int i = 2; i <= 100; i++)
            {
                StringBuilder variables = new StringBuilder();
                variables.Append("T ");
                for (int I = 0; I < i - 1; I++)
                    variables.Append($"e{I}, ");
                variables.Append($"e{i - 1};");

                StringBuilder get_switch = new StringBuilder();
                for (int I = 0; I < i; I++)
                    get_switch.AppendLine($"                    case {I}: return e{I};");

                StringBuilder set_switch = new StringBuilder();
                for (int I = 0; I < i; I++)
                    set_switch.AppendLine($"                    case {I}: e{I} = value; break;");

                StringBuilder clear = new StringBuilder();
                for (int I = 0; I < i; I++)
                    clear.AppendLine($"            e{I} = default!;");

                StringBuilder contains = new StringBuilder();
                for (int I = 0; I < i; I++)
                    contains.AppendLine($"            if ((e{I} is null) ? (item is null) : e{I}.Equals(item)) return true;");

                StringBuilder coppyto = new StringBuilder();
                coppyto.AppendLine($"            if (arrayIndex < Count) array[arrayIndex] = e0;");
                for (int I = 1; I < i; I++)
                    coppyto.AppendLine($"            if (arrayIndex + {I} < Count) array[arrayIndex + {I}] = e{I};");

                StringBuilder index_of = new StringBuilder();
                for (int I = 0; I < i; I++)
                    index_of.AppendLine($"            if ((e{I} is null) ? (item is null) : e{I}.Equals(item)) return {I};");

                arrays.Append('\n');
                arrays.Append($@"    public struct Count{i}<T> : IList<T>, IList
    {{
        {variables}

        public int Count => {i};
        public T this[int index]
        {{
            get
            {{
                switch (index)
                {{
{get_switch}                    default: throw new IndexOutOfRangeException(""Index must be in range [0..{i - 1}]"");
                }}
            }}
            set
            {{
                switch (index)
                {{
{set_switch}                    default: throw new IndexOutOfRangeException(""Index must be in range [0..{i - 1}]"");
                }}
            }}
        }}
        public void Clear()
        {{
{clear}       }}
        public bool Contains(T item)
        {{
{contains}            return false;
        }}
        void copyTo(T[] array, int arrayIndex)
        {{
{coppyto}        }}
        public int IndexOf(T item)
        {{
{index_of}            return -1;
        }}

        object? IList.this[int index] {{ get => this[index]; set => this[index] = (T)value!; }}
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);
        bool IList.IsReadOnly => false;
        bool ICollection<T>.IsReadOnly => false;
        bool IList.IsFixedSize => true;
        bool ICollection.IsSynchronized => true;
        object ICollection.SyncRoot => new object();
        bool IList.Contains(object? value) => Contains((T)value!);
        void ICollection<T>.CopyTo(T[] array, int arrayIndex) => copyTo(array, arrayIndex);
        void ICollection.CopyTo(Array array, int arrayIndex) => copyTo((T[])array, arrayIndex);
        int IList.IndexOf(object? value) => IndexOf((T)value!);
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        void ICollection<T>.Add(T item) => throw new NotImplementedException();
        int IList.Add(object? value) => throw new NotImplementedException();
        void IList<T>.Insert(int index, T item) => throw new NotImplementedException();
        void IList.Insert(int index, object? value) => throw new NotImplementedException();
        bool ICollection<T>.Remove(T item) => throw new NotImplementedException();
        void IList.Remove(object? value) => throw new NotImplementedException();
        void IList.RemoveAt(int index) => throw new NotImplementedException();
        void IList<T>.RemoveAt(int index) => throw new NotImplementedException();

        public struct Enumerator : IEnumerator<T>
        {{
            byte index = 0;
            Count{i}<T> array;

            public Enumerator(Count{i}<T> array) => this.array = array;
            public T Current => index == 0 ? default! : array[index - 1];
            object IEnumerator.Current => Current!;

            public void Dispose() {{ }}
            public void Reset() => index = 0;
            public bool MoveNext()
            {{
                index++;
                return index < {i + 1};
            }}
        }}
    }}");
            }

            var source = $@"// <auto-generated/>
using System.Collections;
#nullable enable
namespace Create.Conteiner;
public static class StructArray
{{
    static bool equals<T>(T b, T e) => (b is null) ? (e is null) : b.Equals(e);
    {arrays}
}}";
            
            return source;
        }

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(contex => contex.AddSource("StructArray.g.cs", generated) );
        }
    }
}