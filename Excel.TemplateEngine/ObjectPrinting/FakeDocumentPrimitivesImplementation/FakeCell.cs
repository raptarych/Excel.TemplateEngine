﻿using Excel.TemplateEngine.ObjectPrinting.DataTypes;
using Excel.TemplateEngine.ObjectPrinting.DocumentPrimitivesInterfaces;
using Excel.TemplateEngine.ObjectPrinting.NavigationPrimitives;

namespace Excel.TemplateEngine.ObjectPrinting.FakeDocumentPrimitivesImplementation
{
    public class FakeCell : ICell
    {
        public FakeCell(ICellPosition position)
        {
            CellPosition = position;
        }

        public string StringValue { get; set; }
        public CellType CellType { get; set; }
        public ICellPosition CellPosition { get; private set; }

        public void CopyStyle(ICell templateCell)
        {
            var fakeCell = (FakeCell)templateCell;
            StyleId = fakeCell.StyleId;
        }

        public string StyleId { get; set; }
    }
}