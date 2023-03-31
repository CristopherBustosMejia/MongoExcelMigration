using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace MongoExcelMigration.Modelos
{
    public static class clsMongoConection
    {
        public static void SubirDatos(String sDBNombre,BsonDocument oDocumento)
        {
            try
            {
                MongoClient oClient = new MongoClient("mongodb://localhost:27017");

                var oDatabase = oClient.GetDatabase(sDBNombre);

                var coleccion = oDatabase.GetCollection<BsonDocument>("datos");
                coleccion.InsertOne(oDocumento);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       
    }
}
