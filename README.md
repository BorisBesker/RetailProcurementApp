# RetailProcurementApp


## Endpoints:

### Store Items Controller:

● GET /api/store-items: Retrieve a list of all store items. 

● GET /api/store-items/{id}: Retrieve details of a specific store item.

● POST /api/store-items: Add a new store item.

● PUT /api/store-items/{id}: Update details of a specific store item.

● DELETE /api/store-items/{id}: Delete a specific store item.

### Suppliers Controller:

● GET /api/suppliers: Retrieve a list of all suppliers.

● GET /api/suppliers/{id}: Retrieve details of a specific supplier.

● POST /api/suppliers: Add a new supplier.

● PUT /api/suppliers/{id}: Update details of a specific supplier.

● DELETE /api/suppliers/{id}: Delete a specific supplier.


### Supplier-Store Items Controller (Many-to-Many Relationship):

● GET /api/supplier-store-items: Retrieve all relationships between suppliers and store
items.

● POST /api/supplier-store-items: Create a new relationship between a supplier and a
store item.

● DELETE /api/supplier-store-items/{supplierId}/{storeId}: Remove a relationship between
a supplier and a store item.


### User Controller 

● POST /api/user/login: Gets token for autenthication

● POST /api/user/register: Registers a new user


## Requirements 

● .NET 8.0

●  Git 

●  Microsoft® SQL Server® 2019 Express (for local setup)

●  Docker Engine (for running docker containers)

## Setup

```
git clone https://github.com/BorisBesker/RetailProcurementApp.git
```

### Local
●  Set your db connection string 
```
  "Data": {
    "DefaultConnection": {
      "ConnectionString": "Server=localhost\\SQLEXPRESS;Database=RetailProcurement;Trusted_Connection=True;TrustServerCertificate=True"
    }
  },
```
```
dotnet restore .\RetailProcurementApp\RetailProcurementApp\
dotnet build .\RetailProcurementApp\RetailProcurementApp\
```

●  Run tests
```
dotnet test .\RetailProcurementApp\RetailProcurementApp\
```

●  Run app
```
dotnet run --project .\RetailProcurementApp\RetailProcurementApp\
```

Open [https://localhost:7222/swagger/index.html](https://localhost:7222/swagger/index.html) and take a look around.


### Docker
●  Build docker images 

```
docker-compose -f ./RetailProcurementApp/t/docker-compose.yml build
```

●  Run Containers 

```
docker-compose -f ./RetailProcurementApp/docker-compose.yml up -d
```

Run command to chek container state

```
docker-ps
```

<img width="911" alt="image" src="https://github.com/BorisBesker/RetailProcurementApp/assets/26566198/106caa2a-b549-42f6-9f3b-0ab86746d0e4">


Open [https://localhost:8000/swagger/index.html](http://localhost:8080/swagger/index.html) and take a look around.

## Assumptions, limitations, or additional considerations

●  Unit and intergations test are written for StoreItemController and Service 

●  Potenntial db schema for statistics controller 

<img width="935" alt="Screenshot 2024-01-27 162628" src="https://github.com/BorisBesker/RetailProcurementApp/assets/26566198/f74c08e2-6182-47c1-966a-7fc5f1994d4e">



