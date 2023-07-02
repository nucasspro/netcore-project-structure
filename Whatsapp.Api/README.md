### How to run migration
- Add migration
```bash
dotnet ef migrations add "{migrations_name}" --project .\Whatsapp.Infrastructure --startup-project .\Whatsapp.Api --output-dir Persistence\Migrations
```

- Remove migration
```bash
dotnet ef migrations remove "{migrations_name}" --project .\Whatsapp.Infrastructure --startup-project .\Whatsapp.Api
```

- Update database
```bash
dotnet ef database update --project .\Whatsapp.Infrastructure --startup-project .\Whatsapp.Api
```


## Project references
```csharp
- [Common.Logging] -> 
- [Contracts] ->
- [Infrastructure] -> Contracts
- [Shared] ->

- [Whatsapp.Api] -> 
                    Common.Logging,
                    Whatsapp.Application,
                    Whatsapp.Infrastructure
                    
- [Whatsapp.Application] -> 
                    Shared, 
                    Whatsapp.Domain
                    
- [Whatsapp.Domain] -> 
                    Contracts
                    
- [Whatsapp.Infrastructure] -> 
                    Infrastructure, 
                    Whatsapp.Application
```