# Card Management module and Payment Fees module

To test this API you should follow these steps:

1. At first you need to check the database ConnectionString in appsettings.Development.json file. This project is configured to automatically create the database from scratch when you run it.

2. You should authenticate using login endpoint from Authentication module. After running the login endpoint, a token will be generated. For authentication, there are two users that you can authenticate:
    - Admin: User with "Admin" role. Users with "Admin" role are authorized to run any endpoint from Card Management module. The credentials of this user are:
    UserName: "admin"
    Password: "adminpassword"
  
    - User: User with "User" role. Users with "User" role are only authorized to run get-card-balance and ​pay endpoints from Card Management module. The credentials of this user are:
    UserName = "user"
    Password = "userpassword"

3. You should be authorized, otherwise any endpoint from Card Management module will return the error code 401 (Unauthorized). To be authorized, you can do it in two ways:
    - If you are consuming the API from a browser, press SWAGGER's "Authorize" button and paste the generated token described above.
    - If you are consuming the API from POSTMAN, go to the "Authorization" tab, choose the "Bearer Token" type and within "Token" text box paste the generated token described above.

4. Once you are authenticated and authorized, you can test the following endpoints from Card Management module:

    - ​create-card: It allows you to create new credit cards.
    - get-card-balance: It allows you to get current balance of an existing credit card.
    - pay: It allows you to register new payments to an existing credit card.

5. Payment fees is calculated within the Card Management module and used when executing pay endpoint.
