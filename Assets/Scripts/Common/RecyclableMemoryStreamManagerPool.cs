using Microsoft.IO;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class RecyclableMemoryStreamManagerPool
{
    static RecyclableMemoryStreamManager memoryStreamManager;

    static RecyclableMemoryStreamManagerPool()
    {
        memoryStreamManager = new RecyclableMemoryStreamManager();
    }

    public static MemoryStream GetStream()
    {
        return new RecyclableMemoryStream(memoryStreamManager);
    }

    public static MemoryStream GetStream(string tag)
    {
        return new RecyclableMemoryStream(memoryStreamManager, tag);
    }

    public static MemoryStream GetStream(string tag, int requiredSize)
    {
        return new RecyclableMemoryStream(memoryStreamManager, tag, requiredSize);
    }
}
