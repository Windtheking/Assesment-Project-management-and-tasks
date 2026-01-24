# This folder 

<h5>Is the one that changes the most using as reference the basic Repository model</h5>

This folder must follow this working tree

```
Application
├── Interfaces
├── Services
├── DTOs
└── Validators

```


<h3>Interfaces</h3>
On the domain there are interfaces but you can also have the interfaces here 
as long as they are not part of the main bussines logic for exmaple.

in the domain you would use entities like "client" or "products" 
but on the apllication interfaces you would use autentication or pagination

<h3>Services</h3>
Here goes the services we created like the premade querys and the pagination of the queries we do to the database

<h3>DTO</h3>
This is like a data depurator, it only returns what needs to be read, if for 
example I recover sensitive data like crdit card numbers I just return the name 
and the email of the card holder with the dto 
<br>
<h3>Validators</h3>
Here is where you create the methods to avoid receiving wrong data, 
like negative numbers, alphabetic characters in number fields, empty 
spaces and so on.
