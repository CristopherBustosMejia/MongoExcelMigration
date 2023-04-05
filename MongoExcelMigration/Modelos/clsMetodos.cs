using MongoDB.Bson;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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
        public static void ReadExcel()
        {
            try
            {
                String filepath = @"D:\cris-\Descargas\R01-Colaboradores.xlsx";
                FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                IWorkbook workbook = new XSSFWorkbook(fs);
                ISheet sheet = workbook.GetSheetAt(0);
                DataFormatter dataf = new DataFormatter();
                if (sheet == null) 
                    return;
                IRow headRow = sheet.GetRow(5);
                for(int i = 6; i < sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    mdlEmplea.mdl_Emplea employee = new mdlEmplea.mdl_Emplea();
                    mdlEmplea.mdl_Beneficiario benefe = new mdlEmplea.mdl_Beneficiario();
                    for (int j = 0; i > row.Cells.Count; j++)
                    {
                        switch (dataf.FormatCellValue(headRow.GetCell(j)))
                        {

                            case "e-Mail e-Mail":
                                employee.sEm_email = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Tel.  ":
                                employee.sRh_telefo = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Escolaridad":
                                employee.sRh_escolar = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Cd. Nac.":
                                employee.sRh_nciudad = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Edo. Nac.":
                                employee.sRh_nestado = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Calle":
                                employee.sRh_dcalle = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Colonia":
                                employee.sRh_dcolon = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Ciudad":
                                employee.sRh_destado = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Estado":
                                employee.sRh_destado = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Municipio":
                                employee.sRh_dmunici = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "C.P.":
                                employee.sRh_dcp = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Nombre del padre":
                                employee.sRh_npadre = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Nombre del madre":
                                employee.sRh_npadre = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "P Fin":
                                employee.iRh_fpadre = int.Parse(dataf.FormatCellValue(row.GetCell(j)));
                                break;

                            case "M Fin":
                                employee.iRh_fmadre = int.Parse(dataf.FormatCellValue(row.GetCell(j)));
                                break;

                            case "Nacionalidad":
                                employee.sRh_nacion = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "1er. Asegurado GMM":
                                employee.sRh_gmmaseg = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "F.Nac. Asegurado":
                                employee.dtRh_gmmfnac = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Sexo":
                                employee.sRh_gmmsexo = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Paren":
                                employee.sRh_gmmpare = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Nom. p/repor emergencia":
                                employee.fRh_gmmpor = row.GetCell(j).NumericCellValue;
                                break;

                            case "Primer Nombre p/emergencia":
                                employee.sRh_noavis1 = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Tel.del emergente":
                                employee.sRh_teavis1 = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Parentesco":
                                if (employee.sRh_paavis1 == null)
                                {
                                    employee.sRh_paavis1 = dataf.FormatCellValue(row.GetCell(j));
                                }
                                else
                                {
                                    employee.sRh_paavis2 = dataf.FormatCellValue(row.GetCell(j));
                                }
                                break;

                            case "Segundo Nombre de emergencia":
                                employee.sRh_noavis1 = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Tel del emergente":
                                employee.sRh_teavis2 = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Fotografia Asoc.":
                                employee.sRh_picture = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Cve. P.GMM":
                                employee.sRh_gmmpcve = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Area Col":
                                employee.fRh_area = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Oficio":
                                employee.sRh_oficio = dataf.FormatCellValue(row.GetCell(j));
                                break;
                            
                            case "Estat":
                                benefe.fBe_estatur = row.GetCell(j).NumericCellValue;
                                break;

                            case "Peso":
                                benefe.fBe_peso = row.GetCell(j).NumericCellValue;
                                break;

                            case "GMM":
                                employee.sRh_gmm = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Seg Vida":
                                employee.sRh_gmmsgv = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Suma  Aseg. GMM":
                                employee.fRh_gmmsuma = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Plan Seg.  Vida":
                                employee.iRh_plansv = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "P.A. S.V.":
                                employee.iRh_aplansv = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Prima Aseg.S.V.":
                                employee.fRh_psvsuma = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Ubicación del Colaborador":
                                employee.sRh_ubicado = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Estat.":
                                employee.fRh_estatu = dataf.FormatCellValue(row.GetCell(j));
                                break;

                            case "Peso":
                                lstHeaders.Add("rh_peso");

                                break;

                            case "Talla Camisa":
                                lstHeaders.Add("rh_tallac");
                                break;

                            case "Talla Pantalon":
                                lstHeaders.Add("rh_tallap");
                                break;

                            case "Calzado":
                                lstHeaders.Add("rh_calzado");
                                break;

                            case "Color Ojos":
                                lstHeaders.Add("rh_coloroj");
                                break;

                            case "Color Cabello":
                                lstHeaders.Add("rh_colorca");
                                break;

                            case "Color Piel":
                                lstHeaders.Add("rh_piel");
                                break;

                            case "Señas Particulares":
                                lstHeaders.Add("rh_separt");
                                break;

                            case "Estudio Soc-Eco":
                                lstHeaders.Add("rh_soceco");
                                break;

                            case "Alta Seg. Pub.":
                                lstHeaders.Add("rh_segpub");
                                break;

                            case "Examen Antidoping":
                                lstHeaders.Add("rh_antidop");
                                break;
                        }
                    }
                    clsMongoConection.SubirDatos("", employee);
                }
                
 
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
