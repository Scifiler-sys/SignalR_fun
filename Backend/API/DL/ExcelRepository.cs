using Backend.Models;
using Ganss.Excel;

namespace Backend.DL
{
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