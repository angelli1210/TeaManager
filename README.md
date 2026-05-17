# TeaManager

A full-stack web application for managing tea products, brands, suppliers, and supplier orders.

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Frontend | React 19, Vite, Tailwind CSS, React Router DOM, Axios |
| Backend | ASP.NET Core 10.0, Entity Framework Core |
| Database | SQL Server |

---

## Features

- **Products** — Add, edit, delete, and search tea products with brand/supplier linkage, price, stock, origin, and harvest year
- **Brands** — Manage tea brands with contact info, country, and business details
- **Suppliers** — Manage suppliers with contact and country information
- **Orders** — Track supplier orders linked to specific products and suppliers
- **Overview Dashboard** — Real-time stats (total products, brands, suppliers, orders) and recent products table
- **Stock Indicators** — Color-coded stock levels (green / yellow / red)
- **Form Validation** — Client-side and server-side validation with error messages
- **Global Error Handling** — Standardized JSON error responses from the API

---

## Project Structure

```
Asg_1/
├── TeaManager.API/          # ASP.NET Core backend
│   ├── Controllers/         # REST API endpoints
│   ├── Services/            # Business logic layer
│   ├── Repositories/        # Data access layer
│   ├── Models/              # Domain entities
│   ├── DTOs/                # Request & Response DTOs
│   ├── Data/                # EF Core DbContext + seed data
│   └── Middleware/          # Global exception handler
├── Tea-Manager-app/         # React frontend
│   └── src/
│       ├── API/             # Axios service layer
│       ├── pages/           # Page components
│       └── components/      # Shared UI components
└── tea-manager-schema.sql   # Database schema
```

---

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/)
- [Node.js](https://nodejs.org/) (v18+)
- SQL Server (local instance)

### 1. Database Setup

Create the database and run the schema:

```sql
-- Run tea-manager-schema.sql in SQL Server Management Studio
-- or via sqlcmd:
sqlcmd -S YourServerName -i tea-manager-schema.sql
```

Update the connection string in `TeaManager.API/appsettings.json`:

```json
"ConnectionStrings": {
  "TeaManagerDbConnectionString": "Server=YourServerName;Database=TeaManagerDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 2. Run the Backend

```bash
cd TeaManager.API
dotnet run
```

API runs at: `http://localhost:5028`  
Swagger UI: `http://localhost:5028/swagger`

### 3. Run the Frontend

```bash
cd Tea-Manager-app
npm install
npm run dev
```

App runs at: `http://localhost:5173`

---

## API Endpoints

| Resource | Endpoint | Methods |
|----------|----------|---------|
| Brands | `/api/brands` | GET, POST |
| Brand | `/api/brands/{id}` | GET, PUT, DELETE |
| Products | `/api/products` | GET, POST |
| Product | `/api/products/{id}` | GET, PUT, DELETE |
| Suppliers | `/api/suppliers` | GET, POST |
| Supplier | `/api/suppliers/{id}` | GET, PUT, DELETE |
| Orders | `/api/supplierorders` | GET, POST |
| Order | `/api/supplierorders/{id}` | GET, PUT, DELETE |

---

## Architecture

```
Browser
  └── React SPA (Axios)
        └── ASP.NET Core API (CORS + Exception Handler + Swagger)
              ├── BrandsController          → BrandService          → BrandRepository
              ├── ProductsController        → ProductService        → ProductRepository
              ├── SuppliersController       → SupplierService       → SupplierRepository
              └── SupplierOrdersController  → SupplierOrderService  → SupplierOrderRepository
                                                                          └── EF Core → SQL Server
```

---

## Seed Data

The database is pre-loaded with sample data:
- 16 Brands (e.g. Lipston, TeaCoo, LaobanTea)
- 13 Suppliers
- 13 Products
- 11 Supplier Orders
