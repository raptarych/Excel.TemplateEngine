﻿namespace Excel.TemplateEngine.FileGenerating.DataTypes
{
    public class ExcelCellFillStyle
    {
        public ExcelColor Color { get; set; }

        public override string ToString()
        {
            return string.Format("FillColor = {{{0}}}", Color);
        }
    }
}