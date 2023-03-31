using MongoExcelMigration.Modelos;
using NPOI.SS.Formula.Functions;

bool flag = true;

while (flag)
{
    Console.WriteLine("Elige una opcion a realizar:\n1.-Migrar Excel a Mongo\n2.-Salir");
    try
    {
        int option = int.Parse(Console.ReadLine());
        switch (option)
        {
            case 1:
                {
                    try
                    {
                        Console.WriteLine("Ingrese el nombre del archivo de Excel (Ejemplo.xlsx)");
                        String path = Console.ReadLine();
                        Console.WriteLine("Ingrese el valor de la fila en que comienzan los encabezados de la tabla (Ejem. 0)");
                        int initTable = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese el nombre de la base de datos en MongoDB (Ejem. Ventas)");
                        String dbName = Console.ReadLine();
                        clsMetodos.ReadExcel(path, initTable, dbName);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                break;
            case 2:
                {
                    flag = false;
                }
                break;
            default:
                {
                    Console.WriteLine("Porfavor ingrese un valor valido");
                }
                break;
        }
    }catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    Console.WriteLine("Presione una tecla para continuar");
    Console.ReadKey();
    Console.Clear();
}