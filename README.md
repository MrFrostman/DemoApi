# Install Product
dotnet new -i .

# Uninstall Product
dotnet new --uninstall .

# Create Project with Product
dotnet new msgql --db Product -n Product

# Install EF
dotnet tool install --global dotnet-ef


# Create DbContext with EF & PostgreSQL
create .env for test


DB_NAME=product-cs-staging

DB_HOST=localhost

DB_PORT=5432

DATABASE_USER=user-product

DATABASE_PASSWORD=t3mpl4t3

