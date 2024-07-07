using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSQLQueryExpress;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Employees", Schema = "dbo")]
	    public class Employees : ISQLQueryEntity
	    {
		    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		    [Column("EmployeeID")]
		    public int EmployeeID { get; set; }

		    [Required]
		    [Column("LastName")]
		    public string LastName { get; set; }

		    [Required]
		    [Column("FirstName")]
		    public string FirstName { get; set; }

		    [Column("Title")]
		    public string Title { get; set; }

		    [Column("TitleOfCourtesy")]
		    public string TitleOfCourtesy { get; set; }

		    [Column("BirthDate")]
		    public DateTime? BirthDate { get; set; }

		    [Column("HireDate")]
		    public DateTime? HireDate { get; set; }

		    [Column("Address")]
		    public string Address { get; set; }

		    [Column("City")]
		    public string City { get; set; }

		    [Column("Region")]
		    public string Region { get; set; }

		    [Column("PostalCode")]
		    public string PostalCode { get; set; }

		    [Column("Country")]
		    public string Country { get; set; }

		    [Column("HomePhone")]
		    public string HomePhone { get; set; }

		    [Column("Extension")]
		    public string Extension { get; set; }

		    [Column("Photo")]
		    public byte[] Photo { get; set; }

		    [Column("Notes")]
		    public string Notes { get; set; }

		    [Column("ReportsTo")]
		    public int? ReportsTo { get; set; }

		    [Column("PhotoPath")]
		    public string PhotoPath { get; set; }

	    }
    }
}