# Csharp JWT Authentication sample

This is a Csharp Web API that supports username and password authentication with JWTs and has APIs that return user information to the GUI client and vice-versa.

Everything about the Csharp WEB API runs from the `Api` Directory.

The Desktop C# app runs from the `WpfClient` Directory.

## Available APIs

### User APIs

#### POST `/users`

You can do a POST to `/users` to create a new user.

```json
{
  "username": "<username>",

  "firstname": "<firstname>",

  "middlename": "<middlename>",

  "lastname": "<lastname>",

  "age": <age>
}
```

The body must have:

* `username`: The username
* `password`: The password

It returns the following:

```json
{

  "id": "<id>",

  "username": "<username>",

  "access_token": "<jwt>"
}
```

That JWT will contain the `id`, `username` and an `expires` indicating when the token will expire.

#### POST `/users/login`

You can do a POST to `/users/login` to log a user in.

The body must have:

* `username`: The username
* `password`: The password

It returns the following:

```json
{

  "id": "<id>",

  "username": "<username>",

  "access_token": "<jwt>"
}
```

That JWT will contain the `id`, `username` and an `expires` indicating when the token will expire.

### User API

#### GET `/api/users/{id}`

Where `id` is the Id of a user

It returns the complete user information

```json
{
  "id": "<id>",
  "username": "<username>",

  "firstname": "<firstname>",

  "middlename": "<middlename>",

  "lastname": "<lastname>",

  "age": <age>
}
```

The JWT must be sent on the `Authorization` header as follows: `Authorization: <jwt>`

## Running it

Just clone the repository, and launch the solution in Visual Studio. That's it :).
This project was built using Visual Studio 2015.

## Issue Reporting

If you have found a bug or if you have a feature request, please report them at this repository issues section.

## Author

[Prosper Otemuyiwa](https://twitter.com/unicodeveloper)

## License

This project is licensed under the MIT license. See the [LICENSE](LICENSE) file for more info.
