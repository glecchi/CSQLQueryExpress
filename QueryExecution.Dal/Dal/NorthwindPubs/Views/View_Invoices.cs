
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQLQueryBuilder;
using SQLQueryBuilder.Schema;

namespace QueryExecution.Dal.NorthwindPubs
{
    public partial class dbo
    {
	    [Table("Invoices", Schema = "dbo")]
	    public class View_Invoices : ISQLQueryEntity
	    {
		    [Column("ShipName")]
		    public string ShipName { get; set; }

		    [Column("ShipAddress")]
		    public string ShipAddress { get; set; }

		    [Column("ShipCity")]
		    public string ShipCity { get; set; }

		    [Column("ShipRegion")]
		    public string ShipRegion { get; set; }

		    [Column("ShipPostalCode")]
		    public string ShipPostalCode { get; set; }

		    [Column("ShipCountry")]
		    public string ShipCountry { get; set; }

		    [Column("CustomerID")]
		    public string CustomerID { get; set; }

		    [Required]
		    [Column("CustomerName")]
		    public string CustomerName { get; set; }

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

		    [Required]
		    [Column("Salesperson")]
		    public string Salesperson { get; set; }

		    [Column("OrderID")]
		    public int OrderID { get; set; }

		    [Column("OrderDate")]
		    public DateTime? OrderDate { get; set; }

		    [Column("RequiredDate")]
		    public DateTime? RequiredDate { get; set; }

		    [Column("ShippedDate")]
		    public DateTime? ShippedDate { get; set; }

		    [Required]
		    [Column("ShipperName")]
		    public string ShipperName { get; set; }

		    [Column("ProductID")]
		    public int ProductID { get; set; }

		    [Required]
		    [Column("ProductName")]
		    public string ProductName { get; set; }

		    [Column("UnitPrice")]
		    public decimal UnitPrice { get; set; }

		    [Column("Quantity")]
		    public short Quantity { get; set; }

		    [Column("Discount")]
		    public float Discount { get; set; }

		    [Column("ExtendedPrice")]
		    public decimal? ExtendedPrice { get; set; }

		    [Column("Freight")]
		    public decimal? Freight { get; set; }

	    }
    }
}