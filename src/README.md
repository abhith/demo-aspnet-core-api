# Complaints API

DEMO API project.

## Features

### Exception handling

Exception handling for APIs baked in using [Code.Library](https://code-library.abhith.net/docs/articles/aspnet-core/api-exception-handling.html)

### Logging

Extensive logging enabled by using [Code.Library](https://code-library.abhith.net/docs/articles/aspnet-core/logging.html).
You may able to see the automatically produced logs in the console.

### API Versioning

API versioning enabled in the project.

### API Documentation

Project configured to enable Swagger UI. Also, it supports versioned APIs.

### Security

Security to API endpoints can be achieved using by setting up the ASP.NET Core Authorization middle-ware.
I have an article on a similar topic here. And sample code commented in the project `Startup.cs` for reference even though I don't prefer to have commented code in the repo.

- [ASP.NET Core - Using Mutliple Authentication Schemes](https://www.abhith.net/blog/aspnet-core-using-multiple-authentication-schemes/)

### Testing

For testing, I prefer to use xUnit along with [FluentAssertions](https://fluentassertions.com/).

### Fault tolerance

I used to use **Polly** to enable Fault tolerance.

### Health Checks

Health checks enabled using [Code.Library](https://code-library.abhith.net/docs/articles/aspnet-core/default-health-checks.html)

## Other Notes

- I tend to use Dapper for repository implementation as well as for Queries
- Some useful behaviors configured in the Application (MediatR)

### Additional Resources

- [abhith.net Website](https://www.abhith.net/)
- [Code.Library Website](https://code-library.abhith.net/)
