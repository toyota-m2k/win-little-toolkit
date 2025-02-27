namespace io.github.toyota32k.toolkit.net;
public class DisposablePool : List<IDisposable>, IDisposable {
    public void Dispose() {
        foreach (var e in this) {
            e.Dispose();
        }
        Clear();
    }
}
