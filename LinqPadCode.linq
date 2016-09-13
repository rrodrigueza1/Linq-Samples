<Query Kind="Expression">
  <Connection>
    <ID>ad741726-09be-4ee4-b05d-6bc95395d63e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//Waiters

// C# Expression
from x in Waiters
where x.FirstName.Contains("A")
orderby x.LastName
select x.FirstName + ", " + x.LastName + ". "

from x in Waiters
where x.FirstName.StartsWith("A")
orderby x.LastName
select x.FirstName + ", " + x.LastName + ". "

from x in Items
where x.CurrentPrice > 5.00m
select new{ x.Description, x.Calories}
// C#
// Run the whole thing in order to apply;
var results = from x in Waiters
where x.FirstName.Contains("a")
orderby x.LastName
select x.FirstName + ", " + x.LastName + ". ";
results.Dump();
