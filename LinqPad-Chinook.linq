<Query Kind="Statements">
  <Connection>
    <ID>6f927bd0-d320-4c6c-a973-1ab7f1f6e543</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

// List all customers served by employee Jane Peacock showthe customer LastName, FirstName, City, State, PHone, Email
// fROM Child to Parent
from x in Customers
where x.SupportRepIdEmployee.FirstName.Equals("Jane") && x.SupportRepIdEmployee.LastName.Equals("Peacock")
select new CheckCustomer {Name = x.LastName + ", " + x.FirstName,
			City = x.City,  
			State = x.State, 
			Phone = x.Phone, 
			Email = x.Email}
			
// List a Number Tracks in an Albums
// Use of Aggregates in Queries, Parent to Child in Aggregate ALWAYS
from x in Albums
orderby x.Title ascending // ascending is the default
select new {Album_Title = x.Title,
			Count = x.Tracks.Count()}
			
// Find what is the price for all the tracks for that Album

from x in Albums
orderby x.Title ascending
where x.Tracks.Count() > 0
select new {Album_Title = x.Title,
			Count = x.Tracks.Count(),
			TotalUnitPrice = x.Tracks.Sum(y => y.UnitPrice)}

from x in Albums
orderby x.Title ascending
where x.Tracks.Count() > 0
select new {Album_Title = x.Title,
			Count = x.Tracks.Count(),
			}
		
// Averages a specific field or expression that you might need to use a delegate
from x in Albums
orderby x.Title ascending
where x.Tracks.Count() > 0
select new {Album_Title = x.Title,
			Count = x.Tracks.Count(),
			TotalUnitPrice = x.Tracks.Sum(y => y.UnitPrice),
			AverageTrackLengthinSecondsA = x.Tracks.Average(y => y.Milliseconds/1000),
			AverageTrackLengthinSecondsB = (x.Tracks.Average(y => y.Milliseconds))/1000
			}
			
// Looking for album with no null using Union

// To be Continued

/*(from x in Albums
orderby x.Title ascending
where x.Tracks.Count() > 0
select new {Album_Title = x.Title,
			Count = x.Tracks.Count(),
			TotalUnitPrice = x.Tracks.Sum(y => y.UnitPrice),
			AverageTrackLengthinSecondsA = x.Tracks.Average(y => y.Milliseconds/1000),
			AverageTrackLengthinSecondsB = (x.Tracks.Average(y => y.Milliseconds))/1000
			}).Union(
			
from x in Albums
orderby x.Title ascending
where x.Tracks.Count() > 0
select new {Album_Title = x.Title,
			Count = 0,
			TotalUnitPrice = 0,
			AverageTrackLengthinSecondsA = 0.00m,
			AverageTrackLengthinSecondsB = 0.00m
			});
*/

// Know the media type of the most tracks
// Working with mulitple steps problem you need to switch from either Statement ir Program
// the result of each quiry will be save in a variable then can be use in future queries

var maxcount = (from x in MediaTypes
	select x.Tracks.Count()).Max();
	
// to dispaly the contents of a variable in LinqPad you use the method .Dump()

maxcount.Dump();

// use a value in preceeding create Variable
var popularMediaType = from x in MediaTypes
where x.Tracks.Count() == maxcount
select new {
			Type = x.Name,
			TypeCount = x.Tracks.Count()};
			
popularMediaType.Dump();

// Using SubQuery in the previous problem
var popularMediaTypez = from x in MediaTypes
	where x.Tracks.Count() == (from y in MediaTypes 
								select y.Tracks.Count()).Max()
	select new {Type = x.Name,
				TypeCount = x.Tracks.Count()};
				
popularMediaTypez.Dump();

//Using SubQuery and Method Type
var popularMediaTypeSubMethod = from x in MediaTypes
	where x.Tracks.Count() == MediaTypes.Select(y => y.Tracks.Count()).Max()
	select new {Type = x.Name,
				TypeCount = x.Tracks.Count()};
				
popularMediaTypeSubMethod.Dump();