namespace io.github.toyota32k.toolkit.net;
public static class Utils {
    public static T? GetValue<T>(this WeakReference<T> w) where T : class {
        return w.TryGetTarget(out T? o) ? o : null;
    }

    public static V? GetValue<K, V>(this Dictionary<K, V> dic, K key, V? def = default) where K:notnull {
        return dic.TryGetValue(key, out var v) ? v : def;
    }

    public static bool IsEmpty<T>(this IEnumerable<T>? v) {
        return (v == null) ? true : !v.Any();
    }
    public static bool IsNullOrEmpty<T>(IEnumerable<T>? v) {
        return v.IsEmpty();
    }


    public static T Apply<T>(this T obj, Action<T> fn) where T : class {
        fn(obj);
        return obj;
    }
    public static R Run<T, R>(this T obj, Func<T, R> fn) where T : class {
        return fn(obj);
    }

    /**
     * IListをIEnumerable<T>に変換
     * ListView の Items などを IEnumerable<T> に変換するためのメソッド
     * T は、IList の要素の正しい型を指定する必要がある。
     * 当然のことながらなんにでもキャストできるわけではない。
     */
    public static IEnumerable<T> ToEnumerable<T>(this System.Collections.IList list) {
        foreach (var o in list) {
            yield return (T)o;
        }
    }

    public static IEnumerable<T> ToSingleEnumerable<T>(this T item) {
        yield return item;
    }

    public static T[] ArrayOf<T>(params T[] args) {
        return args;
    }

    // ありそうでないメソッド
    public static bool IsEmpty(this String? s) {
        return string.IsNullOrEmpty(s);
    }

    public static bool ContainsIgnoreCase(this string s, string partialText) {
        return s.IndexOf(partialText, StringComparison.CurrentCultureIgnoreCase) >= 0;
    }

    /**
     * 文字列-->Enum変換
     */
    public static T ParseToEnum<T>(string name, T defValue, bool igonreCase=true) where T: struct {
        if(Enum.TryParse<T>(name, igonreCase, out T result)) {
            return result;
        } 
        else {
            return defValue;
        }
    }
}
