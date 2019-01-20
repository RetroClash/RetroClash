using System.Collections.Generic;
using System.IO;

namespace RetroClashCsvConverter
{
    public class CsWriter
    {
        internal CsWriter(string name, IReadOnlyList<string> header, string[] types)
        {
            using (var writer = new StreamWriter($"CS Output/{UppercaseFirst(name)}.cs"))
            {
                writer.WriteLine("namespace RetroClash.Files.Logic");
                writer.WriteLine("{");
                writer.WriteLine($"    public class {UppercaseFirst(name)} : Data");
                writer.WriteLine("    {");
                writer.WriteLine(
                    $"        public {UppercaseFirst(name)}(Row row, DataTable datatable) : base(row, datatable)");
                writer.WriteLine("        {");
                writer.WriteLine("            LoadData(this, GetType(), row);");
                writer.WriteLine("        }");
                writer.WriteLine();

                var count = header.Count;

                for (var index = 0; index < count; index++)
                {
                    var type = types[index].ToLower() == "boolean" ? "bool" : types[index].ToLower();

                    writer.WriteLine("        public " + type + " " + header[index] + " { get; set; }");

                    if (index < count - 1)
                        writer.WriteLine();
                }

                writer.WriteLine("    }");
                writer.WriteLine("}");
            }
        }

        internal string UppercaseFirst(string _String)
        {
            if (string.IsNullOrEmpty(_String))
                return string.Empty;

            var _Char = _String.ToCharArray();
            _Char[0] = char.ToUpper(_Char[0]);

            return new string(_Char);
        }
    }
}