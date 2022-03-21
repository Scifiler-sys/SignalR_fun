using Ganss.Excel;

namespace Models
{
    /// <summary>
    /// Actual model that gets mapped into via the excel sheet
    /// </summary>
    public class Population
    {
        public int LocID { get; set; }
        public string Location { get; set; }
        [Column("PopMale")]
        public double MalePopulation { get; set; }
        [Column("PopFemale")]
        public double FemalePopulation { get; set; }
        [Column("PopTotal")]
        public double TotalPopulation { get; set; }
        public int Time { get; set; }
    }
}