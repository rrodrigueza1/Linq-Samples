<Query Kind="Statements">
  <Connection>
    <ID>6f927bd0-d320-4c6c-a973-1ab7f1f6e543</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

var MaxBills = (from x in Waiters
select x.Bills.Count()).Max();

var MaxWaiterBills = from x in Waiters
where x.Bills.Count() == MaxBills
select new 	{ WaiterID = x.WaiterID,
			WaiterName = x.FirstName + ", " + x.LastName,
			BillCount = MaxBills,
			};
			
MaxWaiterBills.Dump();

// Create a data set which contains the summary bill info by waiter
var waiterBills = 	from x in Waiters
					orderby x.LastName, x.FirstName
					select new {Name = x.LastName + " ," + x.FirstName, 
								BillInfo = (from y in x.Bills
											where y.BillItems.Count () > 0
											select new { BillID = y.BillID,
														 BillDate = y.BillDate,
														 TableID = y.TableID,
														 Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
														}
											)
								};

waiterBills.Dump();