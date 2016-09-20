<Query Kind="Program">
  <Connection>
    <ID>6f927bd0-d320-4c6c-a973-1ab7f1f6e543</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	// a list of bill counts for all waiters
	// this query will create a flat dataset
	// the columns are native data types (ie int, string ....)
	// One is not concerned with repeated data in a column
	// insteand of using an anonymous datatype (new {....})
	// We wish to use a defined class definition

	var MaxWaiterBills = 	from x in Waiters
							select new WaiterBillCounts { 	WaiterID = x.WaiterID,
											WaiterName = x.FirstName + ", " + x.LastName,
											BillCount = x.Bills.Count(),
										};
										
MaxWaiterBills.Dump();

var waiterBills = 	from x in Waiters
					orderby x.LastName, x.FirstName
					where x.LastName.Contains("K")
					select new WaiterList {Name = x.LastName + " ," + x.FirstName, 
								TotalBill = x.Bills.Count(),
								BillInfo = (from y in x.Bills
											where y.BillItems.Count () > 0
											select new BillItemSummary { BillID = y.BillID,
														 BillDate = y.BillDate,
														 TableID = y.TableID,
														 Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
														}
											).ToList()
								};

waiterBills.Dump();
}

// Define other methods and classes here
// An example of POCO Class {flat}
public class WaiterBillCounts
{
	//Whatever recieving field on you query in your select appears as a property in this class
	public int WaiterID{get; set;}
	public string WaiterName{get; set;}
	public int BillCount{get; set;}
}

public class BillItemSummary
{
	public int BillID {get; set;}
	public DateTime BillDate {get; set;}
	public int? TableID {get; set;}
	public decimal Total {get; set;}
}

// An Example of DTO Class {structure}
public class WaiterList
{
	public string Name {get; set;}
	public int TotalBill {get; set;}
	//public IEnumerable<BillItemSummary> BillInfo {get; set;}
	public List<BillItemSummary> BillInfo {get; set;}
}
