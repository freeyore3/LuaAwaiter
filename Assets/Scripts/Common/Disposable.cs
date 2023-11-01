using System;

public class Disposable : IDisposable
{
    private bool m_IsDisposed;
    protected virtual void OnDispose() { }
    public bool IsDisposed => m_IsDisposed;
    public void Dispose()
    {
        if (m_IsDisposed)
            return;

        m_IsDisposed = true;
        OnDispose();
    }
}
