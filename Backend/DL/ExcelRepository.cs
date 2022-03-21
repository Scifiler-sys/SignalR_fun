using Ganss.Excel;
using Models;

namespace DL
{
    /// <summary>
    /// Using Excel as our database and an ExcelMapper library is used to transfer data from excel into an object C# understands
    /// </summary>
    public class ExcelRepository : IRepository<Population>
    {
        private readonly List<Population> _dataSource;

        public ExcelRepository(string p_filepath)
        {
            //Attach our data source to the excel sheet
            //Since the excel sheet is the database at the moment
            _dataSource = new ExcelMapper(p_filepath).Fetch<Population>().ToList();
        }

        public List<Population> read()
        {
            return _dataSource;
        }
    }
}