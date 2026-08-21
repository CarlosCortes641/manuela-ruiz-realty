# Build with low memory (Render Free builders are tight).
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish src/ManuelaRuiz.Web/ManuelaRuiz.Web.csproj \
    -c Release \
    -r linux-musl-x64 \
    --self-contained false \
    -o /app/publish \
    -p:PublishReadyToRun=true \
    -p:UseSharedCompilation=false \
    -m:1

# Smaller Alpine runtime + ICU (needed for EN/ES cultures).
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
RUN apk add --no-cache icu-libs icu-data-full tzdata
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    DOTNET_EnableDiagnostics=0 \
    DOTNET_GCServer=0 \
    DOTNET_ThreadPool_ForceMinWorkerThreads=1 \
    DOTNET_ThreadPool_ForceMaxWorkerThreads=4 \
    LANG=en_US.UTF-8 \
    LC_ALL=en_US.UTF-8

EXPOSE 8080
ENTRYPOINT ["/bin/sh", "-c", "dotnet ManuelaRuiz.Web.dll --urls http://0.0.0.0:${PORT:-8080}"]
