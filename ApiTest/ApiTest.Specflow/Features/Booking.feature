Feature: Pet
https://petstore.swagger.io/#/pe

@pet
Scenario: Get Booking By Id
	Given I get booking id
	When I get booking by id '34'
	Then The result contains:
	| name       | value |
	| FirstName  | Sally |
	| LastName   | Brown |
	| TotalPrice | 111   |

