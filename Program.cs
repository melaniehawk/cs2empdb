using System.Runtime.ExceptionServices;


namespace EmpDB
{
    internal class Program
    {
        static void Main()
        {
            DbApp app = new DbApp();

            app.ReadEmployeeDataFromInputFile();

            app.RunDatabaseApp();
        }
    }
}
