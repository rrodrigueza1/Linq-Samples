<Query Kind="Expression">
  <Connection>
    <ID>6f927bd0-d320-4c6c-a973-1ab7f1f6e543</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

// Multi column group 
// grouping data place in a local TempDataSet for furthur processing
//.Key allows you to have access to the values in your Group Key(s)
// if you have multiple group columns they must be in an anonymous data type
// To create a DTO type collection you can use .ToList() on the TempDataSet 
// you can have a custom anonymous collection by using a nested query
from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice} into TempDataSet
	select new {MenuCategoryID = TempDataSet.Key.MenuCategoryID,
				CurrentPrice = TempDataSet.Key.CurrentPrice,
				FoodItems = TempDataSet.ToList()}

// multi group column with subquery
from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice} into TempDataSet
	select new {MenuCategoryID = TempDataSet.Key.MenuCategoryID,
				CurrentPrice = TempDataSet.Key.CurrentPrice,
				FoodItems = from x in TempDataSet 
							select new {
										ItemID = x.ItemID,
										ItemDescription = x.Description,
										ItemCurrentCost = x.CurrentCost,
										TimesServe = x.BillItems.Count()
										}
							
						}

