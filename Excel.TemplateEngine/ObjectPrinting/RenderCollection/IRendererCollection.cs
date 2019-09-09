﻿using System;

using Excel.TemplateEngine.ObjectPrinting.RenderCollection.Renderers;

namespace Excel.TemplateEngine.ObjectPrinting.RenderCollection
{
    public interface IRendererCollection
    {
        IRenderer GetRenderer(Type modelType);
        IFormControlRenderer GetFormControlRenderer(string typeName, Type modelType);
    }
}