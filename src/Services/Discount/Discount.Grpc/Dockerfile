# Stage 1: base runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG APP_UID=1001
ARG APP_GID=1001
# Create user & group
RUN addgroup --gid $APP_GID appgroup \
    && adduser --uid $APP_UID --gid $APP_GID --disabled-password --gecos "" appuser
WORKDIR /app
# Create writable db directory for SQLite
RUN mkdir -p /app/db && chown -R appuser:appgroup /app/db
EXPOSE 8080
EXPOSE 8081

# Stage 2: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Discount/Discount.Grpc/Discount.Grpc.csproj", "Services/Discount/Discount.Grpc/"]
RUN dotnet restore "Services/Discount/Discount.Grpc/Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Discount.Grpc"
RUN dotnet build "./Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: final
FROM base AS final
COPY --from=publish /app/publish .
# Set runtime user
USER appuser
# Point SQLite to writable location
ENV ConnectionStrings__Database=Data Source=/app/db/DiscountDb.db
ENTRYPOINT ["dotnet", "Discount.Grpc.dll"]