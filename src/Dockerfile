FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
RUN apt-get update
RUN apt-get install --no-install-recommends -y python3
RUN dotnet --info
RUN dotnet workload install wasm-tools
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app/publish --nologo

FROM caddy:latest AS final
WORKDIR /app
COPY --from=publish /app/publish/wwwroot .
ENTRYPOINT ["caddy", "run"]