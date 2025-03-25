# BookStoreApp
Book Store App using blazor

This project uses Database first approach to generate the models and context classes.

## Local Requirements:
### Docker
- Download and install Docker from https://www.docker.com/products/docker-desktop

### Seq
- Download and install Seq from https://datalust.co/download or use docker image `datalust/seq:latest`
- When using docker, run this command to start the container:
`docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest`

### SQl Server
- Download and install SQL Server from https://www.microsoft.com/en-us/sql-server/sql-server-downloads
- Alternatively, you can use docker image `mcr.microsoft.com/mssql/server:2019-latest`
- To run the container, use this command:
`docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Admin@1234" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
`
- This will use `sa` as the username and `Admin@1234` as the password