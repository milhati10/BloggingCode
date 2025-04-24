# Blogging Code

A blogging project developed with .NET 8.0 with possibility to create posts with comments.

## Technologies Used

- .NET 8.0 Main framework for the backend.
- SQL Server for database
- Entity framework Core for access database
- FluentValidation for input validations
- xUnit Framework for unit and integration tests.

## Run locally

Before run the project, you need run the command "docker-compose up -d" in the project root.

If would like to ensure docker compose was executed if successfully, you can run the command "docker ps" and check if the containers are running.

After run the command "dotnet restore" and then " dotnet run --project src/WebApi".

Now you can access "https://localhost:7154/swagger/index.html"

## Next steps

- Add more unit test to ensure the code is working as expected in all scenarios
- Add cache for retrieve information faster and create redundancy
- Add more integrated tests to ensure the code is working as expected in all scenarios
- Create a better documentation for the project
- Create a builder (Designer pattern) in domain layer to ensure that entities was created with all required properties