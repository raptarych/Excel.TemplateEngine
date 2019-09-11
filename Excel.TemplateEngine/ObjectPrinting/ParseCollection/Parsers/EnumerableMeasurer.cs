using System;
using System.Collections.Generic;

using Excel.TemplateEngine.Exceptions;
using Excel.TemplateEngine.Helpers;
using Excel.TemplateEngine.ObjectPrinting.DocumentPrimitivesInterfaces;
using Excel.TemplateEngine.ObjectPrinting.NavigationPrimitives;
using Excel.TemplateEngine.ObjectPrinting.TableParser;

namespace Excel.TemplateEngine.ObjectPrinting.ParseCollection.Parsers
{
    public class EnumerableMeasurer : IEnumerableMeasurer
    {
        public EnumerableMeasurer(ParserCollection parserCollection)
        {
            this.parserCollection = parserCollection;
        }

        public int GetLength(ITableParser tableParser, Type modelType, IEnumerable<ICell> primaryParts)
        {
            var parserState = new List<(IAtomicValueParser parser, ICell cell, Type itemType)>();

            foreach (var primaryPart in primaryParts)
            {
                var childModelPath = ExcelTemplatePath.FromRawExpression(primaryPart.StringValue);
                var childModelType = ObjectPropertiesExtractor.ExtractChildObjectTypeFromPath(modelType, childModelPath);

                var parser = parserCollection.GetAtomicValueParser();

                parserState.Add((parser, primaryPart, childModelType));
            }

            for (var i = 0; i <= ParsingParameters.MaxEnumerableLength; i++)
            {
                var parsed = false;
                foreach (var (parser, cell, type) in parserState)
                {
                    tableParser.PushState(cell.CellPosition.Add(new ObjectSize(0, i)));
                    if (parser.TryParse(tableParser, type, out var result) && result != null)
                        parsed = true;
                    tableParser.PopState();
                }
                if (!parsed)
                    return i;
            }
            throw new EnumerableTooLongException(ParsingParameters.MaxEnumerableLength);
        }

        private readonly ParserCollection parserCollection;
    }
}