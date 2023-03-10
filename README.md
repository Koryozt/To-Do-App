# Re:Mindme

Simple To Do Application with a basic CRUD and some other funtionalities. 

### Development

--- 

The application was made using Angular, Bootstrap and SASS as Frontend, .NET as Backend and using MS SQL as Database to store the tasks. The frontend connects with the backend through ToDoService. This service sends a request to the server and the server replies with a response. The toastr service shows the state of the request, if an error has ocurred then you'll see a little red box indicating that something went wrong.

### Functionality

---

It's so simple, there are 2 tables; the left table shows the current tasks list, giving you the options to read the data, modify a single task, delete a single task or check it done (still in development 'cause when you refresh the page will be unchecked). In the right side you have the Add/Edit table, where you have to put the data and this has to be valid, or either it won't add the task.

![Screenshot 2022-08-24 160921](https://user-images.githubusercontent.com/93677342/186517330-91bdfdc4-9335-498d-a0e4-44c3b3e6d8fc.png)

### Validations

Before listing the validations I have to say that currently the component does not implement a validation in its forms, it will be added soon.

- All inputs are required.
- The title must be at least 3 characters and don't exceed more than 24 characters.
- The description has no limitations so feel free to write all you want.
- The data must be added in the format YYYY-MM-DD or it won't be valid.
- Priority goes from 1 (the less importants) to 5 (the most importants).

<br>

![Screenshot 2022-08-24 161022](https://user-images.githubusercontent.com/93677342/186519850-825d3bdb-9982-49f6-9011-719937787b54.png)

---

![Screenshot 2022-08-24 161157](https://user-images.githubusercontent.com/93677342/186519904-fb65635a-7ac5-4ffd-b05c-898320036dd2.png)
