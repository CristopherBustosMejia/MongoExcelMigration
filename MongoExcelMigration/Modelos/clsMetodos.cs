using MongoDB.Bson;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoExcelMigration.Modelos
{
    public static class clsMetodos
    {
        public static void ReadExcel(String filePath, int rowHeaderC, String dbName)
        {
            try
            {
                string fileName = @"D:\cris-\Descargas\" + filePath;

                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                IWorkbook workbook = new XSSFWorkbook(fs);

                ISheet sheet = workbook.GetSheetAt(0);

                DataFormatter dataf = new();

                List<String> lstHeaders = new List<string>();


                if (sheet != null)
                {
                    IRow headersRow = sheet.GetRow(rowHeaderC);
                    int auxInt = 0;
                    while (dataf.FormatCellValue(headersRow.GetCell(auxInt)) != "")
                    {
                        lstHeaders.Add(dataf.FormatCellValue(headersRow.GetCell(auxInt)));
                        auxInt++;
                    }

                    List<CellType> cellTypes = new List<CellType>();
                    bool flag = true;
                    for (int i = rowHeaderC + 1; i < sheet.LastRowNum; i++)
                    {
                        IRow currRow = sheet.GetRow(i);
                        var data = new Dictionary<string, object>();
                        for (int j = 0; j < lstHeaders.Count(); j++)
                        {
                            var cell = currRow.GetCell(j);
                            if (cell != null)
                            {
                                CellType type = cell.CellType;
                                switch (type)
                                {
                                    case CellType.String:
                                        string stringValue = cell.StringCellValue;
                                        data.Add(lstHeaders[j], stringValue);
                                        if (flag)
                                            cellTypes.Add(type);
                                        break;
                                    case CellType.Numeric:
                                        if (DateUtil.IsCellDateFormatted(cell))
                                        {
                                            DateTime dateValue = cell.DateCellValue;
                                            data.Add(lstHeaders[j], dateValue);
                                            if (flag)
                                                cellTypes.Add(type);
                                        }
                                        else
                                        {
                                            double numericValue = cell.NumericCellValue;
                                            data.Add(lstHeaders[j], numericValue);
                                            if (flag)
                                                cellTypes.Add(type);
                                        }
                                        break;
                                    case CellType.Boolean:
                                        bool booleanValue = cell.BooleanCellValue;
                                        data.Add(lstHeaders[j], booleanValue);
                                        if (flag)
                                            cellTypes.Add(type);
                                        break;
                                    case CellType.Formula:
                                        switch (currRow.GetCell(j).CachedFormulaResultType)
                                        {
                                            case CellType.String:
                                                string formulaStringValue = cell.StringCellValue;
                                                data.Add(lstHeaders[j], formulaStringValue);
                                                if (flag)
                                                    cellTypes.Add(currRow.GetCell(j).CachedFormulaResultType);
                                                break;
                                            case CellType.Numeric:
                                                double formulaNumericValue = cell.NumericCellValue;
                                                data.Add(lstHeaders[j], formulaNumericValue);
                                                if (flag)
                                                    cellTypes.Add(currRow.GetCell(j).CachedFormulaResultType);
                                                break;
                                            case CellType.Boolean:
                                                bool formulaBooleanValue = cell.BooleanCellValue;
                                                data.Add(lstHeaders[j], formulaBooleanValue);
                                                if(flag)
                                                    cellTypes.Add(currRow.GetCell(j).CachedFormulaResultType);
                                                break;
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                switch (cellTypes[j])
                                {
                                    case CellType.String:
                                        string stringValue = "String por defecto";
                                        data.Add(lstHeaders[j], stringValue);
                                        break;
                                    case CellType.Numeric:
                                        if (DateUtil.IsCellDateFormatted(cell))
                                        {
                                            DateTime dateValue = new DateTime(1900, 1, 1);
                                            data.Add(lstHeaders[j], dateValue);
                                        }
                                        else
                                        {
                                            double numericValue = 0;
                                            data.Add(lstHeaders[j], numericValue);
                                        }
                                        break;
                                    case CellType.Boolean:
                                        bool booleanValue = true;
                                        data.Add(lstHeaders[j], booleanValue);
                                        break;
                                    default:
                                        Console.WriteLine("Ha ocurrido un error inesperado");
                                        break;
                                }
                                Console.WriteLine("Celda Vacia, se relleno con el valor por defecto");
                            }
                        }
                        if (cellTypes.Count != lstHeaders.Count && flag)
                        {
                            cellTypes.Clear();
                        }
                        else
                        {
                            flag = false;
                        }
                        BsonDocument document = new BsonDocument();
                        /*foreach(var variable in data)
                        {
                            document.Add(variable.Key, BsonValue.Create(variable.Value));
                        }*/
                        clsMongoConection.SubirDatos(dbName, data.ToBsonDocument());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
